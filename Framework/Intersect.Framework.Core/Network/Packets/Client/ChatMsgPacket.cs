using System.Collections.Generic;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class ChatMsgPacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public ChatMsgPacket()
    {
    }

    public ChatMsgPacket(string msg, byte channel, List<ChatItem>? items = null)
    {
        Message = msg;
        Channel = channel;
        Items = items ?? new List<ChatItem>();
    }

    [Key(0)]
    public string Message { get; set; }

    [Key(1)]
    public byte Channel { get; set; }

    [Key(2)]
    public List<ChatItem> Items { get; set; } = new List<ChatItem>();
}
