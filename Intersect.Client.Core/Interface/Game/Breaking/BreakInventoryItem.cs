using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using System;
using System.Linq;

namespace Intersect.Client.Interface.Game.Breaking;

public class BreakInventoryItem : SlotItem
{
    private readonly BreakItemWindow _breakWindow;
    private readonly ImagePanel _icon;
    private readonly Label _quantityLabel;
    private ItemDescriptionWindow? _descWindow;

    public bool IsSelected { get; private set; }

    public BreakInventoryItem(BreakItemWindow breakWindow, Base parent, int slotIndex, ContextMenu contextMenu)
        : base(parent, nameof(BreakInventoryItem), slotIndex, contextMenu)
    {
        _breakWindow = breakWindow;

        TextureFilename = "inventoryitem.png";

        _icon = new ImagePanel(this, "BreakInventoryItemIcon");
        _icon.HoverEnter += Icon_HoverEnter;
        _icon.HoverLeave += Icon_HoverLeave;
        _icon.Clicked += Icon_Clicked;
        _icon.DoubleClicked += Icon_DoubleClicked;

        _quantityLabel = new Label(this, "Quantity")
        {
            Alignment = [Alignments.Bottom, Alignments.Right],
            BackgroundTemplateName = "quantity.png",
            FontName = "sourcesansproblack",
            FontSize = 8,
            Padding = new Padding(2),
        };

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        if (Globals.Me is { } player)
        {
            player.InventoryUpdated += PlayerOnInventoryUpdated;
        }
    }

    private void PlayerOnInventoryUpdated(Player player, int slotIndex)
    {
        if (player != Globals.Me)
        {
            return;
        }

        if (slotIndex != SlotIndex)
        {
            return;
        }

        if (Globals.Me.Inventory[SlotIndex] == default)
        {
            return;
        }

        // empty texture to reload on update
        Icon.Texture = default;
    }

    public override void Update()
    {
        if (Globals.Me?.Inventory[SlotIndex] is not { } inventorySlot)
        {
            _reset();
            return;
        }

        if (!ItemDescriptor.TryGet(inventorySlot.ItemId, out var descriptor))
        {
            _reset();
            return;
        }

        var isDragging = Icon.IsDragging;

        _quantityLabel.IsVisibleInParent = !isDragging && descriptor.IsStackable && inventorySlot.Quantity > 1;
        if (_quantityLabel.IsVisibleInParent)
        {
            _quantityLabel.Text = Strings.FormatQuantityAbbreviated(inventorySlot.Quantity);
        }

        if (Icon.TextureFilename == descriptor.Icon) return;

        var itemTexture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
        if (itemTexture != null)
        {
            Icon.Texture = itemTexture;
            Icon.RenderColor = descriptor.Color;
            Icon.IsVisibleInParent = true;
        }
        else
        {
            _reset();
        }
    }
    private void Icon_HoverEnter(Base? sender, EventArgs? arguments)
    {

        if (Globals.Me?.Inventory[SlotIndex] is not { } item || item.Descriptor == null)
            return;

        // Si la ventana fue disposeada por Gwen (al cambiar de personaje, etc), recrea
        if (Interface.GameUi.ItemDescriptionWindow == null)
        {
            Interface.GameUi.ItemDescriptionWindow = new ItemDescriptionWindow();
        }

        var desc = Interface.GameUi.ItemDescriptionWindow;
        desc.Show(item.Descriptor, item.Quantity, item.ItemProperties);
    }

    private void Icon_HoverLeave(Base? sender, EventArgs? arguments)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }


    private void Icon_Clicked(Base sender, MouseButtonState args)
    {
        if (args.MouseButton is MouseButton.Left && Globals.Me?.Inventory[SlotIndex] is Items.Item item)
        {
            _breakWindow.SelectItem(item);
        }
    }

    private void Icon_DoubleClicked(Base sender, MouseButtonState args)
    {
        // Si necesitas una acción especial para doble clic, agrégala aquí
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        Icon.RenderColor = selected
            ? new Color(255, 255, 255, 255)
            : new Color(200, 200, 200, 255);
    }

    private void _reset()
    {
        Icon.Texture = null;
        Icon.IsVisibleInParent = false;
        _quantityLabel.IsVisibleInParent = false;
    }
}
