using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Mail;
using Intersect.Client.Items;
namespace Intersect.Client.Interface.Game.Mail;

public partial class MailItem : SlotItem
{
    private ItemDescriptionWindow? _descWindow;
    private readonly SendMailBoxWindow? _sendWindow;

    public Item? CurrentSlot { get; private set; }

    public bool IsEmpty => CurrentSlot == null;

    public MailItem(SendMailBoxWindow sendWindow, Base parent, int index)
        : base(parent, nameof(MailItem), index, contextMenu: null)
    {
        _sendWindow = sendWindow;
        Setup();
    }

    public MailItem(MailBoxWindow _, Base parent, int index)
        : base(parent, nameof(MailItem), index, contextMenu: null)
    {
        Setup();
    }

    private void Setup()
    {
        Icon.HoverEnter += Icon_HoverEnter;
        Icon.HoverLeave += Icon_HoverLeave;
        Icon.Clicked += Icon_Clicked;

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    public void SetItem(Item slot)
    {
        CurrentSlot = slot;

        if (slot != null)
        {
            var itemBase = ItemBase.Get(slot.ItemId);
            if (itemBase != null)
            {
                var texture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, itemBase.Icon);
                Icon.Texture = texture;
                return;
            }
        }

        ClearItem();
    }

    public void ClearItem()
    {
        CurrentSlot = null;
        Icon.Texture = null;
    }

    private void Icon_HoverEnter(Base sender, EventArgs arguments)
    {
        if (CurrentSlot == null)
        {
            return;
        }

        var itemBase = ItemBase.Get(CurrentSlot.ItemId);
        if (itemBase == null)
        {
            return;
        }

        _descWindow = new ItemDescriptionWindow(
            itemBase,
            CurrentSlot.Quantity,
            Icon.X,
            Icon.Y,
            CurrentSlot.ItemProperties
        );
    }

    private void Icon_HoverLeave(Base sender, EventArgs arguments)
    {
        _descWindow?.Dispose();
        _descWindow = null;
    }

    private void Icon_Clicked(Base sender, MouseButtonState arguments)
    {
        if (arguments.MouseButton is MouseButton.Right && _sendWindow != null)
        {
            _sendWindow.RemoveAttachment(this);
        }
    }
}