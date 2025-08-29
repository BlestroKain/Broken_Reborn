using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class PrismListPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public PrismListPacket()
    {
    }

    public PrismListPacket(PrismUpdatePacket[] prisms)
    {
        Prisms = prisms;
    }

    [Key(0)]
    public PrismUpdatePacket[] Prisms { get; set; }
}
