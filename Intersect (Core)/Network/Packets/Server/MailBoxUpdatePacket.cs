using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    public partial class MailBoxUpdatePacket
    {
        public MailBoxUpdatePacket() { }

        public MailBoxUpdatePacket(Guid mailId, string name, string message, string senderName, Guid item, int quantity = 0)
        {
            MailID = mailId;
            Name = name;
            Message = message;
            SenderName = senderName;
            Item = item;
            Quantity = quantity;
        }

        public Guid MailID { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string SenderName { get; set; }

        public Guid Item { get; set; }

        public int Quantity { get; set; }
    }
}
