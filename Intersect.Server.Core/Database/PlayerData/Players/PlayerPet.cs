using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Server.Entities;
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

    public string CustomName { get; set; } = string.Empty;

    public int Level { get; set; } = 1;

    public long Experience { get; set; }

    public long TimeCreated { get; set; } = DateTime.UtcNow.ToBinary();

    [NotMapped, JsonIgnore]
    public PetDescriptor? Descriptor
    {
        get => PetDescriptor.Get(PetDescriptorId);
        set => PetDescriptorId = value?.Id ?? Guid.Empty;
    }
}
