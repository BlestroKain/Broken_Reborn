using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Config;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Utilities;

namespace Intersect.Server.AI.Pets
{
    /// <summary>
    /// Preferencias de IA por defecto. Puedes exponer esto en editor más adelante.
    /// </summary>
    public sealed class PetAIConfig
    {
        // Rangos
        public int LeashTiles { get; set; } = 8;                 // distancia máxima desde el dueño
        public int ChaseRangeTiles { get; set; } = 10;           // persecución de enemigos
        public int DefendTriggerRangeTiles { get; set; } = 8;    // rango para reaccionar al agresor

        // Curación y soporte
        public int HealOwnerThresholdPercent { get; set; } = 50; // si dueño < 50% HP intenta curar
        public int HealSelfThresholdPercent { get; set; } = 40;  // si pet < 40% HP intenta curarse
        public int MinManaPercentToCast { get; set; } = 15;      // si mana < 15% evita castear

        // Cadencias
        public long ThinkIntervalMs { get; set; } = 200;         // período de evaluación
        public long RetargetIntervalMs { get; set; } = 600;      // cambio de objetivo
        public long RepathIntervalMs { get; set; } = 250;        // mover/seguir

        // Seguridad
        public bool NeverPullAggroAlone { get; set; } = true;    // si owner no está en combate, evita iniciar combates lejos
    }

    /// <summary>
    /// Blackboard: estado efímero que la IA consulta/actualiza.
    /// </summary>
    public sealed class PetAIBlackboard
    {
        public long LastThinkTime;
        public long LastRetargetTime;
        public long LastPathTime;

        public Entity? LastOwnerAttacker;
        public long LastOwnerDamagedAt;

        public Entity? CurrentTarget;
        public long CurrentTargetSince;
    }

    public interface IPetBrain
    {
        void Update(long timeMs);
        void OnOwnerDamaged(Entity? attacker);
        void OnBehaviorChanged(PetState newBehavior);
        void OnDied();
        void Reset();
    }

    /// <summary>
    /// Contrato mínimo que usamos del Pet (server) para no acoplar fuerte.
    /// </summary>
    public interface IPetRuntime
    {
        Guid Id { get; }
        bool IsDisposed { get; }
        bool IsDead { get; }
        int X { get; }
        int Y { get; }
        Direction Dir { get; }
        Guid MapId { get; }
        Guid MapInstanceId { get; }
        Player? Owner { get; }
        PetState Behavior { get; }

        long[] MaxVital { get; }
        long[] Vital { get; }

        bool HasManaFor(SpellDescriptor spell);
        bool IsInLineOfSight(Entity target);
        bool IsInRange(Entity target, int tilesRange);
        bool CanCast(SpellDescriptor spell, out string? reason);
        bool TryCastSpell(SpellDescriptor spell, Entity? targetEntity, int? tx = null, int? ty = null);

        bool TryMoveToward(int tx, int ty);
        bool TryStepAwayFrom(Entity from, int tiles = 1);
        bool TryFace(Entity target);

        // Accesos al descriptor de pet
        PetDescriptor? Descriptor { get; }
        IEnumerable<SpellDescriptor> GetUsableSpells(); // indexa las spells del pet que pueden castear (ofensivas/soporte)
    }

    /// <summary>
    /// Implementación por defecto de la IA de mascotas.
    /// </summary>
    public sealed class PetAIController : IPetBrain
    {
        private readonly IPetRuntime _pet;
        private readonly PetAIConfig _cfg;
        private readonly PetAIBlackboard _bb = new();

        // Cooldowns por spell
        private readonly Dictionary<Guid, long> _spellCdUntil = new();

        public PetAIController(IPetRuntime pet, PetAIConfig? cfg = null)
        {
            _pet = pet ?? throw new ArgumentNullException(nameof(pet));
            _cfg = cfg ?? new PetAIConfig();
        }

        public void Reset()
        {
            _bb.LastThinkTime = 0;
            _bb.LastRetargetTime = 0;
            _bb.LastPathTime = 0;
            _bb.LastOwnerAttacker = null;
            _bb.LastOwnerDamagedAt = 0;
            _bb.CurrentTarget = null;
            _bb.CurrentTargetSince = 0;
            _spellCdUntil.Clear();
        }

