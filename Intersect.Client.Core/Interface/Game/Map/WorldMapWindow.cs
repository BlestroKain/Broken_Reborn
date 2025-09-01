using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Controllers;
using Intersect.Client.Core;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Game.Map;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.Interface.Game.Map;

/// <summary>
/// World map con panning, zoom, leyenda, filtros, búsqueda, tooltip y waypoints.
/// Ahora hereda de Window y usa EnsureInitialized como MinimapWindow.
/// </summary>
public sealed class WorldMapWindow : Window
{
    private ImagePanel _canvas = null!;
    private MapLegend _legend = null!;
    private MapFilters _filters = null!;
    private ImagePanel _tooltip = null!;
    private Label _tooltipLabel = null!;
    private Button _navigateButton = null!;
    private Button _openWindowButton = null!;
    private Button _minimapButton = null!;
    private bool _initialized;

    private MapFilters.MapSearchEntry? _activeEntry;
    private Point _activeEntryCenter;
    private readonly List<ImagePanel> _searchHighlights = new();

    public static WaypointLayer? Waypoints { get; private set; }

    private float _zoom = 1f;
    private const float MinZoom = 0.25f;
    private const float MaxZoom = 4f;
    private int _baseWidth;
    private int _baseHeight;
    private DateTime _lastWheelTime = DateTime.MinValue;
    private static readonly TimeSpan WheelDebounce = TimeSpan.FromMilliseconds(125);

    private bool _dragging;
    private Point _dragOrigin;
    private Point _canvasOrigin;

    // --------- CONSTRUCTOR ----------
    public WorldMapWindow(Base parent)
        : base(parent, Strings.WorldMap.Title, false, nameof(WorldMapWindow))
    {
       IsResizable= true;

        // Crea referencias por nombre para que las “enganche” el JSON al EnsureInitialized.
        _canvas = new MapCanvas(this, this, "MapImage");
        _legend = new MapLegend(this);
        _filters = new MapFilters(this);
        _tooltip = new ImagePanel(this, "Tooltip");
        _tooltipLabel = new Label(_tooltip, "Name") { Dock = Pos.Top };
        _navigateButton = new Button(_tooltip, "NavigateButton") { Text = Strings.WorldMap.Navigate, Dock = Pos.Top };
        _openWindowButton = new Button(_tooltip, "OpenWindowButton") { Text = Strings.WorldMap.OpenWindow, Dock = Pos.Top };
        _minimapButton = new Button(this, "MinimapButton");

        // Wire básico (callbacks). El resto se termina en EnsureInitialized.
        _filters.SearchSubmitted += OnSearchSubmitted;
        _navigateButton.Clicked += NavigateButton_Clicked;
        _openWindowButton.Clicked += OpenWindowButton_Clicked;
        _minimapButton.Clicked += MinimapButton_Clicked;

        // Waypoints sobre el canvas (se finaliza al EnsureInitialized).
        Waypoints = new WaypointLayer(_canvas);
    }

    // --------- INICIALIZACIÓN POR JSON ----------
    protected override void EnsureInitialized()
    {
        if (_initialized)
            return;

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        // Tooltips y textos dependientes de Strings/Controles
        var keyHint = GetMinimapKeyHint();
        _minimapButton.SetToolTipText(string.IsNullOrEmpty(keyHint)
            ? Strings.Minimap.Title
            : $"{Strings.Minimap.Title} ({keyHint})");

        // Leyenda de recursos (colores desde Options)
        var colors = Options.Instance.Minimap.MinimapColors.Resource;
        if (colors.TryGetValue(JobType.Lumberjack, out var lumberjackColor)) _legend.AddEntry("Madera", lumberjackColor);
        if (colors.TryGetValue(JobType.Mining, out var miningColor)) _legend.AddEntry("Mina", miningColor);
        if (colors.TryGetValue(JobType.Farming, out var farmingColor)) _legend.AddEntry("Hierbas", farmingColor);
        if (colors.TryGetValue(JobType.Fishing, out var fishingColor)) _legend.AddEntry("Pesca", fishingColor);

        // Filtros rápidos por job
        _filters.AddFilter(JobType.Lumberjack.ToString(), "Madera");
        _filters.AddFilter(JobType.Mining.ToString(), "Mina");
        _filters.AddFilter(JobType.Farming.ToString(), "Hierbas");
        _filters.AddFilter(JobType.Fishing.ToString(), "Pesca");

        // Preferencias guardadas (zoom y posición)
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

        // Tooltip oculto por defecto
        _tooltip.IsHidden = true;

        // Color de texto del label del tooltip (claro, con buen contraste)
        _tooltipLabel.TextColor = new Color(230, 230, 230, 255);

        // Layout inicial
        ApplyLayout();

        _initialized = true;

    }
    protected override void OnResized(int x, int y, int width, int height)
    {
        base.OnResized(x, y, width, height);
        if (!_initialized) return;
        ApplyLayout();
        // Recalcular clamp con el tamaño nuevo
        ClampPosition();
    }

