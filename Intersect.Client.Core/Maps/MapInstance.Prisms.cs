using System;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Client.Maps;

/// <summary>
/// Client-side prism data for displaying conquered zones.
/// </summary>
public partial class MapInstance
{
    /// <summary>
    /// Identifier of the prism owner if the map is conquered.
    /// </summary>
    public Guid? PrismOwnerId { get; set; }

    /// <summary>
    /// State of the controlling prism.
    /// </summary>
    public PrismState PrismState { get; set; } = PrismState.Placed;
}
