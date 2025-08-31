using Intersect.Enums;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class WaypointSetPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public WaypointSetPacket()
    {
    }

    public WaypointSetPacket(int x, int y, WaypointScope scope)
    {
        X = x;
        Y = y;
        Scope = scope;
    }

    [Key(0)]
    public int X { get; set; }

    [Key(1)]
    public int Y { get; set; }

    [Key(2)]
    public WaypointScope Scope { get; set; }
}

