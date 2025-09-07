using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Market;

public class MarketStatistics
{
    private const int MaxSamples = 20;

    [JsonProperty]
    private readonly List<long> _prices = new();

    public void Record(long price)
    {
        _prices.Add(price);
        if (_prices.Count > MaxSamples)
        {
            _prices.RemoveAt(0);
        }
    }

    public long SuggestedPrice => _prices.Count > 0 ? (long)_prices.Average() : 0;

    public long MinPrice => _prices.Count > 0 ? _prices.Min() : 0;

    public long MaxPrice => _prices.Count > 0 ? _prices.Max() : 0;
}
