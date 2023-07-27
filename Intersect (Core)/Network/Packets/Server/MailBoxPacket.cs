using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class MailBoxPacket:IntersectPacket
    {
        public MailBoxPacket() { }

        public MailBoxPacket(bool close, bool send)
        {
            Close = close;
            Send = send;
        }
        [MessagePack.Key(0)]
        public bool Close { get; set; }
        [MessagePack.Key(1)]
        public bool Send { get; set; }
    }
}
