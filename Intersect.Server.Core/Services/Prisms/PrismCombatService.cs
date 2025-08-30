using System;
using System.Collections.Concurrent;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;

namespace Intersect.Server.Services.Prisms;

internal static class PrismCombatService
{
    // Tracks damage dealt by players to prisms within the current tick window.
    private static readonly ConcurrentDictionary<(Guid PrismId, Guid AttackerId), (DateTime TickStart, int Damage)>
        DamageTracker = new();

    public static void ApplyDamage(MapInstance map, AlignmentPrism prism, int amount, Player attacker)
    {
        if (prism == null || amount <= 0 || attacker == null)
        {
            return;
        }

        // Attacker must have wings enabled and belong to a different faction than the prism owner.
        if (attacker.Wings != WingState.On || attacker.Faction == prism.Owner)
        {
            return;
        }

        var now = DateTime.UtcNow;

        // Attacks are only allowed within the vulnerability window or during the under attack timeout.
        var stillUnderAttack = prism.State == PrismState.UnderAttack && prism.LastHitAt.HasValue &&
                               (now - prism.LastHitAt.Value).TotalSeconds <= Options.Instance.Prism.AttackCooldownSeconds;

        if (!PrismService.IsInVulnerabilityWindow(prism, now) && !stillUnderAttack)
        {
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
        }

        if (prism.Hp <= 0)
        {
            prism.State = PrismState.Destroyed;
        }

        PrismService.Broadcast(map);
    }
}
