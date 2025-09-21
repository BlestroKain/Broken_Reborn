using System;
using System.Diagnostics.CodeAnalysis;
using Intersect;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Framework.Reflection;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Pathfinding;
using Intersect.Server.Framework.Items;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
namespace Intersect.Server.Entities;

public sealed class Pet : Entity
{
    private const int FollowDistance = 3;
    private const long PathUpdateInterval = 100;
    private const long TargetLostGracePeriod = 2000;
    private int _followPathFailureCount;
    private long _lastFollowFailureTime;

    private readonly Pathfinder _pathfinder;

    private bool _canAssistOwner;

    private bool _canDefendOwner;

    private bool _canEngageTarget;

    private bool _canFollowOwner;

    private long _combatTimeout;
    private long _lastTargetSeenTime;
    private long _nextPathUpdate;

    private bool _metadataDirty;

    private Guid _ownerMapId;
    private Guid _ownerMapInstanceId;

    private readonly double[] _vitalAccumulators;

    private Player? _ownerCache;

    public PetDescriptor Descriptor { get; }

    public Guid OwnerId { get; }

    public bool Despawnable { get; } = true;

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

    private PetState _state = PetState.Idle;

    private PetState _behavior = PetState.Passive;

    public PetState Behavior
    {
        get => _behavior;
        private set
        {
            if (_behavior == value)
            {
                return;
            }

            var previousState = State;
            _behavior = value;

            ApplyBehaviorSettings(value);
            MarkMetadataDirty();

            if (State == previousState)
            {
                BroadcastState();
            }
        }
    }

    public void SetBehavior(PetState behavior)
    {
        if (!IsSelectableBehavior(behavior))
        {
            return;
        }

        Behavior = behavior;
    }

    public PetState State
    {
        get => _state;
        private set
        {
            if (_state == value)
            {
                return;
            }

            _state = value;
            MarkMetadataDirty();
            BroadcastState();
        }
    }

    public Pet(
        PetDescriptor descriptor,
        Player owner,
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

        _vitalAccumulators = new double[Enum.GetValues<Vital>().Length];

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

        Behavior = PetState.Follow;

        if (register && MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
        {
            instance.AddEntity(this);
            PacketSender.SendEntityDataToProximity(this);
        }
    }

    public override EntityType GetEntityType() => EntityType.Pet;

    public override EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
    {
        packet ??= new PetEntityPacket();

        packet = base.EntityPacket(packet, forPlayer);

        if (packet is not PetEntityPacket petPacket)
        {
            throw new InvalidOperationException(
                $"Invalid packet type '{packet.GetType().GetName(qualified: true)}', expected '{typeof(PetEntityPacket).GetName(qualified: true)}'"
            );
        }

        petPacket.OwnerId = OwnerId;
        petPacket.DescriptorId = Descriptor.Id;
        petPacket.Behavior = Behavior;
        petPacket.Despawnable = Despawnable;

        ResetMetadataDirty();

        return petPacket;
    }

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
            UpdateTarget(owner, timeMs);
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
        foreach (var vital in Enum.GetValues<Vital>())
        {
            if (!Enum.IsDefined(vital))
            {
                continue;
            }

            var index = (int)vital;
            var maximum = GetMaxVital(vital);
            var regenRate = Descriptor.VitalRegen[index] / 100.0;
            if (regenRate == 0)
            {
                continue;
            }

            if (regenRate > 0 && GetVital(vital) >= maximum)
            {
                continue;
            }

            _vitalAccumulators[index] += maximum * regenRate;

            var regenAmount = (long)Math.Floor(_vitalAccumulators[index]);
            if (regenAmount == 0)
            {
                continue;
            }

            _vitalAccumulators[index] -= regenAmount;

            if (regenAmount > 0)
            {
                AddVital(vital, regenAmount);
            }
            else
            {
                SubVital(vital, -regenAmount);
            }
        }
    }

    public override void Reset()
    {
        base.Reset();

        Array.Clear(_vitalAccumulators, 0, _vitalAccumulators.Length);
    }

    public override bool CanAttack(Entity entity, SpellDescriptor spell)
    {
        if (!IsValidCombatTarget(entity))
        {
            return false;
        }

        return base.CanAttack(entity, spell);
    }

