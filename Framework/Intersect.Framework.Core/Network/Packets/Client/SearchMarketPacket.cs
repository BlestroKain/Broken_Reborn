using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class SearchMarketPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public SearchMarketPacket()
    {
    }

    public SearchMarketPacket(string query)
    {
        Query = query;
    }

    [Key(0)]
    public string Query { get; set; } = string.Empty;
}
