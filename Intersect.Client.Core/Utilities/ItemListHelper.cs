// ItemListHelper.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Utilities
{

    public enum SortCriterion
    {
        TypeThenName,   // por tipo → subtipo → nombre (default)
        Name,           // alfabético por nombre
        Quantity,       // por cantidad (desc/asc)
        Price           // por precio base del item (ItemDescriptor.Price)
    }

    public static class ItemListHelper
    {
        public static IEnumerable<T> FilterAndSort<T>(
            IEnumerable<T> items,
            Func<T, ItemDescriptor?> getDescriptor,
            Func<T, int>? getQuantity,
            string? searchText,
            ItemType? type,
            string? subtype,
            SortCriterion criterion,
            bool ascending
        )
        {
            // 1) Filtro: soporta texto simple y operadores (type:, subtype:, minqty:, maxqty:)
            var tokens = SearchQuery.Parse(searchText);

            bool Match(T entry)
            {
                var d = getDescriptor(entry);
                if (d == null) return false;

                // filtros explícitos
                if (type.HasValue && d.ItemType != type.Value) return false;

                if (!string.IsNullOrEmpty(subtype))
                {
                    if (!string.Equals(d.Subtype ?? string.Empty, subtype, StringComparison.OrdinalIgnoreCase))
                        return false;
                }

                // texto libre
                if (!SearchHelper.Matches(tokens.FreeText, d.Name)) return false;

                // type:
                if (tokens.Type is { Length: > 0 } t && !t.Equals(d.ItemType.ToString(), StringComparison.OrdinalIgnoreCase))
                    return false;

                // subtype:
                if (tokens.Subtype is { Length: > 0 } st &&
                    !st.Equals(d.Subtype ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                    return false;

                // minqty/maxqty:
                if (getQuantity != null)
                {
                    var q = getQuantity(entry);
                    if (tokens.MinQty.HasValue && q < tokens.MinQty.Value) return false;
                    if (tokens.MaxQty.HasValue && q > tokens.MaxQty.Value) return false;
                }

                return true;
            }

            var filtered = items.Where(Match);

            // 2) Orden
            IOrderedEnumerable<T> ordered = criterion switch
            {
                SortCriterion.Name => ascending
                    ? filtered.OrderBy(e => getDescriptor(e)?.Name ?? string.Empty)
                    : filtered.OrderByDescending(e => getDescriptor(e)?.Name ?? string.Empty),

                SortCriterion.Quantity when getQuantity != null => ascending
                    ? filtered.OrderBy(e => getQuantity!(e))
                    : filtered.OrderByDescending(e => getQuantity!(e)),

                SortCriterion.Price => ascending
                    ? filtered.OrderBy(e => getDescriptor(e)?.Price ?? int.MaxValue)
                    : filtered.OrderByDescending(e => getDescriptor(e)?.Price ?? int.MinValue),

                _ => ascending
                    ? filtered.OrderBy(e => ItemSortHelper.GetSortKey(getDescriptor(e)))
                    : filtered.OrderByDescending(e => ItemSortHelper.GetSortKey(getDescriptor(e))),
            };

            return ordered;
        }

        public static IEnumerable<T> FilterAndSort<T>(
            IEnumerable<T> items,
            Func<T, ItemDescriptor?> getDescriptor,
            Func<T, int>? getQuantity,
            string? searchText,
            SortCriterion criterion,
            bool ascending
        ) => FilterAndSort(items, getDescriptor, getQuantity, searchText, null, null, criterion, ascending);
    }
}