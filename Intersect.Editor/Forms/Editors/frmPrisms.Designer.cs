using DarkUI.Controls;
using System;
using System.Windows.Forms;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmPrisms
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstPrisms;
        private System.Windows.Forms.TextBox txtMapId;
        private DarkButton btnPickPos;
        private DarkNumericUpDown nudX;
        private DarkNumericUpDown nudY;
        private DarkNumericUpDown nudLevel;
        private System.Windows.Forms.DataGridView dgvWindows;
        private System.Windows.Forms.DataGridView dgvModules;
        private DarkNumericUpDown nudAreaX;
        private DarkNumericUpDown nudAreaY;
        private DarkNumericUpDown nudAreaW;
        private DarkNumericUpDown nudAreaH;
        private DarkButton btnAdd;
        private DarkButton btnDelete;
        private DarkButton btnSave;
        private DarkButton btnWindowAdd;
        private DarkButton btnWindowEdit;
        private DarkButton btnWindowDelete;
        private DarkButton btnModuleAdd;
        private DarkButton btnModuleDelete;
        private DarkButton btnAreaSelect;
        private System.Windows.Forms.Label lblMapId;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblWindows;
        private System.Windows.Forms.Label lblModules;
        private System.Windows.Forms.Label lblAreaX;
        private System.Windows.Forms.Label lblAreaY;
        private System.Windows.Forms.Label lblAreaW;
        private System.Windows.Forms.Label lblAreaH;
        private Intersect.Editor.Forms.Controls.MapPicker mapPicker;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrisms));
            lstPrisms = new ListBox();
            txtMapId = new TextBox();
            btnPickPos = new DarkButton();
            nudX = new DarkNumericUpDown();
            nudY = new DarkNumericUpDown();
            nudLevel = new DarkNumericUpDown();
            dgvWindows = new DataGridView();
            dgvModules = new DataGridView();
            nudAreaX = new DarkNumericUpDown();
            nudAreaY = new DarkNumericUpDown();
            nudAreaW = new DarkNumericUpDown();
            nudAreaH = new DarkNumericUpDown();
            btnAdd = new DarkButton();
            btnDelete = new DarkButton();
            btnSave = new DarkButton();
            btnWindowAdd = new DarkButton();
            btnWindowEdit = new DarkButton();
            btnWindowDelete = new DarkButton();
            btnModuleAdd = new DarkButton();
            btnModuleDelete = new DarkButton();
            btnAreaSelect = new DarkButton();
            lblMapId = new Label();
            lblX = new Label();
            lblY = new Label();
            lblLevel = new Label();
            lblWindows = new Label();
            lblModules = new Label();
            lblAreaX = new Label();
            lblAreaY = new Label();
            lblAreaW = new Label();
            lblAreaH = new Label();
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
            mapPicker = new Intersect.Editor.Forms.Controls.MapPicker();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvWindows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvModules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaH).BeginInit();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // lstPrisms
            // 
            lstPrisms.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lstPrisms.ForeColor = SystemColors.Window;
            lstPrisms.ItemHeight = 15;
            lstPrisms.Location = new System.Drawing.Point(12, 38);
            lstPrisms.Margin = new Padding(4, 3, 4, 3);
            lstPrisms.Name = "lstPrisms";
            lstPrisms.Size = new Size(233, 334);
            lstPrisms.TabIndex = 0;
            lstPrisms.SelectedIndexChanged += lstPrisms_SelectedIndexChanged;
            // 
            // txtMapId
            // 
            txtMapId.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            txtMapId.ForeColor = SystemColors.Window;
            txtMapId.Location = new System.Drawing.Point(257, 38);
            txtMapId.Margin = new Padding(4, 3, 4, 3);
            txtMapId.Name = "txtMapId";
            txtMapId.Size = new Size(233, 23);
            txtMapId.TabIndex = 1;
            //
            // btnPickPos
            //
            btnPickPos.Location = new System.Drawing.Point(497, 38);
            btnPickPos.Margin = new Padding(4, 3, 4, 3);
            btnPickPos.Name = "btnPickPos";
            btnPickPos.Size = new Size(60, 23);
            btnPickPos.TabIndex = 2;
            btnPickPos.Text = "...";
            btnPickPos.Click += btnPickPos_Click;
            //
            // nudX
            //
            nudX.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudX.ForeColor = System.Drawing.Color.Gainsboro;
            nudX.Location = new System.Drawing.Point(257, 72);
            nudX.Margin = new Padding(4, 3, 4, 3);
            nudX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudX.Name = "nudX";
            nudX.Size = new Size(93, 23);
            nudX.TabIndex = 3;
            nudX.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudY
            // 
            nudY.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudY.ForeColor = System.Drawing.Color.Gainsboro;
            nudY.Location = new System.Drawing.Point(257, 107);
            nudY.Margin = new Padding(4, 3, 4, 3);
            nudY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudY.Name = "nudY";
            nudY.Size = new Size(93, 23);
            nudY.TabIndex = 5;
            nudY.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudLevel
            // 
            nudLevel.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudLevel.ForeColor = System.Drawing.Color.Gainsboro;
            nudLevel.Location = new System.Drawing.Point(257, 141);
            nudLevel.Margin = new Padding(4, 3, 4, 3);
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new Size(93, 23);
            nudLevel.TabIndex = 7;
            nudLevel.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // dgvWindows
            //
            dgvWindows.AllowUserToAddRows = false;
            dgvWindows.BackgroundColor = System.Drawing.Color.FromArgb(60, 63, 65);
            dgvWindows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWindows.Location = new System.Drawing.Point(257, 176);
            dgvWindows.Name = "dgvWindows";
            dgvWindows.RowHeadersVisible = false;
            dgvWindows.Size = new Size(233, 92);
            dgvWindows.TabIndex = 9;
            var colDay = new DataGridViewComboBoxColumn();
            colDay.Name = "colDay";
            colDay.HeaderText = "Day";
            colDay.Items.AddRange((object[])Enum.GetValues(typeof(DayOfWeek)));
            var colStart = new DataGridViewTextBoxColumn();
            colStart.Name = "colStart";
            colStart.HeaderText = "Start";
            var colDuration = new DataGridViewTextBoxColumn();
            colDuration.Name = "colDuration";
            colDuration.HeaderText = "Duration";
            dgvWindows.Columns.AddRange(new DataGridViewColumn[] { colDay, colStart, colDuration });
            //
            // dgvModules
            //
            dgvModules.AllowUserToAddRows = false;
            dgvModules.BackgroundColor = System.Drawing.Color.FromArgb(60, 63, 65);
            dgvModules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvModules.Location = new System.Drawing.Point(257, 280);
            dgvModules.Name = "dgvModules";
            dgvModules.RowHeadersVisible = false;
            dgvModules.Size = new Size(233, 92);
            dgvModules.TabIndex = 11;
            var colType = new DataGridViewComboBoxColumn();
            colType.Name = "colType";
            colType.HeaderText = "Type";
            colType.Items.AddRange((object[])Enum.GetValues(typeof(PrismModuleType)));
            var colLevel = new DataGridViewTextBoxColumn();
            colLevel.Name = "colLevel";
            colLevel.HeaderText = "Level";
            dgvModules.Columns.AddRange(new DataGridViewColumn[] { colType, colLevel });
            //
            // btnWindowAdd
            //
            btnWindowAdd.Location = new System.Drawing.Point(496, 176);
            btnWindowAdd.Margin = new Padding(4, 3, 4, 3);
            btnWindowAdd.Name = "btnWindowAdd";
            btnWindowAdd.Size = new Size(75, 23);
            btnWindowAdd.TabIndex = 24;
            btnWindowAdd.Text = "Add";
            btnWindowAdd.Click += btnWindowAdd_Click;
            //
            // btnWindowEdit
            //
            btnWindowEdit.Location = new System.Drawing.Point(496, 205);
            btnWindowEdit.Margin = new Padding(4, 3, 4, 3);
            btnWindowEdit.Name = "btnWindowEdit";
            btnWindowEdit.Size = new Size(75, 23);
            btnWindowEdit.TabIndex = 25;
            btnWindowEdit.Text = "Edit";
            btnWindowEdit.Click += btnWindowEdit_Click;
            //
            // btnWindowDelete
            //
            btnWindowDelete.Location = new System.Drawing.Point(496, 234);
            btnWindowDelete.Margin = new Padding(4, 3, 4, 3);
            btnWindowDelete.Name = "btnWindowDelete";
            btnWindowDelete.Size = new Size(75, 23);
            btnWindowDelete.TabIndex = 26;
            btnWindowDelete.Text = "Delete";
            btnWindowDelete.Click += btnWindowDelete_Click;
            //
            // btnModuleAdd
            //
            btnModuleAdd.Location = new System.Drawing.Point(496, 280);
            btnModuleAdd.Margin = new Padding(4, 3, 4, 3);
            btnModuleAdd.Name = "btnModuleAdd";
            btnModuleAdd.Size = new Size(75, 23);
            btnModuleAdd.TabIndex = 27;
            btnModuleAdd.Text = "Add";
            btnModuleAdd.Click += btnModuleAdd_Click;
            //
            // btnModuleDelete
            //
            btnModuleDelete.Location = new System.Drawing.Point(496, 309);
            btnModuleDelete.Margin = new Padding(4, 3, 4, 3);
            btnModuleDelete.Name = "btnModuleDelete";
            btnModuleDelete.Size = new Size(75, 23);
            btnModuleDelete.TabIndex = 28;
            btnModuleDelete.Text = "Delete";
            btnModuleDelete.Click += btnModuleDelete_Click;
            //
            // btnAreaSelect
            //
            btnAreaSelect.Location = new System.Drawing.Point(496, 384);
            btnAreaSelect.Margin = new Padding(4, 3, 4, 3);
            btnAreaSelect.Name = "btnAreaSelect";
            btnAreaSelect.Size = new Size(75, 23);
            btnAreaSelect.TabIndex = 29;
            btnAreaSelect.Text = "Select";
            btnAreaSelect.Click += btnAreaSelect_Click;
            // 
            // nudAreaX
            // 
            nudAreaX.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAreaX.ForeColor = System.Drawing.Color.Gainsboro;
            nudAreaX.Location = new System.Drawing.Point(257, 384);
            nudAreaX.Margin = new Padding(4, 3, 4, 3);
            nudAreaX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAreaX.Name = "nudAreaX";
            nudAreaX.Size = new Size(93, 23);
            nudAreaX.TabIndex = 13;
            nudAreaX.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudAreaY
            // 
            nudAreaY.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAreaY.ForeColor = System.Drawing.Color.Gainsboro;
            nudAreaY.Location = new System.Drawing.Point(257, 418);
            nudAreaY.Margin = new Padding(4, 3, 4, 3);
            nudAreaY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAreaY.Name = "nudAreaY";
            nudAreaY.Size = new Size(93, 23);
            nudAreaY.TabIndex = 15;
            nudAreaY.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudAreaW
            // 
            nudAreaW.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAreaW.ForeColor = System.Drawing.Color.Gainsboro;
            nudAreaW.Location = new System.Drawing.Point(257, 453);
            nudAreaW.Margin = new Padding(4, 3, 4, 3);
            nudAreaW.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAreaW.Name = "nudAreaW";
            nudAreaW.Size = new Size(93, 23);
            nudAreaW.TabIndex = 17;
            nudAreaW.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudAreaH
            // 
            nudAreaH.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAreaH.ForeColor = System.Drawing.Color.Gainsboro;
            nudAreaH.Location = new System.Drawing.Point(257, 488);
            nudAreaH.Margin = new Padding(4, 3, 4, 3);
            nudAreaH.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAreaH.Name = "nudAreaH";
            nudAreaH.Size = new Size(93, 23);
            nudAreaH.TabIndex = 19;
            nudAreaH.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(12, 395);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new Padding(6, 6, 6, 6);
            btnAdd.Size = new Size(70, 27);
            btnAdd.TabIndex = 21;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(93, 395);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6, 6, 6, 6);
            btnDelete.Size = new Size(70, 27);
            btnDelete.TabIndex = 22;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(175, 395);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6, 6, 6, 6);
            btnSave.Size = new Size(70, 27);
            btnSave.TabIndex = 23;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // lblMapId
            // 
            lblMapId.AutoSize = true;
            lblMapId.Location = new System.Drawing.Point(502, 41);
            lblMapId.Margin = new Padding(4, 0, 4, 0);
            lblMapId.Name = "lblMapId";
            lblMapId.Size = new Size(44, 15);
            lblMapId.TabIndex = 2;
            lblMapId.Text = "Map Id";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Location = new System.Drawing.Point(362, 74);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 4;
            lblX.Text = "X";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Location = new System.Drawing.Point(362, 109);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(14, 15);
            lblY.TabIndex = 6;
            lblY.Text = "Y";
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Location = new System.Drawing.Point(362, 144);
            lblLevel.Margin = new Padding(4, 0, 4, 0);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(34, 15);
            lblLevel.TabIndex = 8;
            lblLevel.Text = "Level";
            // 
            // lblWindows
            // 
            lblWindows.AutoSize = true;
            lblWindows.Location = new System.Drawing.Point(502, 176);
            lblWindows.Margin = new Padding(4, 0, 4, 0);
            lblWindows.Name = "lblWindows";
            lblWindows.Size = new Size(56, 15);
            lblWindows.TabIndex = 10;
            lblWindows.Text = "Windows";
            // 
            // lblModules
            // 
            lblModules.AutoSize = true;
            lblModules.Location = new System.Drawing.Point(502, 280);
            lblModules.Margin = new Padding(4, 0, 4, 0);
            lblModules.Name = "lblModules";
            lblModules.Size = new Size(53, 15);
            lblModules.TabIndex = 12;
            lblModules.Text = "Modules";
            // 
            // lblAreaX
            // 
            lblAreaX.AutoSize = true;
            lblAreaX.Location = new System.Drawing.Point(362, 386);
            lblAreaX.Margin = new Padding(4, 0, 4, 0);
            lblAreaX.Name = "lblAreaX";
            lblAreaX.Size = new Size(41, 15);
            lblAreaX.TabIndex = 14;
            lblAreaX.Text = "Area X";
            // 
            // lblAreaY
            // 
            lblAreaY.AutoSize = true;
            lblAreaY.Location = new System.Drawing.Point(362, 421);
            lblAreaY.Margin = new Padding(4, 0, 4, 0);
            lblAreaY.Name = "lblAreaY";
            lblAreaY.Size = new Size(41, 15);
            lblAreaY.TabIndex = 16;
            lblAreaY.Text = "Area Y";
            // 
            // lblAreaW
            // 
            lblAreaW.AutoSize = true;
            lblAreaW.Location = new System.Drawing.Point(362, 455);
            lblAreaW.Margin = new Padding(4, 0, 4, 0);
            lblAreaW.Name = "lblAreaW";
            lblAreaW.Size = new Size(45, 15);
            lblAreaW.TabIndex = 18;
            lblAreaW.Text = "Area W";
            // 
            // lblAreaH
            // 
            lblAreaH.AutoSize = true;
            lblAreaH.Location = new System.Drawing.Point(362, 490);
            lblAreaH.Margin = new Padding(4, 0, 4, 0);
            lblAreaH.Name = "lblAreaH";
            lblAreaH.Size = new Size(43, 15);
            lblAreaH.TabIndex = 20;
            lblAreaH.Text = "Area H";
            //
            // mapPicker
            //
            mapPicker.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            mapPicker.Location = new System.Drawing.Point(502, 72);
            mapPicker.Margin = new Padding(4, 3, 4, 3);
            mapPicker.Name = "mapPicker";
            mapPicker.Size = new Size(300, 336);
            mapPicker.TabIndex = 27;
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
            toolStrip.Size = new Size(817, 29);
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
            // FrmPrisms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize = new Size(817, 519);
            Controls.Add(toolStrip);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(mapPicker);
            Controls.Add(lblAreaH);
            Controls.Add(nudAreaH);
            Controls.Add(lblAreaW);
            Controls.Add(nudAreaW);
            Controls.Add(lblAreaY);
            Controls.Add(nudAreaY);
            Controls.Add(lblAreaX);
            Controls.Add(nudAreaX);
            Controls.Add(btnAreaSelect);
            Controls.Add(btnModuleDelete);
            Controls.Add(btnModuleAdd);
            Controls.Add(btnWindowDelete);
            Controls.Add(btnWindowEdit);
            Controls.Add(btnWindowAdd);
            Controls.Add(lblModules);
            Controls.Add(dgvModules);
            Controls.Add(lblWindows);
            Controls.Add(dgvWindows);
            Controls.Add(lblLevel);
            Controls.Add(nudLevel);
            Controls.Add(lblY);
            Controls.Add(nudY);
            Controls.Add(lblX);
            Controls.Add(nudX);
            Controls.Add(lblMapId);
            Controls.Add(txtMapId);
            Controls.Add(btnPickPos);
            Controls.Add(lstPrisms);
            ForeColor = SystemColors.ControlLightLight;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmPrisms";
            Text = "Prisms";
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvWindows).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvModules).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaW).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAreaH).EndInit();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
