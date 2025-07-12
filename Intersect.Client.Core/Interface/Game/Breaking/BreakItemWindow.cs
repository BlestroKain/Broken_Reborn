using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Enchanting;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.Extensions;
using Intersect.GameObjects;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game.Breaking
{
    public partial class BreakItemWindow : Window
    {
        private readonly ScrollControl _inventoryScroll;
        private readonly ImagePanel _selectedItemPanel;
        private readonly Button _breakButton;
        private readonly Button _closeButton;

        private readonly List<SlotItem> Items = new();
        private Item? _selectedItem;

        public BreakItemWindow(Canvas canvas) : base(canvas, Strings.Breaking.WindowTitle, false, nameof(BreakItemWindow))
        {
            IsResizable = false;
            SetSize(600, 400);
            SetPosition(100, 100);
            IsVisibleInTree = false;
            IsClosable = true;

            _inventoryScroll = new ScrollControl(this, "ItemContainer");
            _inventoryScroll.SetPosition(20, 20);
            _inventoryScroll.SetSize(260, 360);
            _inventoryScroll.EnableScroll(false, true);

            _selectedItemPanel = new ImagePanel(this, "SelectedItemPanel");
            _selectedItemPanel.SetPosition(300, 60);
            _selectedItemPanel.SetSize(80, 80);
            _selectedItemPanel.Texture = Graphics.Renderer.WhitePixel;

            _breakButton = new Button(this, "BreakButton");
            _breakButton.SetPosition(300, 160);
            _breakButton.SetSize(120, 40);
            _breakButton.Text = Strings.Breaking.Break;
            _breakButton.Clicked += OnBreakButtonClicked;

            _closeButton = new Button(this, "CloseButton");
            _closeButton.SetPosition(440, 160);
            _closeButton.SetSize(120, 40);
            _closeButton.Text = Strings.Breaking.Close;
            _closeButton.Clicked += (s, e) => Hide();

            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitItemContainer();
        }

        protected override void EnsureInitialized()
        {
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitItemContainer();
        }

        private void InitItemContainer()
        {
            Items.Clear();
            _inventoryScroll.DeleteAllChildren();

            for (int i = 0; i < Options.Instance.Player.MaxInventory; i++)
            {
                var slot = new BreakInventoryItem(this, _inventoryScroll, i,null);
                Items.Add(slot);
            }

            PopulateSlotContainer.Populate(_inventoryScroll, Items);
        }

     
        public void SelectItem(Item? item)
        {
            if (item == null || item.Descriptor == null) return;

            _selectedItem = item;

            foreach (var slot in Items)
            {
                if (slot is BreakInventoryItem Brokeslot)
                {
                    Brokeslot.SetSelected(Brokeslot.SlotIndex == Globals.Me.Inventory.IndexOf(item));
                }
            }

            var itemTexture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Item, item.Descriptor.Icon);
            _selectedItemPanel.Texture = itemTexture ?? Graphics.Renderer.WhitePixel;


        }
        public void Update()
        {
            if (!IsVisibleInParent || Globals.Me?.Inventory == null)
                return;

            foreach (var slot in Items)
                slot.Update();
        }
 

        private void OnBreakButtonClicked(Base sender, MouseButtonState arguments)
        {
            if (_selectedItem == null)
            {
                PacketSender.SendChatMsg(Strings.Breaking.NoItemSelected, 4);
                return;
            }

            var itemIndex = Globals.Me.Inventory.IndexOf(_selectedItem);
            if (itemIndex < 0)
            {
                PacketSender.SendChatMsg("Error al identificar el ítem seleccionado.", 4);
                return;
            }

            PacketSender.SendBreakItem(itemIndex);
        }
    }
}
