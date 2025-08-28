using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class KillLog
{
    private KillLog() { }

    public KillLog(Player attacker, Player victim)
    {
        Attacker = attacker;
        Victim = victim;
        AttackerId = attacker.Id;
        VictimId = victim.Id;
        Timestamp = DateTime.UtcNow;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid AttackerId { get; private set; }

    public Guid VictimId { get; private set; }

    public DateTime Timestamp { get; private set; }

    [JsonIgnore]
    public virtual Player Attacker { get; private set; }

    [JsonIgnore]
    public virtual Player Victim { get; private set; }
}
