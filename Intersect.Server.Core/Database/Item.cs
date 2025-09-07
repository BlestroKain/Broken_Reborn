using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Framework.Items;
using Newtonsoft.Json;

namespace Intersect.Server.Database;

public class Item : IItem
{
    [JsonIgnore][NotMapped] public double DropChance { get; set; } = 100;

    public Item()
    {
        Properties = new ItemProperties();
    }

    public Item(Guid itemId, int quantity, ItemProperties properties = null) : this(
        itemId,
        quantity,
        null,
        null,
        properties
    )
    {
    }

    public Item(
        Guid itemId,
        int quantity,
        Guid? bagId,
        Bag bag,
        ItemProperties properties = null
    )
    {
        ItemId = itemId;
        Quantity = quantity;
        BagId = bagId;
        Bag = bag;
        Properties = properties ?? new ItemProperties();

        if (!ItemDescriptor.TryGet(itemId, out var descriptor) || properties != null)
        {
            return;
        }

        if (descriptor.ItemType != ItemType.Equipment)
        {
            return;
        }

        foreach (Stat stat in Enum.GetValues<Stat>())
        {
            if (descriptor.TryGetRangeFor(stat, out var range))
            {
                Properties.StatModifiers[(int)stat] = range.Roll();
            }
        }
    }

    public Item(Item item) : this(item.ItemId, item.Quantity, item.BagId, item.Bag)
    {
        Properties = new ItemProperties(item.Properties);
        DropChance = item.DropChance;
    }

    // TODO: THIS SHOULD NOT BE A NULLABLE. This needs to be fixed.
    public Guid? BagId { get; set; }

    [JsonIgnore] public virtual Bag Bag { get; set; }

    public Guid ItemId { get; set; } = Guid.Empty;

    [NotMapped] public string ItemName => ItemDescriptor.GetName(ItemId);

    public int Quantity { get; set; }

    [NotMapped] public ItemProperties Properties { get; set; }

    [Column(nameof(ItemProperties))]
    [JsonIgnore]
    public string ItemPropertiesJson
    {
        get => JsonConvert.SerializeObject(Properties);
        set =>
            Properties = JsonConvert.DeserializeObject<ItemProperties>(value ?? string.Empty) ?? new ItemProperties();
    }

    [JsonIgnore, NotMapped] public ItemDescriptor Descriptor => ItemDescriptor.Get(ItemId);

    public static Item None => new();

    public Item Clone()
    {
        return new Item(this);
    }

    public string Data()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static bool TryFindSourceSlotsForItem<TItem>(
        Guid itemDescriptorId,
        int slotHint,
        int searchQuantity,
        TItem?[] slots,
        [NotNullWhen(true)] out int[] sourceSlotIndices
    ) where TItem : Item
    {
        Debug.Assert(itemDescriptorId != default);
        Debug.Assert(slots != default);

        if (searchQuantity < 1)
        {
            sourceSlotIndices = default;
            return false;
        }

        var remainingQuantity = searchQuantity;
        List<int> compatibleSlots = new();

        if (slotHint > -1)
        {
            var slot = slots[slotHint];
            if (slot?.ItemId == itemDescriptorId)
            {
                var remainingQuantityInSlot = Math.Min(remainingQuantity, slot.Quantity);
                if (remainingQuantityInSlot > 0)
                {
                    remainingQuantity -= remainingQuantityInSlot;
                    compatibleSlots.Add(slotHint);
                }
            }
        }

        for (var slotIndex = 0; 0 < remainingQuantity && slotIndex < slots.Length; ++slotIndex)
        {
            var slot = slots[slotIndex];
            if (slotIndex == slotHint)
            {
                // If slotHint is < 0 this will never be hit
                // If slotHint is >= 0 we already accounted for the current slot in the if-block above
                continue;
            }

            if (slot?.ItemId != itemDescriptorId)
            {
                continue;
            }

            var remainingQuantityInSlot = Math.Min(remainingQuantity, slot.Quantity);
            if (remainingQuantityInSlot <= 0)
            {
                continue;
            }

            remainingQuantity -= remainingQuantityInSlot;
            compatibleSlots.Add(slotIndex);
        }

        if (remainingQuantity != 0 || compatibleSlots.Count < 1)
        {
            sourceSlotIndices = default;
            return false;
        }

        sourceSlotIndices = compatibleSlots.ToArray();
        return true;
    }

