using System.Collections.Generic;
using Intersect.Enums;
using Intersect;

namespace Intersect.Combat;

/// <summary>
/// Provides a lookup for elemental damage multipliers.
/// </summary>
public static class ElementalAffinity
{
    public static float Advantage { get; private set; } = 1.5f;

    public static float Neutral { get; private set; } = 1.0f;

    public static float Disadvantage { get; private set; } = 0.5f;

    private static Dictionary<ElementType, Dictionary<ElementType, float>> Multipliers = BuildMultipliers();

    private static Dictionary<ElementType, Dictionary<ElementType, float>> BuildMultipliers() => new()
    {
        [ElementType.Water] = new()
        {
            [ElementType.Fire] = Advantage,
            [ElementType.Nature] = Disadvantage,
        },
        [ElementType.Fire] = new()
        {
            [ElementType.Nature] = Advantage,
            [ElementType.Water] = Disadvantage,
        },
        [ElementType.Nature] = new()
        {
            [ElementType.Water] = Advantage,
            [ElementType.Fire] = Disadvantage,
        },
        [ElementType.Earth] = new()
        {
            [ElementType.Air] = Advantage,
        },
        [ElementType.Air] = new()
        {
            [ElementType.Earth] = Disadvantage,
        },
        [ElementType.Light] = new()
        {
            [ElementType.Dark] = Advantage,
        },
        [ElementType.Dark] = new()
        {
            [ElementType.Light] = Advantage,
        },
    };

    public static void LoadFromOptions(Options options)
    {
        Advantage = options.Combat.ElementalAdvantage;
        Neutral = options.Combat.ElementalNeutral;
        Disadvantage = options.Combat.ElementalDisadvantage;
        Multipliers = BuildMultipliers();
    }

    /// <summary>
    /// Get the multiplier for an attacker/defender elemental combination.
    /// </summary>
    /// <param name="attacker">The attacking element.</param>
    /// <param name="defender">The defending element.</param>
    /// <returns>The damage multiplier.</returns>
    public static float GetMultiplier(ElementType attacker, ElementType defender)
    {
        if (Multipliers.TryGetValue(attacker, out var inner) && inner.TryGetValue(defender, out var value))
        {
            return value;
        }

        return Neutral;
    }
}

