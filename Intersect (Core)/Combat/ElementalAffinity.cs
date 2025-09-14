using System.Collections.Generic;
using System.Linq;
using Intersect.Enums;
using Intersect;

namespace Intersect.Combat;

/// <summary>
/// Elemental affinity table with full coverage (every attacker->defender pair),
/// inspired by Pokémon-style strengths/weaknesses.
/// Multipliers are sourced from Options (Advantage/Neutral/Disadvantage).
/// Unspecified pairs default to Neutral; self-vs-self is Neutral.
/// </summary>
public static class ElementalAffinity
{
    public static float Advantage { get; private set; } = 1.5f;
    public static float Neutral { get; private set; } = 1.0f;
    public static float Disadvantage { get; private set; } = 0.5f;

    // Full matrix: attacker -> (defender -> multiplier)
    private static Dictionary<ElementType, Dictionary<ElementType, float>> _table = BuildDefault();

    public static void LoadFromOptions(Options options)
    {
        Advantage = options.Combat.ElementalAdvantage;
        Neutral = options.Combat.ElementalNeutral;
        Disadvantage = options.Combat.ElementalDisadvantage;

        // rebuild defaults with the new numbers and re-ensure coverage
        _table = BuildDefault();
        EnsureCompleteCoverage();
    }

    /// <summary>
    /// Returns the damage multiplier for attacker (element) against defender (element).
    /// </summary>
    public static float GetMultiplier(ElementType attacker, ElementType defender)
    {
        if (_table.TryGetValue(attacker, out var row) && row.TryGetValue(defender, out var val))
        {
            return val;
        }

        return Neutral;
    }

    /// <summary>
    /// Optional: allow runtime overrides (e.g., from an admin command or future config).
    /// Any missing pairs will remain Neutral after calling this.
    /// </summary>
    public static void OverrideMapping(Dictionary<ElementType, Dictionary<ElementType, float>> overrides)
    {
        foreach (var (atk, inner) in overrides)
        {
            if (!_table.TryGetValue(atk, out var row))
            {
                row = new Dictionary<ElementType, float>();
                _table[atk] = row;
            }

            foreach (var (def, mul) in inner)
            {
                row[def] = mul;
            }
        }
        EnsureCompleteCoverage();
    }

    // ---- Internals ----

    private static Dictionary<ElementType, Dictionary<ElementType, float>> BuildDefault()
    {
        // Elements expected in your project:
        // Neutral, Water, Fire, Earth, Air, Light, Dark, Nature
        // Reference logic (Pokémon-ish, adaptada a tu set):
        // - Water:   Adv vs Fire, Earth | Disadv vs Nature
        // - Fire:    Adv vs Nature      | Disadv vs Water, Earth
        // - Nature:  Adv vs Water,Earth,Dark | Disadv vs Fire,Air,Light
        // - Earth:   Adv vs Fire,Air    | Disadv vs Water,Nature
        // - Air:     Adv vs Nature      | Disadv vs Earth
        // - Light:   Adv vs Dark        | (otros neutrales)
        // - Dark:    Adv vs Light       | Disadv vs Nature
        // - Neutral: (todo neutral)

        var T = new Dictionary<ElementType, Dictionary<ElementType, float>>
        {
            [ElementType.Neutral] = new()
            {
                [ElementType.Neutral] = Neutral,
                [ElementType.Water] = Neutral,
                [ElementType.Fire] = Neutral,
                [ElementType.Earth] = Neutral,
                [ElementType.Air] = Neutral,
                [ElementType.Light] = Neutral,
                [ElementType.Dark] = Neutral,
                [ElementType.Nature] = Neutral,
            },

            [ElementType.Water] = new()
            {
                [ElementType.Fire] = Advantage,
                [ElementType.Earth] = Advantage,
                [ElementType.Nature] = Disadvantage,
                // otros pares se rellenan como Neutral
            },

            [ElementType.Fire] = new()
            {
                [ElementType.Nature] = Advantage,
                [ElementType.Water] = Disadvantage,
                [ElementType.Earth] = Disadvantage,
            },

            [ElementType.Nature] = new()
            {
                [ElementType.Water] = Advantage,
                [ElementType.Earth] = Advantage,
                [ElementType.Dark] = Advantage,
                [ElementType.Fire] = Disadvantage,
                [ElementType.Air] = Disadvantage,
                [ElementType.Light] = Disadvantage,
            },

            [ElementType.Earth] = new()
            {
                [ElementType.Fire] = Advantage,
                [ElementType.Air] = Advantage,
                [ElementType.Water] = Disadvantage,
                [ElementType.Nature] = Disadvantage,
            },

            [ElementType.Air] = new()
            {
                [ElementType.Nature] = Advantage,
                [ElementType.Earth] = Disadvantage,
            },

            [ElementType.Light] = new()
            {
                [ElementType.Dark] = Advantage,
            },

            [ElementType.Dark] = new()
            {
                [ElementType.Light] = Advantage,
                [ElementType.Nature] = Disadvantage,
            },
        };

        EnsureCompleteCoverage(T);
        return T;
    }

    /// <summary>
    /// Make sure every attacker has entries for every defender.
    /// Unset pairs become Neutral. Self-vs-self is always Neutral.
    /// </summary>
    private static void EnsureCompleteCoverage()
    {
        EnsureCompleteCoverage(_table);
    }

    private static void EnsureCompleteCoverage(Dictionary<ElementType, Dictionary<ElementType, float>> table)
    {
        var elements = ((ElementType[])System.Enum.GetValues(typeof(ElementType))).ToList();

        foreach (var atk in elements)
        {
            if (!table.TryGetValue(atk, out var row))
            {
                row = new Dictionary<ElementType, float>();
                table[atk] = row;
            }

            foreach (var def in elements)
            {
                if (atk == def)
                {
                    row[def] = Neutral; // self always neutral
                    continue;
                }

                if (!row.ContainsKey(def))
                {
                    row[def] = Neutral; // fill gaps
                }
            }
        }
    }
}
