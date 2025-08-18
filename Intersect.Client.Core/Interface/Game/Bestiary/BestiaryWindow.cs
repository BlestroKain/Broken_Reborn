using Intersect.Client.Controllers;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen;

using Intersect.Client.Utilities;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.Interface;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Core;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using System.Drawing;
using System;
using Intersect;
namespace Intersect.Client.Interface.Game.Bestiary;
public sealed class BestiaryWindow : Window
{
    private readonly ScrollControl _tilesScroll;
    private readonly ScrollControl _detailsScroll;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private bool _sortAsc = true;
    private readonly List<BeastTile> _tiles = new();
    private readonly List<Control> _dynamicControls = new();
    private Guid? _selectedNpcId;

    // Secciones
    private Label _npcTitle;
    private Label _statsTitle, _dropsTitle, _spellsTitle, _behaviorTitle, _loreTitle;

    // Labels de stats
    private Label _levelLabel, _healthLabel, _manaLabel;
    private Label _minDamageLabel, _maxDamageLabel, _experienceLabel;
    private Label _aggressiveLabel, _movementLabel, _fleeHpLabel, _swarmLabel;

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

        _detailsScroll = new ScrollControl(this);
        _detailsScroll.SetPosition(352, 16);
        _detailsScroll.SetSize(340, 480);
        _detailsScroll.EnableScroll(false, true);

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitializeDetailsComponents();

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

    private void InitializeDetailsComponents()
    {
        _npcTitle = _detailsScroll.GetChild<Label>("NpcTitle");

        _statsTitle = _detailsScroll.GetChild<Label>("StatsSectionTitle");
        _dropsTitle = _detailsScroll.GetChild<Label>("DropsSectionTitle");
        _spellsTitle = _detailsScroll.GetChild<Label>("SpellsSectionTitle");
        _behaviorTitle = _detailsScroll.GetChild<Label>("BehaviorSectionTitle");
        _loreTitle = _detailsScroll.GetChild<Label>("LoreSectionTitle");

        _levelLabel = _detailsScroll.GetChild<Label>("LevelLabel");
        _healthLabel = _detailsScroll.GetChild<Label>("HealthLabel");
        _manaLabel = _detailsScroll.GetChild<Label>("ManaLabel");

        _minDamageLabel = _detailsScroll.GetChild<Label>("MinDamageLabel");
        _maxDamageLabel = _detailsScroll.GetChild<Label>("MaxDamageLabel");
        _experienceLabel = _detailsScroll.GetChild<Label>("ExperienceLabel");

        _aggressiveLabel = _detailsScroll.GetChild<Label>("AggressiveLabel");
        _movementLabel = _detailsScroll.GetChild<Label>("MovementLabel");
        _fleeHpLabel = _detailsScroll.GetChild<Label>("FleeHpLabel");
        _swarmLabel = _detailsScroll.GetChild<Label>("SwarmLabel");
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
        foreach (var ctrl in _dynamicControls)
        {
            _detailsScroll.RemoveChild(ctrl, dispose: true);
        }
        _dynamicControls.Clear();

        if (!NPCDescriptor.TryGet(npcId, out var desc)) return;

        int yOffset = 0;

        _npcTitle.Text = desc.Name;
        _npcTitle.SetPosition(10, yOffset);
        _npcTitle.SizeToContents();
        yOffset += 35;

        _statsTitle.Text = "Estadísticas";
        AddSection(npcId, BestiaryUnlock.Stats, _statsTitle, desc, ref yOffset);

        _dropsTitle.Text = "Drops";
        AddSection(npcId, BestiaryUnlock.Drops, _dropsTitle, desc, ref yOffset);

        _spellsTitle.Text = "Hechizos";
        AddSection(npcId, BestiaryUnlock.Spells, _spellsTitle, desc, ref yOffset);

        _behaviorTitle.Text = "Comportamiento";
        AddSection(npcId, BestiaryUnlock.Behavior, _behaviorTitle, desc, ref yOffset);

        _loreTitle.Text = "Historia";
        AddSection(npcId, BestiaryUnlock.Lore, _loreTitle, desc, ref yOffset);

        _detailsScroll.SetInnerSize(_detailsScroll.Width, yOffset);
    }

