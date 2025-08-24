using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Models;
using Intersect.Utilities;
using Intersect.Collections;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Intersect.GameObjects;

public partial class SetDescriptor : DatabaseObject<SetDescriptor>, IFolderable
{

    // Constructor requerido por EF Core (sin parámetros)
    public SetDescriptor() : base(Guid.NewGuid())
    {
        Validate();
    }

    [JsonConstructor]
    public SetDescriptor(Guid guid) : base(guid)
    {
        Validate();
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context) => Validate();



    public string Name { get; set; } = "New Set";

    public string Description { get; set; } = string.Empty;

    [NotMapped]
    public List<Guid> ItemIds { get; set; } = new();

    [Column("ItemIds")]
    [JsonIgnore]
    public string ItemIdsJson
    {
        get => JsonConvert.SerializeObject(ItemIds);
        set => ItemIds = JsonConvert.DeserializeObject<List<Guid>>(value ?? "") ?? new();
    }

    [NotMapped]
    public Dictionary<int, SetBonusTier> BonusTiers { get; set; } = new();

    [Column("BonusTiers")]
    [JsonIgnore]
    public string BonusTiersJson
    {
        get => JsonConvert.SerializeObject(BonusTiers);
        set => BonusTiers = JsonConvert.DeserializeObject<Dictionary<int, SetBonusTier>>(value ?? "") ?? new();
    }

    [Column("VitalsGiven")]
    [JsonIgnore]
    public string VitalsJson
    {
        get => DatabaseUtils.SaveLongArray(Vitals, Enum.GetValues<Vital>().Length);
        set => Vitals = DatabaseUtils.LoadLongArray(value, Enum.GetValues<Vital>().Length);
    }

    [NotMapped]
    public long[] Vitals { get; set; }

    [Column("VitalsRegen")]
    [JsonIgnore]
    public string VitalsRegenJson
    {
        get => DatabaseUtils.SaveLongArray(VitalsRegen, Enum.GetValues<Vital>().Length);
        set => VitalsRegen = DatabaseUtils.LoadLongArray(value, Enum.GetValues<Vital>().Length);
    }

    [NotMapped]
    public long[] VitalsRegen { get; set; }

    [Column("PercentageVitalsGiven")]
    [JsonIgnore]
    public string PercentageVitalsJson
    {
        get => DatabaseUtils.SaveIntArray(PercentageVitals, Enum.GetValues<Vital>().Length);
        set => PercentageVitals = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Vital>().Length);
    }

    [NotMapped]
    public int[] PercentageVitals { get; set; }

    [Column("StatsGiven")]
    [JsonIgnore]
    public string StatsJson
    {
        get => DatabaseUtils.SaveIntArray(Stats, Enum.GetValues<Stat>().Length);
        set => Stats = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Stat>().Length);
    }

    [NotMapped]
    public int[] Stats { get; set; }

    [Column("PercentageStatsGiven")]
    [JsonIgnore]
    public string PercentageStatsJson
    {
        get => DatabaseUtils.SaveIntArray(PercentageStats, Enum.GetValues<Stat>().Length);
        set => PercentageStats = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Stat>().Length);
    }

    [NotMapped]
    public int[] PercentageStats { get; set; }
    [NotMapped]
    public List<EffectData> Effects { get; set; } = new();

    [Column("Effects")]
    [JsonIgnore]
    public string EffectsJson
    {
        get => JsonConvert.SerializeObject(Effects);
        set => Effects = JsonConvert.DeserializeObject<List<EffectData>>(value ?? "") ?? new List<EffectData>();
    }

    public string Folder { get; set; } = "";

    public override void Load(string json, bool keepCreationTime = false)
    {
        base.Load(json, keepCreationTime);
        Validate();
    }

    public void Validate()
    {
        Stats = ArrayExtensions.EnsureLen(Stats, Enum.GetValues<Stat>().Length);
        PercentageStats = ArrayExtensions.EnsureLen(PercentageStats, Enum.GetValues<Stat>().Length);
        Vitals = ArrayExtensions.EnsureLen(Vitals, Enum.GetValues<Vital>().Length);
        VitalsRegen = ArrayExtensions.EnsureLen(VitalsRegen, Enum.GetValues<Vital>().Length);
        PercentageVitals = ArrayExtensions.EnsureLen(PercentageVitals, Enum.GetValues<Vital>().Length);
        Effects ??= new List<EffectData>();

        foreach (var tier in BonusTiers.Values)
        {
            tier.Validate();
        }

        if (!BonusTiers.Any() && HasLegacyBonuses())
        {
            BonusTiers[Math.Max(1, ItemIds.Count)] = new SetBonusTier
            {
                Stats = Stats,
                PercentageStats = PercentageStats,
                Vitals = Vitals,
                VitalsRegen = VitalsRegen,
                PercentageVitals = PercentageVitals,
                Effects = Effects
            };
        }

        // (Opcional) limpiar ItemIds que ya no coinciden
        ItemIds = ItemIds.Where(id => ItemDescriptor.Get(id)?.SetId == Id).ToList();
    }

    private bool HasLegacyBonuses() =>
        Stats.Any(s => s != 0) ||
        PercentageStats.Any(s => s != 0) ||
        Vitals.Any(v => v != 0) ||
        PercentageVitals.Any(v => v != 0) ||
        VitalsRegen.Any(v => v != 0) ||
        Effects.Any();

    public (int[] stats, int[] percentStats, long[] vitals, long[] vitalsRegen, int[] percentVitals, List<EffectData> effects) GetBonuses(int pieces)
    {
        if (!BonusTiers.Any())
        {
            return (
                new int[Enum.GetValues<Stat>().Length],
                new int[Enum.GetValues<Stat>().Length],
                new long[Enum.GetValues<Vital>().Length],
                new long[Enum.GetValues<Vital>().Length],
                new int[Enum.GetValues<Vital>().Length],
                new List<EffectData>()
            );
        }

        var key = BonusTiers.Keys.Where(k => k <= pieces).DefaultIfEmpty(0).Max();
        if (key == 0 || !BonusTiers.TryGetValue(key, out var tier))
        {
            return (
                new int[Enum.GetValues<Stat>().Length],
                new int[Enum.GetValues<Stat>().Length],
                new long[Enum.GetValues<Vital>().Length],
                new long[Enum.GetValues<Vital>().Length],
                new int[Enum.GetValues<Vital>().Length],
                new List<EffectData>()
            );
        }

        return tier.GetBonuses();
    }

    [NotMapped, JsonIgnore]
    public bool HasBonuses => BonusTiers.Values.Any(t => t.HasBonuses);

    public int GetEffectPercentage(ItemEffect type)
    {
        return BonusTiers.TryGetValue(Math.Max(1, ItemIds.Count), out var tier)
            ? tier.GetEffectPercentage(type)
            : 0;
    }

    public void SetEffectOfType(ItemEffect type, int value)
    {
        if (BonusTiers.TryGetValue(Math.Max(1, ItemIds.Count), out var tier))
        {
            tier.SetEffectOfType(type, value);
        }
    }

    [NotMapped, JsonIgnore]
    public ItemEffect[] EffectsEnabled => BonusTiers.TryGetValue(Math.Max(1, ItemIds.Count), out var tier)
        ? tier.EffectsEnabled
        : Array.Empty<ItemEffect>();

    public static string GetName(Guid id) => Get(id)?.Name ?? "???";

    public static string GetDescription(Guid id) => Get(id)?.Description ?? string.Empty;
}
