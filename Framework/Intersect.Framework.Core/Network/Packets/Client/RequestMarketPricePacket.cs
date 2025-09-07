using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class RequestMarketPricePacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public RequestMarketPricePacket()
    {
    }

    public RequestMarketPricePacket(int itemId)
    {
        ItemId = itemId;
    }

    [Key(0)]
    public int ItemId { get; set; }
}

