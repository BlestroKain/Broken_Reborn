using System;
using Intersect.Enums;
using Intersect.Config;

namespace Intersect.Framework.Core.GameObjects.Items;

public static class ItemSortHelper
{
    public static (int TypeRank, int SubtypeRank, string Name) GetSortKey(ItemDescriptor? descriptor)
    {
        if (descriptor == null)
        {
            return (int.MaxValue, int.MaxValue, string.Empty);
        }

        var typeRank = descriptor.ItemType switch
        {
            ItemType.Currency => 0,
            ItemType.Equipment => 1,
            ItemType.Bag => 2,
            ItemType.Resource => 3,
            ItemType.Event => 4,
            ItemType.Spell => 5,
            ItemType.None => 6,
            _ => 7,
        };

        var subtypeRank = int.MaxValue;
        if (Options.Instance.Items.ItemSubtypes.TryGetValue(descriptor.ItemType, out var subtypes))
        {
            var index = subtypes.FindIndex(s => s.Equals(descriptor.Subtype, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                subtypeRank = index;
            }
        }

        return (typeRank, subtypeRank, descriptor.Name ?? string.Empty);
    }

    public static int Compare(ItemDescriptor? a, ItemDescriptor? b) => GetSortKey(a).CompareTo(GetSortKey(b));
}

