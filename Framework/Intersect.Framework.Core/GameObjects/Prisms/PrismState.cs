using System;

namespace Intersect.Framework.Core.GameObjects.Prisms;

/// <summary>
/// Represents the possible states of a prism used for territory control.
/// </summary>
public enum PrismState
{
    /// <summary>
    /// The prism has been placed and is protected for a short duration.
    /// </summary>
    Placed,

    /// <summary>
    /// The prism can be attacked by opposing factions.
    /// </summary>
    Vulnerable,

    /// <summary>
    /// The prism is currently under attack.
    /// </summary>
    UnderAttack,

    /// <summary>
    /// The prism has been dominated after surviving attacks.
    /// </summary>
    Dominated,

    /// <summary>
    /// The prism has been destroyed.
    /// </summary>
    Destroyed
}
