using System;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;
using Intersect.GameObjects;
using Intersect.Server.Entities;

namespace Intersect.Server.Entities.Combat;

/// <summary>
/// Helper methods for resolving a spell cast at a given level.
/// </summary>
public static class SpellCastResolver
{
    /// <summary>
    /// Builds an adjusted spell based on the caster's level for the provided spell descriptor.
    /// </summary>
    public static SpellLevelingService.AdjustedSpell Resolve(Entity caster, SpellDescriptor baseSpell)
    {
        if (baseSpell == null)
        {
            throw new ArgumentNullException(nameof(baseSpell));
        }

        var level = 1;
        var row = new SpellProperties();

        if (caster is Player player)
        {
            level = player.Spellbook.GetLevel(baseSpell.Id);

            if (SpellProgressionStore.BySpellId.TryGetValue(baseSpell.Id, out var progression))
            {
                row = progression.GetLevel(level) ?? new SpellProperties();
            }
        }

        return SpellLevelingService.BuildAdjusted(baseSpell, row);
    }
}
