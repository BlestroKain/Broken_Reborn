using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class SpellLevelChangePacket : IntersectPacket
    {
        // Constructor sin parámetros requerido por MessagePack
        public SpellLevelChangePacket() { }

        public SpellLevelChangePacket(int slotIndex, int delta)
        {
            SlotIndex = slotIndex;
            Delta = delta;
        }

        [Key(0)]
        public int SlotIndex { get; set; }

        [Key(1)]
        public int Delta { get; set; } // +1 o -1
    }
}
