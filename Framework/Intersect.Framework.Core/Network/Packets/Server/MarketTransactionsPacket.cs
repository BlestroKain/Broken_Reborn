using System;
using System.Collections.Generic;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MarketTransactionsPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public MarketTransactionsPacket()
    {
    }

    public MarketTransactionsPacket(List<MarketTransactionInfo> transactions)
    {
        Transactions = transactions;
    }

    [Key(0)]
    public List<MarketTransactionInfo> Transactions { get; set; } = new();
}

[MessagePackObject]
public partial class MarketTransactionInfo
{
    // Parameterless constructor for MessagePack
    public MarketTransactionInfo()
    {
    }

    public MarketTransactionInfo(Guid listingId, Guid buyerId, int quantity, long price)
    {
        ListingId = listingId;
        BuyerId = buyerId;
        Quantity = quantity;
        Price = price;
    }

    [Key(0)]
    public Guid ListingId { get; set; }

    [Key(1)]
    public Guid BuyerId { get; set; }

    [Key(2)]
    public int Quantity { get; set; }

    [Key(3)]
    public long Price { get; set; }
}
