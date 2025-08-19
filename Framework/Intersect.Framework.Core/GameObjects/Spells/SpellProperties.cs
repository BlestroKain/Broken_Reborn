using System;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Spells;

[MessagePackObject]
public partial class SpellProperties
{
    public SpellProperties()
    {
    }

    public SpellProperties(SpellProperties other)
    {
        if (other == default)
        {
            throw new ArgumentNullException(nameof(other));
        }

        Array.Copy(other.VitalCostDeltas, VitalCostDeltas, Enum.GetValues<Vital>().Length);
        PowerBonusFlat = other.PowerBonusFlat;
        PowerScalingBonus = other.PowerScalingBonus;
        CastTimeDeltaMs = other.CastTimeDeltaMs;
        CooldownDeltaMs = other.CooldownDeltaMs;
        BuffStrengthFactor = other.BuffStrengthFactor;
        BuffDurationFactor = other.BuffDurationFactor;
        DebuffStrengthFactor = other.DebuffStrengthFactor;
        DebuffDurationFactor = other.DebuffDurationFactor;
        UnlocksAoE = other.UnlocksAoE;
        AoERadiusDelta = other.AoERadiusDelta;
    }

    [Key(0)]
    public long[] VitalCostDeltas { get; set; } = new long[Enum.GetValues<Vital>().Length];

    [Key(1)]
    public int PowerBonusFlat { get; set; }

    [Key(2)]
    public float PowerScalingBonus { get; set; }

    [Key(3)]
    public int CastTimeDeltaMs { get; set; }

    [Key(4)]
    public int CooldownDeltaMs { get; set; }

    [Key(5)]
    public float BuffStrengthFactor { get; set; }

    [Key(6)]
    public float BuffDurationFactor { get; set; }

    [Key(7)]
    public float DebuffStrengthFactor { get; set; }

    [Key(8)]
    public float DebuffDurationFactor { get; set; }

    [Key(9)]
    public bool UnlocksAoE { get; set; }

    [Key(10)]
    public int AoERadiusDelta { get; set; }
}

