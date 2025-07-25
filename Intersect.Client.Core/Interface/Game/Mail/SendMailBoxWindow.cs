using System;
using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.Job;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Newtonsoft.Json.Linq;

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

        private Label mAttachmentLabel;
        private ScrollControl mAttachmentContainer;
        private List<MailItem> mAttachmentSlots;

        private Label mInventoryLabel;
    
        //Item List
        public List<InventoryItem> Items = new List<InventoryItem>();

        //Initialized Items?
        private bool mInitializedItems = false;



        private ScrollControl mItemContainer;

        private List<Label> mValues = new List<Label>();

        private Label mQuantityLabel;
        private TextBoxNumeric mQuantityTextBox;

        private Button mAddItemButton;
        private Button mSendButton;
        private Button mCloseButton;
        private List<MailAttachmentPacket> mAttachments;

        private InventoryItem mSelectedItem;
        private int mSelectedSlot;

        public int X { get;  set; }
        public int Y { get;  set; }

        public SendMailBoxWindow(Canvas gameCanvas)
        {
            mSendMailBoxWindow = new WindowControl(gameCanvas, Strings.MailBox.sendtitle, false, "SendMailBoxWindow");
            Interface.InputBlockingElements.Add(mSendMailBoxWindow);

            // Sección Izquierda: Información del Correo
            mTo = new Label(mSendMailBoxWindow, "To") { Text = Strings.MailBox.mailto };
            mTo.SetBounds(20, 20, 100, 20);

            mToTextbox = new TextBox(mSendMailBoxWindow, "ToTextbox");
            mToTextbox.SetBounds(130, 20, 200, 25);
            Interface.FocusElements.Add(mToTextbox);

            mTitle = new Label(mSendMailBoxWindow, "Title") { Text = Strings.MailBox.mailtitle };
            mTitle.SetBounds(20, 60, 100, 20);

            mTitleTextbox = new TextBox(mSendMailBoxWindow, "TitleTextbox");
            mTitleTextbox.SetBounds(130, 60, 200, 25);
            mTitleTextbox.SetMaxLength(50);

            mMessage = new Label(mSendMailBoxWindow, "Message") { Text = Strings.MailBox.mailmsg };
            mMessage.SetBounds(20, 100, 100, 20);

            mMsgTextbox = new TextBox(mSendMailBoxWindow, "MsgTextbox");
            mMsgTextbox.SetBounds(130, 100, 200, 80);
            mMsgTextbox.SetMaxLength(255);
      

            mAttachmentLabel = new Label(mSendMailBoxWindow, "Attachments") { Text = "📦 Attachments" };
            mAttachmentLabel.SetBounds(20, 200, 100, 20);

            mAttachmentContainer = new ScrollControl(mSendMailBoxWindow, "AttachmentContainer");
            mAttachmentContainer.SetBounds(20, 220, 360, 60);
            mAttachmentContainer.EnableScroll(false, true);
 
            InitializeAttachmentSlots();

            // Sección Derecha: Inventario
            mInventoryLabel = new Label(mSendMailBoxWindow, "Inventory") { Text = "🏷 Inventory" };
            mInventoryLabel.SetBounds(400, 20, 100, 20);
            mItemContainer = new ScrollControl(mSendMailBoxWindow, "ItemContainer");
            mItemContainer.SetBounds(400, 40, 300, 240);
            mItemContainer.EnableScroll(false, true);
            Interface.FocusElements.Add(mItemContainer);
            mSendMailBoxWindow.AddChild(mItemContainer); 
            mQuantityLabel = new Label(mSendMailBoxWindow, "QuantityLabel") { Text = "Quantity" };
            mQuantityLabel.SetBounds(400, 290, 100, 20);

            mQuantityTextBox = new TextBoxNumeric(mSendMailBoxWindow, "QuantityTextBox");
            mQuantityTextBox.SetBounds(500, 290, 100, 25);

            mAddItemButton = new Button(mSendMailBoxWindow, "AddItemButton");
            mAddItemButton.SetText("➕ Add Item");
            mAddItemButton.SetBounds(400, 320, 200, 30);
            mAddItemButton.Clicked += AddItemButton_Clicked;

            mSendButton = new Button(mSendMailBoxWindow, "SendButton");
            mSendButton.SetText("📤 Send");
            mSendButton.SetBounds(150, 300, 100, 30);
            mSendButton.Clicked += SendButton_Clicked;

            mCloseButton = new Button(mSendMailBoxWindow, "CloseButton");
            mCloseButton.SetText("❌ Close");
            mCloseButton.SetBounds(260, 300, 100, 30);
            mCloseButton.Clicked += CloseButton_Clicked;
            mSendMailBoxWindow.SetBounds(100, 100, 720, 360);
            mSendMailBoxWindow.DisableResizing();
            mSendMailBoxWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
          
        }
        public void LoadInventoryItems()
        {
          
            Items.Clear();
            mValues.Clear();

            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                Items.Add(new InventoryItem(this, i));
                Items[i].Container = new ImagePanel(mItemContainer, "InventoryMailItem");
                Items[i].Setup();

                mValues.Add(new Label(Items[i].Container, "InventoryMailItemValue"));
                mValues[i].Text = "";

                Items[i].Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

                if (Items[i].EquipPanel.Texture == null)
                {
                    Items[i].EquipPanel.Texture = Graphics.Renderer.GetWhiteTexture();
                }

                var xPadding = Items[i].Container.Margin.Left + Items[i].Container.Margin.Right;
                var yPadding = Items[i].Container.Margin.Top + Items[i].Container.Margin.Bottom;

                Items[i].Container.SetPosition(
                    i % (mItemContainer.Width / (Items[i].Container.Width + xPadding)) * (Items[i].Container.Width + xPadding) + xPadding,
                    i / (mItemContainer.Width / (Items[i].Container.Width + xPadding)) * (Items[i].Container.Height + yPadding) + yPadding
                );
                

            }
        }
        public void Update()
        {
            if (!mInitializedItems)
            {
                mInitializedItems = true;
                LoadInventoryItems();
            }

            if (mSendMailBoxWindow.IsHidden)
            {
                return;
            }

            // Verificar que la cantidad de items en la lista sea consistente con el tamaño esperado
            if (Items.Count != Options.MaxInvItems || mValues.Count != Options.MaxInvItems)
            {
                LoadInventoryItems();
            }

            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                if (i >= Items.Count || i >= mValues.Count)
                {
                    Console.WriteLine($"⚠️ Update(): Índice {i} fuera del rango de Items ({Items.Count}) o mValues ({mValues.Count}).");
                    continue;
                }

                var inventorySlot = Globals.Me.Inventory[i];

                if (inventorySlot == null || inventorySlot.ItemId == Guid.Empty)
                {
                    Items[i].Container.IsHidden = true;
                    mValues[i].IsHidden = true;
                    continue;
                }

                var item = ItemBase.Get(inventorySlot.ItemId);
                if (item != null)
                {
                    Items[i].Container.IsHidden = false;
                    Items[i].Container.RenderColor = CustomColors.Items.Rarities.TryGetValue(item.Rarity, out var rarityColor)
                        ? rarityColor
                        : Color.White;

                    mValues[i].IsHidden = inventorySlot.Quantity <= 1;
                    mValues[i].Text = item.IsStackable ? Strings.FormatQuantityAbbreviated(inventorySlot.Quantity) : "";

                    if (Items[i].IsDragging)
                    {
                        Items[i].Container.IsHidden = true;
                        mValues[i].IsHidden = true;
                    }

                    Items[i].Update();
                }
                else
                {
                    Items[i].Container.IsHidden = true;
                    mValues[i].IsHidden = true;
                }
            }
        }


        // ✅ Método para seleccionar un ítem del inventario
        public void SelectItem(InventoryItem itemSlot, int slotIndex)
        {
            var itemId = Globals.Me.Inventory[slotIndex]?.ItemId;
            if (itemId == null || itemId == Guid.Empty)
            {
                PacketSender.SendChatMsg("⚠️ No item selected or item is invalid!", 5);
                return;
            }

            mSelectedItem = itemSlot;
            mSelectedSlot = slotIndex;
            PacketSender.SendChatMsg($"📦 Selected item: {ItemBase.Get(itemId.Value)?.Name}", 5);
        }

        private void AddItemButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            // 🔍 Verificar si se ha seleccionado un ítem
            if (mSelectedItem == null)
            {
                PacketSender.SendChatMsg("⚠️ No item selected!", 4);
                return;
            }

            // 🔍 Verificar si el slot seleccionado es válido
            if (mSelectedSlot < 0 || mSelectedSlot >= Globals.Me.Inventory.Length)
            {
                PacketSender.SendChatMsg("⚠️ Invalid inventory slot selected!", 4);
                return;
            }

            var itemSlot = Globals.Me.Inventory[mSelectedSlot];

            // 🔍 Verificar si el slot contiene un ítem válido
            if (itemSlot == null || itemSlot.ItemId == Guid.Empty)
            {
                PacketSender.SendChatMsg("⚠️ No item in this slot!", 4);
                return;
            }

            // 🔍 Verificar si `mAttachments` está inicializado
            if (mAttachments == null)
            {
                mAttachments = new List<MailAttachmentPacket>();
            }

            // 🔍 Verificar si se ha ingresado una cantidad válida
            if (string.IsNullOrWhiteSpace(mQuantityTextBox.Text))
            {
                PacketSender.SendChatMsg("⚠️ Please enter a quantity.", 4);
                return;
            }

            if (!int.TryParse(mQuantityTextBox.Text, out var quantity))
            {
                PacketSender.SendChatMsg("⚠️ Invalid quantity.", 4);
                return;
            }

            if (quantity <= 0)
            {
                PacketSender.SendChatMsg("⚠️ Quantity must be greater than 0.", 4);
                return;
            }

            var itemBase = ItemBase.Get(itemSlot.ItemId);
            if (itemBase == null)
            {
                PacketSender.SendChatMsg("⚠️ Invalid item!", 4);
                return;
            }

            // 🔍 Verificar si la cantidad ingresada no supera la disponible
            if (quantity > itemSlot.Quantity)
            {
                PacketSender.SendChatMsg("⚠️ Not enough items in this slot!", 4);
                return;
            }

            // 🔍 Si el ítem no es apilable, solo permitir cantidad = 1
            if (!itemBase.IsStackable && quantity > 1)
            {
                PacketSender.SendChatMsg("⚠️ You can only send one of this item!", 4);
                return;
            }

            // 🔍 Verificar si ya hay un adjunto con el mismo ítem
            foreach (var attachment in mAttachments)
            {
                if (attachment.ItemId == itemSlot.ItemId)
                {
                    // Si el ítem es apilable, simplemente aumentar la cantidad
                    if (itemBase.IsStackable)
                    {
                        attachment.Quantity += quantity;
                        itemSlot.Quantity -= quantity;

                        // Si el inventario llega a 0, ocultarlo visualmente
                        if (itemSlot.Quantity <= 0)
                        {
                            Globals.Me.Inventory[mSelectedSlot] = null;
                            mSelectedItem.Container.IsHidden = true;
                        }

                        UpdateAttachmentSlots();
                        return;
                    }
                    else
                    {
                        PacketSender.SendChatMsg("⚠️ You can't send duplicate non-stackable items!", 4);
                        return;
                    }
                }
            }

            // 🔍 Buscar un espacio vacío en los adjuntos
            foreach (var mailSlot in mAttachmentSlots)
            {
                if (mailSlot.IsEmpty)
                {
                    mailSlot.SetItem(new Item { ItemId = itemSlot.ItemId, Quantity = quantity, ItemProperties = itemSlot.ItemProperties });
                    mAttachments.Add(new MailAttachmentPacket { ItemId = itemSlot.ItemId, Quantity = quantity, Properties = itemSlot.ItemProperties });

                    // Reducir cantidad en el inventario
                    itemSlot.Quantity -= quantity;
                    if (itemSlot.Quantity <= 0)
                    {
                        Globals.Me.Inventory[mSelectedSlot] = null;
                        mSelectedItem.Container.IsHidden = true;
                    }

                    UpdateAttachmentSlots();
                    return;
                }
            }

            // Si no hay slots disponibles
            PacketSender.SendChatMsg("⚠️ No free slots available to add the item.", 4);
        }

        public void InitializeAttachmentSlots()
        {             
            mAttachmentSlots = new List<MailItem>();

            for (int i = 0; i < 5; i++) // Número máximo de slots  
            {
                var mailSlot = new MailItem(this, i, mAttachmentContainer);
                mailSlot.SlotPanel.RightClicked += (sender, args) => RemoveAttachment(mailSlot); // Agregar eliminación con clic derecho  
                mAttachmentSlots.Add(mailSlot);

                var xPadding = 5;
                var yPadding = 5;

                int columns = 5; // Número de columnas en la disposición de los adjuntos  
                int xPos = (i % columns) * (mailSlot.SlotPanel.Width + xPadding) + xPadding;
                int yPos = (i / columns) * (mailSlot.SlotPanel.Height + yPadding) + yPadding;

                mailSlot.SlotPanel.SetPosition(xPos, yPos);
            }
        }
        public void UpdateAttachmentSlots()
        {
            if (mAttachmentSlots == null || mAttachments == null)
            {
                Console.WriteLine("⚠️ UpdateAttachmentSlots(): `mAttachmentSlots` o `mAttachments` es null.");
                return;
            }

            for (int i = 0; i < mAttachmentSlots.Count; i++)
            {
                if (i < mAttachments.Count)
                {
                    var attachment = mAttachments[i];
                    if (attachment == null || attachment.ItemId == Guid.Empty)
                    {
                        Console.WriteLine($"⚠️ UpdateAttachmentSlots(): El adjunto en el índice {i} es inválido.");
                        mAttachmentSlots[i].ClearItem();
                        continue;
                    }

                    var item = new Item
                    {
                        ItemId = attachment.ItemId,
                        Quantity = attachment.Quantity,
                        ItemProperties = attachment.Properties
                    };

                    mAttachmentSlots[i].SetItem(item);
                }
                else
                {
                    mAttachmentSlots[i].ClearItem();
                }
            }
        }


        void SendButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            // 🔍 Verificar que el destinatario y el título no estén vacíos
            if (string.IsNullOrWhiteSpace(mToTextbox.Text))
            {
                PacketSender.SendChatMsg("⚠️ You must enter a recipient.", 4);
                return;
            }

            if (string.IsNullOrWhiteSpace(mTitleTextbox.Text))
            {
                PacketSender.SendChatMsg("⚠️ You must enter a title.", 4);
                return;
            }

            // 🔍 Verificar que `mAttachments` no sea null
            if (mAttachments == null)
            {
                PacketSender.SendChatMsg("⚠️ No attachments found.", 4);
                return;
            }

            // 🔍 Verificar que hay al menos un adjunto
            if (mAttachments.Count == 0)
            {
                PacketSender.SendChatMsg("⚠️ You must attach at least one item.", 4);
                return;
            }

            // 🔍 Validar ítems no apilables
            foreach (var attachment in mAttachments)
            {
                if (attachment == null || attachment.ItemId == Guid.Empty)
                {
                    PacketSender.SendChatMsg("⚠️ Invalid attachment detected.", 4);
                    return;
                }

                var itemBase = ItemBase.Get(attachment.ItemId);
                if (itemBase == null)
                {
                    PacketSender.SendChatMsg($"⚠️ ItemBase not found for ID {attachment.ItemId}.", 4);
                    return;
                }

                if (!itemBase.IsStackable && attachment.Quantity > 1)
                {
                    PacketSender.SendChatMsg($"⚠️ {itemBase.Name} cannot be sent in multiple quantities.", 4);
                    return;
                }
            }

            Console.WriteLine($"📨 Sending mail to {mToTextbox.Text} with {mAttachments.Count} attachments.");

            // 🔍 Enviar correo
            PacketSender.SendMail(mToTextbox.Text, mTitleTextbox.Text, mMsgTextbox.Text, mAttachments);

            // 🔄 Cerrar la ventana tras el envío
            Close();
        }

        void CloseButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendCloseMail();
            Close();
        }
        private void RemoveAttachment(MailItem mailSlot)
        {
            if (mailSlot == null || mailSlot.CurrentSlot == null)
            {
                Console.WriteLine("⚠️ RemoveAttachment(): mailSlot o CurrentSlot es null.");
                return;
            }

            var attachment = mAttachments.Find(a => a.ItemId == mailSlot.CurrentSlot.ItemId);
            if (attachment == null)
            {
                Console.WriteLine($"⚠️ RemoveAttachment(): No se encontró el adjunto con ItemId {mailSlot.CurrentSlot.ItemId}");
                return;
            }

            // Devolver los ítems al inventario
            bool addedBackToInventory = false;
            foreach (var invSlot in Globals.Me.Inventory)
            {
                if (invSlot != null && invSlot.ItemId == attachment.ItemId)
                {
                    invSlot.Quantity += attachment.Quantity;
                    addedBackToInventory = true;
                    break;
                }
            }

            // Si no se encontró un slot, asignarlo a un slot vacío
            if (!addedBackToInventory)
            {
                for (int i = 0; i < Globals.Me.Inventory.Length; i++)
                {
                    if (Globals.Me.Inventory[i] == null)
                    {
                        Globals.Me.Inventory[i] = new Item
                        {
                            ItemId = attachment.ItemId,
                            Quantity = attachment.Quantity,
                            ItemProperties = attachment.Properties
                        };
                        break;
                    }
                }
            }

            // Eliminar el ítem de los adjuntos
            mAttachments.Remove(attachment);
            mailSlot.ClearItem();

            // Actualizar los adjuntos en la interfaz
            UpdateAttachmentSlots();
            PacketSender.SendChatMsg("✅ Item removed from attachments.", 4);
        }

        public void RestoreItemsToInventory()
        {
            foreach (var slot in mAttachmentSlots)
            {
                if (!slot.IsEmpty)
                {
                    var item = slot.CurrentSlot;
                    if (item == null || item.ItemId == Guid.Empty)
                    {
                        continue;
                    }

                    bool added = false;
                    for (int i = 0; i < Globals.Me.Inventory.Length; i++)
                    {
                        var inventorySlot = Globals.Me.Inventory[i];

                        if (inventorySlot == null || inventorySlot.ItemId == Guid.Empty)
                        {
                            Globals.Me.Inventory[i] = new Item
                            {
                                ItemId = item.ItemId,
                                Quantity = item.Quantity,
                                ItemProperties = item.ItemProperties
                            };
                            added = true;
                            break;
                        }

                        if (inventorySlot.ItemId == item.ItemId && inventorySlot.Base.IsStackable)
                        {
                            var maxStack = inventorySlot.Base.MaxInventoryStack;
                            var spaceLeft = maxStack - inventorySlot.Quantity;

                            var toAdd = Math.Min(spaceLeft, item.Quantity);
                            inventorySlot.Quantity += toAdd;
                            item.Quantity -= toAdd;

                            if (item.Quantity <= 0)
                            {
                                added = true;
                                break;
                            }
                        }
                    }

                    if (!added)
                    {
                        PacketSender.SendChatMsg("Inventory full. Could not restore item.", 4);
                    }

                    slot.ClearItem();
                }
            }
        }


        public void Close()
        {
            if (mAttachmentSlots != null)
            {
                RestoreItemsToInventory();
            }

            mSendMailBoxWindow.Close();
        }


        public bool IsVisible()
        {
           return !mSendMailBoxWindow.IsHidden;
        }

        public void Show()
        {
            mSendMailBoxWindow.IsHidden = false;
            Update();
        }
    }
}
