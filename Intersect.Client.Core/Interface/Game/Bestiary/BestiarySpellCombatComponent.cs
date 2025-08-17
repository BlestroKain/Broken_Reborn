using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using System;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiarySpellCombatComponent : ImagePanel
{
    private readonly Label _label;
    private bool _unlocked;

    public BestiarySpellCombatComponent(Base parent, string name = "BestiarySpellCombat") : base(parent, name)
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

    public void ShowSpellTooltip(Guid spellId)
    {
        Interface.GameUi.SpellDescriptionWindow.Show(spellId);
    }
}
