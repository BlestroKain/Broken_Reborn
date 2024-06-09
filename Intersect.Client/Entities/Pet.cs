using System;
using Intersect.Client.Framework.Entities;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Maps;
using Intersect.Enums;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;

namespace Intersect.Client.Entities
{
    public class Pet : Entity
    {
        public Guid OwnerId { get; set; }
        public int Level { get; set; }
        public int[] CurrentStats { get; set; }
       // public PetBehavior Behavior { get; set; }
        public float MovementSpeed { get; set; }

        public Pet(Guid id, PetPacket packet) : base(id, packet, EntityType.Pet)
        {
            Load(packet);
        }

        public override void Load(EntityPacket packet)
        {
            base.Load(packet);
            var petPacket = (PetPacket)packet;
            OwnerId = petPacket.OwnerId;
            Level = petPacket.Level;
            CurrentStats = petPacket.CurrentStats;
           // Behavior = petPacket.Behavior;
            MovementSpeed = petPacket.MovementSpeed;
        }

        public override bool Update()
        {
            // Actualizar lógica de movimiento según MovementSpeed
            if (IsMoving)
            {
                MoveTimer = (Timing.Global.Milliseconds) + (long)GetMovementTime();
            }

            return base.Update();
        }

        public override float GetMovementTime()
        {
            return MovementSpeed;
        }
    }
}
