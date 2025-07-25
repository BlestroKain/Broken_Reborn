using Intersect.Network.Packets.Server;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class MailBoxSendPacket : IntersectPacket
    {
        public MailBoxSendPacket() { }

        public MailBoxSendPacket(string to, string title, string message, List<MailAttachmentPacket> attachments)
        {
            To = to;
            Title = title;
            Message = message;
            Attachments = attachments ?? new List<MailAttachmentPacket>();
        }

        [Key(0)] public string To { get; set; }
        [Key(1)] public string Title { get; set; }
        [Key(2)] public string Message { get; set; }
        [Key(3)] public List<MailAttachmentPacket> Attachments { get; set; }
    }
}
