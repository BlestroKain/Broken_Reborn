using System;
using System.Linq;
using Intersect.Framework.Core.GameObjects.Conditions.ConditionMetadata;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands.Conditions;

public partial class ConditionControl_BeastHasUnlock : UserControl
{
    public ConditionControl_BeastHasUnlock()
    {
        InitializeComponent();
    }

    public void SetupFormValues(BeastHasUnlockCondition condition)
    {
        cmbNpc.SelectedIndex = NPCDescriptor.ListIndex(condition.NpcId);
        cmbUnlock.SelectedItem = condition.Unlock;
        nudValue.Value = condition.Value;
    }

    public void SaveFormValues(BeastHasUnlockCondition condition)
    {
        condition.NpcId = NPCDescriptor.IdFromList(cmbNpc.SelectedIndex);
        condition.Unlock = (BestiaryUnlock)cmbUnlock.SelectedItem;
        condition.Value = (int)nudValue.Value;
    }

    public new void Show()
    {
        cmbNpc.Items.Clear();
        cmbNpc.Items.AddRange(NPCDescriptor.Names);
        cmbUnlock.Items.Clear();
        cmbUnlock.Items.AddRange(Enum.GetValues<BestiaryUnlock>().Cast<object>().ToArray());
        if (cmbNpc.Items.Count > 0 && cmbNpc.SelectedIndex < 0)
        {
            cmbNpc.SelectedIndex = 0;
        }
        if (cmbUnlock.Items.Count > 0 && cmbUnlock.SelectedIndex < 0)
        {
            cmbUnlock.SelectedIndex = 0;
        }
        base.Show();
    }
}
