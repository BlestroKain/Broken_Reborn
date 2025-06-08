using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Enums;
using Intersect.Framework.Core.Config;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Newtonsoft.Json;

namespace Intersect.Server.Entities;

public partial class Player
{
    [Column("JobsData")]
    public string JobsJson
    {
        get => JsonConvert.SerializeObject(Jobs);
        set => Jobs = string.IsNullOrWhiteSpace(value)
            ? new Dictionary<JobType, PlayerJob>()
            : JsonConvert.DeserializeObject<Dictionary<JobType, PlayerJob>>(value) ?? new();
    }

    [NotMapped]
    public Dictionary<JobType, PlayerJob> Jobs { get; set; } = new();

    public void InitializeJobs()
    {
        for (var i = (int)JobType.Farming; i < (int)JobType.JobCount; i++)
        {
            var jt = (JobType)i;
            if (!Jobs.ContainsKey(jt))
            {
                Jobs[jt] = new PlayerJob(jt);
            }
        }
    }

    public void GiveJobExperience(JobType jobType, long experience)
    {
        InitializeJobs();
        if (Jobs.TryGetValue(jobType, out var job))
        {
            job.AddExperience(experience, this);
        }
    }

    public PlayerJob? GetJob(JobType jobType)
    {
        InitializeJobs();
        Jobs.TryGetValue(jobType, out var job);
        return job;
    }
}

public class PlayerJob
{
    public PlayerJob()
    {
    }

    public PlayerJob(JobType type)
    {
        JobType = type;
    }

    public JobType JobType { get; set; }
    public int Level { get; set; } = 1;
    public long Experience { get; set; } = 0;

    public void AddExperience(long amount, Player player)
    {
        Experience += amount;
        while (Experience >= GetExperienceToNextLevel() && Level < Options.JobOptions.MaxJobLevel)
        {
            Experience -= GetExperienceToNextLevel();
            Level++;
            var msg = Strings.Player.LevelUp;
            PacketSender.SendChatMsg(player, string.Format(msg, Level), ChatMessageType.Experience);
        }
        PacketSender.SendJobSync(player);
    }

    public long GetExperienceToNextLevel()
    {
        var baseExp = Options.JobOptions.BaseJobExp;
        if (Options.JobOptions.JobBaseExp.TryGetValue(JobType, out var value))
        {
            baseExp = value;
        }
        return baseExp * Level;
    }
}
