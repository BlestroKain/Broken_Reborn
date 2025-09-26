using System;
using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;

namespace Intersect.Server.AI.Pets
{
    /// <summary>
    /// Adaptador de la entidad Pet (server) al contrato IPetRuntime para el cerebro.
    /// </summary>
    public sealed class PetRuntimeAdapter : IPetRuntime
    {
        private readonly Pet _pet;

        public PetRuntimeAdapter(Pet pet)
        {
            _pet = pet ?? throw new ArgumentNullException(nameof(pet));
        }

        public Guid Id => _pet.Id;
        public bool IsDisposed => _pet.IsDisposed;
        public bool IsDead => _pet.IsDead;

        public int X => _pet.X;
        public int Y => _pet.Y;
        public Direction Dir => _pet.Dir;
        public Guid MapId => _pet.MapId;
        public Guid MapInstanceId => _pet.MapInstanceId;

        public Player? Owner => _pet.Owner;
        public PetState Behavior => _pet.Behavior;

        public long[] MaxVital => _pet.GetMaxVitals();
        public long[] Vital => _pet.GetVitals();

        public PetDescriptor? Descriptor => _pet.Descriptor;

        public IEnumerable<SpellDescriptor> GetUsableSpells()
        {
            foreach (var slot in _pet.Spells)
            {
                if (slot == null || slot.IsEmpty)
                {
                    continue;
                }

                if (SpellDescriptor.TryGet(slot.SpellId, out var descriptor) && descriptor != null)
                {
                    yield return descriptor;
                }
            }
        }

        public bool HasManaFor(SpellDescriptor spell)
        {
            if (spell == null)
            {
                return false;
            }

            if (!TryGetSpellSlot(spell.Id, out var slot))
            {
                return false;
            }

            var manaCost = spell.GetEffectiveVitalCost(Vital.Mana, slot.Properties);
            return _pet.GetVital(Vital.Mana) >= manaCost;
        }

        public bool CanCast(SpellDescriptor spell, out string? reason)
        {
            reason = null;

            if (spell == null)
            {
                reason = "null spell";
                return false;
            }

            if (IsDead || IsDisposed)
            {
                reason = "pet invalid";
                return false;
            }

            if (!TryGetSpellSlot(spell.Id, out var slot))
            {
                reason = "missing spell";
                return false;
            }

            var manaCost = spell.GetEffectiveVitalCost(Vital.Mana, slot.Properties);
            if (_pet.GetVital(Vital.Mana) < manaCost)
            {
                reason = "low mana";
                return false;
            }

            if (_pet.HasStatusEffect(SpellEffect.Stun) ||
                _pet.HasStatusEffect(SpellEffect.Sleep) ||
                _pet.HasStatusEffect(SpellEffect.Silence))
            {
                reason = "status";
                return false;
            }

            return true;
        }

        public bool TryCastSpell(SpellDescriptor spell, Entity? targetEntity, int? tx = null, int? ty = null)
        {
            if (spell == null)
            {
                return false;
            }

            if (!TryGetSpellSlot(spell.Id, out var slot))
            {
                return false;
            }

            return _pet.TryCastSpell(spell, slot, targetEntity, tx, ty);
        }

        public bool IsInLineOfSight(Entity target)
        {
            if (target == null)
            {
                return false;
            }

            return target.MapId == MapId && target.MapInstanceId == MapInstanceId;
        }

        public bool IsInRange(Entity target, int tilesRange)
        {
            if (target == null)
            {
                return false;
            }

            return _pet.GetDistanceTo(target) <= tilesRange;
        }

        public bool TryMoveToward(int tx, int ty) => _pet.TryMoveToward(tx, ty);

        public bool TryStepAwayFrom(Entity from, int tiles = 1) => _pet.TryStepAwayFrom(from, tiles);

        public bool TryFace(Entity target) => _pet.TryFace(target);

        private bool TryGetSpellSlot(Guid spellId, out PlayerSpell slot)
        {
            foreach (var candidate in _pet.Spells)
            {
                if (candidate == null || candidate.IsEmpty)
                {
                    continue;
                }

                if (candidate.SpellId == spellId)
                {
                    slot = candidate;
                    return true;
                }
            }

            slot = null!;
            return false;
        }
    }
}
