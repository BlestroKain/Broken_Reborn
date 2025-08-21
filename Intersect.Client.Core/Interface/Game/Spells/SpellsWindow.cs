using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellsWindow : Window
{
    public List<SpellItem> Items { get; private set; } = [];

    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly Label _lblSpellPoints;

    public SpellsWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Spells.Title, false, nameof(SpellsWindow))
    {
        DisableResizing();

        Alignment = [Alignments.Bottom, Alignments.Right];
        MinimumSize = new Point(340, 400);
        Margin = new Margin(0, 0, 15, 60);
        IsVisibleInTree = false;
        IsResizable = false;
        IsClosable = true;

        // Scrollable spell list
        _slotContainer = new ScrollControl(this)
        {
            Dock = Pos.Top,
            Margin = Margin.Two,
          
            OverflowX = OverflowBehavior.Hidden,
            OverflowY = OverflowBehavior.Scroll,
        };
        _slotContainer.SetSize (330, 320);
        // Context Menu for spells
        _contextMenu = new ContextMenu(gameCanvas)
        {
            IsVisibleInParent = false,
            IconMarginDisabled = true,
            ItemFont = GameContentManager.Current.GetFont("sourcesansproblack"),
            ItemFontSize = 10,
        };

        // Label for remaining spell points
        _lblSpellPoints = new Label(this)
        {
            Dock = Pos.Bottom,
            Padding = Padding.Five,
            AutoSizeToContents = true,
            Text = "Puntos de hechizo: 0",
        };
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
    }

    private void InitItemContainer()
    {
        Items.Clear();
        _slotContainer.DeleteAllChildren();

        for (var i = 0; i < Options.Instance.Player.MaxSpells; i++)
        {
            var spellItem = new SpellItem(this, _slotContainer, i, _contextMenu);
            Items.Add(spellItem);
        }
    }

    public void Update()
    {
        if (!IsVisibleInTree)
            return;

        var slotCount = Math.Min(Items.Count, Options.Instance.Player.MaxSpells);
        for (var i = 0; i < slotCount; i++)
        {
            Items[i].Update();
        }

        // Aquí puedes actualizar los puntos si tienes acceso
        // _lblSpellPoints.Text = $"Puntos de hechizo: {PlayerInfo.SpellPoints}";
    }

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
