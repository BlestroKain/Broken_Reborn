using System;
using System.Threading;
using System.Threading.Tasks;
using Intersect.Enums;
using Intersect.Config;
using Intersect.Server.Entities;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Networking;

namespace Intersect.Server.Services;

public sealed class AlignmentService
{
    private readonly IPlayerRepository _repository;

    public AlignmentService(IPlayerRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool ok, string? reason, DateTime? nextAllowed)> TrySetAlignment(
        Player player,
        Alignment desired,
        CancellationToken cancellationToken = default
    )
    {
        var cooldown = Options.Instance.Prism.AlignmentSwapCooldown;
        var now = DateTime.UtcNow;
        var nextAllowed = player.LastFactionSwapAt + cooldown;

        if (player.Alignment == desired)
        {
            return (false, "same", nextAllowed);
        }

        var guildFaction = player.GuildFaction;
        if (guildFaction != Alignment.Neutral && desired != Alignment.Neutral && desired != guildFaction)
        {
            return (false, "guild", nextAllowed);
        }

        if (now < nextAllowed)
        {
            return (false, "cooldown", nextAllowed);
        }

        if (player.Wings == WingState.On)
        {
            return (false, "wings", nextAllowed);
        }

        player.Alignment = desired;
        player.LastFactionSwapAt = now;
        if (desired == Alignment.Neutral && player.Wings == WingState.On)
        {
            player.Wings = WingState.Off;
        }

        _repository.Players.Update(player);
        await _repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        PacketSender.SendEntityDataToProximity(player);

        return (true, null, now + cooldown);
    }
}

