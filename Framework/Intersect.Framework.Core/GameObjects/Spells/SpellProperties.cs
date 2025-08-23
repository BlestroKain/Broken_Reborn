using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Spells;

[MessagePackObject]
public class SpellProperties
{
    public SpellProperties() { }

    public SpellProperties(SpellProperties other)
    {
        Level = other.Level;
        CustomUpgrades = new Dictionary<string, int>(
            other.CustomUpgrades.Where(kv => SpellUpgradeKeys.IsValid(kv.Key))
        );
    }

    [Key(0)]
    public int Level { get; set; } = 1;

    private Dictionary<string, int> _customUpgrades = new();

    [Key(1)]
    public Dictionary<string, int> CustomUpgrades
    {
        get => _customUpgrades;
        set
        {
            _customUpgrades = new Dictionary<string, int>();
            if (value == null)
            {
                return;
            }

            foreach (var kv in value)
            {
                if (SpellUpgradeKeys.IsValid(kv.Key))
                {
                    _customUpgrades[kv.Key] = kv.Value;
                }
            }
        }
    }

    public bool TrySetCustomUpgrade(string key, int value)
    {
        if (!SpellUpgradeKeys.IsValid(key))
        {
            return false;
        }

        _customUpgrades[key] = value;
        return true;
    }
}
