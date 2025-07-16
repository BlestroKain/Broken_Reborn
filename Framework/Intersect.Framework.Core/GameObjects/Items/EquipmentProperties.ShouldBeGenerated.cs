using Intersect.Enums;
using Intersect.GameObjects.Ranges;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace Intersect.Framework.Core.GameObjects.Items;

public partial class EquipmentProperties
{
    [JsonIgnore]
    public ItemRange StatRange_Attack
    {
        get => StatRanges.TryGetValue(Stat.Attack, out var range) ? range : StatRange_Attack = new ItemRange();
        set => StatRanges[Stat.Attack] = value;
    }

    [JsonIgnore]
    public ItemRange StatRange_Intelligence
    {
        get =>
            StatRanges.TryGetValue(Stat.Intelligence, out var range) ? range : StatRange_Intelligence = new ItemRange();
        set => StatRanges[Stat.Intelligence] = value;
    }

    [JsonIgnore]
    public ItemRange StatRange_Defense
    {
        get => StatRanges.TryGetValue(Stat.Defense, out var range) ? range : StatRange_Defense = new ItemRange();
        set => StatRanges[Stat.Defense] = value;
    }

    [JsonIgnore]
    public ItemRange StatRange_Vitality
    {
        get =>
            StatRanges.TryGetValue(Stat.Vitality, out var range) ? range : StatRange_Vitality = new ItemRange();
        set => StatRanges[Stat.Vitality] = value;
    }

    [JsonIgnore]
    public ItemRange StatRange_Speed
    {
        get => StatRanges.TryGetValue(Stat.Speed, out var range) ? range : StatRange_Speed = new ItemRange();
        set => StatRanges[Stat.Speed] = value;
    }
}