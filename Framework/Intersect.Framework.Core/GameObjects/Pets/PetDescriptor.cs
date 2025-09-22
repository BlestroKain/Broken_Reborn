using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Animations;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using Intersect.Models;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Framework.Core.GameObjects.Pets;

public partial class PetDescriptor : DatabaseObject<PetDescriptor>, IFolderable
{
    private long[] _maxVitals = new long[Enum.GetValues<Vital>().Length];

    private int[] _stats = new int[Enum.GetValues<Stat>().Length];

    private long[] _vitalRegen = new long[Enum.GetValues<Vital>().Length];

    [NotMapped]
    public List<EquipmentScaling> EquipmentScaling { get; set; } = [];

    [Column("EquipmentScaling"), JsonIgnore]
    public string EquipmentScalingJson
    {
        get => JsonConvert.SerializeObject(EquipmentScaling ?? []);
        set => EquipmentScaling = JsonConvert.DeserializeObject<List<EquipmentScaling>>(value ?? string.Empty) ?? [];
    }

    [NotMapped]
    public List<SpellEffect> Immunities { get; set; } = [];

    [Column("Immunities"), JsonIgnore]
    public string ImmunitiesJson
    {
        get => JsonConvert.SerializeObject(Immunities ?? []);
        set => Immunities = JsonConvert.DeserializeObject<List<SpellEffect>>(value ?? string.Empty) ?? [];
    }

    [Column("AttackAnimation")]
    public Guid AttackAnimationId { get; set; }

    [NotMapped, JsonIgnore]
    public AnimationDescriptor? AttackAnimation
    {
        get => AnimationDescriptor.Get(AttackAnimationId);
        set => AttackAnimationId = value?.Id ?? Guid.Empty;
    }

    [Column("DeathAnimation")]
    public Guid DeathAnimationId { get; set; }

    [NotMapped, JsonIgnore]
    public AnimationDescriptor? DeathAnimation
    {
        get => AnimationDescriptor.Get(DeathAnimationId);
        set => DeathAnimationId = value?.Id ?? Guid.Empty;
    }

    [Column("IdleAnimation")]
    public Guid IdleAnimationId { get; set; }

    [NotMapped, JsonIgnore]
    public AnimationDescriptor? IdleAnimation
    {
        get => AnimationDescriptor.Get(IdleAnimationId);
        set => IdleAnimationId = value?.Id ?? Guid.Empty;
    }

    public int AttackSpeedModifier { get; set; }

    public int AttackSpeedValue { get; set; }

    public int Damage { get; set; } = 1;

    public int DamageType { get; set; }

    public int CritChance { get; set; }

    public double CritMultiplier { get; set; } = 1.5;

    public double Tenacity { get; set; }

    public int ResetRadius { get; set; }

    public int Scaling { get; set; } = 100;

    public int ScalingStat { get; set; }

    public int Level { get; set; } = 1;

    public long Experience { get; set; }

    public int ExperienceRate { get; set; } = 100;

    public int StatPointsPerLevel { get; set; }

    private int _maxLevel = 100;

    public int MaxLevel
    {
        get => _maxLevel <= 0 ? 1 : _maxLevel;
        set => _maxLevel = value <= 0 ? 1 : value;
    }

    public PetLevelingMode LevelingMode { get; set; } = PetLevelingMode.Experience;

    public bool CanEvolve { get; set; }

    public int EvolutionLevel { get; set; }

    [Column("EvolutionTarget")]
    public Guid EvolutionTargetId { get; set; }

    [NotMapped, JsonIgnore]
    public PetDescriptor? EvolutionTarget
    {
        get => EvolutionTargetId == Guid.Empty ? null : Get(EvolutionTargetId);
        set => EvolutionTargetId = value?.Id ?? Guid.Empty;
    }

    public string Sprite { get; set; } = string.Empty;

    [Column("Spells"), JsonIgnore]
    public string SpellsJson
    {
        get => JsonConvert.SerializeObject(Spells ?? []);
        set => Spells = JsonConvert.DeserializeObject<DbList<SpellDescriptor>>(value ?? string.Empty) ?? [];
    }

    [NotMapped]
    public DbList<SpellDescriptor> Spells { get; set; } = [];

    [Column("Stats"), JsonIgnore]
    public string StatsJson
    {
        get => DatabaseUtils.SaveIntArray(_stats, Enum.GetValues<Stat>().Length);
        set => DatabaseUtils.LoadIntArray(ref _stats, value, Enum.GetValues<Stat>().Length);
    }

    [Column("MaxVital"), JsonIgnore]
    public string MaxVitalJson
    {
        get => DatabaseUtils.SaveLongArray(_maxVitals, Enum.GetValues<Vital>().Length);
        set => DatabaseUtils.LoadLongArray(ref _maxVitals, value, Enum.GetValues<Vital>().Length);
    }

    [Column("VitalRegen"), JsonIgnore]
    public string VitalRegenJson
    {
        get => DatabaseUtils.SaveLongArray(_vitalRegen, Enum.GetValues<Vital>().Length);
        set => DatabaseUtils.LoadLongArray(ref _vitalRegen, value, Enum.GetValues<Vital>().Length);
    }

    [JsonProperty(nameof(MaxVitals)), NotMapped]
    public IReadOnlyDictionary<Vital, long> MaxVitalsLookup
    {
        get =>
            MaxVitals.Select((value, index) => (value, index))
                .ToDictionary(t => (Vital)t.index, t => t.value)
                .AsReadOnly();
        set
        {
            foreach (var (key, val) in value)
            {
                MaxVitals[(int)key] = val;
            }
        }
    }

    [JsonProperty(nameof(VitalRegen)), NotMapped]
    public IReadOnlyDictionary<Vital, long> VitalRegenLookup
    {
        get =>
            VitalRegen.Select((value, index) => (value, index))
                .ToDictionary(t => (Vital)t.index, t => t.value)
                .AsReadOnly();
        set
        {
            foreach (var (key, val) in value)
            {
                VitalRegen[(int)key] = val;
            }
        }
    }

    [JsonProperty(nameof(Stats)), NotMapped]
    public IReadOnlyDictionary<Stat, int> StatsLookup
    {
        get =>
            Stats.Select((statValue, index) => (statValue, index))
                .ToDictionary(t => (Stat)t.index, t => t.statValue)
                .AsReadOnly();
        set
        {
            foreach (var (key, val) in value)
            {
                Stats[(int)key] = val;
            }
        }
    }

    [NotMapped, JsonIgnore]
    public long[] MaxVitals
    {
        get => _maxVitals;
        set => _maxVitals = value ?? new long[Enum.GetValues<Vital>().Length];
    }

    [NotMapped, JsonIgnore]
    public long[] VitalRegen
    {
        get => _vitalRegen;
        set => _vitalRegen = value ?? new long[Enum.GetValues<Vital>().Length];
    }

    [NotMapped, JsonIgnore]
    public int[] Stats
    {
        get => _stats;
        set => _stats = value ?? new int[Enum.GetValues<Stat>().Length];
    }

    public string Folder { get; set; } = string.Empty;

    [JsonConstructor]
    public PetDescriptor(Guid id) : base(id)
    {
        Name = "New Pet";
        ExperienceRate = 100;
        MaxLevel = 100;
        LevelingMode = PetLevelingMode.Experience;
    }

    public PetDescriptor()
    {
        Name = "New Pet";
        ExperienceRate = 100;
        MaxLevel = 100;
        LevelingMode = PetLevelingMode.Experience;
    }
}

public sealed class EquipmentScaling
{
    public Stat Stat { get; set; }

    public double Multiplier { get; set; } = 1.0;
}
