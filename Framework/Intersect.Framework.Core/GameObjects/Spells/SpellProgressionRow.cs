using System;
using System.Collections.Generic;
using Intersect.Enums;

namespace Intersect.GameObjects;

public class SpellProgressionRow
{
    public long[] VitalCostDeltas { get; set; } = new long[Enum.GetValues<Vital>().Length];

    public int CastTimeDeltaMs { get; set; }

    public int CooldownDeltaMs { get; set; }

    public Dictionary<string, int> CustomUpgradeDeltas { get; set; } = new();
}

