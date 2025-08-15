using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Items;


namespace Intersect.Client.Interface.Game.Inventory;

public partial class InventoryWindow : Window
{
    public List<SlotItem> Items { get; set; } = [];
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;

    private readonly TextBox _searchBox;
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

        _searchBox = new TextBox(this, "SearchBox")
        {
            Margin = new Margin(4),
            Width = 150,
        };
        _searchBox.TextChanged += (s, e) => ApplyFilters();

        _sortButton = new Button(this, "SortButton")
        {
            Margin = new Margin(4),
        };
        _sortButton.SetText("Sort");
        _sortButton.Clicked += SortButton_Clicked;
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
        ApplyFilters();
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

        ApplyFilters();

        IsClosable = Globals.CanCloseInventory;

        if (Globals.Me?.Inventory == default)
        {
            return;
        }

        var slotCount = Math.Min(Options.Instance.Player.MaxInventory, Items.Count);
        for (var slotIndex = 0; slotIndex < slotCount; slotIndex++)
        {
            Items[slotIndex].Update();
        }
    }

    private void SortButton_Clicked(Base sender, ClickedEventArgs arguments)
    {
        _sortAscending = !_sortAscending;
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        if (Globals.Me?.Inventory == null)
        {
            return;
        }

        var query = Items.Where(i => SearchHelper.Matches(_searchBox.Text, GetItemName(i.SlotIndex)));

        query = _sortAscending
            ? query.OrderBy(i => GetSortKey(i.SlotIndex))
            : query.OrderByDescending(i => GetSortKey(i.SlotIndex));


        var visible = query.ToList();
        var visibleSet = visible.ToHashSet();

        foreach (var item in Items)
        {
            item.IsHidden = !visibleSet.Contains(item);
        }

        PopulateSlotContainer.Populate(_slotContainer, visible);
    }

    private static string GetItemName(int slot)
    {
        var inventory = Globals.Me?.Inventory;
        if (inventory == null || slot >= inventory.Length)
        {
            return string.Empty;
        }

        var item = inventory[slot];
        return item?.Descriptor?.Name ?? string.Empty;
    }

    private static (int, int, string) GetSortKey(int slot)
    {
        var inventory = Globals.Me?.Inventory;
        if (inventory == null || slot >= inventory.Length)
        {
            return (int.MaxValue, int.MaxValue, string.Empty);
        }

        var item = inventory[slot];
        return ItemSortHelper.GetSortKey(item?.Descriptor);
    }

    private void InitItemContainer()
    {
        for (var slotIndex = 0; slotIndex < Options.Instance.Player.MaxInventory; slotIndex++)
        {
            Items.Add(new InventoryItem(this, _slotContainer, slotIndex, _contextMenu));
        }

        PopulateSlotContainer.Populate(_slotContainer, Items);
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
