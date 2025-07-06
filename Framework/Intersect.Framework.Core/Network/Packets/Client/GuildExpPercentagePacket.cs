using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class GuildExpPercentagePacket:IntersectPacket
    {
        public GuildExpPercentagePacket() { }
        public GuildExpPercentagePacket(float percentage)
        {
            Percentage = percentage;
        }
        [Key(0)]
        public float Percentage { get; set; }
    }
}
