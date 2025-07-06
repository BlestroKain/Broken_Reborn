namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class CreateGuildCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.CreateGuild;
}