using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmPet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPet));
            this.grpPets = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.nudLevel = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbSprite = new DarkUI.Controls.DarkComboBox();
            this.lblPic = new System.Windows.Forms.Label();
            this.picPet = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.nudSightRange = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSightRange = new System.Windows.Forms.Label();
            this.grpStats = new DarkUI.Controls.DarkGroupBox();
            this.nudAgi = new DarkUI.Controls.DarkNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMana = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHp = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSpd = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMR = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDef = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMag = new DarkUI.Controls.DarkNumericUpDown();
            this.nudStr = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSpd = new System.Windows.Forms.Label();
            this.lblMR = new System.Windows.Forms.Label();
            this.lblDef = new System.Windows.Forms.Label();
            this.lblMag = new System.Windows.Forms.Label();
            this.lblStr = new System.Windows.Forms.Label();
            this.lblMana = new System.Windows.Forms.Label();
            this.lblHP = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            this.pnlPetlevel = new System.Windows.Forms.Panel();
            this.nudPetPnts = new DarkUI.Controls.DarkNumericUpDown();
            this.DarkLabel13 = new DarkUI.Controls.DarkLabel();
            this.DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            this.cmbEvolve = new DarkUI.Controls.DarkComboBox();
            this.DarkLabel15 = new DarkUI.Controls.DarkLabel();
            this.nudEvolveLvl = new DarkUI.Controls.DarkNumericUpDown();
            this.DarkLabel14 = new DarkUI.Controls.DarkLabel();
            this.chkEvolve = new DarkUI.Controls.DarkCheckBox();
            this.nudMaxLevel = new DarkUI.Controls.DarkNumericUpDown();
            this.DarkLabel12 = new DarkUI.Controls.DarkLabel();
            this.nudPetExp = new DarkUI.Controls.DarkNumericUpDown();
            this.DarkLabel11 = new DarkUI.Controls.DarkLabel();
            this.optDoNotLevel = new DarkUI.Controls.DarkRadioButton();
            this.optLevel = new DarkUI.Controls.DarkRadioButton();
            this.grpAnimation = new DarkUI.Controls.DarkGroupBox();
            this.cmbDeathAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblDeathAnimation = new System.Windows.Forms.Label();
            this.grpImmunities = new DarkUI.Controls.DarkGroupBox();
            this.chkTaunt = new DarkUI.Controls.DarkCheckBox();
            this.chkSleep = new DarkUI.Controls.DarkCheckBox();
            this.chkTransform = new DarkUI.Controls.DarkCheckBox();
            this.chkBlind = new DarkUI.Controls.DarkCheckBox();
            this.chkSnare = new DarkUI.Controls.DarkCheckBox();
            this.chkStun = new DarkUI.Controls.DarkCheckBox();
            this.chkSilence = new DarkUI.Controls.DarkCheckBox();
            this.chkKnockback = new DarkUI.Controls.DarkCheckBox();
            this.grpCombat = new DarkUI.Controls.DarkGroupBox();
            this.grpAttackSpeed = new DarkUI.Controls.DarkGroupBox();
            this.nudAttackSpeedValue = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAttackSpeedValue = new System.Windows.Forms.Label();
            this.cmbAttackSpeedModifier = new DarkUI.Controls.DarkComboBox();
            this.lblAttackSpeedModifier = new System.Windows.Forms.Label();
            this.nudCritMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCritMultiplier = new System.Windows.Forms.Label();
            this.nudScaling = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudCritChance = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbScalingStat = new DarkUI.Controls.DarkComboBox();
            this.lblScalingStat = new System.Windows.Forms.Label();
            this.lblScaling = new System.Windows.Forms.Label();
            this.cmbDamageType = new DarkUI.Controls.DarkComboBox();
            this.lblDamageType = new System.Windows.Forms.Label();
            this.lblCritChance = new System.Windows.Forms.Label();
            this.cmbAttackAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblAttackAnimation = new System.Windows.Forms.Label();
            this.lblDamage = new System.Windows.Forms.Label();
            this.grpRegen = new DarkUI.Controls.DarkGroupBox();
            this.nudMpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.lblHpRegen = new System.Windows.Forms.Label();
            this.lblManaRegen = new System.Windows.Forms.Label();
            this.lblRegenHint = new System.Windows.Forms.Label();
            this.chkSwarm = new DarkUI.Controls.DarkCheckBox();
            this.chkAggressive = new DarkUI.Controls.DarkCheckBox();
            this.cmbSpell = new DarkUI.Controls.DarkComboBox();
            this.cmbFreq = new DarkUI.Controls.DarkComboBox();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblSpell = new System.Windows.Forms.Label();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.btnAdd = new DarkUI.Controls.DarkButton();
            this.lstSpells = new System.Windows.Forms.ListBox();
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
            this.grpPets.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSightRange)).BeginInit();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.DarkGroupBox4.SuspendLayout();
            this.pnlPetlevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPetPnts)).BeginInit();
            this.DarkGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvolveLvl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPetExp)).BeginInit();
            this.grpAnimation.SuspendLayout();
            this.grpImmunities.SuspendLayout();
            this.grpCombat.SuspendLayout();
            this.grpAttackSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).BeginInit();
            this.grpRegen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpRegen)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPets
            // 
            this.grpPets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPets.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPets.Controls.Add(this.btnClearSearch);
            this.grpPets.Controls.Add(this.txtSearch);
            this.grpPets.Controls.Add(this.lstGameObjects);
            this.grpPets.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPets.Location = new System.Drawing.Point(12, 39);
            this.grpPets.Name = "grpPets";
            this.grpPets.Size = new System.Drawing.Size(203, 570);
            this.grpPets.TabIndex = 13;
            this.grpPets.TabStop = false;
            this.grpPets.Text = "Pets";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 18);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(18, 20);
            this.btnClearSearch.TabIndex = 34;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(6, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 20);
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
            this.lstGameObjects.Location = new System.Drawing.Point(6, 44);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(191, 520);
            this.lstGameObjects.TabIndex = 32;
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.lblLevel);
            this.grpGeneral.Controls.Add(this.nudLevel);
            this.grpGeneral.Controls.Add(this.cmbSprite);
            this.grpGeneral.Controls.Add(this.lblPic);
            this.grpGeneral.Controls.Add(this.picPet);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(2, 1);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(207, 253);
            this.grpGeneral.TabIndex = 14;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(177, 45);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 67;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 49);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 66;
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
            this.cmbFolder.Location = new System.Drawing.Point(60, 45);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(113, 21);
            this.cmbFolder.TabIndex = 65;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(6, 78);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(36, 13);
            this.lblLevel.TabIndex = 64;
            this.lblLevel.Text = "Level:";
            // 
            // nudLevel
            // 
            this.nudLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudLevel.Location = new System.Drawing.Point(60, 75);
            this.nudLevel.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(135, 20);
            this.nudLevel.TabIndex = 63;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevel.ValueChanged += new System.EventHandler(this.nudLevel_ValueChanged);
            // 
            // cmbSprite
            // 
            this.cmbSprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSprite.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSprite.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSprite.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSprite.DrawDropdownHoverOutline = false;
            this.cmbSprite.DrawFocusRectangle = false;
            this.cmbSprite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSprite.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSprite.FormattingEnabled = true;
            this.cmbSprite.Items.AddRange(new object[] {
            "None"});
            this.cmbSprite.Location = new System.Drawing.Point(60, 98);
            this.cmbSprite.Name = "cmbSprite";
            this.cmbSprite.Size = new System.Drawing.Size(136, 21);
            this.cmbSprite.TabIndex = 11;
            this.cmbSprite.Text = "None";
            this.cmbSprite.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSprite.SelectedIndexChanged += new System.EventHandler(this.cmbSprite_SelectedIndexChanged);
            // 
            // lblPic
            // 
            this.lblPic.AutoSize = true;
            this.lblPic.Location = new System.Drawing.Point(7, 101);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(37, 13);
            this.lblPic.TabIndex = 6;
            this.lblPic.Text = "Sprite:";
            // 
            // picPet
            // 
            this.picPet.BackColor = System.Drawing.Color.Black;
            this.picPet.Location = new System.Drawing.Point(4, 124);
            this.picPet.Name = "picPet";
            this.picPet.Size = new System.Drawing.Size(102, 123);
            this.picPet.TabIndex = 4;
            this.picPet.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(60, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // nudSightRange
            // 
            this.nudSightRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSightRange.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSightRange.Location = new System.Drawing.Point(0, 0);
            this.nudSightRange.Name = "nudSightRange";
            this.nudSightRange.Size = new System.Drawing.Size(120, 20);
            this.nudSightRange.TabIndex = 0;
            this.nudSightRange.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblSightRange
            // 
            this.lblSightRange.Location = new System.Drawing.Point(0, 0);
            this.lblSightRange.Name = "lblSightRange";
            this.lblSightRange.Size = new System.Drawing.Size(100, 23);
            this.lblSightRange.TabIndex = 0;
            // 
            // grpStats
            // 
            this.grpStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStats.Controls.Add(this.nudAgi);
            this.grpStats.Controls.Add(this.label1);
            this.grpStats.Controls.Add(this.nudMana);
            this.grpStats.Controls.Add(this.nudHp);
            this.grpStats.Controls.Add(this.nudSpd);
            this.grpStats.Controls.Add(this.nudMR);
            this.grpStats.Controls.Add(this.nudDef);
            this.grpStats.Controls.Add(this.nudMag);
            this.grpStats.Controls.Add(this.nudStr);
            this.grpStats.Controls.Add(this.lblSpd);
            this.grpStats.Controls.Add(this.lblMR);
            this.grpStats.Controls.Add(this.lblDef);
            this.grpStats.Controls.Add(this.lblMag);
            this.grpStats.Controls.Add(this.lblStr);
            this.grpStats.Controls.Add(this.lblMana);
            this.grpStats.Controls.Add(this.lblHP);
            this.grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStats.Location = new System.Drawing.Point(3, 253);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(206, 162);
            this.grpStats.TabIndex = 15;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Stats:";
            // 
            // nudAgi
            // 
            this.nudAgi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAgi.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAgi.Location = new System.Drawing.Point(105, 128);
            this.nudAgi.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAgi.Name = "nudAgi";
            this.nudAgi.Size = new System.Drawing.Size(77, 20);
            this.nudAgi.TabIndex = 47;
            this.nudAgi.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAgi.ValueChanged += new System.EventHandler(this.nudAgi_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Agility:";
            // 
            // nudMana
            // 
            this.nudMana.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMana.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMana.Location = new System.Drawing.Point(133, 17);
            this.nudMana.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMana.Name = "nudMana";
            this.nudMana.Size = new System.Drawing.Size(72, 20);
            this.nudMana.TabIndex = 44;
            this.nudMana.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMana.ValueChanged += new System.EventHandler(this.nudMana_ValueChanged);
            // 
            // nudHp
            // 
            this.nudHp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHp.Location = new System.Drawing.Point(31, 16);
            this.nudHp.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHp.Name = "nudHp";
            this.nudHp.Size = new System.Drawing.Size(77, 20);
            this.nudHp.TabIndex = 43;
            this.nudHp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHp.ValueChanged += new System.EventHandler(this.nudHp_ValueChanged);
            // 
            // nudSpd
            // 
            this.nudSpd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpd.Location = new System.Drawing.Point(13, 128);
            this.nudSpd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpd.Name = "nudSpd";
            this.nudSpd.Size = new System.Drawing.Size(77, 20);
            this.nudSpd.TabIndex = 42;
            this.nudSpd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpd.ValueChanged += new System.EventHandler(this.nudSpd_ValueChanged);
            // 
            // nudMR
            // 
            this.nudMR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMR.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMR.Location = new System.Drawing.Point(105, 92);
            this.nudMR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMR.Name = "nudMR";
            this.nudMR.Size = new System.Drawing.Size(79, 20);
            this.nudMR.TabIndex = 41;
            this.nudMR.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMR.ValueChanged += new System.EventHandler(this.nudMR_ValueChanged);
            // 
            // nudDef
            // 
            this.nudDef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDef.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDef.Location = new System.Drawing.Point(12, 92);
            this.nudDef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDef.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.nudDef.Name = "nudDef";
            this.nudDef.Size = new System.Drawing.Size(79, 20);
            this.nudDef.TabIndex = 40;
            this.nudDef.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDef.ValueChanged += new System.EventHandler(this.nudDef_ValueChanged);
            // 
            // nudMag
            // 
            this.nudMag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMag.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMag.Location = new System.Drawing.Point(105, 55);
            this.nudMag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMag.Name = "nudMag";
            this.nudMag.Size = new System.Drawing.Size(77, 20);
            this.nudMag.TabIndex = 39;
            this.nudMag.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMag.ValueChanged += new System.EventHandler(this.nudMag_ValueChanged);
            // 
            // nudStr
            // 
            this.nudStr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStr.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStr.Location = new System.Drawing.Point(13, 55);
            this.nudStr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(77, 20);
            this.nudStr.TabIndex = 38;
            this.nudStr.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStr.ValueChanged += new System.EventHandler(this.nudStr_ValueChanged);
            // 
            // lblSpd
            // 
            this.lblSpd.AutoSize = true;
            this.lblSpd.Location = new System.Drawing.Point(10, 115);
            this.lblSpd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpd.Name = "lblSpd";
            this.lblSpd.Size = new System.Drawing.Size(71, 13);
            this.lblSpd.TabIndex = 37;
            this.lblSpd.Text = "Move Speed:";
            // 
            // lblMR
            // 
            this.lblMR.AutoSize = true;
            this.lblMR.Location = new System.Drawing.Point(104, 76);
            this.lblMR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMR.Name = "lblMR";
            this.lblMR.Size = new System.Drawing.Size(71, 13);
            this.lblMR.TabIndex = 36;
            this.lblMR.Text = "Magic Resist:";
            // 
            // lblDef
            // 
            this.lblDef.AutoSize = true;
            this.lblDef.Location = new System.Drawing.Point(9, 76);
            this.lblDef.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDef.Name = "lblDef";
            this.lblDef.Size = new System.Drawing.Size(50, 13);
            this.lblDef.TabIndex = 35;
            this.lblDef.Text = "Defense:";
            // 
            // lblMag
            // 
            this.lblMag.AutoSize = true;
            this.lblMag.Location = new System.Drawing.Point(106, 38);
            this.lblMag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMag.Name = "lblMag";
            this.lblMag.Size = new System.Drawing.Size(39, 13);
            this.lblMag.TabIndex = 34;
            this.lblMag.Text = "Magic:";
            // 
            // lblStr
            // 
            this.lblStr.AutoSize = true;
            this.lblStr.Location = new System.Drawing.Point(9, 39);
            this.lblStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStr.Name = "lblStr";
            this.lblStr.Size = new System.Drawing.Size(50, 13);
            this.lblStr.TabIndex = 33;
            this.lblStr.Text = "Strength:";
            // 
            // lblMana
            // 
            this.lblMana.AutoSize = true;
            this.lblMana.Location = new System.Drawing.Point(108, 18);
            this.lblMana.Name = "lblMana";
            this.lblMana.Size = new System.Drawing.Size(26, 13);
            this.lblMana.TabIndex = 15;
            this.lblMana.Text = "MP:";
            // 
            // lblHP
            // 
            this.lblHP.AutoSize = true;
            this.lblHP.Location = new System.Drawing.Point(10, 19);
            this.lblHP.Name = "lblHP";
            this.lblHP.Size = new System.Drawing.Size(25, 13);
            this.lblHP.TabIndex = 14;
            this.lblHP.Text = "HP:";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.DarkGroupBox4);
            this.pnlContainer.Controls.Add(this.grpAnimation);
            this.pnlContainer.Controls.Add(this.grpImmunities);
            this.pnlContainer.Controls.Add(this.grpCombat);
            this.pnlContainer.Controls.Add(this.grpRegen);
            this.pnlContainer.Controls.Add(this.grpGeneral);
            this.pnlContainer.Controls.Add(this.grpStats);
            this.pnlContainer.Location = new System.Drawing.Point(225, 39);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(464, 537);
            this.pnlContainer.TabIndex = 17;
            // 
            // DarkGroupBox4
            // 
            this.DarkGroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.DarkGroupBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.DarkGroupBox4.Controls.Add(this.pnlPetlevel);
            this.DarkGroupBox4.Controls.Add(this.optDoNotLevel);
            this.DarkGroupBox4.Controls.Add(this.optLevel);
            this.DarkGroupBox4.ForeColor = System.Drawing.Color.Gainsboro;
            this.DarkGroupBox4.Location = new System.Drawing.Point(0, 536);
            this.DarkGroupBox4.Name = "DarkGroupBox4";
            this.DarkGroupBox4.Size = new System.Drawing.Size(207, 257);
            this.DarkGroupBox4.TabIndex = 35;
            this.DarkGroupBox4.TabStop = false;
            this.DarkGroupBox4.Text = "Leveling";
            // 
            // pnlPetlevel
            // 
            this.pnlPetlevel.Controls.Add(this.nudPetPnts);
            this.pnlPetlevel.Controls.Add(this.DarkLabel13);
            this.pnlPetlevel.Controls.Add(this.DarkGroupBox5);
            this.pnlPetlevel.Controls.Add(this.nudMaxLevel);
            this.pnlPetlevel.Controls.Add(this.DarkLabel12);
            this.pnlPetlevel.Controls.Add(this.nudPetExp);
            this.pnlPetlevel.Controls.Add(this.DarkLabel11);
            this.pnlPetlevel.Location = new System.Drawing.Point(6, 65);
            this.pnlPetlevel.Name = "pnlPetlevel";
            this.pnlPetlevel.Size = new System.Drawing.Size(203, 202);
            this.pnlPetlevel.TabIndex = 2;
            // 
            // nudPetPnts
            // 
            this.nudPetPnts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPetPnts.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPetPnts.Location = new System.Drawing.Point(100, 66);
            this.nudPetPnts.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPetPnts.Name = "nudPetPnts";
            this.nudPetPnts.Size = new System.Drawing.Size(55, 20);
            this.nudPetPnts.TabIndex = 9;
            this.nudPetPnts.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // DarkLabel13
            // 
            this.DarkLabel13.AutoSize = true;
            this.DarkLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.DarkLabel13.Location = new System.Drawing.Point(7, 69);
            this.DarkLabel13.Name = "DarkLabel13";
            this.DarkLabel13.Size = new System.Drawing.Size(87, 13);
            this.DarkLabel13.TabIndex = 8;
            this.DarkLabel13.Text = "Points Per Level:";
            // 
            // DarkGroupBox5
            // 
            this.DarkGroupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.DarkGroupBox5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.DarkGroupBox5.Controls.Add(this.cmbEvolve);
            this.DarkGroupBox5.Controls.Add(this.DarkLabel15);
            this.DarkGroupBox5.Controls.Add(this.nudEvolveLvl);
            this.DarkGroupBox5.Controls.Add(this.DarkLabel14);
            this.DarkGroupBox5.Controls.Add(this.chkEvolve);
            this.DarkGroupBox5.ForeColor = System.Drawing.Color.Gainsboro;
            this.DarkGroupBox5.Location = new System.Drawing.Point(6, 94);
            this.DarkGroupBox5.Name = "DarkGroupBox5";
            this.DarkGroupBox5.Size = new System.Drawing.Size(192, 98);
            this.DarkGroupBox5.TabIndex = 7;
            this.DarkGroupBox5.TabStop = false;
            this.DarkGroupBox5.Text = "Evolution";
            // 
            // cmbEvolve
            // 
            this.cmbEvolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEvolve.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEvolve.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEvolve.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEvolve.DrawDropdownHoverOutline = false;
            this.cmbEvolve.DrawFocusRectangle = false;
            this.cmbEvolve.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEvolve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEvolve.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEvolve.FormattingEnabled = true;
            this.cmbEvolve.Location = new System.Drawing.Point(102, 65);
            this.cmbEvolve.Name = "cmbEvolve";
            this.cmbEvolve.Size = new System.Drawing.Size(83, 21);
            this.cmbEvolve.TabIndex = 4;
            this.cmbEvolve.Text = null;
            this.cmbEvolve.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // DarkLabel15
            // 
            this.DarkLabel15.AutoSize = true;
            this.DarkLabel15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.DarkLabel15.Location = new System.Drawing.Point(6, 64);
            this.DarkLabel15.Name = "DarkLabel15";
            this.DarkLabel15.Size = new System.Drawing.Size(68, 13);
            this.DarkLabel15.TabIndex = 3;
            this.DarkLabel15.Text = "Evolves into:";
            // 
            // nudEvolveLvl
            // 
            this.nudEvolveLvl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEvolveLvl.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEvolveLvl.Location = new System.Drawing.Point(105, 39);
            this.nudEvolveLvl.Name = "nudEvolveLvl";
            this.nudEvolveLvl.Size = new System.Drawing.Size(80, 20);
            this.nudEvolveLvl.TabIndex = 2;
            this.nudEvolveLvl.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // DarkLabel14
            // 
            this.DarkLabel14.AutoSize = true;
            this.DarkLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.DarkLabel14.Location = new System.Drawing.Point(7, 41);
            this.DarkLabel14.Name = "DarkLabel14";
            this.DarkLabel14.Size = new System.Drawing.Size(92, 13);
            this.DarkLabel14.TabIndex = 1;
            this.DarkLabel14.Text = "Evolves on Level:";
            // 
            // chkEvolve
            // 
            this.chkEvolve.AutoSize = true;
            this.chkEvolve.Location = new System.Drawing.Point(6, 19);
            this.chkEvolve.Name = "chkEvolve";
            this.chkEvolve.Size = new System.Drawing.Size(100, 17);
            this.chkEvolve.TabIndex = 0;
            this.chkEvolve.Text = "Pet Can Evolve";
            // 
            // nudMaxLevel
            // 
            this.nudMaxLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxLevel.Location = new System.Drawing.Point(100, 38);
            this.nudMaxLevel.Name = "nudMaxLevel";
            this.nudMaxLevel.Size = new System.Drawing.Size(47, 20);
            this.nudMaxLevel.TabIndex = 6;
            this.nudMaxLevel.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // DarkLabel12
            // 
            this.DarkLabel12.AutoSize = true;
            this.DarkLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.DarkLabel12.Location = new System.Drawing.Point(6, 43);
            this.DarkLabel12.Name = "DarkLabel12";
            this.DarkLabel12.Size = new System.Drawing.Size(59, 13);
            this.DarkLabel12.TabIndex = 5;
            this.DarkLabel12.Text = "Max Level:";
            // 
            // nudPetExp
            // 
            this.nudPetExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPetExp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPetExp.Location = new System.Drawing.Point(100, 12);
            this.nudPetExp.Name = "nudPetExp";
            this.nudPetExp.Size = new System.Drawing.Size(47, 20);
            this.nudPetExp.TabIndex = 1;
            this.nudPetExp.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // DarkLabel11
            // 
            this.DarkLabel11.AutoSize = true;
            this.DarkLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.DarkLabel11.Location = new System.Drawing.Point(4, 14);
            this.DarkLabel11.Name = "DarkLabel11";
            this.DarkLabel11.Size = new System.Drawing.Size(64, 13);
            this.DarkLabel11.TabIndex = 0;
            this.DarkLabel11.Text = "Exp Gain %:";
            // 
            // optDoNotLevel
            // 
            this.optDoNotLevel.AutoSize = true;
            this.optDoNotLevel.Location = new System.Drawing.Point(6, 42);
            this.optDoNotLevel.Name = "optDoNotLevel";
            this.optDoNotLevel.Size = new System.Drawing.Size(113, 17);
            this.optDoNotLevel.TabIndex = 1;
            this.optDoNotLevel.Text = "Does Not LevelUp";
            // 
            // optLevel
            // 
            this.optLevel.AutoSize = true;
            this.optLevel.Location = new System.Drawing.Point(6, 19);
            this.optLevel.Name = "optLevel";
            this.optLevel.Size = new System.Drawing.Size(121, 17);
            this.optLevel.TabIndex = 0;
            this.optLevel.Text = "Level by Experience";
            // 
            // grpAnimation
            // 
            this.grpAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAnimation.Controls.Add(this.cmbDeathAnimation);
            this.grpAnimation.Controls.Add(this.lblDeathAnimation);
            this.grpAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAnimation.Location = new System.Drawing.Point(212, 500);
            this.grpAnimation.Margin = new System.Windows.Forms.Padding(2);
            this.grpAnimation.Name = "grpAnimation";
            this.grpAnimation.Padding = new System.Windows.Forms.Padding(2);
            this.grpAnimation.Size = new System.Drawing.Size(229, 69);
            this.grpAnimation.TabIndex = 34;
            this.grpAnimation.TabStop = false;
            this.grpAnimation.Text = "Animations";
            // 
            // cmbDeathAnimation
            // 
            this.cmbDeathAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDeathAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDeathAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDeathAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDeathAnimation.DrawDropdownHoverOutline = false;
            this.cmbDeathAnimation.DrawFocusRectangle = false;
            this.cmbDeathAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDeathAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeathAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDeathAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDeathAnimation.FormattingEnabled = true;
            this.cmbDeathAnimation.Location = new System.Drawing.Point(19, 36);
            this.cmbDeathAnimation.Name = "cmbDeathAnimation";
            this.cmbDeathAnimation.Size = new System.Drawing.Size(182, 21);
            this.cmbDeathAnimation.TabIndex = 23;
            this.cmbDeathAnimation.Text = null;
            this.cmbDeathAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDeathAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbDeathAnimation_SelectedIndexChanged_1);
            // 
            // lblDeathAnimation
            // 
            this.lblDeathAnimation.AutoSize = true;
            this.lblDeathAnimation.Location = new System.Drawing.Point(16, 20);
            this.lblDeathAnimation.Name = "lblDeathAnimation";
            this.lblDeathAnimation.Size = new System.Drawing.Size(85, 13);
            this.lblDeathAnimation.TabIndex = 22;
            this.lblDeathAnimation.Text = "Death Animation";
            // 
            // grpImmunities
            // 
            this.grpImmunities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpImmunities.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpImmunities.Controls.Add(this.chkTaunt);
            this.grpImmunities.Controls.Add(this.chkSleep);
            this.grpImmunities.Controls.Add(this.chkTransform);
            this.grpImmunities.Controls.Add(this.chkBlind);
            this.grpImmunities.Controls.Add(this.chkSnare);
            this.grpImmunities.Controls.Add(this.chkStun);
            this.grpImmunities.Controls.Add(this.chkSilence);
            this.grpImmunities.Controls.Add(this.chkKnockback);
            this.grpImmunities.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpImmunities.Location = new System.Drawing.Point(212, 575);
            this.grpImmunities.Margin = new System.Windows.Forms.Padding(2);
            this.grpImmunities.Name = "grpImmunities";
            this.grpImmunities.Padding = new System.Windows.Forms.Padding(2);
            this.grpImmunities.Size = new System.Drawing.Size(229, 141);
            this.grpImmunities.TabIndex = 33;
            this.grpImmunities.TabStop = false;
            this.grpImmunities.Text = "Immunities";
            // 
            // chkTaunt
            // 
            this.chkTaunt.AutoSize = true;
            this.chkTaunt.Location = new System.Drawing.Point(116, 95);
            this.chkTaunt.Name = "chkTaunt";
            this.chkTaunt.Size = new System.Drawing.Size(54, 17);
            this.chkTaunt.TabIndex = 86;
            this.chkTaunt.Text = "Taunt";
            this.chkTaunt.CheckedChanged += new System.EventHandler(this.chkTaunt_CheckedChanged);
            // 
            // chkSleep
            // 
            this.chkSleep.AutoSize = true;
            this.chkSleep.Location = new System.Drawing.Point(11, 95);
            this.chkSleep.Name = "chkSleep";
            this.chkSleep.Size = new System.Drawing.Size(53, 17);
            this.chkSleep.TabIndex = 85;
            this.chkSleep.Text = "Sleep";
            this.chkSleep.CheckedChanged += new System.EventHandler(this.chkSleep_CheckedChanged);
            // 
            // chkTransform
            // 
            this.chkTransform.AutoSize = true;
            this.chkTransform.Location = new System.Drawing.Point(116, 72);
            this.chkTransform.Name = "chkTransform";
            this.chkTransform.Size = new System.Drawing.Size(73, 17);
            this.chkTransform.TabIndex = 84;
            this.chkTransform.Text = "Transform";
            this.chkTransform.CheckedChanged += new System.EventHandler(this.chkTransform_CheckedChanged);
            // 
            // chkBlind
            // 
            this.chkBlind.AutoSize = true;
            this.chkBlind.Location = new System.Drawing.Point(11, 72);
            this.chkBlind.Name = "chkBlind";
            this.chkBlind.Size = new System.Drawing.Size(49, 17);
            this.chkBlind.TabIndex = 83;
            this.chkBlind.Text = "Blind";
            this.chkBlind.CheckedChanged += new System.EventHandler(this.chkBlind_CheckedChanged);
            // 
            // chkSnare
            // 
            this.chkSnare.AutoSize = true;
            this.chkSnare.Location = new System.Drawing.Point(116, 49);
            this.chkSnare.Name = "chkSnare";
            this.chkSnare.Size = new System.Drawing.Size(54, 17);
            this.chkSnare.TabIndex = 82;
            this.chkSnare.Text = "Snare";
            this.chkSnare.CheckedChanged += new System.EventHandler(this.chkSnare_CheckedChanged);
            // 
            // chkStun
            // 
            this.chkStun.AutoSize = true;
            this.chkStun.Location = new System.Drawing.Point(11, 49);
            this.chkStun.Name = "chkStun";
            this.chkStun.Size = new System.Drawing.Size(48, 17);
            this.chkStun.TabIndex = 81;
            this.chkStun.Text = "Stun";
            this.chkStun.CheckedChanged += new System.EventHandler(this.chkStun_CheckedChanged);
            // 
            // chkSilence
            // 
            this.chkSilence.AutoSize = true;
            this.chkSilence.Location = new System.Drawing.Point(116, 26);
            this.chkSilence.Name = "chkSilence";
            this.chkSilence.Size = new System.Drawing.Size(61, 17);
            this.chkSilence.TabIndex = 80;
            this.chkSilence.Text = "Silence";
            this.chkSilence.CheckedChanged += new System.EventHandler(this.chkSilence_CheckedChanged);
            // 
            // chkKnockback
            // 
            this.chkKnockback.AutoSize = true;
            this.chkKnockback.Location = new System.Drawing.Point(11, 26);
            this.chkKnockback.Name = "chkKnockback";
            this.chkKnockback.Size = new System.Drawing.Size(81, 17);
            this.chkKnockback.TabIndex = 79;
            this.chkKnockback.Text = "Knockback";
            this.chkKnockback.CheckedChanged += new System.EventHandler(this.chkKnockback_CheckedChanged);
            // 
            // grpCombat
            // 
            this.grpCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCombat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCombat.Controls.Add(this.grpAttackSpeed);
            this.grpCombat.Controls.Add(this.nudCritMultiplier);
            this.grpCombat.Controls.Add(this.lblCritMultiplier);
            this.grpCombat.Controls.Add(this.nudScaling);
            this.grpCombat.Controls.Add(this.nudDamage);
            this.grpCombat.Controls.Add(this.nudCritChance);
            this.grpCombat.Controls.Add(this.cmbScalingStat);
            this.grpCombat.Controls.Add(this.lblScalingStat);
            this.grpCombat.Controls.Add(this.lblScaling);
            this.grpCombat.Controls.Add(this.cmbDamageType);
            this.grpCombat.Controls.Add(this.lblDamageType);
            this.grpCombat.Controls.Add(this.lblCritChance);
            this.grpCombat.Controls.Add(this.cmbAttackAnimation);
            this.grpCombat.Controls.Add(this.lblAttackAnimation);
            this.grpCombat.Controls.Add(this.lblDamage);
            this.grpCombat.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCombat.Location = new System.Drawing.Point(215, 84);
            this.grpCombat.Name = "grpCombat";
            this.grpCombat.Size = new System.Drawing.Size(226, 411);
            this.grpCombat.TabIndex = 17;
            this.grpCombat.TabStop = false;
            this.grpCombat.Text = "Combat";
            // 
            // grpAttackSpeed
            // 
            this.grpAttackSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAttackSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAttackSpeed.Controls.Add(this.nudAttackSpeedValue);
            this.grpAttackSpeed.Controls.Add(this.lblAttackSpeedValue);
            this.grpAttackSpeed.Controls.Add(this.cmbAttackSpeedModifier);
            this.grpAttackSpeed.Controls.Add(this.lblAttackSpeedModifier);
            this.grpAttackSpeed.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAttackSpeed.Location = new System.Drawing.Point(12, 310);
            this.grpAttackSpeed.Name = "grpAttackSpeed";
            this.grpAttackSpeed.Size = new System.Drawing.Size(192, 86);
            this.grpAttackSpeed.TabIndex = 64;
            this.grpAttackSpeed.TabStop = false;
            this.grpAttackSpeed.Text = "Attack Speed";
            // 
            // nudAttackSpeedValue
            // 
            this.nudAttackSpeedValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAttackSpeedValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAttackSpeedValue.Location = new System.Drawing.Point(60, 58);
            this.nudAttackSpeedValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAttackSpeedValue.Name = "nudAttackSpeedValue";
            this.nudAttackSpeedValue.Size = new System.Drawing.Size(114, 20);
            this.nudAttackSpeedValue.TabIndex = 56;
            this.nudAttackSpeedValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAttackSpeedValue.ValueChanged += new System.EventHandler(this.nudAttackSpeedValue_ValueChanged);
            // 
            // lblAttackSpeedValue
            // 
            this.lblAttackSpeedValue.AutoSize = true;
            this.lblAttackSpeedValue.Location = new System.Drawing.Point(9, 60);
            this.lblAttackSpeedValue.Name = "lblAttackSpeedValue";
            this.lblAttackSpeedValue.Size = new System.Drawing.Size(37, 13);
            this.lblAttackSpeedValue.TabIndex = 29;
            this.lblAttackSpeedValue.Text = "Value:";
            // 
            // cmbAttackSpeedModifier
            // 
            this.cmbAttackSpeedModifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAttackSpeedModifier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAttackSpeedModifier.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAttackSpeedModifier.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAttackSpeedModifier.DrawDropdownHoverOutline = false;
            this.cmbAttackSpeedModifier.DrawFocusRectangle = false;
            this.cmbAttackSpeedModifier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAttackSpeedModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttackSpeedModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAttackSpeedModifier.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAttackSpeedModifier.FormattingEnabled = true;
            this.cmbAttackSpeedModifier.Location = new System.Drawing.Point(60, 24);
            this.cmbAttackSpeedModifier.Name = "cmbAttackSpeedModifier";
            this.cmbAttackSpeedModifier.Size = new System.Drawing.Size(114, 21);
            this.cmbAttackSpeedModifier.TabIndex = 28;
            this.cmbAttackSpeedModifier.Text = null;
            this.cmbAttackSpeedModifier.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackSpeedModifier.SelectedIndexChanged += new System.EventHandler(this.cmbAttackSpeedModifier_SelectedIndexChanged);
            // 
            // lblAttackSpeedModifier
            // 
            this.lblAttackSpeedModifier.AutoSize = true;
            this.lblAttackSpeedModifier.Location = new System.Drawing.Point(9, 27);
            this.lblAttackSpeedModifier.Name = "lblAttackSpeedModifier";
            this.lblAttackSpeedModifier.Size = new System.Drawing.Size(47, 13);
            this.lblAttackSpeedModifier.TabIndex = 0;
            this.lblAttackSpeedModifier.Text = "Modifier:";
            // 
            // nudCritMultiplier
            // 
            this.nudCritMultiplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritMultiplier.DecimalPlaces = 2;
            this.nudCritMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudCritMultiplier.Location = new System.Drawing.Point(13, 110);
            this.nudCritMultiplier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCritMultiplier.Name = "nudCritMultiplier";
            this.nudCritMultiplier.Size = new System.Drawing.Size(191, 20);
            this.nudCritMultiplier.TabIndex = 63;
            this.nudCritMultiplier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritMultiplier.ValueChanged += new System.EventHandler(this.nudCritMultiplier_ValueChanged);
            // 
            // lblCritMultiplier
            // 
            this.lblCritMultiplier.AutoSize = true;
            this.lblCritMultiplier.Location = new System.Drawing.Point(10, 96);
            this.lblCritMultiplier.Name = "lblCritMultiplier";
            this.lblCritMultiplier.Size = new System.Drawing.Size(135, 13);
            this.lblCritMultiplier.TabIndex = 62;
            this.lblCritMultiplier.Text = "Crit Multiplier (Default 1.5x):";
            // 
            // nudScaling
            // 
            this.nudScaling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudScaling.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudScaling.Location = new System.Drawing.Point(12, 238);
            this.nudScaling.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudScaling.Name = "nudScaling";
            this.nudScaling.Size = new System.Drawing.Size(191, 20);
            this.nudScaling.TabIndex = 61;
            this.nudScaling.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudScaling.ValueChanged += new System.EventHandler(this.nudScaling_ValueChanged);
            // 
            // nudDamage
            // 
            this.nudDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDamage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDamage.Location = new System.Drawing.Point(12, 34);
            this.nudDamage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDamage.Name = "nudDamage";
            this.nudDamage.Size = new System.Drawing.Size(192, 20);
            this.nudDamage.TabIndex = 60;
            this.nudDamage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDamage.ValueChanged += new System.EventHandler(this.nudDamage_ValueChanged);
            // 
            // nudCritChance
            // 
            this.nudCritChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritChance.Location = new System.Drawing.Point(13, 71);
            this.nudCritChance.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCritChance.Name = "nudCritChance";
            this.nudCritChance.Size = new System.Drawing.Size(191, 20);
            this.nudCritChance.TabIndex = 59;
            this.nudCritChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritChance.ValueChanged += new System.EventHandler(this.nudCritChance_ValueChanged);
            // 
            // cmbScalingStat
            // 
            this.cmbScalingStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbScalingStat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbScalingStat.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbScalingStat.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbScalingStat.DrawDropdownHoverOutline = false;
            this.cmbScalingStat.DrawFocusRectangle = false;
            this.cmbScalingStat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbScalingStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScalingStat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbScalingStat.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbScalingStat.FormattingEnabled = true;
            this.cmbScalingStat.Location = new System.Drawing.Point(13, 192);
            this.cmbScalingStat.Name = "cmbScalingStat";
            this.cmbScalingStat.Size = new System.Drawing.Size(191, 21);
            this.cmbScalingStat.TabIndex = 58;
            this.cmbScalingStat.Text = null;
            this.cmbScalingStat.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbScalingStat.SelectedIndexChanged += new System.EventHandler(this.cmbScalingStat_SelectedIndexChanged);
            // 
            // lblScalingStat
            // 
            this.lblScalingStat.AutoSize = true;
            this.lblScalingStat.Location = new System.Drawing.Point(10, 175);
            this.lblScalingStat.Name = "lblScalingStat";
            this.lblScalingStat.Size = new System.Drawing.Size(67, 13);
            this.lblScalingStat.TabIndex = 57;
            this.lblScalingStat.Text = "Scaling Stat:";
            // 
            // lblScaling
            // 
            this.lblScaling.AutoSize = true;
            this.lblScaling.Location = new System.Drawing.Point(9, 218);
            this.lblScaling.Name = "lblScaling";
            this.lblScaling.Size = new System.Drawing.Size(84, 13);
            this.lblScaling.TabIndex = 56;
            this.lblScaling.Text = "Scaling Amount:";
            // 
            // cmbDamageType
            // 
            this.cmbDamageType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDamageType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDamageType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDamageType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDamageType.DrawDropdownHoverOutline = false;
            this.cmbDamageType.DrawFocusRectangle = false;
            this.cmbDamageType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDamageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDamageType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDamageType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDamageType.FormattingEnabled = true;
            this.cmbDamageType.Items.AddRange(new object[] {
            "Physical",
            "Magic",
            "True"});
            this.cmbDamageType.Location = new System.Drawing.Point(13, 151);
            this.cmbDamageType.Name = "cmbDamageType";
            this.cmbDamageType.Size = new System.Drawing.Size(191, 21);
            this.cmbDamageType.TabIndex = 54;
            this.cmbDamageType.Text = "Physical";
            this.cmbDamageType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDamageType.SelectedIndexChanged += new System.EventHandler(this.cmbDamageType_SelectedIndexChanged);
            // 
            // lblDamageType
            // 
            this.lblDamageType.AutoSize = true;
            this.lblDamageType.Location = new System.Drawing.Point(10, 134);
            this.lblDamageType.Name = "lblDamageType";
            this.lblDamageType.Size = new System.Drawing.Size(77, 13);
            this.lblDamageType.TabIndex = 53;
            this.lblDamageType.Text = "Damage Type:";
            // 
            // lblCritChance
            // 
            this.lblCritChance.AutoSize = true;
            this.lblCritChance.Location = new System.Drawing.Point(9, 58);
            this.lblCritChance.Name = "lblCritChance";
            this.lblCritChance.Size = new System.Drawing.Size(82, 13);
            this.lblCritChance.TabIndex = 52;
            this.lblCritChance.Text = "Crit Chance (%):";
            // 
            // cmbAttackAnimation
            // 
            this.cmbAttackAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAttackAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAttackAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAttackAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAttackAnimation.DrawDropdownHoverOutline = false;
            this.cmbAttackAnimation.DrawFocusRectangle = false;
            this.cmbAttackAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAttackAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttackAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAttackAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAttackAnimation.FormattingEnabled = true;
            this.cmbAttackAnimation.Location = new System.Drawing.Point(12, 275);
            this.cmbAttackAnimation.Name = "cmbAttackAnimation";
            this.cmbAttackAnimation.Size = new System.Drawing.Size(192, 21);
            this.cmbAttackAnimation.TabIndex = 50;
            this.cmbAttackAnimation.Text = null;
            this.cmbAttackAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAttackAnimation_SelectedIndexChanged);
            // 
            // lblAttackAnimation
            // 
            this.lblAttackAnimation.AutoSize = true;
            this.lblAttackAnimation.Location = new System.Drawing.Point(9, 260);
            this.lblAttackAnimation.Name = "lblAttackAnimation";
            this.lblAttackAnimation.Size = new System.Drawing.Size(90, 13);
            this.lblAttackAnimation.TabIndex = 49;
            this.lblAttackAnimation.Text = "Attack Animation:";
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(9, 18);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(77, 13);
            this.lblDamage.TabIndex = 48;
            this.lblDamage.Text = "Base Damage:";
            // 
            // grpRegen
            // 
            this.grpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRegen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRegen.Controls.Add(this.nudMpRegen);
            this.grpRegen.Controls.Add(this.nudHpRegen);
            this.grpRegen.Controls.Add(this.lblHpRegen);
            this.grpRegen.Controls.Add(this.lblManaRegen);
            this.grpRegen.Controls.Add(this.lblRegenHint);
            this.grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRegen.Location = new System.Drawing.Point(2, 420);
            this.grpRegen.Margin = new System.Windows.Forms.Padding(2);
            this.grpRegen.Name = "grpRegen";
            this.grpRegen.Padding = new System.Windows.Forms.Padding(2);
            this.grpRegen.Size = new System.Drawing.Size(206, 99);
            this.grpRegen.TabIndex = 31;
            this.grpRegen.TabStop = false;
            this.grpRegen.Text = "Regen";
            // 
            // nudMpRegen
            // 
            this.nudMpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMpRegen.Location = new System.Drawing.Point(8, 69);
            this.nudMpRegen.Name = "nudMpRegen";
            this.nudMpRegen.Size = new System.Drawing.Size(86, 20);
            this.nudMpRegen.TabIndex = 31;
            this.nudMpRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMpRegen.ValueChanged += new System.EventHandler(this.nudMpRegen_ValueChanged);
            // 
            // nudHpRegen
            // 
            this.nudHpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHpRegen.Location = new System.Drawing.Point(8, 31);
            this.nudHpRegen.Name = "nudHpRegen";
            this.nudHpRegen.Size = new System.Drawing.Size(86, 20);
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
            this.lblHpRegen.Location = new System.Drawing.Point(5, 17);
            this.lblHpRegen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHpRegen.Name = "lblHpRegen";
            this.lblHpRegen.Size = new System.Drawing.Size(42, 13);
            this.lblHpRegen.TabIndex = 26;
            this.lblHpRegen.Text = "HP: (%)";
            // 
            // lblManaRegen
            // 
            this.lblManaRegen.AutoSize = true;
            this.lblManaRegen.Location = new System.Drawing.Point(5, 54);
            this.lblManaRegen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManaRegen.Name = "lblManaRegen";
            this.lblManaRegen.Size = new System.Drawing.Size(54, 13);
            this.lblManaRegen.TabIndex = 27;
            this.lblManaRegen.Text = "Mana: (%)";
            // 
            // lblRegenHint
            // 
            this.lblRegenHint.Location = new System.Drawing.Point(102, 26);
            this.lblRegenHint.Name = "lblRegenHint";
            this.lblRegenHint.Size = new System.Drawing.Size(100, 72);
            this.lblRegenHint.TabIndex = 0;
            this.lblRegenHint.Text = "% of HP/Mana to restore per tick.\r\n\r\nTick timer saved in server config.json.";
            // 
            // chkSwarm
            // 
            this.chkSwarm.Location = new System.Drawing.Point(0, 0);
            this.chkSwarm.Name = "chkSwarm";
            this.chkSwarm.Size = new System.Drawing.Size(104, 24);
            this.chkSwarm.TabIndex = 0;
            // 
            // chkAggressive
            // 
            this.chkAggressive.Location = new System.Drawing.Point(0, 0);
            this.chkAggressive.Name = "chkAggressive";
            this.chkAggressive.Size = new System.Drawing.Size(104, 24);
            this.chkAggressive.TabIndex = 0;
            // 
            // cmbSpell
            // 
            this.cmbSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpell.DrawDropdownHoverOutline = false;
            this.cmbSpell.DrawFocusRectangle = false;
            this.cmbSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpell.Location = new System.Drawing.Point(0, 0);
            this.cmbSpell.Name = "cmbSpell";
            this.cmbSpell.Size = new System.Drawing.Size(121, 21);
            this.cmbSpell.TabIndex = 0;
            this.cmbSpell.Text = null;
            this.cmbSpell.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbFreq
            // 
            this.cmbFreq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFreq.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFreq.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFreq.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFreq.DrawDropdownHoverOutline = false;
            this.cmbFreq.DrawFocusRectangle = false;
            this.cmbFreq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFreq.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFreq.Location = new System.Drawing.Point(0, 0);
            this.cmbFreq.Name = "cmbFreq";
            this.cmbFreq.Size = new System.Drawing.Size(121, 21);
            this.cmbFreq.TabIndex = 0;
            this.cmbFreq.Text = null;
            this.cmbFreq.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(13, 260);
            this.lblFreq.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(38, 16);
            this.lblFreq.TabIndex = 41;
            this.lblFreq.Text = "Freq:";
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(13, 159);
            this.lblSpell.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(41, 16);
            this.lblSpell.TabIndex = 39;
            this.lblSpell.Text = "Spell:";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(156, 217);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnRemove.Size = new System.Drawing.Size(100, 28);
            this.btnRemove.TabIndex = 38;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(16, 217);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnAdd.Size = new System.Drawing.Size(100, 28);
            this.btnAdd.TabIndex = 37;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstSpells
            // 
            this.lstSpells.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstSpells.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSpells.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstSpells.FormattingEnabled = true;
            this.lstSpells.ItemHeight = 16;
            this.lstSpells.Location = new System.Drawing.Point(16, 18);
            this.lstSpells.Margin = new System.Windows.Forms.Padding(4);
            this.lstSpells.Name = "lstSpells";
            this.lstSpells.Size = new System.Drawing.Size(239, 130);
            this.lstSpells.TabIndex = 29;
            this.lstSpells.SelectedIndexChanged += new System.EventHandler(this.lstSpells_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(476, 582);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(190, 27);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(271, 582);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(190, 27);
            this.btnSave.TabIndex = 18;
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
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(699, 25);
            this.toolStrip.TabIndex = 46;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripItemNew
            // 
            this.toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemNew.Image")));
            this.toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemNew.Name = "toolStripItemNew";
            this.toolStripItemNew.Size = new System.Drawing.Size(24, 22);
            this.toolStripItemNew.Text = "New";
            this.toolStripItemNew.Click += new System.EventHandler(this.toolStripItemNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemDelete
            // 
            this.toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemDelete.Enabled = false;
            this.toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemDelete.Image")));
            this.toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemDelete.Name = "toolStripItemDelete";
            this.toolStripItemDelete.Size = new System.Drawing.Size(24, 22);
            this.toolStripItemDelete.Text = "Delete";
            this.toolStripItemDelete.Click += new System.EventHandler(this.toolStripItemDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAlphabetical
            // 
            this.btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnAlphabetical.Image = ((System.Drawing.Image)(resources.GetObject("btnAlphabetical.Image")));
            this.btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlphabetical.Name = "btnAlphabetical";
            this.btnAlphabetical.Size = new System.Drawing.Size(24, 22);
            this.btnAlphabetical.Text = "Order Chronologically";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemCopy
            // 
            this.toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemCopy.Enabled = false;
            this.toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemCopy.Image")));
            this.toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemCopy.Name = "toolStripItemCopy";
            this.toolStripItemCopy.Size = new System.Drawing.Size(24, 22);
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
            this.toolStripItemPaste.Size = new System.Drawing.Size(24, 22);
            this.toolStripItemPaste.Text = "Paste";
            this.toolStripItemPaste.Click += new System.EventHandler(this.toolStripItemPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemUndo
            // 
            this.toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemUndo.Enabled = false;
            this.toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemUndo.Image")));
            this.toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemUndo.Name = "toolStripItemUndo";
            this.toolStripItemUndo.Size = new System.Drawing.Size(24, 22);
            this.toolStripItemUndo.Text = "Undo";
            this.toolStripItemUndo.Click += new System.EventHandler(this.toolStripItemUndo_Click);
            // 
            // FrmPet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(699, 615);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpPets);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmPet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pet Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPet_FormClosed);
            this.Load += new System.EventHandler(this.frmPet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
            this.grpPets.ResumeLayout(false);
            this.grpPets.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSightRange)).EndInit();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.DarkGroupBox4.ResumeLayout(false);
            this.DarkGroupBox4.PerformLayout();
            this.pnlPetlevel.ResumeLayout(false);
            this.pnlPetlevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPetPnts)).EndInit();
            this.DarkGroupBox5.ResumeLayout(false);
            this.DarkGroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvolveLvl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPetExp)).EndInit();
            this.grpAnimation.ResumeLayout(false);
            this.grpAnimation.PerformLayout();
            this.grpImmunities.ResumeLayout(false);
            this.grpImmunities.PerformLayout();
            this.grpCombat.ResumeLayout(false);
            this.grpCombat.PerformLayout();
            this.grpAttackSpeed.ResumeLayout(false);
            this.grpAttackSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).EndInit();
            this.grpRegen.ResumeLayout(false);
            this.grpRegen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpRegen)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DarkGroupBox grpPets;
        private DarkGroupBox grpGeneral;
        private DarkComboBox cmbSprite;
        private System.Windows.Forms.Label lblPic;
        private System.Windows.Forms.PictureBox picPet;
        private System.Windows.Forms.Label lblName;
        private DarkTextBox txtName;
        private DarkGroupBox grpStats;
        private System.Windows.Forms.Label lblMana;
        private System.Windows.Forms.Label lblHP;
        private System.Windows.Forms.Label lblSightRange;
        private System.Windows.Forms.Panel pnlContainer;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private DarkButton btnRemove;
        private DarkButton btnAdd;
        private System.Windows.Forms.ListBox lstSpells;
        private System.Windows.Forms.Label lblSpell;
        private DarkComboBox cmbFreq;
        private System.Windows.Forms.Label lblFreq;
        private DarkGroupBox grpCombat;
        private DarkComboBox cmbScalingStat;
        private System.Windows.Forms.Label lblScalingStat;
        private System.Windows.Forms.Label lblScaling;
        private DarkComboBox cmbDamageType;
        private System.Windows.Forms.Label lblDamageType;
        private System.Windows.Forms.Label lblCritChance;
        private DarkComboBox cmbAttackAnimation;
        private System.Windows.Forms.Label lblAttackAnimation;
        private System.Windows.Forms.Label lblDamage;
        private DarkComboBox cmbSpell;
        private DarkNumericUpDown nudSpd;
        private DarkNumericUpDown nudMR;
        private DarkNumericUpDown nudDef;
        private DarkNumericUpDown nudMag;
        private DarkNumericUpDown nudStr;
        private System.Windows.Forms.Label lblSpd;
        private System.Windows.Forms.Label lblMR;
        private System.Windows.Forms.Label lblDef;
        private System.Windows.Forms.Label lblMag;
        private System.Windows.Forms.Label lblStr;
        private DarkNumericUpDown nudScaling;
        private DarkNumericUpDown nudDamage;
        private DarkNumericUpDown nudCritChance;
        private DarkNumericUpDown nudSightRange;
        private DarkNumericUpDown nudMana;
        private DarkNumericUpDown nudHp;
        private System.Windows.Forms.Label lblLevel;
        private DarkNumericUpDown nudLevel;
        private DarkGroupBox grpRegen;
        private DarkNumericUpDown nudMpRegen;
        private DarkNumericUpDown nudHpRegen;
        private System.Windows.Forms.Label lblHpRegen;
        private System.Windows.Forms.Label lblManaRegen;
        private System.Windows.Forms.Label lblRegenHint;
        private DarkCheckBox chkSwarm;
        private DarkCheckBox chkAggressive;
        private DarkNumericUpDown nudCritMultiplier;
        private System.Windows.Forms.Label lblCritMultiplier;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkComboBox cmbFolder;
        private DarkGroupBox grpAttackSpeed;
        private DarkNumericUpDown nudAttackSpeedValue;
        private System.Windows.Forms.Label lblAttackSpeedValue;
        private DarkComboBox cmbAttackSpeedModifier;
        private System.Windows.Forms.Label lblAttackSpeedModifier;
        private Controls.GameObjectList lstGameObjects;
        private DarkGroupBox grpImmunities;
        private DarkCheckBox chkTaunt;
        private DarkCheckBox chkSleep;
        private DarkCheckBox chkTransform;
        private DarkCheckBox chkBlind;
        private DarkCheckBox chkSnare;
        private DarkCheckBox chkStun;
        private DarkCheckBox chkSilence;
        private DarkCheckBox chkKnockback;
        private DarkComboBox cmbDeathAnimation;
        private System.Windows.Forms.Label lblDeathAnimation;
        private DarkGroupBox grpAnimation;
        private DarkNumericUpDown nudAgi;
        private System.Windows.Forms.Label label1;
        private DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton toolStripItemCopy;
        public System.Windows.Forms.ToolStripButton toolStripItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripItemUndo;
        internal DarkGroupBox DarkGroupBox4;
        internal System.Windows.Forms.Panel pnlPetlevel;
        internal DarkGroupBox DarkGroupBox5;
        internal DarkComboBox cmbEvolve;
        internal DarkLabel DarkLabel15;
        internal DarkNumericUpDown nudEvolveLvl;
        internal DarkLabel DarkLabel14;
        internal DarkCheckBox chkEvolve;
        internal DarkNumericUpDown nudMaxLevel;
        internal DarkLabel DarkLabel12;
        internal DarkNumericUpDown nudPetExp;
        internal DarkLabel DarkLabel11;
        internal DarkRadioButton optDoNotLevel;
        internal DarkRadioButton optLevel;
        internal DarkNumericUpDown nudPetPnts;
        internal DarkLabel DarkLabel13;
    }
}
