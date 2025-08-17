using System;
using System.Collections.Generic;
using Intersect.Client.Interface;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.Controllers;

/// <summary>
///     Tracks kill counts and unlock progress for the bestiary on the client.
///     Refreshes state from the server and exposes helpers for UI queries.
/// </summary>
public static class BestiaryController
{
    private static readonly Dictionary<Guid, int> Kills = new();
    private static readonly Dictionary<Guid, Dictionary<BestiaryUnlock, int>> Values = new();

    /// <summary>
    ///     Gets a read-only view of NPC kill counts.
    /// </summary>
    public static IReadOnlyDictionary<Guid, int> KillCounts => Kills;

    /// <summary>
    ///     Updates the cached kill counts and unlock values from the server, displaying a toast when
    ///     an unlock threshold is crossed.
    /// </summary>
    /// <param name="unlocks">Dictionary of NPC IDs to unlock type/value pairs.</param>
    public static void SetUnlocked(Dictionary<Guid, Dictionary<BestiaryUnlock, int>> unlocks)
    {
        // Keep a copy of the existing values so we can detect newly unlocked entries.
        var previous = new Dictionary<Guid, Dictionary<BestiaryUnlock, int>>();
        foreach (var (npcId, dict) in Values)
        {
            previous[npcId] = new Dictionary<BestiaryUnlock, int>(dict);
        }

        Values.Clear();
        Kills.Clear();

        foreach (var (npcId, dict) in unlocks)
        {
            var current = Values[npcId] = new Dictionary<BestiaryUnlock, int>();
            foreach (var (unlockType, value) in dict)
            {
                current[unlockType] = value;
                if (unlockType == BestiaryUnlock.Kill)
                {
                    Kills[npcId] = value;
                }

                var wasUnlocked = previous.TryGetValue(npcId, out var prevDict)
                                   && prevDict.TryGetValue(unlockType, out var prevValue)
                                   && IsUnlocked(npcId, unlockType, prevValue);

                var isUnlocked = IsUnlocked(npcId, unlockType, value);

                if (!wasUnlocked && isUnlocked)
                {
                    var npc = NPCDescriptor.Get(npcId);
                    if (npc != null)
                    {
                        Interface.Interface.EnqueueInGame(
                            gi => gi.AnnouncementWindow.ShowAnnouncement(
                                $"Bestiary unlocked: {npc.Name}",
                                3000
                            )
                        );
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Determines whether the specified NPC has the given unlock type.
    /// </summary>
    public static bool HasUnlock(Guid npcId, BestiaryUnlock type)
    {
        if (!Values.TryGetValue(npcId, out var dict))
        {
            return false;
        }

        if (!dict.TryGetValue(type, out var value))
        {
            return false;
        }

        return IsUnlocked(npcId, type, value);
    }

    private static bool IsUnlocked(Guid npcId, BestiaryUnlock type, int value)
    {
        var descriptor = NPCDescriptor.Get(npcId);
        if (descriptor == null)
        {
            return false;
        }

        if (!descriptor.BestiaryUnlocks.TryGetValue(type, out var required))
        {
            return false;
        }

        return value >= required;
    }
}

