using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Bank;

public partial class BankWindow : Window
{
    public List<SlotItem> Items = [];
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;

    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private readonly Label _valueLabel;
    private readonly ComboBox _typeBox;
    private readonly ComboBox _subtypeBox;
    private SortCriterion _criterion = SortCriterion.TypeThenName;
    private string? _lastQuery;
    private bool _lastAsc;
    private bool _sortAscending = true;
    private ItemType? _selectedType;
    private string? _selectedSubtype;
    private bool _slotsDirty;

    //Init
    public BankWindow(Canvas gameCanvas) : base(
     gameCanvas,
     Globals.IsGuildBank
         ? Strings.Guilds.Bank.ToString(Globals.Me?.Guild)
         : Strings.Bank.Title.ToString(),
     false,
     nameof(BankWindow)
 )
    {
        DisableResizing();
        Interface.InputBlockingComponents.Add(this);

        Alignment = [Alignments.Center];
        MinimumSize = new Point(x: 500, y: 480);
        IsResizable = false;
        IsClosable = true;

        TitleLabel.FontSize = 14;
        TitleLabel.TextColorOverride = Color.White;

        Closed += (b, s) =>
        {
            _contextMenu?.Close();
            Interface.GameUi.NotifyCloseBank();
        };

        // Panel superior para filtros y controles
        var topPanel = new ImagePanel(this, "TopControlsPanel");
        topPanel.SetSize(480, 40);
        topPanel.SetPosition(10, 10);

        _searchBox = new TextBox(topPanel, "SearchBox");
        _searchBox.SetSize(130, 24);
        _searchBox.SetPosition(0, 8);

        _sortButton = new Button(topPanel, "SortButton");
        _sortButton.SetSize(100, 24);
        _sortButton.SetPosition(140, 8);
        UpdateSortButtonText();
        _sortButton.Clicked += SortButton_Clicked;

        _typeBox = new ComboBox(topPanel, "TypeFilter");
        _typeBox.SetSize(100, 24);
        _typeBox.SetPosition(200, 8);
        var allType = _typeBox.AddItem("All", userData: null);
        _typeBox.SelectedItem = allType;
        foreach (var type in Enum.GetValues<ItemType>())
        {
            if (type == ItemType.None) continue;
            _typeBox.AddItem(type.ToString(), userData: type);
        }

        _subtypeBox = new ComboBox(topPanel, "SubtypeFilter");
        _subtypeBox.SetSize(100, 24);
        _subtypeBox.SetPosition(310, 8);
        var allSub = _subtypeBox.AddItem("All", userData: null);
        _subtypeBox.SelectedItem = allSub;

        _valueLabel = new Label(topPanel, "ValueLabel");
        _valueLabel.SetPosition(380, 11);
        _valueLabel.SetText("Bank Value: 0");
        _valueLabel.TextColor = Color.White;
        _valueLabel.FontSize = 10;

        // Contenedor para los slots
        _slotContainer = new ScrollControl(this, "ItemContainer")
        {
            Margin = new Margin(10, 60, 10, 10),
            Dock = Pos.Fill,
            OverflowX = OverflowBehavior.Auto,
            OverflowY = OverflowBehavior.Scroll,
        };

        _contextMenu = new ContextMenu(gameCanvas, "BankContextMenu")
        {
            IsVisibleInParent = false,
            IconMarginDisabled = true,
            ItemFont = GameContentManager.Current.GetFont(name: "sourcesansproblack"),
            ItemFontSize = 10,
        };

        _lastQuery = _searchBox.Text;
        _selectedType = (ItemType?)_typeBox.SelectedItem?.UserData;
        _selectedSubtype = (string?)_subtypeBox.SelectedItem?.UserData;
        _lastAsc = _sortAscending;
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
        ApplyFilters();
    }

    private void InitItemContainer()
    {
        for (var slotIndex = 0; slotIndex < Globals.BankSlotCount; slotIndex++)
        {
            Items.Add(new BankItem(this, _slotContainer, slotIndex, _contextMenu));
        }

        PopulateSlotContainer.Populate(_slotContainer, Items);
    }

    public void Update()
    {
        if (IsVisibleInTree == false)
        {
            return;
        }

        var query = _searchBox.Text;
        var asc = _sortAscending;
        var type = (ItemType?)_typeBox.SelectedItem?.UserData;
        var subtype = (string?)_subtypeBox.SelectedItem?.UserData;

        var changed = _slotsDirty;

        if (type != _selectedType)
        {
            _selectedType = type;
            UpdateSubtypeOptions();
            subtype = (string?)_subtypeBox.SelectedItem?.UserData;
            changed = true;
        }

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

        for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i] is BankItem bankItem)
            {
                bankItem.Update();
            }
        }

        _valueLabel.SetText(Strings.Bank.BankValue.ToString(Strings.FormatQuantityAbbreviated(Globals.BankValue)));
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
    }

    private void UpdateSortButtonText()
    {
        var arrow = _sortAscending ? "▲" : "▼";
        _sortButton.SetText($"{Strings.Inventory.Sort}: {_criterion} {arrow}");
    }

    private void SortItems(Base sender, MouseButtonState arguments)
    {
        if (Globals.BankSlots is null)
        {
            return;
        }

        var bank = Globals.BankSlots;
        var max = Math.Min(Globals.BankSlotCount, bank.Length);

        for (var pass = 0; pass < max - 1; pass++)
        {
            for (var i = 0; i < max - pass - 1; i++)
            {
                if (ItemListHelper.CompareItems(bank[i], bank[i + 1], _criterion, _sortAscending) > 0)
                {
                    PacketSender.SendMoveBankItems(i, i + 1);
                    (bank[i], bank[i + 1]) = (bank[i + 1], bank[i]);
                }
            }
        }

        ApplyFilters();
    }
    private void UpdateSubtypeOptions()
    {
        _subtypeBox.ClearItems();
        var all = _subtypeBox.AddItem("All", userData: null);

        if (_selectedType.HasValue &&
            Options.Instance.Items.ItemSubtypes.TryGetValue(_selectedType.Value, out var subtypes))
        {
            foreach (var st in subtypes)
            {
                var local = st;
                _subtypeBox.AddItem(local, userData: local);
            }
        }

        _subtypeBox.SelectedItem = all;
    }

    private void ApplyFilters()
    {
        if (Globals.BankSlots is null)
        {
            return;
        }

        var searchText = _searchBox?.Text ?? string.Empty;

        var matchedSet = Items.Where(i =>
        {
            var slot = Globals.BankSlots[i.SlotIndex];
            var descriptor = slot?.Descriptor;
            if (descriptor == null)
            {
                return false;
            }

            if (_selectedType.HasValue && descriptor.ItemType != _selectedType)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(_selectedSubtype) &&
                !descriptor.Subtype.Equals(_selectedSubtype, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return SearchHelper.Matches(searchText, descriptor.Name);
        }).ToHashSet();

        var filterActive = !string.IsNullOrWhiteSpace(searchText) ||
                           _selectedType.HasValue ||
                           !string.IsNullOrEmpty(_selectedSubtype);

        foreach (var item in Items)
        {
            if (item is BankItem bankItem)
            {
                var isMatch = matchedSet.Contains(item);
                var show = isMatch || !filterActive;
                bankItem.IsVisibleInParent = show;
                bankItem.SetFilterMatch(isMatch);
                bankItem.Update();
            }
        }

        var visibleItems = Items.Where(i => i.IsVisibleInParent).ToList();
        PopulateSlotContainer.Populate(_slotContainer, visibleItems);
    }


    public void Refresh()
    {
        _slotsDirty = true;
    }

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
