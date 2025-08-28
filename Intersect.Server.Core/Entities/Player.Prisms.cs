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
    public event Action<Player, AlignmentPrism>? PrismPlaced;

    /// <summary>
    /// Invoked when the player attacks a prism.
    /// </summary>
    public event Action<Player, AlignmentPrism>? PrismAttacked;

    /// <summary>
    /// Places a new prism and notifies listeners.
    /// </summary>
    public void PlacePrism()
    {
        var prism = new AlignmentPrism
        {
            Id = Guid.NewGuid(),
            Owner = Faction,
            State = PrismState.Placed,
            PlacedAt = DateTime.UtcNow,
            MapId = this.MapId,
            Level = 1,
            MaxHp = 1,
            Hp = 1,
        };

        PrismPlaced?.Invoke(this, prism);
    }

    /// <summary>
    /// Notifies that the player attacked a prism. Additional honor is awarded.
    /// </summary>
    public void AttackPrism(AlignmentPrism prism)
    {
        PrismAttacked?.Invoke(this, prism);
        Honor += 5; // bonus honor for participating in prism combat
    }
}
