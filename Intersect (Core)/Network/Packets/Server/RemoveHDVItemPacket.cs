using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class RemoveHDVItemPacket : IntersectPacket
    {
        public RemoveHDVItemPacket(Guid id)
        {
            RemoveItemHDVId = id;
        }

        [Key(0)]
        public Guid RemoveItemHDVId { get; set; }
    }
}
