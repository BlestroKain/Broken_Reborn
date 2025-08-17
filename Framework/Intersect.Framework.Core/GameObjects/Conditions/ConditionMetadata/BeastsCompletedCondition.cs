using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Framework.Core.GameObjects.Conditions.ConditionMetadata;

public partial class BeastsCompletedCondition : Condition
{
    public override ConditionType Type { get; } = ConditionType.BeastsCompleted;

    public BestiaryUnlock Unlock { get; set; }

    public int Count { get; set; }
}
