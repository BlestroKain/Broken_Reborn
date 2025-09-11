using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Items;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketListingPacket
    {
        [Key(0)] public Guid ListingId { get; set; }
        [Key(1)] public Guid ItemId { get; set; }
        [Key(2)] public int Quantity { get; set; }
        [Key(3)] public int Price { get; set; }
        [Key(4)] public string SellerName { get; set; }
        [Key(5)] public ItemProperties Properties { get; set; }
    }

    [MessagePackObject]
    public class MarketListingsPacket : IntersectPacket
    {
        [Key(0)] public List<MarketListingPacket> Listings { get; set; }

        public MarketListingsPacket(List<MarketListingPacket> listings)
        {
            Listings = listings ?? new List<MarketListingPacket>();
        }
    }
}
