using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class Achievement : IPlayerOwned
{
    public Achievement() { }

    public Achievement(Guid id)
    {
        AchievementId = id;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; private set; }

    [JsonIgnore]
    public Guid AchievementId { get; private set; }

    public int Progress { get; set; }

    public bool Completed { get; set; }

    [JsonIgnore]
    public Guid PlayerId { get; private set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; private set; }

    public string Data()
    {
        return JsonConvert.SerializeObject(this);
    }
}
