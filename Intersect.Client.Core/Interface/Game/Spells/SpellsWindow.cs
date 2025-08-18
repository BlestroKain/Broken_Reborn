using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellsWindow : Window
{
    private readonly ScrollControl _spellList;
    private readonly ImagePanel _detailPanel;
    private readonly Label _nameLabel;
    private readonly Label _levelLabel;
    private readonly Label _currentLabel;
    private readonly Label _nextLabel;
    private readonly Button _levelUpButton;

    private Guid _selectedSpellId = Guid.Empty;

    public List<SpellItem> Items { get; } = new();

    public SpellsWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Spells.Title, false, nameof(SpellsWindow))
    {
        Alignment = [Alignments.Bottom, Alignments.Right];
        MinimumSize = new Point(300, 300);
        Margin = new Margin(0, 0, 15, 60);
        IsResizable = false;
        IsVisibleInTree = false;
        IsClosable = true;

        _spellList = new ScrollControl(this, nameof(_spellList))
        {
            Dock = Pos.Left,
            Width = 150,
            OverflowY = OverflowBehavior.Scroll,
        };

        _detailPanel = new ImagePanel(this, nameof(_detailPanel))
        {
            Dock = Pos.Fill,
            IsVisibleInParent = false,
        };

        _nameLabel = new Label(_detailPanel, nameof(_nameLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 12,
        };
        _nameLabel.SetPosition(4, 4);

        _levelLabel = new Label(_detailPanel, nameof(_levelLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _levelLabel.SetPosition(4, 24);

        _currentLabel = new Label(_detailPanel, nameof(_currentLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _currentLabel.SetPosition(4, 44);

        _nextLabel = new Label(_detailPanel, nameof(_nextLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _nextLabel.SetPosition(4, 84);

        _levelUpButton = new Button(_detailPanel, nameof(_levelUpButton))
        {
            Text = "Level Up",
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _levelUpButton.SetPosition(4, 120);
        _levelUpButton.Clicked += (_, _) => LevelUp();
    }

    protected override void EnsureInitialized()
    {
        Refresh();
    }

    public void Refresh()
    {
        _spellList.ClearChildren();
        Items.Clear();

        if (Globals.Me?.Spellbook?.SpellLevels == null)
        {
            return;
        }

        var index = 0;
        foreach (var spellId in Globals.Me.Spellbook.SpellLevels.Keys.OrderBy(SpellDescriptor.GetName))
        {
            var item = new SpellItem(this, _spellList, index++, spellId);
            Items.Add(item);
        }

        _spellList.SetInnerSize(_spellList.Width, index * 40);
        UpdateDetails();
    }

    public void SelectSpell(Guid spellId)
    {
        _selectedSpellId = spellId;
        UpdateDetails();
    }

    public void Update()
    {
        if (!IsVisibleInParent)
        {
            return;
        }

        foreach (var item in Items)
        {
            item.Update();
        }
    }

    private void UpdateDetails()
    {
        if (_selectedSpellId == Guid.Empty || Globals.Me?.Spellbook?.SpellLevels == null)
        {
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        if (!Globals.Me.Spellbook.SpellLevels.ContainsKey(_selectedSpellId))
        {
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        if (!SpellDescriptor.TryGet(_selectedSpellId, out var descriptor))
        {
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        _detailPanel.IsVisibleInParent = true;
        _nameLabel.Text = descriptor.Name;
        var level = Globals.Me.Spellbook.GetLevelOrDefault(_selectedSpellId);
        _levelLabel.Text = Strings.EntityBox.Level.ToString(level);

        SpellProgressionStore.BySpellId.TryGetValue(_selectedSpellId, out var progression);
        var currentRow = progression?.GetLevel(level) ?? new SpellProperties();
        var currentAdjusted = SpellLevelingService.BuildAdjusted(descriptor, currentRow);
        _currentLabel.Text = FormatAdjusted(currentAdjusted);

        var nextRow = progression?.GetLevel(level + 1);
        if (nextRow != null)
        {
            var nextAdjusted = SpellLevelingService.BuildAdjusted(descriptor, nextRow);
            _nextLabel.Text = FormatAdjusted(nextAdjusted);
            _levelUpButton.IsDisabled = !(Globals.Me.Spellbook.AvailableSpellPoints > 0 && level < 5);
        }
        else
        {
            _nextLabel.Text = Strings.EntityBox.MaxLevel;
            _levelUpButton.IsDisabled = true;
        }
    }

    private static string FormatAdjusted(SpellLevelingService.EffectiveSpellStats adjusted)
    {
        return $"{Strings.SpellDescription.CastTime}: {TimeSpan.FromMilliseconds(adjusted.CastTimeMs).WithSuffix()}\n" +
               $"{Strings.SpellDescription.Cooldown}: {TimeSpan.FromMilliseconds(adjusted.CooldownTimeMs).WithSuffix()}";
    }

    private void LevelUp()
    {
        if (_selectedSpellId == Guid.Empty)
        {
            return;
        }

        PacketSender.SendRequestSpellUpgrade(_selectedSpellId);
    }
}

