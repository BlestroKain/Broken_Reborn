using System;
using System.Collections.Generic;
using MessagePack;
using Intersect.GameObjects;

namespace Intersect.Framework.Core.GameObjects.Spells;

[MessagePackObject]
public partial class PlayerSpellbookState
{
    [Key(0)]
    public int AvailableSpellPoints { get; set; }

    [Key(1)]
    public Dictionary<Guid, SpellProperties> Spells { get; set; } = [];

    [Key(2)]
    public Dictionary<Guid, int> SpellLevels { get; set; } = [];

    public int GetLevel(Guid spellId) =>
        SpellLevels.TryGetValue(spellId, out var level) ? level : 1;

    public bool TryUpgradeSpell(Guid spellId, out int newLevel)
    {
        var currentLevel = GetLevel(spellId);
        newLevel = currentLevel;

        if (!SpellProgressionStore.BySpellId.TryGetValue(spellId, out var progression))
        {
            return false;
        }

        if (currentLevel >= progression.Levels.Count)
        {
            return false;
        }

        newLevel = currentLevel + 1;
        SpellLevels[spellId] = newLevel;
        return true;
    }
}

