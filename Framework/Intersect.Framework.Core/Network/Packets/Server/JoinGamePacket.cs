using System;
using System.Collections.Generic;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class JoinGamePacket : AbstractTimedPacket
{
    public JoinGamePacket()
    {
        MapDiscovery = new Dictionary<Guid, byte[]>();
    }

    public JoinGamePacket(Dictionary<Guid, byte[]> discovery)
    {
        MapDiscovery = discovery;
    }

    [Key(0)]
    public Dictionary<Guid, byte[]> MapDiscovery { get; set; }
}
