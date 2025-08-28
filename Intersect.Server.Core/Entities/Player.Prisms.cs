using System;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Server.Entities;

/// <summary>
/// Prism-related actions that a player can perform.
/// </summary>
public partial class Player
{
    /// <summary>
    /// Invoked when the player places a prism.
    /// </summary>
    public event Action<Player, PrismDescriptor>? PrismPlaced;

    /// <summary>
    /// Invoked when the player attacks a prism.
    /// </summary>
    public event Action<Player, PrismDescriptor>? PrismAttacked;

    /// <summary>
    /// Places a new prism and notifies listeners.
    /// </summary>
    public void PlacePrism()
    {
        var prism = new PrismDescriptor { OwnerId = Id, State = PrismState.Placed };
        PrismPlaced?.Invoke(this, prism);
    }

    /// <summary>
    /// Notifies that the player attacked a prism. Additional honor is awarded.
    /// </summary>
    public void AttackPrism(PrismDescriptor prism)
    {
        prism.State = PrismState.UnderAttack;
        PrismAttacked?.Invoke(this, prism);
        Honor += 5; // bonus honor for participating in prism combat
    }

    /// <summary>
    /// Marks a prism as dominated after a successful defense.
    /// </summary>
    public void DominatePrism(PrismDescriptor prism)
    {
        prism.State = PrismState.Dominated;
        Honor += 10; // extra honor for dominating a prism
    }
}
