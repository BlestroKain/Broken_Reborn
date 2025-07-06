using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intersect.Framework.Core.GameObjects.Guild;
using Intersect.Network;
using MessagePack;

namespace Intersect.Network.Packets.Client;
[MessagePackObject]
public class ApplyGuildUpgradePacket : IntersectPacket
{
    public ApplyGuildUpgradePacket()
    {

    }
   
    public ApplyGuildUpgradePacket(GuildUpgradeType upgradeType)
    {
        UpgradeType = upgradeType;
    }
    [Key(0)]
    public GuildUpgradeType UpgradeType { get; set; }
}
