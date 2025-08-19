using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class SpellUpgradedPacket : IntersectPacket
{
    // Parameterless Constructor for MessagePack
    public SpellUpgradedPacket()
    {
    }

    public SpellUpgradedPacket(Guid spellId, int newLevel, int remainingSpellPoints)
    {
        SpellId = spellId;
        NewLevel = newLevel;
        RemainingSpellPoints = remainingSpellPoints;
    }

    [Key(0)]
    public Guid SpellId { get; set; }

    [Key(1)]
    public int NewLevel { get; set; }

    [Key(2)]
    public int RemainingSpellPoints { get; set; }
}

