using System;
using System.Windows.Forms;

using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects.Events.Commands;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{

    public partial class EventCommandChangeJobLevel : UserControl
    {

        private readonly FrmEvent mEventEditor;

        private ChangeJobLevelCommand mMyCommand;

        public EventCommandChangeJobLevel(ChangeJobLevelCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;

            cmbJob.Items.Clear();
            for (var x = 0; x < (int)Jobs.SkillCount; x++)
            {
                cmbJob.Items.Add(General.Globals.GetJobName(x));
            }

            nudLevel.Maximum = Options.MaxJobLevel;

            switch (mMyCommand.Jobtype)
            {
                case Jobs.Farming:
                    nudLevel.Value = mMyCommand.FarmingLevel;
                    break;
                case Jobs.Mining:
                    nudLevel.Value = mMyCommand.MiningLevel;
                    break;
                case Jobs.Fishing:
                    nudLevel.Value = mMyCommand.FishingLevel;
                    break;
                case Jobs.Lumberjack:
                    nudLevel.Value = mMyCommand.WoodcuttingLevel;
                    break;
                case Jobs.Hunter:
                    nudLevel.Value = mMyCommand.HuntingLevel;
                    break;
                case Jobs.Alquemy:
                    nudLevel.Value = mMyCommand.AlchemyLevel;
                    break;
                case Jobs.Smithing:
                    nudLevel.Value = mMyCommand.BlacksmithingLevel;
                    break;
                case Jobs.Cooking:
                    nudLevel.Value = mMyCommand.CookingLevel;
                    break;
                case Jobs.Crafting:
                    nudLevel.Value = mMyCommand.CraftingLevel;
                    break;
                default:
                    nudLevel.Value = 1; // O algún valor por defecto que desees.
                    break;
            }


            // Establecer el trabajo seleccionado en el combobox
            cmbJob.SelectedIndex = (int)mMyCommand.Jobtype;

            InitLocalization();
        }


        private void InitLocalization()
        {
            grpChangeLevel.Text = Strings.EventChangeLevel.title;
            lblLevel.Text = Strings.EventChangeLevel.label;
            btnSave.Text = Strings.EventChangeLevel.okay;
            btnCancel.Text = Strings.EventChangeLevel.cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Jobtype = (Jobs)cmbJob.SelectedIndex;
            switch (mMyCommand.Jobtype)
            {
                case Jobs.Farming:
                    mMyCommand.FarmingLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Mining:
                    mMyCommand.MiningLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Fishing:
                    mMyCommand.FishingLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Lumberjack:
                    mMyCommand.WoodcuttingLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Hunter:
                    mMyCommand.HuntingLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Alquemy:
                    mMyCommand.AlchemyLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Smithing:
                    mMyCommand.BlacksmithingLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Cooking:
                    mMyCommand.CookingLevel = (int)nudLevel.Value;
                    break;
                case Jobs.Crafting:
                    mMyCommand.CraftingLevel = (int)nudLevel.Value;
                    break;
            }
            mEventEditor.FinishCommandEdit();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void cmbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.Jobtype = (Jobs)cmbJob.SelectedIndex;

        }
    }

}
