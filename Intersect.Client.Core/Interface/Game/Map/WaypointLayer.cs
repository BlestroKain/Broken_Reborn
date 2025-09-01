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
    private readonly Stack<Waypoint> _waypointPool = new();
    private readonly Stack<ImagePanel> _panelPool = new();

    // Throttle dynamic overlay updates to ~4Hz
    private DateTime _lastUpdate = DateTime.MinValue;
    private static readonly TimeSpan OverlayInterval = TimeSpan.FromMilliseconds(250);

    public WaypointLayer(Base parent)
    {
        _parent = parent;
    }

    public void AddWaypoint(Point pos, WaypointScope scope)
    {
        var panel = _panelPool.Count > 0 ? _panelPool.Pop() : new ImagePanel(_parent, "Waypoint");
        panel.Parent = _parent;
        panel.SetPosition(pos.X - panel.Width / 2, pos.Y - panel.Height / 2);
        panel.RenderColor = ScopeToColor(scope);
        panel.IsHidden = false;

        var wp = _waypointPool.Count > 0 ? _waypointPool.Pop() : new Waypoint();
        wp.Panel = panel;
        wp.Expire = DateTime.UtcNow + GetTtl(scope);
        wp.Scope = scope;
        _waypoints.Add(wp);
    }

    public void Clear(WaypointScope scope, bool fromServer = false)
    {
        foreach (var wp in _waypoints.Where(w => w.Scope == scope).ToList())
        {
            ReturnWaypoint(wp);
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
        if (now - _lastUpdate < OverlayInterval)
        {
            return;
        }
        _lastUpdate = now;

        HashSet<WaypointScope> expiredScopes = new();
        foreach (var wp in _waypoints.ToList())
        {
            if (now >= wp.Expire)
            {
                ReturnWaypoint(wp);
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

    private void ReturnWaypoint(Waypoint wp)
    {
        wp.Panel.IsHidden = true;
        wp.Panel.Parent = null;
        _panelPool.Push(wp.Panel);
        _waypointPool.Push(wp);
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

