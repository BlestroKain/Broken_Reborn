using System;
using MessagePack;

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
        int? itemId,
        int? minPrice,
        int? maxPrice,
        bool? status,
        Guid? sellerId
    )
    {
        Page = page;
        PageSize = pageSize;
        ItemId = itemId;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        Status = status;
        SellerId = sellerId;
    }

    [Key(0)]
    public int Page { get; set; }

    [Key(1)]
    public int PageSize { get; set; }

    [Key(2)]
    public int? ItemId { get; set; }

    [Key(3)]
    public int? MinPrice { get; set; }

    [Key(4)]
    public int? MaxPrice { get; set; }

    [Key(5)]
    public bool? Status { get; set; }

    [Key(6)]
    public Guid? SellerId { get; set; }
}
