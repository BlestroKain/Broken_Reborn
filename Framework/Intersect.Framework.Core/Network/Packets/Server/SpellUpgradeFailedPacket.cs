using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class SpellUpgradeFailedPacket : IntersectPacket
{
    public enum Reason
    {
        NotEnoughPoints,
        AlreadyMaxLevel,
        SpellNotOwned,
        ServerError,
    }

    // Parameterless Constructor for MessagePack
    public SpellUpgradeFailedPacket()
    {
    }

    public SpellUpgradeFailedPacket(Guid spellId, Reason reason)
    {
        SpellId = spellId;
        Reason = reason;
    }

    [Key(0)]
    public Guid SpellId { get; set; }

    [Key(1)]
    public Reason Reason { get; set; }
}

