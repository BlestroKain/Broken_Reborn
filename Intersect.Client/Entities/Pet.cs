using System;
using Intersect.Core.Pets;
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
        public float MovementSpeed { get; set; }

        // Nuevas Propiedades
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public PetRarity Rarity { get; set; }
        public PetPersonality Personality { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Hunger { get; set; }
        public int Mood { get; set; }
        public int BreedCount { get; set; }
        public int BreedStatus { get; set; }

        private PetPersonalityHandler mPersonalityHandler;

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
            MovementSpeed = petPacket.MovementSpeed;

            // Cargar nuevas propiedades desde el paquete
            Name = petPacket.Name;
            Gender = petPacket.Gender;
            Rarity = petPacket.Rarity;
            Personality = petPacket.Personality;
            Health = petPacket.Health;
            MaxHealth = petPacket.MaxHealth;
            Hunger = petPacket.Hunger;
            Mood = petPacket.Mood;
            BreedCount = petPacket.BreedCount;
                     
        }

        public override bool Update()
        {
            // Actualizar lógica de movimiento según MovementSpeed
            if (IsMoving)
            {
                MoveTimer = Timing.Global.Milliseconds + (long)GetMovementTime();
            }

            mPersonalityHandler?.OnTick();
            return base.Update();
        }

        public override float GetMovementTime()
        {
            return MovementSpeed;
        }

        internal void SetPersonalityHandler()
        {
            throw new NotImplementedException();
        }
    }
}
