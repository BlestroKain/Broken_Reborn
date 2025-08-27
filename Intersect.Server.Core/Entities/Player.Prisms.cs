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
        PrismAttacked?.Invoke(this, prism);
        Honor += 5; // bonus honor for participating in prism combat
    }
}
