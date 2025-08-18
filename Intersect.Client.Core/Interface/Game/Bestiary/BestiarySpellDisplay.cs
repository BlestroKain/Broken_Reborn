using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiarySpellDisplay : Base
{
    private readonly ImagePanel _icon;

    private readonly SpellDescriptor _spell;

    public BestiarySpellDisplay(Base parent, SpellDescriptor spell)
        : base(parent, nameof(BestiarySpellDisplay))
    {
        _spell = spell;


        SetSize(40, 40);

        _icon = new ImagePanel(this, "SpellIcon");
        _icon.SetSize(32, 32);
        _icon.SetPosition(4, 4);


        var tex = GameContentManager.Current.GetTexture(TextureType.Spell, spell.Icon);
        if (tex != null)
        {
            _icon.Texture = tex;
        }

        HoverEnter += OnHoverEnter;
        HoverLeave += OnHoverLeave;

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
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
