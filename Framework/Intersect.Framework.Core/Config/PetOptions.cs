namespace Intersect.Config;

/// <summary>
/// Configuration values controlling pet behaviour.
/// </summary>
public partial class PetOptions
{
    /// <summary>
    /// Distance (in tiles) at which pets are teleported back to their owner.
    /// Set to 0 or less to disable leash-based teleporting.
    /// </summary>
    public int FollowLeashDistance { get; set; } = 12;

    /// <summary>
    /// Number of consecutive pathfinding failures before a pet is teleported to its owner.
    /// Set to 0 or less to disable failure-based teleporting.
    /// </summary>
    public int FollowFailureTeleportThreshold { get; set; } = 3;

    /// <summary>
    /// Milliseconds without pathfinding failures before the failure counter resets.
    /// Set to 0 or less to disable the automatic reset.
    /// </summary>
    public int FollowFailureResetMilliseconds { get; set; } = 5000;
}
