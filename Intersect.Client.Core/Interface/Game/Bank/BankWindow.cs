using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Inventory;
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
    private bool _sortAscending;
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

        // 📦 Panel superior para filtros y controles
        var topPanel = new ImagePanel(this, "TopControlsPanel");
        topPanel.SetSize(480, 40);
        topPanel.SetPosition(10, 10);

        _searchBox = new TextBox(topPanel, "SearchBox");
        _searchBox.SetSize(130, 24);
        _searchBox.SetPosition(0, 8);

        _sortButton = new Button(topPanel, "SortButton");
        _sortButton.SetSize(50, 24);
        _sortButton.SetPosition(140, 8);
        _sortButton.SetText("Sort");
        _sortButton.Clicked += (_, _) =>
        {
            _sortAscending = !_sortAscending;
            PacketSender.SendBankSortPacket(); // Evita reapertura
        };

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
        PacketSender.SendBankSortPacket();
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

    // InventoryWindow.cs
    private void ApplyFilters()
    {
        if (Globals.BankSlots is null)
            return;

        // 1) Calcular los que coinciden con la búsqueda y filtros
        var matched = Items.Where(i =>
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

            return SearchHelper.Matches(_searchBox.Text, descriptor.Name);
        });

        // 2) Reordenar: primero coincidentes, luego no-coincidentes
        var matchedList = matched.ToList();
        var matchedSet = matchedList.ToHashSet();
        var nonMatched = Items.Where(i => !matchedSet.Contains(i));

        var arranged = matchedList.Concat(nonMatched).ToList();

        // 3) "Vaciar" visualmente los que NO coinciden (sin ocultarlos)
        foreach (var it in Items)
        {
            if (it is BankItem b)
                b.SetFilterMatch(matchedSet.Contains(it));
            // Importante: NO tocar it.IsHidden
        }

        // 4) Siempre poblar con TODA la lista para mantener el grid y permitir drop en cualquier slot
        PopulateSlotContainer.Populate(_slotContainer, arranged);
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
