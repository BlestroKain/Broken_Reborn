using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Config;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Maps.Prisms;

public static class PrismVisualManager
{
    private static readonly Dictionary<Guid, PrismVisual> Prisms = new();

    public static void Synchronize(PrismListPacket packet)
    {
        Prisms.Clear();
        foreach (var prism in packet.Prisms)
        {
            Update(prism);
        }
    }

    public static void Update(PrismUpdatePacket packet)
    {
        if (!Prisms.TryGetValue(packet.MapId, out var visual))
        {
            var info = PrismConfig.Prisms.FirstOrDefault(p => p.MapId == packet.MapId);
            if (info == null)
            {
                return;
            }

            visual = new PrismVisual(info);
            Prisms[packet.MapId] = visual;
        }

        visual.Update(packet.PrismId, packet.Owner, packet.State, packet.Hp, packet.MaxHp);
    }

    public static void Draw(MapInstance map)
    {
        if (Prisms.TryGetValue(map.Id, out var visual))
        {
            visual.Draw(map);
        }
    }

    public static bool TryClickAttack()
    {
        var mouse = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
        var x = (int)mouse.X;
        var y = (int)mouse.Y;

        foreach (var visual in Prisms.Values)
        {
            if (visual.HitTest(x, y))
            {
                PacketSender.SendPrismAttack(visual.MapId, visual.PrismId);
                return true;
            }
        }

        return false;
    }
}
