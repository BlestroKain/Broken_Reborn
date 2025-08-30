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

    public PrismUpdatePacket(Guid mapId, Factions owner, PrismState state, int hp, int maxHp, DateTime? nextVulnerabilityStart)
    {
        MapId = mapId;
        Owner = owner;
        State = state;
        Hp = hp;
        MaxHp = maxHp;
        NextVulnerabilityStart = nextVulnerabilityStart;
    }

    [Key(0)]
    public Guid MapId { get; set; }

    [Key(1)]
    public Factions Owner { get; set; }

    [Key(2)]
    public PrismState State { get; set; }

    [Key(3)]
    public int Hp { get; set; }

    [Key(4)]
    public int MaxHp { get; set; }

    [Key(5)]
    public DateTime? NextVulnerabilityStart { get; set; }
}
