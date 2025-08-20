using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class SpellUpgradeFailedPacket : IntersectPacket
{
    public enum FailureReason
    {
        NoSuchSpell,
        MaxLevelReached,
        NotEnoughPoints,
        RequirementsNotMet,
    }

    // Parameterless Constructor for MessagePack
    public SpellUpgradeFailedPacket()
    {
    }

    public SpellUpgradeFailedPacket(Guid spellId, FailureReason reason)
    {
        SpellId = spellId;
        Reason = reason;
    }

    [Key(0)]
    public Guid SpellId { get; set; }

    [Key(1)]
    public FailureReason Reason { get; set; }
}

