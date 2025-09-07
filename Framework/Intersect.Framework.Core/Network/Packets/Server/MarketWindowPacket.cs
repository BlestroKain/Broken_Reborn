using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketWindowPacket : IntersectPacket
    {
        public MarketWindowPacket(MarketWindowPacketType action, int slot = -1, int page = 1, int pageSize = 0, int total = 0)
        {
            Action = action;
            Slot = slot;
            Page = page;
            PageSize = pageSize;
            Total = total;
        }

        [Key(0)] public MarketWindowPacketType Action { get; set; }
        [Key(1)] public int Slot { get; set; }
        [Key(2)] public int Page { get; set; }
        [Key(3)] public int PageSize { get; set; }
        [Key(4)] public int Total { get; set; }
    }

    public enum MarketWindowPacketType
    {
        OpenMarket,
        CloseMarket,
        OpenSell,
        CloseSell,
    }
}
