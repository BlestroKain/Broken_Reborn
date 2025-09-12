using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.Layout;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Client.Utilities;
using Intersect.Client.General;

namespace Intersect.Client.Interface.Game.Market
{
    /// <summary> Ventana para publicar objetos en el mercado. </summary>
    public sealed class SellMarketWindow
    {
        #region UI

        private readonly WindowControl _window;
        private readonly ScrollControl _inventoryScroll;
        private readonly TextBoxNumeric _priceInput;
        private readonly TextBoxNumeric _quantityInput;
        private readonly Button _confirmButton;
        private readonly Label _infoLabel;
        private readonly Label _taxLabel;
        private readonly Label _suggestedPriceLabel;
        private readonly Label _suggestedRangeLabel;
        private readonly Checkbox _autoSplitCheckbox;

        private readonly TextBox _searchBox;
        private readonly Button _sortButton;
        private readonly ComboBox _typeBox;
        private readonly ComboBox _subtypeBox;

        private readonly Dictionary<int, HashSet<string>> _subtypesByType = new();
        private readonly HashSet<string> _allSubtypes = new(StringComparer.OrdinalIgnoreCase);

        // Slots del inventario (nuestros, no InventoryItem)
        public readonly List<SlotItem> Items = new();

        #endregion

        #region State

        private int _selectedSlot = -1;
        private Guid _selectedItemId = Guid.Empty;
        private bool _selectedItemStackable;
        public Guid _waitingPriceForItemId = Guid.Empty;

        private SortCriterion _criterion = SortCriterion.TypeThenName;
        private string? _lastQuery;
        private bool _lastAsc;
        private bool _sortAscending = true;
        private ItemType? _selectedType;
        private string? _selectedSubtype;
        private bool _slotsDirty;
        #endregion

        public SellMarketWindow(Canvas canvas)
        {
            _window = new WindowControl(canvas, "📤 " + Strings.Market.sellwindow, false, "SellMarketWindow");
            _window.SetSize(600, 460);
            _window.DisableResizing();

            _searchBox = new TextBox(_window, "SearchBox");
            _sortButton = new Button(_window, "SortButton");
            _typeBox = new ComboBox(_window, "TypeFilter");
            _subtypeBox = new ComboBox(_window, "SubtypeFilter");

            // Panel inventario
            _inventoryScroll = new ScrollControl(_window, "SellInventoryScroll");
            _ = _inventoryScroll.VerticalScrollBar;
            _inventoryScroll.EnableScroll(false, true);

            // Etiquetas y campos de entrada
            _infoLabel = new Label(_window, "SelectedItem") { Text = Strings.Market.selectitem };
            _suggestedPriceLabel = new Label(_window, "SuggestedLabel");
            _suggestedRangeLabel = new Label(_window, "SuggestedRange");
            _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(0));
            _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(0, 0));

            _quantityInput = new TextBoxNumeric(_window, "QuantityInput");
            _priceInput = new TextBoxNumeric(_window, "PriceInput");

            // (Quitamos Interface.FocusElements: no existe en esta base)
            _quantityInput.TextChanged += (_, _) => RefreshTax();
            _priceInput.TextChanged += (_, _) => RefreshTax();

            _taxLabel = new Label(_window, "TaxLabel") { Text = Strings.Market.taxes_0 };

            _autoSplitCheckbox = new Checkbox(_window, "SplitCheckBox") { Text = Strings.Market.splitpackages };
            _autoSplitCheckbox.Disable();
            _autoSplitCheckbox.IsChecked = false;

            _confirmButton = new Button(_window, "CorfimButton") { Text = Strings.Market.publish };
            _confirmButton.Disable();
            _confirmButton.Clicked += OnConfirmClicked;
            _sortButton.Clicked += SortButton_Clicked;
            UpdateSortButtonText();

            _searchBox.TextChanged += (_, _) => Update();
            _subtypeBox.ItemSelected += (_, _) => { _slotsDirty = true; Update(); };

            var allType = _typeBox.AddItem(Strings.Inventory.All, userData: null);
            _typeBox.SelectedItem = allType;
            foreach (var type in Enum.GetValues<ItemType>())
            {
                if (type == ItemType.None) continue;
                _typeBox.AddItem(type.ToString(), userData: type);
            }

