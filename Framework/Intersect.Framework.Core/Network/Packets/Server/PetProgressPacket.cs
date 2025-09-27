using System;
using MessagePack;

namespace Intersect.Network.Packets.Server;

/// <summary>
///     Broadcasts progression related information for a pet to its owner.
/// </summary>
[MessagePackObject]
public sealed class PetProgressPacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public PetProgressPacket()
    {
    }

    public PetProgressPacket(
        Guid petId,
        long experience,
        long experienceToNextLevel,
        int statPoints,
        int[] statPointAllocations,
        int energy,
        int mood,
        int maturity
    )
    {
        PetId = petId;
        Experience = experience;
        ExperienceToNextLevel = experienceToNextLevel;
        StatPoints = statPoints;
        StatPointAllocations = statPointAllocations ?? Array.Empty<int>();
        Energy = energy;
        Mood = mood;
        Maturity = maturity;
    }

    [Key(0)]
    public Guid PetId { get; set; }

    [Key(1)]
    public long Experience { get; set; }

    [Key(2)]
    public long ExperienceToNextLevel { get; set; }

    [Key(3)]
    public int StatPoints { get; set; }

    [Key(4)]
    public int[] StatPointAllocations { get; set; } = Array.Empty<int>();

    [Key(5)]
    public int Energy { get; set; }

    [Key(6)]
    public int Mood { get; set; }

    [Key(7)]
    public int Maturity { get; set; }
}
