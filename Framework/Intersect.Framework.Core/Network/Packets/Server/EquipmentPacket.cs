using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class EquipmentPacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public EquipmentPacket()
    {
    }

    //We send item ids for the equipment of others, but we send the correlating inventory slots for wearers because we want the equipped icons to show in the inventory.
    public EquipmentPacket(Guid entityId, Dictionary<int, List<int>>? invSlots, Dictionary<int, List<Guid>>? itemIds)
    {
        EntityId = entityId;
        InventorySlots = invSlots;
        ItemIds = itemIds;
    }

    [Key(0)]
    public Guid EntityId { get; set; }

    [Key(1)]
    public Dictionary<int, List<int>>? InventorySlots { get; set; }

    [Key(2)]
    public Dictionary<int, List<Guid>>? ItemIds { get; set; }

}
