using System;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Mail
{
    public class SendMailBoxWindow
    {
        private WindowControl mSendMailBoxWindow;

        private Label mTo;
        private TextBox mToTextbox;
        private Label mTitle;
        private TextBox mTitleTextbox;
        private Label mMessage;
        private TextBox mMsgTextbox;

        private Label mItem;
        private ComboBox mItemComboBox;
        private MailItem mSendItem;
        private Label mQuantity;
        private TextBoxNumeric mQuantityTextBoxNumeric;

        private Button mSendButton;
        private Button mCloseButton;


        public SendMailBoxWindow(Canvas gameCanvas)
        {
            mSendMailBoxWindow = new WindowControl(gameCanvas, Strings.MailBox.sendtitle, false, "SendMailBoxWindow");
            Interface.InputBlockingElements.Add(mSendMailBoxWindow);

            mTo = new Label(mSendMailBoxWindow, "To")
            {
                Text = Strings.MailBox.mailto
            };
            mToTextbox = new TextBox(mSendMailBoxWindow, "ToTextbox");
            Interface.FocusElements.Add(mToTextbox);

            mTitle = new Label(mSendMailBoxWindow, "Title")
            {
                Text = Strings.MailBox.mailtitle
            };
            mTitleTextbox = new TextBox(mSendMailBoxWindow, "TitleTextbox");
            mTitleTextbox.SetMaxLength(20);
            Interface.FocusElements.Add(mTitleTextbox);

            mMessage = new Label(mSendMailBoxWindow, "Message")
            {
                Text = Strings.MailBox.mailmsg
            };
            mMsgTextbox = new TextBox(mSendMailBoxWindow, "MsgTextbox");
            mMsgTextbox.SetMaxLength(255);
            Interface.FocusElements.Add(mMsgTextbox);

            mItem = new Label(mSendMailBoxWindow, "Item")
            {
                Text = Strings.MailBox.mailitem
            };

            mItemComboBox = new ComboBox(mSendMailBoxWindow, "ItemComboBox");
            mItemComboBox.ItemSelected += Item_SelectedComboBox;
            Interface.FocusElements.Add(mItemComboBox);

            mSendItem = new MailItem(this, -1, new ImagePanel(mSendMailBoxWindow, "MailItem"));

            mQuantity = new Label(mSendMailBoxWindow, "Quantity")
            {
                Text = Strings.MailBox.mailquantity
            };
            mQuantity.IsHidden = true;
            mQuantityTextBoxNumeric = new TextBoxNumeric(mSendMailBoxWindow, "QuantityTextBoxNumeric");
            mQuantityTextBoxNumeric.IsHidden = true;
            mQuantityTextBoxNumeric.TextChanged += Quantity_ChangeTextBoxNumeric;
            Interface.FocusElements.Add(mQuantityTextBoxNumeric);


            mSendButton = new Button(mSendMailBoxWindow, "SendButton");
            mSendButton.SetText(Strings.MailBox.send);
            mSendButton.Clicked += SendButton_Clicked;

            mCloseButton = new Button(mSendMailBoxWindow, "CloseButton");
            mCloseButton.SetText(Strings.MailBox.close);
            mCloseButton.Clicked += CloseButton_Clicked;
            // Posiciones para los controles de texto
            mTo.SetPosition(50, 50);
            mToTextbox.SetPosition(100, 50);

            mTitle.SetPosition(50, 100);
            mTitleTextbox.SetPosition(100, 100);

            mMessage.SetPosition(50, 150);
            mMsgTextbox.SetPosition(100, 150);

            // Posiciones para los controles de MailItem
            mItem.SetPosition(50, 200);
            

            // Posiciones para los controles de cantidad
            mQuantity.SetPosition(50, 250);
            mQuantityTextBoxNumeric.SetPosition(100, 250);

            // Posiciones para los botones
            mSendButton.SetPosition(50, 300);
            mCloseButton.SetPosition(150, 300);
            mSendMailBoxWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            
            mSendMailBoxWindow.SetPosition(
                Graphics.Renderer.GetScreenWidth() / 2 - mSendMailBoxWindow.Width / 2,
                Graphics.Renderer.GetScreenHeight() / 2 - mSendMailBoxWindow.Height / 2
            );
            
            mSendMailBoxWindow.DisableResizing();
        }

        public SendMailBoxWindow()
        {
        }

        public int X { get => mSendMailBoxWindow.X; }
        public int Y { get => mSendMailBoxWindow.Y; }

        public int GetQuantity()
        {
            return (int)mQuantityTextBoxNumeric.Value;
        }

        public void UpdateItemListBySlot(int index)
        {
            mItemComboBox.SelectByUserData(index);
            mSendItem.SetSlot(index);
            int invSlot = index;
            if (invSlot < 0)
            {
                mQuantityTextBoxNumeric.Value = 0;
                mQuantityTextBoxNumeric.IsHidden = true;
                mQuantity.IsHidden = true;
            }
            else
            {
                Guid itemID = Globals.Me.Inventory[invSlot].ItemId;
                if (itemID == Guid.Empty)
                {
                    mQuantityTextBoxNumeric.Value = 0;
                    mQuantityTextBoxNumeric.IsHidden = true;
                    mQuantity.IsHidden = true;
                }
                else
                {
                    var ibase = ItemBase.Get(itemID);
                    if (ibase != null)
                    {
                        mQuantityTextBoxNumeric.Value = 1;
                        mQuantityTextBoxNumeric.IsHidden = !ibase.IsStackable;
                        mQuantity.IsHidden = !ibase.IsStackable;
                    }
                }
            }
        }

        public void UpdateItemList()
        {
        mItemComboBox.DeleteAll();
        mItemComboBox.AddItem(Strings.MailBox.itemnone, "", -1);
        for (int i = 0; i < Globals.Me.Inventory.Length; i++)
        {
        	Items.Item item = (Items.Item)Globals.Me.Inventory[i];
        	if (item.ItemId != Guid.Empty)
        	{
        		mItemComboBox.AddItem(item.Base.Name, "", i);
        	}
        }
        }

        private void Quantity_ChangeTextBoxNumeric(Base sender, EventArgs e)
        {
            var item = mItemComboBox.SelectedItem;
            int invSlot =(int)(item.UserData);
                                               if (invSlot >= 0)
                                               {
                                               	mQuantity.Text = $"{Strings.MailBox.mailquantity}: {0}";
                                               	return;
                                               }
                                              Guid itemID = Globals.Me.Inventory[invSlot].ItemId;
            int val = UpdateQuantity((int)mQuantityTextBoxNumeric.Value);
            mQuantity.Text = $"{Strings.MailBox.mailquantity}: {val}";
        }

        private void Item_SelectedComboBox(Base sender, ItemSelectedEventArgs e)
        {
        	var item = mItemComboBox.SelectedItem;
        	int invSlot = (int)(item.UserData);
        	if (invSlot < 0)
        	{
        		mQuantityTextBoxNumeric.Value = 0;
        		mQuantityTextBoxNumeric.IsHidden = true;
        		mQuantity.IsHidden = true;
        	}
        	else
        	{
        		Guid itemID = Globals.Me.Inventory[invSlot].ItemId;
        		if (itemID == Guid.Empty)
        		{
        			mQuantityTextBoxNumeric.Value = 0;
        			mQuantityTextBoxNumeric.IsHidden = true;
        			mQuantity.IsHidden = true;
        		}
        		else
        		{
        			var ibase = ItemBase.Get(itemID);
        			if (ibase != null)
        			{
        				mQuantityTextBoxNumeric.Value = 1;
        				mQuantityTextBoxNumeric.IsHidden = !ibase.IsStackable;
        				mQuantity.IsHidden = !ibase.IsStackable;
        			}
        		}
        	}
        }

        private int UpdateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                quantity = 0;
            }
            if (mSendItem.GetSlot() >= 0)
            {
                Items.Item item = (Items.Item)Globals.Me.Inventory[mSendItem.GetSlot()];
                if (item == null || item.ItemId == Guid.Empty)
                {
                    return 0;
                }
                if (quantity > item.Quantity)
                {
                    quantity = item.Quantity;
                }
                if (item.Base.IsStackable == false)
                {
                    return 1;
                }
                return quantity;
            }
            return 0;
        }

        void SendButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mToTextbox.Text.Trim().Length <= 3 || mTitleTextbox.Text.Trim().Length <= 3)
            {
                return;
            }
            var item = mItemComboBox.SelectedItem;
            int invSlot = (int)(item.UserData);
            Guid itemID = Globals.Me.Inventory[invSlot].ItemId;
            int quantity = 0;
            if (itemID != Guid.Empty)
            {
                quantity = UpdateQuantity((int)mQuantityTextBoxNumeric.Value);
                if (quantity == 0)
                {
                    itemID = Guid.Empty;
                }
            }
            PacketSender.SendMail(mToTextbox.Text, mTitleTextbox.Text, mMsgTextbox.Text, invSlot, quantity);
        }

        void CloseButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendCloseMail();
        }

        public void Close()
        {
            mSendMailBoxWindow.Close();
        }

        public bool IsVisible()
        {
            return !mSendMailBoxWindow.IsHidden;
        }

        public void Hide()
        {
            mSendMailBoxWindow.IsHidden = true;
        }
    }
}