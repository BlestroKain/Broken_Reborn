using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.Client;
using Intersect.Client.Interface.Game.Mail;

namespace Intersect.Client.Interface.Game.Mail
{
    public class MailBoxWindow
    {
        private WindowControl mMailBoxWindow;

        private Label mMail;
        private ListBox mMailListBox;


        private Label mSender;
        private Label mTitle;
        private RichLabel mMessage;
        private Label mItem;
        private Button mSendMail;
        private Button mCloseButton;

        private Button mTakeButton;

        public MailBoxWindow(Canvas gameCanvas)
        {
            mMailBoxWindow = new WindowControl(gameCanvas, Strings.MailBox.title, false, "MailBoxWindow");

            Interface.InputBlockingElements.Add(mMailBoxWindow);

            mMail = new Label(mMailBoxWindow, "Mail")
            {
                Text = Strings.MailBox.mails
            };

            // Establecer la ubicación para el ListBox
            mMailListBox = new ListBox(mMailBoxWindow, "MailListBox");
            mMailListBox.EnableScroll(false, true);
            mMailListBox.RowSelected += Selected_MailListBox;
            mMailListBox.AllowMultiSelect = false;
            mMailListBox.SetPosition(x: 10, y: 10); // Ejemplo: X = 10, Y = 10 (cambia las coordenadas según tus necesidades)

            // Establecer la ubicación para el Label "Sender"
            mSender = new Label(mMailBoxWindow, "Sender");
            mSender.Hide();
            mSender.SetPosition(x: 20, y: 50); // Ejemplo: X = 20, Y = 50 (cambia las coordenadas según tus necesidades)

            // Establecer la ubicación para el Label "Title"
            mTitle = new Label(mMailBoxWindow, "Title");
            mTitle.Hide();
            mTitle.SetPosition(x: 20, y: 70); // Ejemplo: X = 20, Y = 70 (cambia las coordenadas según tus necesidades)

            // Establecer la ubicación para el RichLabel "Message"
            mMessage = new RichLabel(mMailBoxWindow, "Message");
            mMessage.Hide();
            mMessage.SetPosition(x: 20, y: 100); // Ejemplo: X = 20, Y = 100 (cambia las coordenadas según tus necesidades)

            // Establecer la ubicación para el Label "Item"
            mItem = new Label(mMailBoxWindow, "Item");
            mItem.Hide();
            mItem.SetPosition(x: 20, y: 250); // Ejemplo: X = 20, Y = 250 (cambia las coordenadas según tus necesidades)

            // Establecer la ubicación para el Button "TakeButton"
            mTakeButton = new Button(mMailBoxWindow, "TakeButton");
            mTakeButton.SetText(Strings.MailBox.take);
            mTakeButton.Clicked += Take_Clicked;
            mTakeButton.Hide();
            mTakeButton.SetPosition(x: 150, y: 250); // Ejemplo: X = 150, Y = 250 (cambia las coordenadas según tus necesidades)
           
            mSendMail = new Button(mMailBoxWindow, "SendMailButton");
            mSendMail.SetText("Send Mail");
            mSendMail.Clicked += SendMail_Clicked;
            mSendMail.SetPosition(x: 20, y: 300); // Ejemplo: X = 20, Y = 300 (cambia las coordenadas según tus necesidades)

            // Establecer la ubicación para el Button "CloseButton"
            mCloseButton = new Button(mMailBoxWindow, "CloseButton");
            mCloseButton.SetText(Strings.MailBox.close);
            mCloseButton.Clicked += CloseButton_Clicked;
            mCloseButton.Hide();
            mCloseButton.SetPosition(x: 300, y: 250); // Ejemplo: X = 300, Y = 250 (cambia las coordenadas según tus necesidades)

            mMailBoxWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            
            mMailBoxWindow.SetPosition(
                Graphics.Renderer.GetScreenWidth() / 2 - mMailBoxWindow.Width / 2,
                Graphics.Renderer.GetScreenHeight() / 2 - mMailBoxWindow.Height / 2
            );
            
            mMailBoxWindow.DisableResizing();
        }

        private void SendMail_Clicked(Base sender, ClickedEventArgs arguments)
        {
            SendMailBoxWindow sendMailWindow = new SendMailBoxWindow();

            // Mostrar la ventana
            sendMailWindow.IsVisible();
        }

        private void Take_Clicked(Base sender, ClickedEventArgs e)
        {
            var selected = mMailListBox.SelectedRow;
            var mail = selected.UserData as Client.Mail;
            PacketSender.SendTakeMail(mail.MailID);
        }

        private void Selected_MailListBox(Base sender, ItemSelectedEventArgs e)
        {
            if (Globals.Mails.Count > 0)
            {
                var selected = mMailListBox.SelectedRow;
                var mail = selected.UserData as Client.Mail;
                mSender.Text = $"{Strings.MailBox.sender}: {mail.SenderName}";
                mTitle.Text = $"{Strings.MailBox.mailtitle}: {mail.Name}";
                mMessage.ClearText();
                mMessage.AddText($"{mail.Message}", Color.White);
                mSender.Show();
                mTitle.Show();
                mMessage.Show();
                if (mail.Item != Guid.Empty)
                {
                    var item = ItemBase.Get(mail.Item);
                    if (item.IsStackable)
                    {
                        mItem.Text = $"{Strings.MailBox.mailitem}: [{mail.Quantity}] {item.Name}";
                    }
                    else
                    {
                        mItem.Text = $"{Strings.MailBox.mailitem}: {item.Name}";
                    }
                    mItem.Show();
                }
                else
                {
                    mItem.Hide();
                }
                mTakeButton.Show();
            }
            else
            {
                mSender.Hide();
                mTitle.Hide();
                mMessage.Hide();
                mItem.Hide();
                mTakeButton.Hide();
            }
        }

        void CloseButton_Clicked(Base sender, ClickedEventArgs e)
        {
            PacketSender.SendCloseMail();
        }

        public void UpdateMail()
        {
            mMailListBox.RemoveAllRows();
            mMailListBox.ScrollToTop();
            foreach (Client.Mail mail in Globals.Mails)
            {
                var row = mMailListBox.AddRow(mail.Name.Trim(), "", mail);
                row.SetTextColor(Color.White);
            }
            if (Globals.Mails.Count > 0)
            {
                mMailListBox.SelectByUserData(Globals.Mails[0]);
            }
            else
            {
                mSender.Hide();
                mTitle.Hide();
                mMessage.Hide();
                mItem.Hide();
                mTakeButton.Hide();
            }
        }

        public void Close()
        {
            mMailBoxWindow.Close();
        }

        public bool IsVisible()
        {
            return !mMailBoxWindow.IsHidden;
        }

        public void Hide()
        {
            mMailBoxWindow.IsHidden = true;
        }

        internal void Show()
        {
            mMailBoxWindow.Show();
        }
    }
}