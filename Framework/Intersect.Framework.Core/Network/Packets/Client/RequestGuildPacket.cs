using Intersect.Framework.Core.GameObjects.Guild;
using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class RequestGuildPacket : IntersectPacket
{
    /// <summary>
    /// Parameterless Constructor for MessagePack
    /// </summary>
    public RequestGuildPacket()
    {

    }
    public RequestGuildPacket(string backgroundFile, byte backgroundR, byte backgroundG, byte backgroundB, string symbolFile, byte symbolR, byte symbolG, byte symbolB)
    {
        BackgroundFile = backgroundFile;
        BackgroundR = backgroundR;
        BackgroundG = backgroundG;
        BackgroundB = backgroundB;
        SymbolFile = symbolFile;
        SymbolR = symbolR;
        SymbolG = symbolG;
        SymbolB = symbolB;
      
    }
    [Key(0)]
    public string BackgroundFile { get; }
    [Key(1)]
    public byte BackgroundR { get; }
    [Key(2)]
    public byte BackgroundG { get; }
    [Key(3)]
    public byte BackgroundB { get; }
    [Key(4)]
    public string SymbolFile { get; }
    [Key(5)]
    public byte SymbolR { get; }
    [Key(6)]
    public byte SymbolG { get; }
    [Key(7)]
    public byte SymbolB { get; }
   
}
