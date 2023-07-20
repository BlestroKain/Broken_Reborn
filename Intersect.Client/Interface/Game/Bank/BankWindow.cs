using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects;
using Intersect.Client.Networking;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Items;
using System.Linq;
using Intersect.Client.Utilities;
using Intersect.Enums;
namespace Intersect.Client.Interface.Game.Bank
{

    public partial class BankWindow
    {

        private static int sItemXPadding = 4;

        private static int sItemYPadding = 4;

        public List<BankItem> Items = new List<BankItem>();

        //Controls
        private WindowControl mBankWindow;

        private ScrollControl mItemContainer;

        private List<Label> mValues = new List<Label>();

        //Location
        public int X;

        public int Y;

        private bool mOpen;

        private Button mSortButton;

        private Label mValueLabel;
        // Context menu
        private Framework.Gwen.Control.Menu mContextMenu;

        private MenuItem mWithdrawContextItem;

        //Init
        public BankWindow(Canvas gameCanvas)
        {
            // Create a new window to display the contents of the bank.
            mBankWindow = new WindowControl(gameCanvas,
                Globals.GuildBank
                    ? Strings.Guilds.Bank.ToString(Globals.Me?.Guild)
                    : Strings.Bank.title.ToString(),
                false, "BankWindow");

            // Disable resizing and add to the list of input-blocking elements.
            mBankWindow.DisableResizing();
            Interface.InputBlockingElements.Add(mBankWindow);

            // Create a new scroll control for the items in the bank.
            mItemContainer = new ScrollControl(mBankWindow, "ItemContainer");
            mItemContainer.SetSize(442, 400); // Ajustar el tamaño para que quepan los elementos
            mItemContainer.EnableScroll(false, true);
            // Sort Button
            mSortButton = new Button(mBankWindow, "SortButton");
            mSortButton.SetText("Sort");
            mSortButton.Clicked += sort_Clicked;
            mSortButton.SetPosition(350, 10); // Set the position of the sort button

            // Value Label
            mValueLabel = new Label(mBankWindow, "ValueLabel");
            mValueLabel.SetText("Bank Value: 0"); // Replace 0 with the actual bank value
            mValueLabel.SetPosition(10, 450); // Set the position of the value label

            _BankWindow();
            // Initialize the bank window and item container.
            mBankWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

    
            InitItemContainer();

            // Create a context menu for the bank items.
            mContextMenu = new Framework.Gwen.Control.Menu(gameCanvas, "BankContextMenu");
            mContextMenu.IsHidden = true;
            mContextMenu.IconMarginDisabled = true;

            // Clear the children of the context menu and add a "Withdraw" option.
            mContextMenu.Children.Clear();
            mWithdrawContextItem = mContextMenu.AddItem(Strings.BankContextMenu.Withdraw);
            mWithdrawContextItem.Clicked += MWithdrawContextItem_Clicked;

          
            mContextMenu.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            // Close the window.
            Close();
        }

        public void OpenContextMenu(int slot)
        {
            var item = ItemBase.Get(Globals.Bank[slot].ItemId);

            // No point showing a menu for blank space.
            if (item == null)
            {
                return;
            }

            mWithdrawContextItem.SetText(Strings.BankContextMenu.Withdraw.ToString(item.Name));

            // Set our spell slot as userdata for future reference.
            mContextMenu.UserData = slot;

            mContextMenu.SizeToChildren();
            mContextMenu.Open(Framework.Gwen.Pos.None);
        }

        private void MWithdrawContextItem_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            var slot = (int)sender.Parent.UserData;
            Globals.Me.TryWithdrawItem(slot);
        }

        public void Close()
        {
            mBankWindow.IsHidden = true;
            mOpen = false;
            mContextMenu?.Close();
            _Close();
        }

        public void Open()
        {
            // Hide unavailable bank slots
            var currentBankSlots = Math.Max(0, Globals.BankSlots);
            // For any slot beyond the current bank's maximum slots
            for (var i = currentBankSlots; i < Options.Instance.Bank.MaxSlots; i++)
            {
                var bankItem = Items[i];
                var bankLabel = mValues[i];

                bankItem.Container.Hide();
                bankLabel.Hide();

                // Position this invisible BankItem at 0,0 so the scrollbar doesn't think we have more slots than we do
                SetItemPosition(i);
            }

            mBankWindow.IsHidden = false;
            mOpen = true;
            FillSortedBank();
        }

        public bool IsVisible()
        {
            return !mBankWindow.IsHidden;
        }

