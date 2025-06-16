using MessagePack;
using Intersect.Config;


namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class JobSyncPacket : IntersectPacket
{
    // Constructor sin parámetros para MessagePack
    public JobSyncPacket()
    {
    }

    // Constructor para inicializar los datos
    public JobSyncPacket(Dictionary<JobType, JobData> jobs)
    {
        Jobs = jobs;
    }

    [Key(0)]
    public Dictionary<JobType, JobData> Jobs { get; set; }
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
