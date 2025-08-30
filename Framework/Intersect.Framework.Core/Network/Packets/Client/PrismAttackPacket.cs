using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class PrismAttackPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public PrismAttackPacket()
    {
    }

    public PrismAttackPacket(Guid mapId, Guid prismId)
    {
        MapId = mapId;
        PrismId = prismId;
    }

    [Key(0)]
    public Guid MapId { get; set; }

    [Key(1)]
    public Guid PrismId { get; set; }
}
