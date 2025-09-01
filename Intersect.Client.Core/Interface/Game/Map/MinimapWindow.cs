using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Input;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Maps;
using Intersect.Client.MonoGame.NativeInterop;
using Intersect.Config;
using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Framework.Core.GameObjects.Mapping.Tilesets;
using Intersect.Framework.Core.GameObjects.Maps;
using Intersect.Client.Networking;
namespace Intersect.Client.Interface.Game.Map
{
    public sealed class MinimapWindow : Window
    {
        private IGameRenderTexture _renderTexture;
        private IGameTexture _whiteTexture;
        private bool _redrawMaps;
        private bool _redrawEntities;
        private int _zoomLevel;
        private Dictionary<MapPosition, MapInstance?> _mapGrid = new();

        // Cached entity/POI information per map id
        private Dictionary<Guid, List<EntityLocation>> _entityInfoCache = DictionaryPool<Guid, List<EntityLocation>>.Rent();
        private Point _minimapTileSize;
        private float _dpi;
        private DateTime _lastWheelTime = DateTime.MinValue;
        private static readonly TimeSpan WheelDebounce = TimeSpan.FromMilliseconds(125);
        // Cache of mini tiles and dynamic overlays by map id
        private readonly Dictionary<Guid, IGameRenderTexture> _minimapCache = new();
        private readonly Dictionary<Guid, IGameRenderTexture> _entityCache = new();
        private readonly Dictionary<Guid, MapPosition> _mapPosition = new();
        private readonly ImagePanel _minimap;
        private readonly Button _zoomInButton;
        private readonly Button _zoomOutButton;
        private static readonly GameContentManager ContentManager = Globals.ContentManager;
        private volatile bool _initialized;
        private bool _isClickThrough;

        public static WaypointLayer? Waypoints { get; private set; }

        public bool IsClickThrough
        {
            get => _isClickThrough;
            set
            {
                _isClickThrough = value;
                SetMouseInputEnabledRecursive(this, !value);
            }
        }

        private static void SetMouseInputEnabledRecursive(Base component, bool enabled)
        {
            component.MouseInputEnabled = enabled;

            foreach (var child in component.Children)
            {
                SetMouseInputEnabledRecursive(child, enabled);
            }
        }

        // Throttle dynamic overlay updates to ~4Hz
        private DateTime _lastOverlayUpdate = DateTime.MinValue;
        private static readonly TimeSpan OverlayInterval = TimeSpan.FromMilliseconds(250);
        private DateTime _lastDiscoverySync = DateTime.MinValue;
        private static readonly TimeSpan DiscoverySyncInterval = TimeSpan.FromSeconds(30);
        // Constructors
        public MinimapWindow(Base parent) : base(parent, Strings.Minimap.Title, false, "MinimapWindow")
        {
            IsResizable = false;
            SetZoom(Options.Instance.Minimap.DefaultZoom, false);
            _dpi = Sdl2.GetDisplayDpi();
            _minimapTileSize = Options.Instance.Minimap.GetScaledTileSize(_dpi);
            _minimap = new ImagePanel(this, "MinimapContainer");
            _zoomInButton = new Button(_minimap, "ZoomInButton");
            _zoomOutButton = new Button(_minimap, "ZoomOutButton");
            _zoomInButton.Clicked += MZoomInButton_Clicked;
            _zoomInButton.SetToolTipText(Strings.Minimap.ZoomIn);
            _zoomOutButton.Clicked += MZoomOutButton_Clicked;
            _zoomOutButton.SetToolTipText(Strings.Minimap.ZoomOut);
            _whiteTexture = Graphics.Renderer.WhitePixel;
            _renderTexture = GenerateBaseRenderTexture();
            Waypoints = new WaypointLayer(_minimap);
        }
        protected override void EnsureInitialized()
        {
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            _initialized = true;
        }
        // Public Methods
        public void Update()
        {
            if (!IsVisible() || !_initialized)
            {
                return;
            }

            var dpi = Sdl2.GetDisplayDpi();
            if (Math.Abs(dpi - _dpi) > float.Epsilon)
            {
                _dpi = dpi;
                _minimapTileSize = Options.Instance.Minimap.GetScaledTileSize(_dpi);
                _renderTexture?.Dispose();
                _renderTexture = GenerateBaseRenderTexture();
                foreach (var tex in _minimapCache.Values)
                {
                    tex.Dispose();
                }
                _minimapCache.Clear();
                foreach (var tex in _entityCache.Values)
                {
                    tex.Dispose();
                }
                _entityCache.Clear();
                _redrawMaps = true;
                _redrawEntities = true;
            }

            var now = DateTime.UtcNow;
            if (now - _lastOverlayUpdate >= OverlayInterval)
            {
                Waypoints?.Update();
                UpdateMinimap(Globals.Me, Globals.Entities);
                _lastOverlayUpdate = now;
            }

            DrawMinimap();
        }
        public void Show()
        {
            var minimapOptions = Options.Instance.Minimap;
            var minZoom = minimapOptions.MinimumZoom;
            var maxZoom = minimapOptions.MaximumZoom;
            var prefZoom = MapPreferences.Instance.MinimapZoom;

            if (prefZoom <= 0)
            {
                prefZoom = minimapOptions.DefaultZoom;
            }

            var clampedPref = Math.Clamp(prefZoom, minZoom, maxZoom);

            SetZoom(clampedPref, persist: true);
            IsHidden = false;
        }
        public bool IsVisible()
        {
            return !IsHidden;
        }
        public void Hide()
        {
            IsHidden = true;
        }

