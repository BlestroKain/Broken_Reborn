using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public sealed class PetHubStatePacket : IntersectPacket
{
    public PetHubStatePacket()
    {
    }

    public PetHubStatePacket(bool isActive)
    {
        IsActive = isActive;
    }

    [Key(0)]
    public bool IsActive { get; set; }
}
