using System;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;

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
    public AlignmentPrism? ControllingPrism { get; internal set; }

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
        ControllingPrism = new AlignmentPrism
        {
            Id = Guid.NewGuid(),
            Owner = player.Faction,
            State = PrismState.Placed,
            MapId = MapId,
            PlacedAt = DateTime.UtcNow,
            X = player.X,
            Y = player.Y,
            Level = 1,
            MaxHp = 1,
            Hp = 1,
        };

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
