using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Pathfinding;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;

namespace Intersect.Server.Entities;

public sealed class Pet : Entity
{
    private const int FollowDistance = 3;
    private const long PathUpdateInterval = 100;

    private readonly Pathfinder _pathfinder;

    private long _nextPathUpdate;

    private Guid _ownerMapId;
    private Guid _ownerMapInstanceId;

    private Player? _ownerCache;

    public PetDescriptor Descriptor { get; }

    public Guid OwnerId { get; }

    public bool Despawnable { get; }

    public Player? Owner
    {
        get
        {
            if (_ownerCache == null || _ownerCache.Id != OwnerId || _ownerCache.IsDisposed)
            {
                _ownerCache = Player.FindOnline(OwnerId);
            }

            return _ownerCache;
        }
        private set => _ownerCache = value;
    }

    public PetState State { get; private set; } = PetState.Idle;

    public Pet(
        PetDescriptor descriptor,
        Player owner,
        bool despawnable = false,
        bool register = true,
        Guid? mapIdOverride = null,
        Guid? mapInstanceIdOverride = null,
        int? xOverride = null,
        int? yOverride = null,
        Direction? directionOverride = null
    )
    {
        ArgumentNullException.ThrowIfNull(descriptor);
        ArgumentNullException.ThrowIfNull(owner);

        Descriptor = descriptor;
        OwnerId = owner.Id;
        Owner = owner;
        Despawnable = despawnable;

        var spawnMapId = mapIdOverride ?? owner.MapId;
        var spawnMapInstanceId = mapInstanceIdOverride ?? owner.MapInstanceId;
        var spawnX = xOverride ?? owner.X;
        var spawnY = yOverride ?? owner.Y;
        var spawnDirection = directionOverride ?? owner.Dir;

        Name = string.IsNullOrWhiteSpace(owner.ActivePet?.CustomName)
            ? descriptor.Name
            : owner.ActivePet.CustomName;
        Sprite = descriptor.Sprite;
        Level = descriptor.Level;
        Immunities = descriptor.Immunities?.ToList() ?? [];

        for (var index = 0; index < Enum.GetValues<Stat>().Length; index++)
        {
            BaseStats[index] = descriptor.Stats[index];
            Stat[index] = new Combat.Stat((Stat)index, this);
        }

        for (var index = 0; index < Enum.GetValues<Vital>().Length; index++)
        {
            SetMaxVital(index, descriptor.MaxVitals[index]);
            SetVital(index, descriptor.MaxVitals[index]);
        }

        var spellSlot = 0;
        foreach (var spellId in descriptor.Spells)
        {
            var slot = new PlayerSpell(spellSlot++);
            slot.Set(new Spell(spellId));
            Spells.Add(slot);
        }

        if (descriptor.IdleAnimationId != Guid.Empty)
        {
            Animations.Add(descriptor.IdleAnimationId);
        }

        if (descriptor.DeathAnimationId != Guid.Empty)
        {
            DeathAnimation = descriptor.DeathAnimationId;
        }

        MapId = spawnMapId;
        MapInstanceId = spawnMapInstanceId;
        X = spawnX;
        Y = spawnY;
        Z = owner.Z;
        Dir = spawnDirection;

        _ownerMapId = owner.MapId;
        _ownerMapInstanceId = owner.MapInstanceId;

        _pathfinder = new Pathfinder(this);

        if (register && MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
        {
            instance.AddEntity(this);
            PacketSender.SendEntityDataToProximity(this);
        }
    }

    public override EntityType GetEntityType() => EntityType.GlobalEntity;

    public override void Update(long timeMs)
    {
        base.Update(timeMs);

        var owner = Owner;
        if (owner == null || owner.IsDisposed)
        {
            return;
        }

        SynchronizeWithOwner(owner);

        lock (EntityLock)
        {
            UpdateTarget(owner);
            UpdateState(owner);
        }

        switch (State)
        {
            case PetState.Attack:
                HandleAttackState(timeMs);
                break;

            case PetState.Follow:
                HandleFollowState(owner, timeMs);
                break;

            default:
                _pathfinder.SetTarget(null);
                break;
        }
    }

    public override void ProcessRegen()
    {
        foreach (Vital vital in Enum.GetValues(typeof(Vital)))
        {
            if (!Enum.IsDefined(vital))
            {
                continue;
            }

            var index = (int)vital;
            var current = GetVital(vital);
            var maximum = GetMaxVital(vital);
            if (current >= maximum)
            {
                continue;
            }

            var regenRate = Descriptor.VitalRegen[index] / 100f;
            if (regenRate == 0)
            {
                continue;
            }

            var regenAmount = (long)Math.Max(1, maximum * Math.Abs(regenRate));
            if (regenRate > 0)
            {
                AddVital(vital, regenAmount);
            }
            else
            {
                SubVital(vital, regenAmount);
            }
        }
    }

    public override void TryAttack(Entity target)
    {
        if (target.IsDisposed || !CanAttack(target, null))
        {
            return;
        }

        if (!IsOneBlockAway(target))
        {
            return;
        }

        if (!IsFacingTarget(target))
        {
            ChangeDir(DirectionToTarget(target));
        }

        if (Descriptor.AttackAnimationId != Guid.Empty)
        {
            PacketSender.SendAnimationToProximity(
                Descriptor.AttackAnimationId,
                -1,
                Guid.Empty,
                target.MapId,
                (byte)target.X,
                (byte)target.Y,
                Dir,
                target.MapInstanceId
            );
        }

        base.TryAttack(
            target,
            Descriptor.Damage,
            (DamageType)Descriptor.DamageType,
            (Stat)Descriptor.ScalingStat,
            Descriptor.Scaling,
            Descriptor.CritChance,
            Descriptor.CritMultiplier
        );

        PacketSender.SendEntityAttack(this, CalculateAttackTime());
    }

    public void NotifyOwnerDamaged()
    {
        var owner = Owner;
        if (owner == null || owner.IsDisposed)
        {
            return;
        }

        lock (EntityLock)
        {
            UpdateTarget(owner);
            UpdateState(owner);
        }
    }

    public void Despawn(bool killIfDespawnable = true)
    {
        lock (EntityLock)
        {
            if (IsDisposed)
            {
                return;
            }

            if (killIfDespawnable && Despawnable && !IsDead)
            {
                Die(false, Owner);
            }

            var mapId = MapId;
            var mapInstanceId = MapInstanceId;

            if (mapId != Guid.Empty && mapInstanceId != Guid.Empty)
            {
                PacketSender.SendEntityLeaveInstanceOfMap(this, mapId, mapInstanceId);

                if (MapController.TryGetInstanceFromMap(mapId, mapInstanceId, out var instance))
                {
                    instance.RemoveEntity(this);
                }
            }

            Owner = null;
            Dispose();
        }
    }

    private void UpdateTarget(Player owner)
    {
        Target = owner.Target != null && !owner.Target.IsDisposed ? owner.Target : null;
    }

    private void UpdateState(Player owner)
    {
        if (Target != null && Target.MapInstanceId == MapInstanceId)
        {
            State = PetState.Attack;
            return;
        }

        if (GetDistanceTo(owner) > FollowDistance)
        {
            State = PetState.Follow;
            return;
        }

        State = PetState.Idle;
        Target = null;
    }

    private void HandleAttackState(long timeMs)
    {
        var target = Target;
        if (target == null || target.IsDisposed)
        {
            State = PetState.Idle;
            _pathfinder.SetTarget(null);
            return;
        }

        if (target.MapInstanceId != MapInstanceId)
        {
            State = PetState.Follow;
            _pathfinder.SetTarget(null);
            return;
        }

        if (!IsOneBlockAway(target))
        {
            UpdatePathfinder(target.MapId, target.X, target.Y, target.Z, timeMs);
            return;
        }

        _pathfinder.SetTarget(null);

        if (!IsFacingTarget(target))
        {
            ChangeDir(DirectionToTarget(target));
        }

        if (AttackTimer <= Timing.Global.Milliseconds)
        {
            TryAttack(target);
        }
    }

    private void HandleFollowState(Player owner, long timeMs)
    {
        if (GetDistanceTo(owner) <= 1)
        {
            State = PetState.Idle;
            _pathfinder.SetTarget(null);
            return;
        }

        UpdatePathfinder(owner.MapId, owner.X, owner.Y, owner.Z, timeMs);
    }

    private void UpdatePathfinder(Guid mapId, int targetX, int targetY, int targetZ, long timeMs)
    {
        if (timeMs < _nextPathUpdate)
        {
            return;
        }

        var currentTarget = _pathfinder.GetTarget();
        if (currentTarget == null ||
            currentTarget.TargetMapId != mapId ||
            currentTarget.TargetX != targetX ||
            currentTarget.TargetY != targetY ||
            currentTarget.TargetZ != targetZ)
        {
            _pathfinder.SetTarget(new PathfinderTarget(mapId, targetX, targetY, targetZ));
        }

        var result = _pathfinder.Update(timeMs);
        switch (result.Type)
        {
            case PathfinderResultType.Success:
                var direction = _pathfinder.GetMove();
                if (direction > Direction.None && CanMove(direction, out var blockerType, out var blockingEntity))
                {
                    Move(direction, null);
                }
                else if (blockerType == MovementBlockerType.Entity && blockingEntity != null && blockingEntity != Owner)
                {
                    if (!blockingEntity.IsDisposed && CanAttack(blockingEntity, null))
                    {
                        ChangeDir(direction);
                        TryAttack(blockingEntity);
                    }
                }
                break;

            case PathfinderResultType.OutOfRange:
            case PathfinderResultType.NoPathToTarget:
            case PathfinderResultType.Failure:
                _pathfinder.SetTarget(null);
                break;
        }

        _nextPathUpdate = timeMs + PathUpdateInterval;
    }

    private bool CanMove(Direction direction, out MovementBlockerType blockerType, [NotNullWhen(true)] out Entity? blockingEntity)
    {
        blockingEntity = null;
        var canMove = CanMoveInDirection(direction, out blockerType, out _, out var entity);
        if (!canMove)
        {
            blockingEntity = entity;
        }

        return canMove;
    }

    private void SynchronizeWithOwner(Player owner)
    {
        if (owner.MapId == _ownerMapId && owner.MapInstanceId == _ownerMapInstanceId)
        {
            return;
        }

        var previousMapId = MapId;
        var previousInstanceId = MapInstanceId;

        if (previousMapId != Guid.Empty && previousInstanceId != Guid.Empty)
        {
            PacketSender.SendEntityLeaveInstanceOfMap(this, previousMapId, previousInstanceId);

            if (MapController.TryGetInstanceFromMap(previousMapId, previousInstanceId, out var oldInstance))
            {
                oldInstance.RemoveEntity(this);
            }
        }

        lock (EntityLock)
        {
            MapId = owner.MapId;
            MapInstanceId = owner.MapInstanceId;
            X = owner.X;
            Y = owner.Y;
            Z = owner.Z;
            Dir = owner.Dir;

            _ownerMapId = owner.MapId;
            _ownerMapInstanceId = owner.MapInstanceId;
        }

        if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var newInstance))
        {
            newInstance.AddEntity(this);
        }

        PacketSender.SendEntityDataToProximity(this);
        _pathfinder.SetTarget(null);
    }
}
