using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class PrismAttackPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public PrismAttackPacket()
    {
    }

    public PrismAttackPacket(Guid mapId)
    {
        MapId = mapId;
    }

    [Key(0)]
    public Guid MapId { get; set; }
}
