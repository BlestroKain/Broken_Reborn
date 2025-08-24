using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Networking;
using Serilog;
namespace Intersect.Server.Entities
{
    public partial class Player : Entity
    {
        /// <summary>
        /// Calcula los puntos de experiencia modificados en función de la diferencia de niveles entre el jugador y el enemigo.
        /// </summary>
        /// <param name="enemyLevel">El nivel del enemigo.</param>
        /// <param name="exp">Los puntos de experiencia base ganados.</param>
        /// <param name="playerLevel">El nivel del jugador (opcional, con un valor predeterminado de 0).</param>
        /// <returns>Los puntos de experiencia modificados.</returns>
        public long ExpModifiedByLevel(int enemyLevel, long baseExp, int playerLevel = 0)
        {
            // Calcula la diferencia de niveles entre el jugador y el enemigo.
            int levelDiff = playerLevel == 0 ? Level - enemyLevel : playerLevel - enemyLevel;
            // Inicializa el multiplicador de experiencia como 1.0 (sin modificación).
            float expMultiplier = 1.0f;
            // Aplica modificadores basados en la diferencia de niveles.
            if (levelDiff >= 4 && levelDiff < 6)
            {
                expMultiplier = 0.8f; // Reducción del 20%
            }
            else if (levelDiff >= 6 && levelDiff < 10)
            {
                expMultiplier = 0.6f; // Reducción del 40%
            }
            else if (levelDiff >= 10)
            {
                expMultiplier = 0.2f; // Reducción del 80%
            }
            // Calcula la experiencia modificada y la convierte a long.
            long modifiedExp = (long)(baseExp * expMultiplier);
            return modifiedExp;
        }

        public bool HasSufficientCurrency(Guid currencyId, int amountRequired)
        {
            // Verifica si el jugador tiene suficiente cantidad del ítem moneda
            return FindInventoryItemQuantity(currencyId) >= amountRequired;
        }

