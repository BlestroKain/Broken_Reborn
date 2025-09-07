using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Items;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Market;

public partial class MarketTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ListingId { get; set; }
    public Guid BuyerId { get; set; }
    public int Quantity { get; set; }
    public long Price { get; set; }
    public DateTime TransactionTime { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public ItemProperties Properties { get; set; } = new();

    [Column(nameof(ItemProperties))]
    [JsonIgnore]
    public string ItemPropertiesJson
    {
        get => JsonConvert.SerializeObject(Properties);
        set => Properties = JsonConvert.DeserializeObject<ItemProperties>(value ?? string.Empty) ?? new ItemProperties();
    }
}
