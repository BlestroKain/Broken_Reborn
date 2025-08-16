using System;
using Intersect.Framework.Core.GameObjects.Items;
using MessagePack;

namespace Intersect.Network.Packets;

[MessagePackObject]
public partial class ChatItem
{
    public ChatItem()
    {
    }

    public ChatItem(Guid itemId, ItemProperties properties)
    {
        ItemId = itemId;
        Properties = properties;
    }

    [Key(0)]
    public Guid ItemId { get; set; }

    [Key(1)]
    public ItemProperties Properties { get; set; } = new ItemProperties();
}
