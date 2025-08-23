using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Spells;

[MessagePackObject]
public class SpellProperties
{
    public SpellProperties() { }

    public SpellProperties(SpellProperties other)
    {
        Level = other.Level;
        CustomUpgrades = new Dictionary<string, int>(other.CustomUpgrades);
    }

    [Key(0)]
    public int Level { get; set; } = 1;

    [Key(1)]
    public Dictionary<string, int> CustomUpgrades { get; set; } = new();
}
