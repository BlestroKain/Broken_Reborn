namespace Intersect.Client.Spells;


public partial class Spell
{

    public Guid Id { get; set; }
    public int Level { get; internal set; }

    public Spell Clone()
    {
        var newSpell = new Spell()
        {
            Id = Id,
            Level = Level,
        };

        return newSpell;
    }

    public void Load(Guid spellId, int level)
    {
        Id = spellId;
        Level = level;
    }

}
