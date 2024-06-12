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
        public int Level
        {
            get
            {
                var spell = SpellBase.Get(SpellId);
                return spell?.Level ?? 0; // Devolver 0 si spell es null
            }
        }

        public void LevelUp()
        {
            var spell = SpellBase.Get(SpellId);
            if (spell != null && spell.Level < 5)
            {
                spell.LevelUp();
                // Enviar la actualización al cliente
                PacketSender.SendSpellUpdate(Player, this);
            }
        }
    }
}
