using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiaryStatsComponent : ImagePanel
{
    private readonly Label _label;
    private bool _unlocked;

    public BestiaryStatsComponent(Base parent, string name = "BestiaryStats") : base(parent, name)
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
}
