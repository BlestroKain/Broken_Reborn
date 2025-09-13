using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandGiveFactionHonor
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
            grpGiveHonor = new DarkGroupBox();
            lblSerolf = new System.Windows.Forms.Label();
            lblNidraj = new System.Windows.Forms.Label();
            nudSerolf = new DarkNumericUpDown();
            nudNidraj = new DarkNumericUpDown();
            btnCancel = new DarkButton();
            btnSave = new DarkButton();
            grpGiveHonor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSerolf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNidraj).BeginInit();
            SuspendLayout();
            // 
            // grpGiveHonor
            // 
            grpGiveHonor.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            grpGiveHonor.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpGiveHonor.Controls.Add(lblSerolf);
            grpGiveHonor.Controls.Add(nudSerolf);
            grpGiveHonor.Controls.Add(lblNidraj);
            grpGiveHonor.Controls.Add(nudNidraj);
            grpGiveHonor.Controls.Add(btnCancel);
            grpGiveHonor.Controls.Add(btnSave);
            grpGiveHonor.ForeColor = System.Drawing.Color.Gainsboro;
            grpGiveHonor.Location = new System.Drawing.Point(3, 3);
            grpGiveHonor.Name = "grpGiveHonor";
            grpGiveHonor.Size = new System.Drawing.Size(200, 150);
            grpGiveHonor.TabIndex = 18;
            grpGiveHonor.TabStop = false;
            grpGiveHonor.Text = "Give Faction Honor:";
            // 
            // lblSerolf
            // 
            lblSerolf.AutoSize = true;
            lblSerolf.Location = new System.Drawing.Point(6, 32);
            lblSerolf.Name = "lblSerolf";
            lblSerolf.Size = new System.Drawing.Size(44, 15);
            lblSerolf.TabIndex = 29;
            lblSerolf.Text = "Serolf";
            // 
            // lblNidraj
            // 
            lblNidraj.AutoSize = true;
            lblNidraj.Location = new System.Drawing.Point(6, 61);
            lblNidraj.Name = "lblNidraj";
            lblNidraj.Size = new System.Drawing.Size(45, 15);
            lblNidraj.TabIndex = 30;
            lblNidraj.Text = "Nidraj";
            // 
            // nudSerolf
            // 
            nudSerolf.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudSerolf.ForeColor = System.Drawing.Color.Gainsboro;
            nudSerolf.Location = new System.Drawing.Point(80, 30);
            nudSerolf.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudSerolf.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            nudSerolf.Name = "nudSerolf";
            nudSerolf.Size = new System.Drawing.Size(114, 23);
            nudSerolf.TabIndex = 28;
            nudSerolf.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudNidraj
            // 
            nudNidraj.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudNidraj.ForeColor = System.Drawing.Color.Gainsboro;
            nudNidraj.Location = new System.Drawing.Point(80, 59);
            nudNidraj.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudNidraj.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            nudNidraj.Name = "nudNidraj";
            nudNidraj.Size = new System.Drawing.Size(114, 23);
            nudNidraj.TabIndex = 31;
            nudNidraj.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(119, 121);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(5);
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 27;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(6, 121);
            btnSave.Name = "btnSave";
            btnSave.Padding = new System.Windows.Forms.Padding(5);
            btnSave.Size = new System.Drawing.Size(75, 23);
            btnSave.TabIndex = 26;
            btnSave.Text = "Ok";
            btnSave.Click += btnSave_Click;
            // 
            // EventCommandGiveFactionHonor
            // 
            BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Controls.Add(grpGiveHonor);
            Name = "EventCommandGiveFactionHonor";
            Size = new System.Drawing.Size(206, 156);
            grpGiveHonor.ResumeLayout(false);
            grpGiveHonor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSerolf).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNidraj).EndInit();
            ResumeLayout(false);
        }

        private DarkGroupBox grpGiveHonor;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkNumericUpDown nudSerolf;
        private DarkNumericUpDown nudNidraj;
        private System.Windows.Forms.Label lblSerolf;
        private System.Windows.Forms.Label lblNidraj;
    }
}
