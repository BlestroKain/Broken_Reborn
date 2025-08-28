using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.Prisms;

public partial class FactionAreaBonus
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PrismId { get; set; }

    public int Faction { get; set; }

    public int Bonus { get; set; }
}

