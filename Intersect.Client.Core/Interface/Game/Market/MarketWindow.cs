using System;
using System.Collections.Generic;
using System.Timers;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control.Layout;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Timer = System.Timers.Timer;


namespace Intersect.Client.Interface.Game.Market
{
    public class MarketWindow
    {
        public static MarketWindow Instance;

        private WindowControl mMarketWindow;
        private ScrollControl mListingScroll;
        private Label mTitle;
        private readonly Dictionary<Guid, MarketItem> mCurrentItems = new();
        private readonly List<Guid> mListingOrder = new();

        private TextBox mSearchBox;
        private TextBoxNumeric mMinPriceBox;
        private TextBoxNumeric mMaxPriceBox;
        private ComboBox mItemTypeCombo;
        private Button mSearchButton;
        private Button mSellButton;
        private Button mRetryButton;
        private Label mNoResultsLabel;
        private Label mErrorLabel;
        private Label mMaxLabel;
        private Label mMinLabel;
        private Label mNameLabel;
        private Label mTypeLabel;
        private Label mHeaderNameLabel;
        private Label mHeaderQuantityLabel;
        private Label mHeaderPriceLabel;
        private ImagePanel mListContainer;
        private Label mSubTypeLabel;
        private ComboBox mItemSubTypeCombo;

        private Button mPrevButton;
        private Button mNextButton;

        private ItemType? _selectedType;
        private string? _selectedSubtype;
        private readonly Timer _debounce;

        private int _page;
        private readonly int _pageSize;
        private int _total;

        private string _lastName = string.Empty;
        private int? _lastMinPrice;
        private int? _lastMaxPrice;
        private ItemType? _lastType;
        private string? _lastSubtype;
        private int _lastPage;

        private bool _waiting;
        private bool _closed;

        // Virtualization fields
        private const int ItemHeight = 44;
        private const int VirtualizationThreshold = 40;
        private const int VirtualBuffer = 2;
        private bool _useVirtualization;
        private readonly List<MarketListingPacket> _allListings = new();
        private readonly List<MarketListingPacket> _filteredListings = new();
        private readonly List<MarketItem> _virtualRows = new();


