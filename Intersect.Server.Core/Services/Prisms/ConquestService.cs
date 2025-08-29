using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Core.Services;
using Intersect.Server.Database.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Services.Prisms;

/// <summary>
/// Handles capturing and destroying of prisms and any related bonuses.
/// </summary>
public sealed class ConquestService : IConquestService
{
    private readonly IPrismRepository _repository;
    private readonly IFactionBonusApplier _bonusApplier;

    public ConquestService(IPrismRepository repository, IFactionBonusApplier bonusApplier)
    {
        _repository = repository;
        _bonusApplier = bonusApplier;
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

        var bonus = await _repository.FactionAreaBonuses
            .FirstOrDefaultAsync(b => b.PrismId == prism.Id, cancellationToken)
            .ConfigureAwait(false);

        if (bonus != null)
        {
            _bonusApplier.ClearBonus(prism.Id);
            _repository.FactionAreaBonuses.Remove(bonus);
        }

        map.ControllingPrism = null;
        _repository.Prisms.Remove(prism);
        await _repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (destroyer != null)
        {
            HonorService.AdjustHonor(destroyer, 25);
        }
    }
}

