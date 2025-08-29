using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class PrismUpdatePacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public PrismUpdatePacket()
    {
    }

    public PrismUpdatePacket(Guid mapId, Alignment owner, PrismState state)
    {
        MapId = mapId;
        Owner = owner;
        State = state;
    }

    [Key(0)]
    public Guid MapId { get; set; }

    [Key(1)]
    public Alignment Owner { get; set; }

    [Key(2)]
    public PrismState State { get; set; }
}
