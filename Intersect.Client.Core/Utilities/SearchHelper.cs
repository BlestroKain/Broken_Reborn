using System;

namespace Intersect.Client.Utilities
{
    public static class SearchHelper
    {
        public static bool Matches(string? query, string? text)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            return text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
