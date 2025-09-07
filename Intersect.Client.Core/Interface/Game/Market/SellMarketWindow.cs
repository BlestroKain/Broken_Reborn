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
    private readonly int _slot;

    private readonly Label _quantityLabel;
    private readonly TextBoxNumeric _quantityBox;
    private readonly Label _priceLabel;
    private readonly TextBoxNumeric _priceBox;
    private readonly Label _suggestedPriceLabel;
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
        _slot = slot;
        DisableResizing();
        Alignment = [Alignments.Center];
        IsResizable = false;
        IsClosable = true;
        SetSize(360, 180);

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

        RecomputeLayout();
    }

    private void RecomputeLayout()
    {
        SetSize(360, 180);
        _quantityLabel.SetBounds(PAD, PAD, LABEL_W, CTRL_H);
        _quantityBox.SetBounds(PAD + LABEL_W + GAP, PAD, INPUT_W, CTRL_H);

        _priceLabel.SetBounds(PAD, PAD + CTRL_H + GAP, LABEL_W, CTRL_H);
        _priceBox.SetBounds(PAD + LABEL_W + GAP, PAD + CTRL_H + GAP, INPUT_W, CTRL_H);

        _suggestedPriceLabel.SetBounds(PAD, PAD + (CTRL_H + GAP) * 2, LABEL_W + INPUT_W + GAP, CTRL_H);

        _sellButton.SetBounds(PAD, Height - CTRL_H - PAD, 100, CTRL_H);
        _cancelButton.SetBounds(Width - 100 - PAD, Height - CTRL_H - PAD, 100, CTRL_H);
    }

    private void SellButtonOnClicked(Base sender, MouseButtonState args)
    {
        var quantity = (int)_quantityBox.Value;
        var price = (long)_priceBox.Value;
        SellItem(quantity, price);
        Close();
    }

    public ItemProperties? GetItemProperties()
    {
        return Globals.Me?.Inventory[_slot]?.ItemProperties;
    }

    public void SellItem(int quantity, long price)
    {
        PacketSender.SendCreateMarketListing(_slot, quantity, price);
    }

    private void UpdateSuggestedPrice()
    {
        var suggested = Globals.Me?.Inventory[_slot]?.Descriptor.Price ?? 0;
        _suggestedPriceLabel.Text = Strings.Market.SuggestedPrice.ToString(suggested);
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        RecomputeLayout();
        UpdateSuggestedPrice();
    }
}

