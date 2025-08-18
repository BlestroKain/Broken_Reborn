using System;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.Framework.Core.Services;

public static class SpellLevelingService
{
    public sealed class AdjustedSpell
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

    public static AdjustedSpell BuildAdjusted(SpellDescriptor baseDesc, SpellProperties row)
    {
        if (baseDesc == null)
        {
            throw new ArgumentNullException(nameof(baseDesc));
        }

        if (row == null)
        {
            throw new ArgumentNullException(nameof(row));
        }

        var castTime = baseDesc.CastDuration + row.CastTimeDeltaMs;
        var cooldown = baseDesc.CooldownDuration + row.CooldownDeltaMs;

        var vitalCosts = new long[baseDesc.VitalCost.Length];
        for (var i = 0; i < vitalCosts.Length; ++i)
        {
            var delta = 0L;
            if (row.VitalCostDeltas != null && i < row.VitalCostDeltas.Length)
            {
                delta = row.VitalCostDeltas[i];
            }

            vitalCosts[i] = baseDesc.VitalCost[i] + delta;
        }

        var aoeRadius = baseDesc.Combat?.HitRadius ?? 0;
        aoeRadius += row.AoERadiusDelta;

        return new AdjustedSpell
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
