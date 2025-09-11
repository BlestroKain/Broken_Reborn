using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketPurchaseSuccessPacket : IntersectPacket
    {
        [Key(0)] public string Message { get; set; }

        public MarketPurchaseSuccessPacket(string msg)
        {
            Message = msg;
        }
    }
}
