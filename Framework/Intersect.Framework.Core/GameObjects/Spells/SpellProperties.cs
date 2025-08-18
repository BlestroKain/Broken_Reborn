using System;
using System.Collections.Generic;
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

        Level = other.Level;
        LastUsedAtMs = other.LastUsedAtMs;
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

        CustomRolls = new Dictionary<int, int[]>(other.CustomRolls.Count);
        foreach (var kvp in other.CustomRolls)
        {
            CustomRolls[kvp.Key] = (int[])kvp.Value.Clone();
        }
    }

    [Key(0)]
    public int Level { get; set; }

    [Key(1)]
    public long LastUsedAtMs { get; set; }

    [Key(2)]
    public long[] VitalCostDeltas { get; set; } = new long[Enum.GetValues<Vital>().Length];

    [Key(3)]
    public int PowerBonusFlat { get; set; }

    [Key(4)]
    public float PowerScalingBonus { get; set; }

    [Key(5)]
    public int CastTimeDeltaMs { get; set; }

    [Key(6)]
    public int CooldownDeltaMs { get; set; }

    [Key(7)]
    public float BuffStrengthFactor { get; set; }

    [Key(8)]
    public float BuffDurationFactor { get; set; }

    [Key(9)]
    public float DebuffStrengthFactor { get; set; }

    [Key(10)]
    public float DebuffDurationFactor { get; set; }

    [Key(11)]
    public bool UnlocksAoE { get; set; }

    [Key(12)]
    public int AoERadiusDelta { get; set; }

    [Key(13)]
    public Dictionary<int, int[]> CustomRolls { get; set; } = new Dictionary<int, int[]>();
}

