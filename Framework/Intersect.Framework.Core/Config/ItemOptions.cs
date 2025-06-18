using System.Diagnostics.CodeAnalysis;
using Intersect.Framework.Core.GameObjects.Items;
using Newtonsoft.Json;

namespace Intersect.Config;

/// <summary>
/// Configuration options for items.
/// </summary>
public class ItemOptions
{
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
        { ItemType.Equipment, [ "Weapon", "Armor", "Tool", "Accessory" ] },
        { ItemType.Consumable, [ "Food", "Potion" ] },
        { ItemType.Currency, [ "Money" ] },
        { ItemType.Spell, [ "Scroll" ] },
        { ItemType.Event, [] },
        { ItemType.Bag, [ "Inventory", "Bank" ] },
        { ItemType.Resource, [ "Ore", "Wood", "Herb", "Fish" ] },
    };

    public bool TryGetRarityName(int rarityLevel, [NotNullWhen(true)] out string? rarityName)
    {
        rarityName = RarityTiers.Skip(rarityLevel).FirstOrDefault();
        return rarityName != default;
    }

    public IReadOnlyList<string> GetSubtypesFor(ItemType itemType)
    {
        return ItemSubtypes.TryGetValue(itemType, out var subtypes) ? subtypes : [];
    }
}
