using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;

namespace Intersect.Editor.Serialization;

/// <summary>
/// Provides helpers for working with <see cref="SpellProgression"/> data.
/// </summary>
public static class SpellProgressionSerializer
{
    /// <summary>
    /// Builds a <see cref="SpellProgression"/> from the provided rows, validating that exactly
    /// five rows are present.
    /// </summary>
    /// <param name="spellId">Identifier of the spell the progression belongs to.</param>
    /// <param name="rows">The rows describing each level of the progression.</param>
    /// <exception cref="InvalidDataException">Thrown when the number of rows is not equal to five.</exception>
    public static SpellProgression FromRows(Guid spellId, IEnumerable<SpellProperties> rows)
    {
        if (rows == null)
        {
            throw new ArgumentNullException(nameof(rows));
        }

        var levels = rows.ToList();
        if (levels.Count != 5)
        {
            throw new InvalidDataException($"Spell {spellId} progression must contain exactly five rows (found {levels.Count}).");
        }

        // Ensure rows are ordered by level before returning.
        levels = levels.OrderBy(l => l.Level).ToList();
        return new SpellProgression
        {
            SpellId = spellId,
            Levels = levels,
        };
    }

    /// <summary>
    /// Generates a default five level progression based on the provided level one template.
    /// </summary>
    /// <param name="levelOne">The template row representing level one values.</param>
    /// <returns>A list containing five copies of the template row with incrementing levels.</returns>
    public static List<SpellProperties> AutoGenerateFromLevelOne(SpellProperties levelOne)
    {
        if (levelOne == null)
        {
            throw new ArgumentNullException(nameof(levelOne));
        }

        var list = new List<SpellProperties>(5);
        for (var i = 1; i <= 5; i++)
        {
            var copy = new SpellProperties(levelOne)
            {
                Level = i,
            };
            list.Add(copy);
        }

        return list;
    }
}

