using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Admin;
using Intersect.Client.Interface.Game.Bag;
using Intersect.Client.Interface.Game.Bank;
using Intersect.Client.Interface.Game.Breaking;
using Intersect.Client.Interface.Game.Bestiary;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Crafting;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Interface.Game.Enchanting;
using Intersect.Client.Interface.Game.EntityPanel;
using Intersect.Client.Interface.Game.Guilds;
using Intersect.Client.Interface.Game.Hotbar;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.Mail;
using Intersect.Client.Interface.Game.Shop;
using Intersect.Client.Interface.Game.Market;
using Intersect.Client.Interface.Game.Trades;
using Intersect.Client.Interface.Menu;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Networking;
using Intersect.Core;
using Intersect.Enums;
using Intersect.GameObjects;
using Microsoft.Extensions.Logging;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Client.Interface.Game.Spells;

namespace Intersect.Client.Interface.Game;

public partial class GameInterface : MutableInterface
{
    public bool FocusChat;

    public bool UnfocusChat;

    public bool ChatFocussed => mChatBox.HasFocus;

    //Public Components - For clicking/dragging
    public HotBarWindow Hotbar;

    private AdminWindow? mAdminWindow;

    private BagWindow _bagWindow;

    private BankWindow? _bankWindow;

    private BestiaryWindow? _bestiaryWindow;

    private Chatbox mChatBox;

    private CraftingWindow? mCraftingWindow;

    private PictureWindow mPictureWindow;

    private QuestOfferWindow mQuestOfferWindow;

    private ShopWindow _shopWindow;
    private MarketWindow? _marketWindow;
    private SellMarketWindow? _sellMarketWindow;
    public EnchantItemWindow mEnchantItemWindow;
    private RuneEnchantWindow mRuneItemWindow;
    private BreakItemWindow mBreakItemWindow;
    private MapItemWindow mMapItemWindow;
    private GuildCreationWindow mCreateGuildWindow;
    private SettingsWindow? _settingsWindow;

    private ItemDescriptionWindow? _itemDescriptionWindow;

    private SpellDescriptionWindow? _spellDescriptionWindow;

    private bool mShouldCloseBag;

    private bool _shouldCloseBank;

    private bool mShouldCloseCraftingTable;

    private bool mShouldCloseShop;

    private bool mShouldCloseTrading;

    private bool mShouldOpenGuildCreation;
    private bool mShouldCloseGuildCreation;

    private bool mShouldOpenAdminWindow;

    private bool mShouldOpenBag;

    private bool mShouldOpenBank;

    private bool mShouldOpenCraftingTable;

    private bool mShouldOpenShop;

    private bool _shouldOpenMarket;
    private bool _shouldCloseMarket;
    private bool _shouldOpenSellMarket;
    private bool _shouldCloseSellMarket;
    private int _sellMarketSlot;

    private bool mShouldOpenTrading;

    private bool mShouldUpdateQuestLog = true;

    private bool mShouldUpdateFriendsList;

    private bool mShouldUpdateGuildList;

    private bool mShouldHideGuildWindow;

    private string mTradingTarget;

    private bool mCraftJournal {  get; set; }

    private TradingWindow? mTradingWindow;

    public EntityBox PlayerBox;

    public PlayerStatusWindow PlayerStatusWindow;


    private SettingsWindow GetOrCreateSettingsWindow()
    {
        _settingsWindow ??= new SettingsWindow(GameCanvas)
        {
            IsVisibleInTree = false,
        };

        return _settingsWindow;
    }

    public GameInterface(Canvas canvas) : base(canvas)
    {
        GameCanvas = canvas;

        InitGameGui();
    }

    public Canvas GameCanvas { get; }

    private AnnouncementWindow? _announcementWindow;
    private EscapeMenuWindow? _escapeMenu;
    private SimplifiedEscapeMenu? _simplifiedEscapeMenu;
    private TargetContextMenu? _targetContextMenu;
    private SendMailBoxWindow mSendMailBoxWindow;
    private MailBoxWindow mMailBoxWindow;
    public EscapeMenuWindow EscapeMenu => _escapeMenu ??= new EscapeMenuWindow(GameCanvas, GetOrCreateSettingsWindow)
    {
        IsHidden = true,
    };

    public SimplifiedEscapeMenu SimplifiedEscapeMenu => _simplifiedEscapeMenu ??= new SimplifiedEscapeMenu(GameCanvas, GetOrCreateSettingsWindow) {IsHidden = true};

