using System;
using Intersect.Client.Entities;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;

namespace Intersect.Client.Game.Combat;

public static class SpellMath
{
    public static SpellLevelingService.EffectiveSpellStats GetEffective(
        Player player,
        Guid spellId,
        SpellProgressionStore store,
        PlayerSpellbookState state)
    {
        if (!SpellDescriptor.TryGet(spellId, out var descriptor))
        {
            throw new ArgumentException("Unknown spell", nameof(spellId));
        }

        var level = state.GetLevelOrDefault(spellId);
        store.BySpellId.TryGetValue(spellId, out var progression);
        var row = progression?.GetLevel(level) ?? new SpellProperties();

        return SpellLevelingService.BuildAdjusted(descriptor, row);
    }
}

