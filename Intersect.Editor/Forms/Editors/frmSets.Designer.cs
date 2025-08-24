

using Intersect.Framework.Core.GameObjects.Maps;

namespace Intersect.Editor.Forms.Editors;

partial class frmSets
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSets));
        grpSets = new DarkUI.Controls.DarkGroupBox();
        btnClearSearch = new DarkUI.Controls.DarkButton();
        txtSearch = new DarkUI.Controls.DarkTextBox();
        lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
        grpStats = new DarkUI.Controls.DarkGroupBox();
        lblPercentage8 = new Label();
        lblPercentage7 = new Label();
        lblPercentage6 = new Label();
        lblPercentage5 = new Label();
        lblPercentage4 = new Label();
        lblPercentage3 = new Label();
        lblPercentage2 = new Label();
        lblPercentage1 = new Label();
        nudSpdPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudCurPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudDefPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudDmgPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudStrPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudIntPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudVitPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudAgiPercentage = new DarkUI.Controls.DarkNumericUpDown();
        lblPlus8 = new Label();
        lblPlus7 = new Label();
        lblPlus6 = new Label();
        lblPlus5 = new Label();
        lblPlus4 = new Label();
        lblPlus3 = new Label();
        lblPlus2 = new Label();
        lblPlus1 = new Label();
        nudSpd = new DarkUI.Controls.DarkNumericUpDown();
        nudCur = new DarkUI.Controls.DarkNumericUpDown();
        nudDef = new DarkUI.Controls.DarkNumericUpDown();
        nudDmg = new DarkUI.Controls.DarkNumericUpDown();
        nudStr = new DarkUI.Controls.DarkNumericUpDown();
        nudInt = new DarkUI.Controls.DarkNumericUpDown();
        nudVit = new DarkUI.Controls.DarkNumericUpDown();
        nudAgi = new DarkUI.Controls.DarkNumericUpDown();
        lblSpd = new Label();
        lblMR = new Label();
        lblDef = new Label();
        lblMag = new Label();
        lblStr = new Label();
        lblARP = new Label();
        lblVit = new Label();
        lblWis = new Label();
        grpItemsSets = new DarkUI.Controls.DarkGroupBox();
        cmbItems = new DarkUI.Controls.DarkComboBox();
        btnRemove = new DarkUI.Controls.DarkButton();
        btnAdd = new DarkUI.Controls.DarkButton();
        lblItemSet = new Label();
        lstItems = new ListBox();
        grpGeneral = new DarkUI.Controls.DarkGroupBox();
        btnAddFolder = new DarkUI.Controls.DarkButton();
        lblFolder = new Label();
        cmbFolder = new DarkUI.Controls.DarkComboBox();
        lblName = new Label();
        txtName = new DarkUI.Controls.DarkTextBox();
        grpVitalBonuses = new DarkUI.Controls.DarkGroupBox();
        label1 = new Label();
        label2 = new Label();
        nudMPPercentage = new DarkUI.Controls.DarkNumericUpDown();
        nudHPPercentage = new DarkUI.Controls.DarkNumericUpDown();
        label3 = new Label();
        label4 = new Label();
        nudManaBonus = new DarkUI.Controls.DarkNumericUpDown();
        nudHealthBonus = new DarkUI.Controls.DarkNumericUpDown();
        lblManaBonus = new Label();
        lblHealthBonus = new Label();
        btnCancel = new DarkUI.Controls.DarkButton();
        btnSave = new DarkUI.Controls.DarkButton();
        grpEffects = new DarkUI.Controls.DarkGroupBox();
        lstBonusEffects = new ListBox();
        lblEffectPercent = new Label();
        nudEffectPercent = new DarkUI.Controls.DarkNumericUpDown();
        grpRegen = new DarkUI.Controls.DarkGroupBox();
        nudMpRegen = new DarkUI.Controls.DarkNumericUpDown();
        nudHPRegen = new DarkUI.Controls.DarkNumericUpDown();
        lblHpRegen = new Label();
        lblManaRegen = new Label();
        lblRegenHint = new Label();
        toolStrip = new DarkUI.Controls.DarkToolStrip();
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
        grpSets.SuspendLayout();
        grpStats.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSpdPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudCurPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDefPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDmgPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudStrPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudIntPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudVitPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAgiPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSpd).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudCur).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDef).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDmg).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudStr).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudInt).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudVit).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAgi).BeginInit();
        grpItemsSets.SuspendLayout();
        grpGeneral.SuspendLayout();
        grpVitalBonuses.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudMPPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudHPPercentage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudManaBonus).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudHealthBonus).BeginInit();
        grpEffects.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudEffectPercent).BeginInit();
        grpRegen.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudMpRegen).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudHPRegen).BeginInit();
        toolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // grpSets
        // 
        grpSets.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpSets.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpSets.Controls.Add(btnClearSearch);
        grpSets.Controls.Add(txtSearch);
        grpSets.Controls.Add(lstGameObjects);
        grpSets.ForeColor = System.Drawing.Color.Gainsboro;
        grpSets.Location = new System.Drawing.Point(0, 32);
        grpSets.Margin = new Padding(4, 3, 4, 3);
        grpSets.Name = "grpSets";
        grpSets.Padding = new Padding(4, 3, 4, 3);
        grpSets.Size = new Size(237, 435);
        grpSets.TabIndex = 45;
        grpSets.TabStop = false;
        grpSets.Text = "Sets";
        // 
        // btnClearSearch
        // 
        btnClearSearch.Location = new System.Drawing.Point(209, 15);
        btnClearSearch.Margin = new Padding(4, 3, 4, 3);
        btnClearSearch.Name = "btnClearSearch";
        btnClearSearch.Padding = new Padding(6);
        btnClearSearch.Size = new Size(21, 23);
        btnClearSearch.TabIndex = 28;
        btnClearSearch.Text = "X";
        btnClearSearch.Click += btnClearSearch_Click;
        // 
        // txtSearch
        // 
        txtSearch.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
        txtSearch.Location = new System.Drawing.Point(7, 15);
        txtSearch.Margin = new Padding(4, 3, 4, 3);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(194, 23);
        txtSearch.TabIndex = 27;
        txtSearch.Text = "Search...";
        txtSearch.Click += txtSearch_Click;
        txtSearch.TextChanged += txtSearch_TextChanged;
        txtSearch.Enter += txtSearch_Enter;
        txtSearch.Leave += txtSearch_Leave;
        // 
        // lstGameObjects
        // 
        lstGameObjects.AllowDrop = true;
        lstGameObjects.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
        lstGameObjects.BorderStyle = BorderStyle.None;
        lstGameObjects.ForeColor = System.Drawing.Color.Gainsboro;
        lstGameObjects.HideSelection = false;
        lstGameObjects.ImageIndex = 0;
        lstGameObjects.LineColor = System.Drawing.Color.FromArgb(150, 150, 150);
        lstGameObjects.Location = new System.Drawing.Point(7, 45);
        lstGameObjects.Margin = new Padding(4, 3, 4, 3);
        lstGameObjects.Name = "lstGameObjects";
        lstGameObjects.SelectedImageIndex = 0;
        lstGameObjects.Size = new Size(223, 377);
        lstGameObjects.TabIndex = 26;
        // 
        // grpStats
        // 
        grpStats.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpStats.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpStats.Controls.Add(lblPercentage8);
        grpStats.Controls.Add(lblPercentage7);
        grpStats.Controls.Add(lblPercentage6);
        grpStats.Controls.Add(lblPercentage5);
        grpStats.Controls.Add(lblPercentage4);
        grpStats.Controls.Add(lblPercentage3);
        grpStats.Controls.Add(lblPercentage2);
        grpStats.Controls.Add(lblPercentage1);
        grpStats.Controls.Add(nudSpdPercentage);
        grpStats.Controls.Add(nudCurPercentage);
        grpStats.Controls.Add(nudDefPercentage);
        grpStats.Controls.Add(nudDmgPercentage);
        grpStats.Controls.Add(nudStrPercentage);
        grpStats.Controls.Add(nudIntPercentage);
        grpStats.Controls.Add(nudVitPercentage);
        grpStats.Controls.Add(nudAgiPercentage);
        grpStats.Controls.Add(lblPlus8);
        grpStats.Controls.Add(lblPlus7);
        grpStats.Controls.Add(lblPlus6);
        grpStats.Controls.Add(lblPlus5);
        grpStats.Controls.Add(lblPlus4);
        grpStats.Controls.Add(lblPlus3);
        grpStats.Controls.Add(lblPlus2);
        grpStats.Controls.Add(lblPlus1);
        grpStats.Controls.Add(nudSpd);
        grpStats.Controls.Add(nudCur);
        grpStats.Controls.Add(nudDef);
        grpStats.Controls.Add(nudDmg);
        grpStats.Controls.Add(nudStr);
        grpStats.Controls.Add(nudInt);
        grpStats.Controls.Add(nudVit);
        grpStats.Controls.Add(nudAgi);
        grpStats.Controls.Add(lblSpd);
        grpStats.Controls.Add(lblMR);
        grpStats.Controls.Add(lblDef);
        grpStats.Controls.Add(lblMag);
        grpStats.Controls.Add(lblStr);
        grpStats.Controls.Add(lblARP);
        grpStats.Controls.Add(lblVit);
        grpStats.Controls.Add(lblWis);
        grpStats.ForeColor = System.Drawing.Color.Gainsboro;
        grpStats.Location = new System.Drawing.Point(504, 32);
        grpStats.Margin = new Padding(4, 3, 4, 3);
        grpStats.Name = "grpStats";
        grpStats.Padding = new Padding(4, 3, 4, 3);
        grpStats.Size = new Size(276, 281);
        grpStats.TabIndex = 51;
        grpStats.TabStop = false;
        grpStats.Text = "Stat Modifiers";
        // 
        // lblPercentage8
        // 
        lblPercentage8.AutoSize = true;
        lblPercentage8.Location = new System.Drawing.Point(248, 255);
        lblPercentage8.Margin = new Padding(2, 0, 2, 0);
        lblPercentage8.Name = "lblPercentage8";
        lblPercentage8.Size = new Size(17, 15);
        lblPercentage8.TabIndex = 68;
        lblPercentage8.Text = "%";
        // 
        // lblPercentage7
        // 
        lblPercentage7.AutoSize = true;
        lblPercentage7.Location = new System.Drawing.Point(248, 224);
        lblPercentage7.Margin = new Padding(2, 0, 2, 0);
        lblPercentage7.Name = "lblPercentage7";
        lblPercentage7.Size = new Size(17, 15);
        lblPercentage7.TabIndex = 69;
        lblPercentage7.Text = "%";
        // 
        // lblPercentage6
        // 
        lblPercentage6.AutoSize = true;
        lblPercentage6.Location = new System.Drawing.Point(248, 189);
        lblPercentage6.Margin = new Padding(2, 0, 2, 0);
        lblPercentage6.Name = "lblPercentage6";
        lblPercentage6.Size = new Size(17, 15);
        lblPercentage6.TabIndex = 70;
        lblPercentage6.Text = "%";
        // 
        // lblPercentage5
        // 
        lblPercentage5.AutoSize = true;
        lblPercentage5.Location = new System.Drawing.Point(248, 154);
        lblPercentage5.Margin = new Padding(2, 0, 2, 0);
        lblPercentage5.Name = "lblPercentage5";
        lblPercentage5.Size = new Size(17, 15);
        lblPercentage5.TabIndex = 67;
        lblPercentage5.Text = "%";
        // 
        // lblPercentage4
        // 
        lblPercentage4.AutoSize = true;
        lblPercentage4.Location = new System.Drawing.Point(248, 120);
        lblPercentage4.Margin = new Padding(2, 0, 2, 0);
        lblPercentage4.Name = "lblPercentage4";
        lblPercentage4.Size = new Size(17, 15);
        lblPercentage4.TabIndex = 66;
        lblPercentage4.Text = "%";
        // 
        // lblPercentage3
        // 
        lblPercentage3.AutoSize = true;
        lblPercentage3.Location = new System.Drawing.Point(248, 88);
        lblPercentage3.Margin = new Padding(2, 0, 2, 0);
        lblPercentage3.Name = "lblPercentage3";
        lblPercentage3.Size = new Size(17, 15);
        lblPercentage3.TabIndex = 65;
        lblPercentage3.Text = "%";
        // 
        // lblPercentage2
        // 
        lblPercentage2.AutoSize = true;
        lblPercentage2.Location = new System.Drawing.Point(248, 55);
        lblPercentage2.Margin = new Padding(2, 0, 2, 0);
        lblPercentage2.Name = "lblPercentage2";
        lblPercentage2.Size = new Size(17, 15);
        lblPercentage2.TabIndex = 64;
        lblPercentage2.Text = "%";
        // 
        // lblPercentage1
        // 
        lblPercentage1.AutoSize = true;
        lblPercentage1.Location = new System.Drawing.Point(248, 24);
        lblPercentage1.Margin = new Padding(2, 0, 2, 0);
        lblPercentage1.Name = "lblPercentage1";
        lblPercentage1.Size = new Size(17, 15);
        lblPercentage1.TabIndex = 63;
        lblPercentage1.Text = "%";
        // 
        // nudSpdPercentage
        // 
        nudSpdPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudSpdPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudSpdPercentage.Location = new System.Drawing.Point(192, 152);
        nudSpdPercentage.Margin = new Padding(4, 3, 4, 3);
        nudSpdPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudSpdPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudSpdPercentage.Name = "nudSpdPercentage";
        nudSpdPercentage.Size = new Size(50, 23);
        nudSpdPercentage.TabIndex = 62;
        nudSpdPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudSpdPercentage.ValueChanged += nudSpdPercentage_ValueChanged;
        // 
        // nudCurPercentage
        // 
        nudCurPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudCurPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudCurPercentage.Location = new System.Drawing.Point(192, 118);
        nudCurPercentage.Margin = new Padding(4, 3, 4, 3);
        nudCurPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudCurPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudCurPercentage.Name = "nudCurPercentage";
        nudCurPercentage.Size = new Size(50, 23);
        nudCurPercentage.TabIndex = 61;
        nudCurPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudCurPercentage.ValueChanged += nudMRPercentage_ValueChanged;
        // 
        // nudDefPercentage
        // 
        nudDefPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudDefPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudDefPercentage.Location = new System.Drawing.Point(192, 87);
        nudDefPercentage.Margin = new Padding(4, 3, 4, 3);
        nudDefPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudDefPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudDefPercentage.Name = "nudDefPercentage";
        nudDefPercentage.Size = new Size(50, 23);
        nudDefPercentage.TabIndex = 60;
        nudDefPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudDefPercentage.ValueChanged += nudDefPercentage_ValueChanged;
        // 
        // nudDmgPercentage
        // 
        nudDmgPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudDmgPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudDmgPercentage.Location = new System.Drawing.Point(192, 54);
        nudDmgPercentage.Margin = new Padding(4, 3, 4, 3);
        nudDmgPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudDmgPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudDmgPercentage.Name = "nudDmgPercentage";
        nudDmgPercentage.Size = new Size(50, 23);
        nudDmgPercentage.TabIndex = 59;
        nudDmgPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudDmgPercentage.ValueChanged += nudMagPercentage_ValueChanged;
        // 
        // nudStrPercentage
        // 
        nudStrPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudStrPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudStrPercentage.Location = new System.Drawing.Point(191, 23);
        nudStrPercentage.Margin = new Padding(4, 3, 4, 3);
        nudStrPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudStrPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudStrPercentage.Name = "nudStrPercentage";
        nudStrPercentage.Size = new Size(50, 23);
        nudStrPercentage.TabIndex = 58;
        nudStrPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudStrPercentage.ValueChanged += nudStrPercentage_ValueChanged;
        // 
        // nudIntPercentage
        // 
        nudIntPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudIntPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudIntPercentage.Location = new System.Drawing.Point(191, 187);
        nudIntPercentage.Margin = new Padding(4, 3, 4, 3);
        nudIntPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudIntPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudIntPercentage.Name = "nudIntPercentage";
        nudIntPercentage.Size = new Size(50, 23);
        nudIntPercentage.TabIndex = 71;
        nudIntPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudIntPercentage.ValueChanged += nudARPPercentage_ValueChanged;
        // 
        // nudVitPercentage
        // 
        nudVitPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudVitPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudVitPercentage.Location = new System.Drawing.Point(192, 219);
        nudVitPercentage.Margin = new Padding(4, 3, 4, 3);
        nudVitPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudVitPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudVitPercentage.Name = "nudVitPercentage";
        nudVitPercentage.Size = new Size(50, 23);
        nudVitPercentage.TabIndex = 72;
        nudVitPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudVitPercentage.ValueChanged += nudVitPercentage_ValueChanged;
        // 
        // nudAgiPercentage
        // 
        nudAgiPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudAgiPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudAgiPercentage.Location = new System.Drawing.Point(192, 253);
        nudAgiPercentage.Margin = new Padding(4, 3, 4, 3);
        nudAgiPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudAgiPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        nudAgiPercentage.Name = "nudAgiPercentage";
        nudAgiPercentage.Size = new Size(50, 23);
        nudAgiPercentage.TabIndex = 73;
        nudAgiPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudAgiPercentage.ValueChanged += nudWisPercentage_ValueChanged;
        // 
        // lblPlus8
        // 
        lblPlus8.AutoSize = true;
        lblPlus8.Location = new System.Drawing.Point(173, 156);
        lblPlus8.Margin = new Padding(2, 0, 2, 0);
        lblPlus8.Name = "lblPlus8";
        lblPlus8.Size = new Size(15, 15);
        lblPlus8.TabIndex = 74;
        lblPlus8.Text = "+";
        // 
        // lblPlus7
        // 
        lblPlus7.AutoSize = true;
        lblPlus7.Location = new System.Drawing.Point(173, 188);
        lblPlus7.Margin = new Padding(2, 0, 2, 0);
        lblPlus7.Name = "lblPlus7";
        lblPlus7.Size = new Size(15, 15);
        lblPlus7.TabIndex = 75;
        lblPlus7.Text = "+";
        // 
        // lblPlus6
        // 
        lblPlus6.AutoSize = true;
        lblPlus6.Location = new System.Drawing.Point(173, 221);
        lblPlus6.Margin = new Padding(2, 0, 2, 0);
        lblPlus6.Name = "lblPlus6";
        lblPlus6.Size = new Size(15, 15);
        lblPlus6.TabIndex = 76;
        lblPlus6.Text = "+";
        // 
        // lblPlus5
        // 
        lblPlus5.AutoSize = true;
        lblPlus5.Location = new System.Drawing.Point(173, 255);
        lblPlus5.Margin = new Padding(2, 0, 2, 0);
        lblPlus5.Name = "lblPlus5";
        lblPlus5.Size = new Size(15, 15);
        lblPlus5.TabIndex = 57;
        lblPlus5.Text = "+";
        // 
        // lblPlus4
        // 
        lblPlus4.AutoSize = true;
        lblPlus4.Location = new System.Drawing.Point(173, 120);
        lblPlus4.Margin = new Padding(2, 0, 2, 0);
        lblPlus4.Name = "lblPlus4";
        lblPlus4.Size = new Size(15, 15);
        lblPlus4.TabIndex = 56;
        lblPlus4.Text = "+";
        // 
        // lblPlus3
        // 
        lblPlus3.AutoSize = true;
        lblPlus3.Location = new System.Drawing.Point(173, 88);
        lblPlus3.Margin = new Padding(2, 0, 2, 0);
        lblPlus3.Name = "lblPlus3";
        lblPlus3.Size = new Size(15, 15);
        lblPlus3.TabIndex = 55;
        lblPlus3.Text = "+";
        // 
        // lblPlus2
        // 
        lblPlus2.AutoSize = true;
        lblPlus2.Location = new System.Drawing.Point(173, 55);
        lblPlus2.Margin = new Padding(2, 0, 2, 0);
        lblPlus2.Name = "lblPlus2";
        lblPlus2.Size = new Size(15, 15);
        lblPlus2.TabIndex = 54;
        lblPlus2.Text = "+";
        // 
        // lblPlus1
        // 
        lblPlus1.AutoSize = true;
        lblPlus1.Location = new System.Drawing.Point(173, 23);
        lblPlus1.Margin = new Padding(2, 0, 2, 0);
        lblPlus1.Name = "lblPlus1";
        lblPlus1.Size = new Size(15, 15);
        lblPlus1.TabIndex = 53;
        lblPlus1.Text = "+";
        // 
        // nudSpd
        // 
        nudSpd.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
        nudSpd.Location = new System.Drawing.Point(96, 153);
        nudSpd.Margin = new Padding(4, 3, 4, 3);
        nudSpd.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudSpd.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudSpd.Name = "nudSpd";
        nudSpd.Size = new Size(70, 23);
        nudSpd.TabIndex = 52;
        nudSpd.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudSpd.ValueChanged += nudSpd_ValueChanged;
        // 
        // nudCur
        // 
        nudCur.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudCur.ForeColor = System.Drawing.Color.Gainsboro;
        nudCur.Location = new System.Drawing.Point(96, 118);
        nudCur.Margin = new Padding(4, 3, 4, 3);
        nudCur.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudCur.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudCur.Name = "nudCur";
        nudCur.Size = new Size(70, 23);
        nudCur.TabIndex = 51;
        nudCur.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudCur.ValueChanged += nudMR_ValueChanged;
        // 
        // nudDef
        // 
        nudDef.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudDef.ForeColor = System.Drawing.Color.Gainsboro;
        nudDef.Location = new System.Drawing.Point(96, 87);
        nudDef.Margin = new Padding(4, 3, 4, 3);
        nudDef.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudDef.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudDef.Name = "nudDef";
        nudDef.Size = new Size(70, 23);
        nudDef.TabIndex = 50;
        nudDef.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudDef.ValueChanged += nudDef_ValueChanged;
        // 
        // nudDmg
        // 
        nudDmg.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudDmg.ForeColor = System.Drawing.Color.Gainsboro;
        nudDmg.Location = new System.Drawing.Point(96, 54);
        nudDmg.Margin = new Padding(4, 3, 4, 3);
        nudDmg.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudDmg.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudDmg.Name = "nudDmg";
        nudDmg.Size = new Size(70, 23);
        nudDmg.TabIndex = 49;
        nudDmg.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudDmg.ValueChanged += nudMag_ValueChanged;
        // 
        // nudStr
        // 
        nudStr.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudStr.ForeColor = System.Drawing.Color.Gainsboro;
        nudStr.Location = new System.Drawing.Point(96, 24);
        nudStr.Margin = new Padding(4, 3, 4, 3);
        nudStr.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudStr.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudStr.Name = "nudStr";
        nudStr.Size = new Size(70, 23);
        nudStr.TabIndex = 48;
        nudStr.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudStr.ValueChanged += nudStr_ValueChanged;
        // 
        // nudInt
        // 
        nudInt.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudInt.ForeColor = System.Drawing.Color.Gainsboro;
        nudInt.Location = new System.Drawing.Point(96, 187);
        nudInt.Margin = new Padding(4, 3, 4, 3);
        nudInt.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudInt.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudInt.Name = "nudInt";
        nudInt.Size = new Size(70, 23);
        nudInt.TabIndex = 77;
        nudInt.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudInt.ValueChanged += nudARP_ValueChanged;
        // 
        // nudVit
        // 
        nudVit.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudVit.ForeColor = System.Drawing.Color.Gainsboro;
        nudVit.Location = new System.Drawing.Point(96, 219);
        nudVit.Margin = new Padding(4, 3, 4, 3);
        nudVit.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudVit.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudVit.Name = "nudVit";
        nudVit.Size = new Size(70, 23);
        nudVit.TabIndex = 78;
        nudVit.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudVit.ValueChanged += nudVit_ValueChanged;
        // 
        // nudAgi
        // 
        nudAgi.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudAgi.ForeColor = System.Drawing.Color.Gainsboro;
        nudAgi.Location = new System.Drawing.Point(96, 251);
        nudAgi.Margin = new Padding(4, 3, 4, 3);
        nudAgi.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        nudAgi.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
        nudAgi.Name = "nudAgi";
        nudAgi.Size = new Size(70, 23);
        nudAgi.TabIndex = 79;
        nudAgi.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudAgi.ValueChanged += nudWis_ValueChanged;
        // 
        // lblSpd
        // 
        lblSpd.AutoSize = true;
        lblSpd.Location = new System.Drawing.Point(7, 157);
        lblSpd.Margin = new Padding(2, 0, 2, 0);
        lblSpd.Name = "lblSpd";
        lblSpd.Size = new Size(75, 15);
        lblSpd.TabIndex = 47;
        lblSpd.Text = "Move Speed:";
        // 
        // lblMR
        // 
        lblMR.AutoSize = true;
        lblMR.Location = new System.Drawing.Point(8, 123);
        lblMR.Margin = new Padding(2, 0, 2, 0);
        lblMR.Name = "lblMR";
        lblMR.Size = new Size(40, 15);
        lblMR.TabIndex = 46;
        lblMR.Text = "Cures:";
        // 
        // lblDef
        // 
        lblDef.AutoSize = true;
        lblDef.Location = new System.Drawing.Point(7, 91);
        lblDef.Margin = new Padding(2, 0, 2, 0);
        lblDef.Name = "lblDef";
        lblDef.Size = new Size(44, 15);
        lblDef.TabIndex = 45;
        lblDef.Text = "Armor:";
        // 
        // lblMag
        // 
        lblMag.AutoSize = true;
        lblMag.Location = new System.Drawing.Point(8, 59);
        lblMag.Margin = new Padding(2, 0, 2, 0);
        lblMag.Name = "lblMag";
        lblMag.Size = new Size(59, 15);
        lblMag.TabIndex = 44;
        lblMag.Text = "Damages:";
        // 
        // lblStr
        // 
        lblStr.AutoSize = true;
        lblStr.Location = new System.Drawing.Point(8, 27);
        lblStr.Margin = new Padding(2, 0, 2, 0);
        lblStr.Name = "lblStr";
        lblStr.Size = new Size(55, 15);
        lblStr.TabIndex = 43;
        lblStr.Text = "Strength:";
        // 
        // lblARP
        // 
        lblARP.AutoSize = true;
        lblARP.Location = new System.Drawing.Point(8, 191);
        lblARP.Margin = new Padding(2, 0, 2, 0);
        lblARP.Name = "lblARP";
        lblARP.Size = new Size(74, 15);
        lblARP.TabIndex = 80;
        lblARP.Text = "Intelligence.:";
        // 
        // lblVit
        // 
        lblVit.AutoSize = true;
        lblVit.Location = new System.Drawing.Point(8, 224);
        lblVit.Margin = new Padding(2, 0, 2, 0);
        lblVit.Name = "lblVit";
        lblVit.Size = new Size(46, 15);
        lblVit.TabIndex = 81;
        lblVit.Text = "Vitality:";
        // 
        // lblWis
        // 
        lblWis.AutoSize = true;
        lblWis.Location = new System.Drawing.Point(9, 257);
        lblWis.Margin = new Padding(2, 0, 2, 0);
        lblWis.Name = "lblWis";
        lblWis.Size = new Size(44, 15);
        lblWis.TabIndex = 82;
        lblWis.Text = "Agility:";
        // 
        // grpItemsSets
        // 
        grpItemsSets.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpItemsSets.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpItemsSets.Controls.Add(cmbItems);
        grpItemsSets.Controls.Add(btnRemove);
        grpItemsSets.Controls.Add(btnAdd);
        grpItemsSets.Controls.Add(lblItemSet);
        grpItemsSets.Controls.Add(lstItems);
        grpItemsSets.ForeColor = System.Drawing.Color.Gainsboro;
        grpItemsSets.Location = new System.Drawing.Point(243, 133);
        grpItemsSets.Margin = new Padding(4, 3, 4, 3);
        grpItemsSets.Name = "grpItemsSets";
        grpItemsSets.Padding = new Padding(4, 3, 4, 3);
        grpItemsSets.Size = new Size(253, 307);
        grpItemsSets.TabIndex = 47;
        grpItemsSets.TabStop = false;
        grpItemsSets.Text = "Item";
        // 
        // cmbItems
        // 
        cmbItems.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        cmbItems.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        cmbItems.BorderStyle = ButtonBorderStyle.Solid;
        cmbItems.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
        cmbItems.DrawDropdownHoverOutline = false;
        cmbItems.DrawFocusRectangle = false;
        cmbItems.DrawMode = DrawMode.OwnerDrawFixed;
        cmbItems.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbItems.FlatStyle = FlatStyle.Flat;
        cmbItems.ForeColor = System.Drawing.Color.Gainsboro;
        cmbItems.FormattingEnabled = true;
        cmbItems.Location = new System.Drawing.Point(12, 241);
        cmbItems.Margin = new Padding(4, 3, 4, 3);
        cmbItems.Name = "cmbItems";
        cmbItems.Size = new Size(233, 24);
        cmbItems.TabIndex = 40;
        cmbItems.Text = null;
        cmbItems.TextPadding = new Padding(2);
        cmbItems.SelectedIndexChanged += cmbItems_SelectedIndexChanged;
        // 
        // btnRemove
        // 
        btnRemove.Location = new System.Drawing.Point(153, 271);
        btnRemove.Margin = new Padding(4, 3, 4, 3);
        btnRemove.Name = "btnRemove";
        btnRemove.Padding = new Padding(6);
        btnRemove.Size = new Size(92, 27);
        btnRemove.TabIndex = 38;
        btnRemove.Text = "Remove";
        btnRemove.Click += btnRemove_Click;
        // 
        // btnAdd
        // 
        btnAdd.Location = new System.Drawing.Point(10, 271);
        btnAdd.Margin = new Padding(4, 3, 4, 3);
        btnAdd.Name = "btnAdd";
        btnAdd.Padding = new Padding(6);
        btnAdd.Size = new Size(88, 27);
        btnAdd.TabIndex = 37;
        btnAdd.Text = "Add";
        btnAdd.Click += btnAdd_Click;
        // 
        // lblItemSet
        // 
        lblItemSet.AutoSize = true;
        lblItemSet.Location = new System.Drawing.Point(9, 222);
        lblItemSet.Margin = new Padding(4, 0, 4, 0);
        lblItemSet.Name = "lblItemSet";
        lblItemSet.Size = new Size(34, 15);
        lblItemSet.TabIndex = 31;
        lblItemSet.Text = "Item:";
        // 
        // lstItems
        // 
        lstItems.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
        lstItems.BorderStyle = BorderStyle.FixedSingle;
        lstItems.ForeColor = System.Drawing.Color.Gainsboro;
        lstItems.FormattingEnabled = true;
        lstItems.ItemHeight = 15;
        lstItems.Items.AddRange(new object[] { "Item: None" });
        lstItems.Location = new System.Drawing.Point(14, 22);
        lstItems.Margin = new Padding(4, 3, 4, 3);
        lstItems.Name = "lstItems";
        lstItems.Size = new Size(231, 197);
        lstItems.TabIndex = 29;
        lstItems.SelectedIndexChanged += lstItems_SelectedIndexChanged;
        // 
        // grpGeneral
        // 
        grpGeneral.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpGeneral.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpGeneral.Controls.Add(btnAddFolder);
        grpGeneral.Controls.Add(lblFolder);
        grpGeneral.Controls.Add(cmbFolder);
        grpGeneral.Controls.Add(lblName);
        grpGeneral.Controls.Add(txtName);
        grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
        grpGeneral.Location = new System.Drawing.Point(244, 32);
        grpGeneral.Margin = new Padding(4, 3, 4, 3);
        grpGeneral.Name = "grpGeneral";
        grpGeneral.Padding = new Padding(4, 3, 4, 3);
        grpGeneral.Size = new Size(252, 95);
        grpGeneral.TabIndex = 31;
        grpGeneral.TabStop = false;
        grpGeneral.Text = "General";
        // 
        // btnAddFolder
        // 
        btnAddFolder.Location = new System.Drawing.Point(225, 53);
        btnAddFolder.Margin = new Padding(4, 3, 4, 3);
        btnAddFolder.Name = "btnAddFolder";
        btnAddFolder.Padding = new Padding(6);
        btnAddFolder.Size = new Size(21, 24);
        btnAddFolder.TabIndex = 46;
        btnAddFolder.Text = "+";
        btnAddFolder.Click += btnAddFolder_Click;
        // 
        // lblFolder
        // 
        lblFolder.AutoSize = true;
        lblFolder.Location = new System.Drawing.Point(8, 58);
        lblFolder.Margin = new Padding(4, 0, 4, 0);
        lblFolder.Name = "lblFolder";
        lblFolder.Size = new Size(43, 15);
        lblFolder.TabIndex = 45;
        lblFolder.Text = "Folder:";
        // 
        // cmbFolder
        // 
        cmbFolder.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        cmbFolder.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        cmbFolder.BorderStyle = ButtonBorderStyle.Solid;
        cmbFolder.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
        cmbFolder.DrawDropdownHoverOutline = false;
        cmbFolder.DrawFocusRectangle = false;
        cmbFolder.DrawMode = DrawMode.OwnerDrawFixed;
        cmbFolder.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFolder.FlatStyle = FlatStyle.Flat;
        cmbFolder.ForeColor = System.Drawing.Color.Gainsboro;
        cmbFolder.FormattingEnabled = true;
        cmbFolder.Location = new System.Drawing.Point(59, 53);
        cmbFolder.Margin = new Padding(4, 3, 4, 3);
        cmbFolder.Name = "cmbFolder";
        cmbFolder.Size = new Size(158, 24);
        cmbFolder.TabIndex = 44;
        cmbFolder.Text = null;
        cmbFolder.TextPadding = new Padding(2);
        cmbFolder.SelectedIndexChanged += cmbFolder_SelectedIndexChanged;
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new System.Drawing.Point(8, 20);
        lblName.Margin = new Padding(4, 0, 4, 0);
        lblName.Name = "lblName";
        lblName.Size = new Size(42, 15);
        lblName.TabIndex = 19;
        lblName.Text = "Name:";
        // 
        // txtName
        // 
        txtName.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
        txtName.Location = new System.Drawing.Point(59, 18);
        txtName.Margin = new Padding(4, 3, 4, 3);
        txtName.Name = "txtName";
        txtName.Size = new Size(158, 23);
        txtName.TabIndex = 18;
        txtName.TextChanged += txtName_TextChanged;
        // 
        // grpVitalBonuses
        // 
        grpVitalBonuses.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpVitalBonuses.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpVitalBonuses.Controls.Add(label1);
        grpVitalBonuses.Controls.Add(label2);
        grpVitalBonuses.Controls.Add(nudMPPercentage);
        grpVitalBonuses.Controls.Add(nudHPPercentage);
        grpVitalBonuses.Controls.Add(label3);
        grpVitalBonuses.Controls.Add(label4);
        grpVitalBonuses.Controls.Add(nudManaBonus);
        grpVitalBonuses.Controls.Add(nudHealthBonus);
        grpVitalBonuses.Controls.Add(lblManaBonus);
        grpVitalBonuses.Controls.Add(lblHealthBonus);
        grpVitalBonuses.ForeColor = System.Drawing.Color.Gainsboro;
        grpVitalBonuses.Location = new System.Drawing.Point(504, 319);
        grpVitalBonuses.Margin = new Padding(4, 3, 4, 3);
        grpVitalBonuses.Name = "grpVitalBonuses";
        grpVitalBonuses.Padding = new Padding(4, 3, 4, 3);
        grpVitalBonuses.Size = new Size(276, 127);
        grpVitalBonuses.TabIndex = 61;
        grpVitalBonuses.TabStop = false;
        grpVitalBonuses.Text = "Vital Bonuses";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(252, 95);
        label1.Margin = new Padding(2, 0, 2, 0);
        label1.Name = "label1";
        label1.Size = new Size(17, 15);
        label1.TabIndex = 70;
        label1.Text = "%";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(252, 45);
        label2.Margin = new Padding(2, 0, 2, 0);
        label2.Name = "label2";
        label2.Size = new Size(17, 15);
        label2.TabIndex = 69;
        label2.Text = "%";
        // 
        // nudMPPercentage
        // 
        nudMPPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudMPPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudMPPercentage.Location = new System.Drawing.Point(155, 92);
        nudMPPercentage.Margin = new Padding(4, 3, 4, 3);
        nudMPPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudMPPercentage.Minimum = new decimal(new int[] { -100, 0, 0, int.MinValue });
        nudMPPercentage.Name = "nudMPPercentage";
        nudMPPercentage.Size = new Size(90, 23);
        nudMPPercentage.TabIndex = 68;
        nudMPPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudMPPercentage.ValueChanged += nudMPPercentage_ValueChanged;
        // 
        // nudHPPercentage
        // 
        nudHPPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudHPPercentage.ForeColor = System.Drawing.Color.Gainsboro;
        nudHPPercentage.Location = new System.Drawing.Point(155, 43);
        nudHPPercentage.Margin = new Padding(4, 3, 4, 3);
        nudHPPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudHPPercentage.Minimum = new decimal(new int[] { -100, 0, 0, int.MinValue });
        nudHPPercentage.Name = "nudHPPercentage";
        nudHPPercentage.Size = new Size(90, 23);
        nudHPPercentage.TabIndex = 67;
        nudHPPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudHPPercentage.ValueChanged += nudHPPercentage_ValueChanged;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(134, 95);
        label3.Margin = new Padding(2, 0, 2, 0);
        label3.Name = "label3";
        label3.Size = new Size(15, 15);
        label3.TabIndex = 66;
        label3.Text = "+";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(134, 45);
        label4.Margin = new Padding(2, 0, 2, 0);
        label4.Name = "label4";
        label4.Size = new Size(15, 15);
        label4.TabIndex = 65;
        label4.Text = "+";
        // 
        // nudManaBonus
        // 
        nudManaBonus.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudManaBonus.ForeColor = System.Drawing.Color.Gainsboro;
        nudManaBonus.Location = new System.Drawing.Point(14, 92);
        nudManaBonus.Margin = new Padding(4, 3, 4, 3);
        nudManaBonus.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
        nudManaBonus.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
        nudManaBonus.Name = "nudManaBonus";
        nudManaBonus.Size = new Size(111, 23);
        nudManaBonus.TabIndex = 49;
        nudManaBonus.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudManaBonus.ValueChanged += nudManaBonus_ValueChanged;
        // 
        // nudHealthBonus
        // 
        nudHealthBonus.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudHealthBonus.ForeColor = System.Drawing.Color.Gainsboro;
        nudHealthBonus.Location = new System.Drawing.Point(14, 43);
        nudHealthBonus.Margin = new Padding(4, 3, 4, 3);
        nudHealthBonus.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
        nudHealthBonus.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
        nudHealthBonus.Name = "nudHealthBonus";
        nudHealthBonus.Size = new Size(111, 23);
        nudHealthBonus.TabIndex = 48;
        nudHealthBonus.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudHealthBonus.ValueChanged += nudHealthBonus_ValueChanged;
        // 
        // lblManaBonus
        // 
        lblManaBonus.AutoSize = true;
        lblManaBonus.Location = new System.Drawing.Point(11, 74);
        lblManaBonus.Margin = new Padding(2, 0, 2, 0);
        lblManaBonus.Name = "lblManaBonus";
        lblManaBonus.Size = new Size(40, 15);
        lblManaBonus.TabIndex = 44;
        lblManaBonus.Text = "Mana:";
        // 
        // lblHealthBonus
        // 
        lblHealthBonus.AutoSize = true;
        lblHealthBonus.Location = new System.Drawing.Point(10, 24);
        lblHealthBonus.Margin = new Padding(2, 0, 2, 0);
        lblHealthBonus.Name = "lblHealthBonus";
        lblHealthBonus.Size = new Size(45, 15);
        lblHealthBonus.TabIndex = 43;
        lblHealthBonus.Text = "Health:";
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new System.Drawing.Point(900, 473);
        btnCancel.Margin = new Padding(4, 3, 4, 3);
        btnCancel.Name = "btnCancel";
        btnCancel.Padding = new Padding(6);
        btnCancel.Size = new Size(201, 31);
        btnCancel.TabIndex = 49;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // btnSave
        // 
        btnSave.Location = new System.Drawing.Point(695, 473);
        btnSave.Margin = new Padding(4, 3, 4, 3);
        btnSave.Name = "btnSave";
        btnSave.Padding = new Padding(6);
        btnSave.Size = new Size(197, 31);
        btnSave.TabIndex = 48;
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        // 
        // grpEffects
        // 
        grpEffects.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpEffects.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpEffects.Controls.Add(lstBonusEffects);
        grpEffects.Controls.Add(lblEffectPercent);
        grpEffects.Controls.Add(nudEffectPercent);
        grpEffects.ForeColor = System.Drawing.Color.Gainsboro;
        grpEffects.Location = new System.Drawing.Point(788, 170);
        grpEffects.Margin = new Padding(4, 3, 4, 3);
        grpEffects.Name = "grpEffects";
        grpEffects.Padding = new Padding(4, 3, 4, 3);
        grpEffects.Size = new Size(307, 226);
        grpEffects.TabIndex = 60;
        grpEffects.TabStop = false;
        grpEffects.Text = "Bonus Effects";
        // 
        // lstBonusEffects
        // 
        lstBonusEffects.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
        lstBonusEffects.BorderStyle = BorderStyle.FixedSingle;
        lstBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
        lstBonusEffects.FormattingEnabled = true;
        lstBonusEffects.ItemHeight = 15;
        lstBonusEffects.Location = new System.Drawing.Point(15, 22);
        lstBonusEffects.Margin = new Padding(4, 3, 4, 3);
        lstBonusEffects.Name = "lstBonusEffects";
        lstBonusEffects.Size = new Size(284, 137);
        lstBonusEffects.TabIndex = 58;
        lstBonusEffects.SelectedIndexChanged += lstBonusEffects_SelectedIndexChanged;
        // 
        // lblEffectPercent
        // 
        lblEffectPercent.AutoSize = true;
        lblEffectPercent.Location = new System.Drawing.Point(13, 168);
        lblEffectPercent.Margin = new Padding(4, 0, 4, 0);
        lblEffectPercent.Name = "lblEffectPercent";
        lblEffectPercent.Size = new Size(108, 15);
        lblEffectPercent.TabIndex = 31;
        lblEffectPercent.Text = "Effect Amount (%):";
        // 
        // nudEffectPercent
        // 
        nudEffectPercent.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudEffectPercent.ForeColor = System.Drawing.Color.Gainsboro;
        nudEffectPercent.Location = new System.Drawing.Point(15, 190);
        nudEffectPercent.Margin = new Padding(4, 3, 4, 3);
        nudEffectPercent.Name = "nudEffectPercent";
        nudEffectPercent.Size = new Size(282, 23);
        nudEffectPercent.TabIndex = 55;
        nudEffectPercent.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudEffectPercent.ValueChanged += nudEffectPercent_ValueChanged;
        // 
        // grpRegen
        // 
        grpRegen.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpRegen.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpRegen.Controls.Add(nudMpRegen);
        grpRegen.Controls.Add(nudHPRegen);
        grpRegen.Controls.Add(lblHpRegen);
        grpRegen.Controls.Add(lblManaRegen);
        grpRegen.Controls.Add(lblRegenHint);
        grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
        grpRegen.Location = new System.Drawing.Point(788, 32);
        grpRegen.Margin = new Padding(2);
        grpRegen.Name = "grpRegen";
        grpRegen.Padding = new Padding(2);
        grpRegen.Size = new Size(307, 133);
        grpRegen.TabIndex = 62;
        grpRegen.TabStop = false;
        grpRegen.Text = "Regen";
        // 
        // nudMpRegen
        // 
        nudMpRegen.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudMpRegen.ForeColor = System.Drawing.Color.Gainsboro;
        nudMpRegen.Location = new System.Drawing.Point(155, 40);
        nudMpRegen.Margin = new Padding(4, 3, 4, 3);
        nudMpRegen.Name = "nudMpRegen";
        nudMpRegen.Size = new Size(112, 23);
        nudMpRegen.TabIndex = 31;
        nudMpRegen.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudMpRegen.ValueChanged += nudMpRegen_ValueChanged;
        // 
        // nudHPRegen
        // 
        nudHPRegen.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudHPRegen.ForeColor = System.Drawing.Color.Gainsboro;
        nudHPRegen.Location = new System.Drawing.Point(13, 42);
        nudHPRegen.Margin = new Padding(4, 3, 4, 3);
        nudHPRegen.Name = "nudHPRegen";
        nudHPRegen.Size = new Size(112, 23);
        nudHPRegen.TabIndex = 30;
        nudHPRegen.Value = new decimal(new int[] { 0, 0, 0, 0 });
        nudHPRegen.ValueChanged += nudHPRegen_ValueChanged;
        // 
        // lblHpRegen
        // 
        lblHpRegen.AutoSize = true;
        lblHpRegen.Location = new System.Drawing.Point(5, 21);
        lblHpRegen.Margin = new Padding(2, 0, 2, 0);
        lblHpRegen.Name = "lblHpRegen";
        lblHpRegen.Size = new Size(47, 15);
        lblHpRegen.TabIndex = 26;
        lblHpRegen.Text = "HP: (%)";
        // 
        // lblManaRegen
        // 
        lblManaRegen.AutoSize = true;
        lblManaRegen.Location = new System.Drawing.Point(154, 21);
        lblManaRegen.Margin = new Padding(2, 0, 2, 0);
        lblManaRegen.Name = "lblManaRegen";
        lblManaRegen.Size = new Size(61, 15);
        lblManaRegen.TabIndex = 27;
        lblManaRegen.Text = "Mana: (%)";
        // 
        // lblRegenHint
        // 
        lblRegenHint.Location = new System.Drawing.Point(8, 81);
        lblRegenHint.Margin = new Padding(4, 0, 4, 0);
        lblRegenHint.Name = "lblRegenHint";
        lblRegenHint.Size = new Size(292, 36);
        lblRegenHint.TabIndex = 0;
        lblRegenHint.Text = "% of HP/Mana to restore per tick.\r\nTick timer saved in server config.json.";
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
        toolStrip.Size = new Size(1116, 29);
        toolStrip.TabIndex = 63;
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
        // frmSets
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        ClientSize = new Size(1116, 514);
        ControlBox = false;
        Controls.Add(toolStrip);
        Controls.Add(grpStats);
        Controls.Add(grpEffects);
        Controls.Add(grpVitalBonuses);
        Controls.Add(grpItemsSets);
        Controls.Add(btnCancel);
        Controls.Add(grpGeneral);
        Controls.Add(grpRegen);
        Controls.Add(btnSave);
        Controls.Add(grpSets);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        KeyPreview = true;
        Name = "frmSets";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Set Editor";
        KeyDown += form_KeyDown;
        grpSets.ResumeLayout(false);
        grpSets.PerformLayout();
        grpStats.ResumeLayout(false);
        grpStats.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSpdPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudCurPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDefPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDmgPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudStrPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudIntPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudVitPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAgiPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSpd).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudCur).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDef).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDmg).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudStr).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudInt).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudVit).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAgi).EndInit();
        grpItemsSets.ResumeLayout(false);
        grpItemsSets.PerformLayout();
        grpGeneral.ResumeLayout(false);
        grpGeneral.PerformLayout();
        grpVitalBonuses.ResumeLayout(false);
        grpVitalBonuses.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudMPPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudHPPercentage).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudManaBonus).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudHealthBonus).EndInit();
        grpEffects.ResumeLayout(false);
        grpEffects.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudEffectPercent).EndInit();
        grpRegen.ResumeLayout(false);
        grpRegen.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudMpRegen).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudHPRegen).EndInit();
        toolStrip.ResumeLayout(false);
        toolStrip.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private DarkUI.Controls.DarkGroupBox grpSets;
    private DarkUI.Controls.DarkButton btnClearSearch;
    private DarkUI.Controls.DarkTextBox txtSearch;
    private Controls.GameObjectList lstGameObjects;
    private DarkUI.Controls.DarkGroupBox grpItemsSets;
    private DarkUI.Controls.DarkComboBox cmbItems;
    private DarkUI.Controls.DarkButton btnRemove;
    private DarkUI.Controls.DarkButton btnAdd;
    private Label lblItemSet;
    private ListBox lstItems;
    private DarkUI.Controls.DarkGroupBox grpGeneral;
    private DarkUI.Controls.DarkButton btnAddFolder;
    private Label lblFolder;
    private DarkUI.Controls.DarkComboBox cmbFolder;
    private Label lblName;
    private DarkUI.Controls.DarkTextBox txtName;
    private DarkUI.Controls.DarkButton btnCancel;
    private DarkUI.Controls.DarkButton btnSave;
    private DarkUI.Controls.DarkGroupBox grpStats;
    private Label lblPercentage8;
    private Label lblPercentage7;
    private Label lblPercentage6;
    private Label lblPercentage5;
    private Label lblPercentage4;
    private Label lblPercentage3;
    private Label lblPercentage2;
    private Label lblPercentage1;
    private DarkUI.Controls.DarkNumericUpDown nudSpdPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudCurPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudDefPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudDmgPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudStrPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudIntPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudVitPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudAgiPercentage;
    private Label lblPlus8;
    private Label lblPlus7;
    private Label lblPlus6;
    private Label lblPlus5;
    private Label lblPlus4;
    private Label lblPlus3;
    private Label lblPlus2;
    private Label lblPlus1;
    private DarkUI.Controls.DarkNumericUpDown nudSpd;
    private DarkUI.Controls.DarkNumericUpDown nudCur;
    private DarkUI.Controls.DarkNumericUpDown nudDef;
    private DarkUI.Controls.DarkNumericUpDown nudDmg;
    private DarkUI.Controls.DarkNumericUpDown nudStr;
    private DarkUI.Controls.DarkNumericUpDown nudInt;
    private DarkUI.Controls.DarkNumericUpDown nudVit;
    private DarkUI.Controls.DarkNumericUpDown nudAgi;
    private Label lblSpd;
    private Label lblMR;
    private Label lblDef;
    private Label lblMag;
    private Label lblStr;
    private Label lblARP;
    private Label lblVit;
    private Label lblWis;
    private DarkUI.Controls.DarkGroupBox grpEffects;
    private ListBox lstBonusEffects;
    private Label lblEffectPercent;
    private DarkUI.Controls.DarkNumericUpDown nudEffectPercent;
    private DarkUI.Controls.DarkGroupBox grpRegen;
    private DarkUI.Controls.DarkNumericUpDown nudMpRegen;
    private DarkUI.Controls.DarkNumericUpDown nudHPRegen;
    private Label lblHpRegen;
    private Label lblManaRegen;
    private Label lblRegenHint;
    private DarkUI.Controls.DarkGroupBox grpVitalBonuses;
    private Label label1;
    private Label label2;
    private DarkUI.Controls.DarkNumericUpDown nudMPPercentage;
    private DarkUI.Controls.DarkNumericUpDown nudHPPercentage;
    private Label label3;
    private Label label4;
    private DarkUI.Controls.DarkNumericUpDown nudManaBonus;
    private DarkUI.Controls.DarkNumericUpDown nudHealthBonus;
    private Label lblManaBonus;
    private Label lblHealthBonus;
    private DarkUI.Controls.DarkToolStrip toolStrip;
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