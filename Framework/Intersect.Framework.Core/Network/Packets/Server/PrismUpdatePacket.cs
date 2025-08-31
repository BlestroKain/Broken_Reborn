using System;
using Intersect.Enums;
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
        Guid id,
        Guid mapId,
        int x,
        int y,
        Factions owner,
        PrismState state,
        int hp,
        int maxHp,
        DateTime? nextWindow
    )
    {
        Id = id;
        MapId = mapId;
        X = x;
        Y = y;
        Owner = owner;
        State = state;
        Hp = hp;
        MaxHp = maxHp;
        NextWindow = nextWindow;
    }

    [Key(0)]
    public Guid Id { get; set; }

    [Key(1)]
    public Guid MapId { get; set; }

    [Key(2)]
    public int X { get; set; }

    [Key(3)]
    public int Y { get; set; }

    [Key(4)]
    public Factions Owner { get; set; }

    [Key(5)]
    public PrismState State { get; set; }

    [Key(6)]
    public int Hp { get; set; }

    [Key(7)]
    public int MaxHp { get; set; }

    [Key(8)]
    public DateTime? NextWindow { get; set; }
}
