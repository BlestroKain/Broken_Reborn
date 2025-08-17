using System;
using Intersect.Framework.Core.GameObjects.Conditions.ConditionMetadata;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands.Conditions;

public partial class ConditionControl_BeastsCompleted : UserControl
{
    public ConditionControl_BeastsCompleted()
    {
        InitializeComponent();
    }

    public void SetupFormValues(BeastsCompletedCondition condition)
    {
        cmbUnlock.SelectedIndex = (int)condition.Unlock;
        nudCount.Value = condition.Count;
    }

    public void SaveFormValues(BeastsCompletedCondition condition)
    {
        condition.Unlock = (BestiaryUnlock)cmbUnlock.SelectedIndex;
        condition.Count = (int)nudCount.Value;
    }

    public new void Show()
    {
        cmbUnlock.Items.Clear();
        cmbUnlock.Items.AddRange(Enum.GetNames<BestiaryUnlock>());
        if (cmbUnlock.Items.Count > 0 && cmbUnlock.SelectedIndex < 0)
        {
            cmbUnlock.SelectedIndex = 0;
        }
        base.Show();
    }
}
