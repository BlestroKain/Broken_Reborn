using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.Prisms;

public partial class PrismContribution
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BattleId { get; set; }

    public Guid PlayerId { get; set; }

    public Guid PlayerUserId { get; set; }

    public string PlayerIp { get; set; }

    public string PlayerFingerprint { get; set; }

    public int Contribution { get; set; }
}

