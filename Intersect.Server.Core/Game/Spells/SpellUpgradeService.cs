using System;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Server.Core.Database;
using Intersect.Server.Core.Entities;

namespace Intersect.Server.Core.Game.Spells;

public enum SpellUpgradeResult
{
    Ok,
    NoSuchSpell,
    MaxLevelReached,
    NotEnoughPoints,
    RequirementsNotMet,
}

public static class SpellUpgradeService
{
    public static SpellUpgradeResult CanUpgradeSpell(Player player, Guid spellId, SpellProgressionStore store)
    {
        if (player == null)
        {
            throw new ArgumentNullException(nameof(player));
        }

        if (!player.Spellbook.SpellLevels.ContainsKey(spellId))
        {
            return SpellUpgradeResult.NoSuchSpell;
        }

        if (!store.BySpellId.TryGetValue(spellId, out var progression))
        {
            return SpellUpgradeResult.NoSuchSpell;
        }

        var currentLevel = player.Spellbook.GetLevelOrDefault(spellId);
        if (currentLevel >= progression.Levels.Count)
        {
            return SpellUpgradeResult.MaxLevelReached;
        }

        if (player.Spellbook.AvailableSpellPoints <= 0)
        {
            return SpellUpgradeResult.NotEnoughPoints;
        }

        return SpellUpgradeResult.Ok;
    }

    public static (int newLevel, int remainingPoints) ApplyUpgrade(Player player, Guid spellId, IDbContext db)
    {
        var newLevel = player.Spellbook.GetLevelOrDefault(spellId) + 1;
        player.Spellbook.SpellLevels[spellId] = newLevel;
        player.Spellbook.AvailableSpellPoints--;
        db.SaveChanges();
        return (newLevel, player.Spellbook.AvailableSpellPoints);
    }
}

