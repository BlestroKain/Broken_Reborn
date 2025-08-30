using System;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class PrismUpdatePacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public PrismUpdatePacket()
    {
    }

    public PrismUpdatePacket(
        Guid mapId,
        Guid prismId,
        Factions owner,
        PrismState state,
        int hp,
        int maxHp,
        DateTime? nextVulnerabilityStart
    )
    {
        MapId = mapId;
        PrismId = prismId;
        Owner = owner;
        State = state;
        Hp = hp;
        MaxHp = maxHp;
        NextVulnerabilityStart = nextVulnerabilityStart;
    }

    [Key(0)]
    public Guid MapId { get; set; }

    [Key(1)]
    public Guid PrismId { get; set; }

    [Key(2)]
    public Factions Owner { get; set; }

    [Key(3)]
    public PrismState State { get; set; }

    [Key(4)]
    public int Hp { get; set; }

    [Key(5)]
    public int MaxHp { get; set; }

    [Key(6)]
    public DateTime? NextVulnerabilityStart { get; set; }
}
