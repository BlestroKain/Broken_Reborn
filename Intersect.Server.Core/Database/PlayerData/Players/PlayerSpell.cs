using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Collections.Slotting;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class PlayerSpell : ISlot, IPlayerOwned
{
    public static PlayerSpell Create(int slotIndex) => new(slotIndex);

    public PlayerSpell() { }

    public PlayerSpell(int slot)
    {
        Slot = slot;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; private set; }

    public Guid SpellId { get; set; }

    public int Slot { get; private set; }

    [JsonIgnore]
    public Guid PlayerId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; private set; }

    [NotMapped]
    public SpellProperties Properties { get; set; } = new();

    [Column(nameof(Properties))]
    [JsonIgnore]
    public string SpellPropertiesJson
    {
        get => JsonConvert.SerializeObject(Properties);
        set => Properties = JsonConvert.DeserializeObject<SpellProperties>(value ?? string.Empty) ?? new();
    }

    public int SpellPointsSpent { get; set; } = 0;

    [NotMapped]
    public int Level
    {
        get => Properties.Level;
        set => Properties.Level = value;
    }

    [JsonIgnore]
    public bool IsEmpty => SpellId == default;

    public PlayerSpell Clone()
    {
        return new PlayerSpell(Slot)
        {
            SpellId = SpellId,
            Properties = new SpellProperties(Properties),
            SpellPointsSpent = SpellPointsSpent
        };
    }

    public void Set(Spell spell)
    {
        if (spell == null || spell.SpellId == Guid.Empty)
        {
            SpellId = Guid.Empty;
            Properties = new SpellProperties();
            SpellPointsSpent = 0;
            return;
        }

        SpellId = spell.SpellId;
        var descriptor = SpellDescriptor.Get(spell.SpellId);
        var sourceProperties = spell.Properties;

        if ((sourceProperties == null || sourceProperties.CustomUpgrades.Count == 0) &&
            descriptor?.Properties != null)
        {
            sourceProperties = descriptor.Properties;
        }

        Properties = new SpellProperties(sourceProperties);
    }

    public void Set(PlayerSpell slot)
    {
        SpellId = slot.SpellId;
        Properties = new SpellProperties(slot.Properties);
        SpellPointsSpent = slot.SpellPointsSpent;
    }
}

