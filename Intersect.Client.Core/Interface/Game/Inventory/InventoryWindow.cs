using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Inventory;

public partial class InventoryWindow : Window
{
    public List<SlotItem> Items { get; set; } = [];
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private readonly ComboBox _typeBox;
    private readonly ComboBox _subtypeBox;

    private ItemType? _selectedType;
    private string? _selectedSubtype;

    private SortCriterion _criterion = SortCriterion.TypeThenName;
    private bool _sortAscending = true;

    private string? _lastQuery;
    private bool _lastAsc;

    public InventoryWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Inventory.Title, false, nameof(InventoryWindow))
    {
        DisableResizing();

        Alignment = [Alignments.Bottom, Alignments.Right];
        MinimumSize = new Point(x: 225, y: 327);
        Margin = new Margin(0, 0, 15, 60);
        IsVisibleInTree = false;
        IsResizable = false;
        IsClosable = true;
        _searchBox = new TextBox(this, "SearchBox")
        {
   
            Width = 150,
            Height = 40,
           
       FontName = "source-sans-pro",
    FontSize = 12,// Tamaño de fuente
       
        };
        _searchBox.TextChanged += (s, e) => ApplyFilters(); // reprocesa al escribir
       _searchBox.SetPosition(10, 10);
        _sortButton = new Button(this, "SortButton")
        {
           
            TextColorOverride = Color.White,
            FontName = "source-sans-pro",
            FontSize = 12,// Tamaño de fuente
        };
        _sortButton.SetText("Sort");
        _sortButton.Clicked += SortItems; // ✅ ahora ordena visualmente


        _sortButton.SetPosition(170, 10); // A la derecha del textbox

        _typeBox = new ComboBox(this, "TypeFilter")
        {
            Width = 150,
            Height = 40,
        };
        _typeBox.SetPosition(10, 60);

        var typeAll = _typeBox.AddItem("All", userData: null);
        typeAll.Selected += (_, _) =>
        {
            _selectedType = null;
            UpdateSubtypeOptions();
            ApplyFilters();
        };
        foreach (var type in Enum.GetValues<ItemType>())
        {
            if (type == ItemType.None)
            {
                continue;
            }

            var item = _typeBox.AddItem(type.ToString(), userData: type);
            item.Selected += (_, _) =>
            {
                _selectedType = (ItemType)item.UserData;
                UpdateSubtypeOptions();
                ApplyFilters();
            };
        }

        _subtypeBox = new ComboBox(this, "SubtypeFilter")
        {
            Width = 150,
            Height = 40,
        };
        _subtypeBox.SetPosition(170, 60);

        var subtypeAll = _subtypeBox.AddItem("All", userData: null);
        subtypeAll.Selected += (_, _) =>
        {
            _selectedSubtype = null;
            ApplyFilters();
        };


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
    }

    private void UpdateSubtypeOptions()
    {
        _subtypeBox.RemoveAll();
        var all = _subtypeBox.AddItem("All", userData: null);
        all.Selected += (_, _) =>
        {
            _selectedSubtype = null;
            ApplyFilters();
        };

        if (_selectedType.HasValue &&
            Options.Instance.Items.ItemSubtypes.TryGetValue(_selectedType.Value, out var subtypes))
        {
            foreach (var st in subtypes)
            {
                var local = st; // avoid modified closure
                var item = _subtypeBox.AddItem(local, userData: local);
                item.Selected += (_, _) =>
                {
                    _selectedSubtype = (string?)item.UserData;
                    ApplyFilters();
                };
            }
        }

        _subtypeBox.SelectedItem = all;
    }
    private void ApplyFilters()
    {
        if (Globals.Me?.Inventory == null)
            return;

           var matched = Items.Where(i =>
        {
            var slot = Globals.Me.Inventory[i.SlotIndex];
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

            return SearchHelper.Matches(_searchBox.Text, descriptor.Name);
        });

        // 2) Reordenar: primero coincidentes, luego no-coincidentes
        var matchedList = matched.ToList();
        var matchedSet = matchedList.ToHashSet();
        var nonMatched = Items.Where(i => !matchedSet.Contains(i));

        var arranged = matchedList.Concat(nonMatched).ToList();

        // Actualizar el estado visual de cada slot (visible o no)
        foreach (var item in Items)
        {
            if (item is InventoryItem inventoryItem)
            {
                inventoryItem.SetFilterMatch(matchedSet.Contains(item));
                inventoryItem.Update(); // 🔁 fuerza el refresco visual
            }
        }

        // Mostrar todos los slots en su orden original (sin reordenamiento)
        PopulateSlotContainer.Populate(_slotContainer, arranged);
    }
    private void SortItems(Base sender, MouseButtonState arguments)
    {
        if (Globals.Me?.Inventory == null)
            return;

        var inventory = Globals.Me.Inventory;

        // Obtener los slots ocupados
        var filledItems = Items
            .Where(i => inventory[i.SlotIndex]?.Descriptor != null)
            .ToList();

        // Obtener la lista ordenada de ítems
        var sortedItems = ItemListHelper.FilterAndSort(
            filledItems,
            getDescriptor: i => inventory[i.SlotIndex]?.Descriptor,
            getQuantity: i => inventory[i.SlotIndex]?.Quantity ?? 0,
            searchText: "",
            criterion: _criterion,
            ascending: _sortAscending
        ).ToList();

        // Crear un mapa: Slot actual => ítem que debería ir ahí
        var desiredSlotMap = new Dictionary<int, int>(); // targetSlot => currentSlot

        for (int i = 0; i < sortedItems.Count; i++)
        {
            desiredSlotMap[i] = sortedItems[i].SlotIndex;
        }

        // Swap hasta que todo esté en el lugar correcto
        foreach (var pair in desiredSlotMap)
        {
            int target = pair.Key;
            int current = pair.Value;

            if (current == target)
                continue;

            // Si el ítem actual ya está en el slot destino, no hagas nada
            if (inventory[target]?.Descriptor == inventory[current]?.Descriptor &&
                inventory[target]?.Quantity == inventory[current]?.Quantity)
                continue;

            Globals.Me.SwapItems(current, target);

            // Actualiza el mapa para evitar swaps dobles
            foreach (var key in desiredSlotMap.Keys.ToList())
            {
                if (desiredSlotMap[key] == target)
                {
                    desiredSlotMap[key] = current;
                    break;
                }
            }
        }

        // Refrescar visualmente
        ApplyFilters();
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
        ApplyFilters();
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
