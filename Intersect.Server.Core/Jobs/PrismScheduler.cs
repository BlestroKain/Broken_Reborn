using System;
using System.Collections.Generic;
using System.Threading;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Services.Prisms;

namespace Intersect.Server.Jobs;

internal static class PrismScheduler
{
    private static Timer? _timer;
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(1);

    public static void Start()
    {
        _timer ??= new Timer(Tick, null, Interval, Interval);
    }

    private static void Tick(object? state)
    {
        var visited = new HashSet<Guid>();
        foreach (var player in Player.OnlinePlayers)
        {
            if (player == null || visited.Contains(player.MapInstanceId))
            {
                continue;
            }

            visited.Add(player.MapInstanceId);

            if (!MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                continue;
            }

            PrismService.TickState(mapInstance);
        }
    }
}
