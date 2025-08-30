using System;
using System.Collections.Generic;
using System.Threading;
using Intersect.Config;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Services.Prisms;

namespace Intersect.Server.Jobs;

internal static class PrismScheduler
{
    private static Timer? _timer;

    public static void Start()
    {
        var interval = TimeSpan.FromSeconds(Options.Instance.Prism.SchedulerIntervalSeconds);
        _timer ??= new Timer(Tick, null, interval, interval);
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