        public void SetZoom(int newLevel, bool persist = true)
        {
            newLevel = Math.Clamp(
                newLevel,
                Options.Instance.Minimap.MinimumZoom,
                Options.Instance.Minimap.MaximumZoom
            );

            if (_zoomLevel == newLevel)
            {
                return;
            }

            _zoomLevel = newLevel;

            if (persist)
            {
                MapPreferences.UpdateMinimapZoom(_zoomLevel);
            }
        }

        protected override void OnMouseDown(MouseButton mouseButton, Point mousePosition, bool userAction = true)
        {
            if (IsClickThrough)
            {
                return;
            }

            base.OnMouseDown(mouseButton, mousePosition, userAction);
        }

        protected override void OnMouseUp(MouseButton mouseButton, Point mousePosition, bool userAction = true)
        {
            if (IsClickThrough)
            {
                return;
            }

            base.OnMouseUp(mouseButton, mousePosition, userAction);
        }

        protected override bool OnMouseWheeled(int delta)
        {
            if (IsClickThrough)
            {
                return false;
            }

            var now = DateTime.UtcNow;
            if (now - _lastWheelTime < WheelDebounce)
            {
                return true;
            }

            _lastWheelTime = now;

            var step = Options.Instance.Minimap.ZoomStep;

            if (delta > 0)
            {
                SetZoom(_zoomLevel - step);
            }
            else if (delta < 0)
            {
                SetZoom(_zoomLevel + step);
            }

            return true;
        }
        // Private Methods
        private void UpdateMinimap(Player player, Dictionary<Guid, Entity> allEntities)
        {
            if (player == null)
            {
                Console.WriteLine("Player is null in UpdateMinimap.");
                return;
            }
            if (allEntities == null)
            {
                Console.WriteLine("allEntities is null in UpdateMinimap.");
                return;
            }
            if (player.MapInstance == null)
            {
                Console.WriteLine("player.MapInstance is null in UpdateMinimap.");
                return;
            }
            if (player.MapInstance.Id == Guid.Empty)
            {
                Console.WriteLine("player.MapInstance.Id is empty in UpdateMinimap.");
                return;
            }
            if (!MapInstance.TryGet(player.MapInstance.Id, out var mapInstance))
            {
                Console.WriteLine("MapInstance.TryGet failed in UpdateMinimap.");
                return;
            }
            if (_renderTexture == null)
            {
                Console.WriteLine("_renderTexture is null in UpdateMinimap.");
                return;
            }
            if (_minimapTileSize == null)
            {
                Console.WriteLine("_minimapTileSize is null in UpdateMinimap.");
                return;
            }
            var now = DateTime.UtcNow;
            if (now - _lastDiscoverySync >= DiscoverySyncInterval)
            {
                SyncDiscoveries();
            }

            var radius = Options.Instance.Map.DiscoveryRadius;
            var radiusSq = radius * radius;
            for (var dx = -radius; dx <= radius; dx++)
            {
                for (var dy = -radius; dy <= radius; dy++)
                {
                    if (dx * dx + dy * dy > radiusSq)
                    {
                        continue;
                    }

                    Globals.DiscoverTile(mapInstance.Id, player.X + dx, player.Y + dy);
                }
            }
            var newGrid = CreateMapGridFromMap(mapInstance);
            var changed = _mapGrid.Count != newGrid.Count;
            if (!changed)
            {
                foreach (var kv in newGrid)
                {
                    if (!_mapGrid.TryGetValue(kv.Key, out var old) || old?.Id != kv.Value?.Id)
                    {
                        changed = true;
                        break;
                    }
                }
            }

            if (changed)
            {
                _mapGrid = newGrid;
                _mapPosition.Clear();
                foreach (var map in _mapGrid)
                {
                    if (map.Value != null)
                    {
                        _mapPosition[map.Value.Id] = map.Key;
                    }
                }
                _redrawMaps = true;
                SyncDiscoveries();
                Globals.LoadDiscoveries(Globals.MapDiscoveries.ToDictionary(k => k.Key, v => v.Value.Data));
            }

            var newLocations = GenerateEntityInfo(allEntities, player);
            if (UpdateEntityInfoCache(newLocations))
            {
                _redrawEntities = true;
            }
            // Update our minimap display area
            var centerX = (_renderTexture.Width / 3) + (player.X * _minimapTileSize.X);
            var centerY = (_renderTexture.Height / 3) + (player.Y * _minimapTileSize.Y);
            var displayWidth = (int)(_renderTexture.Width * (_zoomLevel / 100f));
            var displayHeight = (int)(_renderTexture.Height * (_zoomLevel / 100f));
            var x = centerX - (displayWidth / 2);
            if (x + displayWidth > _renderTexture.Width)
            {
                x = _renderTexture.Width - displayWidth;
            }
            if (x < 0)
            {
                x = 0;
            }
            var y = centerY - (displayHeight / 2);
            if (y + displayHeight > _renderTexture.Height)
            {
                y = _renderTexture.Height - displayHeight;
            }
            if (y < 0)
            {
                y = 0;
            }
            _minimap.SetTextureRect(x, y, displayWidth, displayHeight);
        }
        private void DrawMinimap()
        {
            if (!_redrawEntities && !_redrawMaps)
            {
                return;
            }
            _renderTexture.Clear(Color.Transparent);
            _minimap.Texture = _renderTexture;
            _minimap.SetTextureRect(0, 0, _renderTexture.Width, _renderTexture.Height);
            foreach (var kv in _mapGrid)
            {
                if (kv.Value == null)
                {
                    continue;
                }

                if (_redrawMaps)
                {
                    GenerateMinimapCacheFor(kv.Value);
                }

                if (_redrawEntities)
                {
                    GenerateEntityCacheFor(kv.Value);
                }

                DrawMinimapCacheToTexture(kv.Value, kv.Key);
            }
            if (_redrawMaps)
            {
                _redrawMaps = false;
            }
            if (_redrawEntities)
            {
                _redrawEntities = false;
            }
        }
        private void GenerateMinimapCacheFor(MapInstance map)
        {
            if (!_minimapCache.TryGetValue(map.Id, out var cachedMinimap))
            {
                cachedMinimap = GenerateMapRenderTexture();
                _minimapCache[map.Id] = cachedMinimap;
            }

            cachedMinimap.Clear(Color.Transparent);

            foreach (var layer in Options.Instance.Minimap.RenderLayers)
            {
                for (var x = 0; x < Options.Instance.Map.MapWidth; x++)
                {
                    for (var y = 0; y < Options.Instance.Map.MapHeight; y++)
                    {
                        var curTile = map.Layers[layer][x, y];
                        if (curTile.TilesetId == Guid.Empty || !TilesetDescriptor.TryGet(curTile.TilesetId, out var tileSet))
                        {
                            continue;
                        }

                        var texture = ContentManager.GetTexture(TextureType.Tileset, tileSet.Name);
                        if (texture == null)
                        {
                            continue;
                        }

                        var color = Globals.IsTileDiscovered(map.Id, x, y)
                            ? Color.White
                            : new Color(30, 30, 30, 255);

                        Graphics.Renderer.DrawTexture(
                            texture,
                            curTile.X * Options.Instance.Map.TileWidth + (Options.Instance.Map.TileWidth / 2),
                            curTile.Y * Options.Instance.Map.TileHeight + (Options.Instance.Map.TileHeight / 2),
                            1,
                            1,
                            x * _minimapTileSize.X,
                            y * _minimapTileSize.Y,
                            _minimapTileSize.X,
                            _minimapTileSize.Y,
                            color,
                            cachedMinimap);
                    }
                }
            }
        }
        private void GenerateEntityCacheFor(MapInstance map)
        {
            if (!_entityCache.TryGetValue(map.Id, out var cachedEntity))
            {
                cachedEntity = GenerateMapRenderTexture();
                _entityCache[map.Id] = cachedEntity;
            }

            cachedEntity.Clear(Color.Transparent);

            if (!_entityInfoCache.TryGetValue(map.Id, out var cachedEntityInfo))
            {
                return;
            }

            foreach (var entity in cachedEntityInfo)
            {
                var texture = _whiteTexture;
                var color = entity.Info.Color;

                if (!string.IsNullOrWhiteSpace(entity.Info.Texture))
                {
                    var found = ContentManager.GetTexture(TextureType.Misc, entity.Info.Texture);
                    if (found != null)
                    {
                        texture = found;
                        color = Color.White;
                    }
                }

                Graphics.Renderer.DrawTexture(
                    texture,
                    0,
                    0,
                    texture.Width,
                    texture.Height,
                    entity.Position.X * _minimapTileSize.X,
                    entity.Position.Y * _minimapTileSize.Y,
                    _minimapTileSize.X,
                    _minimapTileSize.Y,
                    color,
                    cachedEntity,
                    GameBlendModes.Add);
            }
        }
        private void DrawMinimapCacheToTexture(MapInstance map, MapPosition position)
        {
            if (!_minimapCache.TryGetValue(map.Id, out var value))
            {
                return;
            }

            var x = 0;
            var y = 0;
            switch (position)
            {
                case MapPosition.TopLeft:
                    break;
                case MapPosition.TopMiddle:
                    x = value.Width;
                    break;
                case MapPosition.TopRight:
                    x = value.Width * 2;
                    break;
                case MapPosition.MiddleLeft:
                    y = value.Height;
                    break;
                case MapPosition.Middle:
                    x = value.Width;
                    y = value.Height;
                    break;
                case MapPosition.MiddleRight:
                    x = value.Width * 2;
                    y = value.Height;
                    break;
                case MapPosition.BottomLeft:
                    y = value.Height * 2;
                    break;
                case MapPosition.BottomMiddle:
                    x = value.Width;
                    y = value.Height * 2;
                    break;
                case MapPosition.BottomRight:
                    x = value.Width * 2;
                    y = value.Height * 2;
                    break;
            }

            if (_minimapCache.TryGetValue(map.Id, out var cachedMinimap))
            {
                Graphics.Renderer.DrawTexture(
                    cachedMinimap,
                    0,
                    0,
                    cachedMinimap.Width,
                    cachedMinimap.Height,
                    x,
                    y,
                    cachedMinimap.Width,
                    cachedMinimap.Height,
                    Color.White,
                    _renderTexture);
            }

            if (_entityCache.TryGetValue(map.Id, out var cachedEntity))
            {
                Graphics.Renderer.DrawTexture(
                    cachedEntity,
                    0,
                    0,
                    cachedEntity.Width,
                    cachedEntity.Height,
                    x,
                    y,
                    cachedEntity.Width,
                    cachedEntity.Height,
                    Color.White,
                    _renderTexture);
            }
        }
        private static Dictionary<MapPosition, MapInstance?> CreateMapGridFromMap(MapInstance map)
        {
            var grid = new Dictionary<MapPosition, MapInstance?>();

            for (var x = map.GridX - 1; x <= map.GridX + 1; x++)
            {
                for (var y = map.GridY - 1; y <= map.GridY + 1; y++)
                {
                    if (x < 0 || x >= Globals.MapGridWidth ||
                        y < 0 || y >= Globals.MapGridHeight)
                    {
                        continue;
                    }
                    var currentGridValue = Globals.MapGrid[x, y];
                    if (currentGridValue == Guid.Empty)
                    {
                        continue;
                    }
                    int minimapX = x - (map.GridX - 1);
                    int minimapY = y - (map.GridY - 1);
                    var mapInstanceAt = MapInstance.Get(currentGridValue);
                    if (mapInstanceAt != null)
                    {
                        grid.Add((MapPosition)(minimapX + (minimapY * 3)), mapInstanceAt);

                    }
                }
            }

            return grid;
        }
        private Dictionary<Guid, List<EntityLocation>> GenerateEntityInfo(Dictionary<Guid, Entity> entities, Player player)
        {
            var entityInfo = DictionaryPool<Guid, List<EntityLocation>>.Rent();
            var minimapOptions = Options.Instance.Minimap;
            var minimapColorOptions = minimapOptions.MinimapColors;
            var minimapImageOptions = minimapOptions.MinimapImages;

            foreach (var entity in entities.Values)
            {
                if (!_mapPosition.ContainsKey(entity.MapInstance.Id))
                {
                    continue;
                }

                if (entity.IsHidden)
                {
                    continue;
                }

                var color = Color.Transparent;
                var texture = string.Empty;

                switch (entity.Type)
                {
                    case EntityType.Player:
                        if (entity.IsStealthed)
                        {
                            color = Color.Transparent;
                            texture = string.Empty;
                        }
                        else if (entity.Id == player.Id)
                        {
                            color = minimapColorOptions.MyEntity;
                            texture = minimapImageOptions.MyEntity;
                        }
                        else if (player.IsInMyParty(entity.Id))
                        {
                            color = minimapColorOptions.PartyMember;
                            texture = minimapImageOptions.PartyMember;
                        }
                        else
                        {
                            color = minimapColorOptions.Player;
                            texture = minimapImageOptions.Player;
                        }

                        break;

                    case EntityType.Event:
                        continue;

                    case EntityType.GlobalEntity:
                        if (entity.IsStealthed)
                        {
                            color = Color.Transparent;
                            texture = string.Empty;
                        }
                        else
                        {
                            color = minimapColorOptions.Npc;
                            texture = minimapImageOptions.Npc;
                        }

                        break;

                    case EntityType.Resource:
                        var job = ((Resource)entity).Descriptor?.Jobs ?? JobType.None;

                        if (!minimapColorOptions.Resource.TryGetValue(job, out color))
                        {
                            color = minimapColorOptions.Default;
                        }

                        if (!minimapImageOptions.Resource.TryGetValue(job, out texture))
                        {
                            texture = minimapImageOptions.Default;
                        }

                        break;

                    case EntityType.Projectile:
                        continue;

                    default:
                        color = minimapColorOptions.Default;
                        texture = minimapImageOptions.Default;
                        break;
                }

                if (color == Color.Transparent && string.IsNullOrEmpty(texture))
                {
                    continue;
                }

                if (!entityInfo.TryGetValue(entity.MapInstance.Id, out var list))
                {
                    list = ListPool<EntityLocation>.Rent();
                    entityInfo.Add(entity.MapInstance.Id, list);
                }

                list.Add(new EntityLocation
                {
                    Position = new Point(entity.X, entity.Y),
                    Info = new EntityInfo { Color = color, Texture = texture }
                });
            }

            if (Options.Instance.Minimap.ShowWaypoints && Waypoints != null)
            {
                var mapWidthPixels = Options.Instance.Map.MapWidth * Options.Instance.Map.TileWidth;
                var mapHeightPixels = Options.Instance.Map.MapHeight * Options.Instance.Map.TileHeight;

                foreach (var (position, scope) in Waypoints.Enumerate())
                {
                    var mapX = position.X / mapWidthPixels;
                    var mapY = position.Y / mapHeightPixels;

                    if (mapX < 0 || mapY < 0 || mapX >= Globals.MapGridWidth || mapY >= Globals.MapGridHeight)
                    {
                        continue;
                    }

                    var mapId = Globals.MapGrid[mapX, mapY];
                    if (mapId == Guid.Empty)
                    {
                        continue;
                    }

                    var tileX = (position.X % mapWidthPixels) / Options.Instance.Map.TileWidth;
                    var tileY = (position.Y % mapHeightPixels) / Options.Instance.Map.TileHeight;

                    if (!entityInfo.TryGetValue(mapId, out var list))
                    {
                        list = ListPool<EntityLocation>.Rent();
                        entityInfo.Add(mapId, list);
                    }

                    list.Add(new EntityLocation
                    {
                        Position = new Point(tileX, tileY),
                        Info = new EntityInfo { Color = WaypointLayer.ScopeToColor(scope), Texture = string.Empty }
                    });
                }
            }

            return entityInfo;
        }

