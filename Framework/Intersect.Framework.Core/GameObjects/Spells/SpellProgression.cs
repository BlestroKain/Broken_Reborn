using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Spells;

namespace Intersect.GameObjects;

/// <summary>
/// Describes the complete progression for a spell.
/// Each level row is represented by <see cref="SpellProperties"/> allowing
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
    public IList<SpellProperties> Levels { get; set; } = new List<SpellProperties>();

    /// <summary>
    /// Retrieves the row for the provided level if one exists.
    /// </summary>
    /// <param name="level">The level to query.</param>
    /// <returns>The <see cref="SpellProperties"/> for the requested level or <c>null</c> if not found.</returns>
    public SpellProperties? GetLevel(int level) =>
        Levels.FirstOrDefault(row => row.Level == level);
}
