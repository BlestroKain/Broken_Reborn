using Intersect.Network;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class SpellPointsPacket : IntersectPacket
    {
        public SpellPointsPacket() { }

        public SpellPointsPacket(int spellPoints)
        {
            SpellPoints = spellPoints;
        }

        [Key(0)]
        public int SpellPoints { get; set; }
    }
}