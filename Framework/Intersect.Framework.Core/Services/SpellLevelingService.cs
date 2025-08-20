using System;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.Framework.Core.Services;

public static class SpellLevelingService
{
    public sealed class EffectiveSpellStats
    {
        public int CastTimeMs { get; init; }
        public int CooldownTimeMs { get; init; }
        public long[] VitalCosts { get; init; } = Array.Empty<long>();
        public int PowerBonusFlat { get; init; }
        public float PowerScalingBonus { get; init; }
        public float BuffStrengthFactor { get; init; }
        public float BuffDurationFactor { get; init; }
        public float DebuffStrengthFactor { get; init; }
        public float DebuffDurationFactor { get; init; }
        public bool UnlocksAoE { get; init; }
        public int AoERadius { get; init; }
    }

    public static EffectiveSpellStats BuildAdjusted(SpellDescriptor baseDesc, SpellProgressionRow row)
    {
        if (baseDesc == null)
        {
            throw new ArgumentNullException(nameof(baseDesc));
        }

        if (row == null)
        {
            throw new ArgumentNullException(nameof(row));
        }

        var castTime = Math.Max(0, baseDesc.CastDuration + row.CastTimeDeltaMs);
        var cooldown = Math.Max(0, baseDesc.CooldownDuration + row.CooldownDeltaMs);

        var baseCosts = baseDesc.VitalCost ?? Array.Empty<long>();
        var rowCosts = row.VitalCostDeltas ?? Array.Empty<long>();
        var length = Math.Max(baseCosts.Length, rowCosts.Length);
        var vitalCosts = new long[length];
        var min = Math.Min(baseCosts.Length, rowCosts.Length);

        for (var i = 0; i < min; ++i)
        {
            vitalCosts[i] = baseCosts[i] + rowCosts[i];
        }

        for (var i = min; i < baseCosts.Length; ++i)
        {
            vitalCosts[i] = baseCosts[i];
        }

        for (var i = min; i < rowCosts.Length; ++i)
        {
            vitalCosts[i] = rowCosts[i];
        }

        var aoeRadius = baseDesc.Combat?.HitRadius ?? 0;
        aoeRadius += row.AoERadiusDelta;

        return new EffectiveSpellStats
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