        public MarketWindow(Canvas parent)
        {
            Instance = this;
            _pageSize = Options.Instance.Market.PageSize;
            mMarketWindow = new WindowControl(parent, Strings.Market.windowTitle, false, "MarketWindow");
            mMarketWindow.SetSize(800, 640);
            mMarketWindow.DisableResizing();
            mMarketWindow.Focus();
           

            mNameLabel = new Label(mMarketWindow, "MarketNameLabel");
            mNameLabel.Text = Strings.Market.itemNameLabel;
           
            mNameLabel.SetBounds(20, 45, 140, 20);

            mSearchBox = new TextBox(mMarketWindow, "MarketSearchBox");
           
            mSearchBox.Focus();

   
            mTypeLabel = new Label(mMarketWindow, "MarketTypeLabel");
            mTypeLabel.Text = Strings.Market.itemTypeLabel;
            // Crear mItemTypeCombo
            mItemTypeCombo = new ComboBox(mMarketWindow, "MarketItemTypeCombo");
            var allType = mItemTypeCombo.AddItem("All", userData: null);
            mItemTypeCombo.SelectedItem = allType;
            foreach (ItemType type in Enum.GetValues<ItemType>())
            {
                if (type != ItemType.Currency)
                {
                    mItemTypeCombo.AddItem(type.ToString(), userData: type);
                }
            }

            // Subtipo Label y ComboBox (antes de usar UpdateSubTypeCombo)
            mSubTypeLabel = new Label(mMarketWindow, "MarketSubTypeLabel");
            mSubTypeLabel.Text = Strings.Market.itemSubTypeLabel;
            mSubTypeLabel.SetBounds(560, 40, 60, 20);

            mItemSubTypeCombo = new ComboBox(mMarketWindow, "MarketItemSubTypeCombo");
            mItemSubTypeCombo.SetBounds(620, 40, 160, 25);
            var allSub = mItemSubTypeCombo.AddItem("All", userData: null);
            mItemSubTypeCombo.SelectedItem = allSub;


            mItemTypeCombo.ItemSelected += (s, a) => { UpdateSubTypeCombo(); ApplyFilters(); };
            mItemSubTypeCombo.ItemSelected += (s, a) => ApplyFilters();
            mSearchBox.TextChanged += (s, a) => ApplyFilters();

            _selectedType = (ItemType?)mItemTypeCombo.SelectedItem?.UserData;
            _selectedSubtype = (string?)mItemSubTypeCombo.SelectedItem?.UserData;

            _debounce = new Timer(500)
            {
                AutoReset = false,
            };
            _debounce.Elapsed += DebounceElapsed;
            mMarketWindow.Closed += OnWindowClosed;
            mMarketWindow.Disposed += OnWindowClosed;

            mMinLabel = new Label(mMarketWindow, "MarketMinLabel");
            mMinLabel.Text = Strings.Market.minPriceLabel;
          
            mMinPriceBox = new TextBoxNumeric(mMarketWindow, "MarketMinPriceBox");
         
            mMinPriceBox.SetText("", false);

            mMaxLabel = new Label(mMarketWindow, "MarketMaxLabel");
            mMaxLabel.Text = Strings.Market.maxPriceLabel;

            mMaxPriceBox = new TextBoxNumeric(mMarketWindow, "MarketMaxPriceBox");
          
            mMaxPriceBox.SetText("", false);

            mSearchButton = new Button(mMarketWindow, "MarketSearchButton");

            mSearchButton.SetText(Strings.Market.searchButton);
            mSearchButton.Clicked += (s, a) =>
            {
                _page = 0;
                UpdatePagination();
                QueueSearch();
            };

            mSellButton = new Button(mMarketWindow, "MarketSellButton");
            mSellButton.SetText(Strings.Market.sellButton);

            mSellButton.Clicked += SellMarket_Clicked;
            // Fila 1
            mNameLabel.SetBounds(20, 40, 140, 20);
            mSearchBox.SetBounds(160, 40, 160, 25);

            mTypeLabel.SetBounds(340, 40, 50, 20);
            mItemTypeCombo.SetBounds(390, 40, 160, 25);

            // Fila 2
            mMinLabel.SetBounds(20, 75, 120, 20);
            mMinPriceBox.SetBounds(140, 75, 80, 25);

            mMaxLabel.SetBounds(230, 75, 120, 20);
            mMaxPriceBox.SetBounds(350, 75, 80, 25);

            mSearchButton.SetBounds(450, 75, 100, 30);
            mSellButton.SetBounds(560, 75, 100, 30);

            mPrevButton = new Button(mMarketWindow, "MarketPrevButton");
            mPrevButton.SetText("<");
            mPrevButton.SetBounds(20, 570, 100, 25);
            mPrevButton.Clicked += (s, a) =>
            {
                _page--;
                UpdatePagination();
                QueueSearch();
            };

            mNextButton = new Button(mMarketWindow, "MarketNextButton");
            mNextButton.SetText(">");
            mNextButton.SetBounds(680, 570, 100, 25);
            mNextButton.Clicked += (s, a) =>
            {
                _page++;
                UpdatePagination();
                QueueSearch();
            };

            // 📦 Contenedor general del listado (incluye encabezados + scroll)
            mListContainer = new ImagePanel(mMarketWindow, "MarketContainer");
            mListContainer.SetBounds(20, 115, 760, 450); // Estira casi hasta el fondo


            // 🧾 Encabezados dentro del contenedor
            mHeaderNameLabel = new Label(mListContainer, "MarketHeaderName");
            mHeaderNameLabel.Text = Strings.Market.headerItemName;
            mHeaderNameLabel.SetBounds(10, 0, 200, 20);

            mHeaderQuantityLabel = new Label(mListContainer, "MarketHeaderQuantity");
            mHeaderQuantityLabel.Text = Strings.Market.headerQuantity;
               
            mHeaderQuantityLabel.SetBounds(250, 0, 100, 20);

            mHeaderPriceLabel = new Label(mListContainer, "MarketHeaderPrice");
            mHeaderPriceLabel.Text = Strings.Market.headerPrice;

            mHeaderPriceLabel.SetBounds(360, 0, 100, 20);

        
            // ❌ Mensaje si no hay resultados (fuera del contenedor)
            mNoResultsLabel = new Label(mMarketWindow);
            mNoResultsLabel.SetText(Strings.Market.noResults);
            mNoResultsLabel.SetBounds(250, 300, 300, 30);

            mNoResultsLabel.Hide(); // Oculto por defecto

            mErrorLabel = new Label(mMarketWindow);
            mErrorLabel.SetBounds(250, 300, 300, 30);
            mErrorLabel.Hide();

            mRetryButton = new Button(mMarketWindow);
            mRetryButton.SetText(Strings.Market.retryButton);
            mRetryButton.SetBounds(350, 335, 100, 30);
            mRetryButton.Clicked += (s, a) => ResendLastSearch();
            mRetryButton.Hide();
            InitScrollPanel();

            mMarketWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());


