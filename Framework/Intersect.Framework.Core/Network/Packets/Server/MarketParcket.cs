using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketWindowPacket : IntersectPacket
    {
        public MarketWindowPacket(bool openMarket, bool openSell)
        {
            OpenMarket = openMarket;
            OpenSell = openSell;
        }

        [Key(0)] public bool OpenMarket { get; set; }

        [Key(1)] public bool OpenSell { get; set; }
    }
}
