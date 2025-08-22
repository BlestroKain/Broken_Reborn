using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class SpellUpdatePacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public SpellUpdatePacket()
    {
    }

    public SpellUpdatePacket(int slot, Guid spellId, int level)
    {
        Slot = slot;
        SpellId = spellId;
        Level = level;
    }

    [Key(0)]
    public int Slot { get; set; }

    [Key(1)]
    public Guid SpellId { get; set; }

    [Key(2)]
    public int Level { get; set; }

}
