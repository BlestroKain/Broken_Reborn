using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class AddHDVPacket : IntersectPacket
    {

        public AddHDVPacket(int slot, int quantity, int price)
        {
            Slot = slot;
            Quantity = quantity;
            Price = price;
        }

        [Key(0)]
        public int Slot { get; set; }
        [Key(1)]
        public int Quantity { get; set; }
        [Key(2)]
        public int Price { get; set; }
    }
}
