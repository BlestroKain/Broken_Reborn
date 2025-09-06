using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Intersect.Client.Core;
using Intersect.Client.Entities;
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

    private readonly Base _headerPanel;
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private readonly ComboBox _typeBox;
    private readonly ComboBox _subtypeBox;

 
    private readonly Dictionary<int, HashSet<string>> _subtypesByType = new();

    private readonly HashSet<string> _allSubtypes = new(StringComparer.OrdinalIgnoreCase);

    // --- Filtros/estado ---
    private ItemType? _selectedType;
    private string? _selectedSubtype;
    private SortCriterion _criterion = SortCriterion.TypeThenName;
    private bool _sortAscending = true;
    private string? _lastQuery;
    private bool _lastAsc;
    private bool _inventoryDirty;

    // --- Layout ---
    private const int PAD = 8;       // margen externo
    private const int GAP = 8;       // separación entre controles
    private const int HEADER_H = 60; // alto del header (filtros)
    private const int CTRL_H = 24;   // alto de controles
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

        // Tamaño/posición explícitos
        SetSize(380, 460);

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

        // Construir índices y poblar combo (inicio)
        BuildSubtypeLookup();
        PopulateSubtypeComboForType(null); // al inicio: todos o solo "All" (según implementación abajo)

        _typeBox.ItemSelected += TypeBox_Selected;

        // --------- Lista de ítems (debajo del header) ----------
        _slotContainer = new ScrollControl(this, "ItemsContainer")
        {
            OverflowX = OverflowBehavior.Auto,
            OverflowY = OverflowBehavior.Scroll,
        };
        _slotContainer.SetPosition(PAD, _headerPanel.Y + _headerPanel.Height + GAP);
        

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

        _lastW = Width;
        _lastH = Height;
    }

    // Recalcula posiciones/tamaños si cambia el tamaño de la ventana
    private void RecomputeLayout()
    {
        _headerPanel.SetPosition(PAD, PAD);
        _headerPanel.SetSize(Width - PAD * 2, HEADER_H);

        _searchBox.SetPosition(0, 0);
        _sortButton.SetPosition(_searchBox.X + _searchBox.Width + GAP, 0);
        _typeBox.SetPosition(0, _searchBox.Y + _searchBox.Height + GAP);
        _subtypeBox.SetPosition(_typeBox.X + _typeBox.Width + GAP, _typeBox.Y);

        _slotContainer.SetPosition(PAD, _headerPanel.Y + _headerPanel.Height + GAP);
  
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

    private void TypeBox_Selected(Base sender, ItemSelectedEventArgs args)
    {
        var newType = (ItemType?)args.SelectedItem?.UserData;
        if (newType != _selectedType)
        {
            _selectedType = newType;
            PopulateSubtypeComboForType(_selectedType);
            _selectedSubtype = (string?)_subtypeBox.SelectedItem?.UserData;
            ApplyFilters();
        }
    }

    // Construye los índices de subtipos una sola vez
    private void BuildSubtypeLookup()
    {
        _allSubtypes.Clear();
        _subtypesByType.Clear();

        var dict = Options.Instance.Items.ItemSubtypes;
        if (dict == null) return;

        foreach (var kv in dict)
        {
            // Normaliza el enum (sea cual sea) a int
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
        if (typeof(ComboBox).GetField("_menu", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_subtypeBox) is Intersect.Client.Framework.Gwen.Control.Menu menu)
        {
            menu.ClearChildren(true);
        }
        _selectedSubtype = null;

        var all = _subtypeBox.AddItem(Strings.Inventory.All, userData: null);

        // Por defecto: solo “Todos” (nada de meter todos los subtipos globales)
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

    private void ApplyFilters()
    {
        if (Globals.Me?.Inventory == null) return;

        var inventory = Globals.Me.Inventory;
        var searchText = _searchBox?.Text ?? string.Empty;

        var matchedSet = Items.Where(i =>
        {
            if (i == null) return false;

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
            if (item is InventoryItem inv)
            {
                var isMatch = matchedSet.Contains(item);
                var show = isMatch || !filterActive;

                inv.IsVisibleInParent = show;
                inv.SetFilterMatch(isMatch);
                inv.Update();
            }
        }

        // Mantén el orden original de los slots y distribuye sólo los visibles
        var visibleItems = Items.Where(i => i.IsVisibleInParent).ToList();
        PopulateSlotContainer.Populate(_slotContainer, visibleItems);
    }

    /// <summary>
    /// Ordena todos los ítems del inventario ignorando filtros activos.
    /// Valida nombre, precio, cantidad, tipo y subtipo antes de ordenar;
    /// cualquier entrada inválida se omite, quedando al final del inventario.
    /// </summary>
    private void SortItems(Base sender, MouseButtonState arguments)
    {
        if (Globals.Me?.Inventory == null) return;

        var inventory = Globals.Me.Inventory;

        // 1) Solo slots ocupados
        var filledItems = Items
            .Where(i => i != null && inventory[i.SlotIndex]?.Descriptor != null)
            .ToList();

        // 2) Filtra ítems con datos válidos
        var validItems = new List<SlotItem>();
        foreach (var item in filledItems)
        {
            var slot = inventory[item.SlotIndex];
            var descriptor = slot?.Descriptor;
            var quantity = slot?.Quantity;
            if (descriptor != null && ItemListHelper.IsValid(descriptor, quantity))
            {
                validItems.Add(item);
            }
        }

        // 3) Ordena SIN filtrar (ignora búsqueda/tipo/subtipo)
        var sortedItems = ItemListHelper.FilterAndSort(
            validItems,
            getDescriptor: i => inventory[i.SlotIndex]?.Descriptor,
            getQuantity: i => inventory[i.SlotIndex]?.Quantity ?? 0,
            searchText: null,
            type: null,
            subtype: null,
            criterion: _criterion,
            ascending: _sortAscending
        ).ToList();

        // 4) Mapa “target => current” empaquetando desde 0
        var desiredSlotMap = new Dictionary<int, int>();
        for (int k = 0; k < sortedItems.Count; k++)
            desiredSlotMap[k] = sortedItems[k].SlotIndex;

        // 5) Swaps
        foreach (var kv in desiredSlotMap.ToList())
        {
            int target = kv.Key;
            int current = kv.Value;
            if (current == target) continue;

            if (inventory[target]?.Descriptor == inventory[current]?.Descriptor &&
                inventory[target]?.Quantity == inventory[current]?.Quantity)
                continue;

            Globals.Me.SwapItems(current, target);

            // Actualiza el mapa: quien apuntaba al target ahora apunta al current
            foreach (var key in desiredSlotMap.Keys.ToList())
                if (desiredSlotMap[key] == target)
                    desiredSlotMap[key] = current;
        }

        ApplyFilters();
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
        RecomputeLayout();
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

        // Si cambió el tamaño (future-proof)
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
            // Repoblar combo de subtipos solo con los del tipo actual
            PopulateSubtypeComboForType(_selectedType);
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
