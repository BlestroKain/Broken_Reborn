using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Controllers;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.Interface.Game.Bestiary;

public sealed class BestiaryWindow : Window
{
    private readonly ScrollControl _tilesScroll;
    private readonly ImagePanel _detailsPanel;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;

    private bool _sortAsc = true;
    private readonly List<BeastTile> _tiles = new();

    public BestiaryWindow(Canvas canvas)
        : base(canvas, Strings.Bestiary.Title, false, nameof(BestiaryWindow))
    {
        DisableResizing();
        IsClosable = true; IsResizable = false;

        _tilesScroll = new ScrollControl(this, "TilesScroll") { Dock = Pos.Left, Width = 320, OverflowY = OverflowBehavior.Scroll };
        _detailsPanel = new ImagePanel(this, "DetailsPanel") { Dock = Pos.Fill };

        _searchBox = new TextBox(this, "SearchBox") { Margin = new Margin(4), Width = 150 };
        _searchBox.TextChanged += (_, _) => RebuildTiles();

        _sortButton = new Button(this, "SortButton") { Margin = new Margin(4) };
        _sortButton.SetText("Sort");
        _sortButton.Clicked += (_, _) => { _sortAsc = !_sortAsc; RebuildTiles(); };

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        BestiaryController.InitializeAllBeasts();
        BestiaryController.OnUnlockGained += (_, _) => RefreshTilesState();

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

    private void RebuildTiles() => BuildTiles();
    private void RefreshTilesState() { foreach (var t in _tiles) t.RefreshState(); }

    private void OnSelectNpc(Guid npcId)
    {
        ShowNpcDetails(npcId);
    }

    private void ShowNpcDetails(Guid npcId)
    {
        _detailsPanel.DeleteAllChildren();

        if (!NPCDescriptor.TryGet(npcId, out var desc)) return;

        var header = new Label(_detailsPanel, "Title") { Text = desc.Name, FontName = "sourcesansproblack", FontSize = 12, Margin = new Margin(8, 8, 8, 4) };

        AddSection(BestiaryUnlock.Stats, "Stats", npcId);
        AddSection(BestiaryUnlock.Drops, "Drops", npcId);
        AddSection(BestiaryUnlock.Spells, "Spells", npcId);
        AddSection(BestiaryUnlock.Behavior, "Behavior", npcId);
        AddSection(BestiaryUnlock.Lore, "Lore", npcId);
    }

    private void AddSection(BestiaryUnlock unlock, string title, Guid npcId)
    {
        var section = new ImagePanel(_detailsPanel, title) { Margin = new Margin(8, 4, 8, 4) };
        var unlocked = BestiaryController.HasUnlock(npcId, unlock);

        new Label(section, $"{title}_Label") { Text = title, FontName = "sourcesansproblack", FontSize = 10 };

        if (!unlocked)
        {
            var req = 0;
            if (NPCDescriptor.TryGet(npcId, out var d) && d.BestiaryRequirements.TryGetValue(unlock, out var kills))
                req = kills;

            new Label(section, $"{title}_Locked")
            {
                Text = req > 0 ? $"🔒 Derrota a esta criatura {req} veces para desbloquear." : "🔒 Bloqueado.",
                Margin = new Margin(0, 4, 0, 0)
            };
            return;
        }

        new Label(section, $"{title}_Content") { Text = "TODO", Margin = new Margin(0, 4, 0, 0) };
    }
}
