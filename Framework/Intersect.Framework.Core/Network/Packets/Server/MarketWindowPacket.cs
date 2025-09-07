using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketWindowPacket : IntersectPacket
    {
        public MarketWindowPacket(MarketWindowPacketType action, int slot = -1)
        {
            Action = action;
            Slot = slot;
        }

        [Key(0)] public MarketWindowPacketType Action { get; set; }
        [Key(1)] public int Slot { get; set; }
    }

    public enum MarketWindowPacketType
    {
        OpenMarket,
        CloseMarket,
        OpenSell,
        CloseSell,
    }
}
