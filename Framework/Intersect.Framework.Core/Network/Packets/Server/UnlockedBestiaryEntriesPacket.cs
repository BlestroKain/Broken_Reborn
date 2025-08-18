using System;
using System.Collections.Generic;
using MessagePack;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class UnlockedBestiaryEntriesPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public UnlockedBestiaryEntriesPacket()
    {
    }

    public UnlockedBestiaryEntriesPacket(Dictionary<Guid, int[]> unlocked, Dictionary<Guid, int> killCounts)
    {
        Unlocked = unlocked;
        KillCounts = killCounts;
    }

    [Key(0)]
    public Dictionary<Guid, int[]> Unlocked { get; set; } = new();

    [Key(1)]
    public Dictionary<Guid, int> KillCounts { get; set; } = new();
}

