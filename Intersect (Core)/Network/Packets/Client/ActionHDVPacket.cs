using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class ActionHDVPacket : IntersectPacket
        {
        public ActionHDVPacket(Guid hdvItemId, int action)
        {
            HdvItemId = hdvItemId;
            Action = action;
        }

        [Key(0)]
        public Guid HdvItemId { get; set; }
        [Key(1)]
        public int Action { get; set; }
    }
}
