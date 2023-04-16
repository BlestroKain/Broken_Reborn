using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class FarmingInfoPacket : IntersectPacket
    {

        public FarmingInfoPacket(long exp, long tnl,int level)
        {
            
            FarmingExperience = exp;
            ExperienceToFarmingNextLevel = tnl;
            FarmingLevel= level;
           
        }

        [Key(0)]
        public long FarmingExperience { get; set; }
        [Key(1)]
        public long ExperienceToFarmingNextLevel { get; set; }
        [Key(2)]
        public int FarmingLevel { get; set; }
    }

}