    public static TItem[] FindCompatibleSlotsForItem<TItem>(
        ItemDescriptor itemDescriptor,
        int maximumStack,
        int slotHint,
        int searchQuantity,
        TItem?[] slots,
        bool excludeEmpty = false
    ) where TItem : Item
    {
        Debug.Assert(itemDescriptor != default);
        Debug.Assert(slots != default);

        if (excludeEmpty && itemDescriptor.ItemType == ItemType.Equipment)
        {
            return Array.Empty<TItem>();
        }

        var availableQuantity = 0;
        List<TItem> compatibleSlots = new();

        if (slotHint > -1)
        {
            var slot = slots[slotHint];
            if (slot == null || slot.ItemId == default)
            {
                if (!excludeEmpty)
                {
                    availableQuantity += maximumStack;
                    compatibleSlots.Add(slot);
                }
            }
            else if (slot.ItemId == itemDescriptor.Id)
            {
                var availableQuantityInSlot = maximumStack - slot.Quantity;
                if (availableQuantityInSlot > 0)
                {
                    availableQuantity += availableQuantityInSlot;
                    compatibleSlots.Add(slot);
                }
            }
        }

        for (var slotIndex = 0; availableQuantity < searchQuantity && slotIndex < slots.Length; ++slotIndex)
        {
            var slot = slots[slotIndex];
            if (slotIndex == slotHint)
            {
                // If slotHint is < 0 this will never be hit
                // If slotHint is >= 0 we already accounted for the current slot in the if-block above
                continue;
            }

            if (slot == null || slot.ItemId == default)
            {
                if (excludeEmpty)
                {
                    continue;
                }

                availableQuantity += maximumStack;
                compatibleSlots.Add(slot);
                continue;
            }

            if (itemDescriptor.ItemType == ItemType.Equipment)
            {
                // Equipment slots are not valid target slots because they can have randomized stats
                continue;
            }

            if (slot.ItemId != itemDescriptor.Id)
            {
                continue;
            }

            var availableQuantityInSlot = maximumStack - slot.Quantity;
            if (availableQuantityInSlot <= 0)
            {
                continue;
            }

            availableQuantity += availableQuantityInSlot;
            compatibleSlots.Add(slot);
        }

        return compatibleSlots.ToArray();
    }

    public static int[] FindCompatibleSlotsForItem<TItem>(
        Guid itemDescriptorId,
        ItemType itemType,
        int maximumStack,
        int slotHint,
        int searchQuantity,
        TItem?[] slots
    ) where TItem : Item
    {
        Debug.Assert(itemDescriptorId != default);
        Debug.Assert(slots != default);

        var availableQuantity = 0;
        List<int> compatibleSlots = new();

        if (slotHint > -1)
        {
            var slot = slots[slotHint];
            if (slot == null || slot.ItemId == default)
            {
                availableQuantity += maximumStack;
                compatibleSlots.Add(slotHint);
            }
            else if (slot.ItemId == itemDescriptorId)
            {
                var availableQuantityInSlot = maximumStack - slot.Quantity;
                if (availableQuantityInSlot > 0)
                {
                    availableQuantity += availableQuantityInSlot;
                    compatibleSlots.Add(slotHint);
                }
            }
        }

        for (var slotIndex = 0; availableQuantity < searchQuantity && slotIndex < slots.Length; ++slotIndex)
        {
            var slot = slots[slotIndex];
            if (slotIndex == slotHint)
            {
                // If slotHint is < 0 this will never be hit
                // If slotHint is >= 0 we already accounted for the current slot in the if-block above
                continue;
            }

            if (slot == null || slot.ItemId == default)
            {
                availableQuantity += maximumStack;
                compatibleSlots.Add(slotIndex);
            }
            else if (itemType == ItemType.Equipment)
            {
                // Equipment slots are not valid target slots because they can have randomized stats
                continue;
            }
            else if (slot.ItemId == itemDescriptorId)
            {
                var availableQuantityInSlot = maximumStack - slot.Quantity;
                if (availableQuantityInSlot <= 0)
                {
                    continue;
                }

                availableQuantity += availableQuantityInSlot;
                compatibleSlots.Add(slotIndex);
            }
        }

        return compatibleSlots.ToArray();
    }

