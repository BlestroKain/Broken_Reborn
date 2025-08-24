using Intersect.Client.Framework.Gwen.Control;

namespace Intersect.Client.Interface.Game.DescriptionWindows.Components;

public partial class RowItemContainerComponent : ComponentBase
{
    private int _xOffset;

    public RowItemContainerComponent(Base parent, string name) : base(parent, name)
    {
        SetSize(1, 40); // Altura fija para íconos
    }

    public void AddItemComponent(SetItemComponent item)
    {
        item.SetPosition(_xOffset, 0);
        AddChild(item);
        _xOffset += item.Width + 4; // Espaciado entre ítems
        SetSize(_xOffset, Height); // Expandir ancho
    }
}
