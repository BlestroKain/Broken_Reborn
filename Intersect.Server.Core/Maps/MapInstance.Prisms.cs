using System;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Database.Prisms;
using Intersect.Server.Services.Prisms;

namespace Intersect.Server.Maps;

/// <summary>
/// Prism tracking for map instances. A prism confers a small honor bonus to
/// players of the owning faction while they remain on the map.
/// </summary>
public partial class MapInstance
{
    /// <summary>
    /// Prism currently controlling this map instance, if any.
    /// </summary>
    public PrismRuntime? ControllingPrism { get; internal set; }

    /// <summary>
    /// True if the controlling prism is under attack.
    /// </summary>
    public bool PrismUnderAttack => ControllingPrism?.State == PrismState.UnderAttack;

    /// <summary>
    /// True if the controlling prism is dominated.
    /// </summary>
    public bool PrismDominated => ControllingPrism?.State == PrismState.Dominated;

    /// <summary>
    /// Places a prism on this map instance.
    /// </summary>
    public void PlacePrism(Player player)
    {
        var descriptor = new PrismDescriptor
        {
            Id = Guid.NewGuid(),
            MapId = MapId,
            X = player.X,
            Y = player.Y,
        };

        var entity = new PrismEntity
        {
            PrismId = descriptor.Id,
            Owner = player.Faction,
            State = PrismState.Placed,
            MaxHp = 1,
            Hp = 1,
            LastStateChangeAt = DateTime.UtcNow,
        };

        ControllingPrism = new PrismRuntime(descriptor, entity);

        player.Honor += 10; // bonus honor for placing a prism
    }

    /// <summary>
    /// Applies area bonus to players standing on the map.
    /// </summary>
    public void ApplyPrismBonus(Player player)
    {
        if (ControllingPrism != null && ControllingPrism.Owner == player.Faction)
        {
            if (ControllingPrism.State == PrismState.UnderAttack)
            {
                return; // no bonus while the prism is contested
            }

            var bonus = ControllingPrism.State == PrismState.Dominated ? 2 : 1;
            player.Honor += bonus;
        }
    }
}
