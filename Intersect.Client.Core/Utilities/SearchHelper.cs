// SearchHelper.cs (reemplazar por completo)
using System;
using System.Linq;

namespace Intersect.Client.Utilities
{
    public readonly struct SearchQuery
    {
        public string? FreeText { get; init; }
        public string? Type { get; init; }
        public string? Subtype { get; init; }
        public int? MinQty { get; init; }
        public int? MaxQty { get; init; }

        public static SearchQuery Parse(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return default;

            var parts = raw.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string? free = null, type = null, subtype = null;
            int? min = null, max = null;

            foreach (var p in parts)
            {
                var i = p.IndexOf(':');
                if (i <= 0) { free = string.Join(' ', new[] { free, p }.Where(s => !string.IsNullOrEmpty(s))); continue; }

                var key = p[..i].Trim().ToLowerInvariant();
                var val = p[(i + 1)..].Trim();

                switch (key)
                {
                    case "type": type = val; break;
                    case "subtype": subtype = val; break;
                    case "minqty": if (int.TryParse(val, out var v1)) min = v1; break;
                    case "maxqty": if (int.TryParse(val, out var v2)) max = v2; break;
                    default:
                        free = string.Join(' ', new[] { free, p }.Where(s => !string.IsNullOrEmpty(s)));
                        break;
                }
            }

            return new SearchQuery { FreeText = free, Type = type, Subtype = subtype, MinQty = min, MaxQty = max };
        }
    }

    public static class SearchHelper
    {
        public static bool Matches(string? query, string? text)
        {
            if (string.IsNullOrWhiteSpace(query))
                return true;

            if (string.IsNullOrWhiteSpace(text))
                return false;

            // Mantener comportamiento antiguo para compatibilidad
            return text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
