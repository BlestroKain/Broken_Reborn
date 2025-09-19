using System;
using Intersect.Shared.Pets;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public sealed class PetBehaviorChangePacket : IntersectPacket
{
    public PetBehaviorChangePacket()
    {
    }

    public PetBehaviorChangePacket(PetBehavior behavior, Guid petId = default)
    {
        PetId = petId;
        Behavior = behavior;
    }

    [Key(0)]
    public Guid PetId { get; set; }

    [Key(1)]
    public PetBehavior Behavior { get; set; }
}
