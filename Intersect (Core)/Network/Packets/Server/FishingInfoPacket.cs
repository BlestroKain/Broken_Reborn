using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class FishingInfoPacket : IntersectPacket
    {

        public FishingInfoPacket(long exp, long tnl,int level)
        {
            FishingExperience = exp;
            ExperienceToFishingNextLevel = tnl;
            FishingLevel = level;
        }

        [Key(0)]
        public long FishingExperience { get; set; }
        [Key(1)]
        public long ExperienceToFishingNextLevel { get; set; }
        [Key(2)]
        public int FishingLevel { get; set; }
    }

}
