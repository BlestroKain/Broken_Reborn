using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Enums;
using Intersect.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.GameObjects;

[Owned]
public partial class SpellCombatDescriptor
{
    [NotMapped]
    public long[] VitalDiff = new long[Enum.GetValues<Vital>().Length];

    public int CritChance { get; set; }

    public double CritMultiplier { get; set; } = 1.5;

    public int DamageType { get; set; } = 1;

    [Column("Element")]
    public ElementType Element { get; set; } = ElementType.Neutral;

    public int HitRadius { get; set; }

    public bool Friendly { get; set; }

    public int CastRange { get; set; }

    //Extra Data, Teleport Coords, Custom Spells, Etc
    [Column("Projectile")]
    public Guid ProjectileId { get; set; }

    [NotMapped]
    [JsonIgnore]
    public ProjectileDescriptor Projectile
    {
        get => ProjectileDescriptor.Get(ProjectileId);
        set => ProjectileId = value?.Id ?? Guid.Empty;
    }

    //Heal/Damage
    [Column("VitalDiff")]
    [JsonIgnore]
    public string VitalDiffJson
    {
        get => DatabaseUtils.SaveLongArray(VitalDiff, Enum.GetValues<Vital>().Length);
        set => VitalDiff = DatabaseUtils.LoadLongArray(value, Enum.GetValues<Vital>().Length);
    }

    //Buff/Debuff Data
    [Column("StatDiff")]
    [JsonIgnore]
    public string StatDiffJson
    {
        get => DatabaseUtils.SaveIntArray(StatDiff, Enum.GetValues<Stat>().Length);
        set => StatDiff = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Stat>().Length);
    }

    [NotMapped]
    public int[] StatDiff { get; set; } = new int[Enum.GetValues<Stat>().Length];

    //Buff/Debuff Data
    [Column("PercentageStatDiff")]
    [JsonIgnore]
    public string PercentageStatDiffJson
    {
        get => DatabaseUtils.SaveIntArray(PercentageStatDiff, Enum.GetValues<Stat>().Length);
        set => PercentageStatDiff = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Stat>().Length);
    }

    [NotMapped]
    public int[] PercentageStatDiff { get; set; } = new int[Enum.GetValues<Stat>().Length];

    public int Scaling { get; set; } = 0;

    public int ScalingStat { get; set; }

    public SpellTargetType TargetType { get; set; }

    public bool HoTDoT { get; set; }

    public int HotDotInterval { get; set; }

    public int Duration { get; set; }

    public SpellEffect Effect { get; set; }

    public string TransformSprite { get; set; }

    [Column("OnHit")]
    public int OnHitDuration { get; set; }

    [Column("Trap")]
    public int TrapDuration { get; set; }

    private static bool TryGetUpgrade(SpellProperties props, string key, out int value)
    {
        value = 0;
        return props?.CustomUpgrades != null && props.CustomUpgrades.TryGetValue(key, out value);
    }

    public int GetEffectiveCritChance(SpellProperties props)
    {
        var value = CritChance;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.CritChance, out var up))
        {
            value += up;
        }

        return value;
    }

    public double GetEffectiveCritMultiplier(SpellProperties props)
    {
        var value = CritMultiplier;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.CritMultiplier, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveHitRadius(SpellProperties props)
    {
        var value = HitRadius;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.HitRadius, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveCastRange(SpellProperties props)
    {
        var value = CastRange;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.CastRange, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveStatDiff(Stat stat, SpellProperties props)
    {
        var value = StatDiff[(int)stat];
        var key = SpellUpgradeKeys.Combat.StatDiff.GetKey(stat);
        if (key != null && TryGetUpgrade(props, key, out var up))
        {
            value += up;
        }

        return value;
    }

    public long GetEffectiveVitalDiff(Vital vital, SpellProperties props)
    {
        var value = VitalDiff[(int)vital];
        var key = SpellUpgradeKeys.Combat.VitalDiff.GetKey(vital);
        if (key != null && TryGetUpgrade(props, key, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveScaling(SpellProperties props)
    {
        var value = Scaling;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.Scaling, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveDuration(SpellProperties props)
    {
        var value = Duration;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.Duration, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveHotDotInterval(SpellProperties props)
    {
        var value = HotDotInterval;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.HotDotInterval, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveOnHitDuration(SpellProperties props)
    {
        var value = OnHitDuration;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.OnHitDuration, out var up))
        {
            value += up;
        }

        return value;
    }

    public int GetEffectiveTrapDuration(SpellProperties props)
    {
        var value = TrapDuration;
        if (TryGetUpgrade(props, SpellUpgradeKeys.Combat.TrapDuration, out var up))
        {
            value += up;
        }

        return value;
    }
}