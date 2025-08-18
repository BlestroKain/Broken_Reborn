using Intersect.Client.Controllers;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Utilities;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.GameObjects;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Core;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using System.Drawing;
using Intersect;
using Intersect.Enums;
namespace Intersect.Client.Interface.Game.Bestiary;
public sealed class BestiaryWindow : Window
{
    private readonly ScrollControl _tilesScroll;
    private readonly ScrollControl _detailsScroll;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private bool _sortAsc = true;
    private readonly List<BeastTile> _tiles = new();
    private Guid? _selectedNpcId;
    private Label statLbl;
    private BestiaryStatsPanel _statsPanel;

    public BestiaryWindow(Canvas canvas)
        : base(canvas, Strings.Bestiary.Title, false, nameof(BestiaryWindow))
    {
        SetSize(720, 520);
        SetPosition(100, 100);
        IsResizable = false;
        IsClosable = true;

        _searchBox = new TextBox(this,$"Searcher") { PlaceholderText = "Buscar...", Margin = new Margin(8, 8, 0, 0) };
        _searchBox.SetPosition(16, 8);
        _searchBox.SetSize(280, 40);
        _searchBox.TextChanged += (_, _) => RebuildTiles();

        _sortButton = new Button(this,$"SortBtuttom") { Text = "↕", Margin = new Margin(0, 8, 0, 0) };
        _sortButton.SetSize(30, 30);
        _sortButton.SetPosition(304, 8);
        _sortButton.Clicked += (_, _) => { _sortAsc = !_sortAsc; RebuildTiles(); };

        _tilesScroll = new ScrollControl(this, $"MobsControl");
        _tilesScroll.EnableScroll(false, true);
        _tilesScroll.SetPosition(16, 50);
        _tilesScroll.SetSize(320, 440);

        _detailsScroll = new ScrollControl(this, $"DetailsControl");
        _detailsScroll.SetPosition(352, 16);
        _detailsScroll.SetSize(340, 480);
        _detailsScroll.EnableScroll(false, true);
     
        _detailsScroll.AutoHideBars = true;
      
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        BestiaryController.InitializeAllBeasts();
        BestiaryController.OnUnlockGained += (npcId, _) =>
        {
            RefreshTilesState();
            if (_selectedNpcId == npcId)
            {
                ShowNpcDetails(npcId);
            }
        };

        BuildTiles();
    }

    private void BuildTiles()
    {
        foreach (var ch in _tiles) _tilesScroll.RemoveChild(ch, dispose: true);
        _tiles.Clear();

        var ids = BestiaryController.AllBeastNpcIds;
        IEnumerable<Guid> filtered = ids;

        if (!string.IsNullOrWhiteSpace(_searchBox.Text))
        {
            filtered = ids.Where(id =>
                NPCDescriptor.TryGet(id, out var d) &&
                SearchHelper.Matches(_searchBox.Text, d.Name));
        }

        filtered = _sortAsc
            ? filtered.OrderBy(id => NPCDescriptor.TryGet(id, out var d) ? d.Name : string.Empty)
            : filtered.OrderByDescending(id => NPCDescriptor.TryGet(id, out var d) ? d.Name : string.Empty);

        var list = filtered.ToList();
        const int tileW = 84, tileH = 104, pad = 6;
        var cols = Math.Max(1, (_tilesScroll.Width - _tilesScroll.VerticalScrollBar.Width) / (tileW + pad));

        for (int i = 0; i < list.Count; i++)
        {
            var tile = new BeastTile(_tilesScroll, list[i]);
            tile.ClickedNpc += OnSelectNpc;

            var col = i % cols;
            var row = i / cols;
            tile.SetBounds(col * (tileW + pad), row * (tileH + pad), tileW, tileH);

            _tiles.Add(tile);
        }
    }

    private void RebuildTiles()
    {
        foreach (var tile in _tiles)
        {
            var match = SearchHelper.Matches(
                _searchBox.Text,
                NPCDescriptor.TryGet(tile.NpcId, out var d) ? d.Name : ""
            );

            tile.SetFilterMatch(match);
            tile.Update(); // Actualiza su visibilidad y apariencia
        }
    }


    private void RefreshTilesState() => _tiles.ForEach(t => t.RefreshState());

    private void OnSelectNpc(Guid npcId)
    {
        _selectedNpcId = npcId;
        ShowNpcDetails(npcId);
    }

