using System;
using System.Collections.Generic;

namespace Intersect.Framework.Core.GameObjects.Prisms;

/// <summary>
///     Descriptor for a prism used in territory control.
/// </summary>
public class PrismDescriptor
{
    /// <summary>
    ///     Unique identifier for this prism descriptor.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Identifier of the map where the prism resides.
    /// </summary>
    public Guid MapId { get; set; }

    /// <summary>
    ///     X-coordinate of the prism in tiles.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    ///     Y-coordinate of the prism in tiles.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    ///     Static area affected by the prism.
    /// </summary>
    public PrismArea Area { get; set; } = new();

    /// <summary>
    ///     Collection of vulnerability windows for this prism.
    /// </summary>
    public List<VulnerabilityWindow> Windows { get; set; } = new();

    /// <summary>
    ///     Modules attached to the prism.
    /// </summary>
    public List<PrismModule> Modules { get; set; } = new();

    /// <summary>
    ///     Animation to display when the prism is idle.
    /// </summary>
    public Guid? IdleAnimationId { get; set; } = Guid.Empty;

    /// <summary>
    ///     Animation to display when the prism is vulnerable.
    /// </summary>
    public Guid? VulnerableAnimationId { get; set; } = Guid.Empty;

    /// <summary>
    ///     Animation to display when the prism is under attack.
    /// </summary>
    public Guid? UnderAttackAnimationId { get; set; } = Guid.Empty;

    /// <summary>
    ///     Indicates if the prism should be tinted based on faction ownership.
    /// </summary>
    public bool TintByFaction { get; set; } = false;

    /// <summary>
    ///     Vertical sprite offset for rendering the prism.
    /// </summary>
    public int SpriteOffsetY { get; set; } = 0;

    /// <summary>
    ///     Display name of the prism.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

