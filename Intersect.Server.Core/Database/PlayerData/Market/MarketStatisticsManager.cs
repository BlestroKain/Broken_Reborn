using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.PlayerData.Market;

public static class MarketStatisticsManager
{
    private static readonly Dictionary<Guid, MarketStatistics> _statisticsCache = new();

    public static void LoadFromDatabase()
    {
        using var context = DbInterface.CreatePlayerContext(readOnly: true);

        var allTransactions = context.Market_Transactions.ToList();

        var grouped = allTransactions
            .GroupBy(tx => tx.ItemId);

        foreach (var group in grouped)
        {
            var stats = new MarketStatistics(group.Key, group);
            _statisticsCache[group.Key] = stats;
        }

       
    }

    /// <summary>
    /// Devuelve las estadísticas desde caché o base de datos si no existen aún.
    /// </summary>
    public static MarketStatistics GetStatistics(Guid itemId)
    {
        //LoadFromDatabase();
        if (_statisticsCache.TryGetValue(itemId, out var cachedStats))
        {
            return cachedStats;
        }

        var descriptor = ItemDescriptor.Get(itemId);
        if (descriptor == null || !descriptor.CanSell)
        {
            var basePrice = descriptor?.Price ?? 1;
            if (basePrice <= 0)
            {
                basePrice = 1;
            }

            return new MarketStatistics(itemId)
            {
                TotalRevenue = basePrice,
                TotalSold = 1,
                NumberOfSales = 1
            };
        }

        using var context = DbInterface.CreatePlayerContext(readOnly: true);
        var transactions = context.Market_Transactions
            .Where(t => t.ItemId == itemId)
            .ToList();

        MarketStatistics stats;

        if (transactions.Any())
        {
            stats = new MarketStatistics(itemId, transactions);
        }
        else
        {
            var basePrice = descriptor.Price;
            if (basePrice <= 0)
            {
                basePrice = 1;
            }

            stats = new MarketStatistics(itemId)
            {
                TotalRevenue = basePrice,
                TotalSold = 1,
                NumberOfSales = 1
            };
        }

        _statisticsCache[itemId] = stats;
        return stats;
    }

    /// <summary>
    /// Agrega una venta al historial y actualiza el caché.
    /// </summary>
    public static void UpdateStatistics(MarketTransaction tx)
    {
        //LoadFromDatabase();
        if (!_statisticsCache.TryGetValue(tx.ItemId, out var stats))
        {
            stats = new MarketStatistics(tx.ItemId);
            _statisticsCache[tx.ItemId] = stats;
        }

        stats.AddTransaction(tx);
    }

}
