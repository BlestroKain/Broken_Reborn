using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.General;

public static class BestiaryController
{
    private static readonly Dictionary<Guid, Dictionary<BestiaryUnlock, int>> _unlocked = new();

    public static IReadOnlyDictionary<Guid, Dictionary<BestiaryUnlock, int>> Unlocked => _unlocked;

    public static void SetUnlocked(Dictionary<Guid, Dictionary<BestiaryUnlock, int>> unlocks)
    {
        _unlocked.Clear();
        foreach (var pair in unlocks)
        {
            _unlocked[pair.Key] = pair.Value;
        }
    }
}

