using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class CookingInfoPacket : IntersectPacket
    {

        public CookingInfoPacket(long exp, long tnl, int level)
        {

            CookingExperience = exp;
            ExperienceToCookingNextLevel = tnl;
            CookingLevel = level;

        }

        [Key(0)]
        public long CookingExperience { get; set; }
        [Key(1)]
        public long ExperienceToCookingNextLevel { get; set; }
        [Key(2)]
        public int CookingLevel { get; set; }
    }

}
