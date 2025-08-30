using System;
using System.Collections.Concurrent;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Database.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;

namespace Intersect.Server.Services.Prisms;

/// <summary>
/// Simple in-memory implementation of <see cref="IFactionBonusApplier"/> that tracks
/// <see cref="FactionAreaBonus"/> entries and applies them to calculations.
/// </summary>
public sealed class FactionAreaBonusApplier : IFactionBonusApplier
{
    private readonly ConcurrentDictionary<Guid, FactionAreaBonus> _bonuses = new();

    public float ApplyDropBonus(Player player, float value)
    {
        return Apply(player, value, PrismModuleType.Prospecting);
    }

    public float ApplyGatherBonus(Player player, float value)
    {
        return Apply(player, value);
    }

    public float ApplyCraftBonus(Player player, float value)
    {
        return Apply(player, value, PrismModuleType.Crafting);
    }

    public void ClearBonus(Guid prismId)
    {
        _bonuses.TryRemove(prismId, out _);
    }

    public void ApplyBonus(FactionAreaBonus bonus)
    {
        _bonuses[bonus.PrismId] = bonus;
    }

    private float Apply(Player player, float value, PrismModuleType? moduleType = null)
    {
        if (player == null)
        {
            return value;
        }

        if (!MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var instance))
        {
            return value;
        }

        var prism = instance.ControllingPrism;
        if (prism == null)
        {
            return value;
        }

        if (!_bonuses.TryGetValue(prism.Id, out var bonus))
        {
            return value;
        }

        if (prism.Owner != player.Faction)
        {
            return value;
        }

        if (prism.State != PrismState.Dominated)
        {
            return value;
        }

        var area = prism.Area;
        if (player.X < area.X || player.Y < area.Y || player.X >= area.X + area.Width ||
            player.Y >= area.Y + area.Height)
        {
            return value;
        }

        var bonusValue = bonus.Bonus;

        if (moduleType.HasValue)
        {
            foreach (var module in prism.Modules)
            {
                if (module.Type == moduleType.Value)
                {
                    bonusValue += module.Level * 5;
                }
            }
        }

        return value * (1 + bonusValue / 100f);
    }
}

