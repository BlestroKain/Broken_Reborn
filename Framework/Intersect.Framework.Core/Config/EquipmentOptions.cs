using System.Runtime.Serialization;
using System.Linq;

namespace Intersect.Config;

public partial class EquipmentOptions
{
    public PaperdollOptions Paperdoll { get; set; } = new();

    public int ShieldSlot { get; set; } = 3;

    public List<EquipmentSlotOptions> EquipmentSlots { get; set; } =
    [
        new("Helmet"),
        new("Armor"),
        new("Weapon"),
        new("Shield"),
        new("Boots"),
        new("Ring", 2),
        new("Trophy", 5),
    ];

    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public List<string> Slots => [..EquipmentSlots.Select(es => es.Name)];

    public List<string> ToolTypes { get; set; } =
    [
        "Axe",
        "Pickaxe",
        "Shovel",
        "Fishing Rod",
    ];

    public int WeaponSlot = 2;

    [OnDeserializing]
    internal void OnDeserializingMethod(StreamingContext context)
    {
        EquipmentSlots.Clear();
        ToolTypes.Clear();
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        if (EquipmentSlots.Count == 0 && Slots?.Count > 0)
        {
            foreach (var name in Slots)
            {
                EquipmentSlots.Add(new EquipmentSlotOptions(name));
            }
        }

        Validate();
    }

    public void Validate()
    {
        EquipmentSlots = [..EquipmentSlots.DistinctBy(es => es.Name)];
        foreach (var slot in EquipmentSlots)
        {
            slot.Validate();
        }
        ToolTypes = [..ToolTypes.Distinct()];
        if (WeaponSlot < -1 || WeaponSlot > EquipmentSlots.Count - 1)
        {
            throw new Exception("Config Error: (WeaponSlot) was out of bounds!");
        }

        if (ShieldSlot < -1 || ShieldSlot > EquipmentSlots.Count - 1)
        {
            throw new Exception("Config Error: (ShieldSlot) was out of bounds!");
        }

        Paperdoll.Validate(this);
    }
}
