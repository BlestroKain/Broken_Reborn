using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Core.Networking;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;

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

        if (Globals.Me?.Spellbook?.Spells == null)
        {
            return;
        }

        var index = 0;
        foreach (var (spellId, properties) in Globals.Me.Spellbook.Spells.OrderBy(p => SpellDescriptor.GetName(p.Key)))
        {
            var item = new SpellItem(this, _spellList, index++, spellId, properties);
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

    private void UpdateDetails()
    {
        if (_selectedSpellId == Guid.Empty || Globals.Me?.Spellbook?.Spells == null)
        {
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        if (!Globals.Me.Spellbook.Spells.TryGetValue(_selectedSpellId, out var properties))
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
        _levelLabel.Text = Strings.EntityBox.Level.ToString(properties.Level);

        var currentAdjusted = SpellLevelingService.BuildAdjusted(descriptor, properties);
        _currentLabel.Text = FormatAdjusted(currentAdjusted);

        SpellProperties? nextRow = null;
        if (SpellProgressionStore.BySpellId.TryGetValue(_selectedSpellId, out var progression))
        {
            nextRow = progression.GetLevel(properties.Level + 1);
        }

        if (nextRow != null)
        {
            var nextAdjusted = SpellLevelingService.BuildAdjusted(descriptor, nextRow);
            _nextLabel.Text = FormatAdjusted(nextAdjusted);
            _levelUpButton.IsDisabled = !(Globals.Me.Spellbook.AvailableSpellPoints > 0 && properties.Level < 5);
        }
        else
        {
            _nextLabel.Text = Strings.EntityBox.MaxLevel;
            _levelUpButton.IsDisabled = true;
        }
    }

    private static string FormatAdjusted(SpellLevelingService.AdjustedSpell adjusted)
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

