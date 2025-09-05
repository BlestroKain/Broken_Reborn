using MessagePack;
using Intersect.Framework.Core.GameObjects.Zones;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class MapGridPacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public MapGridPacket()
    {
    }

    public MapGridPacket(
        Guid[,] grid,
        string[,] editorGrid,
        bool clearKnownMaps,
        Guid?[,] zoneIds = null,
        Guid?[,] subzoneIds = null,
        ZoneModifiers?[,] modifiers = null
    )
    {
        Grid = grid;
        EditorGrid = editorGrid;
        ClearKnownMaps = clearKnownMaps;
        ZoneIds = zoneIds;
        SubzoneIds = subzoneIds;
        Modifiers = modifiers;
    }

    [Key(0)]
    public Guid[,] Grid { get; set; }

    [Key(1)]
    public string[,] EditorGrid { get; set; }

    [Key(2)]
    public bool ClearKnownMaps { get; set; }

    [Key(3)]
    public Guid?[,] ZoneIds { get; set; }

    [Key(4)]
    public Guid?[,] SubzoneIds { get; set; }

    [Key(5)]
    public ZoneModifiers?[,] Modifiers { get; set; }
}
