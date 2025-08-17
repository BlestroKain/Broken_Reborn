using System;
using Intersect.Client.Controllers;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.General;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Client.Interface.Game.Bestiary;

public sealed class BeastTile : ImagePanel
{
    public Guid NpcId { get; }
    private readonly ImagePanel _icon;
    private readonly Label _name;
    private readonly ImagePanel _lockOverlay;

    public event Action<Guid>? ClickedNpc;

    public BeastTile(Base parent, Guid npcId) : base(parent, nameof(BeastTile))
    {
        NpcId = npcId;

        MinimumSize = new Point(72, 92);
        Margin = new Margin(6);
        MouseInputEnabled = true;

        _icon = new ImagePanel(this, "Icon") { Alignment = [Alignments.Center] };
        _name = new Label(this, "Name") { Alignment = [Alignments.Bottom, Alignments.Center], FontName = "sourcesansproblack", FontSize = 9 };
        _lockOverlay = new ImagePanel(this, "Lock") { Alignment = [Alignments.Fill] };

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        if (NPCDescriptor.TryGet(npcId, out var desc))
        {
            var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Entity, desc.BestiaryIcon);
            _icon.Texture = tex;
            _name.Text = desc.HiddenUntilDefeated && !BestiaryController.HasUnlock(npcId, BestiaryUnlock.Discovery)
                ? "????"
                : desc.Name;
        }

        Clicked += OnClicked;
        RefreshState();
        BestiaryController.OnUnlockGained += OnUnlock;
    }

    private void OnUnlock(Guid npcId, BestiaryUnlock unlock)
    {
        if (npcId != NpcId) return;
        RefreshState();
    }

    private void OnClicked(Base sender, MouseButtonState e)
    {
        if (e.MouseButton is not MouseButton.Left) return;
        if (!BestiaryController.HasUnlock(NpcId, BestiaryUnlock.Discovery)) return;
        ClickedNpc?.Invoke(NpcId);
    }

    public void RefreshState()
    {
        var discovered = BestiaryController.HasUnlock(NpcId, BestiaryUnlock.Discovery);
        RenderColor = discovered ? Color.White : new Color(160, 160, 160, 255);
        _lockOverlay.IsVisibleInParent = !discovered;

        if (NPCDescriptor.TryGet(NpcId, out var desc))
        {
            _name.Text = desc.HiddenUntilDefeated && !discovered ? "????" : desc.Name;
        }
    }
}
