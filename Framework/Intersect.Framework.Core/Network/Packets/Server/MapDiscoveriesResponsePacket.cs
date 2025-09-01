using System;
using System.Collections.Generic;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MapDiscoveriesResponsePacket : IntersectPacket
{
    public MapDiscoveriesResponsePacket()
    {
    }

    public MapDiscoveriesResponsePacket(Dictionary<Guid, byte[]> discoveries)
    {
        Discoveries = discoveries;
    }

    [Key(0)]
    public Dictionary<Guid, byte[]> Discoveries { get; set; } = new();
}

