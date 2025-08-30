using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Config;
using Intersect.Server.Core.Services;
using Intersect.Server.Database.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Microsoft.EntityFrameworkCore;
using Intersect.Server.Metrics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Intersect.Server.Services.Prisms;

/// <summary>
/// Handles capturing and destroying of prisms and any related bonuses.
/// </summary>
public sealed class ConquestService : IConquestService
{
    private readonly IPrismRepository _repository;
    private readonly IFactionBonusApplier _bonusApplier;
    private readonly ILogger<ConquestService> _logger;
    private static long _captureCount;

    public ConquestService(
        IPrismRepository repository,
        IFactionBonusApplier bonusApplier,
        ILogger<ConquestService> logger
    )
    {
        _repository = repository;
        _bonusApplier = bonusApplier;
        _logger = logger ?? NullLogger<ConquestService>.Instance;
    }

    /// <summary>
    /// Captures the prism for the captor's faction and applies any area bonuses.
    /// </summary>
    public async Task CaptureAsync(MapInstance map, Player captor, CancellationToken cancellationToken = default)
    {
        if (map?.ControllingPrism == null || captor == null)
        {
            return;
        }

        var prism = map.ControllingPrism;
        prism.Owner = captor.Faction;
        prism.State = PrismState.Dominated;
        var now = DateTime.UtcNow;
        _logger.LogInformation(
            "Prism {PrismId} captured by {CaptorId} at {Time}",
            prism.Id,
            captor.Id,
            now
        );
        var captures = Interlocked.Increment(ref _captureCount);
        if (Options.Instance.Metrics.Enable)
        {
            MetricsRoot.Instance.Game.PrismCaptures.Record(captures);
        }

        var bonus = await _repository.FactionAreaBonuses
            .FirstOrDefaultAsync(b => b.PrismId == prism.Id, cancellationToken)
            .ConfigureAwait(false);

        if (bonus == null)
        {
            bonus = new FactionAreaBonus
            {
                PrismId = prism.Id,
                Faction = (int)captor.Faction,
                Bonus = 0,
            };
            _repository.FactionAreaBonuses.Add(bonus);
        }
        else
        {
            _bonusApplier.ClearBonus(prism.Id);
            bonus.Faction = (int)captor.Faction;
        }

        await _repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        _bonusApplier.ApplyBonus(bonus);

        // Award honor for capturing the prism
        HonorService.AdjustHonor(captor, 50);
    }

    /// <summary>
    /// Destroys the prism removing any associated bonuses.
    /// </summary>
    public async Task DestroyAsync(MapInstance map, Player destroyer, CancellationToken cancellationToken = default)
    {
        if (map?.ControllingPrism == null)
        {
            return;
        }

        var prism = map.ControllingPrism;
        var now = DateTime.UtcNow;

        var bonus = await _repository.FactionAreaBonuses
            .FirstOrDefaultAsync(b => b.PrismId == prism.Id, cancellationToken)
            .ConfigureAwait(false);

        if (bonus != null)
        {
            _bonusApplier.ClearBonus(prism.Id);
            _repository.FactionAreaBonuses.Remove(bonus);
        }

        var dbPrism = await _repository.Prisms
            .FindAsync(new object[] { prism.Id }, cancellationToken)
            .ConfigureAwait(false);

        if (dbPrism != null)
        {
            _repository.Prisms.Remove(dbPrism);
        }

        map.ControllingPrism = null;
        await _repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation(
            "Prism {PrismId} destroyed by {DestroyerId} at {Time}",
            prism.Id,
            destroyer?.Id,
            now
        );

        if (destroyer != null)
        {
            HonorService.AdjustHonor(destroyer, 25);
        }
    }
}

