using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketListingCreatedPacket : IntersectPacket
    {
        [Key(0)] public string Message { get; set; }

        public MarketListingCreatedPacket(string msg)
        {
            Message = msg;
        }
    }
}
