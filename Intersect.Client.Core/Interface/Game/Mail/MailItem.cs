using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control.EventArguments.InputSubmissionEvent;
using Intersect.Client.Framework.Gwen.DragDrop;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.Mail;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Mail;

public partial class MailItem : SlotItem
{
    // Ventana de envío (para quitar ítems con clic derecho)
    private readonly SendMailBoxWindow? _sendWindow;

    // Label para cantidad
    private  Label _quantityLabel;

    // Slot actual (ítem adjunto)
    public Item? CurrentSlot { get; private set; }
    public bool IsEmpty => CurrentSlot == null;

    public MailItem(SendMailBoxWindow sendWindow, Base parent, int index)
        : base(parent, nameof(MailItem), index, contextMenu: null)
    {
        _sendWindow = sendWindow;
        Initialize();
    }

    public MailItem(MailBoxWindow _, Base parent, int index)
        : base(parent, nameof(MailItem), index, contextMenu: null)
    {
        Initialize();
    }

    private void Initialize()
    {
        Icon.HoverEnter += Icon_HoverEnter;
        Icon.HoverLeave += Icon_HoverLeave;
        Icon.Clicked += Icon_Clicked;

        _quantityLabel = new Label(this, "Quantity")
        {
            Alignment = [Alignments.Bottom, Alignments.Right],
            BackgroundTemplateName = "quantity.png",
            FontName = "sourcesansproblack",
            FontSize = 8,
            Padding = new Padding(2),
            IsVisibleInParent = false
        };
        if (Globals.Me is { } player)
        {
            player.InventoryUpdated += PlayerOnInventoryUpdated;
        }
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    public void SetItem(Item? slot)
    {
        CurrentSlot = slot;
        Update();
    }

    public void ClearItem()
    {
        CurrentSlot = null;
        _reset();
    }

    private void Icon_HoverEnter(Base? sender, EventArgs? arguments)
    {
        if (CurrentSlot == null)
        {
            return;
        }

        if (!ItemDescriptor.TryGet(CurrentSlot.ItemId, out var descriptor))
        {
            return;
        }

        Interface.GameUi.ItemDescriptionWindow?.Show(
            descriptor,
            CurrentSlot.Quantity,
            CurrentSlot.ItemProperties
        );
    }

    private void Icon_HoverLeave(Base sender, EventArgs arguments)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }

    private void Icon_Clicked(Base sender, MouseButtonState arguments)
    {
        if (arguments.MouseButton == MouseButton.Right && _sendWindow != null)
        {
            _sendWindow.RemoveAttachment(this);
        }
    }
    private void PlayerOnInventoryUpdated(Player player, int slotIndex)
    {
        if (player != Globals.Me)
        {
            return;
        }

        if (slotIndex != SlotIndex)
        {
            return;
        }

        if (Globals.Me.Inventory[SlotIndex] == default)
        {
            return;
        }

        // empty texture to reload on update
        Icon.Texture = default;
    }
    public override void Update()
    {
        if (CurrentSlot == null)
        {
            _reset();
            return;
        }

        if (!ItemDescriptor.TryGet(CurrentSlot.ItemId, out var descriptor))
        {
            _reset();
            return;
        }

        // Actualizamos ícono si cambió
        if (Icon.TextureFilename != descriptor.Icon)
        {
            var itemTexture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
            if (itemTexture != null)
            {
                Icon.Texture = itemTexture;
                Icon.RenderColor = descriptor.Color;
                Icon.IsVisibleInParent = true;
                Icon.TextureFilename = descriptor.Icon;
            }
            else
            {
                _reset();
                return;
            }
        }

        // Mostrar cantidad si es stackeable
        _quantityLabel.IsVisibleInParent = descriptor.IsStackable && CurrentSlot.Quantity > 1;
        if (_quantityLabel.IsVisibleInParent)
        {
            _quantityLabel.Text = Strings.FormatQuantityAbbreviated(CurrentSlot.Quantity);
        }
    }

    private void _reset()
    {
        Icon.IsVisibleInParent = false;
        Icon.Texture = null;
        Icon.TextureFilename = string.Empty;
        _quantityLabel.IsVisibleInParent = false;
    }

    #region Drag and Drop

   public void RequestQuantitySelection(Item inventorySlot, int inventoryIndex)
{
    new InputBox(
        title: Strings.MailBox.selectQuantity,
        prompt: Strings.MailBox.enterQuantity,
        inputType: InputType.NumericInput,
        onSubmit: (sender, args) =>
        {
            var qty = (int)((NumericalSubmissionValue)args.Value).Value;
            if (qty <= 0) qty = 1;
            if (qty > inventorySlot.Quantity) qty = inventorySlot.Quantity;

            FinalizeAttachment(new Item
            {
                ItemId = inventorySlot.ItemId,
                Quantity = qty,
                ItemProperties = inventorySlot.ItemProperties
            }, inventoryIndex);
        },
        onCancel: null,
        userData: null,
        quantity: inventorySlot.Quantity,
        maximumQuantity: inventorySlot.Quantity,
        minimumQuantity: 1
    );
}

public void FinalizeAttachment(Item item, int inventoryIndex)
{
    SetItem(item);

    SendMailBoxWindow.Instance.AddAttachment(item.ItemId, item.Quantity, item.ItemProperties);

    // Ajustar inventario
    var inventorySlot = Globals.Me.Inventory[inventoryIndex];
    inventorySlot.Quantity -= item.Quantity;
    if (inventorySlot.Quantity <= 0)
    {
        Globals.Me.Inventory[inventoryIndex] = null;
    }
 
}

    #endregion

}
