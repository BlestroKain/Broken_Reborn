using System;
using Intersect.Editor.Localization;
using Intersect.Framework.Core.GameObjects.Events.Commands;
using Intersect.Framework.Core.GameObjects.NPCs;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands;

public partial class EventCommandChangeBestiary : UserControl
{
    private readonly FrmEvent _eventEditor;
    private ChangeBestiaryCommand _command;

    public EventCommandChangeBestiary(ChangeBestiaryCommand refCommand, FrmEvent editor)
    {
        InitializeComponent();
        _command = refCommand;
        _eventEditor = editor;

        cmbNpc.Items.AddRange(NPCDescriptor.Names);
        cmbUnlock.Items.AddRange(Enum.GetNames<BestiaryUnlock>());
        cmbOperation.Items.AddRange(new object[] { Strings.EventChangeBestiary.add, Strings.EventChangeBestiary.remove });

        cmbNpc.SelectedIndex = NPCDescriptor.ListIndex(_command.NpcId);
        cmbUnlock.SelectedIndex = (int)_command.UnlockType;
        cmbOperation.SelectedIndex = _command.Add ? 0 : 1;
        nudAmount.Value = _command.Amount;

        InitLocalization();
    }

    private void InitLocalization()
    {
        grpChangeBestiary.Text = Strings.EventChangeBestiary.title;
        lblNpc.Text = Strings.EventChangeBestiary.beast;
        lblUnlock.Text = Strings.EventChangeBestiary.unlock;
        lblOperation.Text = Strings.EventChangeBestiary.operation;
        lblAmount.Text = Strings.EventChangeBestiary.amount;
        btnSave.Text = Strings.EventChangeBestiary.okay;
        btnCancel.Text = Strings.EventChangeBestiary.cancel;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _command.NpcId = NPCDescriptor.IdFromList(cmbNpc.SelectedIndex);
        _command.UnlockType = (BestiaryUnlock)cmbUnlock.SelectedIndex;
        _command.Add = cmbOperation.SelectedIndex == 0;
        _command.Amount = (int)nudAmount.Value;
        _eventEditor.FinishCommandEdit();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        _eventEditor.CancelCommandEdit();
    }
}