        public void Update()
        {
            if (mBankWindow.IsHidden)
            {
                if (mOpen)
                {
                    Interface.GameUi.NotifyCloseBank();
                }

                return;
            }

            X = mBankWindow.X;
            Y = mBankWindow.Y;
            for (var i = 0; i < Math.Min(Globals.BankSlots, Options.Instance.Bank.MaxSlots); i++)
            {
                var bankItem = Items[i];
                var bankLabel = mValues[i];
                var globalBankItem = SortedBank[i]?.Item;

                bankItem.Container.Show();
                SetItemPosition(i);
                if (globalBankItem != null && globalBankItem.ItemId != Guid.Empty)
                {
                    var item = ItemBase.Get(globalBankItem.ItemId);
                    if (item != null)
                    {
                        bankItem.Pnl.IsHidden = false;
                        if (item.IsStackable)
                        {
                            bankLabel.IsHidden = globalBankItem.Quantity <= 1;
                            bankLabel.Text = Strings.FormatQuantityAbbreviated(globalBankItem.Quantity);
                        }
                        else
                        {
                            bankLabel.IsHidden = true;
                        }

                        if (bankItem.IsDragging)
                        {
                            bankItem.Pnl.IsHidden = true;
                            bankLabel.IsHidden = true;
                        }

                        bankItem.Update();
                    }
                }
                else
                {
                    bankItem.Pnl.IsHidden = true;
                    bankLabel.IsHidden = true;
                }
            }
            UpdateBank();
            mValueLabel.SetText(Strings.Bank.bankvalue.ToString(Strings.FormatQuantityAbbreviated(Globals.BankValue)));
            mValueLabel.SetToolTipText(Strings.Bank.bankvaluefull.ToString(Globals.BankValue.ToString("N0").Replace(",", Strings.Numbers.comma)));

        }
                void sort_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mBankWindow.IsHidden) return;

            PacketSender.SendBankSortPacket();
        }

        private void InitItemContainer()
        {
            for (var slotIndex = 0; slotIndex < Options.Instance.Bank.MaxSlots; slotIndex++)
            {
                var bankItem = new BankItem(this, slotIndex);

                bankItem.Container = new ImagePanel(mItemContainer, "BankItem");
                bankItem.Setup();

                var bankLabel = new Label(bankItem.Container, "BankItemValue");
                bankLabel.Text = "";

                bankItem.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                
                Items.Add(bankItem);
                mValues.Add(bankLabel);
            }
        }

        /// <summary>
        /// Sets the item's position based on whether it's hidden or not
        /// </summary>
        /// <param name="i">Index of the item slot</param>
        private void SetItemPosition(int i)
        {
            var item = Items[i];

            // If the item is hidden, the player doesn't have that slot unlocked - move it to the top so that the scrollbar doesn't lie to the player
            if (item.Container.IsHidden)
            {
                item.Container.SetPosition(0, 0);
                return;
            }

            var xPadding = item.Container.Margin.Left + item.Container.Margin.Right;
            var yPadding = item.Container.Margin.Top + item.Container.Margin.Bottom;

            item.Container.SetPosition(
                i %
                (mItemContainer.Width / (item.Container.Width + xPadding)) *
                (item.Container.Width + xPadding) +
                xPadding,
                i /
                (mItemContainer.Width / (item.Container.Width + xPadding)) *
                (item.Container.Height + yPadding) +
                yPadding
            );
        }

        public FloatRect RenderBounds()
        {
            var rect = new FloatRect()
            {
                X = mBankWindow.LocalPosToCanvas(new Point(0, 0)).X - sItemXPadding / 2,
                Y = mBankWindow.LocalPosToCanvas(new Point(0, 0)).Y - sItemYPadding / 2,
                Width = mBankWindow.Width + sItemXPadding,
                Height = mBankWindow.Height + sItemYPadding
            };

            return rect;
        }

    }
    public partial class BankWindow
    {
        private TextBox mSearch;
        private ImagePanel mTextboxBg;
        private Label mSearchLabel;
        private Button mClearButton;
        public List<BankSlot> SortedBank;
        private bool RefreshBank = false;

        public class BankSlot
        {
            public int SlotId { get; set; }
            public Item Item { get; set; }

            public BankSlot(int slotId, Item item)
            {
                SlotId = slotId;
                Item = item;
            }
        }

        private void _BankWindow()
        {
            mSearchLabel = new Label(mBankWindow, "SearchLabel");
            mSearchLabel.Text = "Search:";
            mClearButton = new Button(mBankWindow, "ClearButton");
            mClearButton.Pressed += mClear_Pressed;
            mClearButton.Text = "Clear";
            mTextboxBg = new ImagePanel(mBankWindow, "Textbox");
            mSearch = new TextBox(mTextboxBg, "SearchBox");
            mSearch.TextChanged += mSearch_textChanged;
        }

        private void FillSortedBank()
        {
            SortedBank = new List<BankSlot>();
            for (var i = 0; i < Globals.Bank.Length; i++)
            {
                var item = Globals.Bank[i];
                SortedBank.Add(new BankSlot(i, item));
            }
        }

        private void UpdateBank()
        {
            if (!RefreshBank)
            {
                return;
            }

            FillSortedBank();
            if (!string.IsNullOrEmpty(mSearch.Text))
            {
                SortedBank = SortedBank
                    .Select((bankSlot) =>
                    {
                        var slotItem = bankSlot.Item;
                        if (slotItem == null)
                        {
                            return null;
                        }
                        var item = ItemBase.Get(slotItem.ItemId);
                        if (!ItemIsSearchable(item))
                        {
                            return null;
                        }
                        return bankSlot;
                    })
                    .OrderByDescending(bankItem => bankItem != null && ItemIsSearchable(ItemBase.Get(bankItem.Item.ItemId)))
                    .ToList();
            }
            RefreshBank = false;
        }

        private bool ItemIsSearchable(ItemBase item)
        {
            string itemType = Enum.GetName(typeof(ItemType), item.ItemType).ToLower();
            return SearchHelper.IsSearchable(item?.Name, mSearch.Text) || SearchHelper.IsSearchable(itemType, mSearch.Text);
        }

        public void _Close()
        {
            if (mSearch == null)
            {
                return;
            }
            mSearch.Text = string.Empty;
        }

        private void mSearch_textChanged(Base control, EventArgs args)
        {
            mItemContainer.ScrollToTop();
            InitRefreshBank();
        }

        private void mClear_Pressed(Base control, EventArgs args)
        {
            mSearch.Text = string.Empty;
            InitRefreshBank();
        }

        public void InitRefreshBank()
        {
            RefreshBank = true;
        }
    }
}
