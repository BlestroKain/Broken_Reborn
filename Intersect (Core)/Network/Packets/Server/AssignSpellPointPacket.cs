using Intersect.Network;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class AssignSpellPointPacket : IntersectPacket
    {
        public AssignSpellPointPacket() { }

        public AssignSpellPointPacket(int slot)
        {
            Slot = slot;
        }

        [Key(0)]
        public int Slot { get; set; }
    }
}
