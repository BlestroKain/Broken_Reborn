using System.Collections.Generic;
using System.IO;
using Intersect.Core;
using Microsoft.Extensions.Logging;
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
    public static List<PrismDescriptor> Prisms { get; private set; } = new();

    private static string PrismPath => Path.Combine(Options.ResourcesDirectory, "Config", "prisms.json");

    private class PrismConfigData
    {
        public int Version { get; set; } = 1;

        public List<PrismDescriptor> Prisms { get; set; } = new();
    }

    /// <summary>
    ///     Loads prism definitions from disk if available.
    /// </summary>
    public static void Load()
    {
        if (!File.Exists(PrismPath))
        {
            Prisms = new();
            Save();
            PrismDescriptorStore.Load(Prisms);
            ApplicationContext.Context.Value?.Logger.LogInformation(
                "Created prism configuration at {PrismPath}",
                PrismPath
            );
            return;
        }

        var json = File.ReadAllText(PrismPath);
        if (json.TrimStart().StartsWith("{"))
        {
            var config = JsonConvert.DeserializeObject<PrismConfigData>(json);
            Prisms = config?.Prisms ?? new();
        }
        else
        {
            Prisms = JsonConvert.DeserializeObject<List<PrismDescriptor>>(json) ?? new();
        }

        PrismDescriptorStore.Load(Prisms);
    }

    /// <summary>
    ///     Persists prism definitions to disk.
    /// </summary>
    public static void Save()
    {
        var directory = Path.Combine(Options.ResourcesDirectory, "Config");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonConvert.SerializeObject(new PrismConfigData { Prisms = Prisms }, Formatting.Indented);
        File.WriteAllText(PrismPath, json);
    }
}
