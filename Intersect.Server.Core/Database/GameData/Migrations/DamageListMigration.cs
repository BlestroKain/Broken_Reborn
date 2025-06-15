using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Framework.Core.GameObjects.PlayerClass;
using Intersect.GameObjects;

namespace Intersect.Server.Database.GameData.Migrations;

public static partial class DamageListMigration
{
    public static void Run(GameContext context)
    {
        UpgradeDamageLists(context);
    }

    private static void UpgradeDamageLists(GameContext context)
    {
        foreach (var item in context.Items)
        {
            item.Damages = new int[] { item.Damage };
            item.DamageTypes = new int[] { item.DamageType };
        }

        foreach (var npc in context.Npcs)
        {
            npc.Damages = new int[] { npc.Damage };
            npc.DamageTypes = new int[] { npc.DamageType };
        }

        foreach (var cls in context.Classes)
        {
            cls.Damages = new int[] { cls.Damage };
            cls.DamageTypes = new int[] { cls.DamageType };
        }

        foreach (var spell in context.Spells)
        {
            spell.Combat.DamageTypes = new int[] { spell.Combat.DamageType };
        }

        context.ChangeTracker.DetectChanges();
        context.SaveChanges();
    }
}
