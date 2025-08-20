using System;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;
using Intersect.GameObjects;

namespace Intersect.Framework.Core.Utilities;

public static class SpellMath
{
    public static SpellLevelingService.EffectiveSpellStats GetEffective(
        SpellDescriptor spell,
        int level,
        SpellProgressionRow? row = null)
    {
        if (spell == null)
        {
            throw new ArgumentNullException(nameof(spell));
        }

        row ??= spell.GetProgressionLevel(level) ?? new SpellProgressionRow();

        var castTime = Math.Max(0, spell.CastDuration + row.CastTimeDeltaMs);
        var cooldown = Math.Max(0, spell.CooldownDuration + row.CooldownDeltaMs);

        var baseCosts = spell.VitalCost ?? Array.Empty<long>();
        var rowCosts = row.VitalCostDeltas ?? Array.Empty<long>();
        var length = Math.Max(baseCosts.Length, rowCosts.Length);
        var vitalCosts = new long[length];
        for (var i = 0; i < length; ++i)
        {
            var baseValue = i < baseCosts.Length ? baseCosts[i] : 0;
            var delta = i < rowCosts.Length ? rowCosts[i] : 0;
            var value = baseValue + delta;
            vitalCosts[i] = value < 0 ? 0 : value;
        }

        var aoeRadius = spell.Combat?.HitRadius ?? 0;
        aoeRadius = Math.Max(0, aoeRadius + row.AoERadiusDelta);

        return new SpellLevelingService.EffectiveSpellStats
        {
            CastTimeMs = castTime,
            CooldownTimeMs = cooldown,
            VitalCosts = vitalCosts,
            PowerBonusFlat = row.PowerBonusFlat,
            PowerScalingBonus = row.PowerScalingBonus,
            BuffStrengthFactor = row.BuffStrengthFactor,
            BuffDurationFactor = row.BuffDurationFactor,
            DebuffStrengthFactor = row.DebuffStrengthFactor,
            DebuffDurationFactor = row.DebuffDurationFactor,
            UnlocksAoE = row.UnlocksAoE,
            AoERadius = aoeRadius,
        };
    }
}

