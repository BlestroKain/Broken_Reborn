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

    public UnlockedBestiaryEntriesPacket(Dictionary<Guid, Dictionary<BestiaryUnlock, int>> unlocks)
    {
        Unlocks = unlocks;
    }

    [Key(0)]
    public Dictionary<Guid, Dictionary<BestiaryUnlock, int>> Unlocks { get; set; }
}

