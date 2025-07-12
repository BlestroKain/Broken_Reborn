using Intersect.Enums;
using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Items;

[MessagePackObject]
public partial class ItemProperties
{
    public ItemProperties()
    {
    }

    public ItemProperties(ItemProperties other)
    {
        if (other == default)
        {
            throw new ArgumentNullException(nameof(other));
        }

        EnchantmentLevel = other.EnchantmentLevel;
        Array.Copy(other.StatModifiers, StatModifiers, Enum.GetValues<Stat>().Length);
        Array.Copy(other.VitalModifiers, VitalModifiers, Enum.GetValues<Vital>().Length);
      
    }

    [Key(0)]
    public int[] StatModifiers { get; set; } = new int[Enum.GetValues<Stat>().Length];

    [Key(1)]
    public int EnchantmentLevel { get; set; }  // Nivel de encantamiento  
    [Key(2)]
    public Dictionary<int, int[]> EnchantmentRolls { get; set; } = new Dictionary<int, int[]>();
    [Key(3)]
    public int[] VitalModifiers { get; set; } = new int[Enum.GetValues<Vital>().Length];
    [Key(4)]
    public int MageSink { get;  set; }
}