            SendSearch(); // Ejecutar búsqueda inicial
            UpdatePagination();
           
        }

        public void InitScrollPanel()
        {
            // 🔽 Scroll dentro del contenedor
            mListingScroll = new ScrollControl(mListContainer, "MarketListingScroll");
            var verticalScrollBar = mListingScroll.VerticalScrollBar;
            mListingScroll.EnableScroll(false, true);
            mListingScroll.SetBounds(0, 25, 760, 425); // Más alto, acorde al nuevo mListContainer

            mListingScroll.Show(); // Forzar creación visual

            verticalScrollBar.BarMoved -= OnScroll;
            verticalScrollBar.BarMoved += OnScroll;

            mListingScroll.SizeChanged += OnListingScrollSizeChanged;

        }
        private void SellMarket_Clicked(Base sender, MouseButtonState arguments)
        {
            if (Interface.GameUi.mSellMarketWindow != null && Interface.GameUi.mSellMarketWindow.IsVisible())
            {
                return;
            }

            if (mMarketWindow.Parent is Canvas parentCanvas)
            {
                Interface.GameUi.mSellMarketWindow = new SellMarketWindow(parentCanvas);
                Interface.GameUi.mSellMarketWindow.Show();
                Interface.GameUi.mSellMarketWindow.Update();
            }
        }

        private void DebounceElapsed(object? sender, ElapsedEventArgs e)
        {
            if (_closed)
            {
                return;
            }
            mMarketWindow?.RunOnMainThread(SendSearch);
        }

        private void OnWindowClosed(Base sender, EventArgs args)
        {
            _closed = true;
            if (mListingScroll?.VerticalScrollBar != null)
            {
                mListingScroll.VerticalScrollBar.BarMoved -= OnScroll;
            }
            _debounce.Stop();
            _debounce.Dispose();
        }

        public void QueueSearch()
        {
            _debounce.Stop();
            _debounce.Start();
        }

        public void SendSearch()
        {
            var name = mSearchBox.Text?.Trim() ?? string.Empty;

            int? minPrice = null;
            if (!string.IsNullOrWhiteSpace(mMinPriceBox.Text) && int.TryParse(mMinPriceBox.Text, out var minVal))
            {
                minPrice = minVal;
            }

            int? maxPrice = null;
            if (!string.IsNullOrWhiteSpace(mMaxPriceBox.Text) && int.TryParse(mMaxPriceBox.Text, out var maxVal))
            {
                maxPrice = maxVal;
            }

            var type = (ItemType?)mItemTypeCombo.SelectedItem?.UserData;
            var subType = (string?)mItemSubTypeCombo.SelectedItem?.UserData;

            _lastName = name;
            _lastMinPrice = minPrice;
            _lastMaxPrice = maxPrice;
            _lastType = type;
            _lastSubtype = subType;
            _lastPage = _page;
            _waiting = true;

            mErrorLabel.Hide();
            mRetryButton.Hide();

            PacketSender.SendSearchMarket(name, minPrice, maxPrice, type, subType, _page);

        }

