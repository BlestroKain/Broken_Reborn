using Microsoft.EntityFrameworkCore;

namespace Intersect.Framework.Core.GameObjects.Items;

[Owned]
public partial class EffectData
{
    public EffectData()
    {
        Type = default;
        Percentage = default;
        IsPassive = true;
        Stacking = EffectStacking.Stack;
    }

    public EffectData(
        ItemEffect type,
        int percentage,
        bool isPassive = true,
        EffectStacking stacking = EffectStacking.Stack
    )
    {
        Type = type;
        Percentage = percentage;
        IsPassive = isPassive;
        Stacking = stacking;
    }

    public ItemEffect Type { get; set; }

    public int Percentage { get; set; }

    public bool IsPassive { get; set; }

    public EffectStacking Stacking { get; set; }
}