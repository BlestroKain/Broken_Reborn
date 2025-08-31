using System.Collections.Generic;
using Intersect;
using Intersect.Client.General;
using Intersect.Client.Framework.Database;
using Newtonsoft.Json;

namespace Intersect.Client.Interface.Game.Map;

/// <summary>
/// Stores user map preferences such as minimap zoom, world map filters, and the last
/// position/zoom of the world map window. Handles serialization to the client database.
/// </summary>
public class MapPreferences
{
    private const string PreferenceKey = nameof(MapPreferences);

    /// <summary>
    /// The last zoom level used by the minimap window.
    /// </summary>
    public int MinimapZoom { get; set; } = Options.Instance?.Minimap.DefaultZoom ?? 0;

    /// <summary>
    /// Active world map filters keyed by filter identifier.
    /// </summary>
    public Dictionary<string, bool> ActiveFilters { get; set; } = new();

    /// <summary>
    /// The last canvas position of the world map window.
    /// </summary>
    public Point WorldMapPosition { get; set; } = Point.Empty;

    /// <summary>
    /// The last zoom level of the world map window.
    /// </summary>
    public float WorldMapZoom { get; set; } = 1f;

    /// <summary>
    /// The loaded instance of <see cref="MapPreferences" />.
    /// </summary>
    public static MapPreferences Instance { get; private set; } = new();

    /// <summary>
    /// Load preferences from the client database.
    /// </summary>
    public static void Load()
    {
        var json = Globals.Database.LoadPreference(PreferenceKey);
        if (string.IsNullOrEmpty(json))
        {
            Instance = new MapPreferences();
            return;
        }

        try
        {
            Instance = JsonConvert.DeserializeObject<MapPreferences>(json) ?? new MapPreferences();
        }
        catch
        {
            Instance = new MapPreferences();
        }
    }

    /// <summary>
    /// Persist preferences to the client database.
    /// </summary>
    public static void Save()
    {
        var json = JsonConvert.SerializeObject(Instance);
        Globals.Database.SavePreference(PreferenceKey, json);
    }

    /// <summary>
    /// Update the minimap zoom preference and persist it.
    /// </summary>
    /// <param name="zoom">New minimap zoom level.</param>
    public static void UpdateMinimapZoom(int zoom)
    {
        Instance.MinimapZoom = zoom;
        Save();
    }

    /// <summary>
    /// Update the world map canvas position preference and persist it.
    /// </summary>
    /// <param name="position">New position for the world map canvas.</param>
    public static void UpdateWorldMapPosition(Point position)
    {
        Instance.WorldMapPosition = position;
        Save();
    }

    /// <summary>
    /// Update the world map zoom preference and persist it.
    /// </summary>
    /// <param name="zoom">New world map zoom level.</param>
    public static void UpdateWorldMapZoom(float zoom)
    {
        Instance.WorldMapZoom = zoom;
        Save();
    }

    /// <summary>
    /// Update a world map filter preference and persist it.
    /// </summary>
    /// <param name="key">Filter identifier.</param>
    /// <param name="enabled">Whether the filter is enabled.</param>
    public static void UpdateFilter(string key, bool enabled)
    {
        Instance.ActiveFilters[key] = enabled;
        Save();
    }
}

