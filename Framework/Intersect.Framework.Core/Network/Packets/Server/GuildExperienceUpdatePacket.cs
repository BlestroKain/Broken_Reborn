using Intersect.Network;
using MessagePack;

namespace Intersect.Network.Packets.Server;
[MessagePackObject]
public partial class GuildExperienceUpdatePacket: IntersectPacket

{
    public GuildExperienceUpdatePacket()
    {
    }
    public GuildExperienceUpdatePacket(float experience)
    {
        Experience = experience;
    }

    [Key(0)]
    public float Experience;

}