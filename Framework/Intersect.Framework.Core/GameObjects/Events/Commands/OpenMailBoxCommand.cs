namespace Intersect.Framework.Core.GameObjects.Events.Commands;


public class OpenMailBoxCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.OpenMailBox;
}