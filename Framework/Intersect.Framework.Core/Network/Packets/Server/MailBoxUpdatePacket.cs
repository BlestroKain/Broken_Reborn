using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Items;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class MailBoxUpdatePacket : IntersectPacket
    {
        public MailBoxUpdatePacket() { }

        public MailBoxUpdatePacket(Guid mailId, string name, string message, string senderName, List<MailAttachmentPacket> attachments)
        {
            MailID = mailId;
            Name = name;
            Message = message;
            SenderName = senderName;
            Attachments = attachments ?? new List<MailAttachmentPacket>();
        }

        [Key(0)] public Guid MailID { get; set; }
        [Key(1)] public string Name { get; set; }
        [Key(2)] public string Message { get; set; }
        [Key(3)] public string SenderName { get; set; }
        [Key(4)] public List<MailAttachmentPacket> Attachments { get; set; }
    }

    [MessagePackObject]
    public class MailAttachmentPacket
    {
        [Key(0)] public Guid ItemId { get; set; }
        [Key(1)] public int Quantity { get; set; }
        [Key(2)] public ItemProperties Properties { get; set; }
    }
}
