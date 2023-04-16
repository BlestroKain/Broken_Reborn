using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class AlchemyInfoPacket : IntersectPacket
    {

        public AlchemyInfoPacket(long exp, long tnl, int level)
        {

            AlchemyExperience = exp;
            ExperienceToAlchemyNextLevel = tnl;
            AlchemyLevel = level;

        }

        [Key(0)]
        public long AlchemyExperience { get; set; }
        [Key(1)]
        public long ExperienceToAlchemyNextLevel { get; set; }
        [Key(2)]
        public int AlchemyLevel { get; set; }
    }

}
