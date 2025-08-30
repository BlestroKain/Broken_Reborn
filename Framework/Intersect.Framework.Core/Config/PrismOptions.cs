using System;

namespace Intersect.Config;

/// <summary>
///     Configuration options for alignment prisms.
/// </summary>
public class PrismOptions
{
    /// <summary>
    ///     Synchronize configured prisms to the database on startup.
    /// </summary>
    public bool SyncOnStartup { get; set; } = false;

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

    /// <summary>
    ///     Maximum duration in minutes a prism battle may last.
    /// </summary>
    public int MaxBattleDurationMinutes { get; set; } = 15;

    /// <summary>
    ///     Seconds a player must wait after dying before rejoining a prism battle.
    /// </summary>
    public int RespawnCooldownSeconds { get; set; } = 60;

    /// <summary>
    ///     Allow damage outside the vulnerability window.
    /// </summary>
    public bool AllowDamageOutsideVulnerability { get; set; } = false;
    ///     Maximum distance in tiles at which the prism HP bar is displayed.
    /// </summary>
    public int HudDisplayRadius { get; set; } = 10;


    /// <summary>
    /// </summary>
    public bool CaptureInsteadOfDestroy { get; set; } = true;

    ///     Maximum damage a single player can inflict per scheduler tick.
    /// </summary>
    public int DamageCapPerTick { get; set; } = 50;

    /// <summary>
    ///     Interval in seconds between scheduler ticks.
    /// </summary>
    public int SchedulerIntervalSeconds { get; set; } = 1;

}
