using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class BankSortPacket : IntersectPacket
    {
        [Key(0)]
        public string sortType = "standard";

        public BankSortPacket()
        {
            // Empty constructor for messagepack   
        }
    }
}