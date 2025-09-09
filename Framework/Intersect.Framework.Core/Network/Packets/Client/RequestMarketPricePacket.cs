using System;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestMarketPricePacket : IntersectPacket
    {
        [Key(0)] public Guid ItemId { get; set; }

        public RequestMarketPricePacket(Guid itemId)
        {
            ItemId = itemId;
        }
    }
}
