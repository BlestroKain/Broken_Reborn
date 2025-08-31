using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.Job;
using Intersect.Client.Interface.Game.Spells;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace Intersect.Client.Interface.Game;


public partial class MenuContainer : Panel
{
    private readonly ImagePanel _inventoryButtonContainer;
    private readonly Button _inventoryButton;
    private readonly InventoryWindow _inventoryWindow;

    private readonly ImagePanel _spellsButtonContainer;
    private readonly Button _spellsButton;
    private readonly SpellsWindow _spellsWindow;

    private readonly ImagePanel _characterButtonContainer;
    private readonly Button _characterButton;
    private readonly CharacterWindow _characterWindow;

    private readonly ImagePanel _questsButtonContainer;
    private readonly Button _questsButton;
    private readonly QuestsWindow _questsWindow;

    private readonly ImagePanel _friendsButtonContainer;
    private readonly Button _friendsButton;
    private readonly FriendsWindow _friendsWindow;

    private readonly ImagePanel _partyButtonContainer;
    private readonly Button _partyButton;
    private readonly PartyWindow _partyWindow;

    private readonly ImagePanel _guildButtonContainer;
    private readonly Button _guildButton;
    private readonly GuildWindow _guildWindow;

    private readonly FactionWindow _factionWindow;

    private readonly ConquestWindow _conquestWindow;

    private readonly ImagePanel _wingsButtonContainer;
    private readonly Button _wingsButton;

    private readonly ImagePanel _escapeMenuButtonContainer;
    private readonly Button _escapeMenuButton;
    private readonly JobsWindow mJobsWindow;
    private readonly ImagePanel mJobsBackground;
    private readonly Button mJobsButton;
    private readonly MapItemWindow _mapItemWindow;

