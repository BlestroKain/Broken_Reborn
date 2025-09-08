using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Market
{
    public sealed class SellMarketWindow : Window
    {
        // === UI ===
        private readonly ScrollControl _inventoryScroll;
        private readonly TextBoxNumeric _priceInput;
        private readonly TextBoxNumeric _quantityInput;
        private readonly Button _confirmButton;
        private readonly Label _infoLabel;
        private readonly Label _taxLabel;
        private readonly Label _suggestedPriceLabel;
        private readonly Label _suggestedRangeLabel;
        private readonly Checkbox _autoSplitCheckbox;

        // Render helpers por slot
        public List<SlotItem> Items { get; private set; } = [];

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

            // Controles base (mismo estilo que Enchant)
            _inventoryScroll = new ScrollControl(this, "SellInventoryScroll"); // mismo nombre que en JSON si aplica
            _inventoryScroll.EnableScroll(false, true);
            _inventoryScroll.SetBounds(20, 20, 280, 400);

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
            InitItemContainer();
        }

        protected override void EnsureInitialized()
        {
            // Igual que Enchant: refuerza carga y repuebla por si el JSON alteró tamaños
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitItemContainer();
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

        // === Inventario (mismo enfoque que Enchant) ===
        private void InitItemContainer()
        {
            Items.Clear();

            // Si aún no existe jugador/inventario, no poblar
            if (Globals.Me?.Inventory == null || Options.Instance.Player.MaxInventory <= 0)
                return;

            for (int i = 0; i < Options.Instance.Player.MaxInventory; i++)
            {
                // Nota: En Enchant pasan null como context menu, replicamos.
                var slot = new InventoryItem(this, _inventoryScroll, i, null);

                if (slot.Width <= 0 || slot.Height <= 0)
                    slot.SetSize(DefaultSlotSize, DefaultSlotSize); // por si el JSON no asignó

                Items.Add(slot);
            }

            // Poblar UNA vez (Enchant lo hace dentro del bucle, pero no es necesario)
            if (Items.Count > 0)
            {
                if (Items[0].Width <= 0 || Items[0].Height <= 0)
                {
                    foreach (var it in Items) it.SetSize(DefaultSlotSize, DefaultSlotSize);
                }
                PopulateSlotContainer.Populate(_inventoryScroll, Items);
            }

            // No llamamos Update() aquí: lo hará el Game loop cuando la ventana esté visible.
        }

        public void Update()
        {
            if (!IsVisibleInParent) return;
            if (Globals.Me?.Inventory == null) return;

            // Igual que Enchant: limita el bucle para no pasarte del tamaño real
            var slotCount = Math.Min(Options.Instance.Player.MaxInventory, Items.Count);

            for (int i = 0; i < slotCount; i++)
            {
                var invSlot = Globals.Me.Inventory[i];

                // 🚫 Diferencia clave vs Enchant:
                // InventoryItem.Update() NO tolera slots vacíos → evitamos llamarlo si no hay item
                if (invSlot == null || invSlot.ItemId == Guid.Empty || !ItemDescriptor.TryGet(invSlot.ItemId, out _))
                {
                    Items[i].Hide();
                    continue;
                }

                Items[i].Show();

                // Si tu InventoryItem hace lazy init y aún no está listo, evita NRE
                try
                {
                    Items[i].Update();
                }
                catch (NullReferenceException)
                {
                    // lo ignoramos este frame; siguiente frame ya debe estar inicializado
                }
            }
        }

        // === Publish ===
        public void SelectItem(InventoryItem itemSlot, int slotIndex)
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
                _infoLabel.Text = Strings.Market.publish_colon + " " + descriptor.Name;

            _confirmButton.Enable();

            UpdateSuggestedPrice(slot.ItemId);

            PacketSender.SendRequestMarketInfo(ItemDescriptor.ListIndex(slot.ItemId));

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

            foreach (var entry in matchingSlots)
            {
                int takeFromThisSlot = Math.Min(entry.Slot.Quantity, quantityToTake);
                slotsToUse.Add((entry.Index, takeFromThisSlot));
                quantityToTake -= takeFromThisSlot;
                if (quantityToTake <= 0) break;
            }

            var firstSlot = slotsToUse.First();
            PacketSender.SendCreateMarketListing(firstSlot.SlotIndex, qty, price, _autoSplitCheckbox.IsChecked);

            ResetSelection();
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
