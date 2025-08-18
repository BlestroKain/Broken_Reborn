using Intersect;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Bestiary;

public sealed class BestiaryItemDisplay : Base
{
    private readonly ImagePanel _icon;
    private readonly Label _label;

    public BestiaryItemDisplay(Base parent, ItemDescriptor item, int chance)
        : base(parent, nameof(BestiaryItemDisplay))
    {
        SetSize(300, 20);

        _icon = new ImagePanel(this, "ItemIcon");
        _icon.SetSize(20, 20);
        _icon.SetPosition(0, 0);

        var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, item.Icon);
        if (tex != null)
        {
            _icon.Texture = tex;
        }

        _label = new Label(this, "ItemLabel")
        {
            Text = $"- {item.Name} ({chance}% chance)",
            FontSize = 10,
            TextColorOverride = Color.White,
        };
        _label.SetPosition(24, 0);
        _label.SizeToContents();
    }
}
