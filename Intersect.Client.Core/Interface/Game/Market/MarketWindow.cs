using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Interface.Game.Market;

public partial class MarketWindow : Window
{
    private readonly List<MarketItem> _items = [];
    private List<MarketListingPacket> _allListings = [];
    private readonly ScrollControl _listingScroll;
    private readonly TextBox _searchBox;
    private readonly TextBoxNumeric _minPriceBox;
    private readonly TextBoxNumeric _maxPriceBox;
    private readonly ComboBox _typeBox;
    private readonly ComboBox _subtypeBox;
    private readonly Button _sellButton;
    private readonly Button _prevButton;
    private readonly Button _nextButton;
    private readonly Label _pageLabel;

    private int _page = 1;
    private int _pageSize = 10;
    private int _total;

    private ItemType? _selectedType;
    private string? _selectedSubtype;

    public static MarketWindow Instance;

    public MarketWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Market.Title, false, nameof(MarketWindow))
    {
      IsResizable=false;
        Alignment = [Alignments.Center];
        SetSize(800, 620);

        var topPanel = new ImagePanel(this, "TopControlsPanel");
        topPanel.SetBounds(10, 10, 780, 40);

        _searchBox = new TextBox(topPanel, nameof(_searchBox));
        _searchBox.SetBounds(0, 8, 140, 24);
        _searchBox.TextChanged += (_, _) => ApplyFilters();

        _typeBox = new ComboBox(topPanel, nameof(_typeBox));
        _typeBox.SetBounds(150, 8, 120, 24);
        var allType = _typeBox.AddItem(Strings.Inventory.All, userData: null);
        _typeBox.SelectedItem = allType;
        foreach (var type in Enum.GetValues<ItemType>())
        {
            if (type == ItemType.Currency || type == ItemType.None)
            {
                continue;
            }
            _typeBox.AddItem(type.ToString(), userData: type);
        }
        _typeBox.ItemSelected += TypeBox_Selected;

        _subtypeBox = new ComboBox(topPanel, nameof(_subtypeBox));
        _subtypeBox.SetBounds(280, 8, 120, 24);
        var allSub = _subtypeBox.AddItem(Strings.Inventory.All, userData: null);
        _subtypeBox.SelectedItem = allSub;
        _subtypeBox.ItemSelected += (_, _) =>
        {
            _selectedSubtype = _subtypeBox.SelectedItem?.UserData as string;
            ApplyFilters();
        };

        _minPriceBox = new TextBoxNumeric(topPanel, nameof(_minPriceBox));
        _minPriceBox.SetBounds(410, 8, 80, 24);
        _minPriceBox.TextChanged += (_, _) => ApplyFilters();

        _maxPriceBox = new TextBoxNumeric(topPanel, nameof(_maxPriceBox));
        _maxPriceBox.SetBounds(500, 8, 80, 24);
        _maxPriceBox.TextChanged += (_, _) => ApplyFilters();

        _sellButton = new Button(topPanel, nameof(_sellButton))
        {
            Text = Strings.Market.Sell,
        };
        _sellButton.SetBounds(590, 8, 80, 24);
        _sellButton.Clicked += SellButton_Clicked;

        _listingScroll = new ScrollControl(this, nameof(_listingScroll));
        _ = _listingScroll.VerticalScrollBar;
        _listingScroll.EnableScroll(false, true);
        _listingScroll.SetBounds(10, 60, Width - 20, Height - 110);

        var bottomPanel = new ImagePanel(this, "BottomControlsPanel");
        bottomPanel.SetBounds(10, Height - 40, Width - 20, 30);

        _prevButton = new Button(bottomPanel, nameof(_prevButton)) { Text = "<" };
        _prevButton.SetBounds(0, 3, 30, 24);
        _prevButton.Clicked += (_, _) =>
        {
            if (_page > 1)
            {
                RequestPage(_page - 1);
            }
        };

        _pageLabel = new Label(bottomPanel, nameof(_pageLabel));
        _pageLabel.SetBounds(40, 3, 100, 24);

        _nextButton = new Button(bottomPanel, nameof(_nextButton)) { Text = ">" };
        _nextButton.SetBounds(150, 3, 30, 24);
        _nextButton.Clicked += (_, _) =>
        {
            var maxPage = (int)Math.Ceiling(_total / (double)_pageSize);
            if (_page < maxPage)
            {
                RequestPage(_page + 1);
            }
        };

        RequestPage(1);
    }

    private void TypeBox_Selected(Base sender, ItemSelectedEventArgs args)
    {
        if (_typeBox.SelectedItem?.UserData is ItemType t)
        {
            _selectedType = t;
        }
        else
        {
            _selectedType = null;
        }

        UpdateSubtypeCombo();
        ApplyFilters();
    }

    private void UpdateSubtypeCombo()
    {
        _subtypeBox.DeleteAllChildren();
        var all = _subtypeBox.AddItem(Strings.Inventory.All, userData: null);
        _subtypeBox.SelectedItem = all;
        _selectedSubtype = null;

        if (_selectedType.HasValue &&
            Intersect.Options.Instance?.Items?.ItemSubtypes != null &&
            Intersect.Options.Instance.Items.ItemSubtypes.TryGetValue(_selectedType.Value, out var subtypes))
        {
            foreach (var st in subtypes.Distinct().OrderBy(s => s))
            {
                _subtypeBox.AddItem(st, userData: st);
            }
        }
    }

    public void LoadListings(List<MarketListingPacket> listings, int page, int pageSize, int total)
    {
        var scroll = _listingScroll.VerticalScrollBar.ScrollAmount;
        var focus = InputHandler.KeyboardFocus;

        _page = page;
        _pageSize = pageSize;
        _total = total;

        var existing = _items.ToDictionary(i => i.ListingId);
        var orderedItems = new List<MarketItem>();

        foreach (var listing in listings)
        {
            MarketItem item;
            if (existing.TryGetValue(listing.ListingId, out item))
            {
                existing.Remove(listing.ListingId);
                item.Update(listing.ListingId, listing.SellerName, listing.ItemId, listing.Quantity, listing.Price, listing.Properties);
            }
            else
            {
                item = new MarketItem(_listingScroll, orderedItems.Count, new ContextMenu(this));
                item.Update(listing.ListingId, listing.SellerName, listing.ItemId, listing.Quantity, listing.Price, listing.Properties);
            }

            item.SetBounds(0, orderedItems.Count * 44, _listingScroll.Width - 16, 40);
            item.Update();
            orderedItems.Add(item);
        }

        foreach (var leftover in existing.Values)
        {
            _listingScroll.RemoveChild(leftover, true);
        }

        _items.Clear();
        _items.AddRange(orderedItems);

        _listingScroll.SetInnerSize(_listingScroll.Width - 16, _items.Count * 44);
        UpdatePagination();

        _listingScroll.VerticalScrollBar.ScrollAmount = scroll;
        focus?.Focus();
    }

    private void UpdatePagination()
    {
        var maxPage = Math.Max(1, (int)Math.Ceiling(_total / (double)_pageSize));
        _pageLabel.Text = $"{_page}/{maxPage}";
        _prevButton.IsDisabled = _page <= 1;
        _nextButton.IsDisabled = _page >= maxPage;
    }

    private void RequestPage(int page)
    {
        _page = page;
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var searchText = _searchBox.Text ?? string.Empty;
        int? minPrice = int.TryParse(_minPriceBox.Text, out var mn) ? mn : null;
        int? maxPrice = int.TryParse(_maxPriceBox.Text, out var mx) ? mx : null;

        var filtered = new List<MarketListingPacket>();

        foreach (var listing in _allListings)
        {
            var descriptorId = ItemDescriptor.IdFromList(listing.ItemId.GetHashCode());
            if (!ItemDescriptor.TryGet(descriptorId, out var descriptor))
            {
                continue;
            }

            if (_selectedType.HasValue && descriptor.ItemType != _selectedType)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(_selectedSubtype) &&
                !descriptor.Subtype.Equals(_selectedSubtype, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (minPrice.HasValue && listing.Price < minPrice.Value)
            {
                continue;
            }

            if (maxPrice.HasValue && listing.Price > maxPrice.Value)
            {
                continue;
            }

            if (!SearchHelper.Matches(searchText, descriptor.Name))
            {
                continue;
            }

            filtered.Add(listing);
        }

        _total = filtered.Count;
        var maxPage = Math.Max(1, (int)Math.Ceiling(_total / (double)_pageSize));
        if (_page > maxPage)
        {
            _page = maxPage;
        }

        var pageListings = filtered.Skip((_page - 1) * _pageSize).Take(_pageSize).ToList();
        LoadListings(pageListings, _page, _pageSize, _total);
    }

    private void SellButton_Clicked(Base sender, MouseButtonState args)
    {
        Interface.GameUi.OpenSellMarket();
    }

    public void UpdateTransactionHistory(List<MarketTransactionPacket> transactions)
    {
        if (transactions == null)
        {
            return;
        }

        foreach (var t in transactions)
        {
            var descriptor = ItemDescriptor.Get(t.ItemId);
            var itemName = descriptor?.Name ?? "?";
            var message = $"{t.SoldAt:g} {t.BuyerName} x{t.Quantity} {itemName} -> {t.Price}";
            ChatboxMsg.AddMessage(new ChatboxMsg(message, Color.White, ChatMessageType.Trading));
        }
    }

    public void RefreshAfterPurchase()
    {
        SendSearch();
    }

    public void UpdateListings(List<MarketListingPacket> listings)
    {
        _allListings = listings ?? [];
        ApplyFilters();
    }
    public void SendSearch()
    {
        var name = _searchBox.Text?.Trim() ?? "";

        int? minPrice = null;
        if (!string.IsNullOrWhiteSpace(_minPriceBox.Text) && int.TryParse(_minPriceBox.Text, out var minVal))
        {
            minPrice = minVal;
        }

        int? maxPrice = null;
        if (!string.IsNullOrWhiteSpace(_maxPriceBox.Text) && int.TryParse(_maxPriceBox.Text, out var maxVal))
        {
            maxPrice = maxVal;
        }

        ItemType? type = null;
        if (_typeBox.SelectedItem?.UserData is ItemType parsedType)
        {
            type = parsedType;
        }

        string? subType = _subtypeBox.SelectedItem?.UserData as string;

        // Enviar con subtipo adicional
        PacketSender.SendSearchMarket(name, minPrice, maxPrice, type, subType);

    }
    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        SendSearch();
        _listingScroll.SetBounds(10, 60, Width - 20, Height - 110);
    }
}

