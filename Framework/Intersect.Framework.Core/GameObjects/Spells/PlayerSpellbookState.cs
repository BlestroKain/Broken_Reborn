using System;
using System.Collections.Generic;
using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Spells;

[MessagePackObject]
public partial class PlayerSpellbookState
{
    [Key(0)]
    public int AvailableSpellPoints { get; set; }

    [Key(2)]
    public Dictionary<Guid, int> SpellLevels { get; set; } = [];

    public int GetLevelOrDefault(Guid spellId, int fallback = 1) =>
        SpellLevels.TryGetValue(spellId, out var level) ? level : fallback;
}

