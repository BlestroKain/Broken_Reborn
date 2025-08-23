using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control.Layout;
using Intersect.Client.Framework.Gwen.DragDrop;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Hotbar;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Configuration;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellItem : SlotItem
{
    // Controls
    private readonly Label _cooldownLabel;
    private readonly SpellsWindow _spellWindow;
    private readonly Table _layout;
    private readonly Label _nameLabel;
    private readonly Label _levelLabel;
    private readonly Button _levelUpButton;
    private readonly Button _levelDownButton;
    private readonly Button _removeButton;

    public SpellProperties SpellProperties { get; private set; } = new();
    // Context Menu Handling
    private readonly MenuItem _levelUpMenuItem;
    private readonly MenuItem _forgetSpellMenuItem;
    private bool _selected;

    public SpellItem(SpellsWindow spellWindow, Base parent, int index, ContextMenu contextMenu)
        : base(parent, nameof(SpellItem), index, contextMenu)
    {
        _spellWindow = spellWindow;
        TextureFilename = "spellitem.png";

        SetSize(500, 60);

        _layout = new Table(this, "Layout") { ColumnCount = 6, Dock = Pos.Fill };
        _layout.SetColumnWidth(0, 64); // icon
        _layout.SetColumnWidth(1, 220); // name
        _layout.SetColumnWidth(2, 80); // level
        _layout.SetColumnWidth(3, 40); // +
        _layout.SetColumnWidth(4, 40); // -
        _layout.SetColumnWidth(5, 40); // remove

        var row = _layout.AddRow();

        Icon.HoverEnter += Icon_HoverEnter;
        Icon.HoverLeave += Icon_HoverLeave;
        Icon.Clicked += Icon_Clicked;
        Icon.DoubleClicked += Icon_DoubleClicked;
        Icon.SetSize(48, 48);
        row.SetCellContents(0, Icon, enableMouseInput: true);

        _cooldownLabel = new Label(Icon, "CooldownLabel")
        {
            IsVisibleInParent = false,
            FontName = "sourcesansproblack",
            FontSize = 8,
            TextColor = new Color(0, 255, 255, 255),
            Alignment = [Alignments.Center],
            BackgroundTemplateName = "quantity.png",
            Padding = new Padding(2),
        };
        _cooldownLabel.SetPosition(0, 4); // parte superior del ícono

        _nameLabel = new Label(this, "NameLabel")
        {
            FontSize = 10,
            FontName = "sourcesansproblack",
            TextColor = Color.White,
            Alignment = [Alignments.Left, Alignments.CenterV],
        };
        row.SetCellContents(1, _nameLabel);

        _levelLabel = new Label(this, "LevelLabel")
        {
            FontSize = 10,
            FontName = "sourcesansproblack",
            Alignment = [Alignments.Center],
            TextColor = Color.White,
        };
        row.SetCellContents(2, _levelLabel);

        // Botón de subir nivel
        _levelUpButton = new Button(this, "ButtonMax")
        {
            Text = "+",
            FontSize = 10,
            FontName = "sourcesansproblack",
        };
        _levelUpButton.SetToolTipText("Subir nivel");
        _levelUpButton.Clicked += (_, _) => RequestLevelChange(+1);
        row.SetCellContents(3, _levelUpButton, enableMouseInput: true);

        // Botón de bajar nivel
        _levelDownButton = new Button(this, "ButtonMin")
        {
            Text = "-",
            FontSize = 10,
            FontName = "sourcesansproblack",
        };
        _levelDownButton.SetToolTipText("Bajar nivel");
        _levelDownButton.Clicked += (_, _) => RequestLevelChange(-1);
        row.SetCellContents(4, _levelDownButton, enableMouseInput: true);

        // Botón para olvidar hechizo
        _removeButton = new Button(this, "ButtonRemove")
        {
            Text = "X",
            FontSize = 10,
            FontName = "sourcesansproblack",
        };
        _removeButton.SetToolTipText("Quitar hechizo");
        _removeButton.Clicked += (_, _) => Globals.Me?.TryForgetSpell(SlotIndex);
        row.SetCellContents(5, _removeButton, enableMouseInput: true);

        Clicked += (_, _) => _spellWindow.SelectSlot(SlotIndex);

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        contextMenu.ClearChildren();
        _levelUpMenuItem = contextMenu.AddItem(Strings.SpellContextMenu.LevelUp.ToString());
        _levelUpMenuItem.Clicked += (_, _) => RequestLevelChange(+1);
        _forgetSpellMenuItem = contextMenu.AddItem(Strings.SpellContextMenu.Forget.ToString());
        _forgetSpellMenuItem.Clicked += _forgetSpellMenuItem_Clicked;
        contextMenu.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    private void RequestLevelChange(int delta)
    {
        if (Globals.Me?.Spells is not { Length: > 0 } spellSlots)
        {
            return;
        }

        var slot = spellSlots[SlotIndex];
        var level = slot.Properties?.Level ?? 1;

        if (delta > 0)
        {
            if (Globals.Me.SpellPoints <= 0 || level >= Options.Instance.Player.MaxSpellLevel)
            {
                return;
            }
        }
        else if (delta < 0)
        {
            if (level <= 1)
            {
                return;
            }
        }

        PacketSender.SendSpellPropertiesChange(SlotIndex, delta);
    }


    #region Context Menu

    protected override void OnContextMenuOpening(ContextMenu contextMenu)
    {
        // Clear out the old options.
        contextMenu.ClearChildren();

        if (Globals.Me?.Spells is not { Length: > 0 } spellSlots)
        {
            return;
        }

        // No point showing a menu for blank space.
        if (!SpellDescriptor.TryGet(spellSlots[SlotIndex].Id, out var spell))
        {
            return;
        }

        if (Globals.Me.SpellPoints > 0 &&
            (spellSlots[SlotIndex].Properties?.Level ?? 1) < Options.Instance.Player.MaxSpellLevel)
        {
            contextMenu.AddChild(_levelUpMenuItem);
            _levelUpMenuItem.SetText(Strings.SpellContextMenu.LevelUp.ToString(spell.Name));
        }

        if (!spell.Bound)
        {
            contextMenu.AddChild(_forgetSpellMenuItem);
            _forgetSpellMenuItem.SetText(Strings.SpellContextMenu.Forget.ToString(spell.Name));
        }

        base.OnContextMenuOpening(contextMenu);
    }

    private void _forgetSpellMenuItem_Clicked(Base sender, MouseButtonState arguments)
    {
        Globals.Me?.TryForgetSpell(SlotIndex);
    }

    #endregion

    #region Mouse Events

    private void Icon_HoverEnter(Base? sender, EventArgs? arguments)
    {
        if (InputHandler.MouseFocus != null)
        {
            return;
        }

        if (Globals.InputManager.IsMouseButtonDown(MouseButton.Left))
        {
            return;
        }

        if (Globals.Me?.Spells is not { Length: > 0 } spellSlots)
        {
            return;
        }

        Interface.GameUi.SpellDescriptionWindow?.Show(spellSlots[SlotIndex].Id);
    }

    private void Icon_HoverLeave(Base sender, EventArgs arguments)
    {
        Interface.GameUi.SpellDescriptionWindow?.Hide();
    }

    private void Icon_Clicked(Base sender, MouseButtonState arguments)
    {
        if (arguments.MouseButton is MouseButton.Right)
        {
            if (ClientConfiguration.Instance.EnableContextMenus)
            {
                OpenContextMenu();
            }
            else
            {
                Globals.Me?.TryForgetSpell(SlotIndex);
            }
        }
        else if (arguments.MouseButton is MouseButton.Left)
        {
            _spellWindow.SelectSlot(SlotIndex);
        }
    }

    private void Icon_DoubleClicked(Base sender, MouseButtonState arguments)
    {
        Globals.Me?.TryUseSpell(SlotIndex);
    }

    #endregion

    #region Drag and Drop

    public override bool DragAndDrop_HandleDrop(Package package, int x, int y)
    {
        if (Globals.Me is not { } player)
        {
            return false;
        }

        var targetNode = Interface.FindComponentUnderCursor();

        // Find the first parent acceptable in that tree that can accept the package
        while (targetNode != default)
        {
            switch (targetNode)
            {
                case SpellItem spellItem:
                    player.SwapSpells(SlotIndex, spellItem.SlotIndex);
                    return true;

                case HotbarItem hotbarItem:
                    player.AddToHotbar(hotbarItem.SlotIndex, 1, SlotIndex);
                    return true;

                default:
                    targetNode = targetNode.Parent;
                    break;
            }
        }

        // If we've reached the top of the tree, we can't drop here, so cancel drop
        return false;
    }

    #endregion

    public override void Update()
    {
        if (Globals.Me == default)
        {
            return;
        }

        if (Globals.Me?.Spells is not { Length: > 0 } spellSlots)
        {
            return;
        }

        var slot = spellSlots[SlotIndex];
        if (!SpellDescriptor.TryGet(slot.Id, out var spell))
        {
            Icon.Hide();
            Icon.Texture = null;
            _cooldownLabel.Hide();
            _nameLabel.Text = Strings.Spells.EmptySlot.ToString();
            _levelLabel.Text = string.Empty;
            SetToolTipText(Strings.Spells.EmptySlot.ToString());
            _levelUpButton.IsVisibleInParent = false;
            _levelDownButton.IsVisibleInParent = false;
            _removeButton.IsVisibleInParent = false;
            return;
        }

        _cooldownLabel.IsVisibleInParent = !Icon.IsDragging && Globals.Me.IsSpellOnCooldown(SlotIndex);
        if (_cooldownLabel.IsVisibleInParent)
        {
            var itemCooldownRemaining = Globals.Me.GetSpellRemainingCooldown(SlotIndex);
            _cooldownLabel.Text = TimeSpan.FromMilliseconds(itemCooldownRemaining).WithSuffix("0.0");
            Icon.RenderColor.A = 100;
        }
        else
        {
            Icon.RenderColor.A = 255;
        }

        var properties = slot.Properties ?? new SpellProperties();
        slot.Properties = properties;
        _nameLabel.Text = spell.Name;
        _levelLabel.Text = $"Nivel {properties.Level}";
        SpellProperties = properties;
        SetToolTipText(spell.Name);

        var canLevelUp = Globals.Me.SpellPoints > 0 && properties.Level < Options.Instance.Player.MaxSpellLevel;
        var canLevelDown = properties.Level > 1;
        _levelUpButton.IsVisibleInParent = canLevelUp;
        _levelDownButton.IsVisibleInParent = canLevelDown;
        _removeButton.IsVisibleInParent = !spell.Bound;

        if (Path.GetFileName(Icon.Texture?.Name) != spell.Icon)
        {
            var spellIconTexture = GameContentManager.Current.GetTexture(TextureType.Spell, spell.Icon);
            if (spellIconTexture != null)
            {
                Icon.Texture = spellIconTexture;
                Icon.RenderColor.A = (byte)(_cooldownLabel.IsVisibleInParent ? 100 : 255);
                Icon.IsVisibleInParent = true;
            }
            else
            {
                if (Icon.Texture != null)
                {
                    Icon.Texture = null;
                    Icon.IsVisibleInParent = false;
                }
            }
        }
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;
        RenderColor = selected ? new Color(70, 70, 120, 255) : Color.White;
    }
}
