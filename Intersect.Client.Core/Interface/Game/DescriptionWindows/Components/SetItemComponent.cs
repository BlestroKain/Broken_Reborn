using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Graphics;

namespace Intersect.Client.Interface.Game.DescriptionWindows.Components;

public partial class SetItemComponent : ComponentBase
{
    private readonly ImagePanel _icon;
    private readonly Label _status;

    public SetItemComponent(Base parent, string name = "SetItem") : base(parent, name)
    {
        _icon = new ImagePanel(this, "Icon");
        _status = new Label(this, "Status")
        {
            FontName = "sourcesansproblack",
            FontSize = 12,
        };
    }

    public void SetIcon(IGameTexture texture, Color color)
    {
        _icon.Texture = texture;
        _icon.RenderColor = color;
        _icon.SizeToContents();
        Align.Center(_icon);
    }

    public void SetStatus(bool owned)
    {
        _status.SetText(owned ? "✔" : "✗");
        _status.SetTextColor(owned ? Color.Green : Color.Red, ComponentState.Normal);
        _status.SizeToContents();
        Align.Center(_status);
    }

    public override void CorrectWidth()
    {
        // Do not automatically expand to parent width
    }
}
