using MessagePack;
using Intersect.Network;
using Intersect.Enums;
namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PetPacket :  EntityPacket
    {
        public PetPacket()
        {
        }

        [Key(0)]
        public string PetId { get; set; }

        [Key(1)]
        public int PetLevel { get; set; }

        [Key(2)]
        public long PetExperience { get; set; }

        [Key(3)]
        public PetState CurrentState { get; set; }

        public PetPacket(string petId, int petLevel, long petExperience,PetState currentState /*, ... otros campos */)
        {
            PetId = petId;
            PetLevel = petLevel;
            PetExperience = petExperience;
            CurrentState = currentState;
        }
    }
}
