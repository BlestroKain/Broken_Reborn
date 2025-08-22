using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class UpdateSpellPointsPacket : IntersectPacket
{
    public UpdateSpellPointsPacket()
    {
    }

    public UpdateSpellPointsPacket(int spellPoints)
    {
        SpellPoints = spellPoints;
    }

    [Key(0)]
    public int SpellPoints { get; set; }
}
