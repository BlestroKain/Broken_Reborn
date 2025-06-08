using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.Config;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class JobSyncPacket : IntersectPacket
{
    public JobSyncPacket()
    {
    }

    public JobSyncPacket(Dictionary<JobType, JobData> jobs)
    {
        Jobs = jobs;
    }

    [Key(0)]
    public Dictionary<JobType, JobData> Jobs { get; set; } = new();
}

[MessagePackObject]
public class JobData
{
    [Key(0)]
    public int Level { get; set; }

    [Key(1)]
    public long Experience { get; set; }

    [Key(2)]
    public long ExperienceToNextLevel { get; set; }
}
