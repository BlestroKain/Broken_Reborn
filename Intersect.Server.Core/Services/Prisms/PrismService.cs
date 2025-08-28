using System;
using System.Linq;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Networking;

namespace Intersect.Server.Services.Prisms;

internal static class PrismService
{
    public static AlignmentPrism PlacePrism(Player player, MapInstance map)
    {
        var options = Options.Instance.Prism;
        var maxHp = options.BaseHp + options.HpPerLevel * Math.Max(0, player?.Level - 1 ?? 0);
        var now = DateTime.UtcNow;

        var prism = new AlignmentPrism
        {
            Id = Guid.NewGuid(),
            Owner = player.Faction,
            State = PrismState.Placed,
            MapId = map.MapInstanceId,
            PlacedAt = now,
            MaturationEndsAt = now.AddSeconds(options.MaturationSeconds),
            Level = player.Level,
            MaxHp = maxHp,
            Hp = maxHp,
        };

        map.ControllingPrism = prism;
        Broadcast(map);
        return prism;
    }

    public static void TickState(MapInstance map)
    {
        var prism = map.ControllingPrism;
        if (prism == null)
        {
            return;
        }

        var now = DateTime.UtcNow;
        var changed = false;

        switch (prism.State)
        {
            case PrismState.Placed:
                if (prism.MaturationEndsAt.HasValue && now >= prism.MaturationEndsAt.Value)
                {
                    prism.State = PrismState.Vulnerable;
                    changed = true;
                }
                break;
            case PrismState.UnderAttack:
                if (prism.Hp <= 0)
                {
                    prism.State = PrismState.Destroyed;
                    changed = true;
                }
                else if (prism.LastHitAt.HasValue &&
                         (now - prism.LastHitAt.Value).TotalSeconds >= Options.Instance.Prism.AttackCooldownSeconds)
                {
                    prism.State = PrismState.Dominated;
                    changed = true;
                }
                break;
        }

        if (changed)
        {
            Broadcast(map);
        }
    }

    public static bool IsInVulnerabilityWindow(AlignmentPrism prism, DateTime time)
    {
        if (prism.Windows == null || prism.Windows.Count == 0)
        {
            return true;
        }

        return prism.Windows.Any(window => window.Contains(time));
    }

    public static void Broadcast(MapInstance map)
    {
        foreach (var player in map.GetPlayers())
        {
            PacketSender.SendAreaPacket(player);
        }
    }
}