    private void ShowNpcDetails(Guid npcId)
    {
        _detailsScroll.DeleteAllChildren();

        if (!NPCDescriptor.TryGet(npcId, out var desc)) return;

        int yOffset = 0;

        var title = new Label(_detailsScroll, "NpcTitle")
        {
            Text = desc.Name,
            FontName = "sourcesansproblack",
            FontSize = 14,
            TextColorOverride = Color.White,
        };
        title.SetPosition(10, yOffset);
        title.SetSize(300, 30);
        yOffset += 35;

        // Secciones condicionales por desbloqueo
        AddSection(npcId, BestiaryUnlock.Stats, "Estadísticas", desc, ref yOffset);
        AddSection(npcId, BestiaryUnlock.Drops, "Drops", desc, ref yOffset);
        AddSection(npcId, BestiaryUnlock.Spells, "Hechizos", desc, ref yOffset);
        AddSection(npcId, BestiaryUnlock.Behavior, "Comportamiento", desc, ref yOffset);
        AddSection(npcId, BestiaryUnlock.Lore, "Historia", desc, ref yOffset);
    }

    private void AddSection(Guid npcId, BestiaryUnlock unlock, string title, NPCDescriptor desc, ref int yOffset)
    {
        var unlocked = BestiaryController.HasUnlock(npcId, unlock);

        var sectionTitle = new Label(_detailsScroll, $"{unlock}SectionTitle")
        {
            Text = title,
            FontName = "sourcesansproblack",
            FontSize = 11,
            TextColorOverride = Color.White,
            AutoSizeToContents = true,
        };
        sectionTitle.SetPosition(10, yOffset);
        sectionTitle.SetSize(300, 22);
        yOffset += 22;

        if (!unlocked)
        {
            var killsReq = desc.BestiaryRequirements.TryGetValue(unlock, out var req) ? req : 0;
            var currentKills = BestiaryController.GetKillCount(npcId);
            var lockedText = killsReq > 0
                ? $"🔒 Derrota {currentKills}/{killsReq} veces para desbloquear."
                : "🔒 Información bloqueada.";

            var label = new Label(_detailsScroll, $"{unlock}LockedLabel")
            {
                Text = lockedText,
                FontSize = 10,
                TextColorOverride = Color.White,
                AutoSizeToContents = true,
            };
            label.SetPosition(20, yOffset);
            label.SetSize(300, 22);
            yOffset += 24;
            return;
        }

        // Contenido real por sección
        switch (unlock)
        {
            case BestiaryUnlock.Stats:
                if (_statsPanel == null)
                {
                    _statsPanel = new BestiaryStatsPanel(_detailsScroll);
                    _statsPanel.SetPosition(20, yOffset);
                }
                _statsPanel.IsHidden = false;

                _statsPanel.UpdateData(desc);
           

                yOffset += _statsPanel.Height + 8;
                break;


            case BestiaryUnlock.Drops:
                
                    const int maxPerRow = 6;
                    const int iconSize = 40;
                    const int spacing = 6;
                    int index = 0;

                    foreach (var drop in desc.Drops)
                    {
                        if (!ItemDescriptor.TryGet(drop.ItemId, out _)) continue;

                        var col = index % maxPerRow;
                        var row = index / maxPerRow;

                        var dropDisplay = new BestiaryItemDisplay(_detailsScroll, drop.ItemId, drop.Chance);
                        int x = 20 + col * (iconSize + spacing);
                        int y = yOffset + row * (iconSize + spacing);
                        dropDisplay.SetPosition(x, y);

                        index++;
                    }

                    // Aumenta yOffset según las filas usadas
                    int totalRows = (index + maxPerRow - 1) / maxPerRow;
                    yOffset += totalRows * (iconSize + spacing);
                
                    break;           

            case BestiaryUnlock.Spells:
                foreach (var spellId in desc.Spells)
                {
                    var spell = SpellDescriptor.Get(spellId);
                    if (spell == null) continue;

                    var spellDisplay = new BestiarySpellDisplay(_detailsScroll, spell);
                    spellDisplay.SetPosition(20, yOffset);
                    yOffset += spellDisplay.Height + 4;
                }
                break;

            case BestiaryUnlock.Behavior:
                AddText($"Agresivo: {(desc.Aggressive ? "Sí" : "No")}", ref yOffset, "AggressiveLabel");
                AddText($"Movimiento: {desc.Movement}", ref yOffset, "MovementLabel");
                AddText($"Flee HP %: {desc.FleeHealthPercentage}%", ref yOffset, "FleeHpLabel");
                AddText($"Swarm: {(desc.Swarm ? "Sí" : "No")}", ref yOffset, "SwarmLabel");
                break;

            case BestiaryUnlock.Lore:
                AddText("(Aquí puedes insertar un sistema de descripciones opcionales por NPC)", ref yOffset, "LoreLabel");
                break;
        }

        yOffset += 4;
    }

    private void AddText(string content, ref int yOffset, string name)
    {
        var lbl = new Label(_detailsScroll, name)
        {
            Text = content,
            FontSize = 10,
            TextColorOverride = Color.White,
            AutoSizeToContents = true,
            
        };
        lbl.SetPosition(20, yOffset);
        lbl.SetSize(300, 30);
        lbl.SizeToContents();
        yOffset += lbl.Height + 4;
      
    }

    internal void Update()
    {
        if (!IsVisibleInParent) return;
        foreach (var tile in _tiles) tile.Update();
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }
}
