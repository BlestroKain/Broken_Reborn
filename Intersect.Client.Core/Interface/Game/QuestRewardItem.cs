using System;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game;

public partial class QuestRewardItem : Base
{
    private readonly ImagePanel _icon;
    private readonly Label _quantity;

    public QuestRewardItem(IQuestWindow window, Guid itemId, int quantity) : base(null)
    {
        window.AddRewardWidget(this);

        MinimumSize = new Point(34, 34);
        Margin = new Margin(4);

        _icon = new ImagePanel(this)
        {
            MinimumSize = new Point(32, 32),
            Alignment = [Alignments.Center],
        };

        _quantity = new Label(this)
        {
            Alignment = [Alignments.Bottom, Alignments.Right],
            BackgroundTemplateName = "quantity.png",
            FontName = "sourcesansproblack",
            FontSize = 8,
            Padding = new Padding(2),
        };

        if (ItemDescriptor.TryGet(itemId, out var descriptor))
        {
            var texture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
            if (texture != null)
            {
                _icon.Texture = texture;
                _icon.RenderColor = descriptor.Color;
            }
        }

        _quantity.Text = quantity.ToString();
    }
}

