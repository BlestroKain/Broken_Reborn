using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intersect.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Config;

/// <summary>
///     Provides access to <see cref="PrismDescriptor"/> definitions stored on disk.
/// </summary>
public static class PrismDescriptorStore
{
    /// <summary>
    ///     Cached collection of prism descriptors.
    /// </summary>
    public static List<PrismDescriptor> Prisms { get; private set; } = new();

    private static string PrismPath => Path.Combine(Options.ResourcesDirectory, "Config", "prisms.json");

    private class PrismConfigData
    {
        public int Version { get; set; } = 1;

        public List<PrismDescriptor> Prisms { get; set; } = new();
    }

    /// <summary>
    ///     Loads prism descriptors from disk and returns the cached collection.
    /// </summary>
    public static IReadOnlyList<PrismDescriptor> LoadAll()
    {
        if (!File.Exists(PrismPath))
        {
            Prisms = new();
            SaveAll();
            ApplicationContext.Context.Value?.Logger.LogInformation(
                "Created prism configuration at {PrismPath}",
                PrismPath
            );
            return Prisms;
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

        return Prisms;
    }

    /// <summary>
    ///     Retrieves a prism descriptor by identifier, loading from disk if necessary.
    /// </summary>
    public static PrismDescriptor? Get(Guid id)
    {
        if (Prisms.Count == 0)
        {
            LoadAll();
        }

        return Prisms.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    ///     Persists the cached prism descriptors to disk.
    /// </summary>
    public static void SaveAll()
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

