using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
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

        public Guid Id { get; set; }
        public string Seller { get; set; }
        public Guid ItemId { get; set; } = Guid.Empty;
        public int Quantity { get; set; }
       
        public int[] StatBuffs { get; set; } = new int[(int)Enums.Stats.StatCount];
        public int Price { get; set; }
        public bool Update { get; set; }

    }
}
