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
    public Alignment PrismOwner { get; set; } = Alignment.Neutral;

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
}
