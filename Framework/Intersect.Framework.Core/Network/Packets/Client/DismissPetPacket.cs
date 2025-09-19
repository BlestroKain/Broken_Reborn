using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public sealed class DismissPetPacket : IntersectPacket
{
    public DismissPetPacket()
    {
    }

    public DismissPetPacket(bool closePetHub)
    {
        ClosePetHub = closePetHub;
    }

    [Key(0)]
    public bool ClosePetHub { get; set; }
}
