using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MarketPriceInfoPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MarketPriceInfoPacket()
    {
    }

    public MarketPriceInfoPacket(int itemId, long suggestedPrice, long minPrice, long maxPrice)
    {
        ItemId = itemId;
        SuggestedPrice = suggestedPrice;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
    }

    [Key(0)]
    public int ItemId { get; set; }

    [Key(1)]
    public long SuggestedPrice { get; set; }

    [Key(2)]
    public long MinPrice { get; set; }

    [Key(3)]
    public long MaxPrice { get; set; }
}