        public bool DeductCurrency(Guid currencyId, int amount)
        {
            // Verifica si el jugador tiene suficiente cantidad antes de deducir
            if (!HasSufficientCurrency(currencyId, amount))
            {
                return false; // No hay suficientes recursos
            }

            // Deducir el ítem del inventario
            return TryTakeItem(currencyId, amount, ItemHandling.Normal, sendUpdate: true);
        }
        public void TryUpgradeItem(int itemIndex, int level, bool useAmulet = false)
        {
            if (itemIndex < 0 || itemIndex >= Items.Count)
            {
                PacketSender.SendChatMsg(this, "Índice de ítem no válido.", ChatMessageType.Error);
                return;
            }

            var item = Items[itemIndex];
            if (item == null || item.Descriptor?.ItemType != ItemType.Equipment || item.Properties == null || !item.Descriptor.CanBeEnchanted())
            {
                PacketSender.SendChatMsg(this, "El ítem no es válido o no se puede mejorar.", ChatMessageType.Error);
                return;
            }

            if (level <= item.Properties.EnchantmentLevel)
            {
                PacketSender.SendChatMsg(this, "El nivel de encantamiento debe ser superior al actual.", ChatMessageType.Error);
                return;
            }

            var upgradeMaterialId = item.Descriptor.GetUpgradeMaterialId();
            var upgradeMaterialAmount = item.Descriptor.GetUpgradeCost(level);

            var materialItem = Items.FirstOrDefault(i => i?.ItemId == upgradeMaterialId && i.Quantity >= upgradeMaterialAmount);
            if (materialItem == null || materialItem.Descriptor.ItemType != ItemType.Resource)
            {
                PacketSender.SendChatMsg(this, "El material de mejora no es válido.", ChatMessageType.Error);
                return;
            }

            // Buscamos el amuleto si se pidió usarlo
            var amuletId = item.Descriptor.GetamuletMaterialId();
            var amuletItem = useAmulet
                ? Items.FirstOrDefault(i => i?.ItemId == amuletId && i.Quantity > 0)
                : null;

            if (useAmulet && amuletItem == null)
            {
                PacketSender.SendChatMsg(this, "No tienes el amuleto de protección.", ChatMessageType.Error);
                return;
            }

            if (!DeductCurrency(upgradeMaterialId, upgradeMaterialAmount))
            {
                PacketSender.SendChatMsg(this, "Error al deducir los materiales.", ChatMessageType.Error);
                return;
            }

            var successRate = item.Descriptor.GetUpgradeSuccessRate(level);
            bool success = Random.Shared.NextDouble() <= successRate;

            using (var playerContext = DbInterface.CreatePlayerContext(readOnly: false))
            {
                try
                {
                    int[] previousStats = (int[])item.Properties.StatModifiers.Clone();
                    int[] previousVitals = (int[])item.Properties.VitalModifiers.Clone();
                    int previousLevel = item.Properties.EnchantmentLevel;

                    if (success)
                    {
                        item.ApplyEnchantment(level);
                        PacketSender.SendChatMsg(this, $"¡Encantamiento exitoso! {item.Descriptor.Name} ahora está en nivel +{level}.", ChatMessageType.Experience);
                    }
                    else
                    {
                        if (!useAmulet)
                        {
                            int newLevel = Math.Max(0, item.Properties.EnchantmentLevel - 1);
                            item.ApplyEnchantment(newLevel);
                            PacketSender.SendChatMsg(this, $"El encantamiento falló y el nivel de {item.Descriptor.Name} ha disminuido a +{newLevel}.", ChatMessageType.Error);
                        }
                        else
                        {
                            // Consumir el amuleto
                            amuletItem.Quantity -= 1;

                            if (amuletItem.Quantity <= 0)
                            {
                                Items.Remove(amuletItem);
                                playerContext.Player_Items.Remove(amuletItem);
                            }
                            else
                            {
                                playerContext.Player_Items.Update(amuletItem);
                            }

                            PacketSender.SendChatMsg(this, $"El encantamiento falló, pero el amuleto protegió el nivel +{previousLevel} de {item.Descriptor.Name}.", ChatMessageType.Notice);
                        }
                    }

                    playerContext.Players.Update(this);
                    playerContext.Player_Items.Update(item);
                    if (useAmulet && amuletItem != null)
                        playerContext.Player_Items.Update(amuletItem);

                    playerContext.SaveChanges();

                    PacketSender.SendUpdateItemLevel(this, itemIndex, item.Properties.EnchantmentLevel);
                    PacketSender.SendInventory(this);
                }
                catch (Exception ex)
                {
                    PacketSender.SendChatMsg(this, "Ocurrió un error durante la mejora del ítem.", ChatMessageType.Error);
                }
            }
        }

        public void BreakItem(int itemSlot)
        {
            if (itemSlot < 0 || itemSlot >= Items.Count)
            {
                PacketSender.SendChatMsg(this, "Índice de ítem inválido.", ChatMessageType.Error);
                return;
            }

            var item = Items[itemSlot];
            if (item == null || item.ItemId == Guid.Empty)
            {
                PacketSender.SendChatMsg(this, "No hay ítem en ese slot.", ChatMessageType.Error);
                return;
            }

            var descriptor = item.Descriptor;
            if (descriptor == null || descriptor.ItemType != ItemType.Equipment)
            {
                PacketSender.SendChatMsg(this, "Solo puedes romper equipamientos.", ChatMessageType.Error);
                return;
            }

            // Calcula runas a generar
            var runes = ItemBreakHelper.CalculateRunesFromItem(descriptor, item.Properties);
            if (runes.Count == 0)
            {
                PacketSender.SendChatMsg(this, $"{descriptor.Name} no generó runas al romperse.", ChatMessageType.Notice);
            }
            else
            {
                // Verifica espacio para todas las runas
                foreach (var rune in runes)
                {
                    if (!CanGiveItem(rune.Id, 1))
                    {
                        PacketSender.SendChatMsg(this, "No tienes suficiente espacio en el inventario para las runas.", ChatMessageType.Error);
                        return;
                    }
                }

                // Quita el ítem a romper
                if (!TryTakeItem(descriptor.Id, 1))
                {
                    PacketSender.SendChatMsg(this, "Error al quitar el ítem.", ChatMessageType.Error);
                    return;
                }

                // Da las runas calculadas
                foreach (var rune in runes)
                {
                    TryGiveItem(rune.Id, 1);
                }

                // Prepara resumen visual
                var runeSummary = string.Join(", ", runes
                    .GroupBy(r => r.Name)
                    .Select(g => $"{g.Count()}x {g.Key}"));

                PacketSender.SendChatMsg(this, $"Rompiste {descriptor.Name} y obtuviste: {runeSummary}.", ChatMessageType.Experience);
            }

            // Actualiza inventario en cliente
            PacketSender.SendInventory(this);
        }

