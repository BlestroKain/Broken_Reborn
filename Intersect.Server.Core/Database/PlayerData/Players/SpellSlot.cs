using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Collections.Slotting;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class SpellSlot : ISlot, IPlayerOwned
{
    public static SpellSlot Create(int slotIndex) => new(slotIndex);

    public SpellSlot() { }

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

    // Backing field for NPC or non-player spell identifiers.
    private Guid _spellId;

    [NotMapped]
    public Guid SpellId
    {
        get => PlayerSpell?.SpellId ?? _spellId;
        set
        {
            if (PlayerSpell != null)
            {
                PlayerSpell.SpellId = value;
            }
            else
            {
                _spellId = value;
            }
        }
    }

    [JsonIgnore]
    public bool IsEmpty => SpellId == default;

    public int Level { get; set; } = 1;

    public SpellSlot Clone()
    {
        return new SpellSlot(Slot)
        {
            PlayerSpellId = PlayerSpellId,
            PlayerSpell = PlayerSpell,
            _spellId = _spellId,
            Level = Level
        };
    }

    public void Set(Spell spell)
    {
        SpellId = spell.SpellId;
        Level = spell.Properties?.Level ?? 1;
    }

    public void Set(SpellSlot slot)
    {
        PlayerSpellId = slot.PlayerSpellId;
        PlayerSpell = slot.PlayerSpell;
        _spellId = slot._spellId;
        Level = slot.Level;
    }
}
