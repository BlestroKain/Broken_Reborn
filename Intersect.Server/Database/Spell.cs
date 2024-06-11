using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.GameObjects;

namespace Intersect.Server.Database
{
    public partial class Spell
    {
        public Spell()
        {
        }

        public Spell(Guid spellId)
        {
            SpellId = spellId;
            Level = 1; // Nivel inicial del hechizo
        }

        public Guid SpellId { get; set; }

        [NotMapped]
        public string SpellName => SpellBase.GetName(SpellId);

        public int Level { get; set; } = 1;

        public static Spell None => new Spell(Guid.Empty);

        public Spell Clone()
        {
            var newSpell = new Spell()
            {
                SpellId = SpellId,
                Level = Level
            };

            return newSpell;
        }

        public virtual void Set(Spell spell)
        {
            SpellId = spell.SpellId;
            Level = spell.Level;
        }
    }
}
