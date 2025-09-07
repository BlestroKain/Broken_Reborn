using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Market;

public partial class MarketItem : SlotItem
{
    private Guid _listingId;
    private Guid _sellerId;
    private int _itemId;
    private ItemProperties _properties = new();
    private readonly Button _cancelButton;

    public MarketItem(Base parent, int index, ContextMenu contextMenu)
        : base(parent, nameof(MarketItem), index, contextMenu)
    {
        Icon.HoverEnter += Icon_HoverEnter;
        Icon.HoverLeave += Icon_HoverLeave;

        _cancelButton = new Button(this, nameof(_cancelButton))
        {
            Text = Strings.InputBox.Cancel,
            IsVisibleInParent = false,
        };
        _cancelButton.Clicked += CancelButton_Clicked;
    }

    public void Load(Guid listingId, Guid sellerId, int itemId, ItemProperties properties)
    {
        _listingId = listingId;
        _sellerId = sellerId;
        _itemId = itemId;
        _properties = properties ?? new ItemProperties();

        var descriptorId = ItemDescriptor.IdFromList(itemId);
        if (!ItemDescriptor.TryGet(descriptorId, out var descriptor))
        {
            return;
        }

        var tex = Globals.ContentManager?.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
        if (tex != null)
        {
            Icon.Texture = tex;
            Icon.RenderColor = descriptor.Color;
        }

        _cancelButton.IsVisibleInParent = Globals.Me?.Id == _sellerId;
    }

    private void Icon_HoverEnter(Base sender, EventArgs args)
    {
        var descriptorId = ItemDescriptor.IdFromList(_itemId);
        if (!ItemDescriptor.TryGet(descriptorId, out var descriptor))
        {
            return;
        }

        Interface.GameUi.ItemDescriptionWindow?.Show(descriptor, 1, _properties);
    }

    private void Icon_HoverLeave(Base sender, EventArgs args)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }

    private void CancelButton_Clicked(Base sender, MouseButtonState args)
    {
        PacketSender.SendCancelMarketListing(_listingId);
    }
}
