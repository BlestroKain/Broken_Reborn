using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.DragDrop;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Bag;
using Intersect.Client.Interface.Game.Bank;
using Intersect.Client.Interface.Game.Hotbar;
using Intersect.Client.Interface.Game.Mail;
using Intersect.Client.Interface.Game.Shop;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Configuration;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Market
{

    public sealed class SellInventoryItem : SlotItem
    {
        private readonly SellMarketWindow _owner;

        private readonly Label _qty;
        private bool _filterMatch = true;

        public SellInventoryItem(SellMarketWindow owner, Base parent, int slotIndex, ContextMenu contextMenu)
            : base(parent, nameof(SellInventoryItem), slotIndex, contextMenu)
        {
            _owner = owner;

            // Fondo opcional vía skin
            TextureFilename = "inventoryitem.png";

            // Icono y cantidad
       
            Icon.SetBounds(4, 4, 32, 32);

            _qty = new Label(this, "SellInvQty")
            {
                Alignment = [Alignments.Bottom, Alignments.Right],
                BackgroundTemplateName = "quantity.png",
                FontName = "sourcesansproblack",
                FontSize = 8,
                Padding = new Padding(2),
            };

            Icon.HoverEnter += Icon_HoverEnter;
            Icon.HoverLeave += Icon_HoverLeave;
            Icon.Clicked += Icon_Clicked;

            // Asegura tamaño != 0 antes de gridear
            SetSize(36, 36);

            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void Icon_HoverEnter(Base sender, EventArgs args)
        {
            if (Globals.Me?.Inventory[SlotIndex] is not { } slot) return;
            if (!ItemDescriptor.TryGet(slot.ItemId, out var desc)) return;

            Interface.GameUi.ItemDescriptionWindow?.Show(desc, slot.Quantity, slot.ItemProperties);
        }

        private void Icon_HoverLeave(Base sender, EventArgs args)
        {
            Interface.GameUi.ItemDescriptionWindow?.Hide();
        }

        private void Icon_Clicked(Base sender, MouseButtonState args)
        {
            if (args.MouseButton is MouseButton.Left)
            {
                _owner.SelectItem(this, SlotIndex);
            }
        }

        public override void Update()
        {
            if (!_filterMatch) { _reset(); return; }
            if (Globals.Me?.Inventory == null) { _reset(); return; }
            var slot = Globals.Me.Inventory[SlotIndex];
            if (slot == null || slot.ItemId == Guid.Empty) { _reset(); return; }
            if (!ItemDescriptor.TryGet(slot.ItemId, out var desc)) { _reset(); return; }

            // Render de icono
            var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, desc.Icon);
            if (tex == null) { _reset(); return; }

            Icon.Texture = tex;
            Icon.RenderColor = desc.Color;
            Icon.IsVisibleInParent = true;

            // Cantidad
            var dragging = Icon.IsDragging;
            var showQty = !dragging && desc.IsStackable && slot.Quantity > 1;
            _qty.IsVisibleInParent = showQty;
            if (showQty) _qty.Text = Intersect.Client.Localization.Strings.FormatQuantityAbbreviated(slot.Quantity);

            IsVisibleInParent = true;
        }

        private void _reset()
        {
            Icon.IsVisibleInParent = false;
            Icon.Texture = null;
            _qty.IsVisibleInParent = false;
            IsVisibleInParent = false;
        }

        public void SetFilterMatch(bool match)
        {
            _filterMatch = match;
        }
    }
}
