using Intersect.Framework.Core.GameObjects.Events;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class GiveGuildExperienceCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.GiveGuildExperience;

    public long Exp { get; set; }
}
