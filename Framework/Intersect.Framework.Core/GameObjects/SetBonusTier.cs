using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Collections;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Intersect.GameObjects;

public class SetBonusTier
{
    public int[] Stats { get; set; } = new int[Enum.GetValues<Stat>().Length];

    public int[] PercentageStats { get; set; } = new int[Enum.GetValues<Stat>().Length];

    public long[] Vitals { get; set; } = new long[Enum.GetValues<Vital>().Length];

    public long[] VitalsRegen { get; set; } = new long[Enum.GetValues<Vital>().Length];

    public int[] PercentageVitals { get; set; } = new int[Enum.GetValues<Vital>().Length];

    public List<EffectData> Effects { get; set; } = new();

    public void Validate()
    {
        Stats = ArrayExtensions.EnsureLen(Stats, Enum.GetValues<Stat>().Length);
        PercentageStats = ArrayExtensions.EnsureLen(PercentageStats, Enum.GetValues<Stat>().Length);
        Vitals = ArrayExtensions.EnsureLen(Vitals, Enum.GetValues<Vital>().Length);
        VitalsRegen = ArrayExtensions.EnsureLen(VitalsRegen, Enum.GetValues<Vital>().Length);
        PercentageVitals = ArrayExtensions.EnsureLen(PercentageVitals, Enum.GetValues<Vital>().Length);
        Effects ??= new List<EffectData>();
    }

    [JsonIgnore]
    public bool HasBonuses =>
        Stats.Any(s => s != 0) ||
        PercentageStats.Any(s => s != 0) ||
        Vitals.Any(v => v != 0) ||
        PercentageVitals.Any(v => v != 0) ||
        VitalsRegen.Any(v => v != 0) ||
        Effects.Any();

    public (int[] stats, int[] percentStats, long[] vitals, long[] vitalsRegen, int[] percentVitals, List<EffectData> effects) GetBonuses()
    {
        return (
            Stats.ToArray(),
            PercentageStats.ToArray(),
            Vitals.ToArray(),
            VitalsRegen.ToArray(),
            PercentageVitals.ToArray(),
            Effects
                .Select(e => new EffectData(e.Type, e.Percentage, e.IsPassive, e.Stacking))
                .ToList()
        );
    }

    public int GetEffectPercentage(ItemEffect type)
    {
        return Effects.Find(effect => effect.Type == type)?.Percentage ?? 0;
    }

    public void SetEffectOfType(ItemEffect type, int value)
    {
        var effectToEdit = Effects.Find(effect => effect.Type == type);
        if (effectToEdit != null)
        {
            effectToEdit.Percentage = value;
        }
    }

    [JsonIgnore]
    public ItemEffect[] EffectsEnabled => Effects.Select(effect => effect.Type).ToArray();
}

