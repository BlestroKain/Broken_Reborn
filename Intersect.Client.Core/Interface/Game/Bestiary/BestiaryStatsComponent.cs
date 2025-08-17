using Intersect.Client.Framework.Gwen.Control;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiaryStatsComponent : ImagePanel
{
    private readonly Label _label;
    private bool _unlocked;

    public BestiaryStatsComponent(Base parent, string name = "BestiaryStats") : base(parent, name)
    {
        _label = new Label(this)
        {
            Text = "Locked"
        };
    }

    public void Unlock()
    {
        _unlocked = true;
        _label.Text = "Unlocked";
    }

    public void Lock()
    {
        _unlocked = false;
        _label.Text = "Locked";
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
