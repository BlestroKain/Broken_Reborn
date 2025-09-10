using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Market;

public partial class MarketItem : SlotItem
{
    private Guid _listingId;
    private string _sellerName = string.Empty;
    private Guid _itemId;
    private int _quantity;
    private long _price;
    private ItemProperties _properties = new();

    // Controles mapeados por JSON (nombres IMPORTANTES)
    private readonly ImagePanel _icon;
    private readonly Label _nameLabel;
    private readonly Label _quantityLabel;
    private readonly Label _priceLabel;
    private readonly Button _buyButton;
    private readonly Button _cancelButton;

    public Guid ListingId => _listingId;
    public Guid ItemId => _itemId;
    public long Price => _price;
    public ItemType ItemType { get; private set; }
    public string Subtype { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public MarketItem(Base parent, int index, ContextMenu contextMenu)
        : base(parent, nameof(MarketItem), index, contextMenu)
    {
        // Plantilla/skin del ítem (fondo)
        TextureFilename = "marketitem.png";

        // Crea hijos con los NOMBRES que usará el JSON
        _icon = new ImagePanel(this, "MarketItemIcon");
        _nameLabel = new Label(this, "MarketItemName");
        _quantityLabel = new Label(this, "MarketItemQuantity");
        _priceLabel = new Label(this, "MarketItemPrice");
        _buyButton = new Button(this, "MarketItemBuyButton") { Text = Strings.Market.Buy };
        _cancelButton = new Button(this, "MarketItemCancelButton") { Text = Strings.InputBox.Cancel };

        // Eventos de interacción
        _icon.HoverEnter += Icon_HoverEnter;
        _icon.HoverLeave += Icon_HoverLeave;
        _icon.Clicked += Icon_Clicked;
        _buyButton.Clicked += BuyButton_Clicked;
        _cancelButton.Clicked += CancelButton_Clicked;

        // Tamaños por defecto por si no hay JSON (evita NaN/0)
        SetSize(420, 40);
        _icon.SetBounds(4, 4, 32, 32);

        // Carga el layout desde el pack de UI (coloca y estiliza los controles arriba)
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    public void Load(Guid listingId, string sellerName, Guid itemId, int quantity, long price, ItemProperties properties)
    {
        Update(listingId, sellerName, itemId, quantity, price, properties);
    }


    public void Update(Guid listingId, string sellerName, Guid itemId, int quantity, long price, ItemProperties properties)
    {
        _listingId = listingId;
        _sellerName = sellerName;
        _itemId = itemId;
        _quantity = quantity;
        _price = price;
        _properties = properties ?? new ItemProperties();

        // Convertir id de lista a GUID real del descriptor
        var descriptorId = ItemDescriptor.IdFromList(_itemId.GetHashCode());
        if (!ItemDescriptor.TryGet(descriptorId, out var descriptor))
        {
            // Si no hay descriptor, oculta el item para evitar NRE
            IsVisibleInParent = false;
            return;
        }

        // Icono y color
        var tex = Globals.ContentManager?.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
        if (tex != null)
        {
            _icon.Texture = tex;
            _icon.RenderColor = descriptor.Color;
            _icon.IsVisibleInParent = true;
        }
        else
        {
            _icon.Texture = null;
            _icon.IsVisibleInParent = false;
        }

        // Datos públicos (usados por filtros en MarketWindow)
        Name = descriptor.Name;
        ItemType = descriptor.ItemType;
        Subtype = descriptor.Subtype;

        // Labels
        _nameLabel.Text = Name;
        _quantityLabel.Text = $"x{_quantity}";
        _priceLabel.Text = _price.ToString();

        // Mostrar botón según vendedor
        var isSeller = Globals.Me?.Name == _sellerName;
        _cancelButton.IsVisibleInParent = isSeller;
        _buyButton.IsVisibleInParent = !isSeller;

        // Asegura visibilidad general
        IsVisibleInParent = true;
    }

    // === Eventos ===

    private void Icon_HoverEnter(Base sender, EventArgs args)
    {
        // Convertir id de lista a GUID real del descriptor
        var descriptorId = ItemDescriptor.IdFromList(_itemId.GetHashCode());
        if (!ItemDescriptor.TryGet(descriptorId, out var descriptor)) return;

        // Garantiza ventana de descripción
        if (Interface.GameUi.ItemDescriptionWindow == null)
        {
            Interface.GameUi.ItemDescriptionWindow = new ItemDescriptionWindow();
        }

        Interface.GameUi.ItemDescriptionWindow.Show(descriptor, 1, _properties);
    }

    private void Icon_HoverLeave(Base sender, EventArgs args)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }

    private void Icon_Clicked(Base sender, MouseButtonState args)
    {
        if (Globals.Me?.Name == _sellerName) return;

        PacketSender.SendBuyMarketListing(_listingId);
    }

    private void BuyButton_Clicked(Base sender, MouseButtonState args)
        => Icon_Clicked(sender, args);

    private void CancelButton_Clicked(Base sender, MouseButtonState args)
        => PacketSender.SendCancelMarketListing(_listingId);
}
