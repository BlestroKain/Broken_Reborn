using System;
using System.Windows.Forms;
using Intersect.Editor.Localization;
using Intersect.Framework.Core.GameObjects.Events.Commands;
using Intersect.Editor.Forms.Editors.Events;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandGiveGuildExperience : UserControl
    {
        private readonly FrmEvent mEventEditor;
        private readonly GiveGuildExperienceCommand mMyCommand;

        public EventCommandGiveGuildExperience(GiveGuildExperienceCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            InitLocalization();
            nudExperience.Value = mMyCommand.Exp;
        }

        private void InitLocalization()
        {
            grpGiveExperience.Text = Strings.EventGiveGuildExperience.Title;
            lblExperience.Text = Strings.EventGiveGuildExperience.Label;
            btnSave.Text = Strings.General.Okay;
            btnCancel.Text = Strings.General.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Exp = (long)nudExperience.Value;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
