using System;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MarketListingCreatedPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MarketListingCreatedPacket()
    {
    }

    public MarketListingCreatedPacket(Guid listingId)
    {
        ListingId = listingId;
    }

    [Key(0)]
    public Guid ListingId { get; set; }
}
