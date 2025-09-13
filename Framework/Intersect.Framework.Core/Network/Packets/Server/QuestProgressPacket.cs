using MessagePack;
using Intersect.Enums;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class QuestProgressPacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public QuestProgressPacket()
    {
    }

    public QuestProgressPacket(Dictionary<Guid, string> quests, Guid[] hiddenQuests,
        Dictionary<Guid, Dictionary<Guid, int>> questRewardItems,
        Dictionary<Guid, long> questRewardExperience,
        Dictionary<Guid, Dictionary<JobType, long>> questRewardJobExperience,
        Dictionary<Guid, long> questRewardGuildExperience,
        Dictionary<Guid, Dictionary<Factions, int>> questRewardFactionHonor)
    {
        Quests = quests;
        HiddenQuests = hiddenQuests;
        QuestRewardItems = questRewardItems;
        QuestRewardExperience = questRewardExperience;
        QuestRewardJobExperience = questRewardJobExperience;
        QuestRewardGuildExperience = questRewardGuildExperience;
        QuestRewardFactionHonor = questRewardFactionHonor;
    }

    [Key(0)]
    public Dictionary<Guid, string> Quests { get; set; }

    [Key(1)]
    public Guid[] HiddenQuests { get; set; }

    [Key(2)]
    public Dictionary<Guid, Dictionary<Guid, int>> QuestRewardItems { get; set; }

    [Key(3)]
    public Dictionary<Guid, long> QuestRewardExperience { get; set; }

    [Key(4)]
    public Dictionary<Guid, Dictionary<JobType, long>> QuestRewardJobExperience { get; set; }

    [Key(5)]
    public Dictionary<Guid, long> QuestRewardGuildExperience { get; set; }

    [Key(6)]
    public Dictionary<Guid, Dictionary<Factions, int>> QuestRewardFactionHonor { get; set; }
}

