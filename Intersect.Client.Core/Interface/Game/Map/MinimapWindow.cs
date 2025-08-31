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
using Intersect.Client.Core.Controls;
using Intersect.Client.Maps;
using Intersect.Config;
using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Framework.Core.GameObjects.Mapping.Tilesets;
using Intersect.Framework.Core.GameObjects.Maps;
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
        private Dictionary<Guid, List<EntityLocation>> _entityInfoCache = new();
        private readonly Point _minimapTileSize;
        // Cache of mini tiles and dynamic overlays by map id
        private readonly Dictionary<Guid, IGameRenderTexture> _minimapCache = new();
        private readonly Dictionary<Guid, IGameRenderTexture> _entityCache = new();
        private readonly Dictionary<Guid, MapPosition> _mapPosition = new();
        private readonly ImagePanel _minimap;
        private readonly Button _zoomInButton;
        private readonly Button _zoomOutButton;
        private readonly Button _worldMapButton;
        private static readonly GameContentManager ContentManager = Globals.ContentManager;
        private volatile bool _initialized;

        // Throttle dynamic overlay updates to ~4Hz
        private DateTime _lastOverlayUpdate = DateTime.MinValue;
        private static readonly TimeSpan OverlayInterval = TimeSpan.FromMilliseconds(250);
        // Constructors
        public MinimapWindow(Base parent) : base(parent, Strings.Minimap.Title, false, "MinimapWindow")
        {
            DisableResizing();
            _zoomLevel = Options.Instance.Minimap.DefaultZoom;
            _minimapTileSize = Options.Instance.Minimap.TileSize;
            _minimap = new ImagePanel(this, "MinimapContainer");
            _zoomInButton = new Button(_minimap, "ZoomInButton");
            _zoomOutButton = new Button(_minimap, "ZoomOutButton");
            _worldMapButton = new Button(_minimap, "WorldMapButton");
            _zoomInButton.Clicked += MZoomInButton_Clicked;
            _zoomInButton.SetToolTipText(Strings.Minimap.ZoomIn);
            _zoomOutButton.Clicked += MZoomOutButton_Clicked;
            _zoomOutButton.SetToolTipText(Strings.Minimap.ZoomOut);
            _worldMapButton.Clicked += OpenWorldMapButton_Clicked;
            var keyHint = GetMinimapKeyHint();
            _worldMapButton.SetToolTipText(string.IsNullOrEmpty(keyHint) ? Strings.WorldMap.Title : $"{Strings.WorldMap.Title} ({keyHint})");
            _whiteTexture = Graphics.Renderer.WhitePixel;
            _renderTexture = GenerateBaseRenderTexture();
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

            var now = DateTime.UtcNow;
            if (now - _lastOverlayUpdate >= OverlayInterval)
            {
                UpdateMinimap(Globals.Me, Globals.Entities);
                _lastOverlayUpdate = now;
            }

            DrawMinimap();
        }
        public void Show()
        {
            _zoomLevel = Math.Clamp(
                MapPreferences.Instance.MinimapZoom,
                Options.Instance.Minimap.MinimumZoom,
                Options.Instance.Minimap.MaximumZoom
            );
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
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
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
            var entityInfo = new Dictionary<Guid, List<EntityLocation>>();
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
                        if (MapPreferences.Instance.ActiveFilters.TryGetValue(job.ToString(), out var enabled) && !enabled)
                        {
                            continue;
                        }

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
                _entityInfoCache = newInfo;
            }
            else
            {
                ReleaseEntityInfo(newInfo);
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
        private void MZoomOutButton_Clicked(Base sender, MouseButtonState arguments)
        {
            _zoomLevel = Math.Min(
                _zoomLevel + Options.Instance.Minimap.ZoomStep,
                Options.Instance.Minimap.MaximumZoom
            );
            MapPreferences.Instance.MinimapZoom = _zoomLevel;
            MapPreferences.Save();
        }
        private void MZoomInButton_Clicked(Base sender, MouseButtonState arguments)
        {
            _zoomLevel = Math.Max(
                _zoomLevel - Options.Instance.Minimap.ZoomStep,
                Options.Instance.Minimap.MinimumZoom
            );
            MapPreferences.Instance.MinimapZoom = _zoomLevel;
            MapPreferences.Save();
        }

        private void OpenWorldMapButton_Clicked(Base sender, MouseButtonState arguments)
        {
          Interface.GameUi.GameMenu?.ToggleWorldMapWindow();
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