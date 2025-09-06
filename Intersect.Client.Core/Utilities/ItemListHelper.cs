// ItemListHelper.cs
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intersect.Config;
using Intersect.Client.Utilities; // para SearchHelper / SearchQuery en tu proyecto
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Utilities
{
    public enum SortCriterion
    {
        TypeThenName,   // por tipo → subtipo → nombre
        Name,           // alfabético por nombre
        Quantity,       // por cantidad
        Price           // por precio base (ItemDescriptor.Price)
    }

    public static class ItemListHelper
    {
        private static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

        internal static bool IsValid(ItemDescriptor d, int? quantity)
        {
            if (string.IsNullOrEmpty(d.Name))
            {
                return false;
            }

            if (d.Price < 0)
            {
                return false;
            }

            if (quantity.HasValue && quantity.Value < 0)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(ItemType), d.ItemType))
            {
                return false;
            }

            var subtypes = Options.Instance?.Items?.ItemSubtypes;
            if (subtypes == null)
            {
                return false;
            }

            if (!subtypes.TryGetValue(d.ItemType, out var list) || list == null)
            {
                return false;
            }

            var subtype = d.Subtype;
            if (string.IsNullOrEmpty(subtype))
            {
                return false;
            }

            return list.Any(s => s.Equals(subtype, StringComparison.OrdinalIgnoreCase));
        }

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
            // --- 1) Filtrado robusto ---
            var tokens = SearchQuery.Parse(searchText);

            bool Match(T entry)
            {
                var d = getDescriptor(entry);
                if (d == null)
                {
                    return false;
                }

                var q = getQuantity?.Invoke(entry);
                if (!IsValid(d, q))
                {
                    return false;
                }

                // Filtro explícito por tipo
                if (type.HasValue && d.ItemType != type.Value)
                {
                    return false;
                }

                // Filtro explícito por subtipo
                if (!string.IsNullOrEmpty(subtype))
                {
                    if (!string.Equals(d.Subtype ?? string.Empty, subtype, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }

                // Texto libre (null-safe)
                if (!SearchHelper.Matches(tokens.FreeText, d.Name ?? string.Empty))
                {
                    return false;
                }

                // type: en el query (comparado por ToString del enum)
                if (tokens.Type is { Length: > 0 } t &&
                    !t.Equals(d.ItemType.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                // subtype: en el query
                if (tokens.Subtype is { Length: > 0 } st &&
                    !st.Equals(d.Subtype ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                // minqty / maxqty
                if (q.HasValue)
                {
                    if (tokens.MinQty.HasValue && q.Value < tokens.MinQty.Value)
                    {
                        return false;
                    }

                    if (tokens.MaxQty.HasValue && q.Value > tokens.MaxQty.Value)
                    {
                        return false;
                    }
                }

                return true;
            }

            var filtered = items.Where(Match);

            // Proyecta descriptor una sola vez para evitar recomputes y NREs en ThenBy
            var withDesc = filtered
                .Select(e => (e, d: getDescriptor(e)!)) // seguro por el filtro
                .ToList();

            // --- 2) Comparadores auxiliares ---
            int TypeOrder(ItemType t) => (int)t;

            int SubtypeOrder(ItemDescriptor d)
            {
                // Usa índice en Options.Instance.Items.ItemSubtypes[d.ItemType]
                try
                {
                    var dict = Options.Instance.Items.ItemSubtypes;
                    if (dict != null && dict.TryGetValue(d.ItemType, out var list) && list != null)
                    {
                        var st = d.Subtype ?? string.Empty;
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (string.Equals(list[i], st, StringComparison.OrdinalIgnoreCase))
                                return i;
                        }
                    }
                }
                catch
                {
                    // ignorar y mandar al final
                }
                return int.MaxValue; // subtipos no configurados van al final
            }

            string NameKey(ItemDescriptor d) => d.Name ?? string.Empty;

            // --- 3) Orden ---
            IOrderedEnumerable<(T e, ItemDescriptor d)> ordered;

            switch (criterion)
            {
                case SortCriterion.Name:
                    if (ascending)
                    {
                        ordered = withDesc
                            .OrderBy(x => NameKey(x.d), NameComparer)
                            .ThenBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => x.d.Price);
                    }
                    else
                    {
                        ordered = withDesc
                            .OrderByDescending(x => NameKey(x.d), NameComparer)
                            .ThenBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => x.d.Price);
                    }
                    break;

                case SortCriterion.Quantity:
                    if (getQuantity == null)
                    {
                        // Sin cantidad: cae a TypeThenName como fallback
                        goto case SortCriterion.TypeThenName;
                    }
                    if (ascending)
                    {
                        ordered = withDesc
                            .OrderBy(x => getQuantity(x.e))
                            .ThenBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => NameKey(x.d), NameComparer);
                    }
                    else
                    {
                        ordered = withDesc
                            .OrderByDescending(x => getQuantity(x.e))
                            .ThenBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => NameKey(x.d), NameComparer);
                    }
                    break;

                case SortCriterion.Price:
                    if (ascending)
                    {
                        ordered = withDesc
                            .OrderBy(x => x.d.Price) // si no hay price, el engine suele dar 0; si temes null, usa ?? int.MaxValue
                            .ThenBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => NameKey(x.d), NameComparer);
                    }
                    else
                    {
                        ordered = withDesc
                            .OrderByDescending(x => x.d.Price)
                            .ThenBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => NameKey(x.d), NameComparer);
                    }
                    break;

                case SortCriterion.TypeThenName:
                default:
                    if (ascending)
                    {
                        ordered = withDesc
                            .OrderBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenBy(x => NameKey(x.d), NameComparer);
                    }
                    else
                    {
                        // mantenemos agrupación por tipo/subtipo asc, y solo invertimos el nombre
                        ordered = withDesc
                            .OrderBy(x => TypeOrder(x.d.ItemType))
                            .ThenBy(x => SubtypeOrder(x.d))
                            .ThenByDescending(x => NameKey(x.d), NameComparer);
                    }
                    break;
            }

            return ordered.Select(x => x.e);
        }

        // Overload legacy (sin tipo/subtipo explícitos)
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