    public static int FindQuantityOfItem<TItem>(Guid itemDescriptorId, TItem?[] slots) where TItem : Item
    {
        return slots.Where(slot => slot?.ItemId == itemDescriptorId)
            .Aggregate(0, (totalQuantity, slot) => totalQuantity + slot.Quantity);
    }

    public static int FindSpaceForItem<TItem>(
        Guid itemDescriptorId,
        ItemType itemType,
        int maximumStack,
        int slotHint,
        int searchQuantity,
        TItem?[] slots
    ) where TItem : Item
    {
        Debug.Assert(itemDescriptorId != default);
        Debug.Assert(slots != default);

        var availableQuantity = 0;

        for (var slotIndex = 0; availableQuantity < searchQuantity && slotIndex < slots.Length; ++slotIndex)
        {
            var slot = slots[(slotIndex + Math.Max(0, slotHint)) % slots.Length];
            if (slot == null || slot.ItemId == default)
            {
                availableQuantity += maximumStack;
            }
            else if (itemType == ItemType.Equipment)
            {
                // Equipment slots are not valid target slots because they can have randomized stats
                continue;
            }
            else if (slot.ItemId == itemDescriptorId)
            {
                availableQuantity += maximumStack - slot.Quantity;
            }
        }

        return Math.Min(availableQuantity, searchQuantity);
    }

    public virtual void Set(Item item)
    {
        ItemId = item.ItemId;
        Quantity = item.Quantity;
        BagId = item.BagId;
        Bag = item.Bag;
        Properties = new ItemProperties(item.Properties);
    }

    /// <summary>
    ///     Try to get the bag, with an additional attempt to load it if it is not already loaded (it should be if this is even
    ///     a bag item).
    /// </summary>
    /// <param name="bag">the bag if there is one associated with this <see cref="Item" /></param>
    /// <returns>if <paramref name="bag" /> is not <see langword="null" /></returns>
    public bool TryGetBag(out Bag bag)
    {
        bag = Bag;

        if (bag == null)
        {
            var descriptor = Descriptor;
            if (descriptor?.ItemType == ItemType.Bag)
            {
                if (!Bag.TryGetBag(BagId ?? default, out bag))
                {
                    return false;
                }

                Bag = bag;
                return true;
            }

            return false;
        }

        // Remove any items from this bag that have been removed from the game
        foreach (var slot in bag.Slots)
        {
            if (ItemDescriptor.TryGet(slot.ItemId, out _))
            {
                continue;
            }

            slot.Set(None);
        }

        return true;
    }

