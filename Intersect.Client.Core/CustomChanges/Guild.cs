using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Guild;

namespace Intersect.Client.Entities;

public partial class Guild
{
    public static int GuildLevel { get; set; } = 1;
    public static long GuildExp { get; set; } = 0;
    public static long GuildExpToNextLevel { get; set; } = 0;
    public static int GuildPoints { get; set; }
    public static int GuildSpent { get; set; }

    // Niveles actuales por mejora
    public static Dictionary<GuildUpgradeType, int> GuildUpgrades = new();

    // Niveles máximos permitidos por mejora
    public static Dictionary<GuildUpgradeType, int> MaxUpgradeLevels = new()
    {
        { GuildUpgradeType.ExtraMembers, 5 },
        { GuildUpgradeType.ExtraBankSlots, 5 },
        { GuildUpgradeType.BonusXp, 3 },
        { GuildUpgradeType.BonusDrop, 5 },
        {GuildUpgradeType.BonusJobXp, 3 },
    };

    // Costos por nivel de mejora
    public static Dictionary<GuildUpgradeType, List<int>> UpgradeCosts = new()
    {
        { GuildUpgradeType.ExtraMembers, new List<int> {3, 5, 7, 9, 15} },
        { GuildUpgradeType.ExtraBankSlots, new List<int> { 1, 2, 3, 4, 5 } },
        { GuildUpgradeType.BonusXp, new List<int> { 6, 11, 20 } },
        { GuildUpgradeType.BonusDrop, new List<int> { 1, 2, 3, 4, 5 } },
        {GuildUpgradeType.BonusJobXp,new List<int>{ 5, 10, 15 } }
    };


    public static int GetUpgradeLevel(GuildUpgradeType type)
    {
        return GuildUpgrades.TryGetValue(type, out var level) ? level : 0;
    }
    public static int GetMaxMembers()
    {
        var baseMembers = Options.Instance.Guild.InitialMaxMembers; // valor base, por ejemplo 10
        var extra = 0;

        if (GuildUpgrades.TryGetValue(GuildUpgradeType.ExtraMembers, out var level))
        {
            extra = level * 10; // +10 miembros por nivel de mejora
        }

        return baseMembers + extra;
    }

}
