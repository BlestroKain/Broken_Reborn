using System;
using Intersect.Network;
using MessagePack;


namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class AssignSpellPointPacket : IntersectPacket
    {
        [Key(0)]
        public int Slot { get; set; }

        public AssignSpellPointPacket() { }

        public AssignSpellPointPacket(int slot)
        {
            Slot = slot;
        }
    }
}