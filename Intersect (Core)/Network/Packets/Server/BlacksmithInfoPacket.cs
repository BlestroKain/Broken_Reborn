using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class BlacksmithInfoPacket : IntersectPacket
    {

        public BlacksmithInfoPacket(long exp, long tnl, int level)
        {

            BlacksmithExperience = exp;
            ExperienceToBlacksmithNextLevel = tnl;
            BlacksmithLevel = level;

        }

        [Key(0)]
        public long BlacksmithExperience { get; set; }
        [Key(1)]
        public long ExperienceToBlacksmithNextLevel { get; set; }
        [Key(2)]
        public int BlacksmithLevel { get; set; }
    }

}
