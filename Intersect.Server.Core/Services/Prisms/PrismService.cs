using System;
using System.Linq;
using System.Threading.Tasks;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Core;
using Intersect.Server.Database.Prisms;
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

    public static PrismRuntime PlacePrism(Player player, MapInstance map)
    {
        var options = Options.Instance.Prism;
        var maxHp = options.BaseHp + options.HpPerLevel * Math.Max(0, player?.Level - 1 ?? 0);
        var now = DateTime.UtcNow;

        var descriptor = new PrismDescriptor
        {
            Id = Guid.NewGuid(),
            MapId = map.MapId,
            X = player?.X ?? 0,
            Y = player?.Y ?? 0,
        };

        var entity = new PrismEntity
        {
            PrismId = descriptor.Id,
            Owner = player.Faction,
            State = PrismState.Placed,
            MaxHp = maxHp,
            Hp = maxHp,
            LastHitAt = null,
            LastStateChangeAt = now,
            CurrentBattleId = null,
        };

        var runtime = new PrismRuntime(descriptor, entity);
        map.ControllingPrism = runtime;
        Logger.LogInformation(
            "Prism {PrismId} placed on map {MapId} by {PlayerId} at {Time}",
            runtime.Id,
            map.MapId,
            player?.Id,
            now
        );
        Broadcast(map);
        return runtime;
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
            PrismCombatService.DiminishContributions(prism);
            foreach (var player in map.GetPlayers())
            {
                if (PrismCombatService.IsOnRespawnCooldown(player, now))
                {
                    player.WarpToSpawn();
                    continue;
                }

                PrismCombatService.RecordPresence(prism, player);
            }
        }

        switch (prism.State)
        {
            case PrismState.Placed:
                // Placed prisms immediately transition based on vulnerability windows
                prism.State = inWindow ? PrismState.Vulnerable : PrismState.Dominated;
                changed = true;
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
                else if (PrismCombatService.GetBattleStart(prism) is DateTime startedAt &&
                         (now - startedAt).TotalMinutes >= Options.Instance.Prism.MaxBattleDurationMinutes)
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
            prism.LastStateChangeAt = now;
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

    public static bool IsInVulnerabilityWindow(PrismRuntime prism, DateTime time)
    {
        if (prism.Windows == null || prism.Windows.Count == 0)
        {
            return true;
        }

        return prism.Windows.Any(window => window.Contains(time));
    }

    public static DateTime? GetNextVulnerabilityStart(PrismRuntime prism, DateTime now)
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
