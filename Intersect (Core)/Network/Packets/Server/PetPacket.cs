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

        [Key(25)]
        public PetState CurrentState { get; set; }

        public PetPacket(PetState currentState /*, ... otros campos */)
        {
            
            CurrentState = currentState;
        }
    }
}
