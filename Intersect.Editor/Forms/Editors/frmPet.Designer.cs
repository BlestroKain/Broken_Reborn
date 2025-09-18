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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPet));
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
            grpPets = new DarkGroupBox();
            btnClearSearch = new DarkButton();
            txtSearch = new DarkTextBox();
            lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            pnlContainer = new System.Windows.Forms.Panel();
            grpImmunities = new DarkGroupBox();
            flpImmunities = new System.Windows.Forms.FlowLayoutPanel();
            lblTenacity = new System.Windows.Forms.Label();
            nudTenacity = new DarkNumericUpDown();
            grpSpells = new DarkGroupBox();
            cmbSpell = new DarkComboBox();
            btnRemoveSpell = new DarkButton();
            btnAddSpell = new DarkButton();
            lstSpells = new System.Windows.Forms.ListBox();
            grpCombat = new DarkGroupBox();
            nudAttackSpeedValue = new DarkNumericUpDown();
            lblAttackSpeedValue = new System.Windows.Forms.Label();
            cmbAttackSpeedModifier = new DarkComboBox();
            lblAttackSpeedModifier = new System.Windows.Forms.Label();
            lblCritMultiplier = new System.Windows.Forms.Label();
            nudCritMultiplier = new DarkNumericUpDown();
            lblCritChance = new System.Windows.Forms.Label();
            nudCritChance = new DarkNumericUpDown();
            lblDamage = new System.Windows.Forms.Label();
            nudDamage = new DarkNumericUpDown();
            lblScaling = new System.Windows.Forms.Label();
            nudScaling = new DarkNumericUpDown();
            lblScalingStat = new System.Windows.Forms.Label();
            cmbScalingStat = new DarkComboBox();
            lblDamageType = new System.Windows.Forms.Label();
            cmbDamageType = new DarkComboBox();
            lblAttackAnimation = new System.Windows.Forms.Label();
            cmbAttackAnimation = new DarkComboBox();
            lblDeathAnimation = new System.Windows.Forms.Label();
            cmbDeathAnimation = new DarkComboBox();
            lblIdleAnimation = new System.Windows.Forms.Label();
            cmbIdleAnimation = new DarkComboBox();
            grpVitals = new DarkGroupBox();
            flpVitals = new System.Windows.Forms.FlowLayoutPanel();
            flpVitalRegen = new System.Windows.Forms.FlowLayoutPanel();
            lblVitals = new System.Windows.Forms.Label();
            lblVitalRegen = new System.Windows.Forms.Label();
            grpStats = new DarkGroupBox();
            flpStats = new System.Windows.Forms.FlowLayoutPanel();
            grpGeneral = new DarkGroupBox();
            lblExperience = new System.Windows.Forms.Label();
            nudExperience = new DarkNumericUpDown();
            lblLevel = new System.Windows.Forms.Label();
            nudLevel = new DarkNumericUpDown();
            lblIdleSprite = new System.Windows.Forms.Label();
            cmbSprite = new DarkComboBox();
            btnAddFolder = new DarkButton();
            lblFolder = new System.Windows.Forms.Label();
            cmbFolder = new DarkComboBox();
            lblName = new System.Windows.Forms.Label();
            txtName = new DarkTextBox();
            btnSave = new DarkButton();
            btnCancel = new DarkButton();
            toolStrip.SuspendLayout();
            grpPets.SuspendLayout();
            pnlContainer.SuspendLayout();
            grpImmunities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTenacity).BeginInit();
            grpSpells.SuspendLayout();
            grpCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAttackSpeedValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCritMultiplier).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCritChance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudScaling).BeginInit();
            grpVitals.SuspendLayout();
            grpStats.SuspendLayout();
            grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudExperience).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).BeginInit();
            SuspendLayout();
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
            toolStrip.Size = new System.Drawing.Size(1244, 29);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripItemNew
            // 
            toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemNew.Image = (System.Drawing.Image)resources.GetObject("toolStripItemNew.Image");
            toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemNew.Name = "toolStripItemNew";
            toolStripItemNew.Size = new System.Drawing.Size(23, 26);
            toolStripItemNew.Text = "New";
            toolStripItemNew.Click += toolStripItemNew_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripItemDelete
            // 
            toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemDelete.Enabled = false;
            toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemDelete.Image = (System.Drawing.Image)resources.GetObject("toolStripItemDelete.Image");
            toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemDelete.Name = "toolStripItemDelete";
            toolStripItemDelete.Size = new System.Drawing.Size(23, 26);
            toolStripItemDelete.Text = "Delete";
            toolStripItemDelete.Click += toolStripItemDelete_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // btnAlphabetical
            // 
            btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            btnAlphabetical.Image = (System.Drawing.Image)resources.GetObject("btnAlphabetical.Image");
            btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAlphabetical.Name = "btnAlphabetical";
            btnAlphabetical.Size = new System.Drawing.Size(23, 26);
            btnAlphabetical.Text = "Order Alphabetically";
            btnAlphabetical.Click += btnAlphabetical_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripItemCopy
            // 
            toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemCopy.Enabled = false;
            toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemCopy.Image = (System.Drawing.Image)resources.GetObject("toolStripItemCopy.Image");
            toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemCopy.Name = "toolStripItemCopy";
            toolStripItemCopy.Size = new System.Drawing.Size(23, 26);
            toolStripItemCopy.Text = "Copy";
            toolStripItemCopy.Click += toolStripItemCopy_Click;
            // 
            // toolStripItemPaste
            // 
            toolStripItemPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemPaste.Enabled = false;
            toolStripItemPaste.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemPaste.Image = (System.Drawing.Image)resources.GetObject("toolStripItemPaste.Image");
            toolStripItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemPaste.Name = "toolStripItemPaste";
            toolStripItemPaste.Size = new System.Drawing.Size(23, 26);
            toolStripItemPaste.Text = "Paste";
            toolStripItemPaste.Click += toolStripItemPaste_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripItemUndo
            // 
            toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripItemUndo.Enabled = false;
            toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripItemUndo.Image = (System.Drawing.Image)resources.GetObject("toolStripItemUndo.Image");
            toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripItemUndo.Name = "toolStripItemUndo";
            toolStripItemUndo.Size = new System.Drawing.Size(23, 26);
            toolStripItemUndo.Text = "Undo";
            toolStripItemUndo.Click += toolStripItemUndo_Click;
            // 
            // grpPets
            // 
            grpPets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            grpPets.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpPets.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpPets.Controls.Add(btnClearSearch);
            grpPets.Controls.Add(txtSearch);
            grpPets.Controls.Add(lstGameObjects);
            grpPets.ForeColor = System.Drawing.Color.Gainsboro;
            grpPets.Location = new System.Drawing.Point(12, 44);
            grpPets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpPets.Name = "grpPets";
            grpPets.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpPets.Size = new System.Drawing.Size(300, 653);
            grpPets.TabIndex = 1;
            grpPets.TabStop = false;
            grpPets.Text = "Pets";
            // 
            // btnClearSearch
            // 
            btnClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            btnClearSearch.Location = new System.Drawing.Point(236, 22);
            btnClearSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Padding = new System.Windows.Forms.Padding(6);
            btnClearSearch.Size = new System.Drawing.Size(60, 27);
            btnClearSearch.TabIndex = 2;
            btnClearSearch.Text = "X";
            btnClearSearch.Click += btnClearSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            txtSearch.Location = new System.Drawing.Point(9, 24);
            txtSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(219, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
            // 
            // lstGameObjects
            // 
            lstGameObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lstGameObjects.Location = new System.Drawing.Point(9, 58);
            lstGameObjects.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lstGameObjects.Name = "lstGameObjects";
            lstGameObjects.Size = new System.Drawing.Size(279, 582);
            lstGameObjects.TabIndex = 0;
            // 
            // pnlContainer
            // 
            pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            pnlContainer.Controls.Add(grpImmunities);
            pnlContainer.Controls.Add(grpSpells);
            pnlContainer.Controls.Add(grpCombat);
            pnlContainer.Controls.Add(grpVitals);
            pnlContainer.Controls.Add(grpStats);
            pnlContainer.Controls.Add(grpGeneral);
            pnlContainer.Location = new System.Drawing.Point(320, 44);
            pnlContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new System.Drawing.Size(912, 592);
            pnlContainer.TabIndex = 2;
            // 
            // grpImmunities
            // 
            grpImmunities.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpImmunities.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpImmunities.Controls.Add(flpImmunities);
            grpImmunities.Controls.Add(lblTenacity);
            grpImmunities.Controls.Add(nudTenacity);
            grpImmunities.ForeColor = System.Drawing.Color.Gainsboro;
            grpImmunities.Location = new System.Drawing.Point(458, 302);
            grpImmunities.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpImmunities.Name = "grpImmunities";
            grpImmunities.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpImmunities.Size = new System.Drawing.Size(440, 179);
            grpImmunities.TabIndex = 5;
            grpImmunities.TabStop = false;
            grpImmunities.Text = "Immunities";
            // 
            // flpImmunities
            // 
            flpImmunities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            flpImmunities.AutoScroll = true;
            flpImmunities.Location = new System.Drawing.Point(10, 22);
            flpImmunities.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flpImmunities.Name = "flpImmunities";
            flpImmunities.Size = new System.Drawing.Size(422, 108);
            flpImmunities.TabIndex = 1;
            // 
            // lblTenacity
            // 
            lblTenacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            lblTenacity.AutoSize = true;
            lblTenacity.Location = new System.Drawing.Point(7, 139);
            lblTenacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblTenacity.Name = "lblTenacity";
            lblTenacity.Size = new System.Drawing.Size(59, 15);
            lblTenacity.TabIndex = 2;
            lblTenacity.Text = "Tenacity";
            // 
            // nudTenacity
            // 
            nudTenacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            nudTenacity.DecimalPlaces = 2;
            nudTenacity.Location = new System.Drawing.Point(10, 157);
            nudTenacity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudTenacity.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudTenacity.Name = "nudTenacity";
            nudTenacity.Size = new System.Drawing.Size(154, 23);
            nudTenacity.TabIndex = 0;
            nudTenacity.ValueChanged += nudTenacity_ValueChanged;
            // 
            // grpSpells
            // 
            grpSpells.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpSpells.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpSpells.Controls.Add(cmbSpell);
            grpSpells.Controls.Add(btnRemoveSpell);
            grpSpells.Controls.Add(btnAddSpell);
            grpSpells.Controls.Add(lstSpells);
            grpSpells.ForeColor = System.Drawing.Color.Gainsboro;
            grpSpells.Location = new System.Drawing.Point(458, 16);
            grpSpells.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpSpells.Name = "grpSpells";
            grpSpells.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpSpells.Size = new System.Drawing.Size(440, 272);
            grpSpells.TabIndex = 4;
            grpSpells.TabStop = false;
            grpSpells.Text = "Spells";
            // 
            // cmbSpell
            // 
            cmbSpell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            cmbSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSpell.FormattingEnabled = true;
            cmbSpell.Location = new System.Drawing.Point(10, 209);
            cmbSpell.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbSpell.Name = "cmbSpell";
            cmbSpell.Size = new System.Drawing.Size(420, 24);
            cmbSpell.TabIndex = 3;
            cmbSpell.SelectedIndexChanged += cmbSpell_SelectedIndexChanged;
            // 
            // btnRemoveSpell
            // 
            btnRemoveSpell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnRemoveSpell.Location = new System.Drawing.Point(340, 237);
            btnRemoveSpell.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnRemoveSpell.Name = "btnRemoveSpell";
            btnRemoveSpell.Padding = new System.Windows.Forms.Padding(6);
            btnRemoveSpell.Size = new System.Drawing.Size(90, 27);
            btnRemoveSpell.TabIndex = 2;
            btnRemoveSpell.Text = "Remove";
            btnRemoveSpell.Click += btnRemoveSpell_Click;
            // 
            // btnAddSpell
            // 
            btnAddSpell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnAddSpell.Location = new System.Drawing.Point(10, 237);
            btnAddSpell.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAddSpell.Name = "btnAddSpell";
            btnAddSpell.Padding = new System.Windows.Forms.Padding(6);
            btnAddSpell.Size = new System.Drawing.Size(90, 27);
            btnAddSpell.TabIndex = 1;
            btnAddSpell.Text = "Add";
            btnAddSpell.Click += btnAddSpell_Click;
            // 
            // lstSpells
            // 
            lstSpells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lstSpells.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lstSpells.FormattingEnabled = true;
            lstSpells.ItemHeight = 15;
            lstSpells.Location = new System.Drawing.Point(10, 22);
            lstSpells.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lstSpells.Name = "lstSpells";
            lstSpells.Size = new System.Drawing.Size(420, 182);
            lstSpells.TabIndex = 0;
            lstSpells.SelectedIndexChanged += lstSpells_SelectedIndexChanged;
            // 
            // grpCombat
            // 
            grpCombat.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpCombat.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpCombat.Controls.Add(nudAttackSpeedValue);
            grpCombat.Controls.Add(lblAttackSpeedValue);
            grpCombat.Controls.Add(cmbAttackSpeedModifier);
            grpCombat.Controls.Add(lblAttackSpeedModifier);
            grpCombat.Controls.Add(lblCritMultiplier);
            grpCombat.Controls.Add(nudCritMultiplier);
            grpCombat.Controls.Add(lblCritChance);
            grpCombat.Controls.Add(nudCritChance);
            grpCombat.Controls.Add(lblDamage);
            grpCombat.Controls.Add(nudDamage);
            grpCombat.Controls.Add(lblScaling);
            grpCombat.Controls.Add(nudScaling);
            grpCombat.Controls.Add(lblScalingStat);
            grpCombat.Controls.Add(cmbScalingStat);
            grpCombat.Controls.Add(lblDamageType);
            grpCombat.Controls.Add(cmbDamageType);
            grpCombat.Controls.Add(lblAttackAnimation);
            grpCombat.Controls.Add(cmbAttackAnimation);
            grpCombat.Controls.Add(lblDeathAnimation);
            grpCombat.Controls.Add(cmbDeathAnimation);
            grpCombat.Controls.Add(lblIdleAnimation);
            grpCombat.Controls.Add(cmbIdleAnimation);
            grpCombat.ForeColor = System.Drawing.Color.Gainsboro;
            grpCombat.Location = new System.Drawing.Point(10, 330);
            grpCombat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpCombat.Name = "grpCombat";
            grpCombat.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpCombat.Size = new System.Drawing.Size(440, 260);
            grpCombat.TabIndex = 3;
            grpCombat.TabStop = false;
            grpCombat.Text = "Combat";
            // 
            // nudAttackSpeedValue
            // 
            nudAttackSpeedValue.Location = new System.Drawing.Point(221, 234);
            nudAttackSpeedValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudAttackSpeedValue.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudAttackSpeedValue.Name = "nudAttackSpeedValue";
            nudAttackSpeedValue.Size = new System.Drawing.Size(203, 23);
            nudAttackSpeedValue.TabIndex = 21;
            nudAttackSpeedValue.ValueChanged += nudAttackSpeedValue_ValueChanged;
            // 
            // lblAttackSpeedValue
            // 
            lblAttackSpeedValue.AutoSize = true;
            lblAttackSpeedValue.Location = new System.Drawing.Point(218, 216);
            lblAttackSpeedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblAttackSpeedValue.Name = "lblAttackSpeedValue";
            lblAttackSpeedValue.Size = new System.Drawing.Size(36, 15);
            lblAttackSpeedValue.TabIndex = 20;
            lblAttackSpeedValue.Text = "Value";
            // 
            // cmbAttackSpeedModifier
            // 
            cmbAttackSpeedModifier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbAttackSpeedModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbAttackSpeedModifier.FormattingEnabled = true;
            cmbAttackSpeedModifier.Location = new System.Drawing.Point(10, 234);
            cmbAttackSpeedModifier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbAttackSpeedModifier.Name = "cmbAttackSpeedModifier";
            cmbAttackSpeedModifier.Size = new System.Drawing.Size(203, 24);
            cmbAttackSpeedModifier.TabIndex = 19;
            cmbAttackSpeedModifier.SelectedIndexChanged += cmbAttackSpeedModifier_SelectedIndexChanged;
            // 
            // lblAttackSpeedModifier
            // 
            lblAttackSpeedModifier.AutoSize = true;
            lblAttackSpeedModifier.Location = new System.Drawing.Point(7, 216);
            lblAttackSpeedModifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblAttackSpeedModifier.Name = "lblAttackSpeedModifier";
            lblAttackSpeedModifier.Size = new System.Drawing.Size(57, 15);
            lblAttackSpeedModifier.TabIndex = 18;
            lblAttackSpeedModifier.Text = "Modifier";
            // 
            // lblCritMultiplier
            // 
            lblCritMultiplier.AutoSize = true;
            lblCritMultiplier.Location = new System.Drawing.Point(218, 165);
            lblCritMultiplier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCritMultiplier.Name = "lblCritMultiplier";
            lblCritMultiplier.Size = new System.Drawing.Size(83, 15);
            lblCritMultiplier.TabIndex = 17;
            lblCritMultiplier.Text = "Crit Multiplier";
            // 
            // nudCritMultiplier
            // 
            nudCritMultiplier.DecimalPlaces = 2;
            nudCritMultiplier.Location = new System.Drawing.Point(221, 183);
            nudCritMultiplier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudCritMultiplier.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudCritMultiplier.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            nudCritMultiplier.Name = "nudCritMultiplier";
            nudCritMultiplier.Size = new System.Drawing.Size(203, 23);
            nudCritMultiplier.TabIndex = 16;
            nudCritMultiplier.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            nudCritMultiplier.ValueChanged += nudCritMultiplier_ValueChanged;
            // 
            // lblCritChance
            // 
            lblCritChance.AutoSize = true;
            lblCritChance.Location = new System.Drawing.Point(7, 165);
            lblCritChance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCritChance.Name = "lblCritChance";
            lblCritChance.Size = new System.Drawing.Size(68, 15);
            lblCritChance.TabIndex = 15;
            lblCritChance.Text = "Crit Chance";
            // 
            // nudCritChance
            // 
            nudCritChance.Location = new System.Drawing.Point(10, 183);
            nudCritChance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudCritChance.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudCritChance.Name = "nudCritChance";
            nudCritChance.Size = new System.Drawing.Size(203, 23);
            nudCritChance.TabIndex = 14;
            nudCritChance.ValueChanged += nudCritChance_ValueChanged;
            // 
            // lblDamage
            // 
            lblDamage.AutoSize = true;
            lblDamage.Location = new System.Drawing.Point(218, 118);
            lblDamage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDamage.Name = "lblDamage";
            lblDamage.Size = new System.Drawing.Size(51, 15);
            lblDamage.TabIndex = 13;
            lblDamage.Text = "Damage";
            // 
            // nudDamage
            // 
            nudDamage.Location = new System.Drawing.Point(221, 136);
            nudDamage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudDamage.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new System.Drawing.Size(203, 23);
            nudDamage.TabIndex = 12;
            nudDamage.ValueChanged += nudDamage_ValueChanged;
            // 
            // lblScaling
            // 
            lblScaling.AutoSize = true;
            lblScaling.Location = new System.Drawing.Point(7, 118);
            lblScaling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblScaling.Name = "lblScaling";
            lblScaling.Size = new System.Drawing.Size(45, 15);
            lblScaling.TabIndex = 11;
            lblScaling.Text = "Scaling";
            // 
            // nudScaling
            // 
            nudScaling.Location = new System.Drawing.Point(10, 136);
            nudScaling.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudScaling.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudScaling.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudScaling.Name = "nudScaling";
            nudScaling.Size = new System.Drawing.Size(203, 23);
            nudScaling.TabIndex = 10;
            nudScaling.Value = new decimal(new int[] { 100, 0, 0, 0 });
            nudScaling.ValueChanged += nudScaling_ValueChanged;
            // 
            // lblScalingStat
            // 
            lblScalingStat.AutoSize = true;
            lblScalingStat.Location = new System.Drawing.Point(218, 70);
            lblScalingStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblScalingStat.Name = "lblScalingStat";
            lblScalingStat.Size = new System.Drawing.Size(70, 15);
            lblScalingStat.TabIndex = 9;
            lblScalingStat.Text = "Scaling Stat";
            // 
            // cmbScalingStat
            // 
            cmbScalingStat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbScalingStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbScalingStat.FormattingEnabled = true;
            cmbScalingStat.Location = new System.Drawing.Point(221, 88);
            cmbScalingStat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbScalingStat.Name = "cmbScalingStat";
            cmbScalingStat.Size = new System.Drawing.Size(203, 24);
            cmbScalingStat.TabIndex = 8;
            cmbScalingStat.SelectedIndexChanged += cmbScalingStat_SelectedIndexChanged;
            // 
            // lblDamageType
            // 
            lblDamageType.AutoSize = true;
            lblDamageType.Location = new System.Drawing.Point(7, 70);
            lblDamageType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDamageType.Name = "lblDamageType";
            lblDamageType.Size = new System.Drawing.Size(74, 15);
            lblDamageType.TabIndex = 7;
            lblDamageType.Text = "Damage Type";
            // 
            // cmbDamageType
            // 
            cmbDamageType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbDamageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDamageType.FormattingEnabled = true;
            cmbDamageType.Location = new System.Drawing.Point(10, 88);
            cmbDamageType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbDamageType.Name = "cmbDamageType";
            cmbDamageType.Size = new System.Drawing.Size(203, 24);
            cmbDamageType.TabIndex = 6;
            cmbDamageType.SelectedIndexChanged += cmbDamageType_SelectedIndexChanged;
            // 
            // lblAttackAnimation
            // 
            lblAttackAnimation.AutoSize = true;
            lblAttackAnimation.Location = new System.Drawing.Point(7, 22);
            lblAttackAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblAttackAnimation.Name = "lblAttackAnimation";
            lblAttackAnimation.Size = new System.Drawing.Size(99, 15);
            lblAttackAnimation.TabIndex = 0;
            lblAttackAnimation.Text = "Attack Animation";
            // 
            // cmbAttackAnimation
            // 
            cmbAttackAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbAttackAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbAttackAnimation.FormattingEnabled = true;
            cmbAttackAnimation.Location = new System.Drawing.Point(10, 40);
            cmbAttackAnimation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbAttackAnimation.Name = "cmbAttackAnimation";
            cmbAttackAnimation.Size = new System.Drawing.Size(203, 24);
            cmbAttackAnimation.TabIndex = 1;
            cmbAttackAnimation.SelectedIndexChanged += cmbAttackAnimation_SelectedIndexChanged;
            // 
            // lblDeathAnimation
            // 
            lblDeathAnimation.AutoSize = true;
            lblDeathAnimation.Location = new System.Drawing.Point(218, 22);
            lblDeathAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDeathAnimation.Name = "lblDeathAnimation";
            lblDeathAnimation.Size = new System.Drawing.Size(95, 15);
            lblDeathAnimation.TabIndex = 2;
            lblDeathAnimation.Text = "Death Animation";
            // 
            // cmbDeathAnimation
            // 
            cmbDeathAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbDeathAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDeathAnimation.FormattingEnabled = true;
            cmbDeathAnimation.Location = new System.Drawing.Point(221, 40);
            cmbDeathAnimation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbDeathAnimation.Name = "cmbDeathAnimation";
            cmbDeathAnimation.Size = new System.Drawing.Size(203, 24);
            cmbDeathAnimation.TabIndex = 3;
            cmbDeathAnimation.SelectedIndexChanged += cmbDeathAnimation_SelectedIndexChanged;
            // 
            // lblIdleAnimation
            // 
            lblIdleAnimation.AutoSize = true;
            lblIdleAnimation.Location = new System.Drawing.Point(7, 118);
            lblIdleAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblIdleAnimation.Name = "lblIdleAnimation";
            lblIdleAnimation.Size = new System.Drawing.Size(81, 15);
            lblIdleAnimation.TabIndex = 4;
            lblIdleAnimation.Text = "Idle Animation";
            // 
            // cmbIdleAnimation
            // 
            cmbIdleAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbIdleAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbIdleAnimation.FormattingEnabled = true;
            cmbIdleAnimation.Location = new System.Drawing.Point(10, 136);
            cmbIdleAnimation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbIdleAnimation.Name = "cmbIdleAnimation";
            cmbIdleAnimation.Size = new System.Drawing.Size(203, 24);
            cmbIdleAnimation.TabIndex = 5;
            cmbIdleAnimation.SelectedIndexChanged += cmbIdleAnimation_SelectedIndexChanged;
            // 
            // grpVitals
            // 
            grpVitals.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpVitals.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpVitals.Controls.Add(flpVitals);
            grpVitals.Controls.Add(flpVitalRegen);
            grpVitals.Controls.Add(lblVitals);
            grpVitals.Controls.Add(lblVitalRegen);
            grpVitals.ForeColor = System.Drawing.Color.Gainsboro;
            grpVitals.Location = new System.Drawing.Point(10, 214);
            grpVitals.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpVitals.Name = "grpVitals";
            grpVitals.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpVitals.Size = new System.Drawing.Size(440, 148);
            grpVitals.TabIndex = 2;
            grpVitals.TabStop = false;
            grpVitals.Text = "Vitals";
            // 
            // flpVitals
            // 
            flpVitals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            flpVitals.AutoScroll = true;
            flpVitals.Location = new System.Drawing.Point(10, 40);
            flpVitals.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flpVitals.Name = "flpVitals";
            flpVitals.Size = new System.Drawing.Size(420, 46);
            flpVitals.TabIndex = 0;
            // 
            // flpVitalRegen
            // 
            flpVitalRegen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            flpVitalRegen.AutoScroll = true;
            flpVitalRegen.Location = new System.Drawing.Point(10, 106);
            flpVitalRegen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flpVitalRegen.Name = "flpVitalRegen";
            flpVitalRegen.Size = new System.Drawing.Size(420, 33);
            flpVitalRegen.TabIndex = 1;
            // 
            // lblVitals
            // 
            lblVitals.AutoSize = true;
            lblVitals.Location = new System.Drawing.Point(7, 22);
            lblVitals.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblVitals.Name = "lblVitals";
            lblVitals.Size = new System.Drawing.Size(36, 15);
            lblVitals.TabIndex = 2;
            lblVitals.Text = "Max";
            // 
            // lblVitalRegen
            // 
            lblVitalRegen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            lblVitalRegen.AutoSize = true;
            lblVitalRegen.Location = new System.Drawing.Point(7, 88);
            lblVitalRegen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblVitalRegen.Name = "lblVitalRegen";
            lblVitalRegen.Size = new System.Drawing.Size(66, 15);
            lblVitalRegen.TabIndex = 3;
            lblVitalRegen.Text = "Regeneration";
            // 
            // grpStats
            // 
            grpStats.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpStats.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpStats.Controls.Add(flpStats);
            grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            grpStats.Location = new System.Drawing.Point(10, 148);
            grpStats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpStats.Name = "grpStats";
            grpStats.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpStats.Size = new System.Drawing.Size(440, 60);
            grpStats.TabIndex = 1;
            grpStats.TabStop = false;
            grpStats.Text = "Stats";
            // 
            // flpStats
            // 
            flpStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            flpStats.AutoScroll = true;
            flpStats.Location = new System.Drawing.Point(10, 22);
            flpStats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flpStats.Name = "flpStats";
            flpStats.Size = new System.Drawing.Size(420, 28);
            flpStats.TabIndex = 0;
            // 
            // grpGeneral
            // 
            grpGeneral.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpGeneral.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpGeneral.Controls.Add(lblExperience);
            grpGeneral.Controls.Add(nudExperience);
            grpGeneral.Controls.Add(lblLevel);
            grpGeneral.Controls.Add(nudLevel);
            grpGeneral.Controls.Add(lblIdleSprite);
            grpGeneral.Controls.Add(cmbSprite);
            grpGeneral.Controls.Add(btnAddFolder);
            grpGeneral.Controls.Add(lblFolder);
            grpGeneral.Controls.Add(cmbFolder);
            grpGeneral.Controls.Add(lblName);
            grpGeneral.Controls.Add(txtName);
            grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            grpGeneral.Location = new System.Drawing.Point(10, 16);
            grpGeneral.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpGeneral.Name = "grpGeneral";
            grpGeneral.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpGeneral.Size = new System.Drawing.Size(440, 126);
            grpGeneral.TabIndex = 0;
            grpGeneral.TabStop = false;
            grpGeneral.Text = "General";
            // 
            // lblExperience
            // 
            lblExperience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblExperience.AutoSize = true;
            lblExperience.Location = new System.Drawing.Point(216, 79);
            lblExperience.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblExperience.Name = "lblExperience";
            lblExperience.Size = new System.Drawing.Size(63, 15);
            lblExperience.TabIndex = 10;
            lblExperience.Text = "Experience";
            // 
            // nudExperience
            // 
            nudExperience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            nudExperience.Location = new System.Drawing.Point(219, 97);
            nudExperience.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudExperience.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudExperience.Name = "nudExperience";
            nudExperience.Size = new System.Drawing.Size(203, 23);
            nudExperience.TabIndex = 9;
            nudExperience.ValueChanged += nudExperience_ValueChanged;
            // 
            // lblLevel
            // 
            lblLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblLevel.AutoSize = true;
            lblLevel.Location = new System.Drawing.Point(216, 31);
            lblLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new System.Drawing.Size(36, 15);
            lblLevel.TabIndex = 6;
            lblLevel.Text = "Level";
            // 
            // nudLevel
            // 
            nudLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            nudLevel.Location = new System.Drawing.Point(219, 49);
            nudLevel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudLevel.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLevel.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new System.Drawing.Size(203, 23);
            nudLevel.TabIndex = 5;
            nudLevel.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudLevel.ValueChanged += nudLevel_ValueChanged;
            // 
            // lblIdleSprite
            // 
            lblIdleSprite.AutoSize = true;
            lblIdleSprite.Location = new System.Drawing.Point(7, 79);
            lblIdleSprite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblIdleSprite.Name = "lblIdleSprite";
            lblIdleSprite.Size = new System.Drawing.Size(38, 15);
            lblIdleSprite.TabIndex = 4;
            lblIdleSprite.Text = "Sprite";
            // 
            // cmbSprite
            // 
            cmbSprite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSprite.FormattingEnabled = true;
            cmbSprite.Location = new System.Drawing.Point(10, 97);
            cmbSprite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbSprite.Name = "cmbSprite";
            cmbSprite.Size = new System.Drawing.Size(203, 24);
            cmbSprite.TabIndex = 3;
            cmbSprite.SelectedIndexChanged += cmbSprite_SelectedIndexChanged;
            // 
            // btnAddFolder
            // 
            btnAddFolder.Location = new System.Drawing.Point(373, 23);
            btnAddFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.Padding = new System.Windows.Forms.Padding(6);
            btnAddFolder.Size = new System.Drawing.Size(49, 27);
            btnAddFolder.TabIndex = 2;
            btnAddFolder.Text = "+";
            btnAddFolder.Click += btnAddFolder_Click;
            // 
            // lblFolder
            // 
            lblFolder.AutoSize = true;
            lblFolder.Location = new System.Drawing.Point(216, 9);
            lblFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new System.Drawing.Size(41, 15);
            lblFolder.TabIndex = 7;
            lblFolder.Text = "Folder";
            // 
            // cmbFolder
            // 
            cmbFolder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbFolder.FormattingEnabled = true;
            cmbFolder.Location = new System.Drawing.Point(219, 27);
            cmbFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbFolder.Name = "cmbFolder";
            cmbFolder.Size = new System.Drawing.Size(146, 24);
            cmbFolder.TabIndex = 1;
            cmbFolder.SelectedIndexChanged += cmbFolder_SelectedIndexChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(7, 9);
            lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(39, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(10, 27);
            txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(203, 23);
            txtName.TabIndex = 0;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnSave.Location = new System.Drawing.Point(1062, 642);
            btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new System.Windows.Forms.Padding(6);
            btnSave.Size = new System.Drawing.Size(88, 27);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCancel.Location = new System.Drawing.Point(964, 642);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(6);
            btnCancel.Size = new System.Drawing.Size(88, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmPet
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1244, 709);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(pnlContainer);
            Controls.Add(grpPets);
            Controls.Add(toolStrip);
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FrmPet";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Pet Editor";
            Load += frmPet_Load;
            FormClosed += frmPet_FormClosed;
            KeyDown += form_KeyDown;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            grpPets.ResumeLayout(false);
            grpPets.PerformLayout();
            pnlContainer.ResumeLayout(false);
            grpImmunities.ResumeLayout(false);
            grpImmunities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTenacity).EndInit();
            grpSpells.ResumeLayout(false);
            grpCombat.ResumeLayout(false);
            grpCombat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAttackSpeedValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCritMultiplier).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCritChance).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudScaling).EndInit();
            grpVitals.ResumeLayout(false);
            grpVitals.PerformLayout();
            grpStats.ResumeLayout(false);
            grpGeneral.ResumeLayout(false);
            grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudExperience).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).EndInit();
            ResumeLayout(false);
        }

        #endregion

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
        private DarkGroupBox grpPets;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private Intersect.Editor.Forms.Controls.GameObjectList lstGameObjects;
        private System.Windows.Forms.Panel pnlContainer;
        private DarkGroupBox grpGeneral;
        private DarkGroupBox grpStats;
        private System.Windows.Forms.FlowLayoutPanel flpStats;
        private DarkGroupBox grpVitals;
        private System.Windows.Forms.FlowLayoutPanel flpVitals;
        private System.Windows.Forms.FlowLayoutPanel flpVitalRegen;
        private System.Windows.Forms.Label lblVitals;
        private System.Windows.Forms.Label lblVitalRegen;
        private DarkGroupBox grpCombat;
        private System.Windows.Forms.Label lblAttackAnimation;
        private DarkComboBox cmbAttackAnimation;
        private System.Windows.Forms.Label lblDeathAnimation;
        private DarkComboBox cmbDeathAnimation;
        private System.Windows.Forms.Label lblIdleAnimation;
        private DarkComboBox cmbIdleAnimation;
        private System.Windows.Forms.Label lblDamageType;
        private DarkComboBox cmbDamageType;
        private System.Windows.Forms.Label lblScalingStat;
        private DarkComboBox cmbScalingStat;
        private System.Windows.Forms.Label lblScaling;
        private DarkNumericUpDown nudScaling;
        private System.Windows.Forms.Label lblDamage;
        private DarkNumericUpDown nudDamage;
        private System.Windows.Forms.Label lblCritChance;
        private DarkNumericUpDown nudCritChance;
        private System.Windows.Forms.Label lblCritMultiplier;
        private DarkNumericUpDown nudCritMultiplier;
        private System.Windows.Forms.Label lblAttackSpeedModifier;
        private DarkComboBox cmbAttackSpeedModifier;
        private System.Windows.Forms.Label lblAttackSpeedValue;
        private DarkNumericUpDown nudAttackSpeedValue;
        private DarkGroupBox grpSpells;
        private DarkComboBox cmbSpell;
        private DarkButton btnRemoveSpell;
        private DarkButton btnAddSpell;
        private System.Windows.Forms.ListBox lstSpells;
        private DarkGroupBox grpImmunities;
        private System.Windows.Forms.FlowLayoutPanel flpImmunities;
        private System.Windows.Forms.Label lblTenacity;
        private DarkNumericUpDown nudTenacity;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private System.Windows.Forms.Label lblName;
        private DarkTextBox txtName;
        private System.Windows.Forms.Label lblFolder;
        private DarkComboBox cmbFolder;
        private DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblIdleSprite;
        private DarkComboBox cmbSprite;
        private System.Windows.Forms.Label lblLevel;
        private DarkNumericUpDown nudLevel;
        private System.Windows.Forms.Label lblExperience;
        private DarkNumericUpDown nudExperience;
    }
}
