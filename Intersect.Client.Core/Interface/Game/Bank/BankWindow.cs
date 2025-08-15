using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using System.Linq;

namespace Intersect.Client.Interface.Game.Bank;

public partial class BankWindow : Window
{
    public List<SlotItem> Items = [];
    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;

    private readonly TextBox _searchBox;
    private readonly Button _sortButton;
    private readonly Label _valueLabel;

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

    private void SortButton_Clicked(Base sender, ClickedEventArgs arguments)
    {
        PacketSender.SendBankSortPacket();
    }

    private void ApplyFilters()
    {
        if (Globals.BankSlots is null)
        {
            return;
        }

        var matches = Items
            .Where(i => SearchHelper.Matches(_searchBox.Text, Globals.BankSlots[i.SlotIndex]?.Descriptor?.Name));

        var visible = matches.ToList();
        var visibleSet = visible.ToHashSet();

        foreach (var item in Items)
        {
            item.IsHidden = !visibleSet.Contains(item);
        }

        PopulateSlotContainer.Populate(_slotContainer, visible);
    }

    public void Refresh() => ApplyFilters();

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