        public void OnDied()
        {
            Reset();
        }

        public void OnBehaviorChanged(PetState newBehavior)
        {
            // liberar objetivo si pasa a Passive/Stay
            if (newBehavior is PetState.Passive or PetState.Stay)
            {
                _bb.CurrentTarget = null;
            }
        }

        public void OnOwnerDamaged(Entity? attacker)
        {
            if (attacker == null || attacker.IsDisposed || attacker.IsDead)
            {
                return;
            }

            _bb.LastOwnerAttacker = attacker;
            _bb.LastOwnerDamagedAt = Timing.Global.Milliseconds;
        }

        public void Update(long timeMs)
        {
            if (_pet.IsDisposed || _pet.IsDead)
            {
                return;
            }

            // Throttle de pensamiento
            if (timeMs < _bb.LastThinkTime + _cfg.ThinkIntervalMs)
            {
                return;
            }

            _bb.LastThinkTime = timeMs;

            var owner = _pet.Owner;
            if (owner == null || owner.IsDisposed)
            {
                return;
            }

            // 0) Mantener leash (no irse muy lejos del owner)
            if (!WithinTiles(_pet.X, _pet.Y, owner.X, owner.Y, _cfg.LeashTiles))
            {
                // Si se alejó, prioriza volver
                _ = MoveToward(owner.X, owner.Y);
                return;
            }

            // 1) Curar (dueño o pet) si hace falta
            if (TryHealIfNeeded(owner))
            {
                return;
            }

            // 2) Selección/actualización de objetivo según behavior
            UpdateTarget(timeMs, owner);

            // 3) Intentar ofensiva sobre target actual
            if (TryOffense())
            {
                return;
            }

            // 4) Posicionamiento según behavior
            switch (_pet.Behavior)
            {
                case PetState.Follow:
                    MoveNearOwner(owner, preferredDistance: 1, maxDistance: 2);
                    break;

                case PetState.Defend:
                    // si no hay target, estructura similar a follow
                    MoveNearOwner(owner, preferredDistance: 1, maxDistance: 2);
                    break;

                case PetState.Stay:
                    // quieta; quizá orientar hacia el owner
                    _pet.TryFace(owner);
                    break;

                case PetState.Passive:
                    // idle suave: acercarse un poco si se aleja demasiado
                    if (!WithinTiles(_pet.X, _pet.Y, owner.X, owner.Y, 3))
                    {
                        MoveNearOwner(owner, preferredDistance: 2, maxDistance: 3);
                    }

                    break;
            }
        }

        // ---------- Decisiones de Curación ----------
        private bool TryHealIfNeeded(Player owner)
        {
            // Evitar gastar maná muy bajo
            if (Percent(_pet.Vital[(int)Vital.Mana], _pet.MaxVital[(int)Vital.Mana]) < _cfg.MinManaPercentToCast)
            {
                return false;
            }

            // ¿Se puede curar? (dueño primero)
            var ownerHpPct = Percent(owner.GetVital(Vital.Health), owner.GetMaxVital(Vital.Health));
            if (ownerHpPct < _cfg.HealOwnerThresholdPercent)
            {
                var heal = FindBestHealSpell(targetSelf: false);
                if (heal != null && TryCastWithCd(heal, owner))
                {
                    return true;
                }
            }

            // Curarse a sí misma si está mal
            var selfHpPct = Percent(_pet.Vital[(int)Vital.Health], _pet.MaxVital[(int)Vital.Health]);
            if (selfHpPct < _cfg.HealSelfThresholdPercent)
            {
                var selfHeal = FindBestHealSpell(targetSelf: true);
                if (selfHeal != null && TryCastWithCd(selfHeal, _pet as Entity))
                {
                    return true;
                }
            }

            return false;
        }

        private SpellDescriptor? FindBestHealSpell(bool targetSelf)
        {
            return _pet
                .GetUsableSpells()
                .Where(IsHealingSpell)
                .OrderByDescending(EstimatedHealPower)
                .FirstOrDefault();
        }

