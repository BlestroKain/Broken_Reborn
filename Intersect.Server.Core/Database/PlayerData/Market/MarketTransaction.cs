using System;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ListingId { get; set; }
    public Guid BuyerId { get; set; }
    public int Quantity { get; set; }
    public long Price { get; set; }
    public DateTime TransactionTime { get; set; } = DateTime.UtcNow;
}
