using System;
using MessagePack;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class SearchMarketPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public SearchMarketPacket()
    {
    }

    public SearchMarketPacket(
        int page,
        int pageSize,
        Guid? itemId,
        int? minPrice,
        int? maxPrice,
        bool? status,
        Guid? sellerId,
        string itemName = "",
        ItemType? type = null,
        string? subtype = null
    )
    {
        Page = page;
        PageSize = pageSize;
        ItemId = itemId;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        Status = status;
        SellerId = sellerId;
        ItemName = itemName;
        Type = type;
        Subtype = subtype;
    }

    [Key(0)]
    public int Page { get; set; }

    [Key(1)]
    public int PageSize { get; set; }

    [Key(2)]
    public Guid? ItemId { get; set; }

    [Key(3)]
    public int? MinPrice { get; set; }

    [Key(4)]
    public int? MaxPrice { get; set; }

    [Key(5)]
    public bool? Status { get; set; }

    [Key(6)]
    public Guid? SellerId { get; set; }

    [Key(7)]
    public string ItemName { get; set; } = string.Empty;

    [Key(8)]
    public ItemType? Type { get; set; }

    [Key(9)]
    public string? Subtype { get; set; }
}
