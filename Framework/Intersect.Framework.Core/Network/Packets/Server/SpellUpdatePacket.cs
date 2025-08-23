using MessagePack;

using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class SpellUpdatePacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public SpellUpdatePacket()
    {
    }

    public SpellUpdatePacket(int slot, Guid spellId, int level, SpellProperties? properties = null)
    {
        Slot = slot;
        SpellId = spellId;
        Level = level;
        Properties = properties;
    }

    [Key(0)]
    public int Slot { get; set; }

    [Key(1)]
    public Guid SpellId { get; set; }

    [Key(2)]
    public int Level { get; set; }

    [Key(3)]
    public SpellProperties? Properties { get; set; }

}
