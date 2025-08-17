using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiarySpellDisplay : SlotItem
{
    private readonly ImagePanel _icon;

    private readonly SpellDescriptor _spell;

    public BestiarySpellDisplay(Base parent, SpellDescriptor spell, ContextMenu? contextMenu = null)
        : base(parent, nameof(BestiarySpellDisplay), -1, contextMenu)
    {
        _spell = spell;


        SetSize(40, 40);
        TextureFilename = "inventoryitem.png";
        _icon = new ImagePanel(this, "SpellIcon");
        _icon.SetSize(32, 32);
        _icon.SetPosition(4, 4);


        var tex = GameContentManager.Current.GetTexture(TextureType.Spell, spell.Icon);
        if (tex != null)
        {
            _icon.Texture = tex;
        }

        _icon.HoverEnter += OnHoverEnter;
        _icon.HoverLeave += OnHoverLeave;

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        Icon.Texture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Spell, spell.Icon);
  
        Icon.IsVisibleInParent = true;
    }

    private void OnHoverEnter(Base sender, EventArgs args)
    {
        Interface.GameUi.SpellDescriptionWindow ??= new SpellDescriptionWindow();
        Interface.GameUi.SpellDescriptionWindow.Show(_spell.Id);
    }

    private void OnHoverLeave(Base sender, EventArgs args)
    {
        Interface.GameUi.SpellDescriptionWindow?.Hide();
    }
}