        private void ResendLastSearch()
        {
            PacketSender.SendSearchMarket(_lastName, _lastMinPrice, _lastMaxPrice, _lastType, _lastSubtype, _lastPage);
            _waiting = true;
            mErrorLabel.Hide();
            mRetryButton.Hide();
        }

        public void SearchFailed(string message)
        {
            _waiting = false;
            mErrorLabel.SetText(message);
            mErrorLabel.Show();
            mRetryButton.Show();
        }

        public bool IsWaitingSearch => _waiting;
        private void UpdateSubTypeCombo()
        {
            mItemSubTypeCombo.ClearItems();
            var all = mItemSubTypeCombo.AddItem("All", userData: null);

            var selectedType = (ItemType?)mItemTypeCombo.SelectedItem?.UserData;
            if (!selectedType.HasValue)
            {
                if (Options.Instance?.Items?.ItemSubtypes != null)
                {
                    var allSubtypes = Options.Instance.Items.ItemSubtypes
                        .SelectMany(kvp => kvp.Value)
                        .Distinct()
                        .OrderBy(subtype => subtype);
                    foreach (var subtype in allSubtypes)
                    {
                        mItemSubTypeCombo.AddItem(subtype, userData: subtype);
                    }
                }
            }
            else
            {
                if (Options.Instance?.Items?.ItemSubtypes != null &&
                    Options.Instance.Items.ItemSubtypes.TryGetValue(selectedType.Value, out var subtypes))
                {
                    foreach (var subtype in subtypes.Distinct().OrderBy(sub => sub))
                    {
                        mItemSubTypeCombo.AddItem(subtype, userData: subtype);
                    }
                }
            }

            mItemSubTypeCombo.SelectedItem = all;
            _selectedSubtype = (string?)mItemSubTypeCombo.SelectedItem?.UserData;
        }

        private void ApplyFilters()
        {
            var query = mSearchBox.Text ?? string.Empty;
            _selectedType = (ItemType?)mItemTypeCombo.SelectedItem?.UserData;
            _selectedSubtype = (string?)mItemSubTypeCombo.SelectedItem?.UserData;

            if (_useVirtualization)
            {
                _filteredListings.Clear();
                foreach (var listing in _allListings)
                {
                    if (!ItemDescriptor.TryGet(listing.ItemId, out var desc))
                    {
                        continue;
                    }

                    var match = true;
                    if (_selectedType.HasValue && desc.ItemType != _selectedType)
                    {
                        match = false;
                    }

                    if (!string.IsNullOrEmpty(_selectedSubtype) && !string.Equals(desc.Subtype, _selectedSubtype, StringComparison.OrdinalIgnoreCase))
                    {
                        match = false;
                    }

                    if (!SearchHelper.Matches(query, desc.Name ?? string.Empty))
                    {
                        match = false;
                    }

                    if (match)
                    {
                        _filteredListings.Add(listing);
                    }
                }

                if (_filteredListings.Count > 0)
                {
                    mNoResultsLabel.Hide();
                }
                else
                {
                    mNoResultsLabel.Show();
                }

                mListingScroll.InnerPanel.SetSize(mListingScroll.InnerPanel.Width, _filteredListings.Count * ItemHeight);
                mListingScroll.VerticalScrollBar.ContentSize = _filteredListings.Count * ItemHeight;
                mListingScroll.VerticalScrollBar.ViewableContentSize = mListingScroll.Height;
                UpdateVirtualRows();
                return;
            }

            var visible = new List<MarketItem>();
            foreach (var id in mListingOrder)
            {
                if (!mCurrentItems.TryGetValue(id, out var item))
                {
                    continue;
                }

                var desc = item.Descriptor;
                if (desc == null)
                {
                    item.Container.IsVisibleInParent = false;
                    continue;
                }

                var match = true;
                if (_selectedType.HasValue && desc.ItemType != _selectedType)
                {
                    match = false;
                }

                if (!string.IsNullOrEmpty(_selectedSubtype) && !string.Equals(desc.Subtype, _selectedSubtype, StringComparison.OrdinalIgnoreCase))
                {
                    match = false;
                }

                if (!SearchHelper.Matches(query, desc.Name ?? string.Empty))
                {
                    match = false;
                }

                item.Container.IsVisibleInParent = match;
                if (match)
                {
                    visible.Add(item);
                }
            }

            int offsetY = 0;
            foreach (var item in visible)
            {
                item.Container.SetBounds(0, offsetY, 750, ItemHeight);
                offsetY += ItemHeight;
            }
            mListingScroll.SetInnerSize(
        mListingScroll.Width - 16,
        visible.Count * ItemHeight
    );
            mListingScroll.UpdateScrollBars();

        }

