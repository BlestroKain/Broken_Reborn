using System;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Enchanting;

public partial class EnchantInventoryItem : SlotItem
{
    private readonly Label _cooldownLabel;
    private readonly Label _quantityLabel;
    private readonly ImagePanel _icon;
    private readonly EnchantItemWindow _enchantWindow;
    private readonly int _slotIndex;
    private ItemDescriptionWindow? _descWindow;
    public bool IsSelected { get; private set; }
    public EnchantInventoryItem(EnchantItemWindow enchantWindow, Base parent, int slotIndex, ContextMenu contextMenu) 
        : base(parent, nameof(EnchantInventoryItem), slotIndex,contextMenu)
    {
        _enchantWindow = enchantWindow;
        _slotIndex = slotIndex;
    
        TextureFilename = "inventoryitem.png";
        _icon = new ImagePanel(this, "EnchantInventoryItemIcon");
        _icon.HoverEnter += Icon_HoverEnter;
        _icon.HoverLeave += Icon_HoverLeave;
        _icon.Clicked += Icon_Clicked;
       
        _icon.DoubleClicked += Icon_DoubleClicked;

        _cooldownLabel = new Label(_icon, "EnchantInventoryItemCooldownLabel")
        {
            IsVisibleInParent = false,
            FontName = "sourcesansproblack",
            FontSize = 8,
            TextColor = new Color(0, 255, 255, 255),
            Alignment = [Alignments.Center],
            BackgroundTemplateName = "quantity.png",
            
        };
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
    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        Icon.RenderColor = selected
            ? new Color(255, 255, 255, 255)  // blanco total
            : new Color(200, 200, 200, 255); // más apagado por defecto
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
    private void Icon_HoverEnter(Base? sender, EventArgs? arguments)
    {

        if (Globals.Me?.Inventory[_slotIndex] is not { } item || item.Descriptor == null)
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


    private void Icon_Clicked(Base sender, MouseButtonState arguments)
    {
        if (arguments.MouseButton is MouseButton.Left)
        {
            if (Globals.Me?.Inventory[_slotIndex] is { } inventorySlot && inventorySlot.Descriptor != null)
            {
                if (inventorySlot is Items.Item item)
                {
                    _enchantWindow.SelectItem(item);
                }
            }
        }
        else if (arguments.MouseButton is MouseButton.Right)
        {
            if (Globals.Me?.Inventory[_slotIndex] is { } inventorySlot && inventorySlot.Descriptor != null)
            {
                if (inventorySlot is Items.Item item)
                {
                    _enchantWindow.SelectCurrencyItem(item);
                }
            }
        }
    }

  
    private void Icon_DoubleClicked(Base sender, MouseButtonState arguments)
    {
        // Implementar lógica de doble clic si es necesario
    }

    public override void Update()
    {
        if (Globals.Me == default)
        {
            return;
        }

        if (Globals.Me.Inventory[SlotIndex] is not { } inventorySlot)
        {
            return;
        }

        if (!ItemDescriptor.TryGet(inventorySlot.ItemId, out var descriptor))
        {
            _reset();
            return;
        }

        var equipped = Globals.Me.MyEquipment.Any(s => s == SlotIndex);
        var isDragging = Icon.IsDragging;

        _quantityLabel.IsVisibleInParent = !isDragging && descriptor.IsStackable && inventorySlot.Quantity > 1;
        if (_quantityLabel.IsVisibleInParent)
        {
            _quantityLabel.Text = Strings.FormatQuantityAbbreviated(inventorySlot.Quantity);
        }

        _cooldownLabel.IsVisibleInParent = !isDragging && Globals.Me.IsItemOnCooldown(SlotIndex);
        if (_cooldownLabel.IsVisibleInParent)
        {
            var itemCooldownRemaining = Globals.Me.GetItemRemainingCooldown(SlotIndex);
            _cooldownLabel.Text = TimeSpan.FromMilliseconds(itemCooldownRemaining).WithSuffix("0.0");
            Icon.RenderColor.A = 100;
        }
        else
        {
            Icon.RenderColor.A = descriptor.Color.A;
        }

        if (Icon.TextureFilename == descriptor.Icon)
        {
            return;
        }

        var itemTexture = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, descriptor.Icon);
        if (itemTexture != null)
        {
            Icon.Texture = itemTexture;
            Icon.RenderColor = Globals.Me.IsItemOnCooldown(SlotIndex)
                ? new Color(100, descriptor.Color.R, descriptor.Color.G, descriptor.Color.B)
                : descriptor.Color;
            Icon.IsVisibleInParent = true;
        }
        else
        {
            if (Icon.Texture != null)
            {
                Icon.Texture = null;
                Icon.IsVisibleInParent = false;
            }
        }

    }

    private void _reset()
    {
        Icon.IsVisibleInParent = false;
        Icon.Texture = default;
        _quantityLabel.IsVisibleInParent = false;
        _cooldownLabel.IsVisibleInParent = false;
    }
}
