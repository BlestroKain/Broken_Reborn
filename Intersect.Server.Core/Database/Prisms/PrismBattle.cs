using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.Prisms;

public partial class PrismBattle
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PrismId { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }
}

