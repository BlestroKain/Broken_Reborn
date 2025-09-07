namespace Intersect.Config;

/// <summary>
/// Configurable options for the player market system.
/// </summary>
public partial class MarketOptions
{
    /// <summary>
    /// Allowed variance from the baseline price when listing items.
    /// </summary>
    public float AllowedPriceVariance { get; set; } = 0.3f;
}
