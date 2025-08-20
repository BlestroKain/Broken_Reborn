using System;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;
using Intersect.Framework.Core.Utilities;
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
    public static SpellLevelingService.EffectiveSpellStats Resolve(Entity caster, SpellDescriptor baseSpell)
    {
        if (baseSpell == null)
        {
            throw new ArgumentNullException(nameof(baseSpell));
        }

        var level = 1;
        var row = new SpellProgressionRow();

        if (caster is Player player)
        {
            level = player.Spellbook.GetLevelOrDefault(baseSpell.Id);
            row = baseSpell.GetProgressionLevel(level) ?? new SpellProgressionRow();
        }

        return SpellMath.GetEffective(baseSpell, level, row);
    }
}
