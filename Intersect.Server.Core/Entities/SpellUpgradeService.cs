using System;
using Intersect.GameObjects;
using Intersect.Server.Database;


namespace Intersect.Server.Entities;

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
    public static SpellUpgradeResult CanUpgradeSpell(Player player, Guid spellId)
    {
        if (player == null)
        {
            throw new ArgumentNullException(nameof(player));
        }

        if (!player.Spellbook.SpellLevels.ContainsKey(spellId))
        {
            return SpellUpgradeResult.NoSuchSpell;
        }

        var descriptor = SpellDescriptor.Get(spellId);
        if (descriptor == null)
        {
            return SpellUpgradeResult.NoSuchSpell;
        }

        var currentLevel = player.Spellbook.GetLevelOrDefault(spellId);
        if (currentLevel >= descriptor.Progression.Count)
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

