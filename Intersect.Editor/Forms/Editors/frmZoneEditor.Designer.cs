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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZoneEditor));
            pnlContainer = new Panel();
            btnCancel = new DarkButton();
            btnSave = new DarkButton();
            grpModifiers = new DarkGroupBox();
            chkOverrideModifiers = new CheckBox();
            grpFlags = new DarkGroupBox();
            chkOverrideFlags = new CheckBox();
            txtName = new DarkTextBox();
            treeZones = new TreeView();
            toolStrip = new DarkToolStrip();
            toolStripItemNew = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripItemDelete = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnAlphabetical = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripItemCopy = new ToolStripButton();
            toolStripItemPaste = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripItemUndo = new ToolStripButton();
            pnlContainer.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContainer.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            pnlContainer.Controls.Add(btnCancel);
            pnlContainer.Controls.Add(btnSave);
            pnlContainer.Controls.Add(grpModifiers);
            pnlContainer.Controls.Add(chkOverrideModifiers);
            pnlContainer.Controls.Add(grpFlags);
            pnlContainer.Controls.Add(chkOverrideFlags);
            pnlContainer.Controls.Add(txtName);
            pnlContainer.Controls.Add(treeZones);
            pnlContainer.ForeColor = SystemColors.Window;
            pnlContainer.Location = new System.Drawing.Point(0, 29);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(484, 392);
            pnlContainer.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(300, 365);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(5);
            btnCancel.Size = new Size(80, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(210, 365);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(5);
            btnSave.Size = new Size(80, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            // 
            // grpModifiers
            // 
            grpModifiers.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpModifiers.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpModifiers.ForeColor = System.Drawing.Color.Gainsboro;
            grpModifiers.Location = new System.Drawing.Point(210, 240);
            grpModifiers.Name = "grpModifiers";
            grpModifiers.Size = new Size(260, 120);
            grpModifiers.TabIndex = 5;
            grpModifiers.TabStop = false;
            grpModifiers.Text = "Modifiers";
            // 
            // chkOverrideModifiers
            // 
            chkOverrideModifiers.AutoSize = true;
            chkOverrideModifiers.Location = new System.Drawing.Point(210, 215);
            chkOverrideModifiers.Name = "chkOverrideModifiers";
            chkOverrideModifiers.Size = new Size(124, 19);
            chkOverrideModifiers.TabIndex = 4;
            chkOverrideModifiers.Text = "Override Modifiers";
            chkOverrideModifiers.UseVisualStyleBackColor = true;
            // 
            // grpFlags
            // 
            grpFlags.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpFlags.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpFlags.ForeColor = System.Drawing.Color.Gainsboro;
            grpFlags.Location = new System.Drawing.Point(210, 60);
            grpFlags.Name = "grpFlags";
            grpFlags.Size = new Size(260, 150);
            grpFlags.TabIndex = 3;
            grpFlags.TabStop = false;
            grpFlags.Text = "Flags";
            // 
            // chkOverrideFlags
            // 
            chkOverrideFlags.AutoSize = true;
            chkOverrideFlags.Location = new System.Drawing.Point(210, 35);
            chkOverrideFlags.Name = "chkOverrideFlags";
            chkOverrideFlags.Size = new Size(101, 19);
            chkOverrideFlags.TabIndex = 2;
            chkOverrideFlags.Text = "Override Flags";
            chkOverrideFlags.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            txtName.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtName.Location = new System.Drawing.Point(210, 10);
            txtName.Name = "txtName";
            txtName.Size = new Size(260, 23);
            txtName.TabIndex = 1;
            // 
            // treeZones
            // 
            treeZones.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            treeZones.BorderStyle = BorderStyle.None;
            treeZones.Dock = DockStyle.Left;
            treeZones.ForeColor = System.Drawing.Color.Gainsboro;
            treeZones.Location = new System.Drawing.Point(0, 0);
            treeZones.Name = "treeZones";
            treeZones.Size = new Size(200, 392);
            treeZones.TabIndex = 0;
            // 
            // toolStrip
            // 
            toolStrip.AutoSize = false;
            toolStrip.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            toolStrip.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripItemNew, toolStripSeparator1, toolStripItemDelete, toolStripSeparator2, btnAlphabetical, toolStripSeparator4, toolStripItemCopy, toolStripItemPaste, toolStripSeparator3, toolStripItemUndo });
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(6, 0, 1, 0);
            toolStrip.Size = new Size(484, 29);
            toolStrip.TabIndex = 52;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripItemNew
            // 
            toolStripItemNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemNew.Image = (Image)resources.GetObject("toolStripItemNew.Image");
            toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemNew.Name = "toolStripItemNew";
            toolStripItemNew.Size = new Size(23, 26);
            toolStripItemNew.Text = "New";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator1.Margin = new Padding(0, 0, 2, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 29);
            // 
            // toolStripItemDelete
            // 
            toolStripItemDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemDelete.Enabled = false;
            toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemDelete.Image = (Image)resources.GetObject("toolStripItemDelete.Image");
            toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemDelete.Name = "toolStripItemDelete";
            toolStripItemDelete.Size = new Size(23, 26);
            toolStripItemDelete.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator2.Margin = new Padding(0, 0, 2, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 29);
            // 
            // btnAlphabetical
            // 
            btnAlphabetical.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            btnAlphabetical.Image = (Image)resources.GetObject("btnAlphabetical.Image");
            btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAlphabetical.Name = "btnAlphabetical";
            btnAlphabetical.Size = new Size(23, 26);
            btnAlphabetical.Text = "Order Chronologically";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator4.Margin = new Padding(0, 0, 2, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 29);
            // 
            // toolStripItemCopy
            // 
            toolStripItemCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemCopy.Enabled = false;
            toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemCopy.Image = (Image)resources.GetObject("toolStripItemCopy.Image");
            toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemCopy.Name = "toolStripItemCopy";
            toolStripItemCopy.Size = new Size(23, 26);
            toolStripItemCopy.Text = "Copy";
            // 
            // toolStripItemPaste
            // 
            toolStripItemPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemPaste.Enabled = false;
            toolStripItemPaste.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemPaste.Image = (Image)resources.GetObject("toolStripItemPaste.Image");
            toolStripItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemPaste.Name = "toolStripItemPaste";
            toolStripItemPaste.Size = new Size(23, 26);
            toolStripItemPaste.Text = "Paste";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator3.Margin = new Padding(0, 0, 2, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 29);
            // 
            // toolStripItemUndo
            // 
            toolStripItemUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemUndo.Enabled = false;
            toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemUndo.Image = (Image)resources.GetObject("toolStripItemUndo.Image");
            toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemUndo.Name = "toolStripItemUndo";
            toolStripItemUndo.Size = new Size(23, 26);
            toolStripItemUndo.Text = "Undo";
            // 
            // FrmZoneEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize = new Size(484, 421);
            Controls.Add(toolStrip);
            Controls.Add(pnlContainer);
            ForeColor = SystemColors.ControlLightLight;
            Name = "FrmZoneEditor";
            Text = "Zone Editor";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
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
        private ToolStripButton toolStripItemNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripItemDelete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnAlphabetical;
        private ToolStripSeparator toolStripSeparator4;
        public ToolStripButton toolStripItemCopy;
        public ToolStripButton toolStripItemPaste;
        private ToolStripSeparator toolStripSeparator3;
        public ToolStripButton toolStripItemUndo;
    }
}
