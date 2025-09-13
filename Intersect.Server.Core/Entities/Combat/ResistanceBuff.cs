using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.Server.Entities.Combat;

public class ResistanceBuff
{
    public float[] Resistances;
    public long ExpireTime;
    public SpellDescriptor Spell;

    public ResistanceBuff(SpellDescriptor spell, float[] resistances, long expireTime)
    {
        Spell = spell;
        Resistances = resistances;
        ExpireTime = expireTime;
    }
}

