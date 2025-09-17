using System;
using System.Collections.Generic;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.ControlInternal;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Networking;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game.Map
{
    /// <summary>
    /// Capa de Waypoints sobre el contenedor del minimapa.
    /// - Pooling de paneles para evitar alocaciones constantes
    /// - TTL por alcance (Party/Guild/Personal)
    /// - Limpieza de expirados con SendWaypointClear
    /// - Enumeración de posiciones para que el minimapa pueda representarlos
    /// </summary>
    public sealed class WaypointLayer : IDisposable
    {
        private readonly Base _parent;
        private readonly Stack<ImagePanel> _panelPool = new();
        private readonly Stack<Waypoint> _waypointPool = new();
        private readonly List<Waypoint> _waypoints = new();

        private DateTime _lastCleanup = DateTime.MinValue;
        private static readonly TimeSpan CleanupInterval = TimeSpan.FromMilliseconds(250);

        public WaypointLayer(Base parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <summary>
        /// Crea/actualiza un waypoint. Coordenadas en el sistema local del contenedor _parent.
        /// </summary>
        public void Set(WaypointScope scope, int x, int y, int size = 10)
        {
            var wp = FindByScope(scope) ?? RentWaypoint(scope);

            size = Math.Max(6, Math.Min(48, size));

            wp.Panel.SetSize(size, size);
            // Centramos el rectángulo en (x,y)
            wp.Panel.SetPosition(x - size / 2, y - size / 2);

            // Color por alcance
            wp.Panel.RenderColor = ScopeToColor(scope);

            wp.Scope = scope;
            wp.Expiration = DateTime.UtcNow + GetTtl(scope);
            if (!_waypoints.Contains(wp))
            {
                _waypoints.Add(wp);
            }

            wp.Panel.IsHidden = false;
        }

        /// <summary>
        /// Limpia (elimina) un waypoint por scope si existe.
        /// </summary>
        public bool Clear(WaypointScope scope)
        {
            var wp = FindByScope(scope);
            if (wp == null) return false;

            ReturnWaypoint(wp);
            _waypoints.Remove(wp);
            return true;
        }

        /// <summary>
        /// Actualiza TTL y elimina expirados. Llamar periódicamente (p.ej. ~4–10 Hz).
        /// </summary>
        public void Update()
        {
            var now = DateTime.UtcNow;
            if (now - _lastCleanup < CleanupInterval) return;
            _lastCleanup = now;

            var expired = ListPool<Waypoint>.Rent();

            foreach (var wp in _waypoints)
            {
                if (wp.Expiration <= now)
                {
                    expired.Add(wp);
                }
            }

            if (expired.Count > 0)
            {
                foreach (var wp in expired)
                {
                    // Notificamos al server que este scope se limpió
                    PacketSender.SendWaypointClear(wp.Scope);
                    ReturnWaypoint(wp);
                    _waypoints.Remove(wp);
                }
            }

            ListPool<Waypoint>.Return(expired);
        }

        /// <summary>
        /// Devuelve posiciones (centro) y alcance de cada waypoint en coords locales del contenedor.
        /// </summary>
        public IEnumerable<(Point Position, WaypointScope Scope)> Enumerate()
        {
            foreach (var wp in _waypoints)
            {
                var pos = new Point(
                    wp.Panel.X + wp.Panel.Width / 2,
                    wp.Panel.Y + wp.Panel.Height / 2
                );
                yield return (pos, wp.Scope);
            }
        }

        public void Dispose()
        {
            for (var i = _waypoints.Count - 1; i >= 0; i--)
            {
                var wp = _waypoints[i];
                ReturnWaypoint(wp);
                _waypoints.RemoveAt(i);
            }

            while (_panelPool.Count > 0)
            {
                var panel = _panelPool.Pop();
                panel.Dispose();
            }

            _waypointPool.Clear();
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

        // ---------- Internals ----------

        private Waypoint? FindByScope(WaypointScope scope)
        {
            for (int i = 0; i < _waypoints.Count; i++)
            {
                if (_waypoints[i].Scope == scope) return _waypoints[i];
            }
            return null;
        }

        private Waypoint RentWaypoint(WaypointScope scope)
        {
            var p = _panelPool.Count > 0
                ? _panelPool.Pop()
                : new ImagePanel(_parent)
                {
                    MouseInputEnabled = false,
                    KeyboardInputEnabled = false
                };

            // Por si quedó escondido en un retorno anterior
            p.IsHidden = false;

            var wp = _waypointPool.Count > 0 ? _waypointPool.Pop() : new Waypoint();
            wp.Panel = p;
            wp.Scope = scope;
            wp.Expiration = DateTime.UtcNow + GetTtl(scope);

            return wp;
        }

        private void ReturnWaypoint(Waypoint wp)
        {
            if (wp.Panel != null)
            {
                wp.Panel.IsHidden = true;
                wp.Panel.Parent = null; // sacar del árbol
                _panelPool.Push(wp.Panel);
                wp.Panel = null!;
            }

            _waypointPool.Push(wp);
        }

        private sealed class Waypoint
        {
            public ImagePanel Panel = null!;
            public WaypointScope Scope;
            public DateTime Expiration;
        }

        // Pool simple para listas temporales
        private static class ListPool<T>
        {
            private static readonly Stack<List<T>> Pool = new();

            public static List<T> Rent()
            {
                lock (Pool)
                {
                    return Pool.Count > 0 ? Pool.Pop() : new List<T>();
                }
            }

            public static void Return(List<T> list)
            {
                list.Clear();
                lock (Pool)
                {
                    Pool.Push(list);
                }
            }
        }
    }
}
