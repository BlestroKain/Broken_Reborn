using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Models;
using Newtonsoft.Json;

namespace Intersect.Framework.Core.GameObjects;

public partial class SetBase : DatabaseObject<SetBase>, IFolderable
{
    public SetBase()
    {
        Initialize();
    }

    [JsonConstructor]
    public SetBase(Guid id) : base(id)
    {
        Initialize();
    }

    private void Initialize()
    {
        Name = "New Set";
        ItemIds = [];
        StatsGiven = new int[Enum.GetValues<Stat>().Length];
        PercentageStatsGiven = new int[Enum.GetValues<Stat>().Length];
        VitalsGiven = new long[Enum.GetValues<Vital>().Length];
        PercentageVitalsGiven = new int[Enum.GetValues<Vital>().Length];
        Effects = [];
    }

    [NotMapped]
    public List<Guid> ItemIds { get; set; }

    [Column("ItemIds"), JsonIgnore]
    public string ItemIdsJson
    {
        get => JsonConvert.SerializeObject(ItemIds);
        set => ItemIds = JsonConvert.DeserializeObject<List<Guid>>(value ?? string.Empty) ?? [];
    }

    [Column("StatsGiven")]
    public int[] StatsGiven { get; set; }

    [Column("PercentageStatsGiven")]
    public int[] PercentageStatsGiven { get; set; }

    [Column("VitalsGiven")]
    public long[] VitalsGiven { get; set; }

    [Column("PercentageVitalsGiven")]
    public int[] PercentageVitalsGiven { get; set; }

    [NotMapped]
    public List<EffectData> Effects { get; set; }

    [Column("Effects"), JsonIgnore]
    public string EffectsJson
    {
        get => JsonConvert.SerializeObject(Effects);
        set => Effects = JsonConvert.DeserializeObject<List<EffectData>>(value ?? string.Empty) ?? [];
    }

    public string Folder { get; set; } = string.Empty;

    public bool HasBonuses()
    {
        return (StatsGiven?.Any(v => v != 0) ?? false)
               || (PercentageStatsGiven?.Any(v => v != 0) ?? false)
               || (VitalsGiven?.Any(v => v != 0) ?? false)
               || (PercentageVitalsGiven?.Any(v => v != 0) ?? false)
               || (Effects?.Count > 0);
    }

    public (int[] stats, int[] percentageStats, long[] vitals, int[] percentageVitals, List<EffectData> effects) GetBonuses()
        => (StatsGiven, PercentageStatsGiven, VitalsGiven, PercentageVitalsGiven, Effects);
}

