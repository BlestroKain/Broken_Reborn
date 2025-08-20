using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Interface.Game;
using Intersect.Client.General;
using Intersect.Client.Framework.GenericClasses;
using Intersect.GameObjects;
using Intersect.Utilities;
using Intersect.Client.Framework.Input;
using static Antlr4.Runtime.Atn.SemanticContext;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellItem : SlotItem
{
    public Guid SpellId { get; }

    private readonly Label _nameLabel;
    private readonly Label _levelPips;
    private readonly Label _cooldownLabel;
    private readonly SpellsWindow _window;

    public SpellItem(SpellsWindow window, Base parent, int index, Guid spellId)
        : base(parent, $"{nameof(SpellItem)}{index}", index, null)
    {
        _window = window;
        SpellId = spellId;

        TextureFilename = "spellitem.png";

        Icon.HoverEnter += Icon_HoverEnter;
        Icon.HoverLeave += Icon_HoverLeave;
        Icon.Clicked += Item_Clicked;

        _cooldownLabel = new Label(this, $"CooldownLabel{index}")
        {
            FontName = "sourcesansproblack",
            FontSize = 8,
            IsVisibleInParent = false,
            Alignment = [Alignments.Center],
            BackgroundTemplateName = "quantity.png",
            Padding = new Padding(2),
            TextColor = new Color(0, 255, 255, 255),
        };

        _nameLabel = new Label(this, $"NameLabel{index}")
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };

        _levelPips = new Label(this, $"LevelLabel{index}")
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        _nameLabel.SetPosition(40, 0);
        _levelPips.SetPosition(40, 20);

        Refresh();
    }

    private void Item_Clicked(Base sender, MouseButtonState args)
    {
        if (args.MouseButton == MouseButton.Left)
        {
            _window.SelectSpell(SpellId);
        }
    }

    public void Refresh()
    {
        var level = Globals.Me?.Spellbook.GetLevelOrDefault(SpellId) ?? 1;
        var max = 5;
           if (SpellDescriptor.TryGet(SpellId, out var descriptor))
            max = Math.Max(1, descriptor.Progression?.Count ?? 5);
        _levelPips.Text = BuildPips(level, max);

        if (SpellDescriptor.TryGet(SpellId, out var descriptor2))
        {
            var tex = GameContentManager.Current.GetTexture(TextureType.Spell, descriptor2.Icon);
            if (tex != null)
            {
                Icon.Texture = tex;
                Icon.IsVisibleInParent = true;
            }

            _nameLabel.Text = descriptor2.Name;
        }
        if (Width <= 0) SetSize((Parent?.Width ?? 240) - 6, Height > 0 ? Height : 36);
    }

    private const char PipFilled = '\u25CF'; // ●
    private const char PipEmpty = '\u25CB'; // ○

    private static string BuildPips(int level, int max)
    {
        level = Math.Max(0, Math.Min(level, max));
        var empties = Math.Max(0, max - level);
        return new string(PipFilled, level) + new string(PipEmpty, empties);
    }


    public override void Update()
    {
        // Maintain cooldown label similar to original slot items
        if (Globals.Me == default)
        {
            return;
        }

        var slotIndex = -1;
        var spells = Globals.Me.Spells;
        if (spells != null)
        {
            for (var i = 0; i < spells.Length; i++)
            {
                if (spells[i].Id == SpellId)
                {
                    slotIndex = i;
                    break;
                }
            }
        }

        if (slotIndex < 0)
        {
            _cooldownLabel.IsVisibleInParent = false;
            Icon.RenderColor.A = 255;
            return;
        }

        if (!Globals.Me.IsSpellOnCooldown(slotIndex))
        {
            _cooldownLabel.IsVisibleInParent = false;
            Icon.RenderColor.A = 255;
            return;
        }

        _cooldownLabel.IsVisibleInParent = !Icon.IsDragging;
        var remaining = Globals.Me.GetSpellRemainingCooldown(slotIndex);
        _cooldownLabel.Text = TimeSpan.FromMilliseconds(remaining).WithSuffix("0.0");
        Icon.RenderColor.A = 100;
    }

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

        Interface.GameUi.SpellDescriptionWindow?.Show(SpellId);
    }

    private void Icon_HoverLeave(Base sender, EventArgs arguments)
    {
        Interface.GameUi.SpellDescriptionWindow?.Hide();
    }
}