        private static bool IsHealingSpell(SpellDescriptor? spell)
        {
            if (spell?.Combat == null)
            {
                return false;
            }

            if (!spell.Combat.Friendly)
            {
                return false;
            }

            return spell.Combat.VitalDiff[(int)Vital.Health] > 0;
        }

        private static long EstimatedHealPower(SpellDescriptor spell)
        {
            if (spell?.Combat == null)
            {
                return 0;
            }

            return spell.Combat.VitalDiff[(int)Vital.Health];
        }

        // ---------- Selección de Objetivo ----------
        private void UpdateTarget(long timeMs, Player owner)
        {
            // Si behavior es Passive/Stay, no mantener target
            if (_pet.Behavior is PetState.Passive or PetState.Stay)
            {
                _bb.CurrentTarget = null;
                return;
            }

            // no retargetear tan seguido
            if (timeMs < _bb.LastRetargetTime + _cfg.RetargetIntervalMs)
            {
                return;
            }

            _bb.LastRetargetTime = timeMs;

            // 1) Si el owner fue dañado recientemente, ese agresor tiene prioridad
            var attacker = _bb.LastOwnerAttacker;
            var recent = _bb.LastOwnerDamagedAt > 0 && (timeMs - _bb.LastOwnerDamagedAt) < Options.Instance.Combat.CombatTime;
            if (recent && IsValidEnemy(attacker))
            {
                _bb.CurrentTarget = attacker;
                _bb.CurrentTargetSince = timeMs;
                return;
            }

            // 2) Si ya hay target pero murió o se fue muy lejos, límpialo
            if (!IsValidEnemy(_bb.CurrentTarget))
            {
                _bb.CurrentTarget = null;
            }

            // 3) Si no hay target, busca uno cercano (si behavior lo permite)
            if (_bb.CurrentTarget == null)
            {
                var ownerInCombat = owner.CombatTimer > Timing.Global.Milliseconds;

                // En Defend/Folllow, busca enemigos cerca del owner/pet
                var candidates = NearbyHostiles(maxTiles: _cfg.ChaseRangeTiles)
                    .OrderBy(e => TileDist(e.X, e.Y, owner.X, owner.Y))
                    .ToArray();

                if (_cfg.NeverPullAggroAlone && !ownerInCombat)
                {
                    candidates = candidates.Where(e => WithinTiles(e.X, e.Y, owner.X, owner.Y, 3)).ToArray();
                }

                _bb.CurrentTarget = candidates.FirstOrDefault();
                if (_bb.CurrentTarget != null)
                {
                    _bb.CurrentTargetSince = timeMs;
                }
            }
        }

        private bool IsValidEnemy(Entity? e)
            => e != null && !e.IsDisposed && !e.IsDead && _pet.Owner != null && !_pet.Owner.IsAllyOf(e);

        private IEnumerable<Entity> NearbyHostiles(int maxTiles)
        {
            if (!MapController.TryGetInstanceFromMap(_pet.MapId, _pet.MapInstanceId, out var instance))
            {
                yield break;
            }

            foreach (var entity in instance.GetEntities())
            {
                if (entity == null || entity.IsDisposed || entity.IsDead)
                {
                    continue;
                }

                if (_pet.Owner != null && _pet.Owner.IsAllyOf(entity))
                {
                    continue;
                }

                if (!WithinTiles(_pet.X, _pet.Y, entity.X, entity.Y, maxTiles))
                {
                    continue;
                }

                yield return entity;
            }
        }

        // ---------- Ofensiva ----------
        private bool TryOffense()
        {
            var target = _bb.CurrentTarget;
            if (!IsValidEnemy(target))
            {
                return false;
            }

            // ¿tenemos maná decente?
            if (Percent(_pet.Vital[(int)Vital.Mana], _pet.MaxVital[(int)Vital.Mana]) < _cfg.MinManaPercentToCast)
            {
                // acercarse/mejor posición si no puede castear, o autoataque si existe
                return MoveIntoAttackRange(target);
            }

            // elegir spell ofensivo
            var spell = FindBestAttackSpell(target);
            if (spell != null && TryCastWithCd(spell, target))
            {
                return true;
            }

            // si no se pudo castear (rango/LOS), intenta posicionarse
            return MoveIntoAttackRange(target);
        }

