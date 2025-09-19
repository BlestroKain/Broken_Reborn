using System;
using Intersect.Enums;
using Intersect.Shared.Pets;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public sealed class PetStateUpdatePacket : IntersectPacket
{
    public PetStateUpdatePacket()
    {
    }

    public PetStateUpdatePacket(Guid petId, PetState state, PetBehavior behavior)
    {
        PetId = petId;
        State = state;
        Behavior = behavior;
    }

    [Key(0)]
    public Guid PetId { get; set; }

    [Key(1)]
    public PetState State { get; set; }

    [Key(2)]
    public PetBehavior Behavior { get; set; }
}
