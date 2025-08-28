using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Models;
using Newtonsoft.Json;

namespace Intersect.Framework.Core.GameObjects.Prisms;

/// <summary>
/// Basic descriptor for a territory control prism.
/// </summary>
public partial class PrismDescriptor : DatabaseObject<PrismDescriptor>
{
    /// <summary>
    /// Identifier of the player or guild that owns the prism.
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Current state of the prism.
    /// </summary>
    public PrismState State { get; set; } = PrismState.Placed;

    [JsonConstructor]
    public PrismDescriptor(Guid id) : base(id)
    {
        Name = "New Prism";
    }

    /// <summary>
    /// Constructor used by Entity Framework.
    /// </summary>
    public PrismDescriptor()
    {
        Name = "New Prism";
    }
}
