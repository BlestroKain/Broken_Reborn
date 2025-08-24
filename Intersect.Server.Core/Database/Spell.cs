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
        get => JsonConvert.SerializeObject(Properties ??= new());
        set => Properties = JsonConvert.DeserializeObject<SpellProperties>(value ?? string.Empty) ?? new();
    }

    [NotMapped]
    public Dictionary<int, SpellProperties> LevelUpgrades { get; set; } = new();

    [Column(nameof(LevelUpgrades))]
    [JsonIgnore]
    public string LevelUpgradesJson
    {
        get => JsonConvert.SerializeObject(LevelUpgrades ??= new());
        set => LevelUpgrades =
            JsonConvert.DeserializeObject<Dictionary<int, SpellProperties>>(value ?? string.Empty) ?? new();
    }

    public static Spell None => new Spell(Guid.Empty);

    public Spell Clone()
    {
        return new Spell
        {
            SpellId = this.SpellId,
            Properties = new SpellProperties(this.Properties),
            LevelUpgrades = this.LevelUpgrades.ToDictionary(kv => kv.Key, kv => new SpellProperties(kv.Value))
        };
    }

    public virtual void Set(Spell spell)
    {
        SpellId = spell.SpellId;
        Properties = new SpellProperties(spell.Properties);
        LevelUpgrades = spell.LevelUpgrades.ToDictionary(kv => kv.Key, kv => new SpellProperties(kv.Value));
    }
}
