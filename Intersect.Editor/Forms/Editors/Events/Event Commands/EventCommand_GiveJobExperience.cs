using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Editor.Localization;
using Intersect.GameObjects.Events.Commands;
using Intersect.Enums;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandGiveJobExperience : UserControl
    {
        private readonly FrmEvent mEventEditor;
        private GiveJobExperienceCommand mMyCommand;
        public EventCommandGiveJobExperience(GiveJobExperienceCommand refCommand, FrmEvent editor)
        {

            InitializeComponent();
            InitLocalization();
            mMyCommand = refCommand;
            mEventEditor = editor;
            cmbJob.Items.Clear();
            cmbJob.Items.AddRange(new object[] {
            "None",
            "Farming",
            "Mining",
            "Fishing",
            "Lumberjack",
            "Hunting",
            "Alchemy",
            "Blacksmith",
            "Cooking"});                     
        }
        private void InitLocalization()
        {
            grpGiveExperience.Text = Strings.EventGiveExperience.title;          
            btnSave.Text = Strings.EventGiveExperience.okay;
            btnCancel.Text = Strings.EventGiveExperience.cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (cmbJob.SelectedIndex)
            {
                case 1:
                    mMyCommand.FarmingExp = (long)nudExperience.Value;
                    break;
                case 2:
                    mMyCommand.MiningExp = (long)nudExperience.Value;
                    break;
                case 3:
                    mMyCommand.FishingExp = (long)nudExperience.Value;
                    break;
                case 4:
                    mMyCommand.WoodExp = (long)nudExperience.Value;
                    break;
                case 5:
                    mMyCommand.HuntingExp = (long)nudExperience.Value;
                    break;
                case 6:
                    mMyCommand.AlchemyExp = (long)nudExperience.Value;
                    break;
                case 7:
                    mMyCommand.BlacksmithExp = (long)nudExperience.Value;
                    break;
                case 8:
                    mMyCommand.CookingExp = (long)nudExperience.Value;
                    break;
            }
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            mEventEditor.CancelCommandEdit();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }
    }
}
