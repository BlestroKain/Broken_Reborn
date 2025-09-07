using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Entities;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketListing
{
public Guid Id { get; set; } = Guid.NewGuid();
public Guid SellerId { get; set; }
public virtual Player? Seller { get; set; }
public int ItemId { get; set; }
public int Quantity { get; set; }
    public long Price { get; set; }
    public DateTime ListedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddDays(7);
    public bool IsSold { get; set; }
    public bool ReturnPending { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

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
