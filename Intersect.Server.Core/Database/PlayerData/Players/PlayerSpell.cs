using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class PlayerSpell : Spell, IPlayerOwned
{
    public PlayerSpell() : this(Guid.Empty) { }

    public PlayerSpell(Guid id) : base(id) { }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonIgnore]
    public Guid PlayerId { get; protected set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; protected set; }

    public int Level { get; set; }

    public long LastUsedAtMs { get; set; }
}