    public TargetContextMenu TargetContextMenu => _targetContextMenu ??= new TargetContextMenu(GameCanvas) {IsHidden = true};

    public AnnouncementWindow AnnouncementWindow => _announcementWindow ??= new AnnouncementWindow(GameCanvas) { IsHidden = true };

    public ItemDescriptionWindow? ItemDescriptionWindow
    {
        get => _itemDescriptionWindow ??= new ItemDescriptionWindow();
        set => _itemDescriptionWindow = value;
    }

    public SpellDescriptionWindow? SpellDescriptionWindow
    {
        get => _spellDescriptionWindow ??= new SpellDescriptionWindow();
        set => _spellDescriptionWindow = value;
    }

    public MenuContainer GameMenu { get; private set; }


    public void InitGameGui()
    {
        mChatBox = new Chatbox(GameCanvas, this);
        GameMenu = new MenuContainer(GameCanvas);
        Hotbar = new HotBarWindow(GameCanvas);
        PlayerBox = new EntityBox(GameCanvas, EntityType.Player, Globals.Me, true);
        PlayerBox.SetEntity(Globals.Me);
        PlayerStatusWindow = new PlayerStatusWindow(GameCanvas);
        if (mPictureWindow == null)
        {
            mPictureWindow = new PictureWindow(GameCanvas);
        }

        mQuestOfferWindow = new QuestOfferWindow(GameCanvas);
        mMapItemWindow = new MapItemWindow(GameCanvas);

    }
    public void OpenEnchantWindow()
    {
        if (mEnchantItemWindow == null)
        {
            mEnchantItemWindow = new EnchantItemWindow(GameCanvas);

        }
        mEnchantItemWindow.Show();

    }

    public void OpenRuneItemWindow()
    {
        if (mRuneItemWindow == null)
        {
            mRuneItemWindow = new RuneEnchantWindow(GameCanvas);
        }
        mRuneItemWindow.Show();
    }
    public void HideOrbItemWindow()
    {
        mRuneItemWindow.Hide();
    }
    public void OpenBrokeItemWindow()
    {
        if (mBreakItemWindow == null)
        {
            mBreakItemWindow = new BreakItemWindow(GameCanvas);
        }
        mBreakItemWindow.Show();
    }
    //Chatbox
    public void SetChatboxText(string msg)
    {
        mChatBox.SetChatboxText(msg);
    }

    public void AppendChatboxText(string text)
    {
        mChatBox.AppendText(text);
    }

    public void AppendChatboxItem(ItemDescriptor descriptor, ItemProperties properties)
    {
        mChatBox.AppendItem(descriptor, properties);
    }

    public string ChatboxText => mChatBox.ChatboxText;

    //Friends Window
    public void NotifyUpdateFriendsList()
    {
        mShouldUpdateFriendsList = true;
    }

    //Guild Window
    public void NotifyUpdateGuildList()
    {
        mShouldUpdateGuildList = true;
    }

    public void HideGuildWindow()
    {
        mShouldHideGuildWindow = true;
    }

    //Admin Window
    public void NotifyOpenAdminWindow()
    {
        mShouldOpenAdminWindow = true;
    }

    public bool ToggleAdminWindow()
    {
        mShouldOpenAdminWindow = false;

        if (mAdminWindow == null)
        {
            mAdminWindow ??= new AdminWindow(GameCanvas);
            mAdminWindow.X = GameCanvas.Width - mAdminWindow.OuterWidth;
            mAdminWindow.Y = (GameCanvas.Height - mAdminWindow.OuterHeight) / 2;
        }
        else if (IsAdminWindowOpen)
        {
            mAdminWindow.Hide();
        }
        else
        {
            mAdminWindow.Show();
        }

        return mAdminWindow.IsVisibleInParent;
    }

    //Shop
    public void NotifyOpenShop()
    {
        mShouldOpenShop = true;
    }

    public void NotifyCloseShop()
    {
        mShouldCloseShop = true;
    }

    public void OpenShop()
    {
        _shopWindow = new ShopWindow(GameCanvas) { DeleteOnClose = true };
        mShouldOpenShop = false;
    }

    //Market
    public void NotifyOpenMarket()
    {
        _shouldOpenMarket = true;
    }

    public void NotifyCloseMarket()
    {
        _shouldCloseMarket = true;
    }

