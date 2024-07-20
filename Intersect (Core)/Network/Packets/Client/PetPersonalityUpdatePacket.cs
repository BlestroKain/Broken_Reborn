using MessagePack;
using Intersect.Network;
using Intersect.Enums;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PetPersonalityUpdatePacket : IntersectPacket
    {
        [Key(0)]
        public Guid PetId { get; set; }

        [Key(1)]
        public PetPersonality Personality { get; set; }

        public PetPersonalityUpdatePacket() { }

        public PetPersonalityUpdatePacket(Guid petId, PetPersonality personality)
        {
            PetId = petId;
            Personality = personality;
        }
    }
}
