using System;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketListing
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SellerId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public long Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
