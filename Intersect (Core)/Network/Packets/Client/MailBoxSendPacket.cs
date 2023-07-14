using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class MailBoxSendPacket : IntersectPacket
    {
        public MailBoxSendPacket()
        {
        }

        public MailBoxSendPacket(string to, string title, string message, int slotID, int quantity)
        {
            To = to;
            Title = title;
            Message = message;
            SlotID = slotID;
            Quantity = quantity;
        }
        [Key(0)]
        public string To { get; set; }
        [Key(1)]
        public string Title { get; set; }
        [Key(2)]
        public string Message { get; set; }
        [Key(3)]
        public int SlotID { get; set; }
        [Key(4)]
        public int Quantity { get; set; }
    }

}
