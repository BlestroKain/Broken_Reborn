using MessagePack;
using Intersect.Enums;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class QuestOfferPacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public QuestOfferPacket()
    {
    }

    public QuestOfferPacket(Guid questId, Dictionary<Guid, int> questRewardItems, long questRewardExperience,
        Dictionary<JobType, long> questRewardJobExperience, long questRewardGuildExperience,
        Dictionary<Factions, int> questRewardFactionHonor)
    {
        QuestId = questId;
        QuestRewardItems = questRewardItems;
        QuestRewardExperience = questRewardExperience;
        QuestRewardJobExperience = questRewardJobExperience;
        QuestRewardGuildExperience = questRewardGuildExperience;
        QuestRewardFactionHonor = questRewardFactionHonor;
    }

    [Key(0)]
    public Guid QuestId { get; set; }

    [Key(1)]
    public Dictionary<Guid, int> QuestRewardItems { get; set; }

    [Key(2)]
    public long QuestRewardExperience { get; set; }

    [Key(3)]
    public Dictionary<JobType, long> QuestRewardJobExperience { get; set; }

    [Key(4)]
    public long QuestRewardGuildExperience { get; set; }

    [Key(5)]
    public Dictionary<Factions, int> QuestRewardFactionHonor { get; set; }
}

