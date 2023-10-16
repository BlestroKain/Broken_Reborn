using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class CraftingInfoPacket : IntersectPacket
    {

        public CraftingInfoPacket(long exp, long tnl, int level)
        {

            CraftingExperience = exp;
            ExperienceToCraftingNextLevel = tnl;
            CraftingLevel = level;

        }

        [Key(0)]
        public long CraftingExperience { get; set; }
        [Key(1)]
        public long ExperienceToCraftingNextLevel { get; set; }
        [Key(2)]
        public int CraftingLevel { get; set; }
    }

}
