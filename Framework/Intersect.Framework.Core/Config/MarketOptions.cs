namespace Intersect.Config;

public partial class MarketOptions
{
    /// <summary>
    /// Number of listings to display per page in market searches.
    /// </summary>
    public int PageSize { get; set; } = 35;

    /// <summary>
    /// Maximum number of active listings a player may have at once.
    /// </summary>
    public int MaxActiveListings { get; set; } = 100;

    /// <summary>
    /// Minimum number of seconds between listing or cancel operations.
    /// </summary>
    public int ActionCooldownSeconds { get; set; } = 10;
}

