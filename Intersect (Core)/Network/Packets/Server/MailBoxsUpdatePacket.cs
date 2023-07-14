using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    public partial class MailBoxsUpdatePacket
    {
        public MailBoxsUpdatePacket() { }

        public MailBoxsUpdatePacket(MailBoxUpdatePacket[] mailboxs)
        {
            Mails = mailboxs;
        }

        public MailBoxUpdatePacket[] Mails { get; set; }
    }
}