            BuildSubtypeLookup();
            PopulateSubtypeComboForType(null);
            _typeBox.ItemSelected += (s, a) => { TypeBox_Selected(s, a); Update(); };

            _lastQuery = _searchBox.Text;
            _selectedType = (ItemType?)_typeBox.SelectedItem?.UserData;
            _selectedSubtype = (string?)_subtypeBox.SelectedItem?.UserData;
            _lastAsc = _sortAscending;

            if (Globals.Me is { } player)
            {
                player.InventoryUpdated += PlayerOnInventoryUpdated;
            }

            BuildLayout();
            _window.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            InitItemContainer(); // crea slots visibles
            _slotsDirty = true;
            Update();            // primer pintado
        }

        #region Layout

        private void BuildLayout()
        {
            int labelH = 20,
                inputH = 30,
                padY = 8;

            _searchBox.SetBounds(20, 20, 130, 24);
            _sortButton.SetBounds(160, 20, 100, 24);
            _typeBox.SetBounds(20, 52, 120, 24);
            _subtypeBox.SetBounds(150, 52, 120, 24);
            _inventoryScroll.SetBounds(20, 84, 280, 356);

            int startX = 320,
                startY = 20;

            _infoLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + padY;
            _suggestedPriceLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + padY * 2;
            _suggestedRangeLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + padY * 2;

            new Label(_window, "QuantityLabel") { Text = Strings.Market.quantity }.SetBounds(startX, startY + 10, 100, labelH);
            new Label(_window, "Pricelabel") { Text = Strings.Market.price }.SetBounds(startX + 110, startY + 10, 100, labelH);
            startY += labelH;

            _quantityInput.SetBounds(startX, startY + 10, 100, inputH);
            _priceInput.SetBounds(startX + 110, startY + 10, 100, inputH);
            startY += inputH + padY * 2;

            _taxLabel.SetBounds(startX, startY, 290, labelH); startY += labelH + padY;
            _autoSplitCheckbox.SetBounds(startX, startY, 250, labelH); startY += labelH + padY;
            _confirmButton.SetBounds(startX, startY, 210, 40);
        }

        #endregion

        #region Inventory UI

        public void Update()
        {
            if (Globals.Me?.Inventory == null || Globals.Me.Inventory.Length == 0)
                return;

            if (Items.Count != Options.Instance.Player.MaxInventory)
            {
                InitItemContainer();
                _slotsDirty = true;
            }

            var query = _searchBox.Text;
            var asc = _sortAscending;
            var subtype = (string?)_subtypeBox.SelectedItem?.UserData;

            var changed = _slotsDirty;

            if (subtype != _selectedSubtype)
            {
                _selectedSubtype = subtype;
                changed = true;
            }

            if (query != _lastQuery)
            {
                _lastQuery = query;
                changed = true;
            }

            if (asc != _lastAsc)
            {
                _lastAsc = asc;
                changed = true;
            }

            if (changed)
            {
                ApplyFilters();
                _slotsDirty = false;
            }

            foreach (var item in Items)
            {
                item.Update();
            }
        }

        private void InitItemContainer()
        {
            Items.Clear();

            int max = Math.Max(0, Options.Instance.Player.MaxInventory);
            for (var i = 0; i < max; i++)
            {
                var slot = new SellInventoryItem(this, _inventoryScroll, i, new ContextMenu(_window));
                // Asegura tamaño para evitar divisiones por cero en grids
                if (slot.Width <= 0 || slot.Height <= 0)
                    slot.SetSize(36, 36);

                Items.Add(slot);
            }

            // Usa helper de grilla (ya con tamaños válidos)
            if (Items.Count > 0)
            {
                PopulateSlotContainer.Populate(_inventoryScroll, Items);
            }
        }

        private void BuildSubtypeLookup()
        {
            _allSubtypes.Clear();
            _subtypesByType.Clear();

            var dict = Options.Instance.Items.ItemSubtypes;
            if (dict == null) return;

            foreach (var kv in dict)
            {
                var keyInt = Convert.ToInt32(kv.Key);
                if (!_subtypesByType.TryGetValue(keyInt, out var set))
                {
                    set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    _subtypesByType[keyInt] = set;
                }

                foreach (var st in kv.Value)
                {
                    if (string.IsNullOrWhiteSpace(st)) continue;
                    set.Add(st);
                    _allSubtypes.Add(st);
                }
            }
        }

        private void PopulateSubtypeComboForType(ItemType? type)
        {
            _subtypeBox.ClearItems();
            _selectedSubtype = null;

            var all = _subtypeBox.AddItem(Strings.Inventory.All, userData: null);

            IEnumerable<string> source = Enumerable.Empty<string>();
            if (type.HasValue)
            {
                var key = Convert.ToInt32(type.Value);
                if (_subtypesByType.TryGetValue(key, out var valids))
                {
                    source = valids;
                }
            }

            foreach (var st in source)
            {
                _subtypeBox.AddItem(st, userData: st);
            }

            _subtypeBox.SelectedItem = all;
        }

        private void TypeBox_Selected(Base sender, ItemSelectedEventArgs args)
        {
            _selectedType = (ItemType?)_typeBox.SelectedItem?.UserData;
            PopulateSubtypeComboForType(_selectedType);
            _slotsDirty = true;
        }

        private void SortButton_Clicked(Base sender, MouseButtonState arguments)
        {
            if (_sortAscending)
            {
                _sortAscending = false;
            }
            else
            {
                _sortAscending = true;
                _criterion = _criterion switch
                {
                    SortCriterion.TypeThenName => SortCriterion.Name,
                    SortCriterion.Name => SortCriterion.Quantity,
                    SortCriterion.Quantity => SortCriterion.Price,
                    _ => SortCriterion.TypeThenName,
                };
            }

            UpdateSortButtonText();
            SortItems(sender, arguments);
            Update();
        }

        private void UpdateSortButtonText()
        {
            var arrow = _sortAscending ? "▲" : "▼";
            _sortButton.SetText($"{Strings.Inventory.Sort}: {_criterion} {arrow}");
        }

        private void SortItems(Base sender, MouseButtonState arguments)
        {
            if (Globals.Me?.Inventory == null)
            {
                return;
            }

            var inventory = Globals.Me.Inventory;
            var max = Options.Instance.Player.MaxInventory;

            for (var pass = 0; pass < max - 1; pass++)
            {
                for (var i = 0; i < max - pass - 1; i++)
                {
                    if (ItemListHelper.CompareItems(inventory[i], inventory[i + 1], _criterion, _sortAscending) > 0)
                    {
                        Globals.Me.SwapItems(i, i + 1);
                    }
                }
            }

            _slotsDirty = true;
        }

        private void ApplyFilters()
        {
            if (Globals.Me?.Inventory == null) return;

            var inventory = Globals.Me.Inventory;
            var searchText = _searchBox.Text ?? string.Empty;

            var matchedSet = Items.Where(i =>
            {
                var idx = i.SlotIndex;
                if (idx < 0 || idx >= Options.Instance.Player.MaxInventory) return false;
                var slot = inventory[idx];
                var descriptor = slot?.Descriptor;
                if (descriptor == null) return false;

                if (_selectedType.HasValue && descriptor.ItemType != _selectedType) return false;
                if (!string.IsNullOrEmpty(_selectedSubtype))
                {
                    if (!string.Equals(descriptor.Subtype, _selectedSubtype, StringComparison.OrdinalIgnoreCase))
                        return false;
                }

                var name = descriptor.Name ?? string.Empty;
                return SearchHelper.Matches(searchText, name);
            }).ToHashSet();

            var filterActive = !string.IsNullOrWhiteSpace(searchText) ||
                               _selectedType.HasValue ||
                               !string.IsNullOrEmpty(_selectedSubtype);

            foreach (var item in Items)
            {
                if (item is SellInventoryItem inv)
                {
                    var isMatch = matchedSet.Contains(item);
                    var show = isMatch || !filterActive;
                    inv.IsVisibleInParent = show;
                    inv.SetFilterMatch(isMatch);
                    inv.Update();
                }
            }

            var visibleItems = Items.Where(i => i.IsVisibleInParent).ToList();
            PopulateSlotContainer.Populate(_inventoryScroll, visibleItems);
        }

        private void PlayerOnInventoryUpdated(object? sender, EventArgs e)
        {
            _slotsDirty = true;
            Update();
        }

        public void SelectItem(SlotItem itemSlot, int slotIndex)
        {
            var slot = Globals.Me?.Inventory[slotIndex];
            if (slot == null || slot.ItemId == Guid.Empty)
            {
                PacketSender.SendChatMsg(Strings.Market.invalidItem, (byte)ChatMessageType.Error);
                return;
            }

            _selectedSlot = slotIndex;
            _selectedItemId = slot.ItemId;

            _confirmButton.Enable();
            if (ItemDescriptor.TryGet(slot.ItemId, out var desc))
            {
                _infoLabel.Text = Strings.Market.publish_colon + " " + desc.Name;

                _selectedItemStackable = desc.Stackable;
                if (_selectedItemStackable)
                {
                    _autoSplitCheckbox.Enable();
                    _autoSplitCheckbox.IsChecked = true;
                }
                else
                {
                    _autoSplitCheckbox.Disable();
                    _autoSplitCheckbox.IsChecked = false;
                }
            }
            else
            {
                _selectedItemStackable = false;
                _autoSplitCheckbox.Disable();
                _autoSplitCheckbox.IsChecked = false;
            }

            // Pide info de mercado al server (usa GUID)
            PacketSender.SendRequestMarketInfo(slot.ItemId);

            // Valores por defecto
            _quantityInput.SetText(slot.Quantity.ToString(), false);

            if (MarketPriceCache.TryGet(slot.ItemId, out var avg, out var min, out var max))
            {
                _priceInput.SetText(avg.ToString(), false);
                _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(avg));
                _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(min, max));
                _suggestedPriceLabel.SetTextColor(Color.Orange, ComponentState.Normal);
                _suggestedRangeLabel.SetTextColor(Color.Orange, ComponentState.Normal);
            }
            else
            {
                _priceInput.SetText(string.Empty, false);
            }

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
                Console.WriteLine($"[SellMarket] Esperando precio para {itemId}...");
                return;
            }

            _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(avg));
            _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(min, max));
            _suggestedPriceLabel.Show();
            _suggestedRangeLabel.Show();
            _suggestedPriceLabel.SetTextColor(Color.Orange, ComponentState.Normal);
            _suggestedRangeLabel.SetTextColor(Color.Orange, ComponentState.Normal);
        }

        #endregion

        #region Publish

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

            int totalQty;
            if (_selectedItemStackable)
            {
                var matching = Globals.Me.Inventory
                    .Where(s => s != null && s.ItemId == _selectedItemId)
                    .ToList();
                totalQty = matching.Sum(x => x.Quantity);
            }
            else
            {
                totalQty = Globals.Me.Inventory[_selectedSlot].Quantity;
            }

            if (totalQty < qty)
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.quantityExceeds, Color.Red, ChatMessageType.Error));
                return;
            }

            if (MarketPriceCache.TryGet(_selectedItemId, out var avg, out var min, out var max))
            {
                if (price < min || price > max)
                {
                    ChatboxMsg.AddMessage(new ChatboxMsg(
                        string.Format(Strings.Market.priceOutOfRange.ToString(), min, max),
                        Color.Red, ChatMessageType.Error));
                    return;
                }
            }

            // Propiedades del slot seleccionado
            var slot = Globals.Me.Inventory[_selectedSlot];
            var props = slot.ItemProperties ?? new ItemProperties();

            // Enviar al server
            var slotIndexToSend = _selectedItemStackable ? -1 : _selectedSlot;
            PacketSender.SendCreateMarketListing(
                _selectedItemId,
                qty,
                price,
                props,
                _autoSplitCheckbox.IsChecked,
                slotIndexToSend
            );


            ResetSelection();
        }

        private void ResetSelection()
        {
            _selectedSlot = -1;
            _selectedItemId = Guid.Empty;
            _selectedItemStackable = false;
            _infoLabel.Text = Strings.Market.selectitem;
            _priceInput.SetText(string.Empty, false);
            _quantityInput.SetText(string.Empty, false);
            _confirmButton.Disable();
            _suggestedPriceLabel.SetText("");
            _suggestedRangeLabel.SetText("");
            _taxLabel.SetText("");
            _autoSplitCheckbox.Disable();
            _autoSplitCheckbox.IsChecked = false;

            Update();
        }

        #endregion

        #region Public helpers

        public void Show() => _window.Show();
        public void Hide() => _window.Hide();
        public void Close() => _window.Close();
        public bool IsVisible() => !_window.IsHidden;
        public Guid GetSelectedItemId() => _selectedItemId;

        #endregion
    }

    /// <summary> Cache local de precios de mercado. </summary>
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
