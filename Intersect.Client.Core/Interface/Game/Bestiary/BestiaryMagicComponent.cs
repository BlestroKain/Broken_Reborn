using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiaryMagicComponent : ImagePanel
{
    private readonly Label _label;
    private bool _unlocked;

    public BestiaryMagicComponent(Base parent, string name = "BestiaryMagic") : base(parent, name)
    {
        _label = new Label(this)
        {
            Text = Strings.Bestiary.Locked
        };
    }

    public void Unlock()
    {
        _unlocked = true;
        _label.Text = Strings.Bestiary.Unlocked;
    }

    public void Lock()
    {
        _unlocked = false;
        _label.Text = Strings.Bestiary.Locked;
    }

    public void CorrectWidth()
    {
        if (Parent == null)
        {
            return;
        }

        SetSize(Parent.InnerWidth - 20, Height);
    }

    public void ShowItemTooltip(ItemDescriptor item)
    {
        Interface.GameUi.ItemDescriptionWindow.Show(item, 1);
    }
}
