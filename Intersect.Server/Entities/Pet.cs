using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Models;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Pathfinding;
using Intersect.Utilities;

namespace Intersect.Server.Entities
{
    public partial class Pet : Entity
    {
        // Propiedad para el propietario de la mascota
        public Player Owner { get; set; }

        // Nivel y experiencia de la mascota
        public int Level { get; set; }
        public long Experience { get; set; }

        // Estadísticas base de la mascota
        public int[] BaseStats { get; set; }
        public int[] CurrentStats { get; set; }

        // Comportamientos de la mascota
        public enum PetBehavior
        {
            Passive,
            Defensive,
            Aggressive
        }
        public PetBehavior Behavior { get; set; }
        public int[] MaxVital { get; private set; }
        public int[] VitalRegen { get; private set; }
        public PetState State { get; private set; }
        public PetBase PetBase { get; }

        // Constructor
        public Pet(Player owner, PetBase petBase)
        {
            PetBase = petBase;
            Owner = owner;
            Name = petBase.Name;
            Sprite = petBase.Sprite;
            Level = petBase.Level;
            Experience = petBase.Experience;
            Behavior = PetBehavior.Passive;

            // Copiar estadísticas base
            BaseStats = petBase.PetStats;
            CurrentStats = new int[BaseStats.Length];
            Array.Copy(BaseStats, CurrentStats, BaseStats.Length);

            MaxVital = petBase.MaxVital;
            VitalRegen = petBase.VitalRegen;
        }

        // Método para seguir al propietario
        public void FollowOwner()
        {
            if (Owner != null && !Owner.IsDisposed)
            {
                if (!InRangeOf(Owner, 2))
                {
                    var direction = DirToEnemy(Owner);
                    if (direction != Direction.None && CanMoveInDirection(direction))
                    {
                        Move(direction, null);
                    }
                }
            }
        }

        // Método para atacar enemigos
        public void AttackEnemy(Entity target)
        {
            if (Behavior == PetBehavior.Aggressive && target != null && !target.IsDead() && InRangeOf(target, 1))
            {
                TryAttack(target);
            }
        }

        // Método para procesar experiencia y nivel
        public void AddExperience(long amount)
        {
            Experience += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            var requiredExp = GetExperienceToNextLevel(Level);
            while (Experience >= requiredExp && Level < 100) // Asumiendo nivel máximo 100
            {
                Experience -= requiredExp;
                Level++;
                LevelUp();
                requiredExp = GetExperienceToNextLevel(Level);
            }
        }

        private long GetExperienceToNextLevel(int level)
        {
            return 1000 * level; // Fórmula simplificada para la experiencia requerida por nivel
        }

        private void LevelUp()
        {
            // Incrementar estadísticas al subir de nivel
            for (var i = 0; i < CurrentStats.Length; i++)
            {
                CurrentStats[i] += 10; // Incremento simplificado
            }
        }

        // Sobreescribir el método Update para incluir comportamiento de la mascota
        public override void Update(long timeMs)
        {
            base.Update(timeMs);
            FollowOwner();
            if (Behavior == PetBehavior.Aggressive && Owner != null && Owner.Target != null)
            {
                AttackEnemy(Owner.Target);
            }
        }

        // Sobreescribir el método Die para gestionar la muerte de la mascota
        public override void Die(bool generateLoot = true, Entity killer = null)
        {
            base.Die(generateLoot, killer);
            if (Owner != null)
            {
                Owner.CurrentPet = null;
            }
        }

        // Método para apareamiento de mascotas
        /*public Pet Breed(Pet mate)
        {
            if (mate == null || mate.Owner != Owner)
            {
                return null;
            }

            var newPetBase = PetBase.GetRandomPetBase(); // Método ficticio para obtener una base de datos de mascota aleatoria
            var newPet = new Pet(Owner, newPetBase)
            {
                Level = 1,
                Experience = 0
            };

            // Mezclar estadísticas de los padres
            for (var i = 0; i < BaseStats.Length; i++)
            {
                newPet.BaseStats[i] = (BaseStats[i] + mate.BaseStats[i]) / 2;
                newPet.CurrentStats[i] = newPet.BaseStats[i];
            }

            return newPet;
        }*/
        public override EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
        {
            if (packet == null)
            {
                packet = new PetPacket();
            }

            packet = base.EntityPacket(packet, forPlayer);

            var pkt = (PetPacket)packet;
            pkt.CurrentState = State;  // Aquí asignamos el estado actual de la mascota al paquete

            return pkt;
        }
    }
}
