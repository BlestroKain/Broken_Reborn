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
using Intersect.Client.Entities;

namespace Intersect.Client.Interface.Game.Inventory;

public partial class InventoryWindow : Window
{
    public List<SlotItem> Items { get; set; } = [];

    private readonly Base _headerPanel;
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private readonly ComboBox _typeBox;
    private readonly ComboBox _subtypeBox;

    // --- Filtros/estado ---
    private ItemType? _selectedType;
    private string? _selectedSubtype;
    private SortCriterion _criterion = SortCriterion.TypeThenName;
    private bool _sortAscending = true;
    private string? _lastQuery;
    private bool _lastAsc;
    private bool _inventoryDirty;

    // --- Layout ---
    private const int PAD = 8;                // margen externo
    private const int GAP = 8;                // separación entre controles
    private const int HEADER_H = 92;          // alto del header (contiene filtros)
    private const int CTRL_H = 24;            // alto controles (textbox/botón/combos)
    private const int CTRL_W_SMALL = 140;
    private const int CTRL_W_MED = 150;

    private int _lastW, _lastH;

    public InventoryWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Inventory.Title, false, nameof(InventoryWindow))
    {
        DisableResizing(); // si luego quieres permitir resize, quita esto
        Alignment = [Alignments.Bottom, Alignments.Right];
        Margin = new Margin(0, 0, 15, 60);
        IsVisibleInTree = false;
        IsResizable = false;
        IsClosable = true;

        // Tamaño/posición explícitos del inventario (ajústalos a gusto)
        SetSize(380, 460);
        // Alignment ya lo tira abajo a la derecha con Margin; si quisieras fijo:
        // SetPosition(Graphics.Renderer.ScreenWidth - Width - 15, Graphics.Renderer.ScreenHeight - Height - 60);

        // --------- Header (contenedor de filtros) ----------
        _headerPanel = new Base(this, "HeaderPanel");
        _headerPanel.SetPosition(PAD, PAD);
        _headerPanel.SetSize(Width - PAD * 2, HEADER_H);

        // Campo de búsqueda
        _searchBox = new TextBox(_headerPanel, "SearchBox")
        {
            Width = CTRL_W_MED,
            Height = CTRL_H,
            FontName = "source-sans-pro",
            FontSize = 12,
        };
        _searchBox.SetPosition(0, 0);

        // Botón de orden
        _sortButton = new Button(_headerPanel, "SortButton")
        {
            Width = 100,
            Height = CTRL_H,
            TextColorOverride = Color.White,
            FontName = "source-sans-pro",
            FontSize = 12,
        };
        _sortButton.SetPosition(_searchBox.X + _searchBox.Width + GAP, 0);
        UpdateSortButtonText();
        _sortButton.Clicked += SortButton_Clicked;

        // Combo de tipo
        _typeBox = new ComboBox(_headerPanel, "TypeFilter")
        {
            Width = CTRL_W_SMALL,
            Height = CTRL_H,
        };
        _typeBox.SetPosition(0, _searchBox.Y + _searchBox.Height + GAP);

        var typeAll = _typeBox.AddItem(Strings.Inventory.All, userData: null);
        _typeBox.SelectedItem = typeAll;
        foreach (var type in Enum.GetValues<ItemType>())
        {
            if (type == ItemType.None) continue;
            _typeBox.AddItem(type.ToString(), userData: type);
        }

        // Combo de subtipo
        _subtypeBox = new ComboBox(_headerPanel, "SubtypeFilter")
        {
            Width = CTRL_W_SMALL,
            Height = CTRL_H,
        };
        _subtypeBox.SetPosition(_typeBox.X + _typeBox.Width + GAP, _typeBox.Y);

        var subtypeAll = _subtypeBox.AddItem(Strings.Inventory.All, userData: null);
        _subtypeBox.SelectedItem = subtypeAll;

        // --------- Lista de ítems (debajo del header) ----------
        _slotContainer = new ScrollControl(this, "ItemsContainer")
        {
            OverflowX = OverflowBehavior.Auto,
            OverflowY = OverflowBehavior.Scroll,
        };
        // tamaño/posición explícitos; nada de Dock.Fill
        _slotContainer.SetPosition(PAD, _headerPanel.Y + _headerPanel.Height + GAP);
        _slotContainer.SetSize(Width - PAD * 2, Height - (_headerPanel.Y + _headerPanel.Height + GAP) - PAD);

        // Context menu
        _contextMenu = new ContextMenu(gameCanvas, "InventoryContextMenu")
        {
            IsVisibleInParent = false,
            IconMarginDisabled = true,
            ItemFont = GameContentManager.Current.GetFont(name: "sourcesansproblack"),
            ItemFontSize = 10,
        };

        // Estado inicial
        _lastQuery = _searchBox.Text;
        _selectedType = (ItemType?)_typeBox.SelectedItem?.UserData;
        _selectedSubtype = (string?)_subtypeBox.SelectedItem?.UserData;
        _lastAsc = _sortAscending;

        if (Globals.Me is { } player)
        {
            player.InventoryUpdated += PlayerOnInventoryUpdated;
        }

        // Guarda tamaño para detectar cambios y recalcular layout
        _lastW = Width;
        _lastH = Height;
    }

    // Recalcula posiciones/tamaños si cambia el tamaño de la ventana
    private void RecomputeLayout()
    {
        // Header
        _headerPanel.SetPosition(PAD, PAD);
        _headerPanel.SetSize(Width - PAD * 2, HEADER_H);

        // Reposicionar controles del header por si cambiaste anchos
        _searchBox.SetPosition(0, 0);
        _sortButton.SetPosition(_searchBox.X + _searchBox.Width + GAP, 0);
        _typeBox.SetPosition(0, _searchBox.Y + _searchBox.Height + GAP);
        _subtypeBox.SetPosition(_typeBox.X + _typeBox.Width + GAP, _typeBox.Y);

        // Lista
        _slotContainer.SetPosition(PAD, _headerPanel.Y + _headerPanel.Height + GAP);
        _slotContainer.SetSize(Width - PAD * 2, Height - (_headerPanel.Y + _headerPanel.Height + GAP) - PAD);
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

    private void UpdateSubtypeOptions()
    {
        _subtypeBox.ClearItems();
        var all = _subtypeBox.AddItem(Strings.Inventory.All, userData: null);

        if (_selectedType.HasValue &&
            Options.Instance.Items.ItemSubtypes.TryGetValue(_selectedType.Value, out var subtypes))
        {
            foreach (var st in subtypes)
            {
                var local = st; // evitar cierre modificado
                _subtypeBox.AddItem(local, userData: local);
            }
        }

        _subtypeBox.SelectedItem = all;
    }

    private void ApplyFilters()
    {
        if (Globals.Me?.Inventory == null) return;

        var matched = Items.Where(i =>
        {
            var slot = Globals.Me.Inventory[i.SlotIndex];
            var descriptor = slot?.Descriptor;
            if (descriptor == null) return false;

            if (_selectedType.HasValue && descriptor.ItemType != _selectedType) return false;

            if (!string.IsNullOrEmpty(_selectedSubtype) &&
                !descriptor.Subtype.Equals(_selectedSubtype, StringComparison.OrdinalIgnoreCase))
                return false;

            return SearchHelper.Matches(_searchBox.Text, descriptor.Name);
        });

        var matchedList = matched.ToList();
        var matchedSet = matchedList.ToHashSet();
        var nonMatched = Items.Where(i => !matchedSet.Contains(i));
        var arranged = matchedList.Concat(nonMatched).ToList();

        var filterActive = !string.IsNullOrWhiteSpace(_searchBox.Text) ||
                           _selectedType.HasValue ||
                           !string.IsNullOrEmpty(_selectedSubtype);

        foreach (var item in Items)
        {
            if (item is InventoryItem inventoryItem)
            {
                var isMatch = matchedSet.Contains(item);
                var slot = Globals.Me.Inventory[inventoryItem.SlotIndex];
                var hasDescriptor = slot?.Descriptor != null;

                var show = isMatch || !filterActive || !hasDescriptor;
                inventoryItem.IsVisibleInParent = show;
                inventoryItem.SetFilterMatch(isMatch);
                inventoryItem.Update();
            }
        }

        PopulateSlotContainer.Populate(_slotContainer, arranged);
    }

    private void SortItems(Base sender, MouseButtonState arguments)
    {
        if (Globals.Me?.Inventory == null) return;

        var inventory = Globals.Me.Inventory;

        var filledItems = Items
            .Where(i => inventory[i.SlotIndex]?.Descriptor != null)
            .ToList();

        var sortedItems = ItemListHelper.FilterAndSort(
            filledItems,
            getDescriptor: i => inventory[i.SlotIndex]?.Descriptor,
            getQuantity: i => inventory[i.SlotIndex]?.Quantity ?? 0,
            searchText: "",
            criterion: _criterion,
            ascending: _sortAscending
        ).ToList();

        var desiredSlotMap = new Dictionary<int, int>(); // targetSlot => currentSlot
        for (int i = 0; i < sortedItems.Count; i++)
        {
            desiredSlotMap[i] = sortedItems[i].SlotIndex;
        }

        foreach (var pair in desiredSlotMap.ToList())
        {
            int target = pair.Key;
            int current = pair.Value;
            if (current == target) continue;

            if (inventory[target]?.Descriptor == inventory[current]?.Descriptor &&
                inventory[target]?.Quantity == inventory[current]?.Quantity)
                continue;

            Globals.Me.SwapItems(current, target);

            foreach (var key in desiredSlotMap.Keys.ToList())
            {
                if (desiredSlotMap[key] == target)
                {
                    desiredSlotMap[key] = current;
                    break;
                }
            }
        }
        // visual se refresca en Update()
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
        RecomputeLayout(); // << asegura que todo quede bien puesto
        ApplyFilters();
    }

    public void OpenContextMenu(int slot)
    {
        if (Items.Count <= slot) return;
        Items[slot].OpenContextMenu();
    }

    public void Update()
    {
        if (!IsVisibleInParent) return;

        IsClosable = Globals.CanCloseInventory;
        if (Globals.Me?.Inventory == default) return;

        // Si por algún motivo cambió el tamaño (future-proof)
        if (_lastW != Width || _lastH != Height)
        {
            _lastW = Width;
            _lastH = Height;
            RecomputeLayout();
        }

        var query = _searchBox.Text;
        var asc = _sortAscending;
        var type = (ItemType?)_typeBox.SelectedItem?.UserData;
        var subtype = (string?)_subtypeBox.SelectedItem?.UserData;

        var changed = _inventoryDirty;

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
            _inventoryDirty = false;
        }

        var slotCount = Math.Min(Options.Instance.Player.MaxInventory, Items.Count);
        for (var slotIndex = 0; slotIndex < slotCount; slotIndex++)
        {
            Items[slotIndex].Update();
        }
    }

    private void PlayerOnInventoryUpdated(Player player, int slotIndex)
    {
        _inventoryDirty = true;
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
        if (!Globals.CanCloseInventory) return;
        _contextMenu?.Close();
        base.Hide();
    }
}
