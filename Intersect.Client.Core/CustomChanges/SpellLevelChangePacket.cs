using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class SpellLevelChangePacket : IntersectPacket
    {
        // Constructor sin parámetros requerido por MessagePack
        public SpellLevelChangePacket() { }

        public SpellLevelChangePacket(int spellSlot, int delta)
        {
            SpellSlot = spellSlot;
            Delta = delta;
        }

        [Key(0)]
        public int SpellSlot { get; set; }

        [Key(1)]
        public int Delta { get; set; } // +1 para subir, -1 para bajar
    }
}
