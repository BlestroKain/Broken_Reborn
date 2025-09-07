using System;
using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Mail;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Interface.Game.Mail
{
    public partial class SendMailBoxWindow : Window
    {
        // UI principal
        private Label _toLabel, _titleLabel, _messageLabel, _attachmentLabel;
        private TextBox _toTextbox, _titleTextbox, _messageTextbox;
        private ScrollControl _attachmentContainer;
        private Button _sendButton, _closeButton;

        // Slots de adjuntos
        private List<MailItem> _attachmentSlots;
        public static SendMailBoxWindow Instance { get; private set; }

        // Estado
        private readonly List<MailAttachmentPacket> _attachments = new();
        // keep track of which slot each attachment came from so we can
        // correctly remove duplicates
        private readonly Dictionary<MailItem, MailAttachmentPacket> _mailItemAttachments = new();

        public SendMailBoxWindow(Canvas gameCanvas)
            : base(gameCanvas, Strings.MailBox.sendtitle, false, nameof(SendMailBoxWindow))
        {
            IsResizable = false;
            Interface.HasInputFocus();
            Instance = this;
            InitUI();
            Interface.FocusComponents.Add(_messageTextbox);
            Interface.FocusComponents.Add(_toTextbox);
            Interface.FocusComponents.Add(_titleTextbox);
            SetBounds(100, 100, 480, 300); // Ajustamos tamaño compacto
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        protected override void EnsureInitialized()
        {
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitAttachmentSlots();
        }

        #region Inicialización de UI
        private void InitUI()
        {
            const string defaultFont = "sourcesansproblack";
            const int fontSize = 12;
            var textColor = Color.FromArgb(255, 255, 255, 255); // Blanco

            // **To**
            _toLabel = new Label(this, "ToLabel")
            {
                Text = "To:",
                FontName = defaultFont,
                FontSize = fontSize
            };
            _toLabel.SetBounds(20, 20, 60, 20);
            _toLabel.SetTextColor(textColor, ComponentState.Normal);

            _toTextbox = new TextBox(this, "ToTextbox")
            {
                FontName = defaultFont,
                FontSize = fontSize,
                TextColor = textColor
            };
            _toTextbox.SetBounds(90, 20, 280, 25);
          
            // **Title**
            _titleLabel = new Label(this, "TitleLabel")
            {
                Text = "🏷 Title:",
                FontName = defaultFont,
                FontSize = fontSize
            };
            _titleLabel.SetBounds(20, 60, 60, 20);
            _titleLabel.SetTextColor(textColor, ComponentState.Normal);

            _titleTextbox = new TextBox(this, "TitleTextbox")
            {
                FontName = defaultFont,
                FontSize = fontSize,
                TextColor = textColor
            };
            _titleTextbox.SetBounds(90, 60, 280, 25);
            _titleTextbox.SetMaxLength(50);

            // **Message**
            _messageLabel = new Label(this, "MessageLabel")
            {
                Text = "📝 Message:",
                FontName = defaultFont,
                FontSize = fontSize
            };
            _messageLabel.SetBounds(20, 100, 100, 20);
            _messageLabel.SetTextColor(textColor, ComponentState.Normal);

            _messageTextbox = new TextBox(this, "MessageTextbox")
            {
                FontName = defaultFont,
                FontSize = fontSize,
                TextColor = textColor,
                
            };
            _messageTextbox.SetBounds(90, 100, 280, 60);
            _messageTextbox.SetMaxLength(255);

            // **Attachments**
            _attachmentLabel = new Label(this, "AttachmentLabel")
            {
                Text = "Attachments:",
                FontName = defaultFont,
                FontSize = fontSize
            };
            _attachmentLabel.SetBounds(20, 170, 150, 20);
            _attachmentLabel.SetTextColor(textColor, ComponentState.Normal);

            _attachmentContainer = new ScrollControl(this, "AttachmentContainer");
            _attachmentContainer.SetBounds(20, 195, 420, 50);
            _attachmentContainer.EnableScroll(false, false);

            // **Botones**
            _sendButton = new Button(this, "SendButton")
            {
                Text = "Send",
                FontName = defaultFont,
                FontSize = fontSize
            };
            _sendButton.SetBounds(150, 260, 80, 30);
            _sendButton.Clicked += OnSendButtonClicked;

            _closeButton = new Button(this, "CloseButton")
            {
                Text = "Close",
                FontName = defaultFont,
                FontSize = fontSize
            };
            _closeButton.SetBounds(250, 260, 80, 30);
            _closeButton.Clicked += (_, _) => Close();       
        }
        #endregion

        #region Slots de adjuntos
        private void InitAttachmentSlots()
        {
            _attachmentSlots = new List<MailItem>();
            for (int i = 0; i < 5; i++)
            {
                var mailSlot = new MailItem(this, _attachmentContainer, i);
                _attachmentSlots.Add(mailSlot);
                mailSlot.SetPosition(i * (mailSlot.Width + 5), 0);
                mailSlot.Show(); // Siempre visible (Drag & Drop listo)
            }
        }
        #endregion

        #region Lógica principal
        private void OnSendButtonClicked(Base sender, MouseButtonState arguments)
        {
            if (string.IsNullOrWhiteSpace(_toTextbox.Text))
            {
                PacketSender.SendChatMsg("Enter recipient.", 5);
                return;
            }

            if (string.IsNullOrWhiteSpace(_titleTextbox.Text))
            {
                PacketSender.SendChatMsg("Enter title.", 5);
                return;
            }

            if (_attachments.Count == 0)
            {
                PacketSender.SendChatMsg("Attach at least one item.", 5);
                return;
            }

            PacketSender.SendMail(_toTextbox.Text, _titleTextbox.Text, _messageTextbox.Text, _attachments);
            Close();
        }

        public void AddAttachment(MailItem slot, Guid itemId, int quantity, ItemProperties properties)
        {
            var attachment = new MailAttachmentPacket
            {
                ItemId = itemId,
                Quantity = quantity,
                Properties = properties
            };

            _attachments.Add(attachment);
            _mailItemAttachments[slot] = attachment;
        }

        public void RemoveAttachment(MailItem mailSlot)
        {
            if (mailSlot.CurrentSlot == null)
                return;

            if (_mailItemAttachments.TryGetValue(mailSlot, out var attachment))
            {
                _attachments.Remove(attachment);
                _mailItemAttachments.Remove(mailSlot);
            }
            else
            {
                // Fallback in case mapping is lost
                var att = _attachments.Find(a =>
                    a.ItemId == mailSlot.CurrentSlot.ItemId &&
                    a.Quantity == mailSlot.CurrentSlot.Quantity);
                if (att != null)
                {
                    _attachments.Remove(att);
                }
            }

            mailSlot.ClearItem();
        }
        #endregion

        public void Close()
        {

            _attachments.Clear();
            _mailItemAttachments.Clear();
            base.Close();
            Instance = null;
        }

    }
}
