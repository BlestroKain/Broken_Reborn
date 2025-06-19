using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.Enums;
using System.Linq;

namespace Intersect.Client.Interface.Game.Inventory;

public partial class InventoryWindow : Window
{
    public List<SlotItem> Items { get; set; } = [];
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly TextBox _searchBox;
    private readonly ComboBox mTypeFilterBox;
    private readonly ComboBox mSubTypeFilterBox;
    private readonly Button _clearFilterButton;
    private readonly Button _sortButton;
    private bool _sortAscending = true;

    public InventoryWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Inventory.Title, false, nameof(InventoryWindow))
    {
        DisableResizing();

        Alignment = [Alignments.Bottom, Alignments.Right];
        MinimumSize = new Point(x: 225, y: 327);
        Margin = new Margin(0, 0, 15, 60);
        IsVisibleInTree = false;
        IsResizable = false;
        IsClosable = true;

        _slotContainer = new ScrollControl(this, "ItemsContainer")
        {
            Dock = Pos.Fill,
            OverflowX = OverflowBehavior.Auto,
            OverflowY = OverflowBehavior.Scroll,
        };

        _contextMenu = new ContextMenu(gameCanvas, "InventoryContextMenu")
        {
            IsVisibleInParent = false,
            IconMarginDisabled = true,
            ItemFont = GameContentManager.Current.GetFont(name: "sourcesansproblack"),
            ItemFontSize = 10,
        };

        _searchBox = new TextBox(this, "SearchBox");
        Interface.FocusComponents.Add(_searchBox);
        mTypeFilterBox = new ComboBox(this, nameof(mTypeFilterBox));
        mSubTypeFilterBox = new ComboBox(this, nameof(mSubTypeFilterBox));
        _clearFilterButton = new Button(this, nameof(_clearFilterButton)) { Text = "X" };
        _sortButton = new Button(this, nameof(_sortButton)) { Text = "⇅" };

        _searchBox.TextChanged += (_, _) => SortAndApplyInventory();
        mTypeFilterBox.ItemSelected += (_, _) => { RefreshFilters(); SortAndApplyInventory(); };
        mSubTypeFilterBox.ItemSelected += (_, _) => SortAndApplyInventory();
        _clearFilterButton.Clicked += (_, _) =>
        {
            _searchBox.Text = string.Empty;
            mTypeFilterBox.SelectedItem = null;
            mSubTypeFilterBox.SelectedItem = null;
            SortAndApplyInventory();
        };
        _sortButton.Clicked += (_, _) => { _sortAscending = !_sortAscending; SortAndApplyInventory(); };

        RefreshFilters();
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
    }

    public void OpenContextMenu(int slot)
    {
        if (Items.Count <= slot)
        {
            return;
        }

        Items[slot].OpenContextMenu();
    }

    public void Update()
    {
        if (!IsVisibleInParent)
        {
            return;
        }

        IsClosable = Globals.CanCloseInventory;

        if (Globals.Me?.Inventory == default)
        {
            return;
        }

        SortAndApplyInventory();
        foreach (var item in Items.OfType<InventoryItem>().Where(i => !i.IsHidden))
        {
            item.Update();
        }
    }

    private void InitItemContainer()
    {
        for (var slotIndex = 0; slotIndex < Options.Instance.Player.MaxInventory; slotIndex++)
        {
            Items.Add(new InventoryItem(this, _slotContainer, slotIndex, _contextMenu));
        }

        PopulateSlotContainer.Populate(_slotContainer, Items);
    }

    private void RefreshFilters()
    {
        mTypeFilterBox.DeleteAll();
        _ = mTypeFilterBox.AddItem("All");
        foreach (ItemType type in Enum.GetValues<ItemType>())
        {
            var item = mTypeFilterBox.AddItem(type.ToString());
            item.UserData = type;
        }

        mSubTypeFilterBox.DeleteAll();
        _ = mSubTypeFilterBox.AddItem("All");
    }

    private int GetItemRelevance(ItemDescriptor descriptor, string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return 0;
        }

        term = term.Trim();
        var index = descriptor.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase);
        if (index == 0)
        {
            return 2;
        }

        return index > 0 ? 1 : 0;
    }

    private int CompareInventoryItems(InventoryItem a, InventoryItem b)
    {
        var term = _searchBox.Text?.Trim() ?? string.Empty;
        var descriptorA = Globals.Me.Inventory[a.DisplaySlot].Descriptor;
        var descriptorB = Globals.Me.Inventory[b.DisplaySlot].Descriptor;
        var relevanceA = GetItemRelevance(descriptorA, term);
        var relevanceB = GetItemRelevance(descriptorB, term);
        var result = relevanceB.CompareTo(relevanceA);
        if (result != 0)
        {
            return _sortAscending ? result : -result;
        }

        result = string.Compare(descriptorA.Name, descriptorB.Name, StringComparison.OrdinalIgnoreCase);
        return _sortAscending ? result : -result;
    }

    private void SortAndApplyInventory()
    {
        if (Globals.Me?.Inventory == null)
        {
            return;
        }

        var term = _searchBox.Text?.Trim();
        ItemType? typeFilter = mTypeFilterBox.SelectedItem?.UserData as ItemType?;
        var subFilter = mSubTypeFilterBox.SelectedItem?.UserData as string;

        List<InventoryItem> visible = [];
        for (var i = 0; i < Globals.Me.Inventory.Length && i < Items.Count; i++)
        {
            var slot = Globals.Me.Inventory[i];
            var descriptor = slot.Descriptor;
            if (descriptor != null && SearchHelper.IsSearchable(descriptor, term, typeFilter, subFilter))
            {
                var item = (InventoryItem)Items[i];
                item.DisplaySlot = i;
                item.IsHidden = false;
                visible.Add(item);
            }
            else
            {
                Items[i].IsHidden = true;
            }
        }

        visible.Sort(CompareInventoryItems);
        PopulateSlotContainer.Populate(_slotContainer, visible.Cast<SlotItem>().ToList());
    }

    public override void Hide()
    {
        if (!Globals.CanCloseInventory)
        {
            return;
        }

        _contextMenu?.Close();
        base.Hide();
    }
}
