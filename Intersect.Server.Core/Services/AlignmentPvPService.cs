using System;
using Intersect.Enums;
using Intersect.Server.Entities;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Core.Services;

namespace Intersect.Server.Services;

internal static class AlignmentPvPService
{
    private const int HonorReward = 10;
    private const int FactionPenalty = 5;
    private const int NeutralPenalty = 10;
    private const int UnfairLevelPenalty = 5;
    private static readonly TimeSpan RewardCooldown = TimeSpan.FromMinutes(5);

    public static (bool ok, string reason) CanEngage(Player attacker, Player victim)
    {
        if (attacker == null || victim == null)
        {
            return (false, "");
        }

        if (attacker == victim)
        {
            return (false, "No puedes atacarte a ti mismo.");
        }

        if (attacker.Honor < 0 || victim.Honor < 0)
        {
            return (true, string.Empty);
        }

        if (attacker.Wings != WingState.On)
        {
            return (false, "Debes activar tus alas para combatir.");
        }

        if (victim.Wings != WingState.On)
        {
            return (false, "Ese jugador no tiene las alas activadas.");
        }

        if (attacker.Faction == Factions.Neutral)
        {
            return (false, "Debes pertenecer a una facción para combatir.");
        }

        if (victim.Faction == Factions.Neutral)
        {
            return (false, "No puedes atacar jugadores neutrales.");
        }

        if (attacker.Faction == victim.Faction)
        {
            return (false, "No puedes atacar a miembros de tu facción.");
        }

        return (true, string.Empty);
    }

    public static void HandleKill(Player killer, Player victim)
    {
        if (killer == null || victim == null || killer == victim)
        {
            return;
        }

        // Prevent honor gains if both players share the same account or IP address.
        var killerIp = killer.User?.LastIp;
        var victimIp = victim.User?.LastIp;
        if (killer.UserId == victim.UserId || (!string.IsNullOrEmpty(killerIp) && killerIp == victimIp))
        {
            return;
        }

        var now = DateTime.UtcNow;

        foreach (var entry in killer.RecentPlayerVictims.ToArray())
        {
            if (now - entry.Value > RewardCooldown)
            {
                killer.RecentPlayerVictims.TryRemove(entry.Key, out _);
            }
        }

        if (victim.Faction == Factions.Neutral)
        {
            HonorService.AdjustHonor(killer, -NeutralPenalty);
            return;
        }

        if (killer.Faction == victim.Faction)
        {
            HonorService.AdjustHonor(killer, -FactionPenalty);
            return;
        }
        var levelDiff = Math.Abs(killer.Level - victim.Level);
        var maxLevel = Math.Max(killer.Level, victim.Level);
        if (levelDiff > maxLevel * 0.3f)
        {
            if (killer.Level > victim.Level)
            {
                HonorService.AdjustHonor(killer, -UnfairLevelPenalty);
            }

            return;
        }

        var honorDelta = HonorReward;
        if (killer.RecentPlayerVictims.TryGetValue(victim.Id, out var lastKill) &&
            now - lastKill < RewardCooldown)
        {
            honorDelta /= 2;
        }

        HonorService.AdjustHonor(killer, honorDelta);
        HonorService.AdjustHonor(victim, -honorDelta);

        killer.RecentPlayerVictims[victim.Id] = now;

        try
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.Player_KillLogs.Add(new KillLog(killer, victim));
            context.SaveChanges();
        }
        catch
        {
            // ignore logging failures
        }
    }
}
