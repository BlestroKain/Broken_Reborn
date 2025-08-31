using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Interface.Game.Map;
using Intersect;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Enums;
using Intersect.Client.Core.Controls;
using Intersect.Client.Localization;
using Intersect.Config;
using Intersect.Client.Controllers;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.Interface.Game.Map;

/// <summary>
/// Basic world map window with panning, zooming, legend, filters, and tooltip support.
/// </summary>
public class WorldMapWindow
{
    private readonly Draggable _window;
    private readonly MapCanvas _canvas;
    private readonly MapLegend _legend;
    private readonly MapFilters _filters;
    private readonly Label _tooltip;
    private readonly List<ImagePanel> _searchHighlights = new();
    public static WaypointLayer? Waypoints { get; private set; }
    private readonly Button _minimapButton;

    private float _zoom = 1f;
    private const float MinZoom = 0.25f;
    private const float MaxZoom = 4f;
    private readonly int _baseWidth;
    private readonly int _baseHeight;
    private DateTime _lastWheelTime = DateTime.MinValue;
    private static readonly TimeSpan WheelDebounce = TimeSpan.FromMilliseconds(125);

    private bool _dragging;
    private Point _dragOrigin;
    private Point _canvasOrigin;

    public WorldMapWindow(Canvas gameCanvas)
    {
        _window = new Draggable(gameCanvas, nameof(WorldMapWindow));
        _canvas = new MapCanvas(this, _window, "MapImage");
        _legend = new MapLegend(_window);
        _filters = new MapFilters(_window);
        _filters.AddFilter(JobType.Lumberjack.ToString(), "Madera");
        _filters.AddFilter(JobType.Mining.ToString(), "Mina");
        _filters.AddFilter(JobType.Farming.ToString(), "Hierbas");
        _filters.AddFilter(JobType.Fishing.ToString(), "Pesca");
        _filters.SearchSubmitted += OnSearchSubmitted;
        _minimapButton = new Button(_window, "MinimapButton");
        _minimapButton.Clicked += MinimapButton_Clicked;
        var keyHint = GetMinimapKeyHint();
        _minimapButton.SetToolTipText(string.IsNullOrEmpty(keyHint) ? Strings.Minimap.Title : $"{Strings.Minimap.Title} ({keyHint})");
        var colors = Options.Instance.Minimap.MinimapColors.Resource;
        if (colors.TryGetValue(JobType.Lumberjack, out var lumberjackColor))
        {
            _legend.AddEntry("Madera", lumberjackColor);
        }
        if (colors.TryGetValue(JobType.Mining, out var miningColor))
        {
            _legend.AddEntry("Mina", miningColor);
        }
        if (colors.TryGetValue(JobType.Farming, out var farmingColor))
        {
            _legend.AddEntry("Hierbas", farmingColor);
        }
        if (colors.TryGetValue(JobType.Fishing, out var fishingColor))
        {
            _legend.AddEntry("Pesca", fishingColor);
        }

        _tooltip = new Label(_window, "Tooltip");
        _tooltip.IsHidden = true;

        Waypoints = new WaypointLayer(_canvas);

        _window.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        // Apply stored preferences for zoom and position.
        _baseWidth = _canvas.Width;
        _baseHeight = _canvas.Height;
        _zoom = Math.Clamp(MapPreferences.Instance.WorldMapZoom, MinZoom, MaxZoom);
        _canvas.SetBounds(
            MapPreferences.Instance.WorldMapPosition.X,
            MapPreferences.Instance.WorldMapPosition.Y,
            (int)(_baseWidth * _zoom),
            (int)(_baseHeight * _zoom)
        );
        ClampPosition();
        MapPreferences.Instance.WorldMapZoom = _zoom;
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
    }

    public bool IsVisible() => !_window.IsHidden;

    public void Show()
    {
        _window.IsHidden = false;
    }

    public void Hide()
    {
        _window.IsHidden = true;
    }

    private void OnMapClicked(Point pos)
    {
        _tooltip.IsHidden = true;
    }

    private void OnMapDoubleClicked(Point pos)
    {
        var shift = Globals.InputManager.IsKeyDown(Keys.Shift);
        if (shift)
        {
            Waypoints?.AddWaypoint(pos, WaypointScope.Party);
            PacketSender.SendWaypointSet(pos.X, pos.Y, WaypointScope.Party);
            PacketSender.SendWaypointSet(pos.X, pos.Y, WaypointScope.Guild);
        }
        else
        {
            Waypoints?.AddWaypoint(pos, WaypointScope.Local);
        }
    }

    internal void BeginDrag(Point pos)
    {
        _dragging = true;
        _dragOrigin = pos;
        _canvasOrigin = new Point(_canvas.X, _canvas.Y);
    }

    internal void EndDrag()
    {
        _dragging = false;
        ClampPosition();
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
    }

    private int ClampX(int x) => Math.Clamp(x, _window.Width - (int)(_baseWidth * _zoom), 0);
    private int ClampY(int y) => Math.Clamp(y, _window.Height - (int)(_baseHeight * _zoom), 0);
    private void ClampPosition()
    {
        _canvas.SetPosition(ClampX(_canvas.X), ClampY(_canvas.Y));
    }

