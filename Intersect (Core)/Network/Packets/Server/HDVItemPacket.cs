using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePack.MessagePackObject]
    public class HDVItemPacket : IntersectPacket
    {
        public HDVItemPacket(Guid id, string seller, Guid itemid, int quantity, int[] statBuffs, int price, bool update = false)
        {
            Id = id;
            Seller = seller;
            ItemId = itemid;
            Quantity = quantity;
            StatBuffs = statBuffs;
            Price = price;
            Update = update;
        }

        [MessagePack.Key(0)]
        public Guid Id { get; set; }
        [MessagePack.Key(1)]
        public string Seller { get; set; }
        [MessagePack.Key(2)]
        public Guid ItemId { get; set; } = Guid.Empty;
        [MessagePack.Key(3)]
        public int Quantity { get; set; }

        [MessagePack.Key(4)]
        public int[] StatBuffs { get; set; } = new int[(int)Enums.Stat.StatCount];
        [MessagePack.Key(5)]
        public int Price { get; set; }
        [MessagePack.Key(6)]
        public bool Update { get; set; }

    }
}
