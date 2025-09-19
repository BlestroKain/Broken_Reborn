using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players;

public partial class PlayerPet : IPlayerOwned
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
    public Guid Id { get; private set; }

    public Guid PlayerId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; private set; }

    public Guid PetDescriptorId { get; set; }

    public Guid PetInstanceId { get; set; }

    public string CustomName { get; set; } = string.Empty;

    public int Level { get; set; } = 1;

    public long Experience { get; set; }

    public int StatPoints { get; set; }

    [NotMapped]
    public int[] BaseStats { get; set; } = new int[Enum.GetValues<Stat>().Length];

    [NotMapped]
    public int[] StatPointAllocations { get; set; } = new int[Enum.GetValues<Stat>().Length];

    [NotMapped]
    public long[] MaxVitals { get; set; } = new long[Enum.GetValues<Vital>().Length];

    [NotMapped]
    public long[] Vitals { get; set; } = new long[Enum.GetValues<Vital>().Length];

    public long TimeCreated { get; set; } = DateTime.UtcNow.ToBinary();

    [JsonIgnore]
    [Column("PetBaseStats")]
    public string BaseStatsJson
    {
        get => DatabaseUtils.SaveIntArray(BaseStats, Enum.GetValues<Stat>().Length);
        set => BaseStats = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Stat>().Length);
    }

    [JsonIgnore]
    [Column("PetStatPointAllocations")]
    public string StatPointAllocationsJson
    {
        get => DatabaseUtils.SaveIntArray(StatPointAllocations, Enum.GetValues<Stat>().Length);
        set => StatPointAllocations = DatabaseUtils.LoadIntArray(value, Enum.GetValues<Stat>().Length);
    }

    [JsonIgnore]
    [Column("PetMaxVitals")]
    public string MaxVitalsJson
    {
        get => DatabaseUtils.SaveLongArray(MaxVitals, Enum.GetValues<Vital>().Length);
        set => MaxVitals = DatabaseUtils.LoadLongArray(value, Enum.GetValues<Vital>().Length);
    }

    [JsonIgnore]
    [Column("PetVitals")]
    public string VitalsJson
    {
        get => DatabaseUtils.SaveLongArray(Vitals, Enum.GetValues<Vital>().Length);
        set => Vitals = DatabaseUtils.LoadLongArray(value, Enum.GetValues<Vital>().Length);
    }

    [NotMapped, JsonIgnore]
    public PetDescriptor? Descriptor
    {
        get => PetDescriptor.Get(PetDescriptorId);
        set => PetDescriptorId = value?.Id ?? Guid.Empty;
    }
}
