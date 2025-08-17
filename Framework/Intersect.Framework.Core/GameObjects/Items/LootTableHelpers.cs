using System;
using System.Collections.Generic;

namespace Intersect.Framework.Core.GameObjects.Items;

/// <summary>
///     Helper methods for working with loot tables. Provides a utility to expand
///     nested tables into a flat collection of item chances.
/// </summary>
public static class LootTableHelpers
{
    /// <summary>
    ///     Expands a collection of drops, resolving any nested tables using the
    ///     supplied resolver and returning a dictionary mapping the final item id
    ///     to its accumulated drop chance.
    /// </summary>
    /// <param name="drops">Initial drops to expand.</param>
    /// <param name="tableResolver">
    ///     Delegate used to resolve the drops belonging to a nested loot table.
    ///     The delegate should return <c>null</c> when the supplied id does not
    ///     reference a loot table.
    /// </param>
    /// <returns>Dictionary of item ids to their aggregated chance values.</returns>
    public static IReadOnlyDictionary<Guid, double> Expand(IEnumerable<Drop> drops,
        Func<Guid, IEnumerable<Drop>?>? tableResolver = null)
    {
        var result = new Dictionary<Guid, double>();
        ExpandInternal(drops, 1d, tableResolver ?? (_ => null), result, new HashSet<Guid>());
        return result;
    }

    private static void ExpandInternal(IEnumerable<Drop> drops, double parentChance,
        Func<Guid, IEnumerable<Drop>?> resolver, IDictionary<Guid, double> result,
        ISet<Guid> visited)
    {
        foreach (var drop in drops)
        {
            var nested = resolver(drop.ItemId);
            if (nested != null && visited.Add(drop.ItemId))
            {
                ExpandInternal(nested, parentChance * drop.Chance, resolver, result, visited);
                visited.Remove(drop.ItemId);
            }
            else
            {
                var chance = parentChance * drop.Chance;
                if (result.TryGetValue(drop.ItemId, out var existing))
                {
                    result[drop.ItemId] = existing + chance;
                }
                else
                {
                    result[drop.ItemId] = chance;
                }
            }
        }
    }
}

