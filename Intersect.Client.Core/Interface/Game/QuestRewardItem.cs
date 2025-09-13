using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game
{
    public sealed class QuestRewardItem
    {
        public ImagePanel Container { get; private set; }
        private readonly ImagePanel _icon;
        private readonly Label _qty;

        // Estilo base (el JSON puede sobrescribir)
        private const int CardW = 40;
        private const int CardH = 40;
        private const string QtyFont = "sourcesansproblack";
        private const int QtyFontSize = 8;

        private Guid _itemId;
        private long _quantity;

        public QuestRewardItem(IQuestWindow window, Guid itemId, long quantity)
        {
            _itemId = itemId;
            _quantity = quantity;

            Container = new ImagePanel(null, "RewardItem");
            Container.SetSize(CardW, CardH);

            _icon = new ImagePanel(Container, "RewardItemIcon")
            {
                Width = CardW,
                Height = CardH,
            };

            _qty = new Label(Container, "RewardItemValue")
            {
                FontName = QtyFont,
                FontSize = QtyFontSize,
                Alignment = [Alignments.Bottom, Alignments.Right],
                Padding = new Padding(2),
            };

            // Cargar skin si existe (puede ajustar tamaños/posiciones)
            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            // Hover con descripción
            _icon.HoverEnter += Icon_HoverEnter;
            _icon.HoverLeave += Icon_HoverLeave;

            // Añadir al contenedor de rewards
            window.AddRewardWidget(Container);

            // Dejar todo pintado
            Update();
        }

        /// <summary>
        /// Refresca el ítem (icono, color, cantidad, visibilidad del badge).
        /// Puedes pasar nuevos valores; si no, usa los actuales.
        /// </summary>
        public void Update(Guid? newItemId = null, long? newQuantity = null, bool force = false)
        {
            if (newItemId.HasValue) _itemId = newItemId.Value;
            if (newQuantity.HasValue) _quantity = newQuantity.Value;

            if (!ItemDescriptor.TryGet(_itemId, out var desc))
            {
                _icon.Texture = null;
                _icon.IsVisibleInParent = false;
                _qty.IsVisibleInParent = false;
                return;
            }

            // Icono y color
            var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, desc.Icon);
            if (tex != null && (force || _icon.Texture != tex))
            {
                _icon.Texture = tex;
            }
            _icon.RenderColor = desc.Color;
            _icon.IsVisibleInParent = tex != null;

            // Cantidad (muestra si >1 o si quieres mostrar siempre para rewards)
            var showQty = _quantity > 1 || desc.IsStackable;
            _qty.IsVisibleInParent = showQty;
            if (showQty)
            {
                var txt = Strings.FormatQuantityAbbreviated((int)Math.Max(1, _quantity));
                if (_qty.Text != txt) _qty.Text = txt;
            }

            // Fallback de tamaño/posición del badge si el JSON no los definió
            if ((_qty.Width == 0 || _qty.Height == 0) && Container.Width > 0 && Container.Height > 0)
            {
                _qty.SetSize(Math.Max(24, Container.Width / 2), 16);
                _qty.SetPosition(Container.Width - _qty.Width, Container.Height - _qty.Height);
            }
        }

        private void Icon_HoverEnter(Base sender, EventArgs e)
        {
            if (!ItemDescriptor.TryGet(_itemId, out var descriptor)) return;

            Interface.GameUi.ItemDescriptionWindow?.Show(
                descriptor,
                (int)Math.Max(1, _quantity),
                null
            );
        }

        private void Icon_HoverLeave(Base sender, EventArgs e)
        {
            Interface.GameUi.ItemDescriptionWindow?.Hide();
        }
    }
}
