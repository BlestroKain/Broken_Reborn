using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
namespace Intersect.Server.General;

public static class SpellMath
{
    public static SpellProperties Scale(SpellDescriptor descriptor, SpellProperties? properties = null)
    {
        var scaled = descriptor.GetPropertiesForLevel(properties?.Level ?? 1);

        if (properties?.CustomUpgrades?.Count > 0)
        {
            foreach (var kv in properties.CustomUpgrades)
            {
                if (scaled.CustomUpgrades.ContainsKey(kv.Key))
                {
                    scaled.CustomUpgrades[kv.Key] += kv.Value;
                }
                else
                {
                    scaled.CustomUpgrades[kv.Key] = kv.Value;
                }
            }
        }

        return scaled;
    }
}
