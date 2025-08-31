using System;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Database.Prisms;
using Intersect.Server.Services.Prisms;

namespace Intersect.Server.Entities;

/// <summary>
/// Prism-related actions that a player can perform.
/// </summary>
public partial class Player
{
    /// <summary>
    /// Invoked when the player places a prism.
    /// </summary>
    public event Action<Player, PrismRuntime>? PrismPlaced;

    /// <summary>
    /// Invoked when the player attacks a prism.
    /// </summary>
    public event Action<Player, PrismRuntime>? PrismAttacked;

    /// <summary>
    /// Places a new prism and notifies listeners.
    /// </summary>
    public void PlacePrism()
    {
        var descriptor = new PrismDescriptor
        {
            Id = Guid.NewGuid(),
            MapId = this.MapId,
            X = X,
            Y = Y,
        };

        var entity = new PrismEntity
        {
            PrismId = descriptor.Id,
            Owner = Faction,
            State = PrismState.Placed,
            MaxHp = 1,
            Hp = 1,
            LastStateChangeAt = DateTime.UtcNow,
        };

        var prism = new PrismRuntime(descriptor, entity);

        PrismPlaced?.Invoke(this, prism);
    }

    /// <summary>
    /// Notifies that the player attacked a prism. Additional honor is awarded.
    /// </summary>
    public void AttackPrism(PrismRuntime prism)
    {
        prism.State = PrismState.UnderAttack;
        PrismAttacked?.Invoke(this, prism);
        Honor += 5; // bonus honor for participating in prism combat
    }

    /// <summary>
    /// Marks a prism as dominated after a successful defense.
    /// </summary>
    public void DominatePrism(PrismRuntime prism)
    {
        prism.State = PrismState.Dominated;
        Honor += 10; // extra honor for dominating a prism
    }
}
