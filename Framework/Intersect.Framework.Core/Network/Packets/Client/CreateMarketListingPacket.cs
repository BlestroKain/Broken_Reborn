using System;
using Intersect.Framework.Core.GameObjects.Items;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class CreateMarketListingPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public CreateMarketListingPacket()
    {
    }

    public CreateMarketListingPacket(int itemSlot, int quantity, long price, ItemProperties properties)
    {
        ItemSlot = itemSlot;
        Quantity = quantity;
        Price = price;
        Properties = properties;
    }

    [Key(0)]
    public int ItemSlot { get; set; }

    [Key(1)]
    public int Quantity { get; set; }

    [Key(2)]
    public long Price { get; set; }

    [Key(3)]
    public ItemProperties Properties { get; set; } = new();
}
