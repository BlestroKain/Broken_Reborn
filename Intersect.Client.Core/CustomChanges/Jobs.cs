using System.Collections.Generic;
using Intersect.Framework.Core.Config;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Entities;

public partial class Player
{
    public Dictionary<JobType, int> JobLevel { get; set; } = new();
    public Dictionary<JobType, long> JobExp { get; set; } = new();
    public Dictionary<JobType, long> JobExpToNextLevel { get; set; } = new();

    public void UpdateJobsFromPacket(Dictionary<JobType, JobData> jobs)
    {
        foreach (var (jobType, data) in jobs)
        {
            JobLevel[jobType] = data.Level;
            JobExp[jobType] = data.Experience;
            JobExpToNextLevel[jobType] = data.ExperienceToNextLevel;
        }
    }
}