    public void OpenMarket()
    {
        _marketWindow ??= new MarketWindow(GameCanvas) { DeleteOnClose = true };
        _marketWindow.Show();
        _shouldOpenMarket = false;
    }

    public void NotifyOpenSellMarket(int slot)
    {
        _sellMarketSlot = slot;
        _shouldOpenSellMarket = true;
    }

    public void NotifyCloseSellMarket()
    {
        _shouldCloseSellMarket = true;
    }

    public void OpenSellMarket()
    {
        _sellMarketWindow = new SellMarketWindow(GameCanvas);
        _sellMarketWindow.Show();
        _shouldOpenSellMarket = false;
    }

    //Bank
    public void NotifyOpenBank()
    {
        mShouldOpenBank = true;
    }

    public void NotifyCloseBank()
    {
        _shouldCloseBank = true;
    }

    public void OpenBank()
    {
        _bankWindow = new BankWindow(GameCanvas) { DeleteOnClose = true };
        mShouldOpenBank = false;
        Globals.InBank = true;
    }

    //Guild Creation
    public void NotifyOpenGuildCreation()
    {
        mShouldOpenGuildCreation = true;
    }

    public void NotifyCloseGuildCreation()
    {
        mShouldCloseGuildCreation = true;
    }

    public void OpenGuildCreationWindow()
    {
        mCreateGuildWindow ??= new GuildCreationWindow(GameCanvas);
        mCreateGuildWindow.Show();
        mShouldOpenGuildCreation = false;
    }

    public void CloseGuildCreation()
    {
        mCreateGuildWindow?.Hide();
        mShouldCloseGuildCreation = false;
    }

    // Bestiary
    public void ToggleBestiaryWindow()
    {
        if (_bestiaryWindow?.IsVisibleInTree == true)
        {
            _bestiaryWindow.Hide();
        }
        else
        {
            GameMenu.HideWindows();
            _bestiaryWindow ??= new BestiaryWindow(GameCanvas);
            _bestiaryWindow.Show();
        }
    }

    //Bag
    public void NotifyOpenBag()
    {
        mShouldOpenBag = true;
    }

    public void NotifyCloseBag()
    {
        mShouldCloseBag = true;
    }

    public void OpenBag()
    {
        _bagWindow = new BagWindow(GameCanvas) { DeleteOnClose = true };
        mShouldOpenBag = false;
        Globals.InBag = true;
    }

    public BagWindow GetBagWindow()
    {
        return _bagWindow;
    }

    public BankWindow? GetBankWindow()
    {
        return _bankWindow;
    }

    public MarketWindow? GetMarketWindow()
    {
        return _marketWindow;
    }

    public void RefreshBank()
    {
        _bankWindow?.Refresh();
    }

    //Crafting
    public void NotifyOpenCraftingTable(bool journalMode)
    {
        mShouldOpenCraftingTable = true;
        mCraftJournal = journalMode;
    }

    public void NotifyCloseCraftingTable()
    {
        mShouldCloseCraftingTable = true;
        mCraftJournal = false;
    }

    public void OpenCraftingTable()
    {
        if (mCraftingWindow != null)
        {
            mCraftingWindow.Close();
        }

        mCraftingWindow = new CraftingWindow(GameCanvas, mCraftJournal);
        mShouldOpenCraftingTable = false;
        Globals.InCraft = true;
    }

    //Quest Log
    public void NotifyQuestsUpdated()
    {
        mShouldUpdateQuestLog = true;
    }

    //Trading
    public void NotifyOpenTrading(string traderName)
    {
        mShouldOpenTrading = true;
        mTradingTarget = traderName;
    }

    public void NotifyCloseTrading()
    {
        mShouldCloseTrading = true;
    }

    public void OpenTrading()
    {
        mTradingWindow?.Close();
        mTradingWindow = new TradingWindow(GameCanvas, mTradingTarget);
        mShouldOpenTrading = false;
        Globals.InTrade = true;
    }

    public bool IsAdminWindowOpen => !mAdminWindow?.IsHidden ?? false;

    public SpellsWindow SpellsWindow { get;  set; }

    public void AdminWindowSelectName(string playerName)
    {
        if (mAdminWindow is not { } adminWindow)
        {
            return;
        }

        adminWindow.PlayerName = playerName;
    }

