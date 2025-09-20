using System;
using System.Diagnostics.CodeAnalysis;
using Intersect.Framework.Core.GameObjects.Items;
using Newtonsoft.Json;

namespace Intersect.Config;

/// <summary>
/// Configuration options for items.
/// </summary>
public class ItemOptions
{
    public string AmuletItemID { get; set; } = "87A72C45-0BCF-4C09-94E3-E9E4966477EE";

    /// <summary>
    /// The available rarity tiers.
    /// </summary>
    /// <remarks>This is not intended to be a localized string, please see Strings for localization.</remarks>
    [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
    public List<string> RarityTiers { get; set; } =
    [
        @"None",
        @"Common",
        @"Uncommon",
        @"Rare",
        @"Epic",
        @"Legendary",
    ];

    [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
    public Dictionary<ItemType, List<string>> ItemSubtypes { get; set; } = new()
{
    { ItemType.Consumable, new() { "Drink", "Food", "Potion", "Scroll","spell" } },
    {
        ItemType.Equipment,
        new()
        {
            "Axe",
            "Bow",
            "Dagger",
            "Hammer",
            "Spear",
            "Staff",
            "Sword",
            "Wand",
            // Equipment slots
            "Helmet",
            "Armor",
            "Weapon",
            "Shield",
            "Boots",
            "Pants",
            "Cape",
            "Belt",
            "Necklace",
            "Accessory",
            "Ring",
            "Karma",
            "Pet",
        }
    },
    { ItemType.Resource, new()
        {
            "Blood", "Bone", "Cereal", "Claw", "Craft","Crystal", "Ear", "Essence", "Eye", "Fabric", "Feather", "Fiber", "Fish",
            "Fruits", "Gem", "Hair", "Herb", "Hide", "Horn", "Ink", "Leather", "Meat","Metal", "Mushrooms", "Oil", "Ore", "Orb",
            "Paw", "Plant", "Powder", "Root", "Rune", "Scale", "Seed", "Shell", "Soul", "Tail", "Vegetables", "Wing", "Wood"
        }
    }
};
    public string EnchantCurrencyItemID { get; set; } = "A58A19FF-1036-4A2C-89CD-E7CB6EAF2BE2";

    public bool TryGetRarityName(int rarityLevel, [NotNullWhen(true)] out string? rarityName)
    {
        rarityName = RarityTiers.Skip(rarityLevel).FirstOrDefault();
        return rarityName != default;
    }

    public void MergeEquipmentSlots(IEnumerable<string> slots)
    {
        if (!ItemSubtypes.TryGetValue(ItemType.Equipment, out var list) || list == null)
        {
            list = [];
            ItemSubtypes[ItemType.Equipment] = list;
        }

        foreach (var slot in slots)
        {
            if (!list.Any(s => s.Equals(slot, StringComparison.OrdinalIgnoreCase)))
            {
                list.Add(slot);
            }
        }
    }


}
