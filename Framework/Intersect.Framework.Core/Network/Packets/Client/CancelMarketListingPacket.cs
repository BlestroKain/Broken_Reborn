using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client;
[MessagePackObject]
public class CancelMarketListingPacket : IntersectPacket
{
    [Key(0)] public Guid ListingId { get; set; }

    public CancelMarketListingPacket(Guid listingId)
    {
        ListingId = listingId;
    }
}