    public void Update(TimeSpan elapsed, TimeSpan total)
    {
        if (Globals.Me != null && PlayerBox?.MyEntity != Globals.Me)
        {
            PlayerBox?.SetEntity(Globals.Me);
        }

        GameMenu?.Update(mShouldUpdateQuestLog);
        mShouldUpdateQuestLog = false;
        Hotbar?.Update();
        EscapeMenu.Update();
        PlayerBox?.Update();
        PlayerStatusWindow?.Update();
        mMapItemWindow.Update();
        AnnouncementWindow?.Update();
        mPictureWindow?.Update();
        mCreateGuildWindow?.Update();
        mBreakItemWindow?.Update();
        mEnchantItemWindow?.Update();
        mRuneItemWindow?.Update();
        _bestiaryWindow?.Update();
        mMailBoxWindow?.UpdateMail();
        var questDescriptorId = Globals.QuestOffers.FirstOrDefault();
        if (questDescriptorId == default)
        {
            if (mQuestOfferWindow.IsVisible())
            {
                mQuestOfferWindow.Hide();
            }
        }
        else if (QuestDescriptor.TryGet(questDescriptorId, out var questDescriptor))
        {
            mQuestOfferWindow.Update(questDescriptor);
        }
        else
        {
            ApplicationContext.CurrentContext.Logger.LogWarning("Failed to get quest {QuestId}", questDescriptorId);
        }

        if (Globals.Picture != null)
        {
            if (mPictureWindow.Picture != Globals.Picture.Picture ||
                mPictureWindow.Size != Globals.Picture.Size ||
                mPictureWindow.Clickable != Globals.Picture.Clickable)
            {
                mPictureWindow.Setup(Globals.Picture.Picture, Globals.Picture.Size, Globals.Picture.Clickable);
            }
        }
        else
        {
            if (mPictureWindow != null)
            {
                mPictureWindow.Close();
            }
        }

        EventWindow.ShowOrUpdateDialog(GameCanvas);

        //Admin window update
        if (mShouldOpenAdminWindow)
        {
            ToggleAdminWindow();
        }

        //Shop Update
        if (mShouldOpenShop)
        {
            OpenShop();
            GameMenu.OpenInventory();
        }

        if (_shopWindow != null && (!_shopWindow.IsVisibleInTree || mShouldCloseShop))
        {
            CloseShop();
        }

        mShouldCloseShop = false;

        //Market Update
        if (_shouldOpenMarket)
        {
            OpenMarket();
            GameMenu.OpenInventory();
        }

        if (_marketWindow != null && (!_marketWindow.IsVisibleInTree || _shouldCloseMarket))
        {
            _marketWindow.Close();
            _marketWindow = null;
            _shouldCloseMarket = false;
        }

        if (_shouldOpenSellMarket)
        {
            OpenSellMarket();
        }

        if (_sellMarketWindow != null && (!_sellMarketWindow.IsVisible() || _shouldCloseSellMarket))
        {
            _sellMarketWindow.Close();
            _sellMarketWindow = null;
            _shouldCloseSellMarket = false;
        }

        //Bank Update
        if (mShouldOpenBank)
        {
            OpenBank();
            GameMenu?.OpenInventory();
        }
        else if (_shouldCloseBank)
        {
            CloseBank();
        }
        else
        {
            _bankWindow?.Update();
        }

        //Bag Update
        if (mShouldOpenBag)
        {
            OpenBag();
        }

        if (_bagWindow != null)
        {
            if (!_bagWindow.IsVisibleInTree || mShouldCloseBag)
            {
                CloseBagWindow();
            }
            else
            {
                _bagWindow.Update();
            }
        }

        mShouldCloseBag = false;

        //Crafting station update
        if (mShouldOpenCraftingTable)
        {
            OpenCraftingTable();
            GameMenu.OpenInventory();
        }

        if (mCraftingWindow != null)
        {
            if (!mCraftingWindow.IsVisibleInTree || mShouldCloseCraftingTable)
            {
                CloseCraftingTable();
            }
        }

        mShouldCloseCraftingTable = false;

        //Trading update
        if (mShouldOpenTrading)
        {
            OpenTrading();
            GameMenu.OpenInventory();
        }

        if (mTradingWindow != null)
        {
            if (mShouldCloseTrading)
            {
                CloseTrading();
                mShouldCloseTrading = false;
            }
            else
            {
                if (!mTradingWindow.IsVisible())
                {
                    CloseTrading();
                }
                else
                {
                    mTradingWindow.Update();
                }
            }
        }

        //Guild Creation window update
        if (mShouldOpenGuildCreation)
        {
            OpenGuildCreationWindow();
        }
        else if (mShouldCloseGuildCreation)
        {
            CloseGuildCreation();
        }

        if (mShouldUpdateFriendsList)
        {
            GameMenu.UpdateFriendsList();
            mShouldUpdateFriendsList = false;
        }

        if (mShouldUpdateGuildList)
        {
            GameMenu.UpdateGuildList();
            mShouldUpdateGuildList = false;
        }

        if (mShouldHideGuildWindow)
        {
            GameMenu.HideGuildWindow();
            mShouldHideGuildWindow = false;
        }

        mShouldCloseTrading = false;

        if (FocusChat)
        {
            mChatBox.Focus();
            FocusChat = false;
        }

        if (UnfocusChat)
        {
            mChatBox.UnFocus();
            UnfocusChat = false;
        }
    }

