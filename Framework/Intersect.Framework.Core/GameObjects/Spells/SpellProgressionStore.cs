using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.GameObjects;

/// <summary>
/// Provides access to spell progression information for both client and server.
/// </summary>
public static class SpellProgressionStore
{
    private static readonly Dictionary<Guid, SpellProgression> s_bySpellId = new();

    /// <summary>
    /// Gets the progression for the specified spell identifier.
    /// </summary>
    public static IReadOnlyDictionary<Guid, SpellProgression> BySpellId => s_bySpellId;

    /// <summary>
    /// Loads progressions from a JSON file on disk.
    /// If the file does not exist, a hardcoded default is loaded instead.
    /// </summary>
    public static void Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var progressions = JsonConvert.DeserializeObject<List<SpellProgression>>(json) ?? new();
            Load(progressions);
        }
        else
        {
            LoadDefaults();
        }
    }

    /// <summary>
    /// Populates the store with the provided progressions.
    /// Ensures each progression contains at least one row.
    /// </summary>
    private static void Load(IEnumerable<SpellProgression> progressions)
    {
        s_bySpellId.Clear();
        foreach (var progression in progressions)
        {
            if (progression.Levels.Count == 0)
            {
                throw new InvalidDataException($"Spell {progression.SpellId} must define at least one progression row.");
            }

            s_bySpellId[progression.SpellId] = progression;
        }
    }

    /// <summary>
    /// Temporary hardcoded loader that creates five empty rows for every spell in the database.
    /// </summary>
    public static void LoadDefaults()
    {
        var progressions = SpellDescriptor.Lookup
            .Select(pair => (SpellDescriptor)pair.Value)
            .Select(descriptor => new SpellProgression
            {
                SpellId = descriptor.Id,
                Levels = Enumerable.Range(0, 5).Select(_ => new SpellProperties()).ToList()
            });

        Load(progressions);
    }
}

