using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData;

public class MarketStatistics
{
    public Guid ItemId { get; set; }
    public int TotalSold { get; set; }
    public int TotalRevenue { get; set; } // Acumula el valor total pagado
    public int NumberOfSales { get; set; }

    // Suma acumulada de cuadrados del precio por unidad (ponderado por cantidad)
    public double SumOfSquaredPrices { get; set; }

    // ✅ Precio promedio por unidad
    public float AveragePricePerUnit => TotalSold > 0 ? (float)TotalRevenue / TotalSold : 0;

    // Desviación estándar del precio por unidad
    public float StandardDeviation =>
        TotalSold > 0
            ? (float)Math.Sqrt(Math.Max(0, (SumOfSquaredPrices / TotalSold) - Math.Pow(AveragePricePerUnit, 2)))
            : 0;

    public MarketStatistics(Guid itemId, IEnumerable<MarketTransaction> transactions)
    {
        ItemId = itemId;

        foreach (var tx in transactions)
        {
            AddTransaction(tx);
        }
    }

    public MarketStatistics(Guid itemId)
    {
        ItemId = itemId;
    }

    // ✅ Rango basado en precio promedio por unidad
    public int GetMinAllowedPrice(float marginPercent = 0.5f)
    {
        var baseAvg = GetFallbackAverage();
        return (int)Math.Floor(baseAvg * (1f - marginPercent));
    }

    public int GetMaxAllowedPrice(float marginPercent = 0.5f)
    {
        var baseAvg = GetFallbackAverage();
        return (int)Math.Ceiling(baseAvg * (1f + marginPercent));
    }

    public void AddTransaction(MarketTransaction tx)
    {
        if (tx == null || tx.Quantity <= 0 || tx.Price <= 0)
            return;

        TotalSold += tx.Quantity;
        TotalRevenue += tx.Price;
        NumberOfSales++;

        var unitPrice = (double)tx.Price / tx.Quantity;
        SumOfSquaredPrices += unitPrice * unitPrice * tx.Quantity;
    }

    // ✅ Si no hay datos, se usa precio base del ítem
   public float GetFallbackAverage()
    {
        if (AveragePricePerUnit > 0)
        {
            return AveragePricePerUnit;
        }

        var basePrice = ItemDescriptor.Get(ItemId)?.Price ?? 0;
        return basePrice > 0 ? basePrice : 1; // Nunca menor que 1
    }
}

