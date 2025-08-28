using System;

namespace Intersect.Config;

/// <summary>
///     Configuration options for alignment prisms.
/// </summary>
public class PrismOptions
{
    /// <summary>
    ///     Base amount of hit points a prism has at level 1.
    /// </summary>
    public int BaseHp { get; set; } = 1000;

    /// <summary>
    ///     Additional hit points gained per prism level.
    /// </summary>
    public int HpPerLevel { get; set; } = 100;

    /// <summary>
    ///     Seconds after placement until the prism becomes vulnerable.
    /// </summary>
    public int MaturationSeconds { get; set; } = 300;

    /// <summary>
    ///     Seconds after the last hit before the prism battle ends.
    /// </summary>
    public int AttackCooldownSeconds { get; set; } = 30;
}
