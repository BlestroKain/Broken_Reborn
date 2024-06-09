using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Maps;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Pathfinding;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;
using MapAttribute = Intersect.Enums.MapAttribute;

namespace Intersect.Server.Entities
{
    public partial class Pet : Entity
    {
        private Pathfinder mPathFinder;

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

        // Velocidad de movimiento de la mascota
        public float MovementSpeed { get; set; }
        public PetState CurrentState { get; private set; }
        public Guid OwnerId { get; set; }

        public Pet(Player owner, PetBase petBase)
        {
            PetBase = petBase;
            Owner = owner;
            Name = petBase.Name;
            Sprite = petBase.Sprite;
            Level = petBase.Level;
            Experience = petBase.Experience;
            Behavior = PetBehavior.Defensive;

            BaseStats = petBase.PetStats;
            CurrentStats = new int[BaseStats.Length];
            Array.Copy(BaseStats, CurrentStats, BaseStats.Length);
            mPathFinder = new Pathfinder(this);
            MaxVital = new int[petBase.MaxVital.Length];
            VitalRegen = new int[petBase.VitalRegen.Length];
            Array.Copy(petBase.MaxVital, MaxVital, petBase.MaxVital.Length);
            Array.Copy(petBase.VitalRegen, VitalRegen, petBase.VitalRegen.Length);

            for (var i = 0; i < (int)Vital.VitalCount; i++)
            {
                SetMaxVital(i, MaxVital[i]);
                SetVital(i, MaxVital[i]);
            }

            // Ajustar la velocidad de movimiento a la del propietario
            MovementSpeed = CurrentStats[(int)Enums.Stat.Speed];
                }

        public void FollowOwner(long timeMs)
        {
            if (Owner == null || Owner.IsDisposed)
            {
                return;
            }

            var targetMap = Owner.MapId;
            var targetX = Owner.X;
            var targetY = Owner.Y;
            var targetZ = Owner.Z;

            if (!InRangeOf(Owner, 10))
            {
                switch (Owner.Dir)
                {
                    case Direction.Up:
                        targetY += 1;
                        break;
                    case Direction.Down:
                        targetY -= 1;
                        break;
                    case Direction.Left:
                        targetX += 1;
                        break;
                    case Direction.Right:
                        targetX -= 1;
                        break;
                }

                Warp(Owner.MapId, targetX, targetY, Owner.Dir);
            }
            else
            {
                if (mPathFinder.GetTarget() != null)
                {
                    if (targetMap != mPathFinder.GetTarget().TargetMapId ||
                        targetX != mPathFinder.GetTarget().TargetX ||
                        targetY != mPathFinder.GetTarget().TargetY)
                    {
                        mPathFinder.SetTarget(null);
                    }
                }

                if (mPathFinder.GetTarget() == null)
                {
                    mPathFinder.SetTarget(new PathfinderTarget(targetMap, targetX, targetY, targetZ));
                }

                if (mPathFinder.GetTarget() != null)
                {
                    switch (mPathFinder.Update(timeMs))
                    {
                        case PathfinderResult.Success:
                            var direction = mPathFinder.GetMove();

                            if (direction != Direction.None && CanMoveInDirection(direction))
                            {
                                Move(direction, null);
                                MoveTimer = Timing.Global.Milliseconds + (long)(Owner.GetMovementTime() * 0.75f);
                            }
                            break;
                        case PathfinderResult.OutOfRange:
                            PacketSender.SendChatMsg(Owner, "Tu invocación está fuera de rango.", ChatMessageType.Error);
                            break;
                        case PathfinderResult.NoPathToTarget:
                        case PathfinderResult.Failure:
                            PacketSender.SendChatMsg(Owner, "Tu invocación no puede encontrarte.", ChatMessageType.Error);
                            break;
                        case PathfinderResult.Wait:
                            break;
                    }
                }
            }
        }

        public override void Move(Direction moveDir, Player forPlayer, bool dontUpdate = false, bool correction = false)
        {
            lock (EntityLock)
            {
                var oldMap = MapId;
                base.Move(moveDir, forPlayer, dontUpdate, correction);

                // Check for a warp, if so warp the pet.
                var attribute = MapController.Get(MapId).Attributes[X, Y];
                if (attribute != null && attribute.Type == MapAttribute.Warp)
                {
                    var warpAtt = (MapWarpAttribute)attribute;
                    var dir = Dir;
                    if (warpAtt.Direction != WarpDirection.Retain)
                    {
                        dir = (Direction)(warpAtt.Direction - 1);
                    }

                    MapInstanceType? instanceType = null;
                    if (warpAtt.ChangeInstance)
                    {
                        instanceType = warpAtt.InstanceType;
                    }

                    Warp(warpAtt.MapId, warpAtt.X, warpAtt.Y, dir, false, 0, false, false, instanceType);
                }

                MoveTimer = Timing.Global.Milliseconds + (long)(Owner.GetMovementTime() * 0.75f); // Ajustar el tiempo del próximo movimiento según la velocidad
            }
        }


        public override void Warp(Guid newMapId,
            float newX,
            float newY,
            Direction newDir,
            bool adminWarp = false,
            int zOverride = 0,
            bool mapSave = false,
            bool fromWarpEvent = false,
            MapInstanceType? mapInstanceType = null,
            bool fromLogin = false,
            bool forceInstanceChange = false)
        {
            if (!MapController.TryGetInstanceFromMap(newMapId, MapInstanceId, out var map))
            {
                return;
            }

            if (newMapId != MapId)
            {
                if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var oldMap))
                {
                    oldMap.RemoveEntity(this);
                }

                PacketSender.SendEntityLeave(this);
                MapId = newMapId;
                PacketSender.SendEntityDataToProximity(this);
                PacketSender.SendEntityPositionToAll(this);
            }

            X = (int)newX;
            Y = (int)newY;
            Z = zOverride;
            Dir = newDir;
            PacketSender.SendEntityPositionToAll(this);
            PacketSender.SendEntityStats(this);
        }

        public override void Update(long timeMs)
        {
            base.Update(timeMs);
            FollowOwner(timeMs);
        }

        public override void Die(bool generateLoot = true, Entity killer = null)
        {
            base.Die(generateLoot, killer);
            if (Owner != null)
            {
                Owner.CurrentPet = null;
                Owner = null;
            }

            mPathFinder = null;
        }

        public override EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
        {
            if (packet == null)
            {
                packet = new PetPacket(
                    CurrentState,
                    Owner.Id,
                    Level,
                    CurrentStats,
                    X,
                    Y,
                    Z,
                    Dir,
                    MovementSpeed
                );
            }

            packet = base.EntityPacket(packet, forPlayer);

            var pkt = (PetPacket)packet;
            pkt.CurrentState = CurrentState;
            pkt.OwnerId = Owner.Id;
            pkt.Level = Level;
            pkt.CurrentStats = CurrentStats;
            pkt.X = X;
            pkt.Y = Y;
            pkt.Z = Z;
            pkt.Dir = Dir;
            pkt.MovementSpeed = MovementSpeed;

            return pkt;
        }

    }
}
