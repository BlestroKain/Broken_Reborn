using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control.EventArguments.InputSubmissionEvent;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Market;

public partial class MarketItem : SlotItem
{
    private Guid _listingId;
    private Guid _sellerId;
    private Guid _itemId;
    private int _quantity;
    private long _price;
    private ItemProperties _properties = new();
    private readonly Button _cancelButton;
    private readonly Label _nameLabel;
    private readonly Label _quantityLabel;
    private readonly Label _priceLabel;
    private readonly Button _buyButton;

    public Guid ListingId => _listingId;
    public Guid ItemId => _itemId;
    public long Price => _price;
    public ItemType ItemType { get; private set; }
    public string Subtype { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public MarketItem(Base parent, int index, ContextMenu contextMenu)
        : base(parent, nameof(MarketItem), index, contextMenu)
    {
        Icon.HoverEnter += Icon_HoverEnter;
        Icon.HoverLeave += Icon_HoverLeave;
        Icon.Clicked += Icon_Clicked;

        _cancelButton = new Button(this, nameof(_cancelButton))
        {
            Text = Strings.InputBox.Cancel,
            IsVisibleInParent = false,
        };
        _cancelButton.SetBounds(360, 8, 60, 24);
        _cancelButton.Clicked += CancelButton_Clicked;

        _nameLabel = new Label(this, nameof(_nameLabel));
        _nameLabel.SetBounds(44, 4, 150, 32);

        _quantityLabel = new Label(this, nameof(_quantityLabel));
        _quantityLabel.SetBounds(200, 4, 60, 32);

        _priceLabel = new Label(this, nameof(_priceLabel));
        _priceLabel.SetBounds(260, 4, 80, 32);

        _buyButton = new Button(this, nameof(_buyButton))
        {
            Text = Strings.Market.Buy,
            IsVisibleInParent = false,
        };
        _buyButton.SetBounds(360, 8, 60, 24);
        _buyButton.Clicked += BuyButton_Clicked;

        SetSize(420, 40);
        Icon.SetBounds(4, 4, 32, 32);
    }

    public void Load(Guid listingId, Guid sellerId, Guid itemId, int quantity, long price, ItemProperties properties)
    {
        Update(listingId, sellerId, itemId, quantity, price, properties);
    }

    public void Update(Guid listingId, Guid sellerId, Guid itemId, int quantity, long price, ItemProperties properties)
    {
        _listingId = listingId;
        _sellerId = sellerId;
        _itemId = itemId;
        _quantity = quantity;
        _price = price;
        _properties = properties ?? new ItemProperties();

        if (!ItemDescriptor.TryGet(itemId, out var descriptor))
        {
            return;
        }

        var tex = Globals.ContentManager?.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
        if (tex != null)
        {
            Icon.Texture = tex;
            Icon.RenderColor = descriptor.Color;
        }

        Name = descriptor.Name;
        ItemType = descriptor.ItemType;
        Subtype = descriptor.Subtype;

        _nameLabel.Text = Name;
        _quantityLabel.Text = $"x{_quantity}";
        _priceLabel.Text = _price.ToString();

        var isSeller = Globals.Me?.Id == _sellerId;
        _cancelButton.IsVisibleInParent = isSeller;
        _buyButton.IsVisibleInParent = !isSeller;
    }

    private void Icon_HoverEnter(Base sender, EventArgs args)
    {
        if (!ItemDescriptor.TryGet(_itemId, out var descriptor))
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

    private void Icon_Clicked(Base sender, MouseButtonState args)
    {
        if (Globals.Me?.Id == _sellerId)
        {
            return;
        }

        new InputBox(
            title: "Comprar",
            prompt: $"Cantidad (max {_quantity}) - Precio {_price} c/u",
            inputType: InputType.NumericInput,
            onSubmit: (s, e) =>
            {
                if (e.Value is NumericalSubmissionValue value)
                {
                    var qty = (int)value.Value;
                    if (qty <= 0) qty = 1;
                    if (qty > _quantity) qty = _quantity;
                    PacketSender.SendBuyMarketListing(_listingId, qty);
                }
            },
            onCancel: null,
            userData: null,
            quantity: _quantity,
            maximumQuantity: _quantity,
            minimumQuantity: 1
        );
    }

    private void BuyButton_Clicked(Base sender, MouseButtonState args)
    {
        Icon_Clicked(sender, args);
    }
}
