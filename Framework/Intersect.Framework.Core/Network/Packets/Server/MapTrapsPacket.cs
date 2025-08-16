using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MapTrapsPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MapTrapsPacket()
    {
    }

    public MapTrapsPacket(Guid mapId, MapTrapPacket[] traps)
    {
        MapId = mapId;
        Traps = traps;
    }

    [Key(0)]
    public Guid MapId { get; set; }

    [Key(1)]
    public MapTrapPacket[] Traps { get; set; }
}
