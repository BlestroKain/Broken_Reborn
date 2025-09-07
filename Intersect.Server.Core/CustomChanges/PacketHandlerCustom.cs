using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network;
using Intersect.Network.Packets;
using Intersect.Network.Packets.Client;
using Intersect.Server.Database;
using Intersect.Server.Database.Logging.Entities;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Intersect.Core;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Crafting;
using Intersect.Framework.Core.GameObjects.Events;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Maps;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.GameObjects.PlayerClass;
using Intersect.Framework.Core.Security;
using Intersect.Network.Packets.Server;
using Intersect.Server.Core;
using Intersect.Server.Metrics;
using Microsoft.Extensions.Logging;
using Intersect.Server.Database.PlayerData.Market;
using ChatMsgPacket = Intersect.Network.Packets.Client.ChatMsgPacket;
using LoginPacket = Intersect.Network.Packets.Client.LoginPacket;
using PartyInvitePacket = Intersect.Network.Packets.Client.PartyInvitePacket;
using PingPacket = Intersect.Network.Packets.Client.PingPacket;
using TradeRequestPacket = Intersect.Network.Packets.Client.TradeRequestPacket;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Networking;

internal sealed partial class PacketHandler
{
    public void HandlePacket(Client client, GuildExpPercentagePacket packet)
    {
        var player = client?.Entity;
        if (player == null) return;

        player.SetGuildExpPercentage(packet.Percentage);
    }