        private bool MoveIntoAttackRange(Entity target)
        {
            if (!WithinTiles(_pet.X, _pet.Y, target.X, target.Y, tiles: 1))
            {
                return MoveToward(target.X, target.Y);
            }

            _pet.TryFace(target);
            return false;
        }

        private SpellDescriptor? FindBestAttackSpell(Entity target)
        {
            var now = Timing.Global.Milliseconds;

            return _pet
                .GetUsableSpells()
                .Where(IsOffensiveSpell)
                .Where(s => !_spellCdUntil.TryGetValue(s.Id, out var until) || now >= until)
                .OrderByDescending(EstimatedDamage)
                .FirstOrDefault(s => InPracticalRange(s, target));
        }

        private static bool IsOffensiveSpell(SpellDescriptor? spell)
        {
            if (spell?.Combat == null)
            {
                return false;
            }

            if (spell.SpellType != SpellType.CombatSpell)
            {
                return false;
            }

            if (spell.Combat.Friendly)
            {
                return false;
            }

            return spell.Combat.VitalDiff[(int)Vital.Health] < 0 || spell.Combat.TargetType != SpellTargetType.Self;
        }

        private static long EstimatedDamage(SpellDescriptor spell)
        {
            if (spell?.Combat == null)
            {
                return 0;
            }

            var healthDiff = -spell.Combat.VitalDiff[(int)Vital.Health];
            if (healthDiff > 0)
            {
                return healthDiff;
            }

            return spell.Combat.GetEffectiveScaling(null);
        }

        private bool InPracticalRange(SpellDescriptor spell, Entity target)
        {
            if (spell?.Combat == null)
            {
                return false;
            }

            var range = spell.Combat.GetEffectiveCastRange(null);
            if (range <= 0)
            {
                range = 3;
            }

            if (!WithinTiles(_pet.X, _pet.Y, target.X, target.Y, range))
            {
                return false;
            }

            return _pet.IsInLineOfSight(target);
        }

        private bool TryCastWithCd(SpellDescriptor spell, Entity? target)
        {
            if (spell == null)
            {
                return false;
            }

            var now = Timing.Global.Milliseconds;
            if (_spellCdUntil.TryGetValue(spell.Id, out var until) && now < until)
            {
                return false;
            }

            if (!_pet.CanCast(spell, out _))
            {
                return false;
            }

            var ok = _pet.TryCastSpell(spell, target);
            if (ok)
            {
                var cdMs = Math.Max(0, spell.CooldownDuration);
                _spellCdUntil[spell.Id] = now + cdMs;
            }

            return ok;
        }

        // ---------- Movimiento utilitario ----------
        private bool MoveNearOwner(Player owner, int preferredDistance, int maxDistance)
        {
            if (WithinTiles(_pet.X, _pet.Y, owner.X, owner.Y, preferredDistance))
            {
                return false;
            }

            if (!WithinTiles(_pet.X, _pet.Y, owner.X, owner.Y, maxDistance))
            {
                return MoveToward(owner.X, owner.Y);
            }

            return MoveToward(owner.X, owner.Y);
        }

        private bool MoveToward(int tx, int ty)
        {
            var now = Timing.Global.Milliseconds;
            if (now < _bb.LastPathTime + _cfg.RepathIntervalMs)
            {
                return false;
            }

            _bb.LastPathTime = now;

            return _pet.TryMoveToward(tx, ty);
        }

        // ---------- Helpers geométricos ----------
        private static bool WithinTiles(int x1, int y1, int x2, int y2, int tiles)
            => TileDist(x1, y1, x2, y2) <= tiles;

        private static int TileDist(int x1, int y1, int x2, int y2)
            => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        private static int Percent(long cur, long max)
            => max <= 0 ? 0 : (int)((cur * 100L) / max);
    }
}
