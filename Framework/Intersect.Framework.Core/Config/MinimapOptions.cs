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
    /// Configures the size at which each minimap tile is rendered.
    /// </summary>
    public Point TileSize { get; set; } = new(8, 8);

    /// <summary>
    /// Configures the minimum zoom level (0 - 100)
    /// </summary>
    public byte MinimumZoom { get; set; } = 5;

    /// <summary>
    /// Configures the maximum zoom level (0 - 100)
    /// </summary>
    public byte MaximumZoom { get; set; } = 100;

    /// <summary>
    /// Configures the default zoom level (0 - 100)
    /// </summary>
    public byte DefaultZoom { get; set; } = 25;

    /// <summary>
    /// Configures the amount to zoom by each step.
    /// </summary>
    public byte ZoomStep { get; set; } = 5;

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
        if (MinimumZoom is < 0 or > 100)
        {
            MinimumZoom = 0;
        }

        if (MaximumZoom is < 0 or > 100)
        {
            MaximumZoom = 100;
        }

        if (MinimumZoom > MaximumZoom)
        {
            MaximumZoom = MinimumZoom;
        }

        DefaultZoom = Math.Clamp(DefaultZoom, MinimumZoom, MaximumZoom);
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
            { JobType.Lumberjack, Color.SaddleBrown },
            { JobType.Mining, Color.Gray },
            { JobType.Farming, Color.Green },
            { JobType.Fishing, Color.CornflowerBlue },
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
