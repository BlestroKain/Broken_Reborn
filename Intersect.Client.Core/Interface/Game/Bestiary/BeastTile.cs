using Intersect.Client.Controllers;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Input;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Core;
using Intersect;
namespace Intersect.Client.Interface.Game.Bestiary;
public sealed class BeastTile : ImagePanel
{
    public Guid NpcId { get; }
    private readonly ImagePanel _icon;
    private readonly Label _name;
    private readonly ImagePanel _lockOverlay;

    private bool _filterMatch = true;

    public event Action<Guid>? ClickedNpc;

    public BeastTile(Base parent, Guid npcId) : base(parent, nameof(BeastTile))
    {
        NpcId = npcId;

        SetSize(84, 104);
        Margin = new Margin(6);
        Padding = new Padding(4);
        MouseInputEnabled = true;

        _icon = new ImagePanel(this)
        {
      
        };
        _icon.SetSize(64, 64);
        _icon.SetPosition(10, 6);

        _name = new Label(this)
        {
           
            FontName = "sourcesansproblack",
            FontSize = 9,
          
            Width = 84,
            Height = 22
        };

        _lockOverlay = new ImagePanel(this)
        {
            Texture = GameContentManager.Current.GetTexture(Intersect.Client.Framework.Content.TextureType.Gui, "lock_overlay"),
        
            IsVisibleInTree = false,
        };
        _lockOverlay.SetSize(32, 32);
        _lockOverlay.SetPosition(26, 36);

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        Clicked += OnClicked;
        HoverEnter += (_, _) => { if (IsDiscovered()) RenderColor = Color.Gray; };
        HoverLeave += (_, _) => RefreshState();

        BestiaryController.OnUnlockGained += OnUnlock;

        RefreshState();
    }

    private void OnUnlock(Guid npcId, BestiaryUnlock unlock)
    {
        if (npcId == NpcId)
        {
            RefreshState();
        }
    }

    private void OnClicked(Base sender, MouseButtonState e)
    {
        if (e.MouseButton is not MouseButton.Left) return;
        if (!IsDiscovered()) return;

        ClickedNpc?.Invoke(NpcId);
    }

    private bool IsDiscovered() =>
        BestiaryController.HasUnlock(NpcId, BestiaryUnlock.Discovery);

    public void RefreshState()
    {
        if (!NPCDescriptor.TryGet(NpcId, out var desc)) return;

        var discovered = IsDiscovered();
        RenderColor = discovered ? Color.White : new Color(180, 180, 180, 255);
        _lockOverlay.IsVisibleInTree = !discovered;

        _name.Text = desc.HiddenUntilDefeated && !discovered ? "????" : desc.Name;

        var tex = GameContentManager.Current.GetTexture(Intersect.Client.Framework.Content.TextureType.Entity, desc.BestiaryIcon);
        _icon.Texture = tex;
    }

    public  void Update()
    {
        if (!_filterMatch)
        {
            IsVisibleInParent = false;
            return;
        }

        IsVisibleInParent = true;
        RefreshState(); // Reaplica si hay cambios visuales
    }

    public void SetFilterMatch(bool match)
    {
        _filterMatch = match;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        BestiaryController.OnUnlockGained -= OnUnlock;
    }
}
