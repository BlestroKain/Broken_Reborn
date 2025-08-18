using Intersect.Client.Controllers;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen;

using Intersect.Client.Utilities;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.GameObjects;
using Intersect.Client.Interface;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Core;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using System.Drawing;
using Intersect;
namespace Intersect.Client.Interface.Game.Bestiary;
public sealed class BestiaryWindow : Window
{
    private readonly ScrollControl _tilesScroll;
    private readonly ImagePanel _detailsPanel;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private bool _sortAsc = true;
    private readonly List<BeastTile> _tiles = new();
    private Guid? _selectedNpcId;

    public BestiaryWindow(Canvas canvas)
        : base(canvas, Strings.Bestiary.Title, false, nameof(BestiaryWindow))
    {
        SetSize(720, 520);
        SetPosition(100, 100);
        IsResizable = false;
        IsClosable = true;

        _searchBox = new TextBox(this) { PlaceholderText = "Buscar...", Margin = new Margin(8, 8, 0, 0) };
        _searchBox.SetPosition(16, 8);
        _searchBox.SetSize(280, 30);
        _searchBox.TextChanged += (_, _) => RebuildTiles();

        _sortButton = new Button(this) { Text = "↕", Margin = new Margin(0, 8, 0, 0) };
        _sortButton.SetSize(30, 30);
        _sortButton.SetPosition(304, 8);
        _sortButton.Clicked += (_, _) => { _sortAsc = !_sortAsc; RebuildTiles(); };

        _tilesScroll = new ScrollControl(this);


        _tilesScroll.EnableScroll(false, true);
        
        _tilesScroll.SetPosition(16, 50);
        _tilesScroll.SetSize(320, 440);

        _detailsPanel = new ImagePanel(this);
        _detailsPanel.SetPosition(352, 16);
        _detailsPanel.SetSize(340, 480);

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
        _detailsPanel.DeleteAllChildren();

        if (!NPCDescriptor.TryGet(npcId, out var desc)) return;

        int yOffset = 0;
        const int spacing = 26;

        var title = new Label(_detailsPanel)
        {
            Text = desc.Name,
            FontName = "sourcesansproblack",
            FontSize = 14,
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

        var sectionTitle = new Label(_detailsPanel)
        {
            Text = title,
            FontName = "sourcesansproblack",
            FontSize = 11,
            TextColorOverride= Intersect.Color.White,
        };
        sectionTitle.SetPosition(10, yOffset);
        sectionTitle.SetSize(300, 22);
        yOffset += 22;

        if (!unlocked)
        {
            var killsReq = desc.BestiaryRequirements.TryGetValue(unlock, out var req) ? req : 0;
            var lockedText = killsReq > 0
                ? $"🔒 Derrota {killsReq} veces para desbloquear."
                : "🔒 Información bloqueada.";

            var label = new Label(_detailsPanel)
            {
                Text = lockedText,
                FontSize = 10,
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
                foreach (var stat in desc.StatsLookup)
                {
                    var statLbl = new Label(_detailsPanel)
                    {
                        Text = $"{Strings.ItemDescription.StatCounts[(int)stat.Key]}: {stat.Value}",
                        FontSize = 10
                    };
                    statLbl.SetPosition(20, yOffset);
                    statLbl.SetSize(300, 20);
                    yOffset += 20;
                }
                break;

            case BestiaryUnlock.Drops:
                foreach (var drop in desc.Drops)
                {
                    var item = ItemDescriptor.Get(drop.ItemId);
                    if (item == null) continue;

                    var dropLbl = new Label(_detailsPanel)
                    {
                        Text = $"- {item.Name} ({drop.Chance}% chance)",
                        FontSize = 10
                    };
                    dropLbl.SetPosition(20, yOffset);
                    dropLbl.SetSize(300, 20);
                    yOffset += 20;
                }
                break;

            case BestiaryUnlock.Spells:
                foreach (var spellId in desc.Spells)
                {
                    var spell = SpellDescriptor.Get(spellId);
                    if (spell == null) continue;

                    var spellLbl = new Label(_detailsPanel)
                    {
                        Text = $"- {spell.Name}",
                        FontSize = 10
                    };
                    spellLbl.SetPosition(20, yOffset);
                    spellLbl.SetSize(300, 20);
                    yOffset += 20;
                }
                break;

            case BestiaryUnlock.Behavior:
                AddText($"Agresivo: {(desc.Aggressive ? "Sí" : "No")}", ref yOffset);
                AddText($"Movimiento: {desc.Movement}", ref yOffset);
                AddText($"Flee HP %: {desc.FleeHealthPercentage}%", ref yOffset);
                AddText($"Swarm: {(desc.Swarm ? "Sí" : "No")}", ref yOffset);
                break;

            case BestiaryUnlock.Lore:
                AddText("(Aquí puedes insertar un sistema de descripciones opcionales por NPC)", ref yOffset);
                break;
        }
    }

    private void AddText(string content, ref int yOffset)
    {
        var lbl = new Label(_detailsPanel)
        {
            Text = content,
            FontSize = 10
        };
        lbl.SetPosition(20, yOffset);
        lbl.SetSize(300, 20);
        yOffset += 20;
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
