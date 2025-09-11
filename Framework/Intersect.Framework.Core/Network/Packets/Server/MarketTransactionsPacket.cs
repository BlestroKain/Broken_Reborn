using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Items;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MarketTransactionPacket
    {
        [Key(0)] public string BuyerName { get; set; }
        [Key(1)] public Guid ItemId { get; set; }
        [Key(2)] public int Quantity { get; set; }
        [Key(3)] public int Price { get; set; }
        [Key(4)] public ItemProperties Properties { get; set; }
        [Key(5)] public DateTime SoldAt { get; set; }
    }

    [MessagePackObject]
    public class MarketTransactionsPacket : IntersectPacket
    {
        [Key(0)] public List<MarketTransactionPacket> Transactions { get; set; }

        public MarketTransactionsPacket(List<MarketTransactionPacket> transactions)
        {
            Transactions = transactions ?? new List<MarketTransactionPacket>();
        }
    }
}
