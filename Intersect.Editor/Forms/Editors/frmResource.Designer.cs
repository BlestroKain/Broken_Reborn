using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmResource
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResource));
            this.grpResources = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.nudMaxHp = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMinHp = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSpawnDuration = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.lblMaxHp = new System.Windows.Forms.Label();
            this.lblSpawnDuration = new System.Windows.Forms.Label();
            this.chkWalkableAfter = new DarkUI.Controls.DarkCheckBox();
            this.chkWalkableBefore = new DarkUI.Controls.DarkCheckBox();
            this.cmbToolType = new DarkUI.Controls.DarkComboBox();
            this.lblToolType = new System.Windows.Forms.Label();
            this.lblHP = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.btnRequirements = new DarkUI.Controls.DarkButton();
            this.grpGraphics = new DarkUI.Controls.DarkGroupBox();
            this.chkExhaustedBelowEntities = new DarkUI.Controls.DarkCheckBox();
            this.chkInitialBelowEntities = new DarkUI.Controls.DarkCheckBox();
            this.chkExhaustedFromTileset = new DarkUI.Controls.DarkCheckBox();
            this.chkInitialFromTileset = new DarkUI.Controls.DarkCheckBox();
            this.exhaustedGraphicContainer = new System.Windows.Forms.Panel();
            this.picEndResource = new System.Windows.Forms.PictureBox();
            this.initalGraphicContainer = new System.Windows.Forms.Panel();
            this.picInitialResource = new System.Windows.Forms.PictureBox();
            this.cmbEndSprite = new DarkUI.Controls.DarkComboBox();
            this.lblPic2 = new System.Windows.Forms.Label();
            this.cmbInitialSprite = new DarkUI.Controls.DarkComboBox();
            this.lblPic = new System.Windows.Forms.Label();
            this.tmrRender = new System.Windows.Forms.Timer(this.components);
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.nudExpAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbJobType = new DarkUI.Controls.DarkComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpRequirements = new DarkUI.Controls.DarkGroupBox();
            this.lblCannotHarvest = new System.Windows.Forms.Label();
            this.txtCannotHarvest = new DarkUI.Controls.DarkTextBox();
            this.grpCommonEvent = new DarkUI.Controls.DarkGroupBox();
            this.cmbEvent = new DarkUI.Controls.DarkComboBox();
            this.lblEvent = new System.Windows.Forms.Label();
            this.grpRegen = new DarkUI.Controls.DarkGroupBox();
            this.nudHpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.lblHpRegen = new System.Windows.Forms.Label();
            this.lblRegenHint = new System.Windows.Forms.Label();
            this.grpDrops = new DarkUI.Controls.DarkGroupBox();
            this.btnDropRemove = new DarkUI.Controls.DarkButton();
            this.btnDropAdd = new DarkUI.Controls.DarkButton();
            this.lstDrops = new System.Windows.Forms.ListBox();
            this.nudDropAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDropChance = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbDropItem = new DarkUI.Controls.DarkComboBox();
            this.lblDropAmount = new System.Windows.Forms.Label();
            this.lblDropChance = new System.Windows.Forms.Label();
            this.lblDropItem = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.toolStrip = new DarkUI.Controls.DarkToolStrip();
            this.toolStripItemNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAlphabetical = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripItemPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemUndo = new System.Windows.Forms.ToolStripButton();
            this.gameObjectTypeExtensionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gameObjectTypeExtensionsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.LevelNud = new DarkUI.Controls.DarkNumericUpDown();
            this.LvlLbl = new System.Windows.Forms.Label();
            this.grpResources.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinHp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnDuration)).BeginInit();
            this.grpGraphics.SuspendLayout();
            this.exhaustedGraphicContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEndResource)).BeginInit();
            this.initalGraphicContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInitialResource)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.darkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExpAmount)).BeginInit();
            this.grpRequirements.SuspendLayout();
            this.grpCommonEvent.SuspendLayout();
            this.grpRegen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpRegen)).BeginInit();
            this.grpDrops.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropChance)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectTypeExtensionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectTypeExtensionsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LevelNud)).BeginInit();
            this.SuspendLayout();
            // 
            // grpResources
            // 
            this.grpResources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpResources.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpResources.Controls.Add(this.btnClearSearch);
            this.grpResources.Controls.Add(this.txtSearch);
            this.grpResources.Controls.Add(this.lstGameObjects);
            this.grpResources.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpResources.Location = new System.Drawing.Point(16, 48);
            this.grpResources.Margin = new System.Windows.Forms.Padding(4);
            this.grpResources.Name = "grpResources";
            this.grpResources.Padding = new System.Windows.Forms.Padding(4);
            this.grpResources.Size = new System.Drawing.Size(271, 538);
            this.grpResources.TabIndex = 14;
            this.grpResources.TabStop = false;
            this.grpResources.Text = "Resources";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(239, 23);
            this.btnClearSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnClearSearch.Size = new System.Drawing.Size(24, 25);
            this.btnClearSearch.TabIndex = 34;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(8, 23);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(222, 22);
            this.txtSearch.TabIndex = 33;
            this.txtSearch.Text = "Search...";
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // lstGameObjects
            // 
            this.lstGameObjects.AllowDrop = true;
            this.lstGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstGameObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstGameObjects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstGameObjects.HideSelection = false;
            this.lstGameObjects.ImageIndex = 0;
            this.lstGameObjects.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lstGameObjects.Location = new System.Drawing.Point(8, 55);
            this.lstGameObjects.Margin = new System.Windows.Forms.Padding(4);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(255, 475);
            this.lstGameObjects.TabIndex = 32;
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.LevelNud);
            this.grpGeneral.Controls.Add(this.LvlLbl);
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.nudMaxHp);
            this.grpGeneral.Controls.Add(this.nudMinHp);
            this.grpGeneral.Controls.Add(this.nudSpawnDuration);
            this.grpGeneral.Controls.Add(this.cmbAnimation);
            this.grpGeneral.Controls.Add(this.lblAnimation);
            this.grpGeneral.Controls.Add(this.lblMaxHp);
            this.grpGeneral.Controls.Add(this.lblSpawnDuration);
            this.grpGeneral.Controls.Add(this.chkWalkableAfter);
            this.grpGeneral.Controls.Add(this.chkWalkableBefore);
            this.grpGeneral.Controls.Add(this.cmbToolType);
            this.grpGeneral.Controls.Add(this.lblToolType);
            this.grpGeneral.Controls.Add(this.lblHP);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(0, 0);
            this.grpGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.grpGeneral.Size = new System.Drawing.Size(297, 346);
            this.grpGeneral.TabIndex = 15;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(256, 55);
            this.btnAddFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnAddFolder.Size = new System.Drawing.Size(24, 26);
            this.btnAddFolder.TabIndex = 52;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(8, 59);
            this.lblFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(49, 16);
            this.lblFolder.TabIndex = 51;
            this.lblFolder.Text = "Folder:";
            // 
            // cmbFolder
            // 
            this.cmbFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFolder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFolder.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFolder.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFolder.DrawDropdownHoverOutline = false;
            this.cmbFolder.DrawFocusRectangle = false;
            this.cmbFolder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFolder.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Location = new System.Drawing.Point(100, 55);
            this.cmbFolder.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(149, 23);
            this.cmbFolder.TabIndex = 50;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // nudMaxHp
            // 
            this.nudMaxHp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxHp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxHp.Location = new System.Drawing.Point(100, 154);
            this.nudMaxHp.Margin = new System.Windows.Forms.Padding(4);
            this.nudMaxHp.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMaxHp.Name = "nudMaxHp";
            this.nudMaxHp.Size = new System.Drawing.Size(180, 22);
            this.nudMaxHp.TabIndex = 42;
            this.nudMaxHp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMaxHp.ValueChanged += new System.EventHandler(this.nudMaxHp_ValueChanged);
            // 
            // nudMinHp
            // 
            this.nudMinHp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinHp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinHp.Location = new System.Drawing.Point(100, 122);
            this.nudMinHp.Margin = new System.Windows.Forms.Padding(4);
            this.nudMinHp.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMinHp.Name = "nudMinHp";
            this.nudMinHp.Size = new System.Drawing.Size(180, 22);
            this.nudMinHp.TabIndex = 41;
            this.nudMinHp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMinHp.ValueChanged += new System.EventHandler(this.nudMinHp_ValueChanged);
            // 
            // nudSpawnDuration
            // 
            this.nudSpawnDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpawnDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpawnDuration.Location = new System.Drawing.Point(164, 220);
            this.nudSpawnDuration.Margin = new System.Windows.Forms.Padding(4);
            this.nudSpawnDuration.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudSpawnDuration.Name = "nudSpawnDuration";
            this.nudSpawnDuration.Size = new System.Drawing.Size(116, 22);
            this.nudSpawnDuration.TabIndex = 40;
            this.nudSpawnDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpawnDuration.ValueChanged += new System.EventHandler(this.nudSpawnDuration_ValueChanged);
            // 
            // cmbAnimation
            // 
            this.cmbAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAnimation.DrawDropdownHoverOutline = false;
            this.cmbAnimation.DrawFocusRectangle = false;
            this.cmbAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAnimation.FormattingEnabled = true;
            this.cmbAnimation.Location = new System.Drawing.Point(100, 254);
            this.cmbAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(179, 23);
            this.cmbAnimation.TabIndex = 39;
            this.cmbAnimation.Text = null;
            this.cmbAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAnimation_SelectedIndexChanged);
            // 
            // lblAnimation
            // 
            this.lblAnimation.AutoSize = true;
            this.lblAnimation.Location = new System.Drawing.Point(8, 258);
            this.lblAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnimation.Name = "lblAnimation";
            this.lblAnimation.Size = new System.Drawing.Size(69, 16);
            this.lblAnimation.TabIndex = 36;
            this.lblAnimation.Text = "Animation:";
            // 
            // lblMaxHp
            // 
            this.lblMaxHp.AutoSize = true;
            this.lblMaxHp.Location = new System.Drawing.Point(8, 156);
            this.lblMaxHp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxHp.Name = "lblMaxHp";
            this.lblMaxHp.Size = new System.Drawing.Size(57, 16);
            this.lblMaxHp.TabIndex = 35;
            this.lblMaxHp.Text = "Max HP:";
            // 
            // lblSpawnDuration
            // 
            this.lblSpawnDuration.AutoSize = true;
            this.lblSpawnDuration.Location = new System.Drawing.Point(8, 225);
            this.lblSpawnDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpawnDuration.Name = "lblSpawnDuration";
            this.lblSpawnDuration.Size = new System.Drawing.Size(104, 16);
            this.lblSpawnDuration.TabIndex = 32;
            this.lblSpawnDuration.Text = "Spawn Duration:";
            // 
            // chkWalkableAfter
            // 
            this.chkWalkableAfter.Location = new System.Drawing.Point(8, 316);
            this.chkWalkableAfter.Margin = new System.Windows.Forms.Padding(4);
            this.chkWalkableAfter.Name = "chkWalkableAfter";
            this.chkWalkableAfter.Size = new System.Drawing.Size(281, 21);
            this.chkWalkableAfter.TabIndex = 31;
            this.chkWalkableAfter.Text = "Walkable after resource removal?";
            this.chkWalkableAfter.CheckedChanged += new System.EventHandler(this.chkWalkableAfter_CheckedChanged);
            // 
            // chkWalkableBefore
            // 
            this.chkWalkableBefore.Location = new System.Drawing.Point(8, 288);
            this.chkWalkableBefore.Margin = new System.Windows.Forms.Padding(4);
            this.chkWalkableBefore.Name = "chkWalkableBefore";
            this.chkWalkableBefore.Size = new System.Drawing.Size(281, 21);
            this.chkWalkableBefore.TabIndex = 30;
            this.chkWalkableBefore.Text = "Walkable before resource removal?";
            this.chkWalkableBefore.CheckedChanged += new System.EventHandler(this.chkWalkableBefore_CheckedChanged);
            // 
            // cmbToolType
            // 
            this.cmbToolType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbToolType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbToolType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbToolType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbToolType.DrawDropdownHoverOutline = false;
            this.cmbToolType.DrawFocusRectangle = false;
            this.cmbToolType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbToolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToolType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbToolType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbToolType.FormattingEnabled = true;
            this.cmbToolType.Location = new System.Drawing.Point(100, 89);
            this.cmbToolType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbToolType.Name = "cmbToolType";
            this.cmbToolType.Size = new System.Drawing.Size(179, 23);
            this.cmbToolType.TabIndex = 29;
            this.cmbToolType.Text = null;
            this.cmbToolType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbToolType.SelectedIndexChanged += new System.EventHandler(this.cmbToolType_SelectedIndexChanged);
            // 
            // lblToolType
            // 
            this.lblToolType.AutoSize = true;
            this.lblToolType.Location = new System.Drawing.Point(8, 92);
            this.lblToolType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolType.Name = "lblToolType";
            this.lblToolType.Size = new System.Drawing.Size(73, 16);
            this.lblToolType.TabIndex = 28;
            this.lblToolType.Text = "Tool Type:";
            // 
            // lblHP
            // 
            this.lblHP.AutoSize = true;
            this.lblHP.Location = new System.Drawing.Point(8, 124);
            this.lblHP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHP.Name = "lblHP";
            this.lblHP.Size = new System.Drawing.Size(53, 16);
            this.lblHP.TabIndex = 16;
            this.lblHP.Text = "Min HP:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 25);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(100, 25);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(179, 22);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnRequirements
            // 
            this.btnRequirements.Location = new System.Drawing.Point(11, 22);
            this.btnRequirements.Margin = new System.Windows.Forms.Padding(4);
            this.btnRequirements.Name = "btnRequirements";
            this.btnRequirements.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnRequirements.Size = new System.Drawing.Size(300, 28);
            this.btnRequirements.TabIndex = 38;
            this.btnRequirements.Text = "Harvesting Requirements";
            this.btnRequirements.Click += new System.EventHandler(this.btnRequirements_Click);
            // 
            // grpGraphics
            // 
            this.grpGraphics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGraphics.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGraphics.Controls.Add(this.chkExhaustedBelowEntities);
            this.grpGraphics.Controls.Add(this.chkInitialBelowEntities);
            this.grpGraphics.Controls.Add(this.chkExhaustedFromTileset);
            this.grpGraphics.Controls.Add(this.chkInitialFromTileset);
            this.grpGraphics.Controls.Add(this.exhaustedGraphicContainer);
            this.grpGraphics.Controls.Add(this.initalGraphicContainer);
            this.grpGraphics.Controls.Add(this.cmbEndSprite);
            this.grpGraphics.Controls.Add(this.lblPic2);
            this.grpGraphics.Controls.Add(this.cmbInitialSprite);
            this.grpGraphics.Controls.Add(this.lblPic);
            this.grpGraphics.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGraphics.Location = new System.Drawing.Point(0, 347);
            this.grpGraphics.Margin = new System.Windows.Forms.Padding(4);
            this.grpGraphics.Name = "grpGraphics";
            this.grpGraphics.Padding = new System.Windows.Forms.Padding(4);
            this.grpGraphics.Size = new System.Drawing.Size(941, 559);
            this.grpGraphics.TabIndex = 16;
            this.grpGraphics.TabStop = false;
            this.grpGraphics.Text = "Graphics";
            // 
            // chkExhaustedBelowEntities
            // 
            this.chkExhaustedBelowEntities.Location = new System.Drawing.Point(796, 16);
            this.chkExhaustedBelowEntities.Margin = new System.Windows.Forms.Padding(4);
            this.chkExhaustedBelowEntities.Name = "chkExhaustedBelowEntities";
            this.chkExhaustedBelowEntities.Size = new System.Drawing.Size(131, 26);
            this.chkExhaustedBelowEntities.TabIndex = 35;
            this.chkExhaustedBelowEntities.Text = "Below Entities";
            this.chkExhaustedBelowEntities.CheckedChanged += new System.EventHandler(this.chkExhaustedBelowEntities_CheckedChanged);
            // 
            // chkInitialBelowEntities
            // 
            this.chkInitialBelowEntities.Location = new System.Drawing.Point(327, 16);
            this.chkInitialBelowEntities.Margin = new System.Windows.Forms.Padding(4);
            this.chkInitialBelowEntities.Name = "chkInitialBelowEntities";
            this.chkInitialBelowEntities.Size = new System.Drawing.Size(131, 26);
            this.chkInitialBelowEntities.TabIndex = 34;
            this.chkInitialBelowEntities.Text = "Below Entities";
            this.chkInitialBelowEntities.CheckedChanged += new System.EventHandler(this.chkInitialBelowEntities_CheckedChanged);
            // 
            // chkExhaustedFromTileset
            // 
            this.chkExhaustedFromTileset.Location = new System.Drawing.Point(796, 39);
            this.chkExhaustedFromTileset.Margin = new System.Windows.Forms.Padding(4);
            this.chkExhaustedFromTileset.Name = "chkExhaustedFromTileset";
            this.chkExhaustedFromTileset.Size = new System.Drawing.Size(131, 26);
            this.chkExhaustedFromTileset.TabIndex = 33;
            this.chkExhaustedFromTileset.Text = "From Tileset";
            this.chkExhaustedFromTileset.CheckedChanged += new System.EventHandler(this.chkExhaustedFromTileset_CheckedChanged);
            // 
            // chkInitialFromTileset
            // 
            this.chkInitialFromTileset.Location = new System.Drawing.Point(327, 39);
            this.chkInitialFromTileset.Margin = new System.Windows.Forms.Padding(4);
            this.chkInitialFromTileset.Name = "chkInitialFromTileset";
            this.chkInitialFromTileset.Size = new System.Drawing.Size(131, 26);
            this.chkInitialFromTileset.TabIndex = 32;
            this.chkInitialFromTileset.Text = "From Tileset";
            this.chkInitialFromTileset.CheckedChanged += new System.EventHandler(this.chkInitialFromTileset_CheckedChanged);
            // 
            // exhaustedGraphicContainer
            // 
            this.exhaustedGraphicContainer.AutoScroll = true;
            this.exhaustedGraphicContainer.Controls.Add(this.picEndResource);
            this.exhaustedGraphicContainer.Location = new System.Drawing.Point(487, 76);
            this.exhaustedGraphicContainer.Margin = new System.Windows.Forms.Padding(4);
            this.exhaustedGraphicContainer.Name = "exhaustedGraphicContainer";
            this.exhaustedGraphicContainer.Size = new System.Drawing.Size(440, 475);
            this.exhaustedGraphicContainer.TabIndex = 25;
            // 
            // picEndResource
            // 
            this.picEndResource.Location = new System.Drawing.Point(0, 0);
            this.picEndResource.Margin = new System.Windows.Forms.Padding(4);
            this.picEndResource.Name = "picEndResource";
            this.picEndResource.Size = new System.Drawing.Size(243, 357);
            this.picEndResource.TabIndex = 2;
            this.picEndResource.TabStop = false;
            this.picEndResource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picExhustedResource_MouseDown);
            this.picEndResource.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picExhaustedResource_MouseMove);
            this.picEndResource.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picExhaustedResource_MouseUp);
            // 
            // initalGraphicContainer
            // 
            this.initalGraphicContainer.AutoScroll = true;
            this.initalGraphicContainer.Controls.Add(this.picInitialResource);
            this.initalGraphicContainer.Location = new System.Drawing.Point(17, 76);
            this.initalGraphicContainer.Margin = new System.Windows.Forms.Padding(4);
            this.initalGraphicContainer.Name = "initalGraphicContainer";
            this.initalGraphicContainer.Size = new System.Drawing.Size(440, 475);
            this.initalGraphicContainer.TabIndex = 24;
            // 
            // picInitialResource
            // 
            this.picInitialResource.Location = new System.Drawing.Point(0, 0);
            this.picInitialResource.Margin = new System.Windows.Forms.Padding(4);
            this.picInitialResource.Name = "picInitialResource";
            this.picInitialResource.Size = new System.Drawing.Size(240, 357);
            this.picInitialResource.TabIndex = 2;
            this.picInitialResource.TabStop = false;
            this.picInitialResource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picInitialResource_MouseDown);
            this.picInitialResource.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picInitialResource_MouseMove);
            this.picInitialResource.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picInitialResource_MouseUp);
            // 
            // cmbEndSprite
            // 
            this.cmbEndSprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEndSprite.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEndSprite.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEndSprite.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEndSprite.DrawDropdownHoverOutline = false;
            this.cmbEndSprite.DrawFocusRectangle = false;
            this.cmbEndSprite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEndSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEndSprite.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEndSprite.FormattingEnabled = true;
            this.cmbEndSprite.Items.AddRange(new object[] {
            "None"});
            this.cmbEndSprite.Location = new System.Drawing.Point(487, 39);
            this.cmbEndSprite.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEndSprite.Name = "cmbEndSprite";
            this.cmbEndSprite.Size = new System.Drawing.Size(260, 23);
            this.cmbEndSprite.TabIndex = 16;
            this.cmbEndSprite.Text = "None";
            this.cmbEndSprite.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEndSprite.SelectedIndexChanged += new System.EventHandler(this.cmbEndSprite_SelectedIndexChanged);
            // 
            // lblPic2
            // 
            this.lblPic2.AutoSize = true;
            this.lblPic2.Location = new System.Drawing.Point(483, 20);
            this.lblPic2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPic2.Name = "lblPic2";
            this.lblPic2.Size = new System.Drawing.Size(120, 16);
            this.lblPic2.TabIndex = 15;
            this.lblPic2.Text = "Removed Graphic:";
            // 
            // cmbInitialSprite
            // 
            this.cmbInitialSprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbInitialSprite.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbInitialSprite.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbInitialSprite.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbInitialSprite.DrawDropdownHoverOutline = false;
            this.cmbInitialSprite.DrawFocusRectangle = false;
            this.cmbInitialSprite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInitialSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInitialSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbInitialSprite.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbInitialSprite.FormattingEnabled = true;
            this.cmbInitialSprite.Items.AddRange(new object[] {
            "None"});
            this.cmbInitialSprite.Location = new System.Drawing.Point(17, 39);
            this.cmbInitialSprite.Margin = new System.Windows.Forms.Padding(4);
            this.cmbInitialSprite.Name = "cmbInitialSprite";
            this.cmbInitialSprite.Size = new System.Drawing.Size(259, 23);
            this.cmbInitialSprite.TabIndex = 14;
            this.cmbInitialSprite.Text = "None";
            this.cmbInitialSprite.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbInitialSprite.SelectedIndexChanged += new System.EventHandler(this.cmbInitialSprite_SelectedIndexChanged);
            // 
            // lblPic
            // 
            this.lblPic.AutoSize = true;
            this.lblPic.Location = new System.Drawing.Point(13, 20);
            this.lblPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(90, 16);
            this.lblPic.TabIndex = 13;
            this.lblPic.Text = "Initial Graphic:";
            // 
            // tmrRender
            // 
            this.tmrRender.Enabled = true;
            this.tmrRender.Interval = 10;
            this.tmrRender.Tick += new System.EventHandler(this.tmrRender_Tick);
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.darkGroupBox1);
            this.pnlContainer.Controls.Add(this.grpRequirements);
            this.pnlContainer.Controls.Add(this.grpCommonEvent);
            this.pnlContainer.Controls.Add(this.grpRegen);
            this.pnlContainer.Controls.Add(this.grpDrops);
            this.pnlContainer.Controls.Add(this.grpGeneral);
            this.pnlContainer.Controls.Add(this.grpGraphics);
            this.pnlContainer.Location = new System.Drawing.Point(295, 48);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(975, 564);
            this.pnlContainer.TabIndex = 18;
            this.pnlContainer.Visible = false;
            // 
            // darkGroupBox1
            // 
            this.darkGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.darkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.darkGroupBox1.Controls.Add(this.nudExpAmount);
            this.darkGroupBox1.Controls.Add(this.label2);
            this.darkGroupBox1.Controls.Add(this.cmbJobType);
            this.darkGroupBox1.Controls.Add(this.label1);
            this.darkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.darkGroupBox1.Location = new System.Drawing.Point(616, 79);
            this.darkGroupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.darkGroupBox1.Name = "darkGroupBox1";
            this.darkGroupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.darkGroupBox1.Size = new System.Drawing.Size(325, 74);
            this.darkGroupBox1.TabIndex = 34;
            this.darkGroupBox1.TabStop = false;
            this.darkGroupBox1.Text = "Experience";
            // 
            // nudExpAmount
            // 
            this.nudExpAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudExpAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudExpAmount.Location = new System.Drawing.Point(159, 44);
            this.nudExpAmount.Margin = new System.Windows.Forms.Padding(4);
            this.nudExpAmount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudExpAmount.Name = "nudExpAmount";
            this.nudExpAmount.Size = new System.Drawing.Size(157, 22);
            this.nudExpAmount.TabIndex = 63;
            this.nudExpAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudExpAmount.ValueChanged += new System.EventHandler(this.nudExpAmount_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 62;
            this.label2.Text = "EXP Amount:";
            // 
            // cmbJobType
            // 
            this.cmbJobType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbJobType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbJobType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbJobType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbJobType.DrawDropdownHoverOutline = false;
            this.cmbJobType.DrawFocusRectangle = false;
            this.cmbJobType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbJobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJobType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbJobType.FormattingEnabled = true;
            this.cmbJobType.Location = new System.Drawing.Point(11, 43);
            this.cmbJobType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbJobType.Name = "cmbJobType";
            this.cmbJobType.Size = new System.Drawing.Size(140, 23);
            this.cmbJobType.TabIndex = 19;
            this.cmbJobType.Text = "None";
            this.cmbJobType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbJobType.SelectedIndexChanged += new System.EventHandler(this.cmbJobType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "JobType";
            // 
            // grpRequirements
            // 
            this.grpRequirements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRequirements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRequirements.Controls.Add(this.lblCannotHarvest);
            this.grpRequirements.Controls.Add(this.btnRequirements);
            this.grpRequirements.Controls.Add(this.txtCannotHarvest);
            this.grpRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRequirements.Location = new System.Drawing.Point(616, 233);
            this.grpRequirements.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRequirements.Name = "grpRequirements";
            this.grpRequirements.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRequirements.Size = new System.Drawing.Size(325, 113);
            this.grpRequirements.TabIndex = 33;
            this.grpRequirements.TabStop = false;
            this.grpRequirements.Text = "Requirements";
            // 
            // lblCannotHarvest
            // 
            this.lblCannotHarvest.AutoSize = true;
            this.lblCannotHarvest.Location = new System.Drawing.Point(7, 58);
            this.lblCannotHarvest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCannotHarvest.Name = "lblCannotHarvest";
            this.lblCannotHarvest.Size = new System.Drawing.Size(162, 16);
            this.lblCannotHarvest.TabIndex = 54;
            this.lblCannotHarvest.Text = "Cannot Harvest Message:";
            // 
            // txtCannotHarvest
            // 
            this.txtCannotHarvest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCannotHarvest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCannotHarvest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCannotHarvest.Location = new System.Drawing.Point(11, 78);
            this.txtCannotHarvest.Margin = new System.Windows.Forms.Padding(4);
            this.txtCannotHarvest.Name = "txtCannotHarvest";
            this.txtCannotHarvest.Size = new System.Drawing.Size(299, 22);
            this.txtCannotHarvest.TabIndex = 53;
            this.txtCannotHarvest.TextChanged += new System.EventHandler(this.txtCannotHarvest_TextChanged);
            // 
            // grpCommonEvent
            // 
            this.grpCommonEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCommonEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCommonEvent.Controls.Add(this.cmbEvent);
            this.grpCommonEvent.Controls.Add(this.lblEvent);
            this.grpCommonEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCommonEvent.Location = new System.Drawing.Point(616, 154);
            this.grpCommonEvent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpCommonEvent.Name = "grpCommonEvent";
            this.grpCommonEvent.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpCommonEvent.Size = new System.Drawing.Size(325, 74);
            this.grpCommonEvent.TabIndex = 33;
            this.grpCommonEvent.TabStop = false;
            this.grpCommonEvent.Text = "Common Event";
            // 
            // cmbEvent
            // 
            this.cmbEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEvent.DrawDropdownHoverOutline = false;
            this.cmbEvent.DrawFocusRectangle = false;
            this.cmbEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Location = new System.Drawing.Point(11, 43);
            this.cmbEvent.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(259, 23);
            this.cmbEvent.TabIndex = 19;
            this.cmbEvent.Text = null;
            this.cmbEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
            // 
            // lblEvent
            // 
            this.lblEvent.AutoSize = true;
            this.lblEvent.Location = new System.Drawing.Point(7, 22);
            this.lblEvent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Size = new System.Drawing.Size(44, 16);
            this.lblEvent.TabIndex = 18;
            this.lblEvent.Text = "Event:";
            // 
            // grpRegen
            // 
            this.grpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRegen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRegen.Controls.Add(this.nudHpRegen);
            this.grpRegen.Controls.Add(this.lblHpRegen);
            this.grpRegen.Controls.Add(this.lblRegenHint);
            this.grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRegen.Location = new System.Drawing.Point(616, 2);
            this.grpRegen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRegen.Name = "grpRegen";
            this.grpRegen.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRegen.Size = new System.Drawing.Size(325, 73);
            this.grpRegen.TabIndex = 32;
            this.grpRegen.TabStop = false;
            this.grpRegen.Text = "Regen";
            // 
            // nudHpRegen
            // 
            this.nudHpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHpRegen.Location = new System.Drawing.Point(11, 38);
            this.nudHpRegen.Margin = new System.Windows.Forms.Padding(4);
            this.nudHpRegen.Name = "nudHpRegen";
            this.nudHpRegen.Size = new System.Drawing.Size(115, 22);
            this.nudHpRegen.TabIndex = 30;
            this.nudHpRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHpRegen.ValueChanged += new System.EventHandler(this.nudHpRegen_ValueChanged);
            // 
            // lblHpRegen
            // 
            this.lblHpRegen.AutoSize = true;
            this.lblHpRegen.Location = new System.Drawing.Point(7, 21);
            this.lblHpRegen.Name = "lblHpRegen";
            this.lblHpRegen.Size = new System.Drawing.Size(52, 16);
            this.lblHpRegen.TabIndex = 26;
            this.lblHpRegen.Text = "HP: (%)";
            // 
            // lblRegenHint
            // 
            this.lblRegenHint.Location = new System.Drawing.Point(136, 34);
            this.lblRegenHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegenHint.Name = "lblRegenHint";
            this.lblRegenHint.Size = new System.Drawing.Size(183, 26);
            this.lblRegenHint.TabIndex = 0;
            this.lblRegenHint.Text = "% of HP to restore per tick.\r\n";
            this.lblRegenHint.Click += new System.EventHandler(this.lblRegenHint_Click);
            // 
            // grpDrops
            // 
            this.grpDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDrops.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDrops.Controls.Add(this.btnDropRemove);
            this.grpDrops.Controls.Add(this.btnDropAdd);
            this.grpDrops.Controls.Add(this.lstDrops);
            this.grpDrops.Controls.Add(this.nudDropAmount);
            this.grpDrops.Controls.Add(this.nudDropChance);
            this.grpDrops.Controls.Add(this.cmbDropItem);
            this.grpDrops.Controls.Add(this.lblDropAmount);
            this.grpDrops.Controls.Add(this.lblDropChance);
            this.grpDrops.Controls.Add(this.lblDropItem);
            this.grpDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDrops.Location = new System.Drawing.Point(308, 0);
            this.grpDrops.Margin = new System.Windows.Forms.Padding(4);
            this.grpDrops.Name = "grpDrops";
            this.grpDrops.Padding = new System.Windows.Forms.Padding(4);
            this.grpDrops.Size = new System.Drawing.Size(301, 346);
            this.grpDrops.TabIndex = 31;
            this.grpDrops.TabStop = false;
            this.grpDrops.Text = "Drops";
            // 
            // btnDropRemove
            // 
            this.btnDropRemove.Location = new System.Drawing.Point(168, 310);
            this.btnDropRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnDropRemove.Name = "btnDropRemove";
            this.btnDropRemove.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnDropRemove.Size = new System.Drawing.Size(100, 28);
            this.btnDropRemove.TabIndex = 64;
            this.btnDropRemove.Text = "Remove";
            this.btnDropRemove.Click += new System.EventHandler(this.btnDropRemove_Click);
            // 
            // btnDropAdd
            // 
            this.btnDropAdd.Location = new System.Drawing.Point(8, 310);
            this.btnDropAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnDropAdd.Name = "btnDropAdd";
            this.btnDropAdd.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnDropAdd.Size = new System.Drawing.Size(100, 28);
            this.btnDropAdd.TabIndex = 63;
            this.btnDropAdd.Text = "Add";
            this.btnDropAdd.Click += new System.EventHandler(this.btnDropAdd_Click);
            // 
            // lstDrops
            // 
            this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstDrops.FormattingEnabled = true;
            this.lstDrops.ItemHeight = 16;
            this.lstDrops.Location = new System.Drawing.Point(12, 23);
            this.lstDrops.Margin = new System.Windows.Forms.Padding(4);
            this.lstDrops.Name = "lstDrops";
            this.lstDrops.Size = new System.Drawing.Size(255, 114);
            this.lstDrops.TabIndex = 62;
            this.lstDrops.SelectedIndexChanged += new System.EventHandler(this.lstDrops_SelectedIndexChanged);
            // 
            // nudDropAmount
            // 
            this.nudDropAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDropAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDropAmount.Location = new System.Drawing.Point(8, 213);
            this.nudDropAmount.Margin = new System.Windows.Forms.Padding(4);
            this.nudDropAmount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudDropAmount.Name = "nudDropAmount";
            this.nudDropAmount.Size = new System.Drawing.Size(260, 22);
            this.nudDropAmount.TabIndex = 61;
            this.nudDropAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDropAmount.ValueChanged += new System.EventHandler(this.nudDropAmount_ValueChanged);
            // 
            // nudDropChance
            // 
            this.nudDropChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDropChance.DecimalPlaces = 2;
            this.nudDropChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDropChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudDropChance.Location = new System.Drawing.Point(8, 270);
            this.nudDropChance.Margin = new System.Windows.Forms.Padding(4);
            this.nudDropChance.Name = "nudDropChance";
            this.nudDropChance.Size = new System.Drawing.Size(260, 22);
            this.nudDropChance.TabIndex = 60;
            this.nudDropChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDropChance.ValueChanged += new System.EventHandler(this.nudDropChance_ValueChanged);
            // 
            // cmbDropItem
            // 
            this.cmbDropItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDropItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDropItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDropItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDropItem.DrawDropdownHoverOutline = false;
            this.cmbDropItem.DrawFocusRectangle = false;
            this.cmbDropItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDropItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDropItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDropItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDropItem.FormattingEnabled = true;
            this.cmbDropItem.Location = new System.Drawing.Point(8, 161);
            this.cmbDropItem.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDropItem.Name = "cmbDropItem";
            this.cmbDropItem.Size = new System.Drawing.Size(259, 23);
            this.cmbDropItem.TabIndex = 17;
            this.cmbDropItem.Text = null;
            this.cmbDropItem.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDropItem.SelectedIndexChanged += new System.EventHandler(this.cmbDropItem_SelectedIndexChanged);
            // 
            // lblDropAmount
            // 
            this.lblDropAmount.AutoSize = true;
            this.lblDropAmount.Location = new System.Drawing.Point(4, 193);
            this.lblDropAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropAmount.Name = "lblDropAmount";
            this.lblDropAmount.Size = new System.Drawing.Size(55, 16);
            this.lblDropAmount.TabIndex = 15;
            this.lblDropAmount.Text = "Amount:";
            // 
            // lblDropChance
            // 
            this.lblDropChance.AutoSize = true;
            this.lblDropChance.Location = new System.Drawing.Point(4, 249);
            this.lblDropChance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropChance.Name = "lblDropChance";
            this.lblDropChance.Size = new System.Drawing.Size(79, 16);
            this.lblDropChance.TabIndex = 13;
            this.lblDropChance.Text = "Chance (%):";
            // 
            // lblDropItem
            // 
            this.lblDropItem.AutoSize = true;
            this.lblDropItem.Location = new System.Drawing.Point(4, 140);
            this.lblDropItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropItem.Name = "lblDropItem";
            this.lblDropItem.Size = new System.Drawing.Size(35, 16);
            this.lblDropItem.TabIndex = 11;
            this.lblDropItem.Text = "Item:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1016, 619);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancel.Size = new System.Drawing.Size(253, 33);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(755, 619);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSave.Size = new System.Drawing.Size(253, 33);
            this.btnSave.TabIndex = 41;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripItemNew,
            this.toolStripSeparator1,
            this.toolStripItemDelete,
            this.toolStripSeparator2,
            this.btnAlphabetical,
            this.toolStripSeparator4,
            this.toolStripItemCopy,
            this.toolStripItemPaste,
            this.toolStripSeparator3,
            this.toolStripItemUndo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(7, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(1276, 31);
            this.toolStrip.TabIndex = 47;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripItemNew
            // 
            this.toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemNew.Image")));
            this.toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemNew.Name = "toolStripItemNew";
            this.toolStripItemNew.Size = new System.Drawing.Size(29, 28);
            this.toolStripItemNew.Text = "New";
            this.toolStripItemNew.Click += new System.EventHandler(this.toolStripItemNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripItemDelete
            // 
            this.toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemDelete.Enabled = false;
            this.toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemDelete.Image")));
            this.toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemDelete.Name = "toolStripItemDelete";
            this.toolStripItemDelete.Size = new System.Drawing.Size(29, 28);
            this.toolStripItemDelete.Text = "Delete";
            this.toolStripItemDelete.Click += new System.EventHandler(this.toolStripItemDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // btnAlphabetical
            // 
            this.btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnAlphabetical.Image = ((System.Drawing.Image)(resources.GetObject("btnAlphabetical.Image")));
            this.btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlphabetical.Name = "btnAlphabetical";
            this.btnAlphabetical.Size = new System.Drawing.Size(29, 28);
            this.btnAlphabetical.Text = "Order Chronologically";
            this.btnAlphabetical.Click += new System.EventHandler(this.btnAlphabetical_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripItemCopy
            // 
            this.toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemCopy.Enabled = false;
            this.toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemCopy.Image")));
            this.toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemCopy.Name = "toolStripItemCopy";
            this.toolStripItemCopy.Size = new System.Drawing.Size(29, 28);
            this.toolStripItemCopy.Text = "Copy";
            this.toolStripItemCopy.Click += new System.EventHandler(this.toolStripItemCopy_Click);
            // 
            // toolStripItemPaste
            // 
            this.toolStripItemPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemPaste.Enabled = false;
            this.toolStripItemPaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemPaste.Image")));
            this.toolStripItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemPaste.Name = "toolStripItemPaste";
            this.toolStripItemPaste.Size = new System.Drawing.Size(29, 28);
            this.toolStripItemPaste.Text = "Paste";
            this.toolStripItemPaste.Click += new System.EventHandler(this.toolStripItemPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripItemUndo
            // 
            this.toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemUndo.Enabled = false;
            this.toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemUndo.Image")));
            this.toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemUndo.Name = "toolStripItemUndo";
            this.toolStripItemUndo.Size = new System.Drawing.Size(29, 28);
            this.toolStripItemUndo.Text = "Undo";
            this.toolStripItemUndo.Click += new System.EventHandler(this.toolStripItemUndo_Click);
            // 
            // gameObjectTypeExtensionsBindingSource
            // 
            this.gameObjectTypeExtensionsBindingSource.DataSource = typeof(Intersect.Enums.GameObjectTypeExtensions);
            // 
            // gameObjectTypeExtensionsBindingSource1
            // 
            this.gameObjectTypeExtensionsBindingSource1.DataSource = typeof(Intersect.Enums.GameObjectTypeExtensions);
            // 
            // LevelNud
            // 
            this.LevelNud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.LevelNud.ForeColor = System.Drawing.Color.Gainsboro;
            this.LevelNud.Location = new System.Drawing.Point(101, 188);
            this.LevelNud.Margin = new System.Windows.Forms.Padding(4);
            this.LevelNud.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.LevelNud.Name = "LevelNud";
            this.LevelNud.Size = new System.Drawing.Size(180, 22);
            this.LevelNud.TabIndex = 54;
            this.LevelNud.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LevelNud.ValueChanged += new System.EventHandler(this.LevelNud_ValueChanged);
            // 
            // LvlLbl
            // 
            this.LvlLbl.AutoSize = true;
            this.LvlLbl.Location = new System.Drawing.Point(9, 190);
            this.LvlLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LvlLbl.Name = "LvlLbl";
            this.LvlLbl.Size = new System.Drawing.Size(43, 16);
            this.LvlLbl.TabIndex = 53;
            this.LvlLbl.Text = "Level:";
            // 
            // FrmResource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1276, 661);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpResources);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmResource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resource Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmResource_FormClosed);
            this.Load += new System.EventHandler(this.frmResource_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
            this.grpResources.ResumeLayout(false);
            this.grpResources.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinHp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnDuration)).EndInit();
            this.grpGraphics.ResumeLayout(false);
            this.grpGraphics.PerformLayout();
            this.exhaustedGraphicContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEndResource)).EndInit();
            this.initalGraphicContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInitialResource)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.darkGroupBox1.ResumeLayout(false);
            this.darkGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExpAmount)).EndInit();
            this.grpRequirements.ResumeLayout(false);
            this.grpRequirements.PerformLayout();
            this.grpCommonEvent.ResumeLayout(false);
            this.grpCommonEvent.PerformLayout();
            this.grpRegen.ResumeLayout(false);
            this.grpRegen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpRegen)).EndInit();
            this.grpDrops.ResumeLayout(false);
            this.grpDrops.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropChance)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectTypeExtensionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectTypeExtensionsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LevelNud)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpResources;
        private DarkGroupBox grpGeneral;
        private DarkGroupBox grpGraphics;
        private System.Windows.Forms.Label lblName;
        private DarkTextBox txtName;
        private System.Windows.Forms.Label lblHP;
        private DarkCheckBox chkWalkableAfter;
        private DarkCheckBox chkWalkableBefore;
        private DarkComboBox cmbToolType;
        private System.Windows.Forms.Label lblToolType;
        private DarkComboBox cmbEndSprite;
        private System.Windows.Forms.Label lblPic2;
        private DarkComboBox cmbInitialSprite;
        private System.Windows.Forms.Label lblPic;
        private System.Windows.Forms.Label lblSpawnDuration;
        public System.Windows.Forms.PictureBox picEndResource;
        public System.Windows.Forms.PictureBox picInitialResource;
        private System.Windows.Forms.Label lblMaxHp;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.Timer tmrRender;
        private System.Windows.Forms.Panel pnlContainer;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton toolStripItemCopy;
        public System.Windows.Forms.ToolStripButton toolStripItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripItemUndo;
        private System.Windows.Forms.Panel exhaustedGraphicContainer;
        private System.Windows.Forms.Panel initalGraphicContainer;
        private DarkButton btnRequirements;
        private DarkComboBox cmbAnimation;
        private DarkNumericUpDown nudSpawnDuration;
        private DarkNumericUpDown nudMaxHp;
        private DarkNumericUpDown nudMinHp;
        private DarkGroupBox grpDrops;
        private DarkButton btnDropRemove;
        private DarkButton btnDropAdd;
        private System.Windows.Forms.ListBox lstDrops;
        private DarkNumericUpDown nudDropAmount;
        private DarkNumericUpDown nudDropChance;
        private DarkComboBox cmbDropItem;
        private System.Windows.Forms.Label lblDropAmount;
        private System.Windows.Forms.Label lblDropChance;
        private System.Windows.Forms.Label lblDropItem;
        private DarkCheckBox chkExhaustedFromTileset;
        private DarkCheckBox chkInitialFromTileset;
        private DarkGroupBox grpRegen;
        private DarkNumericUpDown nudHpRegen;
        private System.Windows.Forms.Label lblHpRegen;
        private System.Windows.Forms.Label lblRegenHint;
        private DarkGroupBox grpCommonEvent;
        private DarkComboBox cmbEvent;
        private System.Windows.Forms.Label lblEvent;
        private DarkCheckBox chkExhaustedBelowEntities;
        private DarkCheckBox chkInitialBelowEntities;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkComboBox cmbFolder;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Controls.GameObjectList lstGameObjects;
        private DarkGroupBox grpRequirements;
        private System.Windows.Forms.Label lblCannotHarvest;
        private DarkTextBox txtCannotHarvest;
        private DarkGroupBox darkGroupBox1;
        private DarkComboBox cmbJobType;
        private System.Windows.Forms.Label label1;
        private DarkNumericUpDown nudExpAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource gameObjectTypeExtensionsBindingSource;
        private System.Windows.Forms.BindingSource gameObjectTypeExtensionsBindingSource1;
        private DarkNumericUpDown LevelNud;
        private System.Windows.Forms.Label LvlLbl;
    }
}