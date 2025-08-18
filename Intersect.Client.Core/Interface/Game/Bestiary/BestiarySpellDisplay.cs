using Intersect;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Bestiary;

public sealed class BestiarySpellDisplay : Base
{
    private readonly ImagePanel _icon;
    private readonly Label _label;

    public BestiarySpellDisplay(Base parent, SpellDescriptor spell)
        : base(parent, nameof(BestiarySpellDisplay))
    {
        SetSize(300, 20);

        _icon = new ImagePanel(this, "SpellIcon");
        _icon.SetSize(20, 20);
        _icon.SetPosition(0, 0);

        var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Spell, spell.Icon);
        if (tex != null)
        {
            _icon.Texture = tex;
        }

        _label = new Label(this, "SpellLabel")
        {
            Text = $"- {spell.Name}",
            FontSize = 10,
            TextColorOverride = Color.White,
        };
        _label.SetPosition(24, 0);
        _label.SizeToContents();
    }
}