    public void HandlePacket(Client client, ApplyGuildUpgradePacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        var guild = player.Guild;
        if (player?.Guild != null)
        {
            var success = player.Guild.ApplyUpgrade(packet.UpgradeType);

            if (success)
            {
                PacketSender.UpdateGuild(player);
                PacketSender.SendChatMsg(player, "¡Mejora aplicada exitosamente!", ChatMessageType.Notice);
            }
            else
            {
                PacketSender.SendChatMsg(player, "No se pudo aplicar la mejora.", ChatMessageType.Notice);
            }
        }
    }
    public void HandlePacket(Client client, CreateGuildPacket packet)
    {
        var player = client?.Entity;
        if (player == null)
        {
            return;
        }

        var success = false;

        // We only accept Strings as our Guild Names!
        if (FieldChecking.IsValidGuildName(packet.Name, Strings.Regex.GuildName))
        {
            // Is the name already in use?
            if (Guild.GetGuild(packet.Name) == null)
            {
                // Is the player already in a guild?
                if (player.Guild == null)
                {
                    // Finally, we can actually MAKE this guild happen!
                    var guild = Guild.CreateGuild(player, packet.Name);
                    if (guild != null)
                    {
                        // Set the guild logo
                        guild.SetLogo(packet.LogoBackground, packet.BackgroundR, packet.BackgroundG, packet.BackgroundB, packet.LogoSymbol, packet.SymbolR, packet.SymbolG, packet.SymbolB);

                        // Send them a welcome message!
                        PacketSender.SendChatMsg(player, Strings.Guilds.Welcome.ToString(packet.Name), ChatMessageType.Guild, CustomColors.Alerts.Success);

                        // Denote that we were successful.
                        success = true;
                    }
                }
                else
                {
                    // This cheeky bugger is already in a guild, tell him so!
                    PacketSender.SendChatMsg(player, Strings.Guilds.AlreadyInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                }
            }
            else
            {
                // This name already exists, oh dear!
                PacketSender.SendChatMsg(player, Strings.Guilds.GuildNameInUse, ChatMessageType.Guild, CustomColors.Alerts.Error);
            }
        }
        else
        {
            // Let our player know they need to adjust their name.
            PacketSender.SendChatMsg(player, Strings.Guilds.VariableInvalid, ChatMessageType.Guild, CustomColors.Alerts.Error);
        }

        if (success)
        {
            // Additional logic for success case if needed
            PacketSender.SendChatMsg(player, Strings.Guilds.GuildCreated, ChatMessageType.Guild, CustomColors.Alerts.Success);
            Log.Information($"Guild '{packet.Name}' created successfully by player '{player.Name}'.");
        }
        else
        {
            // Additional logic for failure case if needed
            PacketSender.SendChatMsg(player, Strings.Guilds.GuildCreationFailed, ChatMessageType.Guild, CustomColors.Alerts.Error);
            Log.Warning($"Failed to create guild '{packet.Name}' for player '{player.Name}'.");
        }
    }
    public void HandlePacket(Client client, EnchantItemPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        // Validar índice del ítem
        if (packet.ItemIndex < 0 || packet.ItemIndex >= player.Items.Count)
        {
            PacketSender.SendChatMsg(player, "Ítem no encontrado en el inventario.", ChatMessageType.Error);
            return;
        }

        var item = player.Items[packet.ItemIndex];
        if (item == null || item.Descriptor?.ItemType != ItemType.Equipment || item.Properties == null || !item.Descriptor.CanBeEnchanted())
        {
            PacketSender.SendChatMsg(player, "El ítem no es válido o no se puede encantar.", ChatMessageType.Error);
            return;
        }

        // Validar moneda de encantamiento
        var currency = player.Items.FirstOrDefault(i => i?.ItemId == packet.CurrencyId && i.Quantity >= packet.CurrencyAmount);
        if (currency == null)
        {
            PacketSender.SendChatMsg(player, "No tienes suficiente moneda para encantar el ítem.", ChatMessageType.Error);
            return;
        }

        // Validar existencia del amuleto si se quiere usar
        if (packet.UseAmulet)
        {
            var amuletId = item.Descriptor.GetamuletMaterialId();
            var amuletItem = player.Items.FirstOrDefault(i => i?.ItemId == amuletId && i.Quantity > 0);

            if (amuletItem == null)
            {
                PacketSender.SendChatMsg(player, "No tienes amuletos de protección para usar.", ChatMessageType.Error);
                return;
            }
        }

        // Ejecutar mejora
        player.TryUpgradeItem(packet.ItemIndex, packet.TargetLevel, packet.UseAmulet);

        // Refrescar inventario en cliente
        PacketSender.SendInventoryItemUpdate(player, packet.ItemIndex);
    }

    public void HandlePacket(Client client, UpgradeItemStatPacket packet)
    {
        var player = client.Entity;
        if (player == null) return;

        // 1) Validaciones...
        if (packet.ItemSlot < 0 || packet.ItemSlot >= player.Items.Count ||
            packet.RuneSlot < 0 || packet.RuneSlot >= player.Items.Count)
        {
            PacketSender.SendChatMsg(player, "Ítem o Runa inválido.", ChatMessageType.Error);
            return;
        }

        var equipment = player.Items[packet.ItemSlot];
        var rune = player.Items[packet.RuneSlot];

        if (equipment == null || rune == null)
        {
            PacketSender.SendChatMsg(player, "Faltan ítems para el proceso.", ChatMessageType.Error);
            return;
        }
        if (equipment.Properties.EnchantmentLevel<5)
        {
            PacketSender.SendChatMsg(player, "Nivel de encantamiento debe ser mayor a +5", ChatMessageType.Error);
            return;
        }
        // 2) Aplica la runa
        if (!equipment.ApplyRuneUpgrade(equipment, rune, out var success, out var message))
        {
            PacketSender.SendChatMsg(player, message, ChatMessageType.Error);
            return;
        }

        // 3) Ajusta el slot en memoria, pero **no** quites la entidad de EF
        if (rune.Quantity <= 0)
        {
            rune.ItemId = Guid.Empty;
            rune.Quantity = 0;
            // opcional: limpia también sus Properties si hace falta
        }

        // 4) Guarda **solo** las dos filas de inventario
        using (var ctx = DbInterface.CreatePlayerContext(readOnly: false))
        {
            try
            {
                ctx.Player_Items.Update(equipment);
                ctx.Player_Items.Update(rune);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error guardando mejora de ítem con runa.");
                PacketSender.SendChatMsg(player, "Ocurrió un error al guardar en la base de datos.", ChatMessageType.Error);
                return;
            }
        }

        // 5) Notifica al usuario y refresca su inventario
        PacketSender.SendChatMsg(player, message, ChatMessageType.Notice);
        PacketSender.SendInventory(player);
    }

    public void HandlePacket(Client client, BrokeItemPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }
        ItemBreakHelper.InitializeRunes();
        player.BreakItem(packet.ItemSlot);
    }
    public void HandlePacket(Client client, MailBoxSendPacket packet)
    {
        var sender = client?.Entity;
        if (sender == null)
            return;

        using (var context = DbInterface.CreatePlayerContext(readOnly: false))
        {
            // Buscar destinatario e incluir sus correos
            var recipient = context.Players.Include(p => p.MailBoxs)
                .SingleOrDefault(p => p.Name == packet.To);

            if (recipient == null)
            {
                PacketSender.SendChatMsg(sender, $"{Strings.Mails.playernotfound} ({packet.To})", ChatMessageType.Error, CustomColors.Alerts.Info);
                sender.CloseMailBox();
                return;
            }

            // Bloquear envío al mismo personaje
            if (sender.Id == recipient.Id)
            {
                PacketSender.SendChatMsg(sender, "No puedes enviarte un correo a ti mismo.", ChatMessageType.Error, CustomColors.Alerts.Info);
                sender.CloseMailBox();
                return;
            }

            // Bloquear envío a otros personajes de la misma cuenta
            if (sender.UserId == recipient.UserId)
            {
                PacketSender.SendChatMsg(sender, "No puedes enviar correo a personajes de tu misma cuenta.", ChatMessageType.Error, CustomColors.Alerts.Info);
                sender.CloseMailBox();
                return;
            }

            // Manejo de adjuntos con snapshot para rollback
            var attachments = new List<MailAttachment>();
            var removedItems = new List<(Guid ItemId, int Quantity, ItemProperties Properties)>();

            foreach (var attachment in packet.Attachments)
            {
                if (sender.TryTakeItem(attachment.ItemId, attachment.Quantity))
                {
                    removedItems.Add((attachment.ItemId, attachment.Quantity, attachment.Properties));

                    attachments.Add(new MailAttachment
                    {
                        ItemId = attachment.ItemId,
                        Quantity = attachment.Quantity,
                        Properties = attachment.Properties
                    });
                }
                else
                {
                    // Si falla la extracción, devolver los que ya quitamos
                    foreach (var item in removedItems)
                    {
                        sender.TryGiveItem(item.ItemId, item.Quantity, item.Properties);
                    }

                    PacketSender.SendChatMsg(sender, Strings.Mails.invaliditem, ChatMessageType.Error, CustomColors.Alerts.Info);
                    sender.CloseMailBox();
                    return;
                }
            }

            // Adjuntar entidades para evitar conflictos
            context.Attach(recipient).State = EntityState.Unchanged;
            context.Attach(sender).State = EntityState.Unchanged;

            // Crear mail
            var mail = new MailBox(sender, recipient, packet.Title, packet.Message, attachments);
            recipient.MailBoxs.Add(mail);
            context.Entry(mail).State = EntityState.Added;

            // Guardar con reintentos y rollback si falla
            var success = false;
            int retries = 3;

            while (retries > 0 && !success)
            {
                try
                {
                    context.SaveChanges();
                    success = true;
                }
                catch (DbUpdateException ex)
                {
                    Log.Error($"Error de concurrencia al guardar el correo. Intentos restantes: {retries - 1}", ex);
                    retries--;
                }
            }

            // Si falla, devolver ítems al inventario
            if (!success)
            {
                foreach (var item in removedItems)
                {
                    sender.TryGiveItem(item.ItemId, item.Quantity, item.Properties);
                }

                PacketSender.SendChatMsg(sender, "Error al enviar el correo. Los ítems fueron devueltos.", ChatMessageType.Error, CustomColors.Alerts.Info);
                sender.CloseMailBox();
                return;
            }

            // Notificar destinatario online
            var onlineRecipient = Player.FindOnline(packet.To);
            if (onlineRecipient != null)
            {
                onlineRecipient.MailBoxs.Add(mail);
                PacketSender.SendChatMsg(onlineRecipient, Strings.Mails.newmail, ChatMessageType.Trading, CustomColors.Alerts.Accepted);
                PacketSender.SendOpenMailBox(onlineRecipient);
            }
        }

        // Confirmación al remitente
        PacketSender.SendChatMsg(sender, "Correo enviado con éxito.", ChatMessageType.Trading, CustomColors.Alerts.Accepted);
        sender.CloseMailBox();
    }

