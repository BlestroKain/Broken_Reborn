using System;
using Intersect;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Interface.Game.Market;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Interface.Game.Market
{
    public class MarketItem
    {
        // Contenedor externo: debes asignarlo desde MarketWindow antes de llamar Setup()
        public ImagePanel Container;

        private ImagePanel _iconPanel;
        private Label _nameLabel;
        private Label _quantityLabel;
        private Label _priceLabel;
        private Button _buyButton;
        private Button _cancelButton;

        private readonly MarketWindow _marketWindow;
        private MarketListingPacket _listing;
        private ItemDescriptor _itemDescriptor;

        public ItemDescriptor? Descriptor => _itemDescriptor;
        public MarketListingPacket Listing => _listing;

        public MarketItem(MarketWindow marketWindow, MarketListingPacket listing)
        {
            _marketWindow = marketWindow;
            _listing = listing;
        }

        public void Setup()
        {
            if (Container == null) return;

            // Descriptor a partir de GUID directo
            if (!ItemDescriptor.TryGet(_listing.ItemId, out _itemDescriptor))
            {
                _itemDescriptor = null;
            }

            // Icono
            _iconPanel = new ImagePanel(Container, "MarketItemIcon");
            _iconPanel.SetBounds(5, 4, 32, 32);

            if (_itemDescriptor != null)
            {
                var tex = Globals.ContentManager?.GetTexture(
                    Framework.Content.TextureType.Item,
                    _itemDescriptor.Icon
                );
                if (tex != null)
                {
                    _iconPanel.Texture = tex;
                    _iconPanel.RenderColor = _itemDescriptor.Color;
                }
            }

            _iconPanel.HoverEnter += OnHoverEnter;   // (Base, EventArgs)
            _iconPanel.HoverLeave += OnHoverLeave;   // (Base, EventArgs)
            _iconPanel.Clicked += OnClick;        // (Base, MouseButtonState)

            // Nombre
            _nameLabel = new Label(Container, "MarketItemName")
            {
                Text = _itemDescriptor?.Name ?? "???"
            };
            _nameLabel.SetBounds(42, 5, 200, 30);

            // Cantidad
            _quantityLabel = new Label(Container, "MarketItemQuantity")
            {
                Text = $"x{_listing.Quantity}"
            };
            _quantityLabel.SetBounds(250, 5, 50, 30);

            // Precio
            _priceLabel = new Label(Container, "MarketItemPrice")
            {
                Text = $"{_listing.Price} 🪙"
            };
            _priceLabel.SetBounds(310, 5, 120, 30);

            // Botón comprar/cancelar según vendedor
            var isSeller = string.Equals(Globals.Me?.Name, _listing.SellerName, StringComparison.OrdinalIgnoreCase);

            if (!isSeller)
            {
                _buyButton = new Button(Container, "BuyMarketItemButton");
                _buyButton.SetText("🛒 " + Intersect.Client.Localization.Strings.Market.Buy);
                _buyButton.SetBounds(500, 5, 100, 30);
                _buyButton.Clicked += OnBuyClick;    // (Base, MouseButtonState)
            }
            else
            {
                _cancelButton = new Button(Container, "CancelMarketItemButton");
                _cancelButton.SetText("❌ " + Intersect.Client.Localization.Strings.InputBox.Cancel);
                _cancelButton.SetBounds(500, 5, 100, 30);
                _cancelButton.Clicked += OnCancelClick; // (Base, MouseButtonState)
            }
        }

        public void Update(MarketListingPacket newListing)
        {
            _listing = newListing;

            if (!ItemDescriptor.TryGet(_listing.ItemId, out _itemDescriptor))
            {
                _itemDescriptor = null;
            }

            if (_itemDescriptor == null)
            {
                if (_nameLabel != null) _nameLabel.Text = "???";
                if (_iconPanel != null) _iconPanel.Texture = null;
                if (_quantityLabel != null) _quantityLabel.Text = $"x{_listing.Quantity}";
                if (_priceLabel != null) _priceLabel.Text = $"{_listing.Price}";
                return;
            }

            _nameLabel.Text = _itemDescriptor.Name;
            _quantityLabel.Text = $"x{_listing.Quantity}";
            _priceLabel.Text = $"{_listing.Price}";

            var tex = Globals.ContentManager?.GetTexture(
                Framework.Content.TextureType.Item,
                _itemDescriptor.Icon
            );
            if (tex != null)
            {
                _iconPanel.Texture = tex;
                _iconPanel.RenderColor = _itemDescriptor.Color;
            }
        }

        // ===== Eventos =====

        private void OnCancelClick(Base sender, MouseButtonState args)
        {
            PacketSender.SendCancelMarketListing(_listing.ListingId);
        }

        private void OnBuyClick(Base sender, MouseButtonState args)
        {
            // Compra directa (si quieres prompt de cantidad, aquí pones un InputBox)
            PacketSender.SendBuyMarketListing(_listing.ListingId);
        }

        private void OnClick(Base sender, MouseButtonState args)
        {
            // Click sobre el icono: mismo comportamiento que el botón
            if (!string.Equals(Globals.Me?.Name, _listing.SellerName, StringComparison.OrdinalIgnoreCase))
            {
                PacketSender.SendBuyMarketListing(_listing.ListingId);
            }
        }

        private void OnHoverEnter(Base sender, EventArgs args)
        {
            if (_itemDescriptor == null) return;

            // Garantiza ventana y muestra descripción con qty y props del listing
            Interface.GameUi.ItemDescriptionWindow ??= new ItemDescriptionWindow();
            Interface.GameUi.ItemDescriptionWindow.Show(_itemDescriptor, _listing.Quantity, _listing.Properties);
        }

        private void OnHoverLeave(Base sender, EventArgs args)
        {
            Interface.GameUi.ItemDescriptionWindow?.Hide();
        }
    }
}
