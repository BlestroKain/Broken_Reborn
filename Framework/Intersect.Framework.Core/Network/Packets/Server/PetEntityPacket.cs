
using System;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public sealed class PetEntityPacket : EntityPacket
{
    // Parameterless constructor for MessagePack
    public PetEntityPacket()
    {
    }

    [Key(24)]
    public Guid OwnerId { get; set; }

    [Key(25)]
    public Guid DescriptorId { get; set; }

    [Key(27)]
    public bool Despawnable { get; set; }

    [Key(28)]
    public PetState Behavior { get; set; }
}