    public void HandlePacket(Client client, TakeMailPacket packet)
    {
        var player = client?.Entity;
        if (player == null)
        {
            return;
        }

        var mail = player.MailBoxs.FirstOrDefault(m => m.Id == packet.MailID);
        if (mail == null)
        {
            PacketSender.SendChatMsg(player, Strings.Mails.mailnotfound, ChatMessageType.Error, CustomColors.Alerts.Declined);
            return;
        }

        if (mail.Attachments == null || mail.Attachments.Count == 0)
        {
            PacketSender.SendChatMsg(player, "Este correo no contiene ítems.", ChatMessageType.Error, CustomColors.Alerts.Info);
            return;
        }

        var deliveredItems = new List<MailAttachment>();
        var pendingItems = new List<MailAttachment>();

        // Procesar adjuntos de forma segura
        foreach (var attachment in mail.Attachments)
        {
            var item = new Item(attachment.ItemId, attachment.Quantity)
            {
                Properties = attachment.Properties
            };

            // Validar espacio para este ítem
            if (player.CanGiveItem(item, out _))
            {
                if (player.TryGiveItem(item, ItemHandling.Normal))
                {
                    deliveredItems.Add(attachment);
                }
                else
                {
                    pendingItems.Add(attachment);
                }
            }
            else
            {
                pendingItems.Add(attachment);
            }
        }

        // Actualización en base de datos
        using (var context = DbInterface.CreatePlayerContext(readOnly: false))
        {
            var dbMail = context.Player_MailBox.FirstOrDefault(m => m.Id == mail.Id);
            if (dbMail != null)
            {
                if (pendingItems.Any())
                {
                    // Guardar solo los ítems que no se pudieron entregar
                    dbMail.Attachments = pendingItems;
                    context.Update(dbMail);
                }
                else
                {
                    // Eliminar el correo si ya no quedan adjuntos
                    context.Player_MailBox.Remove(dbMail);
                }
                context.SaveChanges();
            }
        }

        // Actualizar en memoria
        if (pendingItems.Any())
        {
            mail.Attachments = pendingItems;
            PacketSender.SendChatMsg(player, "No todos los ítems se entregaron. Verifica tu inventario y vuelve a intentarlo.", ChatMessageType.Notice, CustomColors.Alerts.Info);
        }
        else
        {
            player.MailBoxs.Remove(mail);
            PacketSender.SendChatMsg(player, "Todos los ítems del correo fueron entregados.", ChatMessageType.Trading, CustomColors.Alerts.Accepted);
        }

        // Actualización visual
        PacketSender.SendInventory(player);
        PacketSender.SendOpenMailBox(player);
    }

