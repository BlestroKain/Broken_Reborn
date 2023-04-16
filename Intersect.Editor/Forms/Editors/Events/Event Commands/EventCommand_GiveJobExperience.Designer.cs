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
            this.grpGiveExperience = new DarkUI.Controls.DarkGroupBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.cmbJob = new DarkUI.Controls.DarkComboBox();
            this.nudExperience = new DarkUI.Controls.DarkNumericUpDown();
            this.grpGiveExperience.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExperience)).BeginInit();
            this.SuspendLayout();
            // 
            // grpGiveExperience
            // 
            this.grpGiveExperience.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpGiveExperience.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGiveExperience.Controls.Add(this.cmbJob);
            this.grpGiveExperience.Controls.Add(this.nudExperience);
            this.grpGiveExperience.Controls.Add(this.btnCancel);
            this.grpGiveExperience.Controls.Add(this.btnSave);
            this.grpGiveExperience.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGiveExperience.Location = new System.Drawing.Point(3, 3);
            this.grpGiveExperience.Name = "grpGiveExperience";
            this.grpGiveExperience.Size = new System.Drawing.Size(168, 123);
            this.grpGiveExperience.TabIndex = 18;
            this.grpGiveExperience.TabStop = false;
            this.grpGiveExperience.Text = "Give Experience:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(87, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 94);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            // 
            // cmbJob
            // 
            this.cmbJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbJob.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbJob.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbJob.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbJob.DrawDropdownHoverOutline = false;
            this.cmbJob.DrawFocusRectangle = false;
            this.cmbJob.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJob.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbJob.FormattingEnabled = true;
            this.cmbJob.Items.AddRange(new object[] {
            "None",
            "Farming",
            "Mining",
            "Fishing",
            "Lumberjack",
            "Hunting",
            "Alchemy",
            "Blacksmith",
            "Cooking"});
            this.cmbJob.Location = new System.Drawing.Point(6, 32);
            this.cmbJob.Name = "cmbJob";
            this.cmbJob.Size = new System.Drawing.Size(156, 21);
            this.cmbJob.TabIndex = 27;
            this.cmbJob.Text = "None";
            this.cmbJob.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // nudExperience
            // 
            this.nudExperience.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudExperience.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudExperience.Location = new System.Drawing.Point(6, 59);
            this.nudExperience.Maximum = new decimal(new int[] {
            410065408,
            2,
            0,
            0});
            this.nudExperience.Name = "nudExperience";
            this.nudExperience.Size = new System.Drawing.Size(156, 20);
            this.nudExperience.TabIndex = 26;
            this.nudExperience.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // EventCommandGiveJobExperience
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.grpGiveExperience);
            this.Name = "EventCommandGiveJobExperience";
            this.Size = new System.Drawing.Size(176, 129);
            this.grpGiveExperience.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudExperience)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpGiveExperience;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbJob;
        private DarkUI.Controls.DarkNumericUpDown nudExperience;
    }
}
