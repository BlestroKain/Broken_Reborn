using System;
using System.Collections.Generic;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class MapDiscoveriesRequestPacket : IntersectPacket
{
    public MapDiscoveriesRequestPacket()
    {
    }

    public MapDiscoveriesRequestPacket(Dictionary<Guid, byte[]> discoveries)
    {
        Discoveries = discoveries;
    }

    [Key(0)]
    public Dictionary<Guid, byte[]> Discoveries { get; set; } = new();
}
