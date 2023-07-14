using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class TakeMailPacket : IntersectPacket
    {
        public TakeMailPacket() { }

        public TakeMailPacket(Guid mailId)
        {
            MailID = mailId;
        }
        [Key(0)]
        public Guid MailID { get; private set; }
    }
}
