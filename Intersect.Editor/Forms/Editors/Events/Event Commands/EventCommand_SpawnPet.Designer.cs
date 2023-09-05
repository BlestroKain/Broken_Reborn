using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandSpawnPet
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
            this.grpSpawnPet = new DarkUI.Controls.DarkGroupBox();
            this.cmbPet = new DarkUI.Controls.DarkComboBox();
            this.lblPet = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpEntitySpawn = new DarkUI.Controls.DarkGroupBox();
            this.chkDirRelative = new DarkUI.Controls.DarkCheckBox();
            this.pnlSpawnLoc = new System.Windows.Forms.Panel();
            this.lblRelativeLocation = new System.Windows.Forms.Label();
            this.cmbEntities = new DarkUI.Controls.DarkComboBox();
            this.lblEntity = new System.Windows.Forms.Label();
            this.grpSpawnPet.SuspendLayout();
            this.grpEntitySpawn.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSpawnPet
            // 
            this.grpSpawnPet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSpawnPet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpawnPet.Controls.Add(this.cmbPet);
            this.grpSpawnPet.Controls.Add(this.lblPet);
            this.grpSpawnPet.Controls.Add(this.btnCancel);
            this.grpSpawnPet.Controls.Add(this.btnSave);
            this.grpSpawnPet.Controls.Add(this.grpEntitySpawn);
            this.grpSpawnPet.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpawnPet.Location = new System.Drawing.Point(4, 4);
            this.grpSpawnPet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSpawnPet.Name = "grpSpawnPet";
            this.grpSpawnPet.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSpawnPet.Size = new System.Drawing.Size(341, 432);
            this.grpSpawnPet.TabIndex = 17;
            this.grpSpawnPet.TabStop = false;
            this.grpSpawnPet.Text = "Spawn Pet";
            // 
            // cmbPet
            // 
            this.cmbPet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbPet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPet.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbPet.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbPet.DrawDropdownHoverOutline = false;
            this.cmbPet.DrawFocusRectangle = false;
            this.cmbPet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPet.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbPet.FormattingEnabled = true;
            this.cmbPet.Location = new System.Drawing.Point(117, 18);
            this.cmbPet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPet.Name = "cmbPet";
            this.cmbPet.Size = new System.Drawing.Size(208, 23);
            this.cmbPet.TabIndex = 26;
            this.cmbPet.Text = null;
            this.cmbPet.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblPet
            // 
            this.lblPet.AutoSize = true;
            this.lblPet.Location = new System.Drawing.Point(8, 22);
            this.lblPet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPet.Name = "lblPet";
            this.lblPet.Size = new System.Drawing.Size(30, 16);
            this.lblPet.TabIndex = 25;
            this.lblPet.Text = "Pet:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(117, 382);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 382);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpEntitySpawn
            // 
            this.grpEntitySpawn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpEntitySpawn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEntitySpawn.Controls.Add(this.chkDirRelative);
            this.grpEntitySpawn.Controls.Add(this.pnlSpawnLoc);
            this.grpEntitySpawn.Controls.Add(this.lblRelativeLocation);
            this.grpEntitySpawn.Controls.Add(this.cmbEntities);
            this.grpEntitySpawn.Controls.Add(this.lblEntity);
            this.grpEntitySpawn.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEntitySpawn.Location = new System.Drawing.Point(11, 49);
            this.grpEntitySpawn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpEntitySpawn.Name = "grpEntitySpawn";
            this.grpEntitySpawn.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpEntitySpawn.Size = new System.Drawing.Size(315, 325);
            this.grpEntitySpawn.TabIndex = 24;
            this.grpEntitySpawn.TabStop = false;
            this.grpEntitySpawn.Text = "On/Around Entity";
            // 
            // chkDirRelative
            // 
            this.chkDirRelative.AutoSize = true;
            this.chkDirRelative.Location = new System.Drawing.Point(51, 290);
            this.chkDirRelative.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDirRelative.Name = "chkDirRelative";
            this.chkDirRelative.Size = new System.Drawing.Size(184, 20);
            this.chkDirRelative.TabIndex = 30;
            this.chkDirRelative.Text = "Relative to Entity Direction";
            // 
            // pnlSpawnLoc
            // 
            this.pnlSpawnLoc.Location = new System.Drawing.Point(51, 85);
            this.pnlSpawnLoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSpawnLoc.Name = "pnlSpawnLoc";
            this.pnlSpawnLoc.Size = new System.Drawing.Size(213, 197);
            this.pnlSpawnLoc.TabIndex = 29;
            this.pnlSpawnLoc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlSpawnLoc_MouseDown);
            // 
            // lblRelativeLocation
            // 
            this.lblRelativeLocation.AutoSize = true;
            this.lblRelativeLocation.Location = new System.Drawing.Point(49, 60);
            this.lblRelativeLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelativeLocation.Name = "lblRelativeLocation";
            this.lblRelativeLocation.Size = new System.Drawing.Size(114, 16);
            this.lblRelativeLocation.TabIndex = 28;
            this.lblRelativeLocation.Text = "Relative Location:";
            // 
            // cmbEntities
            // 
            this.cmbEntities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEntities.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEntities.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEntities.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEntities.DrawDropdownHoverOutline = false;
            this.cmbEntities.DrawFocusRectangle = false;
            this.cmbEntities.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEntities.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Items.AddRange(new object[] {
            "Retain Direction",
            "Up",
            "Down",
            "Left",
            "Right"});
            this.cmbEntities.Location = new System.Drawing.Point(99, 23);
            this.cmbEntities.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(160, 23);
            this.cmbEntities.TabIndex = 27;
            this.cmbEntities.Text = "Retain Direction";
            this.cmbEntities.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.Location = new System.Drawing.Point(49, 27);
            this.lblEntity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(42, 16);
            this.lblEntity.TabIndex = 22;
            this.lblEntity.Text = "Entity:";
            // 
            // EventCommandSpawnPet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpSpawnPet);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "EventCommandSpawnPet";
            this.Size = new System.Drawing.Size(356, 443);
            this.grpSpawnPet.ResumeLayout(false);
            this.grpSpawnPet.PerformLayout();
            this.grpEntitySpawn.ResumeLayout(false);
            this.grpEntitySpawn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpSpawnPet;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkGroupBox grpEntitySpawn;
        private DarkCheckBox chkDirRelative;
        private System.Windows.Forms.Panel pnlSpawnLoc;
        private DarkComboBox cmbEntities;
        private System.Windows.Forms.Label lblEntity;
        private System.Windows.Forms.Label lblRelativeLocation;
        private DarkComboBox cmbPet;
        private System.Windows.Forms.Label lblPet;
    }
}
