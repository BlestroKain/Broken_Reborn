using Intersect.Server.Entities;

namespace Intersect.Server.Core.Services;

/// <summary>
/// Utility service for set updates.
/// Notifies connected clients and clears server-side caches
/// whenever a set is modified, typically after rename or deletion.
/// </summary>
public static class SetService
{
    /// <summary>
    /// Invalidate set bonus caches for all online players so that
    /// they refresh any information related to sets.
    /// </summary>
    public static void NotifyClientsAndClearCaches()
    {
        foreach (var player in Player.OnlinePlayers)
        {
            player.InvalidateSetBonuses();
        }
    }
}

