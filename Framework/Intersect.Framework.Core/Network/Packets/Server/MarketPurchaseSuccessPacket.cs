using System;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MarketPurchaseSuccessPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MarketPurchaseSuccessPacket()
    {
    }

    public MarketPurchaseSuccessPacket(Guid listingId)
    {
        ListingId = listingId;
    }

    [Key(0)]
    public Guid ListingId { get; set; }
}
