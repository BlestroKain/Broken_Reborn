using System;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class ChangeBestiaryCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.ChangeBestiary;

    public Guid NpcId { get; set; }

    public BestiaryUnlock UnlockType { get; set; }

    public int Amount { get; set; }

    public bool Add { get; set; } = true;
}
