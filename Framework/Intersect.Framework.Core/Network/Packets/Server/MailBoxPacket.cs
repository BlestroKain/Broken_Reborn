using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MailBoxPacket : IntersectPacket
    {
        public MailBoxPacket(bool close, bool send)
        {
            Close = close;
            Send = send;
        }

        [Key(0)] public bool Close { get; set; }
        [Key(1)] public bool Send { get; set; }
    }
}
