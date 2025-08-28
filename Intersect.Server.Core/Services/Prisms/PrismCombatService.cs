using System;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;

namespace Intersect.Server.Services.Prisms;

internal static class PrismCombatService
{
    public static void ApplyDamage(MapInstance map, AlignmentPrism prism, int amount, Player attacker)
    {
        if (prism == null || amount <= 0)
        {
            return;
        }

        prism.Hp = Math.Max(0, prism.Hp - amount);
        prism.LastHitAt = DateTime.UtcNow;

        if (prism.State != PrismState.UnderAttack)
        {
            prism.State = PrismState.UnderAttack;
            prism.CurrentBattleId ??= Guid.NewGuid();
        }

        if (prism.Hp <= 0)
        {
            prism.State = PrismState.Destroyed;
        }

        PrismService.Broadcast(map);
    }
}
