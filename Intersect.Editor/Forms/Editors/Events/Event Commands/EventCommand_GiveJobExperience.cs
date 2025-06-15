using Intersect.Editor.Localization;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Events.Commands;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandGiveJobExperience : UserControl
    {
        private readonly FrmEvent mEventEditor;
        private readonly GiveJobExperienceCommand mMyCommand;

        private JobType selectedJob;
        private long selectedExperience;

        // Diccionario para manejar las experiencias de los trabajos
        private readonly Dictionary<JobType, long> JobExperience = new Dictionary<JobType, long>();

        // Diccionario para mapear los índices del ComboBox con los valores del JobType
        private readonly Dictionary<int, JobType> ComboBoxJobMapping = new Dictionary<int, JobType>();

        public EventCommandGiveJobExperience(GiveJobExperienceCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            // Inicializar el combo box con los trabajos y mapear los índices
            InitializeComboBox();

            // Seleccionar valores iniciales
            if (selectedJob != JobType.None)
            {
                var selectedIndex = ComboBoxJobMapping.FirstOrDefault(x => x.Value == selectedJob).Key;
                cmbJob.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            }

            nudExperience.Value = selectedExperience;

            // Cargar localización
            InitLocalization();

            // Inicializar experiencias
            InitializeJobExperiences();

        }

        private void InitLocalization()
        {
            grpGiveExperience.Text = Strings.EventGiveExperience.Title;
          
          
        }

        private void InitializeComboBox()
        {
            cmbJob.Items.Clear();
            ComboBoxJobMapping.Clear();

            // Agregar una opción por defecto para evitar selección accidental
            cmbJob.Items.Add("-- Select a Job --");
            ComboBoxJobMapping[-1] = JobType.None;

            int comboIndex = 0;

            // Iterar manualmente desde 1 hasta `JobCount`
            for (int i = 1; i < (int)JobType.JobCount; i++)
            {
                JobType job = (JobType)i;
                ComboBoxJobMapping[comboIndex] = job;
                cmbJob.Items.Add(General.Globals.GetJobName(i));
                comboIndex++;
            }

            // Asegurar que el índice inicial sea el valor por defecto
            cmbJob.SelectedIndex = 0;
        }

        private void InitializeJobExperiences()
        {
            foreach (JobType job in Enum.GetValues(typeof(JobType)))
            {
                if (job != JobType.None && job != JobType.JobCount)
                {
                    JobExperience[job] = 0; // Inicializar todas las experiencias a 0
                }
            }
        }

        private void UpdateCommandPrinter()
        {
            string commandText = GetCommandText();
            //printerCommand.Text = commandText; // Actualizar el texto del comando en la interfaz
        }

        private string GetCommandText()
        {
            return $"Give {selectedExperience} EXP to {General.Globals.GetJobName((int)selectedJob)}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            long expAmount = (long)nudExperience.Value;
            JobType selectedJob = (JobType)cmbJob.SelectedIndex;

            if (selectedJob == JobType.None)
            {
                MessageBox.Show("Please select a valid job.");
                return;
            }

            // Limpiar la experiencia antes de asignar la nueva
            mMyCommand.JobExp.Clear(); // <-- Esto asegurará que solo se guarde la nueva experiencia

            // Guardar los datos en el comando
            mMyCommand.JobExp[selectedJob] = expAmount;

            // Actualizar el texto en la UI del evento
            UpdateCommandPrinter();

            // Guardar cambios
            mEventEditor.FinishCommandEdit();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void nudExperience_ValueChanged(object sender, EventArgs e)
        {
            selectedExperience = (long)nudExperience.Value;

            // Si ya hay una entrada para el trabajo seleccionado, actualizarla
            if (mMyCommand.JobExp.ContainsKey(selectedJob))
            {
                mMyCommand.JobExp[selectedJob] = selectedExperience;
            }

            // Forzar actualización del texto en UI
            UpdateCommandPrinter();
        }

        private void cmbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxJobMapping.TryGetValue(cmbJob.SelectedIndex, out var job))
            {
                selectedJob = job;
                UpdateCommandPrinter(); // Refrescar el texto del comando
            }
            else
            {
                selectedJob = JobType.None;
                MessageBox.Show("Job seleccionado no válido. Por favor, revisa el ComboBoxJobMapping.");
            }
        }


    }
}
