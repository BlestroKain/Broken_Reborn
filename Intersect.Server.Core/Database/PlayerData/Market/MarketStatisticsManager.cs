using System;
using System.Collections.Generic;

namespace Intersect.Server.Database.PlayerData.Market;

public static class MarketStatisticsManager
{
    private static readonly Dictionary<int, MarketStatistics> _statistics = new();

    private static MarketStatistics GetOrCreateStats(int itemId)
    {
        if (!_statistics.TryGetValue(itemId, out var stats))
        {
            stats = new MarketStatistics();
            _statistics[itemId] = stats;
        }

        return stats;
    }

    public static void RecordListing(int itemId, long price) => GetOrCreateStats(itemId).Record(price);

    public static void RecordSale(int itemId, long price) => GetOrCreateStats(itemId).Record(price);

    public static (long suggested, long min, long max) GetStatistics(int itemId)
    {
        var stats = GetOrCreateStats(itemId);
        return (stats.SuggestedPrice, stats.MinPrice, stats.MaxPrice);
    }

    public static void UpdateStatistics(MarketTransaction transaction)
    {
        RecordSale(transaction.ItemId, transaction.Price);
    }
}