    public void ApplyEnchantment(int newLevel)
    {
        if (Descriptor?.ItemType != ItemType.Equipment || Properties == null)
        {
            return;
        }

        newLevel = Math.Clamp(newLevel, 0, 8);
        int currentLevel = Properties.EnchantmentLevel;

        if (newLevel == currentLevel)
        {
            return;
        }

        if (newLevel > currentLevel)
        {
            double factor = 0.01;
            for (int lvl = currentLevel + 1; lvl <= newLevel; lvl++)
            {
                int[] statBonuses = new int[Enum.GetValues(typeof(Stat)).Length];
                int[] vitalBonuses = new int[Enum.GetValues(typeof(Vital)).Length];

                foreach (Stat stat in Enum.GetValues(typeof(Stat)))
                {
                    var statIndex = (int)stat;
                    int baseStat = Descriptor.StatsGiven[statIndex];

                    double levelInfluence = Math.Log2(lvl + 1) + Math.Sqrt(lvl);
                    int bonus = (int)Math.Ceiling(baseStat * factor * levelInfluence);

                    Properties.StatModifiers[statIndex] += bonus;
                    statBonuses[statIndex] = bonus;
                }

                foreach (Vital vital in Enum.GetValues(typeof(Vital)))
                {
                    var vitalIndex = (int)vital;
                    int baseVital = (int)Descriptor.VitalsGiven[vitalIndex];

                    double levelInfluence = Math.Log2(lvl + 1) + Math.Sqrt(lvl);
                    int bonus = (int)Math.Ceiling(baseVital * factor * levelInfluence);

                    Properties.VitalModifiers[vitalIndex] += bonus;
                    vitalBonuses[vitalIndex] = bonus;
                }

                // Combinar stats y vitals en un solo diccionario con índices consecutivos si quieres,
                // o guardar por separado si prefieres. Aquí los unimos en uno solo:
                Properties.EnchantmentRolls[lvl] = statBonuses.Concat(vitalBonuses).ToArray();
            }
        }
        else
        {
            for (int lvl = currentLevel; lvl > newLevel; lvl--)
            {
                if (Properties.EnchantmentRolls.TryGetValue(lvl, out var levelBonuses))
                {
                    int statCount = Enum.GetValues(typeof(Stat)).Length;
                    int vitalCount = Enum.GetValues(typeof(Vital)).Length;

                    for (int i = 0; i < statCount; i++)
                    {
                        int bonus = levelBonuses[i];
                        Properties.StatModifiers[i] -= bonus;
                        Properties.StatModifiers[i] = Math.Max(0, Properties.StatModifiers[i]);
                    }

                    for (int i = 0; i < vitalCount; i++)
                    {
                        int bonus = levelBonuses[statCount + i];
                        Properties.VitalModifiers[i] -= bonus;
                        Properties.VitalModifiers[i] = Math.Max(0, Properties.VitalModifiers[i]);
                    }

                    Properties.EnchantmentRolls.Remove(lvl);
                }
            }
        }

        Properties.EnchantmentLevel = newLevel;
    }
    public bool ApplyRuneUpgrade(Item equipment, Item runeItem, out bool success, out string resultMessage)
    {
        success = false;
        resultMessage = "";

        // 1) Validaciones básicas
        if (Descriptor?.ItemType != ItemType.Equipment || Properties == null)
        {
            resultMessage = "El ítem no es un equipamiento válido.";
            return false;
        }
        if (runeItem?.Descriptor == null
         || runeItem.Descriptor.ItemType != ItemType.Resource
         || runeItem.Descriptor.Subtype != "Rune")
        {
            resultMessage = "El ítem usado no es una Runa válida.";
            return false;
        }

        // 2) ¿A qué apunta la runa y cuánto modifica?
        var desc = runeItem.Descriptor;
       int targetStat = (int)desc.TargetStat;
        int targetVit = (int)desc.TargetVital;
        var amount = desc.AmountModifier;

        if (amount == 0)
        {
            resultMessage = "Esta Runa no tiene un modificador válido.";
            return false;
        }

        bool isStat = targetStat >= 0 && targetStat < Properties.StatModifiers.Length;
        bool isVital = targetVit >= 0 && targetVit < Properties.VitalModifiers.Length;


        if (!isStat && !isVital)
        {
            resultMessage = "La Runa no apunta a un atributo válido.";
            return false;
        }

        // 4) Guardar valor actual para posibles penalizaciones
        int idx = isStat
            ? (int)targetStat
            : (int)targetVit;
        int currentValue = isStat
            ? Properties.StatModifiers[idx]
            : Properties.VitalModifiers[idx];

        // 5) Calcular tasa de éxito sólo en función de MageSink
        var sinkMod = RarityMageoSettings.GetSinkMod(Descriptor.Rarity);
        double sinkFac = Math.Min(0.35, Properties.MageSink / 100.0);
        double finalRate = Math.Clamp(0.35 + sinkFac, 0.05, 0.95);

        // 6) Tirada de éxito
        if (Random.Shared.NextDouble() <= finalRate)
        {
            // 6a) Crítico
            bool isCritical = false;
            double critChance = 0;
            if (finalRate >= 0.9) critChance += 0.15;
            if (Properties.MageSink >= 100) critChance += 0.10;
            if (Random.Shared.NextDouble() <= critChance)
            {
                isCritical = true;
                amount *= 2;
            }

            // 6b) Aplicar bonus
            if (isStat)
            {
                Properties.StatModifiers[idx] += amount;
            }
            else
            {
                Properties.VitalModifiers[idx] += amount;
            }

            // 6c) Penalización suave si pasamos 2× valor base (30% de chance, -½ amount)
            int baseVal = isStat
                ? equipment.Descriptor.StatsGiven[idx]
                : (int)equipment.Descriptor.VitalsGiven[idx];
            int newValue = isStat
                ? Properties.StatModifiers[idx]
                : Properties.VitalModifiers[idx];
            if (newValue > baseVal * 2 && Random.Shared.NextDouble() < 0.30)
            {
                int penal = Math.Max(1, amount / 2);
                PenalizeOtherRandomAttribute(isStat, penal);
            }

            // 6d) Reducir MageSink
            Properties.MageSink = Math.Max(
                0,
                Properties.MageSink - (int)(amount * 5 * sinkMod)
            );

            // 6e) Mensaje de éxito
            success = true;
            var name = (isStat ? targetStat : (object)targetVit).ToString();
            resultMessage = isCritical
                ? $"¡Éxito Crítico! {name} +{amount}."
                : $"¡Éxito! {name} +{amount}.";
        }
        else
        {
            // 7) Fracaso: penalización reducida (-½ amount) y subir MageSink
            int penalty = Math.Min(currentValue, Math.Max(1, amount / 2));
            if (penalty > 0)
            {
                if (isStat)
                {
                    Properties.StatModifiers[idx] -= penalty;
                }
                else
                {
                    Properties.VitalModifiers[idx] -= penalty;
                }
                var name = (isStat ? targetStat : (object)targetVit).ToString();
                resultMessage = $"Falló. {name} -{penalty}.";
            }
            Properties.MageSink += (int)(amount * 10 * sinkMod);
            resultMessage += $" MageSink: {Properties.MageSink}.";
        }

        // 8) Consumir la runa
        runeItem.Quantity--;

        return true;
    }

