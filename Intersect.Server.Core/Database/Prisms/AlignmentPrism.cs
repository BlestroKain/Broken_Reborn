using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.Prisms;

public partial class AlignmentPrism
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Map identifier where the prism is located.
    /// </summary>
    public Guid MapId { get; set; }

    /// <summary>
    /// Faction currently owning the prism.
    /// </summary>
    public int Faction { get; set; }
}

