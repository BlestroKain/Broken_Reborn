using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public sealed class SpawnPetRequestPacket : IntersectPacket
{
    public SpawnPetRequestPacket()
    {
    }

    public SpawnPetRequestPacket(bool openPetHub)
    {
        OpenPetHub = openPetHub;
    }

    [Key(0)]
    public bool OpenPetHub { get; set; }
}