    private void AddSection(Guid npcId, BestiaryUnlock unlock, Label sectionTitle, NPCDescriptor desc, ref int yOffset)
    {
        var unlocked = BestiaryController.HasUnlock(npcId, unlock);

        sectionTitle.SetPosition(10, yOffset);
        sectionTitle.SizeToContents();
        yOffset += 22;

        if (!unlocked)
        {
            var killsReq = desc.BestiaryRequirements.TryGetValue(unlock, out var req) ? req : 0;
            var currentKills = BestiaryController.GetKillCount(npcId);
            var lockedText = killsReq > 0
                ? $"\ud83d\udd12 Derrota {currentKills}/{killsReq} veces para desbloquear."
                : "\ud83d\udd12 Información bloqueada.";

            var label = new Label(_detailsScroll, $"{unlock}LockedLabel")
            {
                Text = lockedText,
                FontSize = 10,
                TextColorOverride = Color.White,
            };
            label.SetPosition(20, yOffset);
            label.SizeToContents();
            yOffset += 24;
            _dynamicControls.Add(label);
            return;
        }

        switch (unlock)
        {
            case BestiaryUnlock.Stats:
                var statIndex = 0;
                foreach (var stat in desc.StatsLookup)
                {
                    if (stat.Value == 0)
                    {
                        continue;
                    }

                    var statLbl = new Label(_detailsScroll, $"StatLabel_{statIndex++}")
                    {
                        Text = $"{Strings.ItemDescription.StatCounts[(int)stat.Key]}: {stat.Value}",
                        FontSize = 10,
                        TextColorOverride = Color.White,
                    };
                    statLbl.SetPosition(20, yOffset);
                    statLbl.SizeToContents();
                    yOffset += 20;
                    _dynamicControls.Add(statLbl);
                }

                UpdateLabel(_levelLabel, $"Nivel: {desc.Level}", ref yOffset);
                UpdateLabel(_healthLabel, $"Vida: {desc.MaxVitalsLookup[Vital.Health]}", ref yOffset);
                UpdateLabel(_manaLabel, $"Mana: {desc.MaxVitalsLookup[Vital.Mana]}", ref yOffset);

                var scalingValue = desc.Stats.Length > desc.ScalingStat ? desc.Stats[desc.ScalingStat] : 0;
                var baseDamage = desc.Damage + scalingValue * (desc.Scaling / 100f);
                UpdateLabel(_minDamageLabel, $"Daño Mínimo: {(int)Math.Round(baseDamage * 0.975f)}", ref yOffset);
                UpdateLabel(_maxDamageLabel, $"Daño Máximo: {(int)Math.Round(baseDamage * 1.025f)}", ref yOffset);
                UpdateLabel(_experienceLabel, $"Experiencia: {desc.Experience}", ref yOffset);
                break;

            case BestiaryUnlock.Drops:
                foreach (var drop in desc.Drops)
                {
                    var item = ItemDescriptor.Get(drop.ItemId);
                    if (item == null) continue;

                    var dropDisplay = new BestiaryItemDisplay(_detailsScroll, item, drop.Chance);
                    dropDisplay.SetPosition(20, yOffset);
                    yOffset += 24;
                    _dynamicControls.Add(dropDisplay);
                }
                break;

            case BestiaryUnlock.Spells:
                foreach (var spellId in desc.Spells)
                {
                    var spell = SpellDescriptor.Get(spellId);
                    if (spell == null) continue;

                    var spellDisplay = new BestiarySpellDisplay(_detailsScroll, spell);
                    spellDisplay.SetPosition(20, yOffset);
                    yOffset += 24;
                    _dynamicControls.Add(spellDisplay);
                }
                break;

            case BestiaryUnlock.Behavior:
                UpdateLabel(_aggressiveLabel, $"Agresivo: {(desc.Aggressive ? \"Sí\" : \"No\")}", ref yOffset);
                UpdateLabel(_movementLabel, $"Movimiento: {desc.Movement}", ref yOffset);
                UpdateLabel(_fleeHpLabel, $"Flee HP %: {desc.FleeHealthPercentage}%", ref yOffset);
                UpdateLabel(_swarmLabel, $"Swarm: {(desc.Swarm ? \"Sí\" : \"No\")}", ref yOffset);
                break;

            case BestiaryUnlock.Lore:
                var loreLabel = new Label(_detailsScroll, "LoreLabel")
                {
                    Text = "(Aquí puedes insertar un sistema de descripciones opcionales por NPC)",
                    FontSize = 10,
                    TextColorOverride = Color.White,
                };
                loreLabel.SetPosition(20, yOffset);
                loreLabel.SizeToContents();
                yOffset += 20;
                _dynamicControls.Add(loreLabel);
                break;
        }
    }

    private void UpdateLabel(Label label, string content, ref int yOffset)
    {
        label.Text = content;
        label.FontSize = 10;
        label.TextColorOverride = Color.White;
        label.SetPosition(20, yOffset);
        label.SizeToContents();
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
