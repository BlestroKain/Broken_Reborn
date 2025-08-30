namespace Intersect.Framework.Core.GameObjects.Prisms;

/// <summary>
///     Rectangular area affected by a prism.
/// </summary>
public class PrismArea
{
    /// <summary>
    ///     X-coordinate of the top-left corner in tiles.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    ///     Y-coordinate of the top-left corner in tiles.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    ///     Width of the area in tiles.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    ///     Height of the area in tiles.
    /// </summary>
    public int Height { get; set; }
}

