using System;
using System.Collections.Concurrent;
using System.Threading;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Metrics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Intersect.Server.Services.Prisms;

internal static class PrismCombatService
{
    public static ILogger Logger { get; set; } = NullLogger.Instance;

    // Tracks damage dealt by players to prisms within the current tick window.
    private static readonly ConcurrentDictionary<(Guid PrismId, Guid AttackerId), (DateTime TickStart, int Damage)>
        DamageTracker = new();

    private static int _activeBattles;

    public static int ActiveBattles => _activeBattles;

    private static void RecordActiveBattles()
    {
        if (Options.Instance.Metrics.Enable)
        {
            MetricsRoot.Instance.Game.ActivePrismBattles.Record(_activeBattles);
        }
    }

    internal static void BattleEnded()
    {
        var battles = Interlocked.Decrement(ref _activeBattles);
        RecordActiveBattles();
        Logger.LogInformation(
            "Prism battle ended. Active battles: {Count} at {Time}",
            battles,
            DateTime.UtcNow
        );
    }

    public static bool CanDamage(AlignmentPrism prism, DateTime now)
    {
        if (Options.Instance.Prism.AllowDamageOutsideVulnerability)
        {
            return true;
        }

        var stillUnderAttack = prism.State == PrismState.UnderAttack && prism.LastHitAt.HasValue &&
                               (now - prism.LastHitAt.Value).TotalSeconds <= Options.Instance.Prism.AttackCooldownSeconds;

        return PrismService.IsInVulnerabilityWindow(prism, now) || stillUnderAttack;
    }

    public static void ApplyDamage(MapInstance map, AlignmentPrism prism, int amount, Player attacker)
    {
        var now = DateTime.UtcNow;

        if (prism == null || amount <= 0 || attacker == null)
        {
            Logger.LogDebug(
                "Ignored attack at {Time} due to invalid arguments (prism={PrismId}, attacker={AttackerId})",
                now,
                prism?.Id,
                attacker?.Id
            );
            return;
        }

        // Attacker must have wings enabled and belong to a different faction than the prism owner.
        if (attacker.Wings != WingState.On || attacker.Faction == prism.Owner)
        {
            Logger.LogInformation(
                "Ignored attack on prism {PrismId} by {AttackerId} at {Time}: invalid state",
                prism.Id,
                attacker.Id,
                now
            );
            return;
        }

        // Attacks are only allowed within the vulnerability window or during the under attack timeout.
        if (!CanDamage(prism, now))
        {
            Logger.LogInformation(
                "Ignored attack on prism {PrismId} by {AttackerId} at {Time}: outside vulnerability window",
                prism.Id,
                attacker.Id,
                now
            );
            return;
        }

        // Apply diminishing returns on repeated hits by the same player.
        var key = (prism.Id, attacker.Id);
        var tickDuration = TimeSpan.FromSeconds(Options.Instance.Prism.SchedulerIntervalSeconds);
        var record = DamageTracker.GetOrAdd(key, _ => (now, 0));

        if (now - record.TickStart > tickDuration)
        {
            record = (now, 0);
        }

        var remaining = Options.Instance.Prism.DamageCapPerTick - record.Damage;
        if (remaining <= 0)
        {
            Logger.LogInformation(
                "Ignored attack on prism {PrismId} by {AttackerId} at {Time}: damage cap reached",
                prism.Id,
                attacker.Id,
                now
            );
            return;
        }

        amount = Math.Min(amount, remaining);
        DamageTracker[key] = (record.TickStart, record.Damage + amount);

        prism.Hp = Math.Max(0, prism.Hp - amount);
        prism.LastHitAt = now;

        if (prism.State != PrismState.UnderAttack)
        {
            prism.State = PrismState.UnderAttack;
            prism.CurrentBattleId ??= Guid.NewGuid();
            var battles = Interlocked.Increment(ref _activeBattles);
            Logger.LogInformation(
                "Prism {PrismId} entered battle {BattleId} due to attack by {AttackerId} at {Time}",
                prism.Id,
                prism.CurrentBattleId,
                attacker.Id,
                now
            );
            RecordActiveBattles();
        }

        Logger.LogInformation(
            "Prism {PrismId} took {Damage} damage from {AttackerId} at {Time}. HP: {Hp}",
            prism.Id,
            amount,
            attacker.Id,
            now,
            prism.Hp
        );

        if (prism.Hp <= 0)
        {
            prism.State = PrismState.Destroyed;
            Logger.LogInformation(
                "Prism {PrismId} destroyed during battle {BattleId} at {Time}",
                prism.Id,
                prism.CurrentBattleId,
                now
            );
        }

        PrismService.Broadcast(map);
    }
}