    internal void DragBy(int dx, int dy)
    {
        if (!_dragging)
        {
            return;
        }
        var targetX = ClampX(_canvasOrigin.X + dx);
        var targetY = ClampY(_canvasOrigin.Y + dy);
        var newX = (int)(_canvas.X + (targetX - _canvas.X) * 0.2f);
        var newY = (int)(_canvas.Y + (targetY - _canvas.Y) * 0.2f);
        _canvas.SetPosition(newX, newY);
        ClampPosition();
    }

    internal void Zoom(int delta, int x, int y)
    {
        var now = DateTime.UtcNow;
        if (now - _lastWheelTime < WheelDebounce)
        {
            return;
        }
        _lastWheelTime = now;

        var oldZoom = _zoom;
        var step = Options.Instance.Minimap.ZoomStep / 100f;
        if (delta > 0)
        {
            _zoom = Math.Clamp(_zoom + step, MinZoom, MaxZoom);
        }
        else if (delta < 0)
        {
            _zoom = Math.Clamp(_zoom - step, MinZoom, MaxZoom);
        }

        if (Math.Abs(_zoom - oldZoom) < 0.001f)
        {
            return;
        }

        var scale = _zoom / oldZoom;
        var relX = x - _canvas.X;
        var relY = y - _canvas.Y;
        var newWidth = (int)(_baseWidth * _zoom);
        var newHeight = (int)(_baseHeight * _zoom);
        var newX = _canvas.X - (int)(relX * (scale - 1f));
        var newY = _canvas.Y - (int)(relY * (scale - 1f));

        _canvas.SetBounds(newX, newY, newWidth, newHeight);
        ClampPosition();

        MapPreferences.Instance.WorldMapZoom = _zoom;
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
    }

    private void CenterOn(Point pos)
    {
        var targetX = ClampX(_window.Width / 2 - pos.X);
        var targetY = ClampY(_window.Height / 2 - pos.Y);
        _canvas.SetPosition(targetX, targetY);
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
    }

    private void MinimapButton_Clicked(Base sender, MouseButtonState args)
    {
        Interface.GameUi.GameMenu?.ToggleMinimapWindow();
    }

    private static string GetMinimapKeyHint()
    {

        if (!Controls.ActiveControls.TryGetMappingFor(Control.OpenMinimap, out var mapping) ||
            mapping.Bindings.Length == 0)
        {
            return string.Empty;
        }

        var binding = mapping.Bindings[0];
        if (binding.Key == Keys.None)
        {
            return string.Empty;
        }

        return Strings.Keys.FormatKeyName(binding.Modifier, binding.Key);
    }

    private void OnSearchSubmitted(string query)
    {
        foreach (var highlight in _searchHighlights)
        {
            highlight.Dispose();
        }
        _searchHighlights.Clear();

        var results = _filters.Search(query)
            .Where(r => r.NpcId == null || BestiaryController.HasUnlock(r.NpcId.Value, BestiaryUnlock.Kill))
            .ToList();
        if (results.Count == 0)
        {
            return;
        }

        var first = results[0];
        var center = new Point(first.Area.X + first.Area.Width / 2, first.Area.Y + first.Area.Height / 2);
        CenterOn(center);

        _tooltip.Text = first.Name;
        _tooltip.SetPosition(center.X + 5, center.Y + 5);
        _tooltip.IsHidden = false;

        foreach (var result in results)
        {
            var area = new ImagePanel(_canvas, "SearchHighlight");
            area.SetBounds(result.Area.X, result.Area.Y, result.Area.Width, result.Area.Height);
            area.IsHidden = false;
            _searchHighlights.Add(area);
        }
    }

    private class MapCanvas : ImagePanel
    {
        private readonly WorldMapWindow _window;

        public MapCanvas(WorldMapWindow window, Base parent, string name) : base(parent, name)
        {
            _window = window;
        }

        protected override void OnMouseDown(MouseButton mouseButton, Point mousePosition, bool userAction = true)
        {
            base.OnMouseDown(mouseButton, mousePosition, userAction);
            if (mouseButton == MouseButton.Left)
            {
                _window.BeginDrag(mousePosition);
            }
        }

        protected override void OnMouseUp(MouseButton mouseButton, Point mousePosition, bool userAction = true)
        {
            base.OnMouseUp(mouseButton, mousePosition, userAction);
            if (mouseButton == MouseButton.Left)
            {
                _window.EndDrag();
            }
        }

        protected override void OnMouseMoved(int x, int y, int dx, int dy)
        {
            base.OnMouseMoved(x, y, dx, dy);
            _window.DragBy(dx, dy);
        }

        protected override bool OnMouseWheeled(int delta)
        {
            var mouse = InputHandler.MousePosition;
            var local = ToLocal(mouse.X, mouse.Y);
            _window.Zoom(delta, local.X, local.Y);
            return true;
        }

        protected override void OnMouseClicked(MouseButton mouseButton, Point mousePosition, bool userAction = true)
        {
            base.OnMouseClicked(mouseButton, mousePosition, userAction);
            if (mouseButton == MouseButton.Left)
            {
                _window.OnMapClicked(mousePosition);
            }
        }

        protected override void OnMouseDoubleClicked(MouseButton mouseButton, Point mousePosition, bool userAction = true)
        {
            base.OnMouseDoubleClicked(mouseButton, mousePosition, userAction);
            if (mouseButton == MouseButton.Left)
            {
                _window.OnMapDoubleClicked(mousePosition);
            }
        }

        protected  void Think()
        {
            base.Think();
            Waypoints?.Update();
        }
    }
}

