using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class HuntingInfoPacket : IntersectPacket
    {

        public HuntingInfoPacket(long exp, long tnl, int level)
        {

            HuntingExperience = exp;
            ExperienceToHuntingNextLevel = tnl;
            HuntingLevel = level;

        }

        [Key(0)]
        public long HuntingExperience { get; set; }
        [Key(1)]
        public long ExperienceToHuntingNextLevel { get; set; }
        [Key(2)]
        public int HuntingLevel { get; set; }
    }

}