        private void OnScroll(Base sender, EventArgs args)
        {
            UpdateVirtualRows();
        }

        private void OnListingScrollSizeChanged(Base sender, ValueChangedEventArgs<Point> args)
        {
            if (!_useVirtualization || _allListings.Count == 0)
            {
                return;
            }

            var visibleRows = Math.Max(1, mListingScroll.Height / ItemHeight);
            var requiredRows = visibleRows + VirtualBuffer * 2;

            if (requiredRows == _virtualRows.Count)
            {
                return;
            }

            foreach (var row in _virtualRows)
            {
                row.Container?.DelayedDelete();
            }

            _virtualRows.Clear();

            for (var i = 0; i < requiredRows; i++)
            {
                var dummy = _allListings[0];
                var item = new MarketItem(this, dummy)
                {
                    Container = new ImagePanel(mListingScroll, "MarketItemRow"),
                };
                item.Setup();
                item.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                item.Update(dummy);
                item.Container.Show();
                _virtualRows.Add(item);
            }

            UpdateVirtualRows();
            mListingScroll.UpdateScrollBars();
        }

        private void UpdateVirtualRows()
        {
            if (!_useVirtualization)
            {
                return;
            }

            var totalHeight = _filteredListings.Count * ItemHeight;
            var viewHeight = mListingScroll.Height;
            var scrollPixels = (int)(mListingScroll.VerticalScrollBar.ScrollAmount * Math.Max(0, totalHeight - viewHeight));
            var firstIndex = scrollPixels / ItemHeight;
            var startIndex = Math.Max(0, firstIndex - VirtualBuffer);

            for (var i = 0; i < _virtualRows.Count; i++)
            {
                var dataIndex = startIndex + i;
                var row = _virtualRows[i];
                if (dataIndex >= _filteredListings.Count)
                {
                    row.Container.Hide();
                    continue;
                }

                var listing = _filteredListings[dataIndex];
                row.Update(listing);
                row.Container.Show();
                row.Container.SetBounds(0, dataIndex * ItemHeight, 750, ItemHeight);
            }
        }

        private Guid? CaptureScrollAnchor(float scrollAmount)
        {
            if (mListingScroll?.VerticalScrollBar == null)
            {
                return null;
            }

            var scrollBar = mListingScroll.VerticalScrollBar;
            var totalHeight = scrollBar.ContentSize;
            var viewHeight = scrollBar.ViewableContentSize;
            var scrollPixels = (int)(scrollAmount * Math.Max(0, totalHeight - viewHeight));
            var firstIndex = scrollPixels / ItemHeight;

            var query = mSearchBox.Text ?? string.Empty;
            var selectedType = (ItemType?)mItemTypeCombo.SelectedItem?.UserData;
            var selectedSubtype = (string?)mItemSubTypeCombo.SelectedItem?.UserData;

            if (_useVirtualization)
            {
                if (firstIndex >= 0 && firstIndex < _filteredListings.Count)
                {
                    return _filteredListings[firstIndex].ListingId;
                }

                return null;
            }

            var visibleIndex = 0;
            foreach (var id in mListingOrder)
            {
                if (!mCurrentItems.TryGetValue(id, out var item))
                {
                    continue;
                }

                var desc = item.Descriptor;
                if (desc == null)
                {
                    continue;
                }

                var match = true;
                if (selectedType.HasValue && desc.ItemType != selectedType)
                {
                    match = false;
                }

                if (!string.IsNullOrEmpty(selectedSubtype) &&
                    !string.Equals(desc.Subtype, selectedSubtype, StringComparison.OrdinalIgnoreCase))
                {
                    match = false;
                }

                if (!SearchHelper.Matches(query, desc.Name ?? string.Empty))
                {
                    match = false;
                }

                if (!match)
                {
                    continue;
                }

                if (visibleIndex == firstIndex)
                {
                    return id;
                }

                visibleIndex++;
            }

            return null;
        }

