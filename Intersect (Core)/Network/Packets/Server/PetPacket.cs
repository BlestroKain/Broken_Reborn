using MessagePack;
using Intersect.Network;
using Intersect.Enums;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PetPacket : EntityPacket
    {
        public PetPacket()
        {
        }

        [Key(24)]
        public PetState CurrentState { get; set; }

        [Key(25)]
        public Guid OwnerId { get; set; }

        [Key(26)]
        public int Level { get; set; }

        [Key(27)]
        public int[] CurrentStats { get; set; }

        [Key(28)]
        public int X { get; set; }

        [Key(29)]
        public int Y { get; set; }

        [Key(30)]
        public int Z { get; set; }

        [Key(31)]
        public Direction Dir { get; set; }

        [Key(32)]
        public float MovementSpeed { get; set; }
        [Key(33)]
        public Guid Id { get; set; }

        public PetPacket(
            PetState currentState,
            Guid ownerId,
            int level,
            int[] currentStats,
            int x,
            int y,
            int z,
            Direction dir,
            float movementSpeed
        /*, ... otros campos */
        )
        {
            CurrentState = currentState;
            OwnerId = ownerId;
            Level = level;
            CurrentStats = currentStats;
            X = x;
            Y = y;
            Z = z;
            Dir = dir;
            MovementSpeed = movementSpeed;
        }
    }
}
