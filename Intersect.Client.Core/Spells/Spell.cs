using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.Client.Spells;


public partial class Spell
{

    public Guid Id { get; set; }
    public int Level { get; set; }
    public SpellProperties Properties { get; set; } = new();

    public Spell Clone()
    {
        var newSpell = new Spell()
        {
            Id = Id,
            Level = Level,
            Properties = new SpellProperties(Properties),
        };

        return newSpell;
    }

    public void Load(Guid spellId, int level)
    {
        Id = spellId;
        Level = level;
        Properties ??= new SpellProperties();
    }

}
