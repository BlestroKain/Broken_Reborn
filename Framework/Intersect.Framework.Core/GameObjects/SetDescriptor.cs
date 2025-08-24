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

        // (Opcional) limpiar ItemIds que ya no coinciden
        ItemIds = ItemIds.Where(id => ItemDescriptor.Get(id)?.SetId == Id).ToList();
    }

    public (int[] stats, int[] percentStats, long[] vitals, long[] vitalsRegen, int[] percentVitals, List<EffectData> effects) GetBonuses(int pieces)
    {
        var totalPieces = Math.Max(1, ItemIds.Count);
        var ratio = Math.Clamp(pieces / (float)totalPieces, 0f, 1f);

        var stats = new int[Enum.GetValues<Stat>().Length];
        var percentStats = new int[Enum.GetValues<Stat>().Length];
        var vitals = new long[Enum.GetValues<Vital>().Length];
        var vitalsRegen = new long[Enum.GetValues<Vital>().Length];
        var percentVitals = new int[Enum.GetValues<Vital>().Length];

        for (var i = 0; i < stats.Length; i++)
        {
            stats[i] = (int)Math.Round(Stats[i] * ratio);
            percentStats[i] = (int)Math.Round(PercentageStats[i] * ratio);
        }

        for (var i = 0; i < vitals.Length; i++)
        {
            vitals[i] = (long)Math.Round(Vitals[i] * ratio);
            vitalsRegen[i] = (long)Math.Round(VitalsRegen[i] * ratio);
            percentVitals[i] = (int)Math.Round(PercentageVitals[i] * ratio);
        }

        var effects = Effects
            .Select(e => new EffectData(e.Type, (int)Math.Round(e.Percentage * ratio), e.IsPassive, e.Stacking))
            .ToList();

        return (stats, percentStats, vitals, vitalsRegen, percentVitals, effects);
    }

    [NotMapped, JsonIgnore]
    public bool HasBonuses =>
        Stats.Any(s => s != 0) ||
        PercentageStats.Any(s => s != 0) ||
        Vitals.Any(v => v != 0) ||
        PercentageVitals.Any(v => v != 0) ||
        VitalsRegen.Any(v => v != 0) ||
        Effects.Any();

    public int GetEffectPercentage(ItemEffect type)
    {
        return Effects.Find(effect => effect.Type == type)?.Percentage ?? 0;
    }

    public void SetEffectOfType(ItemEffect type, int value)
    {
        var effectToEdit = Effects.Find(effect => effect.Type == type);
        if (effectToEdit != null)
        {
            effectToEdit.Percentage = value;
        }
    }

    [NotMapped, JsonIgnore]
    public ItemEffect[] EffectsEnabled => Effects.Select(effect => effect.Type).ToArray();

    public static string GetName(Guid id) => Get(id)?.Name ?? "???";

    public static string GetDescription(Guid id) => Get(id)?.Description ?? string.Empty;
}
