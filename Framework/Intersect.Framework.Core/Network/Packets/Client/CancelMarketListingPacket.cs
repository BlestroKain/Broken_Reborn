using System;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class CancelMarketListingPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public CancelMarketListingPacket()
    {
    }

    public CancelMarketListingPacket(Guid listingId)
    {
        ListingId = listingId;
    }

    [Key(0)]
    public Guid ListingId { get; set; }
}
