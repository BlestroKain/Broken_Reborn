using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    public partial class MailBoxPacket
    {
        public MailBoxPacket() { }

        public MailBoxPacket(bool close, bool send)
        {
            Close = close;
            Send = send;
        }
        public bool Close { get; set; }

        public bool Send { get; set; }
    }
}
