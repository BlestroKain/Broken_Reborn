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
    public static SpellProgression FromRows(Guid spellId, IEnumerable<SpellProgressionRow> rows)
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

        // Assume rows are already ordered by level.
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
    public static List<SpellProgressionRow> AutoGenerateFromLevelOne(SpellProgressionRow levelOne)
    {
        if (levelOne == null)
        {
            throw new ArgumentNullException(nameof(levelOne));
        }

        var list = new List<SpellProgressionRow>(5);
        for (var i = 0; i < 5; i++)
        {
            var copy = new SpellProgressionRow(levelOne);
            list.Add(copy);
        }

        return list;
    }
}

