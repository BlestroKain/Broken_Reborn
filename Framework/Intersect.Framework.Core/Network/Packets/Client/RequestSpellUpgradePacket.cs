using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class RequestSpellUpgradePacket : IntersectPacket
{
    // Parameterless Constructor for MessagePack
    public RequestSpellUpgradePacket()
    {
    }

    public RequestSpellUpgradePacket(Guid spellId)
    {
        SpellId = spellId;
    }

    [Key(0)]
    public Guid SpellId { get; set; }
}

