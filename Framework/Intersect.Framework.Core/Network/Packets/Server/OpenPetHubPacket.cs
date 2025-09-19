using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public sealed class OpenPetHubPacket : IntersectPacket
{
    public OpenPetHubPacket()
    {
    }

    public OpenPetHubPacket(bool close)
    {
        Close = close;
    }

    [Key(0)]
    public bool Close { get; set; }
}

