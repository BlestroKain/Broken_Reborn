using Intersect.Client.Framework.Gwen.Control;

namespace Intersect.Client.Interface.Game.DescriptionWindows.Components
{
    public class RowItemContainerComponent : ComponentBase
    {
        private int mXOffset = 0;

        public RowItemContainerComponent(Base parent, string name) : base(parent, name)
        {
            GenerateComponents();
        }

        protected override void GenerateComponents()
        {
            base.GenerateComponents();
            mContainer.SetSize(1, 40); // Altura fija para íconos
        }

        public void AddItemComponent(SetItemComponent item)
        {
            item.Container.SetPosition(mXOffset, 0);
            mContainer.AddChild(item.Container);
            mXOffset += item.Container.Width + 4; // Espaciado entre ítems
            mContainer.SetSize(mXOffset, mContainer.Height); // Expandir ancho
        }
    }
}
