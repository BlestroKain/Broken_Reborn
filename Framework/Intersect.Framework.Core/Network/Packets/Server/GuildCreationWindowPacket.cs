using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public class GuildCreationWindowPacket : IntersectPacket
{
    public GuildCreationWindowPacket()
    {
    }
}
