using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public sealed class InvokePetPacket : IntersectPacket
{
    public InvokePetPacket()
    {
    }

    public InvokePetPacket(bool openPetHub)
    {
        OpenPetHub = openPetHub;
    }

    [Key(0)]
    public bool OpenPetHub { get; set; }
}
