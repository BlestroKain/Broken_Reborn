using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Network.Packets;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class ChatMsgPacket : IntersectPacket
{
    public ChatMsgPacket(string message, ChatMessageType type, Color color, string target, List<ChatItem>? items = null)
    {
        Message = message;
        Type = type;
        Color = color;
        Target = target;
        Items = items ?? new List<ChatItem>();
    }

    [Key(0)]
    public string Message { get; set; }

    [Key(1)]
    public ChatMessageType Type { get; set; }

    [Key(2)]
    public Color Color { get; set; }

    [Key(3)]
    public string Target { get; set; }

    [Key(4)]
    public List<ChatItem> Items { get; set; } = new List<ChatItem>();
}
