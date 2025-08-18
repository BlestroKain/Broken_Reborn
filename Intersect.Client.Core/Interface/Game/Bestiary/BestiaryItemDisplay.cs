using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Input;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Bestiary;

public class BestiaryItemDisplay : SlotItem
{
    private readonly Label _chanceLabel;

    public Guid ItemId { get; }
    public double Chance { get; }
    private readonly ImagePanel _icon;
    public BestiaryItemDisplay(Base parent, Guid itemId, double chance, ContextMenu? contextMenu = null)
        : base(parent, nameof(BestiaryItemDisplay), -1, contextMenu) // slotIndex = -1 porque no se basa en inventario real
    {
        ItemId = itemId;
        Chance = chance;

        TextureFilename = "inventoryitem.png";
        _icon = new ImagePanel(this, "DropItemIcon");
        _icon.HoverEnter += OnHoverEnter;
        _icon.HoverLeave += OnHoverLeave;
        _icon.Clicked += OnClick;
        _icon.SetSize(32, 32);
        _chanceLabel = new Label(this, "Chance")
        {
            Alignment = [Alignments.Bottom, Alignments.Right],
            BackgroundTemplateName = "quantity.png",
            FontName = "sourcesansproblack",
            FontSize = 8,
            Padding = new Padding(2),
        };

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        HoverEnter += OnHoverEnter;
        HoverLeave += OnHoverLeave;
        Clicked += OnClick;

        Update();
    }

    public override void Update()
    {
        if (!ItemDescriptor.TryGet(ItemId, out var descriptor))
        {
            Icon.Texture = null;
            Icon.IsVisibleInParent = false;
            _chanceLabel.IsVisibleInParent = false;
            return;
        }

        Icon.Texture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
        Icon.RenderColor = descriptor.Color;
        Icon.IsVisibleInParent = true;

        _chanceLabel.IsVisibleInParent = true;
        _chanceLabel.Text = $"{Chance:0.#}%";
    }

    private void OnHoverEnter(Base sender, EventArgs args)
    {
        if (!ItemDescriptor.TryGet(ItemId, out var descriptor)) return;

        Interface.GameUi.ItemDescriptionWindow ??= new ItemDescriptionWindow();
        Interface.GameUi.ItemDescriptionWindow.Show(descriptor, 1);
    }

    private void OnHoverLeave(Base sender, EventArgs args)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }

    private void OnClick(Base sender, MouseButtonState args)
    {
        if (args.MouseButton is MouseButton.Left && ItemDescriptor.TryGet(ItemId, out var descriptor))
        {
            Interface.GameUi.AppendChatboxItem(descriptor, new ItemProperties()); // No hay propiedades reales
        }
    }
}
