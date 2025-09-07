using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Market;

public partial class SellMarketWindow : Window
{
    public static SellMarketWindow? Instance { get; private set; }

    private readonly int _slot;
    private long _minPrice;
    private long _maxPrice = long.MaxValue;

    private readonly Label _quantityLabel;
    private readonly TextBoxNumeric _quantityBox;
    private readonly Label _priceLabel;
    private readonly TextBoxNumeric _priceBox;
    private readonly Label _suggestedPriceLabel;
    private readonly LabeledCheckBox _autoSplitCheckbox;
    private readonly Button _sellButton;
    private readonly Button _cancelButton;

    private const int PAD = 8;
    private const int GAP = 8;
    private const int LABEL_W = 80;
    private const int INPUT_W = 140;
    private const int CTRL_H = 24;

    public SellMarketWindow(Canvas gameCanvas, int slot)
        : base(gameCanvas, Strings.Market.Title, false, nameof(SellMarketWindow))
    {
        Instance = this;
        _slot = slot;
        DisableResizing();
        Alignment = [Alignments.Center];
        IsResizable = false;
        IsClosable = true;
        SetSize(360, 210);

        _quantityLabel = new Label(this, nameof(_quantityLabel))
        {
            Text = Strings.Market.Quantity,
        };

        _quantityBox = new TextBoxNumeric(this, nameof(_quantityBox))
        {
            Minimum = 1,
            Value = 1,
        };

        _priceLabel = new Label(this, nameof(_priceLabel))
        {
            Text = Strings.Market.Price,
        };

        _priceBox = new TextBoxNumeric(this, nameof(_priceBox))
        {
            Minimum = 0,
        };

        _suggestedPriceLabel = new Label(this, nameof(_suggestedPriceLabel));

        _autoSplitCheckbox = new LabeledCheckBox(this, nameof(_autoSplitCheckbox))
        {
            Text = "Dividir en paquetes",
        };

        _sellButton = new Button(this, nameof(_sellButton))
        {
            Text = Strings.Market.Sell,
        };
        _sellButton.Clicked += SellButtonOnClicked;

        _cancelButton = new Button(this, nameof(_cancelButton))
        {
            Text = Strings.InputBox.Cancel,
        };
        _cancelButton.Clicked += (_, _) => Close();

        Closed += (_, _) => Instance = null;
        RecomputeLayout();
    }

    private void RecomputeLayout()
    {
        SetSize(360, 210);
        _quantityLabel.SetBounds(PAD, PAD, LABEL_W, CTRL_H);
        _quantityBox.SetBounds(PAD + LABEL_W + GAP, PAD, INPUT_W, CTRL_H);

        _priceLabel.SetBounds(PAD, PAD + CTRL_H + GAP, LABEL_W, CTRL_H);
        _priceBox.SetBounds(PAD + LABEL_W + GAP, PAD + CTRL_H + GAP, INPUT_W, CTRL_H);

        _suggestedPriceLabel.SetBounds(PAD, PAD + (CTRL_H + GAP) * 2, LABEL_W + INPUT_W + GAP, CTRL_H);
        _autoSplitCheckbox.SetBounds(PAD, PAD + (CTRL_H + GAP) * 3, LABEL_W + INPUT_W + GAP, CTRL_H);

        _sellButton.SetBounds(PAD, Height - CTRL_H - PAD, 100, CTRL_H);
        _cancelButton.SetBounds(Width - 100 - PAD, Height - CTRL_H - PAD, 100, CTRL_H);
    }

    private void SellButtonOnClicked(Base sender, MouseButtonState args)
    {
        var quantity = (int)_quantityBox.Value;
        var price = (long)_priceBox.Value;
        if (price < _minPrice || price > _maxPrice)
        {
            return;
        }
        var autoSplit = _autoSplitCheckbox.IsChecked;
        SellItem(quantity, price, autoSplit);
        Close();
    }

    public ItemProperties? GetItemProperties()
    {
        return Globals.Me?.Inventory[_slot]?.ItemProperties;
    }

    public void SellItem(int quantity, long price, bool autoSplit)
    {
        PacketSender.SendCreateMarketListing(_slot, quantity, price, autoSplit);
    }

    private void UpdateSuggestedPrice()
    {
        var suggested = Globals.Me?.Inventory[_slot]?.Descriptor.Price ?? 0;
        _suggestedPriceLabel.Text = Strings.Market.SuggestedPrice.ToString(suggested);
    }

    public void SetMarketInfo(long suggested, long min, long max)
    {
        _minPrice = min;
        _maxPrice = max > 0 ? max : long.MaxValue;
        _priceBox.Minimum = min;
        _priceBox.Maximum = _maxPrice;
        _suggestedPriceLabel.Text = $"{Strings.Market.SuggestedPrice.ToString(suggested)} (Min {min} Max {max})";
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        RecomputeLayout();
        UpdateSuggestedPrice();
        var item = Globals.Me?.Inventory[_slot];
        if (item != null)
        {
            PacketSender.SendRequestMarketInfo(ItemDescriptor.ListIndex(item.ItemId));
        }
    }
}

