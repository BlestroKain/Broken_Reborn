using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class WoodInfoPacket : IntersectPacket
    {

        public WoodInfoPacket(long exp, long tnl,int level)
        {
            WoodExperience = exp;
            ExperienceToWoodNextLevel = tnl;
            WoodLevel = level;
        }

        [Key(0)]
        public long WoodExperience { get; set; }
        [Key(1)]
        public long ExperienceToWoodNextLevel { get; set; }
        [Key(2)]
        public int WoodLevel  {get; set; }




    }

}
