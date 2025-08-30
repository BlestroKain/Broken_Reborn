using System;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Localization;

namespace Intersect.Server.Services;

public static class AlignmentService
{
    public readonly record struct Result(
        bool Success,
        string? Message,
        Alignment NewAlignment,
        DateTime? NextAllowedChangeAt
    );

    public static Result TrySetAlignment(
        Player p,
        Alignment desired,
        AlignmentApplyOptions? apply = null
    )
    {
        apply ??= new AlignmentApplyOptions();

        var cooldown = TimeSpan.FromDays(Options.Instance.Alignment.SwapCooldownDays);
        var now = DateTime.UtcNow;
        var nextAllowed = p.LastFactionSwapAt + cooldown;

        if (p.Faction == desired)
        {
            return new Result(false, "same", p.Faction, nextAllowed);
        }

        var guildFaction = p.Guild?.GetFaction() ?? Alignment.Neutral;
        if (!apply.IgnoreGuildLock && guildFaction != Alignment.Neutral && desired != Alignment.Neutral && guildFaction != desired)
        {
            return new Result(false, "guild", p.Faction, nextAllowed);
        }

        if (p.Wings == WingState.On)
        {
            return new Result(false, "wings", p.Faction, nextAllowed);
        }

        if (!apply.IgnoreCooldown && now < nextAllowed)
        {
            return new Result(false, "cooldown", p.Faction, nextAllowed);
        }

        p.Faction = desired;
        p.LastFactionSwapAt = now;
        if (desired == Alignment.Neutral)
        {
            p.Wings = WingState.Off;
        }

        using var context = DbInterface.CreatePlayerContext(readOnly: false);
        var _players = context.Players;
        _players.Update(p);
        context.SaveChanges();

        return new Result(true, null, p.Faction, now + cooldown);
    }

    public static string? GetMessage(string code, DateTime? nextAllowed)
    {
        return code switch
        {
            "same" => Strings.Alignment.AlreadyInFaction.ToString(),
            "guild" => Strings.Alignment.GuildMismatch.ToString(),
            "wings" => Strings.Alignment.WingsOn.ToString(),
            "cooldown" when nextAllowed.HasValue =>
                Strings.Alignment.SwapCooldown.ToString()
                    .Replace("{fecha}", nextAllowed.Value.ToLocalTime().ToString("g")),
            _ => null,
        };
    }
}

