namespace Intersect.Config;

public partial class AlignmentOptions
{
    /// <summary>
    /// Number of days a player must wait before swapping alignment again.
    /// </summary>
    public int SwapCooldownDays { get; set; } = 7;
}
