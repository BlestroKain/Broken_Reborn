using Intersect.Enums;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class WaypointClearPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public WaypointClearPacket()
    {
    }

    public WaypointClearPacket(WaypointScope scope)
    {
        Scope = scope;
    }

    [Key(0)]
    public WaypointScope Scope { get; set; }
}