        public void OpenEnchantment()
        {
            PacketSender.SendOpenEnchantmentWindow(this);
        }

        public void OpenMage()
        {
            PacketSender.SendOpenMageWindow(this);
        }
        public void OpenBrokeItem()
        {
            PacketSender.SendOpenBrokeItemWindow(this);
        }
        [NotMapped] public bool InMailBox;
        [JsonIgnore]
        public virtual List<MailBox> MailBoxs { get; set; } = new List<MailBox>();

        public void OpenMailBox()
        {
            InMailBox = true;
            PacketSender.SendOpenMailBox(this);
        }
        public void CloseMailBox()
        {
            if (InMailBox)
            {
                InMailBox = false;
                PacketSender.SendCloseMailBox(this);
            }
        }
        public void SendMail()
        {
            InMailBox = true;
            PacketSender.SendOpenSendMail(this);
        }

        public (int[] stats, int[] percentStats, long[] vitals, int[] percentVitals, List<EffectData> effects) GetSetBonuses()
        {
            var stats = new int[Enum.GetValues<Stat>().Length];
            var percentStats = new int[Enum.GetValues<Stat>().Length];
            var vitals = new long[Enum.GetValues<Vital>().Length];
            var percentVitals = new int[Enum.GetValues<Vital>().Length];
            var effects = new List<EffectData>();

            var sets = EquippedItems
                .Where(i => i.Descriptor?.SetId != Guid.Empty)
                .GroupBy(i => i.Descriptor.SetId);

            foreach (var grp in sets)
            {
                var set = SetDescriptor.Get(grp.Key);
                if (set == null || !set.HasBonuses())
                {
                    continue;
                }

                var ratio = (float)grp.Count() / Math.Max(1, set.ItemIds.Count);
                var (s, ps, v, pv, eff) = set.GetBonuses();

                for (var i = 0; i < stats.Length; i++)
                {
                    stats[i] += (int)(s[i] * ratio);
                    percentStats[i] += (int)(ps[i] * ratio);
                }

                for (var i = 0; i < vitals.Length; i++)
                {
                    vitals[i] += (long)(v[i] * ratio);
                    percentVitals[i] += (int)(pv[i] * ratio);
                }

                foreach (var e in eff)
                {
                    effects.Add(new EffectData(e.Type, (int)(e.Percentage * ratio)));
                }
            }

            return (stats, percentStats, vitals, percentVitals, effects);
        }

        public bool HasEnoughSpellPoints(int delta)
        {
            return delta <= 0 || SpellPoints >= delta;
        }

        public void ConsumeSpellPoints(int delta)
        {
            SpellPoints -= delta;
            SpellPointsChanged = true;
        }

        public bool HasPermissionToTrade()
        {
            return Level >= 10; //Nivel minimo para comerciar
        }

    }
}