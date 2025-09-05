using Intersect.Client.General;
using Intersect.Client.Framework.Database;
using Newtonsoft.Json;

namespace Intersect.Client.Interface.Game.Map;

/// <summary>
/// Stores user map preferences such as minimap zoom. Handles serialization to the client
/// database.
/// </summary>
public class MapPreferences
{
    private const string PreferenceKey = nameof(MapPreferences);

    /// <summary>
    /// The last zoom level used by the minimap window.
    /// </summary>
    public int MinimapZoom { get; set; } = Options.Instance?.Minimap.DefaultZoom ?? 0;

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

}

