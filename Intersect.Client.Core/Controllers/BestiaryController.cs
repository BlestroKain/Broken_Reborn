using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Controllers;

public static class BestiaryController
{
    public static IReadOnlyDictionary<Guid, HashSet<BestiaryUnlock>> KnownUnlocks => _known;
    private static readonly Dictionary<Guid, HashSet<BestiaryUnlock>> _known = new();

    public static IReadOnlyList<Guid> AllBeastNpcIds => _allBeastNpcIds;
    private static List<Guid> _allBeastNpcIds = new();

    public static event Action<Guid, BestiaryUnlock>? OnUnlockGained;

    public static void InitializeAllBeasts()
    {
        _allBeastNpcIds = NPCDescriptor.Lookup
            .Select(pair => pair.Value)
            .OfType<NPCDescriptor>()
            .Where(d => d.BestiaryRequirements is { Count: > 0 })
            .Select(d => d.Id)
            .ToList();
    }

    public static bool HasUnlock(Guid npcId, BestiaryUnlock unlock)
        => _known.TryGetValue(npcId, out var set) && set.Contains(unlock);

    public static void ApplyPacket(UnlockedBestiaryEntriesPacket packet)
    {
        foreach (var (npcId, unlockInts) in packet.Unlocked)
        {
            if (!_known.TryGetValue(npcId, out var set))
            {
                set = _known[npcId] = new HashSet<BestiaryUnlock>();
            }

            foreach (var val in unlockInts)
            {
                var unlock = (BestiaryUnlock)val;
                if (set.Add(unlock))
                {
                    OnUnlockGained?.Invoke(npcId, unlock); // ✅ esto debe disparar Refresh en BeastTile
                }
            }
        }
    }

}