    public void UpdateAdminWindowMapList()
    {
        mAdminWindow?.UpdateMapList();
    }

    public void Draw(TimeSpan elapsed, TimeSpan total)
    {
        GameCanvas.RenderCanvas(elapsed, total);
    }

    private void CloseShop()
    {
        Globals.GameShop = null;
        _shopWindow?.Hide();
        _shopWindow = null;
        PacketSender.SendCloseShop();
    }

    private void CloseBank()
    {
        _bankWindow = null;
        Globals.InBank = false;
        PacketSender.SendCloseBank();
        _shouldCloseBank = false;
    }

    private void CloseBagWindow()
    {
        _bagWindow?.Hide();
        _bagWindow = null;
        Globals.InBag = false;
        PacketSender.SendCloseBag();
    }

    private void CloseCraftingTable()
    {
        mCraftingWindow?.Close();
        mCraftingWindow = null;
        Globals.InCraft = false;
        PacketSender.SendCloseCrafting();
    }

    private void CloseTrading()
    {
        mTradingWindow?.Close();
        mTradingWindow = null;
        Globals.InTrade = false;
        PacketSender.SendDeclineTrade();
    }

    public bool CloseAllWindows()
    {
        var closedWindows = false;
        if (_bagWindow != null && _bagWindow.IsVisibleInTree)
        {
            CloseBagWindow();
            closedWindows = true;
        }

        if (mTradingWindow != null && mTradingWindow.IsVisible())
        {
            CloseTrading();
            closedWindows = true;
        }

        if (_bankWindow is { IsVisibleInTree: true })
        {
            CloseBank();
            closedWindows = true;
        }

        if (mCraftingWindow is { IsVisibleInTree: true, IsCrafting: false })
        {
            CloseCraftingTable();
            closedWindows = true;
        }

        if (_shopWindow is { IsVisibleInTree: true })
        {
            CloseShop();
            closedWindows = true;
        }

        if (_bestiaryWindow is { IsVisibleInTree: true })
        {
            _bestiaryWindow.Hide();
            closedWindows = true;
        }

        if (GameMenu != null && GameMenu.HasWindowsOpen())
        {
            GameMenu.CloseAllWindows();
            closedWindows = true;
        }

        if (TargetContextMenu.IsVisibleInTree)
        {
            TargetContextMenu.ToggleHidden();
            closedWindows = true;
        }

        return closedWindows;
    }

    //Dispose
    public void Dispose()
    {
        CloseBagWindow();
        CloseBank();
        CloseCraftingTable();
        CloseShop();
        CloseTrading();
        _bestiaryWindow?.Hide();
        _bestiaryWindow = null;
        GameCanvas.Dispose();
    }

    // Mail Box
    public void OpenSendMailBox()
    {
        if (mSendMailBoxWindow == null)
        {
            mSendMailBoxWindow = new SendMailBoxWindow(GameCanvas);
        }

        mSendMailBoxWindow.Show();
       // mSendMailBoxWindow.Update();

    }

    public void OpenMailBox()
    {
        if (mMailBoxWindow == null)
        {
            mMailBoxWindow = new MailBoxWindow(GameCanvas);
        }

        mMailBoxWindow.Show();
        mMailBoxWindow.UpdateMail();
    }

    public void CloseSendMailBox()
    {
        // Ocultar la ventana de envío de correos
        mSendMailBoxWindow?.Close();
    }

    public void CloseMailBox()
    {
        // Ocultar la ventana de bandeja de entrada
        mMailBoxWindow?.Hide();
    }

}
