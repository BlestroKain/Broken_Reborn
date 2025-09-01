using System;
using System.Collections.Generic;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MapDiscoveriesPacket : IntersectPacket
{
    public MapDiscoveriesPacket()
    {
    }

    public MapDiscoveriesPacket(Dictionary<Guid, byte[]> discoveries)
    {
        Discoveries = discoveries;
    }

    [Key(0)]
    public Dictionary<Guid, byte[]> Discoveries { get; set; } = new();
}

