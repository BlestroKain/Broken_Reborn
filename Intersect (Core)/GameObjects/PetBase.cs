using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Models;
using Intersect.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using System;
using Intersect;

public partial class PetBase : DatabaseObject<PetBase>, IFolderable
{
    // Constructor sin parámetros para EF
    public PetBase() : base(Guid.NewGuid())
    {
        Name = "New Pet";
    }

    // Constructor para Json
    [JsonConstructor]
    public PetBase(Guid id) : base(id)
    {
        Name = "New Pet";
    }

    // Propiedades de estadísticas base
    [NotMapped]
    public int[] PetStats  = new int[(int)Stat.StatCount];

    [NotMapped]
    public int[] VitalRegen  = new int[(int)Vital.VitalCount];

    [NotMapped]
    public int[] MaxVital  = new int[(int)Vital.VitalCount];

    // Propiedades de inmunidades y habilidades
    [NotMapped]
    public List<SpellEffect> Immunities { get; set; } = new List<SpellEffect>();

    [JsonIgnore]
    [Column("Immunities")]
    public string ImmunitiesJson
    {
        get => JsonConvert.SerializeObject(Immunities);
        set => Immunities = JsonConvert.DeserializeObject<List<SpellEffect>>(value ?? "") ?? new List<SpellEffect>();
    }

    // Propiedad de nivel y experiencia
    public int Level { get; set; } = 1;
    public long Experience { get; set; } = 0;

    // Comportamiento de la mascota
    public bool Aggressive { get; set; }
    public byte Movement { get; set; }

    // Animaciones
    [Column("AttackAnimation")]
    public Guid AttackAnimationId { get; set; }

    [NotMapped]
    [JsonIgnore]
    public AnimationBase AttackAnimation
    {
        get => AnimationBase.Get(AttackAnimationId);
        set => AttackAnimationId = value?.Id ?? Guid.Empty;
    }

    [Column("DeathAnimation")]
    public Guid DeathAnimationId { get; set; }

    [NotMapped]
    [JsonIgnore]
    public AnimationBase DeathAnimation
    {
        get => AnimationBase.Get(DeathAnimationId);
        set => DeathAnimationId = value?.Id ?? Guid.Empty;
    }

    // Propiedad de combate
    public int Damage { get; set; } = 1;
    public int DamageType { get; set; }
    public int CritChance { get; set; }
    public double CritMultiplier { get; set; } = 1.5;

    // Spells
    [NotMapped]
    public DbList<SpellBase> Spells { get; set; } = new DbList<SpellBase>();

    [JsonIgnore]
    [Column("Spells")]
    public string SpellsJson
    {
        get => JsonConvert.SerializeObject(Spells, Formatting.None);
        protected set => Spells = JsonConvert.DeserializeObject<DbList<SpellBase>>(value);
    }

    // Apariencia
    public string Sprite { get; set; } = "";

    [Column("Color")]
    [JsonIgnore]
    public string JsonColor
    {
        get => JsonConvert.SerializeObject(Color);
        set => Color = !string.IsNullOrWhiteSpace(value) ? JsonConvert.DeserializeObject<Color>(value) : Color.White;
    }

    [NotMapped]
    public Color Color { get; set; } = new Color(255, 255, 255, 255);

    // Vitals & Stats
    [Column("MaxVital")]
    [JsonIgnore]
    public string JsonMaxVital
    {
        get => DatabaseUtils.SaveIntArray(MaxVital, (int)Vital.VitalCount);
        set => DatabaseUtils.LoadIntArray(ref MaxVital, value, (int)Vital.VitalCount);
    }

    [Column("BaseStats")]
    [JsonIgnore]
    public string JsonBaseStats
    {
        get => DatabaseUtils.SaveIntArray(PetStats, (int)Stat.StatCount);
        set => DatabaseUtils.LoadIntArray(ref PetStats, value, (int)Stat.StatCount);
    }

    [Column("VitalRegen")]
    [JsonIgnore]
    public string VitalRegenJson
    {
        get => DatabaseUtils.SaveIntArray(VitalRegen, (int)Vital.VitalCount);
        set => VitalRegen = DatabaseUtils.LoadIntArray(value, (int)Vital.VitalCount);
    }

    // Implementación de IFolderable
    public string Folder { get; set; } = "";
    public decimal Scaling { get; set; }
    public int ScalingStat { get; set; }
    public int AttackSpeedModifier { get; set; }
    public decimal AttackSpeedValue { get; set; }
    public int RequiredLevel { get; set; }
}
