using System;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Utilities;

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

        return SpellMath.GetEffective(baseDesc, 0, row);
    }

    /// <summary>
    ///     Calculates the spell point cost required to upgrade a spell to the
    ///     specified <paramref name="level"/>.
    /// </summary>
    /// <param name="level">The level the spell is being upgraded to.</param>
    /// <returns>The number of spell points required for the upgrade.</returns>
    public static int GetUpgradeCost(int level) => level;
}
