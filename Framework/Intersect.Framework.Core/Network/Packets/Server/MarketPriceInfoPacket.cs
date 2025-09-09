using System;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketPriceInfoPacket : IntersectPacket
    {
        [Key(0)] public Guid ItemId { get; set; }
        [Key(1)] public int SuggestedPrice { get; set; }
        [Key(2)] public int MinAllowedPrice { get; set; }
        [Key(3)] public int MaxAllowedPrice { get; set; }

             
    }
}
