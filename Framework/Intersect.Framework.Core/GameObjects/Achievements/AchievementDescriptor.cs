using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Conditions;
using Intersect.Framework.Core.Serialization;
using Intersect.Models;
using Newtonsoft.Json;

namespace Intersect.GameObjects;

public partial class AchievementDescriptor : DatabaseObject<AchievementDescriptor>, IFolderable
{
    [JsonConstructor]
    public AchievementDescriptor(Guid id) : base(id)
    {
        Name = "New Achievement";
    }

    public AchievementDescriptor()
    {
        Name = "New Achievement";
    }

    public string Category { get; set; } = string.Empty;

    public int Difficulty { get; set; }

    [Column("Requirements")]
    [JsonIgnore]
    public string JsonRequirements
    {
        get => Requirements.Data();
        set => Requirements.Load(value);
    }

    [NotMapped]
    public ConditionLists Requirements { get; set; } = new();

    public string Rewards { get; set; } = string.Empty;

    public string Folder { get; set; } = string.Empty;
}
