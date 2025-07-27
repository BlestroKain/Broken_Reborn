using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Mail
{
    public class MailBoxWindow
    {
        private WindowControl mMailBoxWindow;

        private Label mMailLabel;
        private ListBox mMailListBox;

        private Label mSender;
        private Label mTitle;
        private RichLabel mMessage;

        private Label mAttachmentLabel;
        private ScrollControl mAttachmentContainer;
        private List<MailItem> mAttachmentSlots;

        private Button mTakeButton;
        private Button mSendMailButton;
        private Button mCloseButton;

        public MailBoxWindow(Canvas gameCanvas)
        {
            mMailBoxWindow = new WindowControl(gameCanvas, Strings.MailBox.title, false, "MailBoxWindow");
            mMailBoxWindow.SetSize(700, 500);
            Interface.InputBlockingElements.Add(mMailBoxWindow);

            // 📩 Panel Izquierdo: Lista de Correos
            mMailLabel = new Label(mMailBoxWindow, "Mail") { Text = Strings.MailBox.mails };
            mMailLabel.SetBounds(20, 10, 200, 20);

            mMailListBox = new ListBox(mMailBoxWindow, "MailListBox");
            mMailListBox.SetBounds(20, 40, 250, 400);
            mMailListBox.EnableScroll(false, true);
            mMailListBox.RowSelected += Selected_MailListBox;
            mMailListBox.AllowMultiSelect = false;
                // Por la siguiente línea correcta
            mMailListBox.TextColor = Color.White;
            // 📨 Panel Derecho: Detalles del Correo
            mSender = new Label(mMailBoxWindow, "Sender");
            mSender.SetBounds(300, 40, 350, 20);
            mSender.Hide();

            mTitle = new Label(mMailBoxWindow, "Title");
            mTitle.SetBounds(300, 70, 350, 20);
            mTitle.Hide();

            mMessage = new RichLabel(mMailBoxWindow, "Message");
            mMessage.SetBounds(300, 100, 350, 150);
            mMessage.Hide();

            // 🎁 Adjuntos (Debajo del Mensaje)
            mAttachmentLabel = new Label(mMailBoxWindow, "Attachments") { Text = "📦 Attachments" };
            mAttachmentLabel.SetBounds(300, 270, 350, 20);
            mAttachmentLabel.Hide();

            mAttachmentContainer = new ScrollControl(mMailBoxWindow, "AttachmentContainer");
            mAttachmentContainer.SetBounds(300, 300, 250, 40);
            mAttachmentContainer.EnableScroll(false, true);
            mMailBoxWindow.AddChild(mAttachmentContainer);
            InitializeAttachmentSlots();

            // Botones de Acción
            InitButtons();

            mMailBoxWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            mMailBoxWindow.SetPosition(Graphics.Renderer.GetScreenWidth() / 2 - mMailBoxWindow.Width / 2,
                                       Graphics.Renderer.GetScreenHeight() / 2 - mMailBoxWindow.Height / 2);
            mMailBoxWindow.DisableResizing();
        }
        private void InitializeAttachmentSlots()
        {
            if (mAttachmentSlots == null)
            {
                mAttachmentSlots = new List<MailItem>();
            }

            for (int i = 0; i < 5; i++) // Máximo de 5 slots
            {
                var mailSlot = new MailItem(this, i, mAttachmentContainer);
                mAttachmentSlots.Add(mailSlot);

                var xPadding = 3;
                var yPadding = 3;

                mailSlot.SlotPanel.SetPosition(
                    i % (mAttachmentContainer.Width / (mailSlot.SlotPanel.Width + xPadding)) * (mailSlot.SlotPanel.Width + xPadding) + xPadding,
                    i / (mAttachmentContainer.Width / (mailSlot.SlotPanel.Width + xPadding)) * (mailSlot.SlotPanel.Height + yPadding) + yPadding
                );
            }
        }

        private void InitButtons()
        {
            // 📤 Enviar Correo
            mSendMailButton = new Button(mMailBoxWindow, "SendMailButton");
            mSendMailButton.SetText("📤 Send Mail");
            mSendMailButton.SetBounds(20, 460, 120, 30);
            mSendMailButton.Clicked += SendMail_Clicked;

            // 🎁 Tomar Adjuntos
            mTakeButton = new Button(mMailBoxWindow, "TakeButton");
            mTakeButton.SetText(Strings.MailBox.take);
            mTakeButton.SetBounds(300, 460, 150, 30);
            mTakeButton.Clicked += Take_Clicked;
            mTakeButton.Hide();

            // ❌ Cerrar Ventana
            mCloseButton = new Button(mMailBoxWindow, "CloseButton");
            mCloseButton.SetText("❌ Close");
            mCloseButton.SetBounds(500, 460, 150, 30);
            mCloseButton.Clicked += CloseButton_Clicked;
        }

        private void SendMail_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mMailBoxWindow.Parent is Canvas parentCanvas)
            {
                var sendMailWindow = new SendMailBoxWindow(parentCanvas);
                sendMailWindow.Show();
            }
        }

        private void Take_Clicked(Base sender, ClickedEventArgs e)
        {
            if (mMailListBox.SelectedRow?.UserData is Client.Mail mail)
            {
                PacketSender.SendTakeMail(mail.MailID);
            }
        }

        private void Selected_MailListBox(Base sender, ItemSelectedEventArgs e)
        {
            if (mMailListBox.SelectedRow?.UserData is Client.Mail mail)
            {
                string senderName = !string.IsNullOrWhiteSpace(mail.SenderName) ? mail.SenderName : "Unknown Sender";
                mSender.Text = $"{Strings.MailBox.sender}: {senderName}";

                mTitle.Text = $"{Strings.MailBox.mailtitle}: {mail.Name}";
                mMessage.ClearText();
                mMessage.AddText(mail.Message, Color.White);

                for (int i = 0; i < mAttachmentSlots.Count; i++)
                {
                    if (i < mail.Attachments.Count)
                    {
                        var attachment = mail.Attachments[i];
                        var item = new Item();

                        // Update the following line in the Selected_MailListBox method:  
                        item.Load(attachment.ItemId, attachment.Quantity, null, attachment.Properties);
                   
                        mAttachmentSlots[i].SetItem(item);
                        mAttachmentSlots[i].SlotPanel.Show();
                    }
                    else
                    {
                        mAttachmentSlots[i].ClearItem();
                        mAttachmentSlots[i].SlotPanel.Hide();
                    }
                }

                mSender.Show();
                mTitle.Show();
                mMessage.Show();
                mAttachmentLabel.Show();
                mTakeButton.Show();
            }
        }
       
      
        void CloseButton_Clicked(Base sender, ClickedEventArgs e)
        {
            mMailBoxWindow.Close();
            PacketSender.SendCloseMail();
        }

        public void UpdateMail()
        {
            // 🔍 Validar inicialización
            if (mMailListBox == null || mAttachmentSlots == null)
            {
                Console.WriteLine("⚠️ Componentes de MailBoxWindow no están inicializados.");
                return;
            }

            // 🔄 Limpiar lista de correos y contenedor de adjuntos
            mMailListBox.RemoveAllRows();
            mMailListBox.ScrollToTop();

            foreach (var slot in mAttachmentSlots)
            {
                slot.ClearItem();
                slot.SlotPanel.IsHidden = true;
            }

            // 🔍 Validar y actualizar correos
            if (Globals.Mails == null || Globals.Mails.Count == 0)
            {
                mSender?.Hide();
                mTitle?.Hide();
                mMessage?.Hide();
                mAttachmentLabel?.Hide();
                mTakeButton?.Hide();
                return;
            }

            foreach (var mail in Globals.Mails)
            {
                if (mail == null)
                {
                    Console.WriteLine("⚠️ Se encontró un correo nulo en Globals.Mails.");
                    continue;
                }

                // 📩 Verificar datos antes de agregar a la lista
                Console.WriteLine($"📩 Mail recibido - ID: {mail.MailID}, Remitente: {mail.SenderName}, Título: {mail.Name}");

                string senderName = !string.IsNullOrWhiteSpace(mail.SenderName) ? mail.SenderName : "Unknown Sender";
                string mailTitle = !string.IsNullOrWhiteSpace(mail.Name) ? mail.Name.Trim() : "No Subject";
                string displayText = $"📩 {senderName}: {mailTitle}";

                var row = mMailListBox.AddRow(displayText, "", mail);
                row.SetTextColor(Color.White);
            }


            // 🔹 Seleccionar el primer correo automáticamente
            mMailListBox.SelectByUserData(Globals.Mails[0]);
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

        public void Show()
        {
            mMailBoxWindow.Show();
        }
    }
}
