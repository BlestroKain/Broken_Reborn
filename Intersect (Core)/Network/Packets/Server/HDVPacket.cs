using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class HDVPacket : IntersectPacket
    {
        public HDVPacket(Guid hdvid, HDVItemPacket[] hdvItems)
        {
            HdvID = hdvid;
            HdvItems = hdvItems;
        }

        [Key(0)]
        public Guid HdvID { get; set; }
        [Key(1)]
        public HDVItemPacket[] HdvItems { get; set; }
    }
}
