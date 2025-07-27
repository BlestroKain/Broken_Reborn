using System;

namespace Intersect.Config;

public partial class EquipmentSlotOptions
{
    public EquipmentSlotOptions()
    {
    }

    public EquipmentSlotOptions(string name, int maxItems = 1)
    {
        Name = name;
        MaxItems = maxItems;
    }

    public string Name { get; set; } = string.Empty;

    public int MaxItems { get; set; } = 1;

    public void Validate()
    {
        if (MaxItems < 1)
        {
            throw new Exception($"Config Error: Equipment slot '{Name}' must allow at least one item.");
        }
    }
}
