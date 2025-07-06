using Intersect.Network;
using MessagePack;

[MessagePackObject]
public partial class GuildUpdate : IntersectPacket
{
    public GuildUpdate(
        string name,
        string backgroundFile,
        byte bgR, byte bgG, byte bgB,
        string symbolFile,
        byte symR, byte symG, byte symB,
        int level,
        long exp,
        long expToNextLevel,
        int guildPoints,
        int spentGuildPoints,
        Dictionary<string, int> guildUpgrades
    )
    {
        Name = name;
        LogoBackground = backgroundFile ?? string.Empty;
        BackgroundR = bgR;
        BackgroundG = bgG;
        BackgroundB = bgB;
        LogoSymbol = symbolFile ?? string.Empty;
        SymbolR = symR;
        SymbolG = symG;
        SymbolB = symB;
        GuildLevel = level;
        GuildExp = exp;
        GuildExpToNextLevel = expToNextLevel;
        GuildPoints = guildPoints;
        SpentGuildPoints = spentGuildPoints;
        GuildUpgrades = guildUpgrades ?? new();
    }

    [Key(0)] public string Name { get; set; }
    [Key(1)] public string LogoBackground { get; set; }
    [Key(2)] public byte BackgroundR { get; set; }
    [Key(3)] public byte BackgroundG { get; set; }
    [Key(4)] public byte BackgroundB { get; set; }
    [Key(5)] public string LogoSymbol { get; set; }
    [Key(6)] public byte SymbolR { get; set; }
    [Key(7)] public byte SymbolG { get; set; }
    [Key(8)] public byte SymbolB { get; set; }
    [Key(9)] public int GuildLevel { get; set; }
    [Key(10)] public long GuildExp { get; set; }
    [Key(11)] public long GuildExpToNextLevel { get; set; }

    // NUEVOS CAMPOS
    [Key(12)] public int GuildPoints { get; set; }
    [Key(13)] public int SpentGuildPoints { get; set; }
    [Key(14)] public Dictionary<string, int> GuildUpgrades { get; set; } = new();
}
