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
        private Jobs selectedJob;
        private long selectedExperience;
        public EventCommandGiveJobExperience(GiveJobExperienceCommand refCommand, FrmEvent editor)
        {

            InitializeComponent();
            InitLocalization();
            mMyCommand = refCommand;
            mEventEditor = editor;
          
            
            cmbJob.Items.Clear();
            for (var x = 0; x < (int)Jobs.SkillCount; x++)
            {
                cmbJob.Items.Add(General.Globals.GetJobName(x));
            }
        }
        private void InitLocalization()
        {
            grpGiveExperience.Text = Strings.EventGiveExperience.title;          
            btnSave.Text = Strings.EventGiveExperience.okay;
            btnCancel.Text = Strings.EventGiveExperience.cancel;
           
        }
            private void UpdateCommandPrinter()
        {
            // Actualizar el texto del "printer" de comandos con el valor devuelto por GetCommandText
            // Puedes llamar a este método cada vez que cambie el valor de experiencia
            string commandText = GetCommandText();
            // Actualiza el "printer" de comandos con el nuevo texto
            // printerCommand.Text = commandText;
        }
        private string GetCommandText()
        {
            // Devuelve el texto que se mostrará en el "printer" de comandos
            // en base a los valores de experiencia seleccionados en el formulario
            // Por ejemplo:
            // return $"Give Job Experience: {mMyCommand.FarmingExp} Farming, {mMyCommand.MiningExp} Mining, ...";
            return string.Empty;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ResetExperience();
            switch (selectedJob)
            {
                case Jobs.None:
                    return;
                case Jobs.Farming:
                    mMyCommand.FarmingExp = selectedExperience;
                    break;
                case Jobs.Mining:
                    mMyCommand.MiningExp = selectedExperience;
                    break;
                case Jobs.Fishing:
                    mMyCommand.FishingExp = selectedExperience;
                    break;
                case Jobs.Woodcutter:
                    mMyCommand.WoodExp = selectedExperience;
                    break;
                case Jobs.Hunter:
                    mMyCommand.HuntingExp = selectedExperience;
                    break;
                case Jobs.Alquemy:
                    mMyCommand.AlchemyExp = selectedExperience;
                    break;
                case Jobs.Smithing:
                    mMyCommand.BlacksmithExp = selectedExperience;
                    break;
                case Jobs.Cooking:
                    mMyCommand.CookingExp = selectedExperience;
                    break;
            

        }
            cmbJob.SelectedIndex = (int)selectedJob;
            nudExperience.Value = selectedExperience;
            UpdateCommandPrinter();
            mEventEditor.FinishCommandEdit();
        }
        private void ResetExperience()
        {
            // Reiniciar todas las experiencias a cero
            mMyCommand.FarmingExp = 0;
            mMyCommand.MiningExp = 0;
            mMyCommand.FishingExp = 0;
            mMyCommand.WoodExp = 0;
            mMyCommand.HuntingExp = 0;
            mMyCommand.AlchemyExp = 0;
            mMyCommand.BlacksmithExp = 0;
            mMyCommand.CookingExp = 0;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

            mEventEditor.CancelCommandEdit();
        }

        private void nudExperience_ValueChanged(object sender, EventArgs e)
        {
            selectedExperience = (long)nudExperience.Value;
        }

        private void cmbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedJob = (Jobs)cmbJob.SelectedIndex;
            ResetExperience();
        }
    }
}