    public MenuContainer(Canvas gameCanvas) : base(parent: gameCanvas, name: nameof(MenuContainer))
    {
        Alignment = [Alignments.Bottom, Alignments.Right];
        AlignmentPadding = new Padding { Bottom = 4, Right = 4 };
        Padding = new Padding(size: 4);
        RestrictToParent = true;
        ShouldCacheToTexture = true;

        _inventoryButtonContainer = new ImagePanel(parent: this, name: nameof(_inventoryButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _inventoryButton = new Button(parent: _inventoryButtonContainer, name: nameof(_inventoryButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _inventoryButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "inventoryicon.png");
        _inventoryButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "inventoryicon_hovered.png");
        _inventoryButton.SetToolTipText(text: Strings.GameMenu.Items);
        _inventoryButton.Clicked += InventoryButton_Clicked;

        _spellsButtonContainer = new ImagePanel(parent: this, name: nameof(_spellsButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _spellsButton = new Button(parent: _spellsButtonContainer, name: nameof(_spellsButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _spellsButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "spellicon.png");
        _spellsButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "spellicon_hovered.png");
        _spellsButton.SetToolTipText(text: Strings.GameMenu.Spells);
        _spellsButton.Clicked += SpellsButton_Clicked;

        _characterButtonContainer = new ImagePanel(parent: this, name: nameof(_characterButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _characterButton = new Button(parent: _characterButtonContainer, name: nameof(_characterButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _characterButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "charactericon.png");
        _characterButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "charactericon_hovered.png");
        _characterButton.SetToolTipText(text: Strings.GameMenu.Character);
        _characterButton.Clicked += CharacterButton_Clicked;

        _questsButtonContainer = new ImagePanel(parent: this, name: nameof(_questsButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _questsButton = new Button(parent: _questsButtonContainer, name: nameof(_questsButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _questsButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "questsicon.png");
        _questsButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "questsicon_hovered.png");
        _questsButton.SetToolTipText(text: Strings.GameMenu.Quest);
        _questsButton.Clicked += QuestBtn_Clicked;

        _friendsButtonContainer = new ImagePanel(parent: this, name: nameof(_friendsButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _friendsButton = new Button(parent: _friendsButtonContainer, name: nameof(_friendsButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _friendsButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "friendsicon.png");
        _friendsButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "friendsicon_hovered.png");
        _friendsButton.SetToolTipText(text: Strings.GameMenu.Friends);
        _friendsButton.Clicked += FriendsBtn_Clicked;

        _partyButtonContainer = new ImagePanel(parent: this, name: nameof(_partyButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _partyButton = new Button(parent: _partyButtonContainer, name: nameof(_partyButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _partyButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "partyicon.png");
        _partyButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "partyicon_hovered.png");
        _partyButton.SetToolTipText(text: Strings.GameMenu.Party);
        _partyButton.Clicked += PartyBtn_Clicked;

        _guildButtonContainer = new ImagePanel(parent: this, name: nameof(_guildButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _guildButton = new Button(parent: _guildButtonContainer, name: nameof(_guildButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _guildButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "guildicon.png");
        _guildButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "guildicon_hovered.png");
        _guildButton.SetToolTipText(text: Strings.Guilds.Guild);
        _guildButton.Clicked += GuildBtn_Clicked;

        _factionWindow = new FactionWindow(gameCanvas) { IsHidden = true };

        _conquestWindow = new ConquestWindow(gameCanvas) { IsHidden = true };

        _wingsButtonContainer = new ImagePanel(parent: this, name: nameof(_wingsButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _wingsButton = new Button(parent: _wingsButtonContainer, name: nameof(_wingsButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _wingsButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "wingicon.png");
        _wingsButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "wingicon_hovered.png");
        _wingsButton.SetToolTipText(text: "Toggle Wings");
        _wingsButton.Clicked += (s, e) =>
        {
            if (Globals.Me == null)
            {
                return;
            }

            var newState = Globals.Me.Wings == WingState.On ? WingState.Off : WingState.On;
            Globals.Me.Wings = newState;
            PacketSender.SendToggleWings(newState);
        };

        _escapeMenuButtonContainer = new ImagePanel(parent: this, name: nameof(_escapeMenuButtonContainer))
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        _escapeMenuButton = new Button(parent: _escapeMenuButtonContainer, name: nameof(_escapeMenuButton), disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        _escapeMenuButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "menuicon.png");
        _escapeMenuButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "menuicon_hovered.png");
        _escapeMenuButton.SetToolTipText(text: Strings.GameMenu.Menu);
        _escapeMenuButton.Clicked += EscapeMenuButtonClicked;
        // ... (código existente de inicialización de otros botones)

        mJobsBackground = new ImagePanel(parent: this, name: "JobsContainer")
        {
            Dock = Pos.Left,
            MaximumSize = new Point(x: 36, y: 36),
            MinimumSize = new Point(x: 36, y: 36),
            Padding = new Padding(size: 2),
            Size = new Point(x: 36, y: 36),
            TextureFilename = "menuitem.png",
        };
        mJobsButton = new Button(parent: mJobsBackground, name: "JobsButton", disableText: true)
        {
            Alignment = [Alignments.Center],
            Size = new Point(x: 32, y: 32),
        };
        mJobsButton.SetStateTexture(componentState: ComponentState.Normal, textureName: "menuicon.png");
        mJobsButton.SetStateTexture(componentState: ComponentState.Hovered, textureName: "menuicon_hovered.png");
        mJobsButton.SetToolTipText(Strings.GameMenu.Jobs);
        mJobsButton.Clicked += JobsButton_Clicked;

        var x = 0;
        foreach (var child in Children)
        {
            if (x > 0)
            {
                child.Margin = child.Margin with { Left = 4 };
            }
            child.X = x + child.Margin.Left;
            x += child.OuterWidth;
        }

        SizeToChildren(recursive: true);

        SkipRender();
        RunOnMainThread(
            action: () =>
            {
                LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer!.GetResolutionString());
            }
        );

        _partyWindow = new PartyWindow(gameCanvas: gameCanvas);
        _friendsWindow = new FriendsWindow(gameCanvas: gameCanvas);
        _inventoryWindow = new InventoryWindow(gameCanvas: gameCanvas);
        _spellsWindow = new SpellsWindow(gameCanvas: gameCanvas);
        _characterWindow = new CharacterWindow(gameCanvas: gameCanvas);
        _questsWindow = new QuestsWindow(gameCanvas: gameCanvas);
        _mapItemWindow = new MapItemWindow(gameCanvas: gameCanvas);
        _guildWindow = new GuildWindow(gameCanvas: gameCanvas);
        mJobsWindow= new JobsWindow(gameCanvas: gameCanvas);
    }

    //Methods
    public void Update(bool updateQuestLog)
    {
        _inventoryWindow.Update();
        _spellsWindow.Update();
        _characterWindow.Update();
        _partyWindow.Update();
        _friendsWindow.Update();
        _questsWindow.Update(updateQuestLog);
        _mapItemWindow.Update();
        _guildWindow.Update();
        mJobsWindow.Update();

    }

    public void UpdateFriendsList()
    {
        _friendsWindow.UpdateList();
    }

    public void UpdateGuildList()
    {
        _guildWindow.UpdateList();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void HideWindows()
    {
        if (!Globals.Database.HideOthersOnWindowOpen)
        {
            return;
        }

        _characterWindow.Hide();
        _friendsWindow.Hide();
        _inventoryWindow.Hide();
        _partyWindow.Hide();
        _questsWindow.Hide();
        _spellsWindow.Hide();
        _guildWindow.Hide();
        _factionWindow.Hide();
        _conquestWindow.Hide();
        mJobsWindow.Hide();
    }

    public void ToggleCharacterWindow(Player? player = null)
    {
        if (_characterWindow.IsVisible())
        {
            _characterWindow.Hide();
        }
        else
        {
            HideWindows();
            _characterWindow.SetPlayer(player);
            _characterWindow.Show();
        }
    }

    public bool ToggleFriendsWindow()
    {
        if (_friendsWindow.IsVisible)
        {
            _friendsWindow.Hide();
        }
        else
        {
            HideWindows();
            PacketSender.SendRequestFriends();
            _friendsWindow.UpdateList();
            _friendsWindow.Show();
        }

        return _friendsWindow.IsVisible;
    }

    public bool ToggleGuildWindow()
    {
        if (!_guildWindow.IsHidden)
        {
            _guildWindow.Hide();
        }
        else
        {
            HideWindows();
            PacketSender.SendRequestGuild();
            _guildWindow.UpdateList();
            _guildWindow.Show();
        }

        return !_guildWindow.IsHidden;
    }

    public void HideGuildWindow()
    {
        _guildWindow.Hide();
    }

    public void ToggleFactionWindow()
    {
        if (_factionWindow.IsVisibleInTree)
        {
            _factionWindow.Hide();
        }
        else
        {
            HideWindows();
            _factionWindow.Refresh();
            _factionWindow.Show();
        }
    }

    public void ToggleConquestWindow()
    {
        if (_conquestWindow.IsVisibleInTree)
        {
            _conquestWindow.Hide();
        }
        else
        {
            HideWindows();
            _conquestWindow.Refresh();
            _conquestWindow.Show();
        }
    }

    public void ToggleInventoryWindow()
    {
        if (_inventoryWindow.IsVisibleInTree)
        {
            _inventoryWindow.Hide();
        }
        else
        {
            HideWindows();
            _inventoryWindow.Show();
        }
    }

    public void OpenInventory()
    {
        _inventoryWindow.Show();
    }

    public InventoryWindow GetInventoryWindow()
    {
        return _inventoryWindow;
    }

    public void TogglePartyWindow()
    {
        if (_partyWindow.IsVisible())
        {
            _partyWindow.Hide();
        }
        else
        {
            HideWindows();
            _partyWindow.Show();
        }
    }

    public void ToggleQuestsWindow()
    {
        if (_questsWindow.IsVisible())
        {
            _questsWindow.Hide();
        }
        else
        {
            HideWindows();
            _questsWindow.Show();
        }
    }

    public void ToggleSimplifiedEscapeMenu()
    {
        Interface.GameUi.SimplifiedEscapeMenu.ToggleHidden(_escapeMenuButton);
    }

    public void ToggleSpellsWindow()
    {
        if (_spellsWindow.IsVisibleInTree)
        {
            _spellsWindow.Hide();
        }
        else
        {
            HideWindows();
            _spellsWindow.Show();
        }
    }

    public void CloseAllWindows()
    {
        _characterWindow.Hide();

        _friendsWindow.Hide();

        _inventoryWindow.Hide();

        _questsWindow.Hide();

        _spellsWindow.Hide();

        _partyWindow.Hide();

        _guildWindow.Hide();
        _factionWindow.Hide();
        _conquestWindow.Hide();
    }

    public bool HasWindowsOpen()
    {
        var windowsOpen = _characterWindow.IsVisible() ||
                          _friendsWindow.IsVisible ||
                          _inventoryWindow.IsVisibleInTree ||
                          _questsWindow.IsVisible() ||
                          _spellsWindow.IsVisibleInTree ||
                          _partyWindow.IsVisible() ||
                          _guildWindow.IsVisibleInTree ||
                          _factionWindow.IsVisibleInTree ||
                          _conquestWindow.IsVisibleInTree ||
        mJobsWindow.IsVisible();
        return windowsOpen;
    }

    public ConquestWindow ConquestWindow => _conquestWindow;

    //Input Handlers
    private void EscapeMenuButtonClicked(Base sender, MouseButtonState arguments)
    {
        var simplifiedEscapeMenuSetting = Globals.Database.SimplifiedEscapeMenu;

        if (simplifiedEscapeMenuSetting)
        {
            ToggleSimplifiedEscapeMenu();
        }
        else
        {
            Interface.GameUi.EscapeMenu.Show();
        }
    }

    private void PartyBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        TogglePartyWindow();
    }

    private void FriendsBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        ToggleFriendsWindow();
    }

    private void GuildBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        if (!string.IsNullOrEmpty(Globals.Me?.Guild))
        {
            ToggleGuildWindow();
        }
        else
        {
            ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Guilds.NotInGuild, CustomColors.Alerts.Error, ChatMessageType.Guild));
        }
    }

    private void QuestBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        ToggleQuestsWindow();
    }

    private void InventoryButton_Clicked(Base sender, MouseButtonState arguments)
    {
        ToggleInventoryWindow();
    }

    private void SpellsButton_Clicked(Base sender, MouseButtonState arguments)
    {
        ToggleSpellsWindow();
    }

    private void CharacterButton_Clicked(Base sender, MouseButtonState arguments)
    {
        ToggleCharacterWindow();
    }

    // Add the missing method 'JobsButton_Clicked' to handle the Clicked event for the mJobsButton.  
    private void JobsButton_Clicked(Base sender, MouseButtonState arguments)
    {
        if (mJobsWindow.IsVisible())
        {
            mJobsWindow.Hide();
        }
        else
        {
            HideWindows();
            mJobsWindow.Show();
        }
    }
}
