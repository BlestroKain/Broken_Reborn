using System.Collections.Generic;

namespace Intersect.Server.Database.PlayerData.Market;

public class MarketStatisticsManager
{
    private readonly Dictionary<int, MarketStatistics> _statistics = new();

    private MarketStatistics GetStats(int itemId)
    {
        if (!_statistics.TryGetValue(itemId, out var stats))
        {
            stats = new MarketStatistics();
            _statistics[itemId] = stats;
        }

        return stats;
    }

    public void RecordListing(int itemId, long price) => GetStats(itemId).Record(price);

    public void RecordSale(int itemId, long price) => GetStats(itemId).Record(price);

    public (long suggested, long min, long max) GetStatistics(int itemId)
    {
        var stats = GetStats(itemId);
        return (stats.SuggestedPrice, stats.MinPrice, stats.MaxPrice);
    }
}

