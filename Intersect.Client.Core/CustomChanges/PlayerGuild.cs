using Intersect.Client.Framework.Graphics;
using Intersect.Client.Networking;
using Intersect.Config;
using Intersect.Network.Packets.Server;


namespace Intersect.Client.Entities;

public partial class Player
{
    public string GuildBackgroundFile { get; set; }
    public byte GuildBackgroundR { get; set; }
    public byte GuildBackgroundG { get; set; }
    public byte GuildBackgroundB { get; set; } = 255;

    public string GuildSymbolFile { get; set; }
    public byte GuildSymbolR { get; set; }
    public byte GuildSymbolG { get; set; }
    public byte GuildSymbolB { get; set; }

    public float GuildXpContribution { get; set; }
    private IGameTexture _guildBackgroundTexture; // Changed to IGameTexture  
    private IGameTexture _guildSymbolTexture; // Changed to IGameTexture  
    private bool _logoLoaded = false;

    public void GetLogo(
        string backgroundFile,
        byte backgroundR,
        byte backgroundG,
        byte backgroundB,
        string symbolFile,
        byte symbolR,
        byte symbolG,
        byte symbolB
    )
    {
        // Guardar los valores en propiedades  
        GuildBackgroundFile = backgroundFile;
        GuildBackgroundR = backgroundR;
        GuildBackgroundG = backgroundG;
        GuildBackgroundB = backgroundB;

        GuildSymbolFile = symbolFile;
        GuildSymbolR = symbolR;
        GuildSymbolG = symbolG;
        GuildSymbolB = symbolB;
    }

    public void ConsultGuildLogo()
    {
        if (IsInGuild)
        {
            PacketSender.SendRequestGuild();
        }
    }
}
