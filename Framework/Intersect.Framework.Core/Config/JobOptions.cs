using System.Collections.Generic;
using Intersect.Enums;

namespace Intersect.Framework.Core.Config;

/// <summary>
/// Options related to the job system.
/// </summary>
public class JobOptions
{
    /// <summary>Maximum level for jobs.</summary>
    public int MaxJobLevel { get; set; } = 100;

    /// <summary>Base job experience.</summary>
    public long BaseJobExp { get; set; } = 100;

    /// <summary>Show progress notifications.</summary>
    public bool ShowJobProgressNotifications { get; set; } = true;

    /// <summary>Automatically level up jobs.</summary>
    public bool AutoJobLevelUp { get; set; } = true;

    /// <summary>Maximum active jobs.</summary>
    public int MaxActiveJobs { get; set; } = 3;

    /// <summary>Lose job experience on death.</summary>
    public bool JobExpLossOnDeath { get; set; } = false;

    /// <summary>Percent of experience lost on death.</summary>
    public int JobExpLossPercent { get; set; } = 10;

    /// <summary>Base experience per job type.</summary>
    public Dictionary<JobType, long> JobBaseExp { get; set; } = new();
}

public enum JobType
{
    None,
    Farming,
    Mining,
    Fishing,
    Lumberjack,
    Cooking,
    Alchemy,
    Crafting,
    Smithing,
    Hunter,
    JobCount
}
