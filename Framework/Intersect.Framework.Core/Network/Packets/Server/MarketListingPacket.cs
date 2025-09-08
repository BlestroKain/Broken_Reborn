using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Items;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MarketListingPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MarketListingPacket()
    {
    }

    public MarketListingPacket(Guid listingId, Guid sellerId, Guid itemId, int quantity, long price, ItemProperties properties)
    {
        ListingId = listingId;
        SellerId = sellerId;
        ItemId = itemId;
        Quantity = quantity;
        Price = price;
        Properties = properties;
    }

    [Key(0)]
    public Guid ListingId { get; set; }

    [Key(1)]
    public Guid SellerId { get; set; }

    [Key(2)]
    public Guid ItemId { get; set; }

    [Key(3)]
    public int Quantity { get; set; }

    [Key(4)]
    public long Price { get; set; }

    [Key(5)]
    public ItemProperties Properties { get; set; } = new();
}

[MessagePackObject]
public partial class MarketListingsPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MarketListingsPacket()
    {
    }

    public MarketListingsPacket(List<MarketListingPacket> listings, int page, int pageSize, int total)
    {
        Listings = listings;
        Page = page;
        PageSize = pageSize;
        Total = total;
    }

    [Key(0)]
    public List<MarketListingPacket> Listings { get; set; } = new();

    [Key(1)]
    public int Page { get; set; }

    [Key(2)]
    public int PageSize { get; set; }

    [Key(3)]
    public int Total { get; set; }
}