        private void RestoreScrollPosition(Guid? anchorId, float prevScroll)
        {
            if (mListingScroll?.VerticalScrollBar == null)
            {
                return;
            }

            var scrollBar = mListingScroll.VerticalScrollBar;

            if (anchorId.HasValue)
            {
                var query = mSearchBox.Text ?? string.Empty;
                var selectedType = (ItemType?)mItemTypeCombo.SelectedItem?.UserData;
                var selectedSubtype = (string?)mItemSubTypeCombo.SelectedItem?.UserData;

                var newIndex = -1;
                if (_useVirtualization)
                {
                    for (var i = 0; i < _filteredListings.Count; i++)
                    {
                        if (_filteredListings[i].ListingId == anchorId.Value)
                        {
                            newIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    var visibleIndex = 0;
                    foreach (var id in mListingOrder)
                    {
                        if (!mCurrentItems.TryGetValue(id, out var item))
                        {
                            continue;
                        }

                        var desc = item.Descriptor;
                        if (desc == null)
                        {
                            continue;
                        }

                        var match = true;
                        if (selectedType.HasValue && desc.ItemType != selectedType)
                        {
                            match = false;
                        }

                        if (!string.IsNullOrEmpty(selectedSubtype) &&
                            !string.Equals(desc.Subtype, selectedSubtype, StringComparison.OrdinalIgnoreCase))
                        {
                            match = false;
                        }

                        if (!SearchHelper.Matches(query, desc.Name ?? string.Empty))
                        {
                            match = false;
                        }

                        if (!match)
                        {
                            continue;
                        }

                        if (id == anchorId.Value)
                        {
                            newIndex = visibleIndex;
                            break;
                        }

                        visibleIndex++;
                    }
                }

                if (newIndex >= 0)
                {
                    var newOffset = newIndex * ItemHeight;
                    var denominator = Math.Max(0, scrollBar.ContentSize - scrollBar.ViewableContentSize);
                    var s = denominator > 0 ? (float)newOffset / denominator : 0f;
                    if (s < 0f)
                    {
                        s = 0f;
                    }
                    else if (s > 1f)
                    {
                        s = 1f;
                    }

                    scrollBar.ScrollAmount = s;
                    return;
                }
            }

            var fallback = prevScroll;
            if (fallback < 0f)
            {
                fallback = 0f;
            }
            else if (fallback > 1f)
            {
                fallback = 1f;
            }

            scrollBar.ScrollAmount = fallback;
        }

        public void UpdateListings(List<MarketListingPacket> listings, int total)
        {
            var prevScroll = mListingScroll?.VerticalScrollBar?.ScrollAmount ?? 0f;
            var anchorId = CaptureScrollAnchor(prevScroll);

            _total = total;
            _waiting = false;
            mErrorLabel.Hide();
            mRetryButton.Hide();

            var scrollBar = mListingScroll.VerticalScrollBar;
            scrollBar.BarMoved -= OnScroll;

            if (listings.Count > VirtualizationThreshold)
            {
                _useVirtualization = true;
                _allListings.Clear();
                _allListings.AddRange(listings);

                var visibleRows = Math.Max(1, mListingScroll.Height / ItemHeight);
                var requiredRows = visibleRows + VirtualBuffer * 2;

                if (_virtualRows.Count != requiredRows)
                {
                    foreach (var row in _virtualRows)
                    {
                        row.Container?.DelayedDelete();
                    }
                    _virtualRows.Clear();
                    for (var i = 0; i < requiredRows; i++)
                    {
                        var dummy = listings[0];
                        // DESPUÉS
                        var item = new MarketItem(this, dummy)
                        {
                            Container = new ImagePanel(mListingScroll, "MarketItemRow"),
                        };
                        item.Setup();
                        item.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

                        item.Update(dummy); // no imprescindible, pero así evitas “vacíos” iniciales
                        item.Container.Show();
                        _virtualRows.Add(item);

                    }
                }
            }
            else
            {
                _useVirtualization = false;

                foreach (var row in _virtualRows)
                {
                    row.Container?.DelayedDelete();
                }
                _virtualRows.Clear();
                _allListings.Clear();
                _filteredListings.Clear();

                var newIds = new HashSet<Guid>();
                foreach (var listing in listings)
                {
                    newIds.Add(listing.ListingId);
                }

                var toRemove = new List<Guid>();
                foreach (var id in mCurrentItems.Keys)
                {
                    if (!newIds.Contains(id))
                    {
                        toRemove.Add(id);
                    }
                }

                foreach (var id in toRemove)
                {
                    if (mCurrentItems.TryGetValue(id, out var item))
                    {
                        item.Container?.DelayedDelete();
                    }
                    mCurrentItems.Remove(id);
                }

                mListingOrder.Clear();
                foreach (var listing in listings)
                {
                    MarketItem item;
                    if (!mCurrentItems.TryGetValue(listing.ListingId, out item))
                    {
                        // DESPUÉS
                        item = new MarketItem(this, listing)
                        {
                            Container = new ImagePanel(mListingScroll, "MarketItemRow"),
                        };
                        item.Setup();
                        item.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

                        item.Update(listing); // <- importante para que cargue icono/textos ya mismo
                        item.Container.Show();
                        mCurrentItems[listing.ListingId] = item;
                    }
                    else
                    {
                        item.Update(listing);
                    }

                    mListingOrder.Add(listing.ListingId);
                }
            }

            ApplyFilters();

            if (!_useVirtualization)
            {
                mListingScroll.UpdateScrollBars();
            }

            RestoreScrollPosition(anchorId, prevScroll);
            if (_useVirtualization)
            {
                UpdateVirtualRows();
            }

            scrollBar.BarMoved += OnScroll;

            if ((_useVirtualization ? _filteredListings.Count : mListingOrder.Count) == 0)
            {
                mNoResultsLabel.Show();
            }
            else
            {
                mNoResultsLabel.Hide();
            }

            UpdatePagination();
        }

        private void UpdatePagination()
        {
            var maxPage = Math.Max((_total + _pageSize - 1) / _pageSize - 1, 0);
            if (_page < 0)
            {
                _page = 0;
            }
            else if (_page > maxPage)
            {
                _page = maxPage;
            }

            if (mPrevButton != null)
            {
                mPrevButton.IsDisabled = _page <= 0;
            }

            if (mNextButton != null)
            {
                mNextButton.IsDisabled = _page >= maxPage;
            }
        }

        public void RefreshAfterPurchase()
        {
            if (_useVirtualization)
            {
                foreach (var item in _virtualRows)
                {
                    item.ResetBuying();
                }
            }
            else
            {
                foreach (var item in mCurrentItems.Values)
                {
                    item.ResetBuying();
                }
            }

            QueueSearch();
        }

        public void UpdateTransactionHistory(List<MarketTransactionPacket> transactions)
        {
            // Futuro: historial de transacciones
        }

        public void Close() => mMarketWindow?.Close();
        public void Show()
        {
            mMarketWindow?.Show();
        }
    }
}
