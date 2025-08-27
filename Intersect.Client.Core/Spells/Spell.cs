using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.Client.Spells;


public partial class Spell
{

    public Guid Id { get; set; }
    public int Level { get; set; }
    public SpellProperties Properties { get; set; } = new();
    public int SpellPointsSpent { get; set; }

    public Spell Clone()
    {
        var newSpell = new Spell()
        {
            Id = Id,
            Level = Level,
            Properties = new SpellProperties(Properties),
            SpellPointsSpent = SpellPointsSpent,
        };

        return newSpell;
    }

    public void Load(Guid spellId, SpellProperties? properties, int spellPointsSpent = 0)
    {
        Id = spellId;
        Properties = properties ?? new SpellProperties();
        Level = Properties.Level;
        SpellPointsSpent = spellPointsSpent;
    }

}
