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

    [JsonIgnore]
    public ItemRange StatRange_Agility
    {
        get => StatRanges.TryGetValue(Stat.Agility, out var range) ? range : StatRange_Agility = new ItemRange();
        set => StatRanges[Stat.Agility] = value;
    }

    [JsonIgnore]
    public ItemRange StatRange_Damages
    {
        get => StatRanges.TryGetValue(Stat.Damages, out var range) ? range : StatRange_Damages = new ItemRange();
        set => StatRanges[Stat.Damages] = value;
    }

    [JsonIgnore]
    public ItemRange StatRange_Cures
    {
        get => StatRanges.TryGetValue(Stat.Cures, out var range) ? range : StatRange_Cures = new ItemRange();
        set => StatRanges[Stat.Cures] = value;
    }
}