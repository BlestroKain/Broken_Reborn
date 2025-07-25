namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public class SendMailBoxCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.SendMail;
}