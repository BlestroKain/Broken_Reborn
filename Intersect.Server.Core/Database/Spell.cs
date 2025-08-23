using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Spell
{
    public Spell()
    {
        Properties = new SpellProperties();
    }

    public Spell(Guid spellId, SpellProperties properties = null)
    {
        SpellId = spellId;

        var descriptor = SpellDescriptor.Get(spellId);
        if (properties != null)
        {
            Properties = new SpellProperties(properties);
        }
        else if (descriptor?.Properties != null)
        {
            Properties = new SpellProperties(descriptor.Properties);
        }
        else
        {
            Properties = new SpellProperties();
        }
    }

    public Guid SpellId { get; set; }

    [NotMapped]
    public string SpellName => SpellDescriptor.GetName(SpellId);

    [NotMapped]
    public SpellDescriptor Descriptor => SpellDescriptor.Get(SpellId);

    [NotMapped]
    public SpellProperties Properties { get; set; } = new();

    [Column(nameof(Properties))]
    [JsonIgnore]
    public string SpellPropertiesJson
    {
        get => JsonConvert.SerializeObject(Properties);
        set => Properties = JsonConvert.DeserializeObject<SpellProperties>(value ?? string.Empty) ?? new();
    }

    public static Spell None => new Spell(Guid.Empty);

    public Spell Clone()
    {
        return new Spell
        {
            SpellId = this.SpellId,
            Properties = new SpellProperties(this.Properties)
        };
    }

    public virtual void Set(Spell spell)
    {
        SpellId = spell.SpellId;
        Properties = new SpellProperties(spell.Properties);
    }
}