    private void PenalizeOtherRandomAttribute(bool isStat, int amount)
    {
        var rng = Random.Shared;
        if (isStat)
        {
            // Elegir un stat al azar que tenga valor >0
            var candidates = Properties.StatModifiers
                .Select((v, i) => (v, i))
                .Where(x => x.v > 0)
                .ToArray();
            if (candidates.Length == 0) return;
            var idx = candidates[rng.Next(candidates.Length)].i;
            int reduce = Math.Min(Properties.StatModifiers[idx], amount);
            Properties.StatModifiers[idx] -= reduce;
        }
        else
        {
            // Igual para vitals
            var candidates = Properties.VitalModifiers
                .Select((v, i) => (v, i))
                .Where(x => x.v > 0)
                .ToArray();
            if (candidates.Length == 0) return;
            var idx = candidates[rng.Next(candidates.Length)].i;
            int reduce = Math.Min(Properties.VitalModifiers[idx], amount);
            Properties.VitalModifiers[idx] -= reduce;
        }
    }

    public static class RarityMageoSettings
    {
        // Multiplicador de sink por rareza
        private static readonly Dictionary<int, double> SinkModTable = new()
    {
        { 0, 1.2 },   // None
        { 1, 1.0 },   // Common
        { 2, 0.9 },   // Uncommon
        { 3, 0.8 },   // Rare
        { 4, 0.7 },   // Epic
        { 5, 0.6 },   // Legendary
    };

        public static double GetSinkMod(int rarity)
            => SinkModTable.TryGetValue(rarity, out var m) ? m : 1.2;
    }


}