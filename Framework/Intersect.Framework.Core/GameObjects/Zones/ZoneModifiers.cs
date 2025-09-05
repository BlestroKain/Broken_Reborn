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

    /// <summary>
    ///     General item drop chance percentage.
    /// </summary>
    public int DropRate { get; set; } = 100;

    /// <summary>
    ///     Damage dealt percentage.
    /// </summary>
    public int DamageRate { get; set; } = 100;

    /// <summary>
    ///     Movement speed percentage applied to players.
    /// </summary>
    public int MovementSpeed { get; set; } = 100;

    /// <summary>
    ///     Mount speed percentage applied to players.
    /// </summary>
    public int MountSpeed { get; set; } = 100;

    /// <summary>
    ///     Vital regeneration percentage applied to players.
    /// </summary>
    public int RegenerationRate { get; set; } = 100;

    public ZoneModifiers Clone()
    {
        return new ZoneModifiers
        {
            ExperienceRate = ExperienceRate,
            GoldRate = GoldRate,
            DropRate = DropRate,
            DamageRate = DamageRate,
            MovementSpeed = MovementSpeed,
            MountSpeed = MountSpeed,
            RegenerationRate = RegenerationRate,
        };
    }
}
