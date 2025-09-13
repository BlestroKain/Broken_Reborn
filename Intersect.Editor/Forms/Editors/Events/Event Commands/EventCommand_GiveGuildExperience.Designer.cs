using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandGiveGuildExperience
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpGiveExperience = new DarkGroupBox();
            lblExperience = new System.Windows.Forms.Label();
            nudExperience = new DarkNumericUpDown();
            btnCancel = new DarkButton();
            btnSave = new DarkButton();
            grpGiveExperience.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudExperience).BeginInit();
            SuspendLayout();
            // 
            // grpGiveExperience
            // 
            grpGiveExperience.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            grpGiveExperience.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpGiveExperience.Controls.Add(lblExperience);
            grpGiveExperience.Controls.Add(nudExperience);
            grpGiveExperience.Controls.Add(btnCancel);
            grpGiveExperience.Controls.Add(btnSave);
            grpGiveExperience.ForeColor = System.Drawing.Color.Gainsboro;
            grpGiveExperience.Location = new System.Drawing.Point(3, 3);
            grpGiveExperience.Name = "grpGiveExperience";
            grpGiveExperience.Size = new System.Drawing.Size(168, 123);
            grpGiveExperience.TabIndex = 18;
            grpGiveExperience.TabStop = false;
            grpGiveExperience.Text = "Give Guild Experience:";
            // 
            // lblExperience
            // 
            lblExperience.AutoSize = true;
            lblExperience.Location = new System.Drawing.Point(6, 32);
            lblExperience.Name = "lblExperience";
            lblExperience.Size = new System.Drawing.Size(63, 15);
            lblExperience.TabIndex = 29;
            lblExperience.Text = "Amount:";
            // 
            // nudExperience
            // 
            nudExperience.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudExperience.ForeColor = System.Drawing.Color.Gainsboro;
            nudExperience.Location = new System.Drawing.Point(75, 30);
            nudExperience.Maximum = new decimal(new int[] { 410065408, 2, 0, 0 });
            nudExperience.Name = "nudExperience";
            nudExperience.Size = new System.Drawing.Size(87, 23);
            nudExperience.TabIndex = 28;
            nudExperience.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(87, 94);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(5);
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 27;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(6, 94);
            btnSave.Name = "btnSave";
            btnSave.Padding = new System.Windows.Forms.Padding(5);
            btnSave.Size = new System.Drawing.Size(75, 23);
            btnSave.TabIndex = 26;
            btnSave.Text = "Ok";
            btnSave.Click += btnSave_Click;
            // 
            // EventCommandGiveGuildExperience
            // 
            BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Controls.Add(grpGiveExperience);
            Name = "EventCommandGiveGuildExperience";
            Size = new System.Drawing.Size(176, 129);
            grpGiveExperience.ResumeLayout(false);
            grpGiveExperience.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudExperience).EndInit();
            ResumeLayout(false);
        }

        private DarkGroupBox grpGiveExperience;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkNumericUpDown nudExperience;
        private System.Windows.Forms.Label lblExperience;
    }
}
