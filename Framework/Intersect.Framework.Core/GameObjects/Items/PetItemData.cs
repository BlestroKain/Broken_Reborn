using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Pets;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Intersect.Framework.Core.GameObjects.Items;

[Owned]
public partial class PetItemData
{
    public Guid PetDescriptorId { get; set; }

    public bool SummonOnEquip { get; set; }

    public bool DespawnOnUnequip { get; set; }

    public bool BindOnEquip { get; set; }

    public string? PetNameOverride { get; set; }

    [NotMapped]
    [JsonIgnore]
    public PetDescriptor? Descriptor => PetDescriptorId == Guid.Empty ? null : PetDescriptor.Get(PetDescriptorId);
}
