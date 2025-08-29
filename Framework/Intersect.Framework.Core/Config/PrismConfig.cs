using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Config;

/// <summary>
///     Handles loading and saving of prism definitions that are shared by the server and client.
/// </summary>
public static class PrismConfig
{
    /// <summary>
    ///     Collection of configured prisms.
    /// </summary>
    public static List<AlignmentPrism> Prisms { get; private set; } = new();

    private static string PrismPath => Path.Combine(Options.ResourcesDirectory, "prisms.json");

    /// <summary>
    ///     Loads prism definitions from disk if available.
    /// </summary>
    public static void Load()
    {
        if (!File.Exists(PrismPath))
        {
            Prisms = new();
            return;
        }

        var json = File.ReadAllText(PrismPath);
        Prisms = JsonConvert.DeserializeObject<List<AlignmentPrism>>(json) ?? new();
    }

    /// <summary>
    ///     Persists prism definitions to disk.
    /// </summary>
    public static void Save()
    {
        if (!Directory.Exists(Options.ResourcesDirectory))
        {
            Directory.CreateDirectory(Options.ResourcesDirectory);
        }

        var json = JsonConvert.SerializeObject(Prisms, Formatting.Indented);
        File.WriteAllText(PrismPath, json);
    }
}
