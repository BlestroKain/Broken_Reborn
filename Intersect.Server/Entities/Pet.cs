using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.Logging;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
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
        protected override bool IgnoresNpcAvoid => true; // Mascotas ignoran NpcAvoid
        // Comportamientos de la mascota
        public enum PetBehavior
        {
            Passive,
            Defensive,
            Aggressive
        }
        public PetBehavior Behavior { get; set; }
        public int[] MaxVital { get; set; }
        public int[] VitalRegen { get; set; }
        public PetState State { get; set; }
        public PetBase PetBase { get; }
        public MapController AggroCenterMap;

        /// <summary>
        /// The X value on which this NPC was "aggro'd" and started chasing a target.
        /// </summary>
        public int AggroCenterX;

        /// <summary>
        /// The Y value on which this NPC was "aggro'd" and started chasing a target.
        /// </summary>
        public int AggroCenterY;

        /// <summary>
        /// The Z value on which this NPC was "aggro'd" and started chasing a target.
        /// </summary>
        public int AggroCenterZ;

        // Velocidad de movimiento de la mascota
        public float MovementSpeed { get; set; }
        public PetState CurrentState { get;  set; }
        public Guid OwnerId { get; set; }
        public byte Range;

        public ConcurrentDictionary<Entity, long> DamageMap = new ConcurrentDictionary<Entity, long>();

        public long FindTargetWaitTime;
        public int FindTargetDelay = 500;

        private int mTargetFailCounter = 0;
        private int mTargetFailMax = 10;
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
        public void SetBehavior(PetBehavior newBehavior)
        {
            if (Behavior != newBehavior)
            {
                Behavior = newBehavior;
                PacketSender.SendChatMsg(Owner, $"Tu mascota ahora está en modo {Behavior}.", ChatMessageType.Notice);
            }
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

        public virtual void Move(Direction moveDir, Player forPlayer, bool doNotUpdate = false, bool correction = false)
        {
            if (Timing.Global.Milliseconds < MoveTimer || (!Options.Combat.MovementCancelsCast && IsCasting))
            {
                return;
            }

            lock (EntityLock)
            {
                if (this is Player && IsCasting && Options.Combat.MovementCancelsCast)
                {
                    CastTime = 0;
                    CastTarget = null;
                }

                var xOffset = 0;
                var yOffset = 0;
                switch (moveDir)
                {
                    case Direction.Up:
                        --yOffset;
                        break;
                    case Direction.Down:
                        ++yOffset;
                        break;
                    case Direction.Left:
                        --xOffset;
                        break;
                    case Direction.Right:
                        ++xOffset;
                        break;
                    case Direction.UpLeft:
                        --yOffset;
                        --xOffset;
                        break;
                    case Direction.UpRight:
                        --yOffset;
                        ++xOffset;
                        break;
                    case Direction.DownRight:
                        ++yOffset;
                        ++xOffset;
                        break;
                    case Direction.DownLeft:
                        ++yOffset;
                        --xOffset;
                        break;

                    default:
                        Log.Warn(new ArgumentOutOfRangeException(nameof(moveDir), $@"Bogus move attempt in direction {moveDir}."));
                        return;
                }

                Dir = moveDir;

                var tile = new TileHelper(MapId, X, Y);

                if (tile.Translate(xOffset, yOffset))
                {
                    X = tile.GetX();
                    Y = tile.GetY();

                    var currentMap = MapController.Get(tile.GetMapId());
                    if (MapId != tile.GetMapId())
                    {
                        var oldMap = MapController.Get(MapId);
                        if (oldMap.TryGetInstance(MapInstanceId, out var oldInstance))
                        {
                            oldInstance.RemoveEntity(this);
                        }

                        if (currentMap.TryGetInstance(MapInstanceId, out var newInstance))
                        {
                            newInstance.AddEntity(this);
                        }

                        MapId = tile.GetMapId();
                    }

                    if (doNotUpdate == false)
                    {
                        if (this is EventPageInstance)
                        {
                            if (forPlayer != null)
                            {
                                PacketSender.SendEntityMoveTo(forPlayer, this, correction);
                            }
                            else
                            {
                                PacketSender.SendEntityMove(this, correction);
                            }
                        }
                        else
                        {
                            PacketSender.SendEntityMove(this, correction);
                        }

                        // Check for moving into a projectile, if so this pet needs to be hit
                        if (currentMap != null)
                        {
                            foreach (var instance in MapController.GetSurroundingMapInstances(currentMap.Id, MapInstanceId, true))
                            {
                                var projectiles = instance.MapProjectilesCached;
                                foreach (var projectile in projectiles)
                                {
                                    var spawns = projectile?.Spawns?.ToArray() ?? Array.Empty<ProjectileSpawn>();
                                    foreach (var spawn in spawns)
                                    {
                                        if (spawn == null)
                                        {
                                            continue;
                                        }

                                        if (spawn.IsAtLocation(MapId, X, Y, Z) && spawn.HitEntity(this))
                                        {
                                            spawn.Dead = true;
                                        }
                                    }
                                }
                            }
                        }

                        MoveTimer = Timing.Global.Milliseconds + (long)GetMovementTime();
                    }

                    if (TryToChangeDimension() && doNotUpdate == true)
                    {
                        PacketSender.UpdateEntityZDimension(this, (byte)Z);
                    }

                    // Check for traps
                    if (MapController.TryGetInstanceFromMap(currentMap.Id, MapInstanceId, out var mapInstance))
                    {
                        foreach (var trap in mapInstance.MapTrapsCached)
                        {
                            trap.CheckEntityHasDetonatedTrap(this);
                        }
                    }

                    var attribute = currentMap?.Attributes[X, Y];
                    if (attribute?.Type == MapAttribute.Slide)
                    {
                        if (((MapSlideAttribute)attribute).Direction > 0)
                        {
                            Dir = (Direction)(((MapSlideAttribute)attribute).Direction - 1);
                        }

                        var dash = new Dash(this, 1, Dir);
                    }
                }
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

            if (Behavior == PetBehavior.Aggressive)
            {
                TryFindNewTarget(timeMs);
            }
            else if (Behavior == PetBehavior.Defensive && Owner.IsUnderAttack())
            {
                var attacker = Owner.GetLastAttacker();
                if (attacker != null)
                {
                    TryFindNewTarget(timeMs, Guid.Empty, true, attacker);
                }
            }
        }


        public void TryFindNewTarget(long timeMs, Guid avoidId = new Guid(), bool ignoreTimer = false, Entity attackedBy = null)
        {
            if (!ignoreTimer && FindTargetWaitTime > timeMs)
            {
                return;
            }

            var possibleTargets = new List<Entity>();
            var closestRange = Range + 1;
            var closestIndex = -1;
            var highestDmgIndex = -1;

            foreach (var en in DamageMap.ToArray())
            {
                if (en.Key.Id == avoidId || en.Key.IsDead() || en.Key.MapInstanceId != MapInstanceId)
                {
                    continue;
                }

                if (GetDistanceTo(en.Key) != 9999)
                {
                    possibleTargets.Add(en.Key);

                    if (en.Value > possibleTargets.Select(t => DamageMap[t]).DefaultIfEmpty(0).Max())
                    {
                        highestDmgIndex = possibleTargets.Count - 1;
                    }
                }
            }

            foreach (var instance in MapController.GetSurroundingMapInstances(MapId, MapInstanceId, true))
            {
                foreach (var entity in instance.GetEntities())
                {
                    if (entity != null && !entity.IsDead() && entity != this && entity.Id != avoidId)
                    {
                        if (CanPetCombat(entity))
                        {
                            var dist = GetDistanceTo(entity);
                            if (dist <= Range && dist < closestRange)
                            {
                                possibleTargets.Add(entity);
                                closestIndex = possibleTargets.Count - 1;
                                closestRange = dist;
                            }
                        }
                    }
                }
            }

            if (highestDmgIndex != -1)
            {
                AssignTarget(possibleTargets[highestDmgIndex]);
            }
            else if (possibleTargets.Count > 0)
            {
                AssignTarget(possibleTargets.OrderBy(t => Randomization.Next()).FirstOrDefault());
            }
            else
            {
                mTargetFailCounter += 1;
                
            }

            FindTargetWaitTime = timeMs + FindTargetDelay;
        }
        public void AssignTarget(Entity en)
        {
            var oldTarget = Target;

            var pathTarget = mPathFinder?.GetTarget();
            if (AggroCenterMap != null && pathTarget != null &&
                pathTarget.TargetMapId == AggroCenterMap.Id && pathTarget.TargetX == AggroCenterX && pathTarget.TargetY == AggroCenterY)
            {
                if (en == null)
                {
                    return;
                }
                else
                {
                    return;
                }
            }

            if (en != null && en != Target)
            {
                if ((pathTarget != null && AggroCenterMap != null && (pathTarget.TargetMapId != AggroCenterMap.Id || pathTarget.TargetX != AggroCenterX || pathTarget.TargetY != AggroCenterY)))
                {
                    foreach (var status in CachedStatuses)
                    {
                        if (status.Type == SpellEffect.Taunt && en != status.Attacker && GetDistanceTo(status.Attacker) != 9999)
                        {
                            return;
                        }
                    }
                }

                if (en is Projectile projectile)
                {
                    if (projectile.Owner != this && !TargetHasStealth(projectile))
                    {
                        Target = projectile.Owner;
                    }
                }
                else
                {
                    if (CanPetCombat(en))
                    {
                        Target = en;
                    }
                }

                
            }
            else
            {
                Target = en;
            }

            if (Target != oldTarget)
            {
                CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
                PacketSender.SendPetAggressionToProximity(this);
            }
            mTargetFailCounter = 0;
        }

        public bool TargetHasStealth(Entity target)
        {
            return target == null || target.CachedStatuses.Any(s => s.Type == SpellEffect.Stealth);
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
        #region Attack
        public override void TryAttack(Entity target)
        {
            if (target.IsDisposed)
            {
                return;
            }

            if (!CanAttack(target, null))
            {
                return;
            }

            if (!IsOneBlockAway(target))
            {
                return;
            }

            if (!IsFacingTarget(target))
            {
                return;
            }

            var deadAnimations = new List<KeyValuePair<Guid, Direction>>();
            var aliveAnimations = new List<KeyValuePair<Guid, Direction>>();

            if (IsAttacking)
            {
                return;
            }

            // Puedes definir una animación de ataque específica para la mascota, si es necesario.
            // if (Base.AttackAnimation != null)
            // {
            //     PacketSender.SendAnimationToProximity(
            //         Base.AttackAnimationId, -1, Guid.Empty, target.MapId, (byte)target.X, (byte)target.Y,
            //         Dir, target.MapInstanceId
            //     );
            // }

            base.TryAttack(
               target, PetBase.Damage, (DamageType)PetBase.DamageType, (Enums.Stat)PetBase.ScalingStat, (int)PetBase.Scaling,
               PetBase.CritChance, PetBase.CritMultiplier, deadAnimations, aliveAnimations
               );
           PacketSender.SendEntityAttack(this, CalculateAttackTime());
        }


        public bool CanPetCombat(Entity enemy, bool friendly = false)
        {
            if (friendly)
            {
                return false;
            }

            if (enemy != null && enemy is Npc enemyNpc)
            {
                // Verifica si el NPC enemigo permite el combate entre NPCs
                if (enemyNpc.Base.NpcVsNpcEnabled == false)
                {
                    return false;
                }

                return true;
            }

            if (enemy is Player)
            {
                return true;
            }

            return false;
        }
        public override bool CanAttack(Entity entity, SpellBase spell)
        {
            var npc = entity as Npc;
            if (npc != default && !CanPetCombat(npc))
            {
                return false;
            }

            if (spell?.Combat?.TargetType == SpellTargetType.Self ||
                spell?.Combat?.TargetType == SpellTargetType.Projectile ||
                spell?.Combat.TargetType == SpellTargetType.Trap ||
                spell?.SpellType == SpellType.Dash)
            {
                return true;
            }

            if (!base.CanAttack(entity, spell))
            {
                return false;
            }

            if (entity is EventPageInstance)
            {
                return false;
            }

            var friendly = spell?.Combat != null && spell.Combat.Friendly;
            if (entity is Player player)
            {
                if (player.InParty(Owner) || Owner == player ||
                    (!Options.Instance.Guild.AllowGuildMemberPvp && friendly != (player.Guild != null && player.Guild == Owner.Guild)))
                {
                    return friendly;
                }

                if (!friendly &&
                    (spell?.Combat?.TargetType != SpellTargetType.Self &&
                     spell?.Combat?.TargetType != SpellTargetType.AoE &&
                     spell?.SpellType == SpellType.CombatSpell))
                {
                    if (MapController.Get(MapId).ZoneType == MapZone.Safe ||
                        MapController.Get(player.MapId).ZoneType == MapZone.Safe)
                    {
                        return false;
                    }

                    if (MapController.Get(MapId).ZoneType != MapController.Get(player.MapId).ZoneType)
                    {
                        return false;
                    }
                }
            }

            if (entity is Resource)
            {
                if (spell != null)
                {
                    return false;
                }
            }

            if (npc != default)
            {
                return !friendly && CanPetCombat(npc) || friendly && npc.IsAllyOf(this);
            }

            return true;
        }
       
        #endregion
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
