namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandGiveJobExperience
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            grpGiveExperience = new DarkUI.Controls.DarkGroupBox();
            cmbJob = new DarkUI.Controls.DarkComboBox();
            nudExperience = new DarkUI.Controls.DarkNumericUpDown();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnSave = new DarkUI.Controls.DarkButton();
            grpGiveExperience.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudExperience).BeginInit();
            SuspendLayout();
            // 
            // grpGiveExperience
            // 
            grpGiveExperience.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            grpGiveExperience.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpGiveExperience.Controls.Add(cmbJob);
            grpGiveExperience.Controls.Add(nudExperience);
            grpGiveExperience.Controls.Add(btnCancel);
            grpGiveExperience.Controls.Add(btnSave);
            grpGiveExperience.ForeColor = System.Drawing.Color.Gainsboro;
            grpGiveExperience.Location = new System.Drawing.Point(3, 3);
            grpGiveExperience.Name = "grpGiveExperience";
            grpGiveExperience.Size = new Size(168, 123);
            grpGiveExperience.TabIndex = 18;
            grpGiveExperience.TabStop = false;
            grpGiveExperience.Text = "Give Job Experience:";
            // 
            // cmbJob
            // 
            cmbJob.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbJob.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbJob.BorderStyle = ButtonBorderStyle.Solid;
            cmbJob.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbJob.DrawDropdownHoverOutline = false;
            cmbJob.DrawFocusRectangle = false;
            cmbJob.DrawMode = DrawMode.OwnerDrawFixed;
            cmbJob.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJob.FlatStyle = FlatStyle.Flat;
            cmbJob.ForeColor = System.Drawing.Color.Gainsboro;
            cmbJob.FormattingEnabled = true;
            cmbJob.Location = new System.Drawing.Point(6, 32);
            cmbJob.Name = "cmbJob";
            cmbJob.Size = new Size(156, 24);
            cmbJob.TabIndex = 27;
            cmbJob.Text = "None";
            cmbJob.TextPadding = new Padding(2);
            cmbJob.SelectedIndexChanged += cmbJob_SelectedIndexChanged;
            // 
            // nudExperience
            // 
            nudExperience.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudExperience.ForeColor = System.Drawing.Color.Gainsboro;
            nudExperience.Location = new System.Drawing.Point(6, 59);
            nudExperience.Maximum = new decimal(new int[] { 410065408, 2, 0, 0 });
            nudExperience.Name = "nudExperience";
            nudExperience.Size = new Size(156, 23);
            nudExperience.TabIndex = 26;
            nudExperience.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudExperience.ValueChanged += nudExperience_ValueChanged;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(87, 94);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(5);
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 20;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(6, 94);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(5);
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 19;
            btnSave.Text = "Ok";
            btnSave.Click += btnSave_Click;
            // 
            // EventCommandGiveJobExperience
            // 
            BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Controls.Add(grpGiveExperience);
            Name = "EventCommandGiveJobExperience";
            Size = new Size(176, 129);
            grpGiveExperience.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudExperience).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpGiveExperience;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbJob;
        private DarkUI.Controls.DarkNumericUpDown nudExperience;
    }
}