    public override bool IsAllyOf(Entity otherEntity)
    {
        var owner = Owner;
        if (owner == null || owner.IsDisposed)
        {
            return base.IsAllyOf(otherEntity);
        }

        if (ReferenceEquals(otherEntity, this) || ReferenceEquals(otherEntity, owner))
        {
            return true;
        }

        if (otherEntity is Pet otherPet && otherPet.OwnerId == OwnerId)
        {
            return true;
        }

        if (owner.IsAllyOf(otherEntity))
        {
            return true;
        }

        return base.IsAllyOf(otherEntity);
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

    public override int CalculateAttackTime()
    {
        if (Descriptor.AttackSpeedModifier == 1)
        {
            return Descriptor.AttackSpeedValue;
        }

        return base.CalculateAttackTime();
    }

    public void NotifyOwnerDamaged(Entity? attacker)
    {
        var owner = Owner;
        if (owner == null || owner.IsDisposed)
        {
            return;
        }

        if (!_canDefendOwner || !_canEngageTarget)
        {
            return;
        }

        if (IsRestrained())
        {
            return;
        }

        var aggressor = attacker;
        if (aggressor == null || !IsValidAggressor(aggressor, owner))
        {
            return;
        }

        var timeMs = Timing.Global.Milliseconds;

        lock (EntityLock)
        {
            if (!IsValidAggressor(aggressor, owner))
            {
                return;
            }

            TryAssignTarget(aggressor, timeMs);
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

            // 1) Decidir si la mascota debe morir o solo desaparecer
            //    - Si killIfDespawnable=true y Despawnable=true y no está muerta => Die()
            //    - Si killIfDespawnable=false => no forzamos muerte
            if (killIfDespawnable && Despawnable && !IsDead)
            {
                // No dropeamos nada, ni contamos muerte del owner: es un despawn “limpio”
                Die(false, killer: Owner);
            }

            // 2) Limpieza de estado interno
            //    Evitar que quede peleando, con target, timers, etc.
            ClearCombatTarget();
            _pathfinder?.SetTarget(null); // si usas pathfinder
            State = PetState.Idle;        // forzar estado neutro antes de salir

            var mapId = MapId;
            var mapInstanceId = MapInstanceId;

            // 3) Notificar a clientes y sacar de la instancia (si todavía estaba en mapa)
            if (mapId != Guid.Empty && mapInstanceId != Guid.Empty)
            {
                // primero broadcast del leave
                PacketSender.SendEntityLeaveInstanceOfMap(this, mapId, mapInstanceId);

                // luego quitar de la instancia
                if (MapController.TryGetInstanceFromMap(mapId, mapInstanceId, out var instance))
                {
                    instance.RemoveEntity(this);

                    // Si llevas un diccionario específico de pets en MapInstance, limpialo aquí también:
                    if (instance.PetInstances != null)
                    {
                        _ = instance.PetInstances.TryRemove(Id, out _);
                    }
                }

                // Opcional: resetear para que no haya “residuos” de posición
                MapId = Guid.Empty;
                MapInstanceId = Guid.Empty;
            }

            // 4) Desasociar del dueño (si cacheas la referencia)
            var owner = Owner;
            Owner = null;

            // Si el dueño mantiene una lista/flag de pet activa, limpia aquí (pseudocódigo):
            // owner?.RemoveActivePet(this);

            // 5) Notificación opcional extra a HUD si tienes una ruta específica (si no, el leave basta)
            // PacketSender.SendPetStateUpdate(this); // solo si tu cliente lo espera

            // 6) Dispose final
            Dispose();
        }
    }


    private void UpdateTarget(Player owner, long timeMs)
    {
        if (!_canEngageTarget)
        {
            if (Target != null)
            {
                ClearCombatTarget();
            }

            return;
        }

        if (!IsValidCombatTarget(Target))
        {
            ClearCombatTarget();
        }

        if (Target != null)
        {
            return;
        }

        if (!_canAssistOwner)
        {
            return;
        }

        var ownerTarget = owner.Target;
        if (ownerTarget == null || ownerTarget.IsDisposed)
        {
            return;
        }

        if (!IsValidCombatTarget(ownerTarget))
        {
            return;
        }

        if (ownerTarget.MapInstanceId != MapInstanceId)
        {
            return;
        }

        TryAssignTarget(ownerTarget, timeMs);
    }

    private void UpdateState(Player owner)
    {
        if (_canEngageTarget && Target != null)
        {
            State = PetState.Attack;
            return;
        }

        if (_canFollowOwner && GetDistanceTo(owner) > FollowDistance)
        {
            State = PetState.Follow;
            return;
        }

        State = PetState.Idle;
    }

    private bool IsRestrained()
    {
        return HasStatusEffect(SpellEffect.Stun) || HasStatusEffect(SpellEffect.Sleep);
    }

    private bool IsValidAggressor(Entity? attacker, Player owner)
    {
        if (attacker == null || attacker.IsDisposed || attacker.IsDead)
        {
            return false;
        }

        if (ReferenceEquals(attacker, owner) || ReferenceEquals(attacker, this))
        {
            return false;
        }

        if (attacker is Pet otherPet && otherPet.OwnerId == OwnerId)
        {
            return false;
        }

        if (owner.IsAllyOf(attacker) || IsAllyOf(attacker))
        {
            return false;
        }

        return true;
    }

    private bool IsValidCombatTarget(Entity? entity)
    {
        if (entity == null || entity.IsDisposed || entity.IsDead)
        {
            return false;
        }

        if (ReferenceEquals(entity, this))
        {
            return false;
        }

        if (entity is Resource)
        {
            return false;
        }

        if (entity.MapInstanceId != MapInstanceId)
        {
            return false;
        }

        var owner = Owner;
        if (owner != null)
        {
            if (ReferenceEquals(entity, owner) || owner.IsAllyOf(entity))
            {
                return false;
            }
        }

        if (IsAllyOf(entity))
        {
            return false;
        }

        return true;
    }

    private bool TryAssignTarget(Entity target, long timeMs)
    {
        if (!_canEngageTarget)
        {
            return false;
        }

        if (!IsValidCombatTarget(target))
        {
            return false;
        }

        if (ReferenceEquals(Target, target))
        {
            RefreshCombatTimeout(timeMs);
            return true;
        }

        Target = target;
        State = PetState.Attack;
        _lastTargetSeenTime = timeMs;
        RefreshCombatTimeout(timeMs);

        return true;
    }

    private void ClearCombatTarget()
    {
        Target = null;
        _combatTimeout = 0;
        _lastTargetSeenTime = 0;
        _pathfinder.SetTarget(null);
    }

    private void RefreshCombatTimeout(long timeMs)
    {
        var duration = (long)Options.Instance.Combat.CombatTime;
        if (duration <= 0)
        {
            _combatTimeout = long.MaxValue;
            return;
        }

        var newTimeout = timeMs + duration;
        _combatTimeout = newTimeout < timeMs ? long.MaxValue : newTimeout;
    }

    private void HandleAttackState(long timeMs)
    {
        var target = Target;
        if (!IsValidCombatTarget(target))
        {
            ClearCombatTarget();
            State = _canFollowOwner ? PetState.Follow : PetState.Idle;
            return;
        }

        if (target.MapInstanceId != MapInstanceId)
        {
            ClearCombatTarget();
            State = _canFollowOwner ? PetState.Follow : PetState.Idle;
            return;
        }

        if (timeMs >= _combatTimeout)
        {
            ClearCombatTarget();
            State = _canFollowOwner ? PetState.Follow : PetState.Idle;
            return;
        }

        if (!IsOneBlockAway(target))
        {
            var hasPath = UpdatePathfinder(target.MapId, target.X, target.Y, target.Z, timeMs, out var madeProgress);
            if (!hasPath)
            {
                if (timeMs - _lastTargetSeenTime >= TargetLostGracePeriod)
                {
                    ClearCombatTarget();
                    State = _canFollowOwner ? PetState.Follow : PetState.Idle;
                }

                return;
            }

            if (madeProgress)
            {
                _lastTargetSeenTime = timeMs;
                RefreshCombatTimeout(timeMs);
            }
            else if (timeMs - _lastTargetSeenTime >= TargetLostGracePeriod)
            {
                ClearCombatTarget();
                State = _canFollowOwner ? PetState.Follow : PetState.Idle;
            }

            return;
        }

        _pathfinder.SetTarget(null);

        _lastTargetSeenTime = timeMs;
        RefreshCombatTimeout(timeMs);

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
        var petOptions = Options.Instance.Pets;

        if (!_canFollowOwner)
        {
            State = PetState.Idle;
            _pathfinder.SetTarget(null);
            ResetFollowFailureCounters();
            return;
        }

        ResetFollowFailureCountersIfExpired(petOptions, timeMs);

        var distanceToOwner = GetDistanceTo(owner);
        if (distanceToOwner <= 1)
        {
            State = PetState.Idle;
            _pathfinder.SetTarget(null);
            ResetFollowFailureCounters();
            return;
        }

        if (petOptions.FollowLeashDistance > 0 && distanceToOwner >= petOptions.FollowLeashDistance)
        {
            SnapToOwner(owner);
            return;
        }

        var hasPath = UpdatePathfinder(owner.MapId, owner.X, owner.Y, owner.Z, timeMs, out var madeProgress);
        if (!hasPath)
        {
            RegisterFollowFailure(timeMs);

            if (petOptions.FollowFailureTeleportThreshold > 0 &&
                _followPathFailureCount >= petOptions.FollowFailureTeleportThreshold)
            {
                SnapToOwner(owner);
            }

            return;
        }

        if (madeProgress)
        {
            ResetFollowFailureCounters();
        }
    }

    private void RegisterFollowFailure(long timeMs)
    {
        _followPathFailureCount++;
        _lastFollowFailureTime = timeMs;
    }

    private void ResetFollowFailureCounters()
    {
        _followPathFailureCount = 0;
        _lastFollowFailureTime = 0;
    }

    private void ResetFollowFailureCountersIfExpired(PetOptions petOptions, long timeMs)
    {
        if (_followPathFailureCount == 0)
        {
            return;
        }

        if (petOptions.FollowFailureResetMilliseconds <= 0)
        {
            return;
        }

        if (timeMs - _lastFollowFailureTime >= petOptions.FollowFailureResetMilliseconds)
        {
            ResetFollowFailureCounters();
        }
    }

    private void SnapToOwner(Player owner)
    {
        lock (EntityLock)
        {
            MapId = owner.MapId;
            MapInstanceId = owner.MapInstanceId;
            _ownerMapId = owner.MapId;
            _ownerMapInstanceId = owner.MapInstanceId;
            X = owner.X;
            Y = owner.Y;
            Z = owner.Z;
            Dir = owner.Dir;
        }

        _pathfinder.SetTarget(null);
        PacketSender.SendEntityDataToProximity(this);
        ResetFollowFailureCounters();
    }

    private static bool IsSelectableBehavior(PetState behavior) =>
        behavior is PetState.Follow or PetState.Stay or PetState.Defend or PetState.Passive;

    private void ApplyBehaviorSettings(PetState behavior)
    {
        lock (EntityLock)
        {
            _canFollowOwner = behavior is PetState.Follow or PetState.Defend;
            _canAssistOwner = behavior == PetState.Follow;
            _canDefendOwner = behavior is PetState.Follow or PetState.Stay or PetState.Defend;
            _canEngageTarget = behavior != PetState.Passive;

            if (!_canEngageTarget)
            {
                ClearCombatTarget();
                State = PetState.Idle;
            }
            else if (!_canFollowOwner && State == PetState.Follow)
            {
                State = PetState.Idle;
            }
        }
    }

    private bool UpdatePathfinder(Guid mapId, int targetX, int targetY, int targetZ, long timeMs, out bool madeProgress)
    {
        madeProgress = false;

        if (timeMs < _nextPathUpdate)
        {
            return true;
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
        MovementBlockerType blockerType = MovementBlockerType.NotBlocked; // Initialize blockerType
        Entity? blockingEntity = null; // Initialize blockingEntity

        switch (result.Type)
        {
            case PathfinderResultType.Success:
                var direction = _pathfinder.GetMove();
                if (direction > Direction.None && CanMove(direction, out blockerType, out blockingEntity))
                {
                    Move(direction, null);
                    madeProgress = true;
                }
                else if (blockerType == MovementBlockerType.Entity && blockingEntity != null && blockingEntity != Owner)
                {
                    if (!blockingEntity.IsDisposed && CanAttack(blockingEntity, null))
                    {
                        var face = DirectionToTarget(blockingEntity);

                        if (!IsFacingTarget(blockingEntity))
                        {
                            ChangeDir(face);
                        }

                        TryAttack(blockingEntity);
                        madeProgress = true;
                    }
                }
                break;

            case PathfinderResultType.OutOfRange:
            case PathfinderResultType.NoPathToTarget:
            case PathfinderResultType.Failure:
                _pathfinder.SetTarget(null);
                _nextPathUpdate = timeMs + PathUpdateInterval;
                return false;
        }

        _nextPathUpdate = timeMs + PathUpdateInterval;
        return true;
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

    internal void SynchronizeWithOwner(Player owner)
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
        ClearCombatTarget();
        State = _canFollowOwner ? PetState.Follow : PetState.Idle;
    }

    private void MarkMetadataDirty()
    {
        _metadataDirty = true;
    }

    private void BroadcastState()
    {
        PacketSender.SendPetStateUpdate(this);
    }

    public override void KilledEntity(Entity entity)
    {
        base.KilledEntity(entity);

        var owner = Owner ?? Player.FindOnline(OwnerId);
        owner?.KilledEntity(entity);
    }

    internal bool MetadataDirty => _metadataDirty;

    internal void ResetMetadataDirty()
    {
        _metadataDirty = false;
    }

    protected override EntityItemSource? AsItemSource()
    {
        // Since the Pet class does not seem to represent an item source, we return null.
        // If additional logic is required to represent the Pet as an item source, it can be implemented here.
        return null;
    }
}
