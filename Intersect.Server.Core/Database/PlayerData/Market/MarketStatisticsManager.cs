using System;
using System.Collections.Generic;

namespace Intersect.Server.Database.PlayerData.Market;

public static class MarketStatisticsManager
{
    private static readonly Dictionary<Guid, MarketStatistics> _statistics = new();

    private static MarketStatistics GetOrCreateStats(Guid itemId)
    {
        if (!_statistics.TryGetValue(itemId, out var stats))
        {
            stats = new MarketStatistics();
            _statistics[itemId] = stats;
        }

        return stats;
    }

    public static void RecordListing(Guid itemId, long price) => GetOrCreateStats(itemId).Record(price);

    public static void RecordSale(Guid itemId, long price) => GetOrCreateStats(itemId).Record(price);

    public static (long suggested, long min, long max) GetStatistics(Guid itemId)
    {
        var stats = GetOrCreateStats(itemId);
        return (stats.SuggestedPrice, stats.MinPrice, stats.MaxPrice);
    }

    public static void UpdateStatistics(MarketTransaction transaction)
    {
        RecordSale(transaction.ItemId, transaction.Price);
    }
}

