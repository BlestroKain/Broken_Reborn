using System;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class CreateMarketListingPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public CreateMarketListingPacket()
    {
    }

    public CreateMarketListingPacket(int itemSlot, int quantity, long price)
    {
        ItemSlot = itemSlot;
        Quantity = quantity;
        Price = price;
    }

    [Key(0)]
    public int ItemSlot { get; set; }

    [Key(1)]
    public int Quantity { get; set; }

    [Key(2)]
    public long Price { get; set; }
}
