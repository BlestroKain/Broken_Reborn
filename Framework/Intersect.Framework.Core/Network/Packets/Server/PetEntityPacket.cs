using Intersect.Enums;
using Intersect.Framework.Core;
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

    [Key(26)]
    public PetState State { get; set; } // Ensure PetState is defined in the correct namespace

    [Key(27)]
    public bool Despawnable { get; set; }
}
