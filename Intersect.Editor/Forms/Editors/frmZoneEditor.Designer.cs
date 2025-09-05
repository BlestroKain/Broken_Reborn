using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmZoneEditor
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZoneEditor));
            pnlContainer = new System.Windows.Forms.Panel();
            treeZones = new System.Windows.Forms.TreeView();
            txtName = new DarkTextBox();
            chkOverrideFlags = new System.Windows.Forms.CheckBox();
            grpFlags = new DarkGroupBox();
            chkOverrideModifiers = new System.Windows.Forms.CheckBox();
            grpModifiers = new DarkGroupBox();
            btnSave = new DarkButton();
            btnCancel = new DarkButton();
            toolStrip = new DarkToolStrip();
            toolStripItemNew = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripItemDelete = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            btnAlphabetical = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripItemCopy = new System.Windows.Forms.ToolStripButton();
            toolStripItemPaste = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripItemUndo = new System.Windows.Forms.ToolStripButton();
            pnlContainer.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            //
            // pnlContainer
            //
            pnlContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlContainer.Controls.Add(btnCancel);
            pnlContainer.Controls.Add(btnSave);
            pnlContainer.Controls.Add(grpModifiers);
            pnlContainer.Controls.Add(chkOverrideModifiers);
            pnlContainer.Controls.Add(grpFlags);
            pnlContainer.Controls.Add(chkOverrideFlags);
            pnlContainer.Controls.Add(txtName);
            pnlContainer.Controls.Add(treeZones);
            pnlContainer.Location = new System.Drawing.Point(0, 29);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new System.Drawing.Size(484, 392);
            pnlContainer.TabIndex = 1;
            //
            // treeZones
            //
            treeZones.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            treeZones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            treeZones.Dock = System.Windows.Forms.DockStyle.Left;
            treeZones.ForeColor = System.Drawing.Color.Gainsboro;
            treeZones.Location = new System.Drawing.Point(0, 0);
            treeZones.Name = "treeZones";
            treeZones.Size = new System.Drawing.Size(200, 392);
            treeZones.TabIndex = 0;
            //
            // txtName
            //
            txtName.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtName.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtName.Location = new System.Drawing.Point(210, 10);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(260, 23);
            txtName.TabIndex = 1;
            //
            // chkOverrideFlags
            //
            chkOverrideFlags.AutoSize = true;
            chkOverrideFlags.Location = new System.Drawing.Point(210, 35);
            chkOverrideFlags.Name = "chkOverrideFlags";
            chkOverrideFlags.Size = new System.Drawing.Size(103, 19);
            chkOverrideFlags.TabIndex = 2;
            chkOverrideFlags.Text = "Override Flags";
            chkOverrideFlags.UseVisualStyleBackColor = true;
            //
            // grpFlags
            //
            grpFlags.Location = new System.Drawing.Point(210, 60);
            grpFlags.Name = "grpFlags";
            grpFlags.Size = new System.Drawing.Size(260, 150);
            grpFlags.TabIndex = 3;
            grpFlags.TabStop = false;
            grpFlags.Text = "Flags";
            //
            // chkOverrideModifiers
            //
            chkOverrideModifiers.AutoSize = true;
            chkOverrideModifiers.Location = new System.Drawing.Point(210, 215);
            chkOverrideModifiers.Name = "chkOverrideModifiers";
            chkOverrideModifiers.Size = new System.Drawing.Size(127, 19);
            chkOverrideModifiers.TabIndex = 4;
            chkOverrideModifiers.Text = "Override Modifiers";
            chkOverrideModifiers.UseVisualStyleBackColor = true;
            //
            // grpModifiers
            //
            grpModifiers.Location = new System.Drawing.Point(210, 240);
            grpModifiers.Name = "grpModifiers";
            grpModifiers.Size = new System.Drawing.Size(260, 120);
            grpModifiers.TabIndex = 5;
            grpModifiers.TabStop = false;
            grpModifiers.Text = "Modifiers";
            //
            // btnSave
            //
            btnSave.Location = new System.Drawing.Point(210, 365);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(80, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            //
            // btnCancel
            //
            btnCancel.Location = new System.Drawing.Point(300, 365);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            //
            // toolStrip
            //
            toolStrip.AutoSize = false;
            toolStrip.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            toolStrip.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripItemNew, toolStripSeparator1, toolStripItemDelete, toolStripSeparator2, btnAlphabetical, toolStripSeparator4, toolStripItemCopy, toolStripItemPaste, toolStripSeparator3, toolStripItemUndo });
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            toolStrip.Size = new System.Drawing.Size(484, 29);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            //
            // toolStripItemNew
            //
            toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemNew.Enabled = false;
            toolStripItemNew.Image = (System.Drawing.Image)resources.GetObject("toolStripItemNew.Image");
            toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemNew.Name = "toolStripItemNew";
            toolStripItemNew.Size = new System.Drawing.Size(23, 26);
            toolStripItemNew.Text = "New";
            //
            // toolStripSeparator1
            //
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            //
            // toolStripItemDelete
            //
            toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemDelete.Enabled = false;
            toolStripItemDelete.Image = (System.Drawing.Image)resources.GetObject("toolStripItemDelete.Image");
            toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemDelete.Name = "toolStripItemDelete";
            toolStripItemDelete.Size = new System.Drawing.Size(23, 26);
            toolStripItemDelete.Text = "Delete";
            //
            // toolStripSeparator2
            //
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            //
            // btnAlphabetical
            //
            btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btnAlphabetical.Enabled = false;
            btnAlphabetical.Image = (System.Drawing.Image)resources.GetObject("btnAlphabetical.Image");
            btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAlphabetical.Name = "btnAlphabetical";
            btnAlphabetical.Size = new System.Drawing.Size(23, 26);
            btnAlphabetical.Text = "Alphabetical";
            //
            // toolStripSeparator4
            //
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            //
            // toolStripItemCopy
            //
            toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemCopy.Enabled = false;
            toolStripItemCopy.Image = (System.Drawing.Image)resources.GetObject("toolStripItemCopy.Image");
            toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemCopy.Name = "toolStripItemCopy";
            toolStripItemCopy.Size = new System.Drawing.Size(23, 26);
            toolStripItemCopy.Text = "Copy";
            //
            // toolStripItemPaste
            //
            toolStripItemPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemPaste.Enabled = false;
            toolStripItemPaste.Image = (System.Drawing.Image)resources.GetObject("toolStripItemPaste.Image");
            toolStripItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemPaste.Name = "toolStripItemPaste";
            toolStripItemPaste.Size = new System.Drawing.Size(23, 26);
            toolStripItemPaste.Text = "Paste";
            //
            // toolStripSeparator3
            //
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            //
            // toolStripItemUndo
            //
            toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemUndo.Enabled = false;
            toolStripItemUndo.Image = (System.Drawing.Image)resources.GetObject("toolStripItemUndo.Image");
            toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemUndo.Name = "toolStripItemUndo";
            toolStripItemUndo.Size = new System.Drawing.Size(23, 26);
            toolStripItemUndo.Text = "Undo";
            //
            // FrmZoneEditor
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(484, 421);
            Controls.Add(pnlContainer);
            Controls.Add(toolStrip);
            Name = "FrmZoneEditor";
            Text = "Zone Editor";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.TreeView treeZones;
        private DarkTextBox txtName;
        private System.Windows.Forms.CheckBox chkOverrideFlags;
        private DarkGroupBox grpFlags;
        private System.Windows.Forms.CheckBox chkOverrideModifiers;
        private DarkGroupBox grpModifiers;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripItemCopy;
        private System.Windows.Forms.ToolStripButton toolStripItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripItemUndo;
    }
}
