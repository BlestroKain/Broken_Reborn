using System;

namespace Intersect.Framework.Core.GameObjects.Zones;

/// <summary>
///     Numerical modifiers applied within a zone or subzone.
///     Values are stored as percentages where 100 represents the default rate.
/// </summary>
public class ZoneModifiers
{
    /// <summary>
    ///     Experience gain rate percentage.
    /// </summary>
    public int ExperienceRate { get; set; } = 100;

    /// <summary>
    ///     Gold drop rate percentage.
    /// </summary>
    public int GoldRate { get; set; } = 100;
}
