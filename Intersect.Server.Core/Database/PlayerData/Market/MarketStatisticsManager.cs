using System.Collections.Generic;

namespace Intersect.Server.Database.PlayerData.Market;

public class MarketStatisticsManager
{
    private readonly Dictionary<int, MarketStatistics> _statistics = new();

    private MarketStatistics GetOrCreateStats(int itemId)
    {
        if (!_statistics.TryGetValue(itemId, out var stats))
        {
            stats = new MarketStatistics();
            _statistics[itemId] = stats;
        }

        return stats;
    }

    public void RecordListing(int itemId, long price) => GetOrCreateStats(itemId).Record(price);

    public void RecordSale(int itemId, long price) => GetOrCreateStats(itemId).Record(price);

    public (long suggested, long min, long max) GetStatistics(int itemId)
    {
        var stats = GetOrCreateStats(itemId);
        return (stats.SuggestedPrice, stats.MinPrice, stats.MaxPrice);
    }
}

