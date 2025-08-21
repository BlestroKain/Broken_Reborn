using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class PlayerSpell : IPlayerOwned
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; set; }

    public Guid SpellId { get; set; }

    public int Level { get; set; } = 1;

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
