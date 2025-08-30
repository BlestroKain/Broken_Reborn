using System;
using System.Linq;
using System.Threading.Tasks;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Core;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Metrics;
using Intersect.Server.Networking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Intersect.Server.Services.Prisms;

internal static class PrismService
{
    public static ILogger Logger { get; set; } = NullLogger.Instance;

    public static AlignmentPrism PlacePrism(Player player, MapInstance map)
    {
        var options = Options.Instance.Prism;
        var maxHp = options.BaseHp + options.HpPerLevel * Math.Max(0, player?.Level - 1 ?? 0);
        var now = DateTime.UtcNow;

        var prism = new AlignmentPrism
        {
            Id = Guid.NewGuid(),
            Owner = player.Faction,
            State = PrismState.Placed,
            MapId = map.MapId,
            X = player?.X ?? 0,
            Y = player?.Y ?? 0,
            PlacedAt = now,
            MaturationEndsAt = now.AddSeconds(options.MaturationSeconds),
            Level = player.Level,
            MaxHp = maxHp,
            Hp = maxHp,
        };

        map.ControllingPrism = prism;
        Logger.LogInformation(
            "Prism {PrismId} placed on map {MapId} by {PlayerId} at {Time}",
            prism.Id,
            map.MapId,
            player?.Id,
            now
        );
        Broadcast(map);
        return prism;
    }

    public static void TickState(MapInstance map)
    {
        var prism = map.ControllingPrism;
        if (prism == null)
        {
            return;
        }

        var now = DateTime.UtcNow;
        var changed = false;
        var previousState = prism.State;
        var inWindow = IsInVulnerabilityWindow(prism, now);

        if (prism.State == PrismState.UnderAttack)
        {
            foreach (var player in map.GetPlayers())
            {
                PrismCombatService.RecordPresence(prism, player);
            }
        }

        switch (prism.State)
        {
            case PrismState.Placed:
                if (prism.MaturationEndsAt.HasValue && now >= prism.MaturationEndsAt.Value)
                {
                    prism.State = inWindow ? PrismState.Vulnerable : PrismState.Dominated;
                    changed = true;
                }

                break;

            case PrismState.Vulnerable:
                if (!inWindow)
                {
                    prism.State = PrismState.Dominated;
                    changed = true;
                }

                break;

            case PrismState.Dominated:
                if (inWindow)
                {
                    prism.State = PrismState.Vulnerable;
                    changed = true;
                }

                break;

            case PrismState.UnderAttack:
                if (prism.Hp <= 0)
                {
                    prism.State = PrismState.Destroyed;
                    changed = true;

                    HandleConquest(map);
                }
                else if (prism.LastHitAt.HasValue &&
                         (now - prism.LastHitAt.Value).TotalSeconds >= Options.Instance.Prism.AttackCooldownSeconds)
                {
                    prism.State = inWindow ? PrismState.Vulnerable : PrismState.Dominated;
                    changed = true;
                }

                break;

            case PrismState.Destroyed:
                HandleConquest(map);
                break;
        }

        if (changed)
        {
            Logger.LogInformation(
                "Prism {PrismId} state changed from {OldState} to {NewState} at {Time}",
                prism.Id,
                previousState,
                prism.State,
                now
            );

            if (previousState == PrismState.UnderAttack && prism.State != PrismState.UnderAttack)
            {
                var conquestService = Bootstrapper.Context?.Services
                    .FirstOrDefault(s => s is IConquestService) as IConquestService;
                _ = conquestService?.DefendAsync(map);
                PrismCombatService.BattleEnded(prism);
            }

            Broadcast(map);
        }
    }

    private static void HandleConquest(MapInstance map)
    {
        var conquestService = Bootstrapper.Context?.Services
            .FirstOrDefault(s => s is IConquestService) as IConquestService;

        if (conquestService == null)
        {
            return;
        }

        var prism = map.ControllingPrism;
        var player = map.GetPlayers().FirstOrDefault(p => p.Faction != prism.Owner);

        if (Options.Instance.Prism.CaptureInsteadOfDestroy)
        {
            _ = conquestService.CaptureAsync(map, player);
        }
        else
        {
            _ = conquestService.DestroyAsync(map, player);
        }
    }

    public static bool IsInVulnerabilityWindow(AlignmentPrism prism, DateTime time)
    {
        if (prism.Windows == null || prism.Windows.Count == 0)
        {
            return true;
        }

        return prism.Windows.Any(window => window.Contains(time));
    }

    public static DateTime? GetNextVulnerabilityStart(AlignmentPrism prism, DateTime now)
    {
        if (prism.Windows == null || prism.Windows.Count == 0)
        {
            return null;
        }

        DateTime? next = null;
        foreach (var window in prism.Windows)
        {
            var daysUntil = ((int)window.Day - (int)now.DayOfWeek + 7) % 7;
            var candidate = now.Date.AddDays(daysUntil).Add(window.Start);

            if (candidate <= now)
            {
                candidate = candidate.AddDays(7);
            }

            if (next == null || candidate < next)
            {
                next = candidate;
            }
        }

        return next;
    }

    public static void Broadcast(MapInstance map)
    {
        foreach (var player in map.GetPlayers())
        {
            PacketSender.SendAreaPacket(player);
        }
    }
}
