using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.GameObjects;

/// <summary>
/// Represents a single progression row for a spell level.
/// </summary>
public class SpellLevelRow
{
    /// <summary>
    /// Gets or sets the level this row applies to.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Gets or sets a generic power value for this level.
    /// This can represent damage, healing, or any other stat that scales with level.
    /// </summary>
    public int Power { get; set; }
}

/// <summary>
/// Describes the complete progression for a spell.
/// </summary>
public class SpellProgression
{
    /// <summary>
    /// Gets or sets the id of the spell that this progression belongs to.
    /// </summary>
    public Guid SpellId { get; set; }

    /// <summary>
    /// Gets the ordered list of level rows that make up this progression.
    /// </summary>
    public IList<SpellLevelRow> Levels { get; set; } = new List<SpellLevelRow>();

    /// <summary>
    /// Retrieves the row for the provided level if one exists.
    /// </summary>
    /// <param name="level">The level to query.</param>
    /// <returns>The <see cref="SpellLevelRow"/> for the requested level or <c>null</c> if not found.</returns>
    public SpellLevelRow? GetLevel(int level) =>
        Levels.FirstOrDefault(row => row.Level == level);
}

