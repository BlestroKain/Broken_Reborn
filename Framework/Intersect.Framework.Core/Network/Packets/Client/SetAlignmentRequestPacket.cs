using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class SetAlignmentRequestPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public SetAlignmentRequestPacket()
    {
    }

    public SetAlignmentRequestPacket(Factions desired)
    {
        Desired = desired;
    }

    [Key(0)]
    public Factions Desired { get; set; }
}

