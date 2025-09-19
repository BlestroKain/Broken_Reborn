using System;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public sealed class PetEntityUpdatePacket : IntersectPacket
{
    [Key(0)]
    public Guid MapId { get; set; }

    [Key(1)]
    public PetEntityUpdate[] EntityUpdates { get; set; } = Array.Empty<PetEntityUpdate>();

    public PetEntityUpdatePacket(Guid mapId, PetEntityUpdate[] entityUpdates)
    {
        MapId = mapId;
        EntityUpdates = entityUpdates ?? Array.Empty<PetEntityUpdate>();
    }

    // Parameterless constructor for MessagePack
    public PetEntityUpdatePacket()
    {
    }
}

[MessagePackObject]
public sealed class PetEntityUpdate
{
    [Key(0)]
    public Guid EntityId { get; set; }

    [Key(1)]
    public Guid OwnerId { get; set; }

    [Key(2)]
    public Guid DescriptorId { get; set; }

    [Key(4)]
    public bool Despawnable { get; set; }

    [Key(5)]
    public PetState Behavior { get; set; }
}
