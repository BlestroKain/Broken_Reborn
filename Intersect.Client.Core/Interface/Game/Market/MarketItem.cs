using System;
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
                _iconPanel.Texture = tex;
                if (tex != null)
                {
                    _iconPanel.RenderColor = _itemDescriptor.Color;
                }

                _iconPanel.IsVisibleInParent = tex != null;
            }
            else
            {
                _iconPanel.Texture = null;
                _iconPanel.IsVisibleInParent = false;
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
            UpdateActionButton();
        }

        public void Update(MarketListingPacket newListing)
        {
            _listing = newListing;
            SetBuying(false);

            UpdateActionButton();

            if (!ItemDescriptor.TryGet(_listing.ItemId, out _itemDescriptor))
            {
                _itemDescriptor = null;
            }

            if (_itemDescriptor == null)
            {
                if (_nameLabel != null)
                {
                    _nameLabel.Text = "???";
                }

                if (_iconPanel != null)
                {
                    _iconPanel.Texture = null;
                    _iconPanel.IsVisibleInParent = false;
                }

                if (_quantityLabel != null)
                {
                    _quantityLabel.Text = $"x{_listing.Quantity}";
                }

                if (_priceLabel != null)
                {
                    _priceLabel.Text = $"{_listing.Price}";
                }

                return;
            }

            _nameLabel.Text = _itemDescriptor.Name;
            _quantityLabel.Text = $"x{_listing.Quantity}";
            _priceLabel.Text = $"{_listing.Price}";

            var tex = Globals.ContentManager?.GetTexture(
                Framework.Content.TextureType.Item,
                _itemDescriptor.Icon
            );
            _iconPanel.Texture = tex;
            if (tex != null)
            {
                _iconPanel.RenderColor = _itemDescriptor.Color;
            }
            _iconPanel.IsVisibleInParent = tex != null;
        }

        private void UpdateActionButton()
        {
            if (Container == null)
            {
                return;
            }

            var isSeller = string.Equals(Globals.Me?.Name, _listing.SellerName, StringComparison.OrdinalIgnoreCase);

            if (isSeller)
            {
                if (_buyButton != null)
                {
                    _buyButton.DelayedDelete();
                    _buyButton = null;
                }

                if (_cancelButton == null)
                {
                    _cancelButton = new Button(Container, "CancelMarketItemButton");
                    _cancelButton.SetText("❌ " + Intersect.Client.Localization.Strings.InputBox.Cancel);
                    _cancelButton.SetBounds(500, 5, 100, 30);
                    _cancelButton.Clicked += OnCancelClick;
                    _cancelButton.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                }

                _cancelButton.Show();
            }
            else
            {
                if (_cancelButton != null)
                {
                    _cancelButton.DelayedDelete();
                    _cancelButton = null;
                }

                if (_buyButton == null)
                {
                    _buyButton = new Button(Container, "BuyMarketItemButton");
                    _buyButton.SetText("🛒 " + Intersect.Client.Localization.Strings.Market.Buy);
                    _buyButton.SetBounds(500, 5, 100, 30);
                    _buyButton.Clicked += OnBuyClick;
                    _buyButton.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                }

                _buyButton.Show();
            }
        }

        // ===== Eventos =====

        private void OnCancelClick(Base sender, MouseButtonState args)
        {
            PacketSender.SendCancelMarketListing(_listing.ListingId);
        }

        private void OnBuyClick(Base sender, MouseButtonState args)
        {
            if (string.Equals(Globals.Me?.Name, _listing.SellerName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (_buyButton != null && _buyButton.IsDisabled)
            {
                return;
            }

            SetBuying(true);
            PacketSender.SendBuyMarketListing(_listing.ListingId);
        }

        private void OnClick(Base sender, MouseButtonState args)
        {
            // Click sobre el icono: mismo comportamiento que el botón
            OnBuyClick(sender, args);
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

        public void SetBuying(bool on)
        {
            if (_buyButton != null)
            {
                _buyButton.IsDisabled = on;
            }
        }

        /// <summary>
        /// Detaches all event handlers to avoid ObjectDisposedException when disposing.
        /// </summary>
        public void DetachEvents()
        {
            try
            {
                if (_iconPanel != null)
                {
                    _iconPanel.HoverEnter -= OnHoverEnter;
                    _iconPanel.HoverLeave -= OnHoverLeave;
                    _iconPanel.Clicked -= OnClick;
                }
            }
            catch
            {
            }

            try
            {
                if (_buyButton != null)
                {
                    _buyButton.Clicked -= OnBuyClick;
                }
            }
            catch
            {
            }

            try
            {
                if (_cancelButton != null)
                {
                    _cancelButton.Clicked -= OnCancelClick;
                }
            }
            catch
            {
            }
        }
    }
}
