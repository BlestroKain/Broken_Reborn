using System;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Framework.Core.GameObjects.Conditions.ConditionMetadata;

public partial class BeastHasUnlockCondition : Condition
{
    public override ConditionType Type { get; } = ConditionType.BeastHasUnlock;

    public Guid NpcId { get; set; }

    public BestiaryUnlock Unlock { get; set; }

    public int Value { get; set; }
}
