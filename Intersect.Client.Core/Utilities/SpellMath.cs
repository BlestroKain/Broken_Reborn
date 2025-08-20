using System;
using Intersect.Client.Entities;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;

namespace Intersect.Client.Utilities;

public static class SpellMath
{
    public static SpellLevelingService.EffectiveSpellStats GetEffective(
        Player player,
        Guid spellId,
        PlayerSpellbookState state)
    {
        if (!SpellDescriptor.TryGet(spellId, out var descriptor))
        {
            throw new ArgumentException("Unknown spell", nameof(spellId));
        }

        var level = state.GetLevelOrDefault(spellId);
        var row = descriptor.GetProgressionLevel(level) ?? new SpellProgressionRow();

        return SpellLevelingService.BuildAdjusted(descriptor, row);
    }
}

