using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.Config;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class GiveJobExperienceCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.GiveJobExperience;

    public Dictionary<JobType, long> JobExp { get; set; } = new();
}
