using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.GameObjects;

/// <summary>
/// Describes the complete progression for a spell.
/// Each level row is represented by <see cref="SpellProgressionRow"/> allowing
/// for adjustments to many aspects of a spell as it levels up.
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
    public IList<SpellProgressionRow> Levels { get; set; } = new List<SpellProgressionRow>();

    /// <summary>
    /// Retrieves the row for the provided level if one exists.
    /// </summary>
    /// <param name="level">The level to query.</param>
    /// <returns>The <see cref="SpellProgressionRow"/> for the requested level or <c>null</c> if not found.</returns>
    public SpellProgressionRow? GetLevel(int level) =>
        level >= 1 && level <= Levels.Count ? Levels[level - 1] : null;
}
