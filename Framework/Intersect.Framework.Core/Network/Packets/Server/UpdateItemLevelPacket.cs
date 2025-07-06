using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class UpdateItemLevelPacket : IntersectPacket
    {
        [Key(0)]
        public int ItemIndex { get; set; }

        [Key(1)]
        public int NewEnchantmentLevel { get; set; }

        public UpdateItemLevelPacket() { }

        public UpdateItemLevelPacket(int itemId, int newEnchantmentLevel)
        {
            ItemIndex = itemId;
            NewEnchantmentLevel = newEnchantmentLevel;
        }
    }
}
