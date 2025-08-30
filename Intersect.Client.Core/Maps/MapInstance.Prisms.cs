using System;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Client.Maps;

/// <summary>
/// Client-side prism data for displaying conquered zones.
/// </summary>
public partial class MapInstance
{
    /// <summary>
    /// Alignment of the prism owner if the map is conquered.
    /// </summary>
    public Factions PrismOwner { get; set; } = Factions.Neutral;

    /// <summary>
    /// State of the controlling prism.
    /// </summary>
    public PrismState PrismState { get; set; } = PrismState.Placed;

    /// <summary>
    /// True if the prism is currently under attack.
    /// </summary>
    public bool PrismUnderAttack => PrismState == PrismState.UnderAttack;

    /// <summary>
    /// True if the prism has been dominated.
    /// </summary>
    public bool PrismDominated => PrismState == PrismState.Dominated;

    /// <summary>
    /// Current health of the controlling prism.
    /// </summary>
    public int PrismHp { get; set; }

    /// <summary>
    /// Maximum health of the controlling prism.
    /// </summary>
    public int PrismMaxHp { get; set; }

    /// <summary>
    /// Start time of the next vulnerability window, if any.
    /// </summary>
    public DateTime? PrismNextVulnerabilityStart { get; set; }
}
