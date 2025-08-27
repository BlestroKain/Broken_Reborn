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
    public PrismDescriptor? ControllingPrism { get; private set; }

    /// <summary>
    /// Places a prism on this map instance.
    /// </summary>
    public void PlacePrism(Player player)
    {
        ControllingPrism = new PrismDescriptor { OwnerId = player.Id, State = PrismState.Placed };
        player.Honor += 10; // bonus honor for placing a prism
    }

    /// <summary>
    /// Applies area bonus to players standing on the map.
    /// </summary>
    public void ApplyPrismBonus(Player player)
    {
        if (ControllingPrism != null && ControllingPrism.OwnerId == player.Id)
        {
            // simple honor tick to show the bonus mechanism
            player.Honor += 1;
        }
    }
}
