using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Collections.Slotting;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class SpellSlot : ISlot, IPlayerOwned
{
    public static SpellSlot Create(int slotIndex) => new(slotIndex);

    public SpellSlot()
    {
    }

    public SpellSlot(int slot)
    {
        Slot = slot;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; private set; }

    // Referencia al hechizo del jugador (no directamente al SpellId base)
    public Guid PlayerSpellId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerSpellId))]
    public virtual PlayerSpell PlayerSpell { get; private set; }

    public int Slot { get; private set; }

    [JsonIgnore]
    public Guid PlayerId { get; private set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; private set; }

    [JsonIgnore]
    public bool IsEmpty => PlayerSpellId == default;
}
