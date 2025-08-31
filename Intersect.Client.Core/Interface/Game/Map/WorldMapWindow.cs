using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Interface.Game.Map;
using Intersect;
using Intersect.Client.Framework.Input;

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
    private readonly ImagePanel _waypoint;

    private float _zoom = 1f;
    private const float MinZoom = 0.25f;
    private const float MaxZoom = 4f;

    private bool _dragging;
    private Point _dragOrigin;
    private Point _canvasOrigin;

    public WorldMapWindow(Canvas gameCanvas)
    {
        _window = new Draggable(gameCanvas, nameof(WorldMapWindow));
        _canvas = new MapCanvas(this, _window, "MapImage");
        _legend = new MapLegend(_window);
        _filters = new MapFilters(_window);

        _tooltip = new Label(_window, "Tooltip");
        _tooltip.IsHidden = true;

        _waypoint = new ImagePanel(_canvas, "Waypoint");
        _waypoint.IsHidden = true;

        _window.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        // Apply stored preferences for zoom and position.
        var baseWidth = _canvas.Width;
        var baseHeight = _canvas.Height;
        _zoom = Math.Clamp(MapPreferences.Instance.WorldMapZoom, MinZoom, MaxZoom);
        _canvas.SetBounds(
            MapPreferences.Instance.WorldMapPosition.X,
            MapPreferences.Instance.WorldMapPosition.Y,
            (int)(baseWidth * _zoom),
            (int)(baseHeight * _zoom)
        );
    }

    private void OnMapClicked(Point pos)
    {
        _tooltip.Text = $"{pos.X}, {pos.Y}";
        _tooltip.SetPosition(pos.X + 5, pos.Y + 5);
        _tooltip.IsHidden = false;
    }

    private void OnMapDoubleClicked(Point pos)
    {
        _waypoint.SetPosition(pos.X - _waypoint.Width / 2, pos.Y - _waypoint.Height / 2);
        _waypoint.IsHidden = false;
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
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
    }

    internal void DragBy(int dx, int dy)
    {
        if (!_dragging)
        {
            return;
        }

        _canvas.SetPosition(_canvasOrigin.X + dx, _canvasOrigin.Y + dy);
    }

    internal void Zoom(int delta, int x, int y)
    {
        var oldZoom = _zoom;
        if (delta > 0)
        {
            _zoom = Math.Min(MaxZoom, _zoom + 0.1f);
        }
        else if (delta < 0)
        {
            _zoom = Math.Max(MinZoom, _zoom - 0.1f);
        }

        if (Math.Abs(_zoom - oldZoom) < 0.001f)
        {
            return;
        }

        var scale = _zoom / oldZoom;
        var newWidth = (int)(_canvas.Width * scale);
        var newHeight = (int)(_canvas.Height * scale);
        var relX = x - _canvas.X;
        var relY = y - _canvas.Y;
        _canvas.SetBounds(
            _canvas.X - (int)(relX * (scale - 1f)),
            _canvas.Y - (int)(relY * (scale - 1f)),
            newWidth,
            newHeight
        );
        MapPreferences.Instance.WorldMapZoom = _zoom;
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
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
    }
}