    public void HandlePacket(Client client, SpellPropertiesChangePacket packet)
    {
        var player = client?.Entity;
        if (player == null)
            return;

        if (packet.SpellSlot < 0 || packet.SpellSlot >= player.Spells.Count)
            return;

        var spellSlot = player.Spells[packet.SpellSlot];
        if (spellSlot.SpellId == Guid.Empty)
            return;

        // Cargar el descriptor del hechizo
        var spellDescriptor = SpellDescriptor.Get(spellSlot.SpellId);

        // ¡Olvídate de SpellDescriptor.Levelable y MaxLevel!
        var currentLevel = spellSlot.Properties.Level;
        var delta = packet.Delta;

        if (delta > 0)
        {
            if (player.SpellPoints < delta)
            {
                return;
            }
        }
        else if (delta < 0)
        {
            if (currentLevel <= 1)
            {
                return;
            }

            var refundable = Math.Min(-delta, spellSlot.SpellPointsSpent);
            refundable = Math.Min(refundable, currentLevel - 1);
            if (refundable <= 0)
            {
                return;
            }

            delta = -refundable;
        }

        var newLevel = currentLevel + delta;
        var maxLevel = Options.Instance.Player.MaxSpellLevel;
        if (newLevel < 1 || newLevel > maxLevel)
            return;

        spellSlot.Properties.Level = newLevel;
        spellSlot.SpellPointsSpent += delta;
        player.ConsumeSpellPoints(delta);

        // Fusionar upgrades definidos para este nivel
        if (spellDescriptor != null)
        {
            SpellProperties levelProperties = null;
            var rawProps = spellDescriptor.GetType().GetProperty("Properties")?.GetValue(spellDescriptor);

            if (rawProps is IEnumerable<SpellProperties> propsEnumerable)
            {
                foreach (var props in propsEnumerable)
                {
                    if (props != null && props.Level == newLevel)
                    {
                        levelProperties = props;
                        break;
                    }
                }
            }
            else if (rawProps is IDictionary dict && dict.Contains(newLevel))
            {
                if (dict[newLevel] is SpellProperties sp)
                {
                    levelProperties = sp;
                }
                else if (dict[newLevel] is IDictionary upgradesDict)
                {
                    spellSlot.Properties.CustomUpgrades ??= new Dictionary<string, int>();
                    foreach (DictionaryEntry entry in upgradesDict)
                    {
                        if (entry.Key is string key && entry.Value is int val)
                        {
                            spellSlot.Properties.CustomUpgrades[key] = val;
                        }
                    }
                }
            }
            else if (rawProps is SpellProperties singleProps && singleProps.Level == newLevel)
            {
                levelProperties = singleProps;
            }

            if (levelProperties != null)
            {
                spellSlot.Properties.CustomUpgrades ??= new Dictionary<string, int>();
                foreach (var kv in levelProperties.CustomUpgrades)
                {
                    spellSlot.Properties.CustomUpgrades[kv.Key] = kv.Value;
                }
            }
        }

        using (var context = DbInterface.CreatePlayerContext(readOnly: false))
        {
            context.Update(spellSlot);
            context.SaveChanges();
        }

        PacketSender.SendPlayerSpells(player);
        PacketSender.SendSpellPoints(player);
    }

