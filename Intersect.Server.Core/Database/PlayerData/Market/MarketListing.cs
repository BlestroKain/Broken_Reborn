using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Market;

public class MarketListing
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SellerId { get; set; }
    public virtual Player? Seller { get; set; }

    [Required]
    public Guid ItemId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Price { get; set; }

    [NotMapped]
    public ItemProperties ItemProperties { get; set; } = new();

    [Column("ItemProperties")]
    [JsonIgnore]
    public string ItemPropertiesJson
    {
        get => JsonConvert.SerializeObject(ItemProperties);
        set => ItemProperties = JsonConvert.DeserializeObject<ItemProperties>(value ?? string.Empty) ?? new ItemProperties();
    }

    public DateTime ListedAt { get; set; } = DateTime.UtcNow;

    public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddDays(28);

    public bool IsSold { get; set; } = false;
}
