using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Database;
using Intersect.Server.Database.Prisms;
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

    // Tracks active battles and their contributions.
    private static readonly ConcurrentDictionary<Guid, PrismBattle> Battles = new();

    internal sealed class Contribution
    {
        public Guid PlayerId { get; init; }
        public Guid PlayerUserId { get; init; }
        public string PlayerIp { get; init; }
        public string PlayerFingerprint { get; init; }
        public Alignment Faction { get; init; }

        public int Damage;
        public int Presence;
        public int Heals;

        public int Total => Damage + Presence + Heals;
    }

    private static readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, Contribution>> BattleContributions = new();

    // Tracks death timestamps to enforce respawn cooldown.
    private static readonly ConcurrentDictionary<Guid, DateTime> DeathTimestamps = new();

    private const double ContributionDecayFactor = 0.99;

    private static int _activeBattles;

    public static int ActiveBattles => _activeBattles;

    private static void RecordActiveBattles()
    {
        if (Options.Instance.Metrics.Enable)
        {
            MetricsRoot.Instance.Game.ActivePrismBattles.Record(_activeBattles);
        }
    }

    public static IEnumerable<Contribution> GetContributions(AlignmentPrism prism)
    {
        if (prism?.CurrentBattleId == null)
        {
            return Array.Empty<Contribution>();
        }

        return BattleContributions.TryGetValue(prism.CurrentBattleId.Value, out var dict)
            ? dict.Values
            : Array.Empty<Contribution>();
    }

    public static DateTime? GetBattleStart(AlignmentPrism prism)
    {
        if (prism?.CurrentBattleId == null)
        {
            return null;
        }

        return Battles.TryGetValue(prism.CurrentBattleId.Value, out var battle) ? battle.StartedAt : null;
    }

    public static void DiminishContributions(AlignmentPrism prism)
    {
        if (prism?.CurrentBattleId == null)
        {
            return;
        }

        if (BattleContributions.TryGetValue(prism.CurrentBattleId.Value, out var dict))
        {
            foreach (var contrib in dict.Values)
            {
                contrib.Damage = (int)(contrib.Damage * ContributionDecayFactor);
                contrib.Presence = (int)(contrib.Presence * ContributionDecayFactor);
                contrib.Heals = (int)(contrib.Heals * ContributionDecayFactor);
            }
        }
    }

    public static void RecordDeath(AlignmentPrism prism, Player player)
    {
        if (prism?.State != PrismState.UnderAttack || player == null)
        {
            return;
        }

        DeathTimestamps[player.Id] = DateTime.UtcNow;
    }

    public static bool IsOnRespawnCooldown(Player player, DateTime now)
    {
        if (player == null)
        {
            return false;
        }

        return DeathTimestamps.TryGetValue(player.Id, out var time) &&
               (now - time).TotalSeconds < Options.Instance.Prism.RespawnCooldownSeconds;
    }

    internal static void BattleEnded(AlignmentPrism prism)
    {
        if (prism?.CurrentBattleId == null)
        {
            return;
        }

        var now = DateTime.UtcNow;
        var battleId = prism.CurrentBattleId.Value;
        if (!Battles.TryRemove(battleId, out var battle))
        {
            battle = new PrismBattle { Id = battleId, PrismId = prism.Id, StartedAt = now };
        }

        battle.EndedAt = now;

        BattleContributions.TryRemove(battleId, out var contributions);

        using (var context = DbInterface.CreatePlayerContext(readOnly: false))
        {
            context.PrismBattles.Add(battle);
            if (contributions != null)
            {
                foreach (var contrib in contributions.Values)
                {
                    context.PrismContributions.Add(
                        new PrismContribution
                        {
                            BattleId = battle.Id,
                            PlayerId = contrib.PlayerId,
                            PlayerUserId = contrib.PlayerUserId,
                            PlayerIp = contrib.PlayerIp,
                            PlayerFingerprint = contrib.PlayerFingerprint,
                            Contribution = contrib.Total,
                        }
                    );
                }
            }

            context.SaveChanges();
        }

        prism.CurrentBattleId = null;

        var battles = Interlocked.Decrement(ref _activeBattles);
        RecordActiveBattles();
        Logger.LogInformation(
            "Prism battle ended. Active battles: {Count} at {Time}",
            battles,
            now
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

        if (IsOnRespawnCooldown(attacker, now))
        {
            Logger.LogInformation(
                "Ignored attack on prism {PrismId} by {AttackerId} at {Time}: respawn cooldown",
                prism.Id,
                attacker.Id,
                now
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
            Battles.TryAdd(
                prism.CurrentBattleId.Value,
                new PrismBattle { Id = prism.CurrentBattleId.Value, PrismId = prism.Id, StartedAt = now }
            );
            BattleContributions.TryAdd(prism.CurrentBattleId.Value, new());
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

        if (prism.CurrentBattleId != null)
        {
            var contributions = BattleContributions.GetOrAdd(prism.CurrentBattleId.Value, _ => new());
            var contrib = contributions.GetOrAdd(
                attacker.Id,
                id => new Contribution
                {
                    PlayerId = id,
                    PlayerUserId = attacker.UserId,
                    PlayerIp = attacker.Client?.Ip,
                    PlayerFingerprint = attacker.Client?.Ip,
                    Faction = attacker.Faction,
                }
            );

            contrib.Damage += amount;
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

    public static void RecordPresence(AlignmentPrism prism, Player player, int amount = 1)
    {
        if (prism?.CurrentBattleId == null || player == null || amount <= 0)
        {
            return;
        }

        var contributions = BattleContributions.GetOrAdd(prism.CurrentBattleId.Value, _ => new());
        var contrib = contributions.GetOrAdd(
            player.Id,
            id => new Contribution
            {
                PlayerId = id,
                PlayerUserId = player.UserId,
                PlayerIp = player.Client?.Ip,
                PlayerFingerprint = player.Client?.Ip,
                Faction = player.Faction,
            }
        );

        contrib.Presence += amount;
    }

    public static void RecordHealing(AlignmentPrism prism, Player healer, int amount)
    {
        if (prism?.CurrentBattleId == null || healer == null || amount <= 0)
        {
            return;
        }

        var contributions = BattleContributions.GetOrAdd(prism.CurrentBattleId.Value, _ => new());
        var contrib = contributions.GetOrAdd(
            healer.Id,
            id => new Contribution
            {
                PlayerId = id,
                PlayerUserId = healer.UserId,
                PlayerIp = healer.Client?.Ip,
                PlayerFingerprint = healer.Client?.Ip,
                Faction = healer.Faction,
            }
        );

        contrib.Heals += amount;
    }
}
