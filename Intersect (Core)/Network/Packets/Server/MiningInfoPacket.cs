using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class MiningInfoPacket : IntersectPacket
    {

        public MiningInfoPacket(long exp, long tnl,int level)
        {



            MiningExperience = exp;
            ExperienceToMiningNextLevel = tnl;
            MiningLevel= level; 


        }

        [Key(0)]
        public long MiningExperience { get; set; }
        [Key(1)]
        public long ExperienceToMiningNextLevel { get; set; }
        [Key(2)]
        public int MiningLevel { get; set; }
    }

}
