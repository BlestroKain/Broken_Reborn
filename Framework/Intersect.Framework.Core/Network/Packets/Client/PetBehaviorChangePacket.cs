using System;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public sealed class PetBehaviorChangePacket : IntersectPacket
{
    public PetBehaviorChangePacket()
    {
    }

    public PetBehaviorChangePacket(PetState behavior, Guid petId = default)
    {
        PetId = petId;
        Behavior = behavior;
    }

    [Key(0)]
    public Guid PetId { get; set; }

    [Key(1)]
    public PetState Behavior { get; set; }
}
