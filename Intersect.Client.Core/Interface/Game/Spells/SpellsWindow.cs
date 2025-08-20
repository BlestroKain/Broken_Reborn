using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;
using Intersect.Framework.Core.Utilities;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellsWindow : Window
{
    private readonly ScrollControl _spellList;
    private readonly ImagePanel _detailPanel;
    private readonly ImagePanel _iconPanel;
    private readonly Label _nameLabel;
    private readonly Label _levelLabel;
    private readonly Label _currentLabel;
    private readonly Label _nextLabel;
    private readonly Button _levelUpButton;

    private Guid _selectedSpellId = Guid.Empty;
    private bool _levelUpPending;

    public List<SpellItem> Items { get; } = new();

    public SpellsWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Spells.Title, false, nameof(SpellsWindow))
    {

        Alignment = [Alignments.Bottom, Alignments.Right];
        MinimumSize = new Point(560, 340);
        Margin = new Margin(0, 0, 15, 60);
        IsVisibleInTree = false;
        IsResizable = false;
        IsClosable = true;

        _spellList = new ScrollControl(this, nameof(_spellList))
        {
            Dock = Pos.Left,
            Width = 240,
            OverflowX = OverflowBehavior.Hidden,
            OverflowY = OverflowBehavior.Scroll,
        };

        _detailPanel = new ImagePanel(this, nameof(_detailPanel))
        {
            Dock = Pos.Fill,
            IsVisibleInParent = false,
        };

        _iconPanel = new ImagePanel(_detailPanel, nameof(_iconPanel));
        _iconPanel.SetBounds(8, 8, 48, 48);

        _nameLabel = new Label(_detailPanel, nameof(_nameLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 13,
        };
        _nameLabel.SetPosition(64, 8);

        _levelLabel = new Label(_detailPanel, nameof(_levelLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _levelLabel.SetPosition(64, 30);

        _currentLabel = new Label(_detailPanel, nameof(_currentLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _currentLabel.SetPosition(8, 70);

        _nextLabel = new Label(_detailPanel, nameof(_nextLabel))
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _nextLabel.SetPosition(8, 140);

        _levelUpButton = new Button(_detailPanel, nameof(_levelUpButton))
        {
            Text = "+",
            FontName = "sourcesansproblack",
            FontSize = 14,
        };
        _levelUpButton.SetPosition(8, 210);
   
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        _levelUpButton.Clicked += (_, _) => LevelUp();
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        Refresh();
    }

    public void Refresh()
    {
        _spellList.ClearChildren();
        Items.Clear();

        var spellLevels = Globals.Me?.Spellbook?.SpellLevels;
        if (spellLevels == null || spellLevels.Count == 0)
        {
            _selectedSpellId = Guid.Empty;
            UpdateDetails();
            return;
        }

        var index = 0;
        foreach (var spellId in spellLevels.Keys
            .Where(id => SpellDescriptor.TryGet(id, out _))
            .OrderBy(id => SpellDescriptor.TryGet(id, out var d) ? d.Name : string.Empty))
        {
            var item = new SpellItem(this, _spellList, index++, spellId);
            Items.Add(item);
        }

        // COLOCAR cada item (como hacía el código original con PopulateSlotContainer)
           const int rowH = 36;
           for (var i = 0; i < Items.Count; i++)
                {
            Items[i].SetBounds(0, i * (rowH + 2), _spellList.Width - 6, rowH);
            Items[i].Refresh(); // por si el load se hizo antes de tener bounds
                }
        _spellList.SetInnerSize(_spellList.Width, Items.Count * (rowH + 2));

        TrySelectFirstIfNone();
        _levelUpPending = false;
        UpdateDetails();
    }


    private void TrySelectFirstIfNone()
    {
        if (_selectedSpellId != Guid.Empty) return;

        var spellLevels = Globals.Me?.Spellbook?.SpellLevels;
        if (spellLevels == null || spellLevels.Count == 0) return;

        _selectedSpellId = spellLevels.Keys
            .Where(id => SpellDescriptor.TryGet(id, out _))
            .OrderBy(id => SpellDescriptor.TryGet(id, out var d) ? d.Name : string.Empty)
            .First();
    }

    public void SelectSpell(Guid spellId)
    {
        _selectedSpellId = spellId;
        UpdateDetails();
    }

    public void Update()
    {
        if (!IsVisibleInTree)
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
        var spellbook = Globals.Me?.Spellbook;
        if (_selectedSpellId == Guid.Empty || spellbook?.SpellLevels == null)
        {
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        if (!spellbook.SpellLevels.ContainsKey(_selectedSpellId))
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

        var tex = GameContentManager.Current.GetTexture(TextureType.Spell, descriptor.Icon);
        if (tex != null)
        {
            _iconPanel.Texture = tex;
            _iconPanel.IsVisibleInParent = true;
        }
        else
        {
            _iconPanel.IsVisibleInParent = false;
        }

        _nameLabel.Text = descriptor.Name;
        var level = spellbook.GetLevelOrDefault(_selectedSpellId);
        _levelLabel.Text = Strings.EntityBox.Level.ToString(level);

        var current = SpellMath.GetEffective(descriptor, level);
        _currentLabel.Text = FormatAdjusted(descriptor, current);

        var levelsCount = descriptor.Progression?.Count ?? 0;
        var hasNext = level < levelsCount;

        if (hasNext)
        {
            var next = SpellMath.GetEffective(descriptor, level + 1);
            _nextLabel.Text = FormatAdjusted(descriptor, next);
            _levelUpButton.IsDisabled = _levelUpPending || spellbook.AvailableSpellPoints <= 0;
            _levelUpButton.Text = "+";
        }
        else
        {
            _nextLabel.Text = Strings.EntityBox.MaxLevel;
            _levelUpButton.IsDisabled = true;
            _levelUpButton.Text = Strings.EntityBox.MaxLevel;
        }
    }

    private static string FormatAdjusted(SpellDescriptor desc, SpellLevelingService.EffectiveSpellStats stats)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{Strings.SpellDescription.CastTime} {TimeSpan.FromMilliseconds(stats.CastTimeMs).WithSuffix()}");
        sb.AppendLine($"{Strings.SpellDescription.Cooldown} {TimeSpan.FromMilliseconds(stats.CooldownTimeMs).WithSuffix()}");
        if (desc.Combat.CastRange > 0)
        {
            sb.AppendLine($"{Strings.SpellDescription.Distance} {desc.Combat.CastRange}");
        }
        if (desc.Combat.CritChance > 0)
        {
            sb.AppendLine($"{Strings.SpellDescription.CritChance} {desc.Combat.CritChance}%");
        }
        if (desc.Combat.CritMultiplier > 0)
        {
            sb.AppendLine($"{Strings.SpellDescription.CritMultiplier} x{desc.Combat.CritMultiplier:0.##}");
        }

        return sb.ToString();
    }

    private void LevelUp()
    {
        if (_selectedSpellId == Guid.Empty || _levelUpPending)
        {
            return;
        }

        _levelUpPending = true;
        _levelUpButton.IsDisabled = true;
        PacketSender.SendRequestSpellUpgrade(_selectedSpellId);
    }
}

