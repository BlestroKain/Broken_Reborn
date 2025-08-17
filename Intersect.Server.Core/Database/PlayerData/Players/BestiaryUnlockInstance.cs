using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public class BestiaryUnlockInstance : IPlayerOwned
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; private set; }

    public Guid NpcId { get; set; }

    public BestiaryUnlock UnlockType { get; set; }

    public int Value { get; set; }

    [JsonIgnore]
    public Guid PlayerId { get; private set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public Player Player { get; private set; }
}

