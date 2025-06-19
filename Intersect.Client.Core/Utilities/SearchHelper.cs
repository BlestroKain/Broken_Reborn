using System;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Enums;

namespace Intersect.Client.Utilities;

public static class SearchHelper
{
    public static bool IsSearchable(ItemDescriptor item, string? term, ItemType? typeFilter, string? subTypeFilter)
    {
        if (item == null)
        {
            return false;
        }

        if (typeFilter.HasValue && item.ItemType != typeFilter.Value)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(subTypeFilter) && !string.Equals(item.Subtype, subTypeFilter, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(term))
        {
            return true;
        }

        term = term.Trim();
        return item.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
               (!string.IsNullOrEmpty(item.Description) && item.Description.Contains(term, StringComparison.OrdinalIgnoreCase));
    }
}
