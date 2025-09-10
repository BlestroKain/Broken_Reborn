using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.DragDrop;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Market
{
    public sealed class SellMarketWindow : Window
    {
        // === UI ===
        private readonly SlotItem _sellSlot;
        private readonly Label _sellSlotQuantity;
        private readonly TextBoxNumeric _priceInput;
        private readonly TextBoxNumeric _quantityInput;
        private readonly Button _confirmButton;
        private readonly Label _infoLabel;
        private readonly Label _taxLabel;
        private readonly Label _suggestedPriceLabel;
        private readonly Label _suggestedRangeLabel;
        private readonly Checkbox _autoSplitCheckbox;

        // === State ===
        private int _selectedSlot = -1;
        private Guid _selectedItemId = Guid.Empty;

        public Guid _waitingPriceForItemId = Guid.Empty;

        // Tamaño de seguridad si un slot nace sin size
        private const int DefaultSlotSize = 36;

        public SellMarketWindow(Canvas gameCanvas)
            : base(gameCanvas, Strings.Market.sellwindow, false, nameof(SellMarketWindow))
        {
            IsResizable = false;
            Alignment = [Alignments.Center];
            SetSize(600, 460);

            // Slot único para el ítem a vender
            _sellSlot = new SlotItem(this, "SellSlot", 0, null);
            _sellSlot.SetBounds(20, 20, DefaultSlotSize, DefaultSlotSize);

            _sellSlotQuantity = new Label(_sellSlot, "Quantity")
            {
                Alignment = [Alignments.Bottom, Alignments.Right],
                BackgroundTemplateName = "quantity.png",
                FontName = "sourcesansproblack",
                FontSize = 8,
                Padding = new Padding(2),
                IsVisibleInParent = false,
            };

            _infoLabel = new Label(this, "SelectedItem") { Text = Strings.Market.selectitem };
            _suggestedPriceLabel = new Label(this, "SuggestedLabel");
            _suggestedRangeLabel = new Label(this, "SuggestedRange");
            _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(0));
            _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(0, 0));

            _quantityInput = new TextBoxNumeric(this, "QuantityInput");
            _priceInput = new TextBoxNumeric(this, "PriceInput");

            _taxLabel = new Label(this, "TaxLabel") { Text = Strings.Market.taxes_0 };

            _autoSplitCheckbox = new Checkbox(this, "SplitCheckBox") { Text = Strings.Market.splitpackages };
            _autoSplitCheckbox.IsChecked = true;

            _confirmButton = new Button(this, "ConfirmButton") { Text = Strings.Market.publish };
            _confirmButton.Disable();
            _confirmButton.Clicked += OnConfirmClicked;

            _quantityInput.TextChanged += (_, _) => RefreshTax();
            _priceInput.TextChanged += (_, _) => RefreshTax();

            // Focus navegable
            _quantityInput.Focus();
            Interface.FocusComponents.Add(_quantityInput);
            _priceInput.Focus();
            Interface.FocusComponents.Add(_priceInput);

            BuildLayout();

            // ⚠️ Igual que Enchant: carga JSON ANTES de poblar
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        protected override void EnsureInitialized()
        {
            // Igual que Enchant: refuerza carga por si el JSON alteró tamaños
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void BuildLayout()
        {
            int startX = 320,
                startY = 20,
                labelH = 20,
                inputH = 30,
                padY = 8;

            _infoLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + padY;
            _suggestedPriceLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + (padY * 2);
            _suggestedRangeLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + (padY * 2);

            new Label(this, "QuantityLabel") { Text = Strings.Market.Quantity }.SetBounds(startX, startY + 10, 100, labelH);
            new Label(this, "PriceLabel") { Text = Strings.Market.Price }.SetBounds(startX + 110, startY + 10, 100, labelH);
            startY += labelH;

            _quantityInput.SetBounds(startX, startY + 10, 100, inputH);
            _priceInput.SetBounds(startX + 110, startY + 10, 100, inputH);
            startY += inputH + (padY * 2);

            _taxLabel.SetBounds(startX, startY, 290, labelH); startY += labelH + padY;
            _autoSplitCheckbox.SetBounds(startX, startY, 250, labelH); startY += labelH + padY;
            _confirmButton.SetBounds(startX, startY, 210, 40);
        }

        public override bool DragAndDrop_CanAcceptPackage(Package package)
        {
            return package.DrawControl?.Parent is InventoryItem;
        }

        public override bool DragAndDrop_HandleDrop(Package package, int x, int y)
        {
            if (package.DrawControl?.Parent is InventoryItem inventoryItem)
            {
                SelectItem(inventoryItem.SlotIndex);
                return true;
            }

            return false;
        }

        // === Publish ===
        public void SelectItem(int slotIndex)
        {
            if (Globals.Me?.Inventory == null || slotIndex < 0 || slotIndex >= Globals.Me.Inventory.Length)
                return;

            var slot = Globals.Me.Inventory[slotIndex];
            if (slot?.ItemId == Guid.Empty)
            {
                PacketSender.SendChatMsg(Strings.Market.invalidItem, (byte)ChatMessageType.Error);
                return;
            }

            _selectedSlot = slotIndex;
            _selectedItemId = slot.ItemId;
            _waitingPriceForItemId = slot.ItemId;

            if (ItemDescriptor.TryGet(slot.ItemId, out var descriptor))
            {
                _infoLabel.Text = Strings.Market.publish_colon + " " + descriptor.Name;

                var texture = GameContentManager.Current.GetTexture(Intersect.Client.Framework.Content.TextureType.Item, descriptor.Icon);
                _sellSlot.Icon.Texture = texture ?? Graphics.Renderer.WhitePixel;
                _sellSlot.Icon.RenderColor = descriptor.Color;
                _sellSlot.Icon.IsVisibleInParent = true;
            }

            if (slot.Quantity > 1)
            {
                _sellSlotQuantity.Text = Strings.FormatQuantityAbbreviated(slot.Quantity);
                _sellSlotQuantity.IsVisibleInParent = true;
            }
            else
            {
                _sellSlotQuantity.IsVisibleInParent = false;
            }

            _confirmButton.Enable();

            UpdateSuggestedPrice(slot.ItemId);

            PacketSender.SendRequestMarketInfo(slot.ItemId);

            _quantityInput.SetText(slot.Quantity.ToString(), false);
            if (MarketPriceCache.TryGet(slot.ItemId, out var avg, out _, out _))
                _priceInput.SetText(avg.ToString(), false);
            else
                _priceInput.SetText(string.Empty, false);

            RefreshTax();
        }

        private void RefreshTax()
        {
            if (!int.TryParse(_priceInput.Text, out var unitPrice) || unitPrice <= 0)
            {
                _taxLabel.Text = Strings.Market.taxes_0;
                return;
            }

            int qty = (int)_quantityInput.Value;
            if (qty <= 0)
            {
                _taxLabel.Text = Strings.Market.taxes_0;
                return;
            }

            int tax = (int)Math.Ceiling(unitPrice * qty * 0.02f);
            _taxLabel.Text = Strings.Market.taxes_estimated.ToString(tax);
        }

        public void UpdateSuggestedPrice(Guid itemId)
        {
            if (!MarketPriceCache.TryGet(itemId, out int avg, out int min, out int max))
            {
                Console.WriteLine($"⏳ Esperando precio para {itemId}...");
                return;
            }

            _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(avg));
            _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(min, max));
            _suggestedPriceLabel.Show();
            _suggestedRangeLabel.Show();
            _suggestedPriceLabel.SetTextColor(Color.Orange, ComponentState.Normal);
            _suggestedRangeLabel.SetTextColor(Color.Orange, ComponentState.Normal);

            _waitingPriceForItemId = Guid.Empty;
        }

        private void OnConfirmClicked(Base _, EventArgs __)
        {
            if (_selectedSlot < 0 || _selectedItemId == Guid.Empty)
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.noItemSelected, Color.Red, ChatMessageType.Error));
                return;
            }

            if (!int.TryParse(_quantityInput.Text, out int qty) || qty <= 0)
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.invalidQuantity, Color.Red, ChatMessageType.Error));
                return;
            }

            if (!int.TryParse(_priceInput.Text, out int price) || price <= 0)
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.invalidPrice, Color.Red, ChatMessageType.Error));
                return;
            }

            var matchingSlots = Globals.Me.Inventory
                .Select((slot, index) => new { Slot = slot, Index = index })
                .Where(x => x.Slot != null && x.Slot.ItemId == _selectedItemId)
                .OrderByDescending(x => x.Index == _selectedSlot)
                .ThenBy(x => x.Index)
                .ToList();

            int totalQuantity = matchingSlots.Sum(x => x.Slot.Quantity);

            if (totalQuantity < qty)
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.quantityExceeds, Color.Red, ChatMessageType.Error));
                return;
            }

            if (MarketPriceCache.TryGet(_selectedItemId, out var _, out var min, out var max))
            {
                if (price < min || price > max)
                {
                    ChatboxMsg.AddMessage(new ChatboxMsg(
                        string.Format(Strings.Market.priceOutOfRange.ToString(), min, max),
                        Color.Red,
                        ChatMessageType.Error
                    ));
                    return;
                }
            }

            int quantityToTake = qty;
            List<(int SlotIndex, int Amount)> slotsToUse = new();

            var baseProperties = Globals.Me.Inventory[_selectedSlot].Properties;

            foreach (var entry in matchingSlots)
            {
                if (!AreItemPropertiesEqual(entry.Slot.Properties, baseProperties))
                {
                    ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.differentProperties, Color.Red, ChatMessageType.Error));
                    return;
                }

                int takeFromThisSlot = Math.Min(entry.Slot.Quantity, quantityToTake);
                slotsToUse.Add((entry.Index, takeFromThisSlot));
                quantityToTake -= takeFromThisSlot;
                if (quantityToTake <= 0) break;
            }

            Guid itemId = _selectedItemId;
            PacketSender.SendCreateMarketListing(itemId, qty, price, baseProperties, _autoSplitCheckbox.IsChecked);

            ResetSelection();
        }

        private static bool AreItemPropertiesEqual(ItemProperties? a, ItemProperties? b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null || b == null)
            {
                return false;
            }

            if (a.EnchantmentLevel != b.EnchantmentLevel)
            {
                return false;
            }

            if (a.MageSink != b.MageSink)
            {
                return false;
            }

            if (!a.StatModifiers.SequenceEqual(b.StatModifiers))
            {
                return false;
            }

            if (!a.VitalModifiers.SequenceEqual(b.VitalModifiers))
            {
                return false;
            }

            if (a.EnchantmentRolls.Count != b.EnchantmentRolls.Count)
            {
                return false;
            }

            foreach (var kvp in a.EnchantmentRolls)
            {
                if (!b.EnchantmentRolls.TryGetValue(kvp.Key, out var arr) || !kvp.Value.SequenceEqual(arr))
                {
                    return false;
                }
            }

            return true;
        }

        private void ResetSelection()
        {
            _selectedSlot = -1;
            _selectedItemId = Guid.Empty;
            _infoLabel.Text = Strings.Market.selectitem;
            _priceInput.SetText(string.Empty, false);
            _quantityInput.SetText(string.Empty, false);
            _confirmButton.Disable();
            _suggestedPriceLabel.SetText("");
            _suggestedRangeLabel.SetText("");
            _taxLabel.SetText("");
            _sellSlot.Icon.Texture = null;
            _sellSlot.Icon.IsVisibleInParent = false;
            _sellSlotQuantity.IsVisibleInParent = false;
        }
    }

    // Cache de precios
    public static class MarketPriceCache
    {
        private static readonly Dictionary<Guid, (int Avg, int Min, int Max)> Cache = new();

        public static void Update(Guid itemId, int avg, int min, int max) => Cache[itemId] = (avg, min, max);

        public static bool TryGet(Guid itemId, out int avg, out int min, out int max)
        {
            if (Cache.TryGetValue(itemId, out var t))
            {
                (avg, min, max) = t;
                return true;
            }

            avg = min = max = 0;
            return false;
        }
    }
}
