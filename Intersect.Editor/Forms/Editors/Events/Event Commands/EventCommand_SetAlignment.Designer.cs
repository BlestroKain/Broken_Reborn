using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandSetAlignment
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
            this.grpSetAlignment = new DarkGroupBox();
            this.cmbAlignment = new DarkComboBox();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.chkIgnoreCooldown = new DarkCheckBox();
            this.chkIgnoreGuildLock = new DarkCheckBox();
            this.btnCancel = new DarkButton();
            this.btnSave = new DarkButton();
            this.grpSetAlignment.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSetAlignment
            // 
            this.grpSetAlignment.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            this.grpSetAlignment.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.grpSetAlignment.Controls.Add(this.cmbAlignment);
            this.grpSetAlignment.Controls.Add(this.lblAlignment);
            this.grpSetAlignment.Controls.Add(this.chkIgnoreCooldown);
            this.grpSetAlignment.Controls.Add(this.chkIgnoreGuildLock);
            this.grpSetAlignment.Controls.Add(this.btnCancel);
            this.grpSetAlignment.Controls.Add(this.btnSave);
            this.grpSetAlignment.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetAlignment.Location = new System.Drawing.Point(3, 3);
            this.grpSetAlignment.Name = "grpSetAlignment";
            this.grpSetAlignment.Size = new System.Drawing.Size(176, 126);
            this.grpSetAlignment.TabIndex = 17;
            this.grpSetAlignment.TabStop = false;
            this.grpSetAlignment.Text = "Set Alignment";
            // 
            // cmbAlignment
            // 
            this.cmbAlignment.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            this.cmbAlignment.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.cmbAlignment.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAlignment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAlignment.FormattingEnabled = true;
            this.cmbAlignment.Items.AddRange(new object[] {
            "Neutral",
            "Serolf",
            "Nidraj"});
            this.cmbAlignment.Location = new System.Drawing.Point(68, 19);
            this.cmbAlignment.Name = "cmbAlignment";
            this.cmbAlignment.Size = new System.Drawing.Size(103, 21);
            this.cmbAlignment.TabIndex = 22;
            // 
            // lblAlignment
            // 
            this.lblAlignment.AutoSize = true;
            this.lblAlignment.Location = new System.Drawing.Point(4, 22);
            this.lblAlignment.Name = "lblAlignment";
            this.lblAlignment.Size = new System.Drawing.Size(58, 13);
            this.lblAlignment.TabIndex = 21;
            this.lblAlignment.Text = "Alignment:";
            // 
            // chkIgnoreCooldown
            // 
            this.chkIgnoreCooldown.AutoSize = true;
            this.chkIgnoreCooldown.Location = new System.Drawing.Point(7, 46);
            this.chkIgnoreCooldown.Name = "chkIgnoreCooldown";
            this.chkIgnoreCooldown.Size = new System.Drawing.Size(106, 17);
            this.chkIgnoreCooldown.TabIndex = 23;
            this.chkIgnoreCooldown.Text = "Ignore Cooldown";
            // 
            // chkIgnoreGuildLock
            // 
            this.chkIgnoreGuildLock.AutoSize = true;
            this.chkIgnoreGuildLock.Location = new System.Drawing.Point(7, 66);
            this.chkIgnoreGuildLock.Name = "chkIgnoreGuildLock";
            this.chkIgnoreGuildLock.Size = new System.Drawing.Size(116, 17);
            this.chkIgnoreGuildLock.TabIndex = 24;
            this.chkIgnoreGuildLock.Text = "Ignore Guild Lock";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(89, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 97);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommandSetAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.Controls.Add(this.grpSetAlignment);
            this.Name = "EventCommandSetAlignment";
            this.Size = new System.Drawing.Size(182, 132);
            this.grpSetAlignment.ResumeLayout(false);
            this.grpSetAlignment.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private DarkGroupBox grpSetAlignment;
        private DarkComboBox cmbAlignment;
        private System.Windows.Forms.Label lblAlignment;
        private DarkCheckBox chkIgnoreCooldown;
        private DarkCheckBox chkIgnoreGuildLock;
        private DarkButton btnCancel;
        private DarkButton btnSave;
    }
}
