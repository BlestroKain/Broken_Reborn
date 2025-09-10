using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketWindowPacket : IntersectPacket
    {
        public MarketWindowPacket(bool openMarket, bool openSell, int slot = -1)
        {
            OpenMarket = openMarket;
            OpenSell = openSell;
            Slot = slot;
        }

        [Key(0)] public bool OpenMarket { get; set; }

        [Key(1)] public bool OpenSell { get; set; }

        [Key(2)] public int Slot { get; set; }
    }
}