    public void HandlePacket(Client client, ToggleWingsPacket packet)
    {
        var player = client?.Entity;
        if (player == null)
        {
            return;
        }

        if (packet.State == WingState.On && player.Faction == Factions.Neutral)
        {
            PacketSender.SendChatMsg(player, Strings.Alignment.WingsNeutral, ChatMessageType.Error, CustomColors.Alerts.Error);
            return;
        }

        player.Wings = packet.State;
        PacketSender.SendEntityDataToProximity(player);
    }

    public void HandlePacket(Client client, SearchMarketPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        var sw = Stopwatch.StartNew();

        var (listings, total) = MarketManager.SearchMarket(
            packet.Page,
            packet.PageSize,
            packet.ItemId,
            packet.MinPrice,
            packet.MaxPrice,
            packet.Status,
            packet.SellerId
        );

        sw.Stop();

        var packets = listings.Select(l => new MarketListingPacket(
            l.Id,
            l.SellerId,
            l.ItemId,
            l.Quantity,
            l.Price,
            l.ItemProperties
        )).ToList();

        PacketSender.SendMarketListings(player, packets, packet.Page, packet.PageSize, total);

        if (Options.Instance.Metrics.Enable)
        {
            MetricsRoot.Instance.Game.MarketSearchTime.Record(sw.ElapsedMilliseconds);
        }

        Log.Information(
            "Player {PlayerId} searched market (page {Page}, size {Size}) -> {Returned}/{Total} results in {Elapsed}ms",
            player.Id,
            packet.Page,
            packet.PageSize,
            packets.Count,
            total,
            sw.ElapsedMilliseconds
        );
    }

    public void HandlePacket(Client client, RequestMarketPricePacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        var (suggested, min, max) = MarketStatisticsManager.GetStatistics(packet.ItemId);
        PacketSender.SendMarketPriceInfo(player, packet.ItemId, suggested, min, max);
    }

    public void HandlePacket(Client client, CreateMarketListingPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        if (!MarketManager.CreateListing(player, packet.ItemSlot, packet.Quantity, packet.Price, packet.AutoSplit))
        {
            PacketSender.SendChatMsg(player, "No se pudo crear el listado.", ChatMessageType.Error);
        }
    }

    public void HandlePacket(Client client, BuyMarketListingPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        var sw = Stopwatch.StartNew();

        var success = MarketManager.BuyAsync(player, packet.ListingId)
            .ConfigureAwait(false).GetAwaiter().GetResult();

        sw.Stop();

        if (Options.Instance.Metrics.Enable)
        {
            MetricsRoot.Instance.Game.MarketPurchaseTime.Record(sw.ElapsedMilliseconds);
        }

        if (success)
        {
            PacketSender.SendMarketPurchaseSuccess(player, packet.ListingId);
            PacketSender.SendRefreshMarket(player);
            PacketSender.SendChatMsg(
                player,
                Strings.Market.itempurchased,
                ChatMessageType.Trading,
                CustomColors.Alerts.Accepted
            );

            Log.Information(
                "Player {PlayerId} purchased listing {ListingId} in {Elapsed}ms",
                player.Id,
                packet.ListingId,
                sw.ElapsedMilliseconds
            );
        }
        else
        {
            PacketSender.SendChatMsg(
                player,
                Strings.Market.transactionfailed,
                ChatMessageType.Error,
                CustomColors.Alerts.Error
            );

            Log.Warning(
                "Player {PlayerId} failed to purchase listing {ListingId} after {Elapsed}ms",
                player.Id,
                packet.ListingId,
                sw.ElapsedMilliseconds
            );
        }
    }

    public void HandlePacket(Client client, CancelMarketListingPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        MarketManager.CancelListing(player, packet.ListingId);
    }


}
