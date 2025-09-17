using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Events;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class GiveFactionHonorCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.GiveFactionHonor;

    private Dictionary<Factions, int> mHonor = new();

    public Dictionary<Factions, int> Honor
    {
        get => mHonor;
        set
        {
            mHonor = value ?? new Dictionary<Factions, int>();
            EnsureFactionEntries();
        }
    }

    public GiveFactionHonorCommand()
    {
        EnsureFactionEntries();
    }

    private void EnsureFactionEntries()
    {
        mHonor.Remove(Factions.Neutral);

        if (!mHonor.ContainsKey(Factions.Serolf))
        {
            mHonor[Factions.Serolf] = 0;
        }

        if (!mHonor.ContainsKey(Factions.Nidraj))
        {
            mHonor[Factions.Nidraj] = 0;
        }
    }
}
