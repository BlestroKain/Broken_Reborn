using System;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class BuyMarketListingPacket : IntersectPacket
    {
        public BuyMarketListingPacket(Guid listingId)
        {
            ListingId = listingId;
        }

        [Key(0)] public Guid ListingId { get; set; }
    }
}
