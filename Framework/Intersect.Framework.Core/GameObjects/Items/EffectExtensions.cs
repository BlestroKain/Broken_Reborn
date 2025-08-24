using System.Collections.Generic;

namespace Intersect.Framework.Core.GameObjects.Items;

public static class EffectExtensions
{
    public static void ApplyEffect(this IDictionary<ItemEffect, int> dest, EffectData effect)
    {
        if (!effect.IsPassive)
        {
            return;
        }

        switch (effect.Stacking)
        {
            case EffectStacking.Ignore:
                if (!dest.ContainsKey(effect.Type))
                {
                    dest[effect.Type] = effect.Percentage;
                }
                break;
            case EffectStacking.Renew:
                dest[effect.Type] = effect.Percentage;
                break;
            default:
                if (dest.ContainsKey(effect.Type))
                {
                    dest[effect.Type] += effect.Percentage;
                }
                else
                {
                    dest[effect.Type] = effect.Percentage;
                }
                break;
        }
    }
}
