using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public sealed class DespawnPetRequestPacket : IntersectPacket
{
    public DespawnPetRequestPacket()
    {
    }

    public DespawnPetRequestPacket(bool closePetHub)
    {
        ClosePetHub = closePetHub;
    }

    [Key(0)]
    public bool ClosePetHub { get; set; }
}
