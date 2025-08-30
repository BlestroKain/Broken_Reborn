using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Events.Commands;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands;

public partial class EventCommandSetAlignment : UserControl
{
    private readonly FrmEvent mEventEditor;

    private SetAlignmentCommand mMyCommand;

    public EventCommandSetAlignment(SetAlignmentCommand refCommand, FrmEvent editor)
    {
        InitializeComponent();
        mMyCommand = refCommand;
        mEventEditor = editor;
        InitLocalization();
        cmbAlignment.SelectedIndex = (int)mMyCommand.Desired;
        chkIgnoreCooldown.Checked = mMyCommand.IgnoreCooldown;
        chkIgnoreGuildLock.Checked = mMyCommand.IgnoreGuildLock;
    }

    private void InitLocalization()
    {
        grpSetAlignment.Text = Strings.EventSetAlignment.title;
        lblAlignment.Text = Strings.EventSetAlignment.label;
        cmbAlignment.Items.Clear();
        for (var i = 0; i < Strings.EventSetAlignment.alignments.Count; i++)
        {
            cmbAlignment.Items.Add(Strings.EventSetAlignment.alignments[i]);
        }

        chkIgnoreCooldown.Text = Strings.EventSetAlignment.IgnoreCooldown;
        chkIgnoreGuildLock.Text = Strings.EventSetAlignment.IgnoreGuildLock;
        btnSave.Text = Strings.EventSetAlignment.okay;
        btnCancel.Text = Strings.EventSetAlignment.cancel;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        mMyCommand.Desired = (Alignment)cmbAlignment.SelectedIndex;
        mMyCommand.IgnoreCooldown = chkIgnoreCooldown.Checked;
        mMyCommand.IgnoreGuildLock = chkIgnoreGuildLock.Checked;
        mEventEditor.FinishCommandEdit();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        mEventEditor.CancelCommandEdit();
    }
}
