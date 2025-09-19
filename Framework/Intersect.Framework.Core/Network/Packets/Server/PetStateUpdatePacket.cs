using System;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public sealed class PetStateUpdatePacket : IntersectPacket
{
    public PetStateUpdatePacket()
    {
    }

    public PetStateUpdatePacket(Guid petId, PetState behavior)
    {
        PetId = petId;
        Behavior = behavior;
    }

    [Key(0)]
    public Guid PetId { get; set; }

    [Key(2)]
    public PetState Behavior { get; set; }
}
