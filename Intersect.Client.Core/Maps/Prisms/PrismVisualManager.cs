using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Prisms;
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
        if (!Prisms.TryGetValue(packet.Id, out var visual))
        {
            var descriptor = PrismDescriptorStore.Get(packet.Id);
            if (descriptor == null)
            {
                return;
            }

            visual = new PrismVisual(descriptor);
            Prisms[packet.Id] = visual;
        }

        visual.Update(packet.MapId, packet.X, packet.Y, packet.Owner, packet.State, packet.Hp, packet.MaxHp);
    }

    public static void Draw(MapInstance map)
    {
        foreach (var visual in Prisms.Values.Where(v => v.MapId == map.Id))
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
                PacketSender.SendPrismAttack(visual.MapId, visual.Id);
                return true;
            }
        }

        return false;
    }
}
