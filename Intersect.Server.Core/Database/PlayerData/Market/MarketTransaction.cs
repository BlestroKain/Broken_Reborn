using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketTransaction
{
public Guid Id { get; set; } = Guid.NewGuid();
public Guid ListingId { get; set; }
public Guid? BuyerId { get; set; }
public string BuyerName { get; set; } = string.Empty;
public Guid ItemId { get; set; }
public Guid SellerId { get; set; }
public virtual Player? Seller { get; set; }
public int Quantity { get; set; }
public long Price { get; set; }
public DateTime TransactionTime { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public ItemProperties ItemProperties { get; set; } = new();

    [Column(nameof(ItemProperties))]
    [JsonIgnore]
    public string ItemPropertiesJson
    {
        get => JsonConvert.SerializeObject(ItemProperties);
        set => ItemProperties = JsonConvert.DeserializeObject<ItemProperties>(value ?? string.Empty) ?? new ItemProperties();
    }
}
