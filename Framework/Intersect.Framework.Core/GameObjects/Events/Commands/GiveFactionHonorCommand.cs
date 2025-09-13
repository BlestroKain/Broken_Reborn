using System;
using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Events;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class GiveFactionHonorCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.GiveFactionHonor;

    public Dictionary<Factions, int> Honor { get; set; } = new();

    public GiveFactionHonorCommand()
    {
        foreach (Factions faction in Enum.GetValues(typeof(Factions)))
        {
            if (faction != Factions.Neutral)
            {
                Honor[faction] = 0;
            }
        }
    }
}
