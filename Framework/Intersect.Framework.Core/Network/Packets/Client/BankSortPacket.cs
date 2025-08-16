using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public class BankSortPacket : IntersectPacket
{
    [Key(0)]
    public string SortType { get; set; } = "standard";

    public BankSortPacket()
    {
    }
}