        private bool UpdateEntityInfoCache(Dictionary<Guid, List<EntityLocation>> newInfo)
        {
            var changed = _entityInfoCache.Count != newInfo.Count;
            if (!changed)
            {
                foreach (var kv in newInfo)
                {
                    if (!_entityInfoCache.TryGetValue(kv.Key, out var oldList) || oldList.Count != kv.Value.Count)
                    {
                        changed = true;
                        break;
                    }

                    for (var i = 0; i < kv.Value.Count; i++)
                    {
                        var oldEntry = oldList[i];
                        var newEntry = kv.Value[i];
                        if (!oldEntry.Position.Equals(newEntry.Position) ||
                            !oldEntry.Info.Color.Equals(newEntry.Info.Color) ||
                            oldEntry.Info.Texture != newEntry.Info.Texture)
                        {
                            changed = true;
                            break;
                        }
                    }

                    if (changed)
                    {
                        break;
                    }
                }
            }

            if (changed)
            {
                ReleaseEntityInfo(_entityInfoCache);
                DictionaryPool<Guid, List<EntityLocation>>.Return(_entityInfoCache);
                _entityInfoCache = newInfo;
            }
            else
            {
                ReleaseEntityInfo(newInfo);
                DictionaryPool<Guid, List<EntityLocation>>.Return(newInfo);
            }

            return changed;
        }

