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
        MinimumSize = new Point(x: 436, y: 454);
        IsResizable = false;
        IsClosable = true;

        TitleLabel.FontSize = 14;
        TitleLabel.TextColorOverride = Color.White;

        Closed += (b, s) =>
        {
            _contextMenu?.Close();
            Interface.GameUi.NotifyCloseBank();
        };

        _slotContainer = new ScrollControl(this, "ItemContainer")
        {
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
        _sortButton.SetText(Strings.Bank.Sort);
        _sortButton.Clicked += SortButton_Clicked;

        _typeBox = new ComboBox(this, "TypeFilter")
        {
            Margin = new Margin(4),
            Width = 150,
        };

        var allType = _typeBox.AddItem("All", userData: null);
        allType.Selected += (_, _) =>
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
            Margin = new Margin(4),
            Width = 150,
        };

        var allSub = _subtypeBox.AddItem("All", userData: null);
        allSub.Selected += (_, _) =>
        {
            _selectedSubtype = null;
            ApplyFilters();
        };

        _valueLabel = new Label(this, "ValueLabel")
        {
            Margin = new Margin(4),
        };
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
        ApplyFilters();

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
                var local = st;
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


    public void Refresh() => ApplyFilters();

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
