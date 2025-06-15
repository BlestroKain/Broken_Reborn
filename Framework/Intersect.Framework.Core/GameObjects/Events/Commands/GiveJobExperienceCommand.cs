

using Intersect.Config;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;
public partial class GiveJobExperienceCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.GiveJobExperience;

    // Diccionario para almacenar experiencia por tipo de oficio
    public Dictionary<JobType, long> JobExp { get; set; } = new Dictionary<JobType, long>();

    // Constructor
    public GiveJobExperienceCommand()
    {
        // Inicializar con los trabajos conocidos y experiencia inicial en 0
        foreach (JobType jobType in Enum.GetValues(typeof(JobType)))
        {
            if (jobType != JobType.None)
            {
                JobExp[jobType] = 0;
            }
        }
    }
}
