using System;
using System.Collections.Generic;
using System.IO;
using Intersect.Framework.Core.GameObjects.Items;
using Newtonsoft.Json;
using Intersect;

namespace Intersect.Server.Database.PlayerData.Market;

public static class MarketStatisticsManager
{
    private static Dictionary<int, MarketStatistics> _statistics = new();

    private static readonly string StatsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "market_stats.json");

    static MarketStatisticsManager()
    {
        Load();
    }

    private static void Load()
    {
        if (!File.Exists(StatsFilePath))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(StatsFilePath);
            var data = JsonConvert.DeserializeObject<Dictionary<int, MarketStatistics>>(json);
            if (data != null)
            {
                _statistics = data;
            }
        }
        catch
        {
            // ignore load errors
        }
    }

    private static void Save()
    {
        try
        {
            var json = JsonConvert.SerializeObject(_statistics, Formatting.Indented);
            File.WriteAllText(StatsFilePath, json);
        }
        catch
        {
            // ignore save errors
        }
    }

    private static MarketStatistics GetOrCreateStats(int itemId)
    {
        if (!_statistics.TryGetValue(itemId, out var stats))
        {
            stats = new MarketStatistics();
            _statistics[itemId] = stats;
        }

        return stats;
    }

    public static void RecordListing(int itemId, long price)
    {
        GetOrCreateStats(itemId).Record(price);
        Save();
    }

    public static void RecordSale(int itemId, long price)
    {
        GetOrCreateStats(itemId).Record(price);
        Save();
    }

    public static (long suggested, long min, long max) GetStatistics(int itemId)
    {
        var stats = GetOrCreateStats(itemId);
        var average = stats.SuggestedPrice;

        long basePrice = average;
        if (basePrice <= 0)
        {
            var descriptor = ItemDescriptor.Get(ItemDescriptor.IdFromList(itemId));
            basePrice = descriptor?.Price ?? 0;
        }

        if (basePrice <= 0)
        {
            return (0, 0, long.MaxValue);
        }

        var variance = Options.Instance.Market.AllowedPriceVariance;
        var min = (long)Math.Floor(basePrice * (1 - variance));
        var max = (long)Math.Ceiling(basePrice * (1 + variance));

        var suggested = average > 0 ? average : basePrice;

        return (suggested, min, max);
    }

    public static void UpdateStatistics(MarketTransaction transaction)
    {
        RecordSale(transaction.ItemId, transaction.Price);
    }
}

