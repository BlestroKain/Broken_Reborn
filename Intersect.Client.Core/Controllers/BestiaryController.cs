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

    public static IReadOnlyDictionary<Guid, int> KillCounts => _killCounts;
    private static readonly Dictionary<Guid, int> _killCounts = new();

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

    public static int GetKillCount(Guid npcId)
        => _killCounts.TryGetValue(npcId, out var c) ? c : 0;

    public static void ApplyPacket(UnlockedBestiaryEntriesPacket packet)
    {
        var packetNpcIds = packet.Unlocked.Keys.ToHashSet();

        foreach (var npcId in _known.Keys.Except(packetNpcIds).ToList())
        {
            _known.Remove(npcId);
            OnUnlockGained?.Invoke(npcId, BestiaryUnlock.Kill);
        }

        foreach (var (npcId, unlockInts) in packet.Unlocked)
        {
            if (!_known.TryGetValue(npcId, out var set))
            {
                set = _known[npcId] = new HashSet<BestiaryUnlock>();
            }

            var incoming = unlockInts.Select(i => (BestiaryUnlock)i).ToHashSet();

            var removed = set.Except(incoming).ToList();
            if (removed.Any())
            {
                set.IntersectWith(incoming);
                foreach (var unlock in removed)
                {
                    OnUnlockGained?.Invoke(npcId, unlock);
                }
            }

            foreach (var unlock in incoming)
            {
                if (set.Add(unlock))
                {
                    OnUnlockGained?.Invoke(npcId, unlock); // ✅ esto debe disparar Refresh en BeastTile
                }
            }
        }

        var killIds = packet.KillCounts.Keys.ToHashSet();

        foreach (var npcId in _killCounts.Keys.Except(killIds).ToList())
        {
            _killCounts.Remove(npcId);
            OnUnlockGained?.Invoke(npcId, BestiaryUnlock.Kill);
        }

        foreach (var (npcId, count) in packet.KillCounts)
        {
            if (!_killCounts.TryGetValue(npcId, out var prev) || prev != count)
            {
                _killCounts[npcId] = count;
                OnUnlockGained?.Invoke(npcId, BestiaryUnlock.Kill);
            }
        }
    }

}