    private void ApplyLayout()
    {
        // Dimensiones base de la ventana (ya hereda de Window)
        // Si quieres fijar un tamaño mínimo:
        const int minW = 720;
        const int minH = 480;
        if (Width < minW || Height < minH)
        {
            SetSize(Math.Max(Width, minW), Math.Max(Height, minH));
        }

        // Márgenes y anchos
        const int margin = 12;
        const int sidebarW = 220; // barra derecha para leyenda + filtros + botón
        int contentW = Width - sidebarW - (margin * 3);
        int contentH = Height - (margin * 2);

        // --- Canvas (mapa): ocupa todo el espacio izquierdo
        // Respetamos el zoom actual, así que actualizamos el "lienzo" visual y clamp después
        // Nota: _baseWidth/_baseHeight son el tamaño "base" del canvas (sin zoom)
        // Lo que seteamos aquí es el rectángulo visible del contenedor.
        _canvas.SetPosition(margin, margin);
        _canvas.SetSize(contentW, contentH);

        // Si el canvas aún no tiene baseWidth/baseHeight, inicialízalos una vez
        if (_baseWidth <= 0 || _baseHeight <= 0)
        {
            _baseWidth = _canvas.Width;
            _baseHeight = _canvas.Height;
        }

        // Ajustar bounds del canvas según el zoom actual, manteniendo centro relativo
        var newWidth = (int)(_baseWidth * _zoom);
        var newHeight = (int)(_baseHeight * _zoom);

        // Si el canvas “contenido” debe seguir el mismo rect que el contenedor, puedes igualarlo;
        // si no, dejamos que pan/zoom gestione el SetBounds en Zoom().
        // Aquí garantizamos que nunca sea menor al área visible (para evitar flashes).
        if (newWidth < _canvas.Width) newWidth = _canvas.Width;
        if (newHeight < _canvas.Height) newHeight = _canvas.Height;

        _canvas.SetBounds(_canvas.X, _canvas.Y, newWidth, newHeight);
        ClampPosition();

        // --- Leyenda (arriba derecha)
        int rightX = Width - sidebarW - margin;
        _legend.SetPosition(rightX, margin);
        _legend.SetSize(sidebarW, 150); // alto fijo razonable; ajusta si lo necesitas

        // --- Filtros (debajo de la leyenda, ocupa el resto de la barra)
        int filtersY = _legend.Y + _legend.Height + margin;
        int filtersH = Height - filtersY - margin - 36 - margin; // 36 para botón; margen extra
        if (filtersH < 120) filtersH = 120; // mínimo para que no se aplaste
        _filters.SetPosition(rightX, filtersY);
        _filters.SetSize(sidebarW, filtersH);

        // --- Botón "Ir al minimapa" (abajo derecha)
        _minimapButton.SetSize(sidebarW, 36);
        _minimapButton.SetPosition(rightX, Height - margin - _minimapButton.Height);

        // --- Tooltip (tamaño base; la posición se mueve cuando seleccionas un POI)
        // Mantén un ancho cómodo para el texto y botones
        _tooltip.SetSize(220, 110);
        _tooltipLabel.TextColor = new Color(230, 230, 230, 255); // por si se reasigna UI
                                                                 // Botones dentro del tooltip: en columna
        _tooltipLabel.SetPosition(8, 6);
        _tooltipLabel.SetSize(_tooltip.Width - 16, 22);

        _navigateButton.SetPosition(8, _tooltipLabel.Y + _tooltipLabel.Height + 6);
        _navigateButton.SetSize(_tooltip.Width - 16, 28);

        _openWindowButton.SetPosition(8, _navigateButton.Y + _navigateButton.Height + 6);
        _openWindowButton.SetSize(_tooltip.Width - 16, 28);
    }
    // --------- VISIBILIDAD ----------
    public void ShowPersisted()
    {
        EnsureInitialized();
        IsHidden = false;
    }

