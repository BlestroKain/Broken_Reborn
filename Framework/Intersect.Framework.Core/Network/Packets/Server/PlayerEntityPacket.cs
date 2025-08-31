using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Events;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class PlayerEntityPacket : EntityPacket
{
    //Parameterless Constructor for MessagePack
    public PlayerEntityPacket()
    {
    }


    [Key(24)]
    public Access AccessLevel { get; set; }


    [Key(25)]
    public Gender Gender { get; set; }


    [Key(26)]
    public Guid ClassId { get; set; }


    [Key(27)]
    public EquipmentPacket Equipment { get; set; }


    [Key(28)]
    public long CombatTimeRemaining { get; set; }


    [Key(29)]
    public string Guild { get; set; }


    [Key(30)]
    public int GuildRank { get; set; }
    [Key(31)]
    public string GuildSymbolFile { get; set; }

    [Key(32)]
    public byte GuildSymbolR { get; set; }

    [Key(33)]
    public byte GuildSymbolG { get; set; }

    [Key(34)]
    public byte GuildSymbolB { get; set; }

    [Key(35)]
    public float GuildSymbolScale { get; set; }

    [Key(36)]
    public int GuildSymbolPosY { get; set; }

    [Key(37)]
    public string GuildBackgroundFile { get; set; }

    [Key(38)]
    public byte GuildBackgroundR { get; set; }

    [Key(39)]
    public byte GuildBackgroundG { get; set; }

    [Key(40)]
    public byte GuildBackgroundB { get; set; }

    [Key(41)]
    public SpellUpdatePacket[] Spells { get; set; } = [];

    [Key(42)]
    public int SpellPoints { get; set; }

    [Key(43)]
    public Factions Faction { get; set; }

    [Key(44)]
    public WingState Wings { get; set; }

    [Key(45)]
    public int Honor { get; set; }

    [Key(46)]
    public int Grade { get; set; }
}
