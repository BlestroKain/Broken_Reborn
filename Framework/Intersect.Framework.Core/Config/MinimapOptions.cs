using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Intersect.Config;

public partial class MinimapOptions
{
    /// <summary>
    /// Indicates whether or not the minimap window is enabled.
    /// </summary>
    public bool EnableMinimapWindow { get; set; }

    /// <summary>
    /// Determines whether waypoints should be drawn on the minimap.
    /// </summary>
    public bool ShowWaypoints { get; set; } = true;

    /// <summary>
    /// Configures the size at which each minimap tile is rendered.
    /// </summary>
    public Point TileSize { get; set; } = new(8, 8);

    private const float BaseDpi = 96f;
    private const int MinTileLength = 4;
    private const int MaxTileLength = 32;
    private const int MinZoomLimit = 1;
    private const int MaxZoomLimit = 1000;

    /// <summary>
    /// Returns the minimap tile size scaled for the provided DPI and
    /// clamped to a sensible visible range.
    /// </summary>
    public Point GetScaledTileSize(float dpi)
    {
        var scale = dpi / BaseDpi;
        var scaledX = (int)MathF.Round(TileSize.X * scale);
        var scaledY = (int)MathF.Round(TileSize.Y * scale);

        scaledX = Math.Clamp(scaledX, MinTileLength, MaxTileLength);
        scaledY = Math.Clamp(scaledY, MinTileLength, MaxTileLength);

        return new Point(scaledX, scaledY);
    }

    /// <summary>
    /// Configures the minimum zoom level.
    /// </summary>
    public int MinimumZoom { get; set; } = 20;

    /// <summary>
    /// Configures the maximum zoom level.
    /// </summary>
    public int MaximumZoom { get; set; } = 220;

    /// <summary>
    /// Configures the default zoom level.
    /// </summary>
    public int DefaultZoom { get; set; } = 100;

    /// <summary>
    /// Configures the amount to zoom by each step.
    /// </summary>
    public int ZoomStep { get; set; } = 10;

    /// <summary>
    /// Scales entity icons when drawn on the minimap. Values below 0.8 are
    /// clamped to 0.8 to keep icons visible.
    /// </summary>
    public float IconScale { get; set; } = 1.25f;

    /// <summary>
    /// Scales point of interest icons when drawn on the minimap.
    /// </summary>
    public float PoiIconScale { get; set; } = 1.5f;

    /// <summary>
    /// Maps point of interest types to their icon texture keys.
    /// </summary>
    public Dictionary<string, string> PoiIcons { get; set; } = new()
    {
        ["Bank"] = "poi_bank",
        ["Blacksmith"] = "poi_blacksmith",
        ["Jeweler"] = "poi_jeweler",
        ["Farming"] = "poi_farming",
        ["Mining"] = "poi_mining",
        ["Lumberjack"] = "poi_lumberjack",
        ["Fishing"] = "poi_fishing",
        ["Hunter"] = "poi_hunter",
        ["Cooking"] = "poi_cooking",
        ["Smithing"] = "poi_smithing",
        ["Alchemy"] = "poi_alchemy",
        ["Crafting"] = "poi_crafting",
        ["Jewerly"] = "poi_jewerly",
        ["Tanner"] = "poi_tanner",
        ["Tailoring"] = "poi_tailoring",
        ["Portal"] = "poi_portal",
        ["Tower"] = "poi_tower",
        ["Inn"] = "poi_inn",
        ["Market"] = "poi_market",
        ["Shop"] = "poi_shop",
        ["Default"] = "poi_default"
    };

    /// <summary>
    /// Configures the images used within the minimap. If any are left blank the system will default to its color.
    /// </summary>
    public Images MinimapImages { get; set; } = new();

    /// <summary>
    /// Configures the colours used within the minimap.
    /// </summary>
    public Colors MinimapColors { get; set; } = new();

    /// <summary>
    /// Configures which map layers the minimap will render.
    /// </summary>
    public List<string> RenderLayers { get; set; } = [
     
        "Ground",
        "Mask 1",
        "Mask 2",
        "Mask 3",
        "Mask 4",
        "Fringe 1",
        "Fringe 2",
        "Fringe 3",
              "Fringe 4",
        "Fringe 5"
    ];

    [OnDeserializing]
    internal void OnDeserializingMethod(StreamingContext context)
    {
        RenderLayers.Clear();
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        if (RenderLayers.Count == 0)
        {
            RenderLayers.AddRange([
               "Ground",
        "Mask 1",
        "Mask 2",
        "Mask 3",
        "Mask 4",
        "Fringe 1",
        "Fringe 2",
        "Fringe 3",
              "Fringe 4",
        "Fringe 5"
            ]);
        }

        Validate();
    }

    /// <summary>
    /// Validates the properties of the map options object.
    /// </summary>
    public void Validate()
    {
        MinimumZoom = Math.Clamp(MinimumZoom, MinZoomLimit, MaxZoomLimit);
        MaximumZoom = Math.Clamp(MaximumZoom, MinZoomLimit, MaxZoomLimit);

        if (MinimumZoom > MaximumZoom)
        {
            MaximumZoom = MinimumZoom;
        }

        DefaultZoom = Math.Clamp(DefaultZoom, MinimumZoom, MaximumZoom);
        if (IconScale < 0.8f) IconScale = 0.8f;
        if (PoiIconScale < 0.8f) PoiIconScale = 0.8f;
    }

    public class Colors
    {
        public Color Player { get; set; } = Color.Cyan;
        public Color PartyMember { get; set; } = Color.Blue;
        public Color MyEntity { get; set; } = Color.Red;
        public Color Npc { get; set; } = Color.Orange;
        public Color Event { get; set; } = Color.Blue;
        public Dictionary<JobType, Color> Resource { get; set; } = new()
        {
            { JobType.None, Color.White },
            { JobType.Lumberjack, Color.Orange },
            { JobType.Mining, Color.Gray },
            { JobType.Farming, Color.Green },
            { JobType.Fishing, Color.Cyan },
        };
        public Color Default { get; set; } = Color.Magenta;
    }

    public class Images
    {
        public string Player { get; set; } = "minimap_player.png";
        public string PartyMember { get; set; } = "minimap_partymember.png";
        public string MyEntity { get; set; } = "minimap_me.png";
        public string Npc { get; set; } = "minimap_npc.png";
        public string Event { get; set; } = "minimap_event.png";
        public Dictionary<JobType, string> Resource { get; set; } = new()
        {
            { JobType.None, "minimap_resource_none.png" },
            { JobType.Lumberjack, "minimap_resource_wood.png" },
            { JobType.Mining, "minimap_resource_mine.png" },
            { JobType.Farming, "minimap_resource_herb.png" },
            { JobType.Fishing, "minimap_resource_fish.png" },
        };
        public string Default { get; set; } = "minimap_npc.png";
    }
   
}
