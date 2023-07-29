using System;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class MailBoxUpdatePacket : IntersectPacket
    {
        public MailBoxUpdatePacket() { }

        public MailBoxUpdatePacket(Guid mailId, string name, string message, string senderName, Guid item,
            int quantity = 0)
        {
            MailID = mailId;
            Name = name;
            Message = message;
            SenderName = senderName;
            Item = item;
            Quantity = quantity;
        }

        [Key(0)] public Guid MailID { get; set; }
        [Key(1)] public string Name { get; set; }
        [Key(2)] public string Message { get; set; }
        [Key(3)] public string SenderName { get; set; }
        [Key(4)] public Guid Item { get; set; }
        [Key(5)] public int Quantity { get; set; }
    }
}