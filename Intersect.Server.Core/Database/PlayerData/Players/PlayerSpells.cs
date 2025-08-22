using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class PlayerSpell : IPlayerOwned
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; set; }

    public Guid SpellId { get; set; }

    [NotMapped]
    public SpellProperties Properties { get; set; } = new();

    [Column(nameof(Properties))]
    [JsonIgnore]
    public string SpellPropertiesJson
    {
        get => JsonConvert.SerializeObject(Properties);
        set => Properties = JsonConvert.DeserializeObject<SpellProperties>(value ?? string.Empty) ?? new();
    }

    [NotMapped]
    public int Level
    {
        get => Properties.Level;
        set => Properties.Level = value;
    }

    public int SpellPointsSpent { get; set; } = 0;

    [JsonIgnore]
    public bool IsEmpty => SpellId == default;

    [JsonIgnore]
    public Guid PlayerId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get;  set; }

    [NotMapped]
    [JsonIgnore]
    public GameObjects.SpellDescriptor Spell => GameObjects.SpellDescriptor.Get(SpellId);
}
