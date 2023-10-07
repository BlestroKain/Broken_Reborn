using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandChangeJobLevel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpChangeLevel = new DarkUI.Controls.DarkGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbJob = new DarkUI.Controls.DarkComboBox();
            this.nudLevel = new DarkUI.Controls.DarkNumericUpDown();
            this.lblLevel = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpChangeLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // grpChangeLevel
            // 
            this.grpChangeLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeLevel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeLevel.Controls.Add(this.label1);
            this.grpChangeLevel.Controls.Add(this.cmbJob);
            this.grpChangeLevel.Controls.Add(this.nudLevel);
            this.grpChangeLevel.Controls.Add(this.lblLevel);
            this.grpChangeLevel.Controls.Add(this.btnCancel);
            this.grpChangeLevel.Controls.Add(this.btnSave);
            this.grpChangeLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeLevel.Location = new System.Drawing.Point(4, 4);
            this.grpChangeLevel.Margin = new System.Windows.Forms.Padding(4);
            this.grpChangeLevel.Name = "grpChangeLevel";
            this.grpChangeLevel.Padding = new System.Windows.Forms.Padding(4);
            this.grpChangeLevel.Size = new System.Drawing.Size(234, 144);
            this.grpChangeLevel.TabIndex = 17;
            this.grpChangeLevel.TabStop = false;
            this.grpChangeLevel.Text = "Change Level:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "Job Type:";
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
            this.cmbJob.Location = new System.Drawing.Point(79, 39);
            this.cmbJob.Name = "cmbJob";
            this.cmbJob.Size = new System.Drawing.Size(140, 23);
            this.cmbJob.TabIndex = 28;
            this.cmbJob.Text = "None";
            this.cmbJob.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbJob.SelectedIndexChanged += new System.EventHandler(this.cmbJob_SelectedIndexChanged);
            // 
            // nudLevel
            // 
            this.nudLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudLevel.Location = new System.Drawing.Point(79, 72);
            this.nudLevel.Margin = new System.Windows.Forms.Padding(4);
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(140, 22);
            this.nudLevel.TabIndex = 22;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(5, 74);
            this.lblLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(66, 16);
            this.lblLevel.TabIndex = 21;
            this.lblLevel.Text = "Set Level:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(119, 108);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 108);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommandChangeJobLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeLevel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EventCommandChangeJobLevel";
            this.Size = new System.Drawing.Size(245, 152);
            this.grpChangeLevel.ResumeLayout(false);
            this.grpChangeLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpChangeLevel;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private System.Windows.Forms.Label lblLevel;
        private DarkNumericUpDown nudLevel;
        private DarkComboBox cmbJob;
        private System.Windows.Forms.Label label1;
    }
}
