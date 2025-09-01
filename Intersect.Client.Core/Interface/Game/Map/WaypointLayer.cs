using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Networking;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game.Map;

public sealed class WaypointLayer
{
    private sealed class Waypoint
    {
        public ImagePanel Panel = null!;
        public DateTime Expire;
        public WaypointScope Scope;
    }

    private readonly Base _parent;
    private readonly List<Waypoint> _waypoints = new();

    public WaypointLayer(Base parent)
    {
        _parent = parent;
    }

    public void AddWaypoint(Point pos, WaypointScope scope)
    {
        var panel = new ImagePanel(_parent, "Waypoint");
        panel.SetPosition(pos.X - panel.Width / 2, pos.Y - panel.Height / 2);
        panel.RenderColor = ScopeToColor(scope);
        panel.IsHidden = false;
        _waypoints.Add(new Waypoint
        {
            Panel = panel,
            Expire = DateTime.UtcNow + GetTtl(scope),
            Scope = scope,
        });
    }

    public void Clear(WaypointScope scope, bool fromServer = false)
    {
        foreach (var wp in _waypoints.Where(w => w.Scope == scope).ToList())
        {
            wp.Panel.Dispose();
            _waypoints.Remove(wp);
        }

        if (!fromServer && scope != WaypointScope.Local)
        {
            PacketSender.SendWaypointClear(scope);
        }
    }

    public void Update()
    {
        var now = DateTime.UtcNow;
        HashSet<WaypointScope> expiredScopes = new();
        foreach (var wp in _waypoints.ToList())
        {
            if (now >= wp.Expire)
            {
                wp.Panel.Dispose();
                _waypoints.Remove(wp);
                if (wp.Scope != WaypointScope.Local)
                {
                    expiredScopes.Add(wp.Scope);
                }
            }
        }

        foreach (var scope in expiredScopes)
        {
            PacketSender.SendWaypointClear(scope);
        }
    }

    public static Color ScopeToColor(WaypointScope scope) => scope switch
    {
        WaypointScope.Party => Color.Green,
        WaypointScope.Guild => Color.Blue,
        _ => Color.Yellow,
    };

    private static TimeSpan GetTtl(WaypointScope scope) => scope switch
    {
        WaypointScope.Guild => TimeSpan.FromSeconds(30),
        _ => TimeSpan.FromSeconds(20),
    };

    public IEnumerable<(Point Position, WaypointScope Scope)> Enumerate()
    {
        foreach (var wp in _waypoints)
        {
            var pos = new Point(wp.Panel.X + wp.Panel.Width / 2, wp.Panel.Y + wp.Panel.Height / 2);
            yield return (pos, wp.Scope);
        }
    }
}