        private static void ReleaseEntityInfo(Dictionary<Guid, List<EntityLocation>> info)
        {
            foreach (var list in info.Values)
            {
                ListPool<EntityLocation>.Return(list);
            }

            info.Clear();
        }
        private IGameRenderTexture GenerateRenderTexture(int multiplier)
        {
            var sizeX = _minimapTileSize.X * Options.Instance.Map.MapWidth * multiplier;
            var sizeY = _minimapTileSize.Y * Options.Instance.Map.MapHeight * multiplier;

            return Graphics.Renderer.CreateRenderTexture(sizeX, sizeY);
        }
        private IGameRenderTexture GenerateBaseRenderTexture()
        {
            return GenerateRenderTexture(3);
        }
        private IGameRenderTexture GenerateMapRenderTexture()
        {
            return GenerateRenderTexture(1);
        }

        private void SyncDiscoveries()
        {
            PacketSender.SendMapDiscoveriesRequest(
                Globals.MapDiscoveries.ToDictionary(k => k.Key, v => v.Value.Data)
            );
            _lastDiscoverySync = DateTime.UtcNow;
        }
        private void MZoomOutButton_Clicked(Base sender, MouseButtonState arguments)
        {
            var step = Options.Instance.Minimap.ZoomStep;
            SetZoom(_zoomLevel + step);
        }
        private void MZoomInButton_Clicked(Base sender, MouseButtonState arguments)
        {
            var step = Options.Instance.Minimap.ZoomStep;
            SetZoom(_zoomLevel - step);
        }
        private enum MapPosition
        {
            TopLeft,
            TopMiddle,
            TopRight,
            MiddleLeft,
            Middle,
            MiddleRight,
            BottomLeft,
            BottomMiddle,
            BottomRight,
        }

        private sealed class EntityInfo
        {
            public Color Color { get; set; }
            public string Texture { get; set; }
        }

        private struct EntityLocation
        {
            public Point Position;
            public EntityInfo Info;
        }

        private static class DictionaryPool<TKey, TValue>
        {
            private static readonly Stack<Dictionary<TKey, TValue>> Pool = new();

            public static Dictionary<TKey, TValue> Rent()
            {
                lock (Pool)
                {
                    return Pool.Count > 0 ? Pool.Pop() : new Dictionary<TKey, TValue>();
                }
            }

            public static void Return(Dictionary<TKey, TValue> dictionary)
            {
                dictionary.Clear();
                lock (Pool)
                {
                    Pool.Push(dictionary);
                }
            }
        }

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