    public bool IsVisible() => !IsHidden;

    public void HidePersisted()
    {
        IsHidden = true;
    }

    // --------- EVENTOS DE MAPA ----------
    private void OnMapClicked(Point pos)
    {
        _tooltip.IsHidden = true;
        _activeEntry = null;
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

    private int ClampX(int x) => Math.Clamp(x, Width - (int)(_baseWidth * _zoom), 0);
    private int ClampY(int y) => Math.Clamp(y, Height - (int)(_baseHeight * _zoom), 0);

    private void ClampPosition()
    {
        _canvas.SetPosition(ClampX(_canvas.X), ClampY(_canvas.Y));
    }

    internal void DragBy(int dx, int dy)
    {
        if (!_dragging)
            return;

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
            return;

        _lastWheelTime = now;

        var oldZoom = _zoom;
        var step = Options.Instance.Minimap.ZoomStep / 100f;
        if (delta > 0) _zoom = Math.Clamp(_zoom + step, MinZoom, MaxZoom);
        else if (delta < 0) _zoom = Math.Clamp(_zoom - step, MinZoom, MaxZoom);

        if (Math.Abs(_zoom - oldZoom) < 0.001f)
            return;

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
        var targetX = ClampX(Width / 2 - pos.X);
        var targetY = ClampY(Height / 2 - pos.Y);
        _canvas.SetPosition(targetX, targetY);
        MapPreferences.Instance.WorldMapPosition = new Point(_canvas.X, _canvas.Y);
        MapPreferences.Save();
    }

    private void MinimapButton_Clicked(Base sender, MouseButtonState args)
    {
        Interface.GameUi.MapUIManager.OpenMinimap();
    }

    private static string GetMinimapKeyHint()
    {
        if (!Controls.ActiveControls.TryGetMappingFor(Control.OpenMinimap, out var mapping) ||
            mapping.Bindings.Count == 0)
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
        foreach (var highlight in _searchHighlights) highlight.Dispose();
        _searchHighlights.Clear();

        var results = _filters.Search(query)
            .Where(r => r.NpcId == null || BestiaryController.HasUnlock(r.NpcId.Value, BestiaryUnlock.Kill))
            .ToList();

        if (results.Count == 0)
            return;

        var first = results[0];
        var center = new Point(first.Area.X + first.Area.Width / 2, first.Area.Y + first.Area.Height / 2);
        CenterOn(center);

        _activeEntry = first;
        _activeEntryCenter = center;
        _tooltipLabel.Text = first.Name;
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

    private void NavigateButton_Clicked(Base sender, MouseButtonState args)
    {
        CenterOn(_activeEntryCenter);
        Waypoints?.AddWaypoint(_activeEntryCenter, WaypointScope.Local);
    }

    private void OpenWindowButton_Clicked(Base sender, MouseButtonState args)
    {
        if (_activeEntry == null) return;

        var type = _activeEntry.Type.ToLowerInvariant();
        Interface.EnqueueInGame(gameInterface =>
        {
            switch (type)
            {
                case "bank":
                case "banco":
                    gameInterface.NotifyOpenBank();
                    break;
                case "shop":
                case "market":
                case "mercado":
                    gameInterface.NotifyOpenShop();
                    break;
                case "crafting":
                case "workshop":
                case "taller":
                    gameInterface.NotifyOpenCraftingTable(false);
                    break;
            }
        });
    }

    // Tick opcional por si quieres actualizar overlays (p. ej., waypoints animados)
    public void Update()
    {
        if (!_initialized || IsHidden) return;
        Waypoints?.Update();
    }

    // ---------- Canvas interno (maneja input y lo reenvía a la ventana) ----------
    private sealed class MapCanvas : ImagePanel
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
        /// <summary>
        /// Aplica layout manual (SetPosition/SetSize) a todos los controles del WorldMapWindow.
        /// Mantiene márgenes y una barra lateral derecha para leyenda/filtros/botón.
        /// </summary>
       

        protected void Think()
        {
            base.Think();
            Waypoints?.Update();
        }
    }
}
