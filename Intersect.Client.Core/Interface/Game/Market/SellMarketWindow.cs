using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.Layout;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.General;
using Intersect.Client.Items;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Core;
using Intersect.Client.Localization;
using Intersect.Enums;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Network.Packets.Client;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Logging;
using Intersect.Config;

namespace Intersect.Client.Interface.Game.Market;

/// <summary>
/// Ventana para publicar objetos en el mercado.
/// </summary>
public sealed class SellMarketWindow
{
    #region === UI ===

    private readonly WindowControl _window;
    private readonly ScrollControl mInventoryScroll;
    private readonly TextBoxNumeric _priceInput;
    private readonly TextBoxNumeric _quantityInput;
    private readonly Button _confirmButton;
    private readonly Label _infoLabel;
    private readonly Label _taxLabel;
    private readonly Label _suggestedPriceLabel;
    private readonly CheckBox _autoSplitCheckbox;

    // Render helpers por slot
    public readonly List<InventoryItem> Items = new();
    private readonly List<Label> Values = new();

    #endregion

    #region === State ===

    private int _selectedSlot = -1;
    private Guid _selectedItemId = Guid.Empty;

    private Label _suggestedRangeLabel;

    #endregion

    public int X { get; set; }
    public int Y { get; set; }
    private bool Initialized = false;
    public Guid _waitingPriceForItemId = Guid.Empty;
    public SellMarketWindow(Canvas canvas)
    {
        _window = new WindowControl(canvas, "📤 " + Strings.Market.sellwindow, false, "SellMarketWindow");
        _window.SetSize(600, 460);
        _window.SetPosition(Graphics.Renderer.GetScreenWidth() / 2 - 300, Graphics.Renderer.GetScreenHeight() / 2 - 230);
        _window.DisableResizing();

        // Panel inventario
        mInventoryScroll = new ScrollControl(_window, "SellInventoryScroll");
        mInventoryScroll.GetVerticalScrollBar();
        mInventoryScroll.EnableScroll(false, true);
        mInventoryScroll.SetBounds(20, 20, 280, 400);
        // Etiquetas y campos de entrada
        _infoLabel = new Label(_window,"SelectedItem") { Text = Strings.Market.selectitem };
        _suggestedPriceLabel = new Label(_window, "SuggestedLabel");
        _suggestedRangeLabel = new Label(_window, "SuggestedRange");
        _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(0));
        _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(0, 0)); // 
        _quantityInput = new TextBoxNumeric(_window, "QuantityInput");
        _priceInput = new TextBoxNumeric(_window, "PriceInput");
        _quantityInput.Focus();
        Interface.FocusElements.Add(_quantityInput);
        _priceInput.Focus();
        Interface.FocusElements.Add(_priceInput);

        _quantityInput.TextChanged += (_, _) => RefreshTax();
        _priceInput.TextChanged += (_, _) => RefreshTax();

        _taxLabel = new Label(_window, "TaxLabel") { Text = Strings.Market.taxes_0 };

        _autoSplitCheckbox = new CheckBox(_window,"SplitCheckBox") { Text = Strings.Market.splitpackages };
        _autoSplitCheckbox.IsChecked = true;
  
        _confirmButton = new Button(_window,"CorfimButton") { Text = Strings.Market.publish };
        _confirmButton.Disable();
        _confirmButton.Clicked += OnConfirmClicked;

        BuildLayout();
        _window.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
   
    }

    #region === Layout ===

    private void BuildLayout()
    {
        int startX = 320,
            startY = 20,
            labelH = 20,
            inputH = 30,
            padY = 8;

        _infoLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + padY;
        _suggestedPriceLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + (padY * 2);
        _suggestedRangeLabel.SetBounds(startX, startY, 250, labelH); startY += labelH + (padY * 2);
        // Cantidad / Precio etiquetas
        new Label(_window,"QuantityLabel") { Text = Strings.Market.quantity }.SetBounds(startX, startY + 10, 100, labelH);
        new Label(_window,"Pricelabel") { Text = Strings.Market.price }.SetBounds(startX + 110, startY + 10, 100, labelH);
        startY += labelH;

        _quantityInput.SetBounds(startX, startY+10, 100, inputH);
        _priceInput.SetBounds(startX + 110, startY+10, 100, inputH);
        startY += inputH + (padY * 2);

        _taxLabel.SetBounds(startX, startY, 290, labelH); startY += labelH + padY;
        _autoSplitCheckbox.SetBounds(startX, startY, 250, labelH); startY += labelH + padY;
        _confirmButton.SetBounds(startX, startY, 210, 40);
    }

    #endregion

    #region === Inventory UI ===

    public void Update()
    {
        if (!Initialized)
        {
            Initialized = true;
            InitItemContainer();
        }

        if (Items.Count != Options.MaxInvItems || Values.Count != Options.MaxInvItems)
        {
            InitItemContainer();
        }

        for (int i = 0; i < Options.MaxInvItems; i++)
        {
            var slot = Globals.Me.Inventory[i];
            var item = slot?.ItemId != null ? ItemBase.Get(slot.ItemId) : null;

            if (slot == null || item == null || slot.ItemId == Guid.Empty)
            {
                Items[i].Pnl.IsHidden = true;
                Values[i].IsHidden = true;
                continue;
            }

            Items[i].Pnl.IsHidden = false;
            Items[i].Container.RenderColor = CustomColors.Items.Rarities.TryGetValue(item.Rarity, out var color) ? color : Color.White;

            if (item.IsStackable)
            {
                Values[i].IsHidden = slot.Quantity <= 1;
                Values[i].Text = Strings.FormatQuantityAbbreviated(slot.Quantity);
            }
            else
            {
                Values[i].IsHidden = true;
            }

            if (Items[i].IsDragging)
            {
                Items[i].Pnl.IsHidden = true;
                Values[i].IsHidden = true;
            }

            Items[i].Update();
        }
    }

    private void InitItemContainer()
    {
        Items.Clear();
        Values.Clear();

        for (var i = 0; i < Options.MaxInvItems; i++)
        {
            Items.Add(new InventoryItem(this, i));
            Items[i].Container = new ImagePanel(mInventoryScroll, "SellInvItem");
            Items[i].Setup();

            Values.Add(new Label(Items[i].Container, "InventoryItemValue"));
            Values[i].Text = "";

            Items[i].Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

      
            var xPadding = Items[i].Container.Margin.Left + Items[i].Container.Margin.Right;
            var yPadding = Items[i].Container.Margin.Top + Items[i].Container.Margin.Bottom;

            Items[i].Container.SetPosition(
                i % (mInventoryScroll.Width / (Items[i].Container.Width + xPadding)) * (Items[i].Container.Width + xPadding) + xPadding,
                i / (mInventoryScroll.Width / (Items[i].Container.Width + xPadding)) * (Items[i].Container.Height + yPadding) + yPadding
            );
        }
    }

    public void SelectItem(InventoryItem itemSlot, int slotIndex)
    {
        var slot = Globals.Me.Inventory[slotIndex];
        if (slot?.ItemId == Guid.Empty)
        {
            PacketSender.SendChatMsg(Strings.Market.invalidItem, (byte)ChatMessageType.Error);
            return;
        }

        _selectedSlot = slotIndex;
        _selectedItemId = slot.ItemId;
        _waitingPriceForItemId = slot.ItemId;
        var item = ItemBase.Get(slot.ItemId);
        _confirmButton.Enable();
        _infoLabel.Text = Strings.Market.publish_colon + " " + item?.Name;

        GetSelectedItemId();
        UpdateSuggestedPrice(slot.ItemId);

        // Luego pide al servidor para actualizar si es necesario.
        PacketSender.SendRequestMarketInfo(slot.ItemId);
        // Cargar cantidad y precio del ítem
        _quantityInput.SetText(slot.Quantity.ToString(), false);
        if (MarketPriceCache.TryGet(slot.ItemId, out var avg, out var min, out var max))
        {
            _priceInput.SetText(avg.ToString(), false);
        }
        else
        {
            _priceInput.SetText(string.Empty, false);
        }
        // Calcular y mostrar el impuesto
        RefreshTax();
    }

    private void RefreshTax()
    {
        if (!int.TryParse(_priceInput.Text, out var unitPrice) || unitPrice <= 0) { _taxLabel.Text = Strings.Market.taxes_0; return; }
        int qty = (int)_quantityInput.Value;
        if (qty <= 0) { _taxLabel.Text = Strings.Market.taxes_0; return; }

        int tax = (int)Math.Ceiling(unitPrice * qty * 0.02f);
        _taxLabel.Text = Strings.Market.taxes_estimated.ToString(tax);
    }
    public void UpdateSuggestedPrice(Guid itemId)
    {
        if (!MarketPriceCache.TryGet(itemId, out int avg, out int min, out int max))
        {
            // Nada en cache aún, esperar.
            Log.Debug($"⏳ Esperando precio para {itemId}...");
            return;
        }

        _suggestedPriceLabel.SetText(Strings.Market.pricehint.ToString(avg));
        _suggestedRangeLabel.SetText(Strings.Market.pricerange.ToString(min, max));
        _suggestedPriceLabel.Show();
        _suggestedRangeLabel.Show();
        _suggestedPriceLabel.SetTextColor(Color.Orange, Label.ControlState.Normal);
        _suggestedRangeLabel.SetTextColor(Color.Orange, Label.ControlState.Normal);

        _waitingPriceForItemId = Guid.Empty;
        Log.Debug($"✅ Precio actualizado visualmente para {itemId}");
    }


    #endregion

    #region === Publish ===

    private void OnConfirmClicked(Base _, EventArgs __)
    {
        if (_selectedSlot < 0 || _selectedItemId == Guid.Empty)
        {
            ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.noItemSelected, Color.Red, ChatMessageType.Error));
            return;
        }

        if (!int.TryParse(_quantityInput.Text, out int qty) || qty <= 0)
        {
            ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.invalidQuantity, Color.Red, ChatMessageType.Error));
            return;
        }

        if (!int.TryParse(_priceInput.Text, out int price) || price <= 0)
        {
            ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.invalidPrice, Color.Red, ChatMessageType.Error));
            return;
        }

        // Sumar todas las cantidades de ese ítem
        var matchingSlots = Globals.Me.Inventory
            .Select((slot, index) => new { Slot = slot, Index = index })
            .Where(x => x.Slot != null && x.Slot.ItemId == _selectedItemId)
            .ToList();

        int totalQuantity = matchingSlots.Sum(x => x.Slot.Quantity);

        if (totalQuantity < qty)
        {
            ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Market.quantityExceeds, Color.Red, ChatMessageType.Error));
            return;
        }

        if (MarketPriceCache.TryGet(_selectedItemId, out var avg, out var min, out var max))
        {
            if (price < min || price > max)
            {
                ChatboxMsg.AddMessage(new ChatboxMsg(
                    string.Format(Strings.Market.priceOutOfRange.ToString(), min, max),
                    Color.Red,
                    ChatMessageType.Error
                ));
                return;
            }
        }

        // Tomar los ítems necesarios para alcanzar la cantidad
        int quantityToTake = qty;
        List<(int SlotIndex, int Amount)> slotsToUse = new();

        foreach (var entry in matchingSlots)
        {
            int takeFromThisSlot = Math.Min(entry.Slot.Quantity, quantityToTake);
            slotsToUse.Add((entry.Index, takeFromThisSlot));
            quantityToTake -= takeFromThisSlot;

            if (quantityToTake <= 0)
                break;
        }

        // Enviar al servidor (simplificado: usa solo el primer slot en PacketSender actual)
        var firstSlot = slotsToUse.First();
        var props = Globals.Me.Inventory[firstSlot.SlotIndex].ItemProperties ?? new Network.Packets.Server.ItemProperties();
        PacketSender.SendCreateMarketListing(_selectedItemId, qty, price, props, _autoSplitCheckbox.IsChecked);

        // Luego puedes ajustar para enviar info de múltiples slots si el servidor lo permite

        ResetSelection();
    }


    private void ResetSelection()
    {
        _selectedSlot = -1;
        _selectedItemId = Guid.Empty;
        _infoLabel.Text = Strings.Market.selectitem;
        _priceInput.SetText(string.Empty, false);
        _quantityInput.SetText(string.Empty, false);
        _confirmButton.Disable();
        _suggestedPriceLabel.SetText("");
        _suggestedRangeLabel.SetText("");
        _taxLabel.SetText("");
       InitItemContainer(); // refrescar grid inmediatamente para que el ítem desaparezca si el servidor ya lo descontó
        Update();
    }

    #endregion

    #region === Public helpers ===

    public void Show() => _window.Show();
    public void Hide() => _window.Hide();
    public void Close() => _window.Close();
    public bool IsVisible() => !_window.IsHidden;
    public Guid GetSelectedItemId() => _selectedItemId;

    #endregion
}

/// <summary>
/// Cache local de precios de mercado.
/// </summary>
public static class MarketPriceCache
{
    private static readonly Dictionary<Guid, (int Avg, int Min, int Max)> Cache = new();

    public static void Update(Guid itemId, int avg, int min, int max) => Cache[itemId] = (avg, min, max);

    public static bool TryGet(Guid itemId, out int avg, out int min, out int max)
    {
        if (Cache.TryGetValue(itemId, out var t))
        {
            (avg, min, max) = t;
            return true;
        }

        avg = min = max = 0;
        return false;
    }
 
}
