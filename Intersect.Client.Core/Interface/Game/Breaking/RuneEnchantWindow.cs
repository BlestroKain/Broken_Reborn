using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game;
using Intersect.Client.Utilities;
using Intersect.Client.Networking;
using Intersect.Client.Interface;
using Intersect.Client.Core;
using Intersect.Client.General;
using Intersect;
using Intersect.Client.Localization;
using Intersect.Client.Items;
using Intersect.Client.Framework.File_Management;
using Intersect.Enums;
using Intersect.Extensions;
using Intersect.Client.Interface.Game.Breaking;

public partial class RuneEnchantWindow : Window
{
    public List<SlotItem> Items { get; set; } = [];
    private readonly ScrollControl _projectionContainer;
    private readonly ScrollControl _inventoryScroll;
    private readonly ImagePanel _itemSlot;
    private readonly ImagePanel _runeSlot;
    private readonly Button _applyButton;
    private readonly Button _closeButton;

    public Item? _selectedItem;
    public Item? _selectedRune;

    public RuneEnchantWindow(Canvas gameCanvas)
        : base(gameCanvas, "Rune Application", false, nameof(RuneEnchantWindow))
    {
        IsResizable = false;
        SetSize(600, 500);
        SetPosition(100, 100);
        IsVisibleInTree = false;
        IsClosable = true;

        _projectionContainer = new ScrollControl(this, "ProjectionContainer");
        _projectionContainer.SetPosition(20, 20);
        _projectionContainer.SetSize(260, 440);
        _projectionContainer.EnableScroll(false, true);

        _inventoryScroll = new ScrollControl(this, "ItemContainer");
        _inventoryScroll.SetPosition(300, 20);
        _inventoryScroll.SetSize(260, 200);
        _inventoryScroll.EnableScroll(false, true);

        _itemSlot = new ImagePanel(this, "ItemSlot");
        _itemSlot.SetPosition(300, 240);
        _itemSlot.SetSize(80, 80);
        _itemSlot.Texture = Graphics.Renderer.WhitePixel;

        _runeSlot = new ImagePanel(this, "RuneSlot");
        _runeSlot.SetPosition(400, 240);
        _runeSlot.SetSize(80, 80);
        _runeSlot.Texture = Graphics.Renderer.WhitePixel;

        _applyButton = new Button(this, "ApplyButton");
        _applyButton.SetPosition(300, 340);
        _applyButton.SetSize(120, 40);
        _applyButton.Text = "Apply Rune";
        _applyButton.Clicked += OnApplyButtonClicked;

        _closeButton = new Button(this, "CloseButton");
        _closeButton.SetPosition(440, 340);
        _closeButton.SetSize(120, 40);
        _closeButton.Text = "Close";
        _closeButton.Clicked += (sender, args) => Hide();

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
    }
    private void InitItemContainer()
    {
        for (int i = 0; i < Options.Instance.Player.MaxInventory; i++)
        {
            var item = new RuneInventoryItem(this, _inventoryScroll, i, null);
            Items.Add(item);
        }

        PopulateSlotContainer.Populate(_inventoryScroll, Items);
        Update();
    }

    public void SelectTargetItem(Item? item)
    {
        if (item == null || item.Descriptor == null) return;

        _selectedItem = item;
        _itemSlot.Texture = Globals.ContentManager.GetTexture(Intersect.Client.Framework.Content.TextureType.Item, item.Descriptor.Icon)
                              ?? Graphics.Renderer.WhitePixel;

        UpdateProjection();
    }

    public void SelectRuneItem(Item? rune)
    {
        if (rune == null || rune.Descriptor == null) return;

        _selectedRune = rune;
        _runeSlot.Texture = Globals.ContentManager.GetTexture(Intersect.Client.Framework.Content.TextureType.Item, rune.Descriptor.Icon)
                             ?? Graphics.Renderer.WhitePixel;

        UpdateProjection();
    }
    public void UpdateProjection()
    {
        if (_selectedItem == null || _selectedRune == null || _selectedRune.Descriptor == null)
        {
            _projectionContainer.Hide();
            return;
        }

        _projectionContainer.DeleteAllChildren();
        _projectionContainer.Show();

        var rune = _selectedRune.Descriptor;
        var stat = rune.TargetStat;
        var vital = rune.TargetVital;
        var amount = rune.AmountModifier;
       
        var residualItem = _selectedItem.ItemProperties.MageSink/100.0;

        string labelStatOrVital;
        string labelAmount = $"Cantidad: +{amount}";
      
        string labelMageSink = $"Carga residual del ítem: {residualItem}";

        if (amount == 0)
        {
            labelStatOrVital = "Efecto: Sin efecto";
        }
        else
        {
            if (stat >= 0 && (int)stat < Enum.GetValues<Stat>().Length)
            {
                labelStatOrVital = $"Stat: {Strings.ItemDescription.StatCounts[(int)stat]}";
            }
            else if (vital >= 0 && (int)vital < Enum.GetValues<Vital>().Length)
            {
                labelStatOrVital = $"Vital: {Strings.ItemDescription.Vitals[(int)vital]}";
            }
            else
            {
                labelStatOrVital = "Efecto: Desconocido";
            }
        }

        // Posicionamiento dinámico
        int offsetY = 10;
        int spacing = 25;

        var lblEffect = new Label(_projectionContainer)
        {
            Text = labelStatOrVital
        };
        lblEffect.SetPosition(10, offsetY);
        lblEffect.FontName = "sourcesansproblack";
        lblEffect.FontSize = 10;
        offsetY += spacing;

        var lblAmount = new Label(_projectionContainer)
        {
            Text = labelAmount
        };
        lblAmount.SetPosition(10, offsetY);
        lblAmount.FontName = "sourcesansproblack";
        lblAmount.FontSize = 10;
        offsetY += spacing;

       
        var lblMageSink = new Label(_projectionContainer)
        {
            Text = labelMageSink
        };
        lblMageSink.SetPosition(10, offsetY);
        lblMageSink.FontName = "sourcesansproblack";
        lblMageSink.FontSize = 10;

        // Colores según valor de residual
        Color sinkColor = residualItem >= 0.9 ? Color.Green :
                          residualItem >= 0.7 ? Color.ForestGreen :
                          residualItem >= 0.5 ? Color.Orange :
                          residualItem >= 0.3 ? Color.OrangeRed :
                          Color.Red;

        lblMageSink.SetTextColor(sinkColor, ComponentState.Normal);
    }

    private void OnApplyButtonClicked(Base sender, MouseButtonState args)
    {
        if (_selectedItem == null || _selectedRune == null)
        {
            PacketSender.SendChatMsg("Select item and rune to apply.", 4);
            return;
        }

        int itemIndex = Globals.Me.Inventory.IndexOf(_selectedItem);
        int runeIndex = Globals.Me.Inventory.IndexOf(_selectedRune);

        if (itemIndex < 0 || runeIndex < 0)
        {
            PacketSender.SendChatMsg("Could not locate item or rune in inventory.", 4);
            return;
        }

        PacketSender.SendUpgradeStat(itemIndex, runeIndex);
        UpdateProjection();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void Update()
    {
        if (!IsVisibleInParent || Globals.Me?.Inventory == null) return;

        var slotCount = Math.Min(Options.Instance.Player.MaxInventory, Items.Count);
        for (int i = 0; i < slotCount; i++)
        {
            Items[i].Update();
        }
        UpdateProjection();
    }
}
