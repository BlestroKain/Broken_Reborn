using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using Intersect.Server.Networking;
using Newtonsoft.Json;

namespace Intersect.Server.Database
{
    public partial class SpellSlot : Spell, ISlot, IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
        public Guid Id { get; private set; }

        [JsonIgnore]
        public Guid PlayerId { get; private set; }

        [JsonIgnore]
        public virtual Player Player { get; private set; }

        public int Slot { get; private set; }

        public SpellSlot() { }

        public SpellSlot(int slot)
        {
            Slot = slot;
        }

        [NotMapped]
        public int Level => SpellBase.Get(SpellId).Level;

        public void LevelUp()
        {
            var spell = SpellBase.Get(SpellId);
            if (spell != null && spell.Level < spell.MaxLevel)
            {
                spell.LevelUp();
                // Enviar la actualización al cliente
                PacketSender.SendSpellUpdate(Player, this);
            }
        }
    }
}
