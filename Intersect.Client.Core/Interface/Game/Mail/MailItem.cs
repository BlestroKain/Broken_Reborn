using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Mail;
using Intersect.Client.Items;
namespace Intersect.Client.Interface.Game.Mail
{
    public class MailItem
    {
        public ImagePanel SlotPanel;
        private ItemDescriptionWindow DescWindow;

        public Item CurrentSlot; // Ranura actual asociada al ítem
        private int SlotIndex; // Índice del slot en la ventana de correos
        private MailBoxWindow mMailWindow;
        private SendMailBoxWindow mSendMailWindow;
        private MailBoxWindow mailBoxWindow;
        private int i;
        private ScrollControl mAttachmentContainer;

        public bool IsEmpty => CurrentSlot == null;

        public MailItem(SendMailBoxWindow mailWindow, int index, ScrollControl parent)
        {
            mSendMailWindow = mailWindow;
            SlotIndex = index;

            SlotPanel = new ImagePanel(parent, $"MailSlot{index}");
            SlotPanel.SetBounds(20 + (index * 60), 180, 32, 32);

            SlotPanel.HoverEnter += Pnl_HoverEnter;
            SlotPanel.HoverLeave += Pnl_HoverLeave;
            SlotPanel.RightClicked += Pnl_RightClicked;
            SlotPanel.Clicked += Pnl_LeftClicked; // Fix for CS1061
        }

        public MailItem(MailBoxWindow parent, int index, ScrollControl container)
        {
            mMailWindow = parent;
            SlotIndex = index;
            SlotPanel = new ImagePanel(container, $"AttachmentSlot{index}");
            SlotPanel.SetBounds(20 + (index * 60), 180, 32, 32);
            SlotPanel.HoverEnter += Pnl_HoverEnter;
            SlotPanel.HoverLeave += Pnl_HoverLeave;
            SlotPanel.RightClicked += Pnl_RightClicked;
        }

        public void SetItem(Item slot)
        {
            CurrentSlot = slot;

            if (slot != null)
            {
                var itemBase = ItemBase.Get(slot.ItemId);
                if (itemBase != null)
                {
                    var texture = Globals.ContentManager.GetTexture(Intersect.Client.Framework.Content.TextureType.Item, itemBase.Icon);
                    SlotPanel.Texture = texture ?? null;
                }
            }


            else
            {
                ClearItem();
            }
        }

        public void ClearItem()
        {
            CurrentSlot = null;
            SlotPanel.Texture = null;
        }

        private void Pnl_HoverEnter(Base sender, EventArgs arguments)
        {
            if (CurrentSlot != null && ItemBase.Get(CurrentSlot.ItemId) != null)
            {
                DescWindow = new ItemDescriptionWindow(
                    ItemBase.Get(CurrentSlot.ItemId),
                    CurrentSlot.Quantity,
                    SlotPanel.X,
                    SlotPanel.Y,
                    CurrentSlot.ItemProperties
                );
            }
        }

        private void Pnl_HoverLeave(Base sender, EventArgs arguments)
        {
            if (DescWindow != null)
            {
                DescWindow.Dispose();
                DescWindow = null;
            }
        }

        private void Pnl_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            
        }

        private void Pnl_LeftClicked(Base sender, ClickedEventArgs arguments) // Fix for CS0103
        {
         
        }
    }
}