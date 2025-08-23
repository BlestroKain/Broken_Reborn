using System.Collections.Generic;

namespace Intersect.Framework.Core.GameObjects.Spells;

public static class SpellUpgradeKeys
{
    public const string CastDuration = "CastDuration";
    public const string CooldownDuration = "CooldownDuration";

    public static class Combat
    {
        public const string CritChance = "Combat.CritChance";
        public const string CritMultiplier = "Combat.CritMultiplier";
        public const string HitRadius = "Combat.HitRadius";
        public const string CastRange = "Combat.CastRange";

        public static class StatDiff
        {
            public const string Attack = "Combat.StatDiff.Attack";
            public const string Intelligence = "Combat.StatDiff.Intelligence";
            public const string Defense = "Combat.StatDiff.Defense";
            public const string Vitality = "Combat.StatDiff.Vitality";
            public const string Speed = "Combat.StatDiff.Speed";
            public const string Agility = "Combat.StatDiff.Agility";
            public const string Damages = "Combat.StatDiff.Damages";
            public const string Cures = "Combat.StatDiff.Cures";
        }

        public static class VitalDiff
        {
            public const string Health = "Combat.VitalDiff.Health";
            public const string Mana = "Combat.VitalDiff.Mana";
        }
    }

    public static class Dash
    {
        public const string Range = "Dash.Range";
    }

    public static readonly HashSet<string> All =
    [
        CastDuration,
        CooldownDuration,
        Combat.CritChance,
        Combat.CritMultiplier,
        Combat.HitRadius,
        Combat.CastRange,
        Combat.StatDiff.Attack,
        Combat.StatDiff.Intelligence,
        Combat.StatDiff.Defense,
        Combat.StatDiff.Vitality,
        Combat.StatDiff.Speed,
        Combat.StatDiff.Agility,
        Combat.StatDiff.Damages,
        Combat.StatDiff.Cures,
        Combat.VitalDiff.Health,
        Combat.VitalDiff.Mana,
        Dash.Range,
    ];

    public static bool IsValid(string key) => All.Contains(key);
}

