using System;
using System.Windows.Forms;
using Intersect.Enums;
using Intersect.Editor.Localization;
using Intersect.Framework.Core.GameObjects.Events.Commands;
using Intersect.Editor.Forms.Editors.Events;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandGiveFactionHonor : UserControl
    {
        private readonly FrmEvent mEventEditor;
        private readonly GiveFactionHonorCommand mMyCommand;

        public EventCommandGiveFactionHonor(GiveFactionHonorCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mEventEditor = editor;
            mMyCommand = refCommand;
            InitLocalization();

            if (mMyCommand.Honor.TryGetValue(Factions.Serolf, out var serolf))
            {
                nudSerolf.Value = serolf;
            }

            if (mMyCommand.Honor.TryGetValue(Factions.Nidraj, out var nidraj))
            {
                nudNidraj.Value = nidraj;
            }
        }

        private void InitLocalization()
        {
            grpGiveHonor.Text = Strings.EventGiveFactionHonor.Title;
            lblSerolf.Text = Strings.EventCommandList.serolf;
            lblNidraj.Text = Strings.EventCommandList.nidraj;
            btnSave.Text = Strings.General.Okay;
            btnCancel.Text = Strings.General.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Honor[Factions.Serolf] = (int)nudSerolf.Value;
            mMyCommand.Honor[Factions.Nidraj] = (int)nudNidraj.Value;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
