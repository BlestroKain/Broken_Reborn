using System;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class BuyMarketListingPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public BuyMarketListingPacket()
    {
    }

    public BuyMarketListingPacket(Guid listingId, int quantity)
    {
        ListingId = listingId;
        Quantity = quantity;
    }

    [Key(0)]
    public Guid ListingId { get; set; }

    [Key(1)]
    public int Quantity { get; set; }
}
