using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class ToggleWingsPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public ToggleWingsPacket()
    {
    }

    public ToggleWingsPacket(WingState state)
    {
        State = state;
    }

    [Key(0)]
    public WingState State { get; set; }
}

