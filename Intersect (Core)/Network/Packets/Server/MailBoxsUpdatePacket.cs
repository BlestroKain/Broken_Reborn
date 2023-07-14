using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MailBoxsUpdatePacket : IntersectPacket
    {
        public MailBoxsUpdatePacket(MailBoxUpdatePacket[] mailboxs)
        {
            Mails = mailboxs;
        }

        [Key(0)]
        public MailBoxUpdatePacket[] Mails { get; set; }
    }
}
