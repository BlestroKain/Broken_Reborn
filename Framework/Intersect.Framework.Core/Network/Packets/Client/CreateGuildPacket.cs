
using MessagePack;


namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public partial class CreateGuildPacket : IntersectPacket
    {
        public CreateGuildPacket() { }

        public CreateGuildPacket(string name,
            string backgroundFile,
            byte bgR, byte bgG, byte bgB,
            string symbolFile,
            byte symR, byte symG, byte symB)
         
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

        }

        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public string LogoBackground { get; set; } = string.Empty;

        [Key(2)]
        public byte BackgroundR { get; set; } = 255;

        [Key(3)]
        public byte BackgroundG { get; set; } = 255;

        [Key(4)]
        public byte BackgroundB { get; set; } = 255;

        [Key(5)]
        public string LogoSymbol { get; set; } = string.Empty;

        [Key(6)]
        public byte SymbolR { get; set; } = 255;

        [Key(7)]
        public byte SymbolG { get; set; } = 255;

        [Key(8)]
        public byte SymbolB { get; set; } = 255;


    }
}
