using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmSpell
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpell));
            pnlContainer = new Panel();
            grpGeneral = new DarkGroupBox();
            cmbCastSprite = new DarkComboBox();
            lblSpriteCastAnimation = new Label();
            btnAddFolder = new DarkButton();
            lblFolder = new Label();
            cmbFolder = new DarkComboBox();
            chkBound = new DarkCheckBox();
            cmbHitAnimation = new DarkComboBox();
            cmbCastAnimation = new DarkComboBox();
            lblDesc = new Label();
            txtDesc = new DarkTextBox();
            picSpell = new PictureBox();
            lblHitAnimation = new Label();
            lblCastAnimation = new Label();
            cmbSprite = new DarkComboBox();
            lblIcon = new Label();
            cmbTrapAnimation = new DarkComboBox();
            lblTrapAnimation = new Label();
            lblType = new Label();
            cmbType = new DarkComboBox();
            lblName = new Label();
            txtName = new DarkTextBox();
            grpSpellCost = new DarkGroupBox();
            chkIgnoreCdr = new DarkCheckBox();
            chkIgnoreGlobalCooldown = new DarkCheckBox();
            btnAddCooldownGroup = new DarkButton();
            cmbCooldownGroup = new DarkComboBox();
            lblCooldownGroup = new Label();
            nudCooldownDuration = new DarkNumericUpDown();
            nudCastDuration = new DarkNumericUpDown();
            nudMpCost = new DarkNumericUpDown();
            nudHPCost = new DarkNumericUpDown();
            lblMPCost = new Label();
            lblHPCost = new Label();
            lblCastDuration = new Label();
            lblCooldownDuration = new Label();
            grpRequirements = new DarkGroupBox();
            lblCannotCast = new Label();
            txtCannotCast = new DarkTextBox();
            btnDynamicRequirements = new DarkButton();
            grpTargetInfo = new DarkGroupBox();
            nudDuration = new DarkNumericUpDown();
            lblDuration = new Label();
            nudHitRadius = new DarkNumericUpDown();
            lblHitRadius = new Label();
            cmbTargetType = new DarkComboBox();
            lblCastRange = new Label();
            lblTargetType = new Label();
            nudCastRange = new DarkNumericUpDown();
            lblProjectile = new Label();
            cmbProjectile = new DarkComboBox();
            grpCombat = new DarkGroupBox();
            grpStats = new DarkGroupBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            nudCurPercentage = new DarkNumericUpDown();
            nudDmgPercentage = new DarkNumericUpDown();
            nudAgiPercentage = new DarkNumericUpDown();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            nudCur = new DarkNumericUpDown();
            nudDmg = new DarkNumericUpDown();
            nudAgi = new DarkNumericUpDown();
            Curlabel = new Label();
            dmgLabel = new Label();
            label11 = new Label();
            lblPercentage5 = new Label();
            lblPercentage4 = new Label();
            lblPercentage3 = new Label();
            lblPercentage2 = new Label();
            lblPercentage1 = new Label();
            nudSpdPercentage = new DarkNumericUpDown();
            nudMRPercentage = new DarkNumericUpDown();
            nudDefPercentage = new DarkNumericUpDown();
            nudMagPercentage = new DarkNumericUpDown();
            nudStrPercentage = new DarkNumericUpDown();
            lblPlus5 = new Label();
            lblPlus4 = new Label();
            lblPlus3 = new Label();
            lblPlus2 = new Label();
            lblPlus1 = new Label();
            nudSpd = new DarkNumericUpDown();
            nudMR = new DarkNumericUpDown();
            nudDef = new DarkNumericUpDown();
            nudMag = new DarkNumericUpDown();
            nudStr = new DarkNumericUpDown();
            lblSpd = new Label();
            lblMR = new Label();
            lblDef = new Label();
            lblMag = new Label();
            lblStr = new Label();
            grpHotDot = new DarkGroupBox();
            lblTickAnimation = new Label();
            cmbTickAnimation = new DarkComboBox();
            nudTick = new DarkNumericUpDown();
            chkHOTDOT = new DarkCheckBox();
            lblTick = new Label();
            grpEffect = new DarkGroupBox();
            lblEffect = new Label();
            cmbExtraEffect = new DarkComboBox();
            picSprite = new PictureBox();
            cmbTransform = new DarkComboBox();
            lblSprite = new Label();
            grpEffectDuration = new DarkGroupBox();
            nudBuffDuration = new DarkNumericUpDown();
            lblBuffDuration = new Label();
            grpDamage = new DarkGroupBox();
            nudCritMultiplier = new DarkNumericUpDown();
            lblCritMultiplier = new Label();
            nudCritChance = new DarkNumericUpDown();
            nudScaling = new DarkNumericUpDown();
            nudMPDamage = new DarkNumericUpDown();
            nudHPDamage = new DarkNumericUpDown();
            cmbScalingStat = new DarkComboBox();
            lblScalingStat = new Label();
            chkFriendly = new DarkCheckBox();
            lblCritChance = new Label();
            lblScaling = new Label();
            cmbDamageType = new DarkComboBox();
            lblDamageType = new Label();
            lblHPDamage = new Label();
            lblManaDamage = new Label();
            grpEvent = new DarkGroupBox();
            cmbEvent = new DarkComboBox();
            grpDash = new DarkGroupBox();
            grpDashCollisions = new DarkGroupBox();
            chkIgnoreInactiveResources = new DarkCheckBox();
            chkIgnoreZDimensionBlocks = new DarkCheckBox();
            chkIgnoreMapBlocks = new DarkCheckBox();
            chkIgnoreActiveResources = new DarkCheckBox();
            lblRange = new Label();
            scrlRange = new DarkScrollBar();
            grpWarp = new DarkGroupBox();
            nudWarpY = new DarkNumericUpDown();
            nudWarpX = new DarkNumericUpDown();
            btnVisualMapSelector = new DarkButton();
            cmbWarpMap = new DarkComboBox();
            cmbDirection = new DarkComboBox();
            lblWarpDir = new Label();
            lblY = new Label();
            lblX = new Label();
            lblMap = new Label();
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
            btnCancel = new DarkButton();
            btnSave = new DarkButton();
            grpSpells = new DarkGroupBox();
            btnClearSearch = new DarkButton();
            txtSearch = new DarkTextBox();
            lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            pnlContainer.SuspendLayout();
            grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSpell).BeginInit();
            grpSpellCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCooldownDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCastDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMpCost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHPCost).BeginInit();
            grpRequirements.SuspendLayout();
            grpTargetInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHitRadius).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCastRange).BeginInit();
            grpCombat.SuspendLayout();
            grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCurPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDmgPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAgiPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCur).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDmg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAgi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpdPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMRPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDefPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMagPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStrPercentage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDef).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStr).BeginInit();
            grpHotDot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTick).BeginInit();
            grpEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSprite).BeginInit();
            grpEffectDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBuffDuration).BeginInit();
            grpDamage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCritMultiplier).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCritChance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudScaling).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMPDamage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHPDamage).BeginInit();
            grpEvent.SuspendLayout();
            grpDash.SuspendLayout();
            grpDashCollisions.SuspendLayout();
            grpWarp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWarpY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWarpX).BeginInit();
            toolStrip.SuspendLayout();
            grpSpells.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.AutoScroll = true;
            pnlContainer.Controls.Add(grpGeneral);
            pnlContainer.Controls.Add(grpSpellCost);
            pnlContainer.Controls.Add(grpRequirements);
            pnlContainer.Controls.Add(grpTargetInfo);
            pnlContainer.Controls.Add(grpCombat);
            pnlContainer.Controls.Add(grpEvent);
            pnlContainer.Controls.Add(grpDash);
            pnlContainer.Controls.Add(grpWarp);
            pnlContainer.Location = new System.Drawing.Point(244, 39);
            pnlContainer.Margin = new Padding(4, 3, 4, 3);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1167, 613);
            pnlContainer.TabIndex = 41;
            pnlContainer.Visible = false;
            // 
            // grpGeneral
            // 
            grpGeneral.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpGeneral.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpGeneral.Controls.Add(cmbCastSprite);
            grpGeneral.Controls.Add(lblSpriteCastAnimation);
            grpGeneral.Controls.Add(btnAddFolder);
            grpGeneral.Controls.Add(lblFolder);
            grpGeneral.Controls.Add(cmbFolder);
            grpGeneral.Controls.Add(chkBound);
            grpGeneral.Controls.Add(cmbHitAnimation);
            grpGeneral.Controls.Add(cmbCastAnimation);
            grpGeneral.Controls.Add(lblDesc);
            grpGeneral.Controls.Add(txtDesc);
            grpGeneral.Controls.Add(picSpell);
            grpGeneral.Controls.Add(lblHitAnimation);
            grpGeneral.Controls.Add(lblCastAnimation);
            grpGeneral.Controls.Add(cmbSprite);
            grpGeneral.Controls.Add(lblIcon);
            grpGeneral.Controls.Add(cmbTrapAnimation);
            grpGeneral.Controls.Add(lblTrapAnimation);
            grpGeneral.Controls.Add(lblType);
            grpGeneral.Controls.Add(cmbType);
            grpGeneral.Controls.Add(lblName);
            grpGeneral.Controls.Add(txtName);
            grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            grpGeneral.Location = new System.Drawing.Point(9, 6);
            grpGeneral.Margin = new Padding(4, 3, 4, 3);
            grpGeneral.Name = "grpGeneral";
            grpGeneral.Padding = new Padding(4, 3, 4, 3);
            grpGeneral.Size = new Size(315, 490);
            grpGeneral.TabIndex = 17;
            grpGeneral.TabStop = false;
            grpGeneral.Text = "General";
            // 
            // cmbCastSprite
            // 
            cmbCastSprite.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbCastSprite.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbCastSprite.BorderStyle = ButtonBorderStyle.Solid;
            cmbCastSprite.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbCastSprite.DrawDropdownHoverOutline = false;
            cmbCastSprite.DrawFocusRectangle = false;
            cmbCastSprite.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCastSprite.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCastSprite.FlatStyle = FlatStyle.Flat;
            cmbCastSprite.ForeColor = System.Drawing.Color.Gainsboro;
            cmbCastSprite.FormattingEnabled = true;
            cmbCastSprite.Location = new System.Drawing.Point(113, 325);
            cmbCastSprite.Margin = new Padding(4, 3, 4, 3);
            cmbCastSprite.Name = "cmbCastSprite";
            cmbCastSprite.Size = new Size(188, 24);
            cmbCastSprite.TabIndex = 61;
            cmbCastSprite.Text = null;
            cmbCastSprite.TextPadding = new Padding(2);
            cmbCastSprite.SelectedIndexChanged += cmbCastSprite_SelectedIndexChanged;
            // 
            // lblSpriteCastAnimation
            // 
            lblSpriteCastAnimation.AutoSize = true;
            lblSpriteCastAnimation.Location = new System.Drawing.Point(7, 329);
            lblSpriteCastAnimation.Margin = new Padding(4, 0, 4, 0);
            lblSpriteCastAnimation.Name = "lblSpriteCastAnimation";
            lblSpriteCastAnimation.Size = new Size(101, 15);
            lblSpriteCastAnimation.TabIndex = 60;
            lblSpriteCastAnimation.Text = "Sprite Cast Anim.:";
            // 
            // btnAddFolder
            // 
            btnAddFolder.Location = new System.Drawing.Point(281, 54);
            btnAddFolder.Margin = new Padding(4, 3, 4, 3);
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.Padding = new Padding(6);
            btnAddFolder.Size = new Size(21, 24);
            btnAddFolder.TabIndex = 59;
            btnAddFolder.Text = "+";
            btnAddFolder.Click += btnAddFolder_Click;
            // 
            // lblFolder
            // 
            lblFolder.AutoSize = true;
            lblFolder.Location = new System.Drawing.Point(7, 59);
            lblFolder.Margin = new Padding(4, 0, 4, 0);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new Size(43, 15);
            lblFolder.TabIndex = 58;
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
            cmbFolder.Location = new System.Drawing.Point(70, 55);
            cmbFolder.Margin = new Padding(4, 3, 4, 3);
            cmbFolder.Name = "cmbFolder";
            cmbFolder.Size = new Size(204, 24);
            cmbFolder.TabIndex = 57;
            cmbFolder.Text = null;
            cmbFolder.TextPadding = new Padding(2);
            cmbFolder.SelectedIndexChanged += cmbFolder_SelectedIndexChanged;
            // 
            // chkBound
            // 
            chkBound.AutoSize = true;
            chkBound.CheckAlign = ContentAlignment.MiddleRight;
            chkBound.Location = new System.Drawing.Point(10, 295);
            chkBound.Margin = new Padding(4, 3, 4, 3);
            chkBound.Name = "chkBound";
            chkBound.RightToLeft = RightToLeft.Yes;
            chkBound.Size = new Size(66, 19);
            chkBound.TabIndex = 56;
            chkBound.Text = "Bound?";
            chkBound.CheckedChanged += chkBound_CheckedChanged;
            // 
            // cmbHitAnimation
            // 
            cmbHitAnimation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbHitAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbHitAnimation.BorderStyle = ButtonBorderStyle.Solid;
            cmbHitAnimation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbHitAnimation.DrawDropdownHoverOutline = false;
            cmbHitAnimation.DrawFocusRectangle = false;
            cmbHitAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbHitAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHitAnimation.FlatStyle = FlatStyle.Flat;
            cmbHitAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            cmbHitAnimation.FormattingEnabled = true;
            cmbHitAnimation.Location = new System.Drawing.Point(113, 405);
            cmbHitAnimation.Margin = new Padding(4, 3, 4, 3);
            cmbHitAnimation.Name = "cmbHitAnimation";
            cmbHitAnimation.Size = new Size(188, 24);
            cmbHitAnimation.TabIndex = 21;
            cmbHitAnimation.Text = null;
            cmbHitAnimation.TextPadding = new Padding(2);
            cmbHitAnimation.SelectedIndexChanged += cmbHitAnimation_SelectedIndexChanged;
            // 
            // cmbCastAnimation
            // 
            cmbCastAnimation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbCastAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbCastAnimation.BorderStyle = ButtonBorderStyle.Solid;
            cmbCastAnimation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbCastAnimation.DrawDropdownHoverOutline = false;
            cmbCastAnimation.DrawFocusRectangle = false;
            cmbCastAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCastAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCastAnimation.FlatStyle = FlatStyle.Flat;
            cmbCastAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            cmbCastAnimation.FormattingEnabled = true;
            cmbCastAnimation.Location = new System.Drawing.Point(113, 365);
            cmbCastAnimation.Margin = new Padding(4, 3, 4, 3);
            cmbCastAnimation.Name = "cmbCastAnimation";
            cmbCastAnimation.Size = new Size(188, 24);
            cmbCastAnimation.TabIndex = 20;
            cmbCastAnimation.Text = null;
            cmbCastAnimation.TextPadding = new Padding(2);
            cmbCastAnimation.SelectedIndexChanged += cmbCastAnimation_SelectedIndexChanged;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Location = new System.Drawing.Point(7, 159);
            lblDesc.Margin = new Padding(4, 0, 4, 0);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(98, 15);
            lblDesc.TabIndex = 19;
            lblDesc.Text = "Spell Description:";
            // 
            // txtDesc
            // 
            txtDesc.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtDesc.BorderStyle = BorderStyle.FixedSingle;
            txtDesc.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtDesc.Location = new System.Drawing.Point(10, 181);
            txtDesc.Margin = new Padding(4, 3, 4, 3);
            txtDesc.Multiline = true;
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new Size(291, 102);
            txtDesc.TabIndex = 18;
            txtDesc.TextChanged += txtDesc_TextChanged;
            // 
            // picSpell
            // 
            picSpell.BackColor = System.Drawing.Color.Black;
            picSpell.Location = new System.Drawing.Point(265, 121);
            picSpell.Margin = new Padding(4, 3, 4, 3);
            picSpell.Name = "picSpell";
            picSpell.Size = new Size(37, 37);
            picSpell.TabIndex = 4;
            picSpell.TabStop = false;
            // 
            // lblHitAnimation
            // 
            lblHitAnimation.AutoSize = true;
            lblHitAnimation.Location = new System.Drawing.Point(7, 408);
            lblHitAnimation.Margin = new Padding(4, 0, 4, 0);
            lblHitAnimation.Name = "lblHitAnimation";
            lblHitAnimation.Size = new Size(85, 15);
            lblHitAnimation.TabIndex = 16;
            lblHitAnimation.Text = "Hit Animation:";
            // 
            // lblCastAnimation
            // 
            lblCastAnimation.AutoSize = true;
            lblCastAnimation.Location = new System.Drawing.Point(7, 368);
            lblCastAnimation.Margin = new Padding(4, 0, 4, 0);
            lblCastAnimation.Name = "lblCastAnimation";
            lblCastAnimation.Size = new Size(97, 15);
            lblCastAnimation.TabIndex = 14;
            lblCastAnimation.Text = "Extra Cast Anim.:";
            // 
            // cmbSprite
            // 
            cmbSprite.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbSprite.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbSprite.BorderStyle = ButtonBorderStyle.Solid;
            cmbSprite.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbSprite.DrawDropdownHoverOutline = false;
            cmbSprite.DrawFocusRectangle = false;
            cmbSprite.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSprite.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSprite.FlatStyle = FlatStyle.Flat;
            cmbSprite.ForeColor = System.Drawing.Color.Gainsboro;
            cmbSprite.FormattingEnabled = true;
            cmbSprite.Items.AddRange(new object[] { "None" });
            cmbSprite.Location = new System.Drawing.Point(70, 128);
            cmbSprite.Margin = new Padding(4, 3, 4, 3);
            cmbSprite.Name = "cmbSprite";
            cmbSprite.Size = new Size(187, 24);
            cmbSprite.TabIndex = 11;
            cmbSprite.Text = "None";
            cmbSprite.TextPadding = new Padding(2);
            cmbSprite.SelectedIndexChanged += cmbSprite_SelectedIndexChanged;
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.Location = new System.Drawing.Point(7, 132);
            lblIcon.Margin = new Padding(4, 0, 4, 0);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(33, 15);
            lblIcon.TabIndex = 6;
            lblIcon.Text = "Icon:";
            // 
            // cmbTrapAnimation
            // 
            cmbTrapAnimation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbTrapAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbTrapAnimation.BorderStyle = ButtonBorderStyle.Solid;
            cmbTrapAnimation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbTrapAnimation.DrawDropdownHoverOutline = false;
            cmbTrapAnimation.DrawFocusRectangle = false;
            cmbTrapAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTrapAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrapAnimation.FlatStyle = FlatStyle.Flat;
            cmbTrapAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            cmbTrapAnimation.FormattingEnabled = true;
            cmbTrapAnimation.Location = new System.Drawing.Point(113, 445);
            cmbTrapAnimation.Name = "cmbTrapAnimation";
            cmbTrapAnimation.Size = new Size(188, 24);
            cmbTrapAnimation.TabIndex = 61;
            cmbTrapAnimation.Text = null;
            cmbTrapAnimation.TextPadding = new Padding(2);
            cmbTrapAnimation.SelectedIndexChanged += cmbTrapAnimation_SelectedIndexChanged;
            // 
            // lblTrapAnimation
            // 
            lblTrapAnimation.AutoSize = true;
            lblTrapAnimation.Location = new System.Drawing.Point(7, 448);
            lblTrapAnimation.Name = "lblTrapAnimation";
            lblTrapAnimation.Size = new Size(91, 15);
            lblTrapAnimation.TabIndex = 62;
            lblTrapAnimation.Text = "Trap Animation:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new System.Drawing.Point(7, 93);
            lblType.Margin = new Padding(4, 0, 4, 0);
            lblType.Name = "lblType";
            lblType.Size = new Size(34, 15);
            lblType.TabIndex = 3;
            lblType.Text = "Type:";
            // 
            // cmbType
            // 
            cmbType.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbType.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbType.BorderStyle = ButtonBorderStyle.Solid;
            cmbType.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbType.DrawDropdownHoverOutline = false;
            cmbType.DrawFocusRectangle = false;
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FlatStyle = FlatStyle.Flat;
            cmbType.ForeColor = System.Drawing.Color.Gainsboro;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "Combat Spell", "Warp to Map", "Warp to Target", "Dash", "Event" });
            cmbType.Location = new System.Drawing.Point(70, 90);
            cmbType.Margin = new Padding(4, 3, 4, 3);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(231, 24);
            cmbType.TabIndex = 2;
            cmbType.Text = "Combat Spell";
            cmbType.TextPadding = new Padding(2);
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(7, 23);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(42, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtName.Location = new System.Drawing.Point(70, 22);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(232, 23);
            txtName.TabIndex = 0;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // grpSpellCost
            // 
            grpSpellCost.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpSpellCost.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpSpellCost.Controls.Add(chkIgnoreCdr);
            grpSpellCost.Controls.Add(chkIgnoreGlobalCooldown);
            grpSpellCost.Controls.Add(btnAddCooldownGroup);
            grpSpellCost.Controls.Add(cmbCooldownGroup);
            grpSpellCost.Controls.Add(lblCooldownGroup);
            grpSpellCost.Controls.Add(nudCooldownDuration);
            grpSpellCost.Controls.Add(nudCastDuration);
            grpSpellCost.Controls.Add(nudMpCost);
            grpSpellCost.Controls.Add(nudHPCost);
            grpSpellCost.Controls.Add(lblMPCost);
            grpSpellCost.Controls.Add(lblHPCost);
            grpSpellCost.Controls.Add(lblCastDuration);
            grpSpellCost.Controls.Add(lblCooldownDuration);
            grpSpellCost.ForeColor = System.Drawing.Color.Gainsboro;
            grpSpellCost.Location = new System.Drawing.Point(334, 6);
            grpSpellCost.Margin = new Padding(4, 3, 4, 3);
            grpSpellCost.Name = "grpSpellCost";
            grpSpellCost.Padding = new Padding(4, 3, 4, 3);
            grpSpellCost.Size = new Size(262, 322);
            grpSpellCost.TabIndex = 36;
            grpSpellCost.TabStop = false;
            grpSpellCost.Text = "Spell Cost:";
            // 
            // chkIgnoreCdr
            // 
            chkIgnoreCdr.AutoSize = true;
            chkIgnoreCdr.Location = new System.Drawing.Point(16, 290);
            chkIgnoreCdr.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreCdr.Name = "chkIgnoreCdr";
            chkIgnoreCdr.Size = new Size(180, 19);
            chkIgnoreCdr.TabIndex = 57;
            chkIgnoreCdr.Text = "Ignore Cooldown Reduction?";
            chkIgnoreCdr.CheckedChanged += chkIgnoreCdr_CheckedChanged;
            // 
            // chkIgnoreGlobalCooldown
            // 
            chkIgnoreGlobalCooldown.AutoSize = true;
            chkIgnoreGlobalCooldown.Location = new System.Drawing.Point(16, 263);
            chkIgnoreGlobalCooldown.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreGlobalCooldown.Name = "chkIgnoreGlobalCooldown";
            chkIgnoreGlobalCooldown.Size = new Size(160, 19);
            chkIgnoreGlobalCooldown.TabIndex = 56;
            chkIgnoreGlobalCooldown.Text = "Ignore Global Cooldown?";
            chkIgnoreGlobalCooldown.CheckedChanged += chkIgnoreGlobalCooldown_CheckedChanged;
            // 
            // btnAddCooldownGroup
            // 
            btnAddCooldownGroup.Location = new System.Drawing.Point(230, 225);
            btnAddCooldownGroup.Margin = new Padding(4, 3, 4, 3);
            btnAddCooldownGroup.Name = "btnAddCooldownGroup";
            btnAddCooldownGroup.Padding = new Padding(6);
            btnAddCooldownGroup.Size = new Size(21, 24);
            btnAddCooldownGroup.TabIndex = 55;
            btnAddCooldownGroup.Text = "+";
            btnAddCooldownGroup.Click += btnAddCooldownGroup_Click;
            // 
            // cmbCooldownGroup
            // 
            cmbCooldownGroup.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbCooldownGroup.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbCooldownGroup.BorderStyle = ButtonBorderStyle.Solid;
            cmbCooldownGroup.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbCooldownGroup.DrawDropdownHoverOutline = false;
            cmbCooldownGroup.DrawFocusRectangle = false;
            cmbCooldownGroup.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCooldownGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCooldownGroup.FlatStyle = FlatStyle.Flat;
            cmbCooldownGroup.ForeColor = System.Drawing.Color.Gainsboro;
            cmbCooldownGroup.FormattingEnabled = true;
            cmbCooldownGroup.Location = new System.Drawing.Point(18, 226);
            cmbCooldownGroup.Margin = new Padding(4, 3, 4, 3);
            cmbCooldownGroup.Name = "cmbCooldownGroup";
            cmbCooldownGroup.Size = new Size(205, 24);
            cmbCooldownGroup.TabIndex = 54;
            cmbCooldownGroup.Text = null;
            cmbCooldownGroup.TextPadding = new Padding(2);
            cmbCooldownGroup.SelectedIndexChanged += cmbCooldownGroup_SelectedIndexChanged;
            // 
            // lblCooldownGroup
            // 
            lblCooldownGroup.AutoSize = true;
            lblCooldownGroup.Location = new System.Drawing.Point(14, 207);
            lblCooldownGroup.Margin = new Padding(4, 0, 4, 0);
            lblCooldownGroup.Name = "lblCooldownGroup";
            lblCooldownGroup.Size = new Size(101, 15);
            lblCooldownGroup.TabIndex = 53;
            lblCooldownGroup.Text = "Cooldown Group:";
            // 
            // nudCooldownDuration
            // 
            nudCooldownDuration.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCooldownDuration.ForeColor = System.Drawing.Color.Gainsboro;
            nudCooldownDuration.Location = new System.Drawing.Point(16, 178);
            nudCooldownDuration.Margin = new Padding(4, 3, 4, 3);
            nudCooldownDuration.Maximum = new decimal(new int[] { -100, 49, 0, 0 });
            nudCooldownDuration.Name = "nudCooldownDuration";
            nudCooldownDuration.Size = new Size(234, 23);
            nudCooldownDuration.TabIndex = 39;
            nudCooldownDuration.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCooldownDuration.ValueChanged += nudCooldownDuration_ValueChanged;
            // 
            // nudCastDuration
            // 
            nudCastDuration.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCastDuration.ForeColor = System.Drawing.Color.Gainsboro;
            nudCastDuration.Location = new System.Drawing.Point(16, 133);
            nudCastDuration.Margin = new Padding(4, 3, 4, 3);
            nudCastDuration.Maximum = new decimal(new int[] { -100, 49, 0, 0 });
            nudCastDuration.Name = "nudCastDuration";
            nudCastDuration.Size = new Size(234, 23);
            nudCastDuration.TabIndex = 38;
            nudCastDuration.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCastDuration.ValueChanged += nudCastDuration_ValueChanged;
            // 
            // nudMpCost
            // 
            nudMpCost.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMpCost.ForeColor = System.Drawing.Color.Gainsboro;
            nudMpCost.Location = new System.Drawing.Point(18, 82);
            nudMpCost.Margin = new Padding(4, 3, 4, 3);
            nudMpCost.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudMpCost.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            nudMpCost.Name = "nudMpCost";
            nudMpCost.Size = new Size(233, 23);
            nudMpCost.TabIndex = 37;
            nudMpCost.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMpCost.ValueChanged += nudMpCost_ValueChanged;
            // 
            // nudHPCost
            // 
            nudHPCost.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudHPCost.ForeColor = System.Drawing.Color.Gainsboro;
            nudHPCost.Location = new System.Drawing.Point(18, 37);
            nudHPCost.Margin = new Padding(4, 3, 4, 3);
            nudHPCost.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudHPCost.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            nudHPCost.Name = "nudHPCost";
            nudHPCost.Size = new Size(233, 23);
            nudHPCost.TabIndex = 36;
            nudHPCost.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudHPCost.ValueChanged += nudHPCost_ValueChanged;
            // 
            // lblMPCost
            // 
            lblMPCost.AutoSize = true;
            lblMPCost.Location = new System.Drawing.Point(13, 63);
            lblMPCost.Margin = new Padding(4, 0, 4, 0);
            lblMPCost.Name = "lblMPCost";
            lblMPCost.Size = new Size(67, 15);
            lblMPCost.TabIndex = 23;
            lblMPCost.Text = "Mana Cost:";
            // 
            // lblHPCost
            // 
            lblHPCost.AutoSize = true;
            lblHPCost.Location = new System.Drawing.Point(13, 18);
            lblHPCost.Margin = new Padding(4, 0, 4, 0);
            lblHPCost.Name = "lblHPCost";
            lblHPCost.Size = new Size(53, 15);
            lblHPCost.TabIndex = 22;
            lblHPCost.Text = "HP Cost:";
            // 
            // lblCastDuration
            // 
            lblCastDuration.AutoSize = true;
            lblCastDuration.Location = new System.Drawing.Point(13, 114);
            lblCastDuration.Margin = new Padding(4, 0, 4, 0);
            lblCastDuration.Name = "lblCastDuration";
            lblCastDuration.Size = new Size(89, 15);
            lblCastDuration.TabIndex = 7;
            lblCastDuration.Text = "Cast Time (ms):";
            // 
            // lblCooldownDuration
            // 
            lblCooldownDuration.AutoSize = true;
            lblCooldownDuration.Location = new System.Drawing.Point(13, 159);
            lblCooldownDuration.Margin = new Padding(4, 0, 4, 0);
            lblCooldownDuration.Name = "lblCooldownDuration";
            lblCooldownDuration.Size = new Size(92, 15);
            lblCooldownDuration.TabIndex = 12;
            lblCooldownDuration.Text = "Cooldown (ms):";
            // 
            // grpRequirements
            // 
            grpRequirements.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpRequirements.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpRequirements.Controls.Add(lblCannotCast);
            grpRequirements.Controls.Add(txtCannotCast);
            grpRequirements.Controls.Add(btnDynamicRequirements);
            grpRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            grpRequirements.Location = new System.Drawing.Point(9, 502);
            grpRequirements.Margin = new Padding(4, 3, 4, 3);
            grpRequirements.Name = "grpRequirements";
            grpRequirements.Padding = new Padding(4, 3, 4, 3);
            grpRequirements.Size = new Size(315, 111);
            grpRequirements.TabIndex = 18;
            grpRequirements.TabStop = false;
            grpRequirements.Text = "Casting Requirements";
            // 
            // lblCannotCast
            // 
            lblCannotCast.AutoSize = true;
            lblCannotCast.Location = new System.Drawing.Point(9, 57);
            lblCannotCast.Margin = new Padding(4, 0, 4, 0);
            lblCannotCast.Name = "lblCannotCast";
            lblCannotCast.Size = new Size(124, 15);
            lblCannotCast.TabIndex = 56;
            lblCannotCast.Text = "Cannot Cast Message:";
            // 
            // txtCannotCast
            // 
            txtCannotCast.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtCannotCast.BorderStyle = BorderStyle.FixedSingle;
            txtCannotCast.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtCannotCast.Location = new System.Drawing.Point(13, 77);
            txtCannotCast.Margin = new Padding(4, 3, 4, 3);
            txtCannotCast.Name = "txtCannotCast";
            txtCannotCast.Size = new Size(291, 23);
            txtCannotCast.TabIndex = 55;
            txtCannotCast.TextChanged += txtCannotCast_TextChanged;
            // 
            // btnDynamicRequirements
            // 
            btnDynamicRequirements.Location = new System.Drawing.Point(13, 21);
            btnDynamicRequirements.Margin = new Padding(4, 3, 4, 3);
            btnDynamicRequirements.Name = "btnDynamicRequirements";
            btnDynamicRequirements.Padding = new Padding(6);
            btnDynamicRequirements.Size = new Size(292, 27);
            btnDynamicRequirements.TabIndex = 20;
            btnDynamicRequirements.Text = "Casting Requirements";
            btnDynamicRequirements.Click += btnDynamicRequirements_Click;
            // 
            // grpTargetInfo
            // 
            grpTargetInfo.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpTargetInfo.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpTargetInfo.Controls.Add(nudDuration);
            grpTargetInfo.Controls.Add(lblDuration);
            grpTargetInfo.Controls.Add(nudHitRadius);
            grpTargetInfo.Controls.Add(lblHitRadius);
            grpTargetInfo.Controls.Add(cmbTargetType);
            grpTargetInfo.Controls.Add(lblCastRange);
            grpTargetInfo.Controls.Add(lblTargetType);
            grpTargetInfo.Controls.Add(nudCastRange);
            grpTargetInfo.Controls.Add(lblProjectile);
            grpTargetInfo.Controls.Add(cmbProjectile);
            grpTargetInfo.ForeColor = System.Drawing.Color.Gainsboro;
            grpTargetInfo.Location = new System.Drawing.Point(334, 343);
            grpTargetInfo.Margin = new Padding(4, 3, 4, 3);
            grpTargetInfo.Name = "grpTargetInfo";
            grpTargetInfo.Padding = new Padding(4, 3, 4, 3);
            grpTargetInfo.Size = new Size(262, 256);
            grpTargetInfo.TabIndex = 19;
            grpTargetInfo.TabStop = false;
            grpTargetInfo.Text = "Targetting Info";
            grpTargetInfo.Visible = false;
            // 
            // nudDuration
            // 
            nudDuration.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudDuration.ForeColor = System.Drawing.Color.Gainsboro;
            nudDuration.Location = new System.Drawing.Point(18, 183);
            nudDuration.Margin = new Padding(4, 3, 4, 3);
            nudDuration.Maximum = new decimal(new int[] { -100, 49, 0, 0 });
            nudDuration.Name = "nudDuration";
            nudDuration.Size = new Size(233, 23);
            nudDuration.TabIndex = 38;
            nudDuration.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudDuration.ValueChanged += nudOnHitDuration_ValueChanged;
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.Location = new System.Drawing.Point(9, 165);
            lblDuration.Margin = new Padding(4, 0, 4, 0);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(83, 15);
            lblDuration.TabIndex = 37;
            lblDuration.Text = "Duration (ms):";
            // 
            // nudHitRadius
            // 
            nudHitRadius.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudHitRadius.ForeColor = System.Drawing.Color.Gainsboro;
            nudHitRadius.Location = new System.Drawing.Point(18, 136);
            nudHitRadius.Margin = new Padding(4, 3, 4, 3);
            nudHitRadius.Name = "nudHitRadius";
            nudHitRadius.Size = new Size(233, 23);
            nudHitRadius.TabIndex = 35;
            nudHitRadius.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudHitRadius.ValueChanged += nudHitRadius_ValueChanged;
            // 
            // lblHitRadius
            // 
            lblHitRadius.AutoSize = true;
            lblHitRadius.Location = new System.Drawing.Point(7, 118);
            lblHitRadius.Margin = new Padding(4, 0, 4, 0);
            lblHitRadius.Name = "lblHitRadius";
            lblHitRadius.Size = new Size(64, 15);
            lblHitRadius.TabIndex = 16;
            lblHitRadius.Text = "Hit Radius:";
            // 
            // cmbTargetType
            // 
            cmbTargetType.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbTargetType.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbTargetType.BorderStyle = ButtonBorderStyle.Solid;
            cmbTargetType.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbTargetType.DrawDropdownHoverOutline = false;
            cmbTargetType.DrawFocusRectangle = false;
            cmbTargetType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTargetType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTargetType.FlatStyle = FlatStyle.Flat;
            cmbTargetType.ForeColor = System.Drawing.Color.Gainsboro;
            cmbTargetType.FormattingEnabled = true;
            cmbTargetType.Items.AddRange(new object[] { "Self", "Single Target (includes self)", "AOE", "Linear (projectile)", "On Hit", "Trap" });
            cmbTargetType.Location = new System.Drawing.Point(18, 37);
            cmbTargetType.Margin = new Padding(4, 3, 4, 3);
            cmbTargetType.Name = "cmbTargetType";
            cmbTargetType.Size = new Size(233, 24);
            cmbTargetType.TabIndex = 15;
            cmbTargetType.Text = "Self";
            cmbTargetType.TextPadding = new Padding(2);
            cmbTargetType.SelectedIndexChanged += cmbTargetType_SelectedIndexChanged;
            // 
            // lblCastRange
            // 
            lblCastRange.AutoSize = true;
            lblCastRange.Location = new System.Drawing.Point(7, 69);
            lblCastRange.Margin = new Padding(4, 0, 4, 0);
            lblCastRange.Name = "lblCastRange";
            lblCastRange.Size = new Size(69, 15);
            lblCastRange.TabIndex = 13;
            lblCastRange.Text = "Cast Range:";
            // 
            // lblTargetType
            // 
            lblTargetType.AutoSize = true;
            lblTargetType.Location = new System.Drawing.Point(7, 18);
            lblTargetType.Margin = new Padding(4, 0, 4, 0);
            lblTargetType.Name = "lblTargetType";
            lblTargetType.Size = new Size(69, 15);
            lblTargetType.TabIndex = 12;
            lblTargetType.Text = "Target Type:";
            // 
            // nudCastRange
            // 
            nudCastRange.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCastRange.ForeColor = System.Drawing.Color.Gainsboro;
            nudCastRange.Location = new System.Drawing.Point(18, 88);
            nudCastRange.Margin = new Padding(4, 3, 4, 3);
            nudCastRange.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudCastRange.Name = "nudCastRange";
            nudCastRange.Size = new Size(233, 23);
            nudCastRange.TabIndex = 36;
            nudCastRange.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCastRange.ValueChanged += nudCastRange_ValueChanged;
            // 
            // lblProjectile
            // 
            lblProjectile.AutoSize = true;
            lblProjectile.Location = new System.Drawing.Point(7, 68);
            lblProjectile.Margin = new Padding(4, 0, 4, 0);
            lblProjectile.Name = "lblProjectile";
            lblProjectile.Size = new Size(59, 15);
            lblProjectile.TabIndex = 18;
            lblProjectile.Text = "Projectile:";
            // 
            // cmbProjectile
            // 
            cmbProjectile.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbProjectile.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbProjectile.BorderStyle = ButtonBorderStyle.Solid;
            cmbProjectile.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbProjectile.DrawDropdownHoverOutline = false;
            cmbProjectile.DrawFocusRectangle = false;
            cmbProjectile.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProjectile.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProjectile.FlatStyle = FlatStyle.Flat;
            cmbProjectile.ForeColor = System.Drawing.Color.Gainsboro;
            cmbProjectile.FormattingEnabled = true;
            cmbProjectile.Location = new System.Drawing.Point(18, 87);
            cmbProjectile.Margin = new Padding(4, 3, 4, 3);
            cmbProjectile.Name = "cmbProjectile";
            cmbProjectile.Size = new Size(233, 24);
            cmbProjectile.TabIndex = 19;
            cmbProjectile.Text = null;
            cmbProjectile.TextPadding = new Padding(2);
            cmbProjectile.Visible = false;
            cmbProjectile.SelectedIndexChanged += cmbProjectile_SelectedIndexChanged;
            // 
            // grpCombat
            // 
            grpCombat.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpCombat.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpCombat.Controls.Add(grpStats);
            grpCombat.Controls.Add(grpHotDot);
            grpCombat.Controls.Add(grpEffect);
            grpCombat.Controls.Add(grpEffectDuration);
            grpCombat.Controls.Add(grpDamage);
            grpCombat.ForeColor = System.Drawing.Color.Gainsboro;
            grpCombat.Location = new System.Drawing.Point(603, 222);
            grpCombat.Margin = new Padding(4, 3, 4, 3);
            grpCombat.Name = "grpCombat";
            grpCombat.Padding = new Padding(4, 3, 4, 3);
            grpCombat.Size = new Size(542, 683);
            grpCombat.TabIndex = 39;
            grpCombat.TabStop = false;
            grpCombat.Text = "Combat Spell";
            grpCombat.Visible = false;
            // 
            // grpStats
            // 
            grpStats.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpStats.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpStats.Controls.Add(label3);
            grpStats.Controls.Add(label4);
            grpStats.Controls.Add(label5);
            grpStats.Controls.Add(nudCurPercentage);
            grpStats.Controls.Add(nudDmgPercentage);
            grpStats.Controls.Add(nudAgiPercentage);
            grpStats.Controls.Add(label6);
            grpStats.Controls.Add(label7);
            grpStats.Controls.Add(label8);
            grpStats.Controls.Add(nudCur);
            grpStats.Controls.Add(nudDmg);
            grpStats.Controls.Add(nudAgi);
            grpStats.Controls.Add(Curlabel);
            grpStats.Controls.Add(dmgLabel);
            grpStats.Controls.Add(label11);
            grpStats.Controls.Add(lblPercentage5);
            grpStats.Controls.Add(lblPercentage4);
            grpStats.Controls.Add(lblPercentage3);
            grpStats.Controls.Add(lblPercentage2);
            grpStats.Controls.Add(lblPercentage1);
            grpStats.Controls.Add(nudSpdPercentage);
            grpStats.Controls.Add(nudMRPercentage);
            grpStats.Controls.Add(nudDefPercentage);
            grpStats.Controls.Add(nudMagPercentage);
            grpStats.Controls.Add(nudStrPercentage);
            grpStats.Controls.Add(lblPlus5);
            grpStats.Controls.Add(lblPlus4);
            grpStats.Controls.Add(lblPlus3);
            grpStats.Controls.Add(lblPlus2);
            grpStats.Controls.Add(lblPlus1);
            grpStats.Controls.Add(nudSpd);
            grpStats.Controls.Add(nudMR);
            grpStats.Controls.Add(nudDef);
            grpStats.Controls.Add(nudMag);
            grpStats.Controls.Add(nudStr);
            grpStats.Controls.Add(lblSpd);
            grpStats.Controls.Add(lblMR);
            grpStats.Controls.Add(lblDef);
            grpStats.Controls.Add(lblMag);
            grpStats.Controls.Add(lblStr);
            grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            grpStats.Location = new System.Drawing.Point(261, 20);
            grpStats.Margin = new Padding(4, 3, 4, 3);
            grpStats.Name = "grpStats";
            grpStats.Padding = new Padding(4, 3, 4, 3);
            grpStats.Size = new Size(276, 284);
            grpStats.TabIndex = 50;
            grpStats.TabStop = false;
            grpStats.Text = "Stat Modifiers";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(246, 240);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 112;
            label3.Text = "%";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(247, 211);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(17, 15);
            label4.TabIndex = 111;
            label4.Text = "%";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(246, 181);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(17, 15);
            label5.TabIndex = 110;
            label5.Text = "%";
            // 
            // nudCurPercentage
            // 
            nudCurPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCurPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            nudCurPercentage.Location = new System.Drawing.Point(192, 238);
            nudCurPercentage.Margin = new Padding(4, 3, 4, 3);
            nudCurPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCurPercentage.Minimum = new decimal(new int[] { -100, 0, 0, int.MinValue });
            nudCurPercentage.Name = "nudCurPercentage";
            nudCurPercentage.Size = new Size(50, 23);
            nudCurPercentage.TabIndex = 109;
            nudCurPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCurPercentage.ValueChanged += nudCurPercentage_ValueChanged;
            // 
            // nudDmgPercentage
            // 
            nudDmgPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudDmgPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            nudDmgPercentage.Location = new System.Drawing.Point(193, 209);
            nudDmgPercentage.Margin = new Padding(4, 3, 4, 3);
            nudDmgPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudDmgPercentage.Minimum = new decimal(new int[] { -100, 0, 0, int.MinValue });
            nudDmgPercentage.Name = "nudDmgPercentage";
            nudDmgPercentage.Size = new Size(50, 23);
            nudDmgPercentage.TabIndex = 108;
            nudDmgPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudDmgPercentage.ValueChanged += nudDmgPercentage_ValueChanged;
            // 
            // nudAgiPercentage
            // 
            nudAgiPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAgiPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            nudAgiPercentage.Location = new System.Drawing.Point(192, 179);
            nudAgiPercentage.Margin = new Padding(4, 3, 4, 3);
            nudAgiPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAgiPercentage.Minimum = new decimal(new int[] { -100, 0, 0, int.MinValue });
            nudAgiPercentage.Name = "nudAgiPercentage";
            nudAgiPercentage.Size = new Size(50, 23);
            nudAgiPercentage.TabIndex = 107;
            nudAgiPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudAgiPercentage.ValueChanged += nudAgiPercentage_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(170, 239);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(15, 15);
            label6.TabIndex = 106;
            label6.Text = "+";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(171, 210);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(15, 15);
            label7.TabIndex = 105;
            label7.Text = "+";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(172, 180);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(15, 15);
            label8.TabIndex = 104;
            label8.Text = "+";
            // 
            // nudCur
            // 
            nudCur.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCur.ForeColor = System.Drawing.Color.Gainsboro;
            nudCur.Location = new System.Drawing.Point(96, 238);
            nudCur.Margin = new Padding(4, 3, 4, 3);
            nudCur.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudCur.Name = "nudCur";
            nudCur.Size = new Size(70, 23);
            nudCur.TabIndex = 103;
            nudCur.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCur.ValueChanged += nudCur_ValueChanged;
            // 
            // nudDmg
            // 
            nudDmg.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudDmg.ForeColor = System.Drawing.Color.Gainsboro;
            nudDmg.Location = new System.Drawing.Point(96, 209);
            nudDmg.Margin = new Padding(4, 3, 4, 3);
            nudDmg.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudDmg.Name = "nudDmg";
            nudDmg.Size = new Size(70, 23);
            nudDmg.TabIndex = 102;
            nudDmg.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudDmg.ValueChanged += nudDmg_ValueChanged;
            // 
            // nudAgi
            // 
            nudAgi.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAgi.ForeColor = System.Drawing.Color.Gainsboro;
            nudAgi.Location = new System.Drawing.Point(96, 180);
            nudAgi.Margin = new Padding(4, 3, 4, 3);
            nudAgi.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudAgi.Name = "nudAgi";
            nudAgi.Size = new Size(70, 23);
            nudAgi.TabIndex = 101;
            nudAgi.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudAgi.ValueChanged += nudAgi_ValueChanged;
            // 
            // Curlabel
            // 
            Curlabel.AutoSize = true;
            Curlabel.Location = new System.Drawing.Point(6, 242);
            Curlabel.Margin = new Padding(2, 0, 2, 0);
            Curlabel.Name = "Curlabel";
            Curlabel.Size = new Size(40, 15);
            Curlabel.TabIndex = 100;
            Curlabel.Text = "Curas:";
            // 
            // dmgLabel
            // 
            dmgLabel.AutoSize = true;
            dmgLabel.Location = new System.Drawing.Point(7, 211);
            dmgLabel.Margin = new Padding(2, 0, 2, 0);
            dmgLabel.Name = "dmgLabel";
            dmgLabel.Size = new Size(43, 15);
            dmgLabel.TabIndex = 99;
            dmgLabel.Text = "Daños:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(7, 181);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(41, 15);
            label11.TabIndex = 98;
            label11.Text = "Agility";
            // 
            // lblPercentage5
            // 
            lblPercentage5.AutoSize = true;
            lblPercentage5.Location = new System.Drawing.Point(247, 152);
            lblPercentage5.Margin = new Padding(2, 0, 2, 0);
            lblPercentage5.Name = "lblPercentage5";
            lblPercentage5.Size = new Size(17, 15);
            lblPercentage5.TabIndex = 67;
            lblPercentage5.Text = "%";
            // 
            // lblPercentage4
            // 
            lblPercentage4.AutoSize = true;
            lblPercentage4.Location = new System.Drawing.Point(247, 120);
            lblPercentage4.Margin = new Padding(2, 0, 2, 0);
            lblPercentage4.Name = "lblPercentage4";
            lblPercentage4.Size = new Size(17, 15);
            lblPercentage4.TabIndex = 66;
            lblPercentage4.Text = "%";
            // 
            // lblPercentage3
            // 
            lblPercentage3.AutoSize = true;
            lblPercentage3.Location = new System.Drawing.Point(247, 88);
            lblPercentage3.Margin = new Padding(2, 0, 2, 0);
            lblPercentage3.Name = "lblPercentage3";
            lblPercentage3.Size = new Size(17, 15);
            lblPercentage3.TabIndex = 65;
            lblPercentage3.Text = "%";
            // 
            // lblPercentage2
            // 
            lblPercentage2.AutoSize = true;
            lblPercentage2.Location = new System.Drawing.Point(247, 55);
            lblPercentage2.Margin = new Padding(2, 0, 2, 0);
            lblPercentage2.Name = "lblPercentage2";
            lblPercentage2.Size = new Size(17, 15);
            lblPercentage2.TabIndex = 64;
            lblPercentage2.Text = "%";
            // 
            // lblPercentage1
            // 
            lblPercentage1.AutoSize = true;
            lblPercentage1.Location = new System.Drawing.Point(247, 24);
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
            nudSpdPercentage.Location = new System.Drawing.Point(192, 149);
            nudSpdPercentage.Margin = new Padding(4, 3, 4, 3);
            nudSpdPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSpdPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudSpdPercentage.Name = "nudSpdPercentage";
            nudSpdPercentage.Size = new Size(50, 23);
            nudSpdPercentage.TabIndex = 62;
            nudSpdPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudSpdPercentage.ValueChanged += nudSpdPercentage_ValueChanged;
            // 
            // nudMRPercentage
            // 
            nudMRPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMRPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            nudMRPercentage.Location = new System.Drawing.Point(192, 118);
            nudMRPercentage.Margin = new Padding(4, 3, 4, 3);
            nudMRPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudMRPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudMRPercentage.Name = "nudMRPercentage";
            nudMRPercentage.Size = new Size(50, 23);
            nudMRPercentage.TabIndex = 61;
            nudMRPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMRPercentage.ValueChanged += nudMRPercentage_ValueChanged;
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
            // nudMagPercentage
            // 
            nudMagPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMagPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            nudMagPercentage.Location = new System.Drawing.Point(192, 54);
            nudMagPercentage.Margin = new Padding(4, 3, 4, 3);
            nudMagPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudMagPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudMagPercentage.Name = "nudMagPercentage";
            nudMagPercentage.Size = new Size(50, 23);
            nudMagPercentage.TabIndex = 59;
            nudMagPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMagPercentage.ValueChanged += nudMagPercentage_ValueChanged;
            // 
            // nudStrPercentage
            // 
            nudStrPercentage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudStrPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            nudStrPercentage.Location = new System.Drawing.Point(192, 22);
            nudStrPercentage.Margin = new Padding(4, 3, 4, 3);
            nudStrPercentage.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudStrPercentage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudStrPercentage.Name = "nudStrPercentage";
            nudStrPercentage.Size = new Size(50, 23);
            nudStrPercentage.TabIndex = 58;
            nudStrPercentage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudStrPercentage.ValueChanged += nudStrPercentage_ValueChanged;
            // 
            // lblPlus5
            // 
            lblPlus5.AutoSize = true;
            lblPlus5.Location = new System.Drawing.Point(172, 152);
            lblPlus5.Margin = new Padding(2, 0, 2, 0);
            lblPlus5.Name = "lblPlus5";
            lblPlus5.Size = new Size(15, 15);
            lblPlus5.TabIndex = 57;
            lblPlus5.Text = "+";
            // 
            // lblPlus4
            // 
            lblPlus4.AutoSize = true;
            lblPlus4.Location = new System.Drawing.Point(172, 120);
            lblPlus4.Margin = new Padding(2, 0, 2, 0);
            lblPlus4.Name = "lblPlus4";
            lblPlus4.Size = new Size(15, 15);
            lblPlus4.TabIndex = 56;
            lblPlus4.Text = "+";
            // 
            // lblPlus3
            // 
            lblPlus3.AutoSize = true;
            lblPlus3.Location = new System.Drawing.Point(172, 88);
            lblPlus3.Margin = new Padding(2, 0, 2, 0);
            lblPlus3.Name = "lblPlus3";
            lblPlus3.Size = new Size(15, 15);
            lblPlus3.TabIndex = 55;
            lblPlus3.Text = "+";
            // 
            // lblPlus2
            // 
            lblPlus2.AutoSize = true;
            lblPlus2.Location = new System.Drawing.Point(172, 55);
            lblPlus2.Margin = new Padding(2, 0, 2, 0);
            lblPlus2.Name = "lblPlus2";
            lblPlus2.Size = new Size(15, 15);
            lblPlus2.TabIndex = 54;
            lblPlus2.Text = "+";
            // 
            // lblPlus1
            // 
            lblPlus1.AutoSize = true;
            lblPlus1.Location = new System.Drawing.Point(172, 24);
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
            nudSpd.Location = new System.Drawing.Point(96, 149);
            nudSpd.Margin = new Padding(4, 3, 4, 3);
            nudSpd.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudSpd.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            nudSpd.Name = "nudSpd";
            nudSpd.Size = new Size(70, 23);
            nudSpd.TabIndex = 52;
            nudSpd.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudSpd.ValueChanged += nudSpd_ValueChanged;
            // 
            // nudMR
            // 
            nudMR.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMR.ForeColor = System.Drawing.Color.Gainsboro;
            nudMR.Location = new System.Drawing.Point(96, 118);
            nudMR.Margin = new Padding(4, 3, 4, 3);
            nudMR.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMR.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            nudMR.Name = "nudMR";
            nudMR.Size = new Size(70, 23);
            nudMR.TabIndex = 51;
            nudMR.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMR.ValueChanged += nudMR_ValueChanged;
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
            // nudMag
            // 
            nudMag.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMag.ForeColor = System.Drawing.Color.Gainsboro;
            nudMag.Location = new System.Drawing.Point(96, 54);
            nudMag.Margin = new Padding(4, 3, 4, 3);
            nudMag.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMag.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            nudMag.Name = "nudMag";
            nudMag.Size = new Size(70, 23);
            nudMag.TabIndex = 49;
            nudMag.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMag.ValueChanged += nudMag_ValueChanged;
            // 
            // nudStr
            // 
            nudStr.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudStr.ForeColor = System.Drawing.Color.Gainsboro;
            nudStr.Location = new System.Drawing.Point(96, 22);
            nudStr.Margin = new Padding(4, 3, 4, 3);
            nudStr.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudStr.Minimum = new decimal(new int[] { 255, 0, 0, int.MinValue });
            nudStr.Name = "nudStr";
            nudStr.Size = new Size(70, 23);
            nudStr.TabIndex = 48;
            nudStr.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudStr.ValueChanged += nudStr_ValueChanged;
            // 
            // lblSpd
            // 
            lblSpd.AutoSize = true;
            lblSpd.Location = new System.Drawing.Point(6, 154);
            lblSpd.Margin = new Padding(2, 0, 2, 0);
            lblSpd.Name = "lblSpd";
            lblSpd.Size = new Size(75, 15);
            lblSpd.TabIndex = 47;
            lblSpd.Text = "Move Speed:";
            // 
            // lblMR
            // 
            lblMR.AutoSize = true;
            lblMR.Location = new System.Drawing.Point(7, 123);
            lblMR.Margin = new Padding(2, 0, 2, 0);
            lblMR.Name = "lblMR";
            lblMR.Size = new Size(46, 15);
            lblMR.TabIndex = 46;
            lblMR.Text = "Vitality:";
            // 
            // lblDef
            // 
            lblDef.AutoSize = true;
            lblDef.Location = new System.Drawing.Point(6, 91);
            lblDef.Margin = new Padding(2, 0, 2, 0);
            lblDef.Name = "lblDef";
            lblDef.Size = new Size(44, 15);
            lblDef.TabIndex = 45;
            lblDef.Text = "Armor:";
            // 
            // lblMag
            // 
            lblMag.AutoSize = true;
            lblMag.Location = new System.Drawing.Point(7, 59);
            lblMag.Margin = new Padding(2, 0, 2, 0);
            lblMag.Name = "lblMag";
            lblMag.Size = new Size(71, 15);
            lblMag.TabIndex = 44;
            lblMag.Text = "Intelligence:";
            // 
            // lblStr
            // 
            lblStr.AutoSize = true;
            lblStr.Location = new System.Drawing.Point(6, 27);
            lblStr.Margin = new Padding(2, 0, 2, 0);
            lblStr.Name = "lblStr";
            lblStr.Size = new Size(55, 15);
            lblStr.TabIndex = 43;
            lblStr.Text = "Strength:";
            // 
            // grpHotDot
            // 
            grpHotDot.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpHotDot.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpHotDot.Controls.Add(lblTickAnimation);
            grpHotDot.Controls.Add(cmbTickAnimation);
            grpHotDot.Controls.Add(nudTick);
            grpHotDot.Controls.Add(chkHOTDOT);
            grpHotDot.Controls.Add(lblTick);
            grpHotDot.ForeColor = System.Drawing.Color.Gainsboro;
            grpHotDot.Location = new System.Drawing.Point(7, 455);
            grpHotDot.Margin = new Padding(4, 3, 4, 3);
            grpHotDot.Name = "grpHotDot";
            grpHotDot.Padding = new Padding(4, 3, 4, 3);
            grpHotDot.Size = new Size(246, 128);
            grpHotDot.TabIndex = 53;
            grpHotDot.TabStop = false;
            grpHotDot.Text = "Heal/Damage Over Time";
            // 
            // lblTickAnimation
            // 
            lblTickAnimation.AutoSize = true;
            lblTickAnimation.Location = new System.Drawing.Point(7, 95);
            lblTickAnimation.Margin = new Padding(4, 0, 4, 0);
            lblTickAnimation.Name = "lblTickAnimation";
            lblTickAnimation.Size = new Size(90, 15);
            lblTickAnimation.TabIndex = 56;
            lblTickAnimation.Text = "Tick Animation:";
            // 
            // cmbTickAnimation
            // 
            cmbTickAnimation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbTickAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbTickAnimation.BorderStyle = ButtonBorderStyle.Solid;
            cmbTickAnimation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbTickAnimation.DrawDropdownHoverOutline = false;
            cmbTickAnimation.DrawFocusRectangle = false;
            cmbTickAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTickAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTickAnimation.FlatStyle = FlatStyle.Flat;
            cmbTickAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            cmbTickAnimation.FormattingEnabled = true;
            cmbTickAnimation.Location = new System.Drawing.Point(100, 91);
            cmbTickAnimation.Margin = new Padding(4, 3, 4, 3);
            cmbTickAnimation.Name = "cmbTickAnimation";
            cmbTickAnimation.Size = new Size(131, 24);
            cmbTickAnimation.TabIndex = 54;
            cmbTickAnimation.Text = null;
            cmbTickAnimation.TextPadding = new Padding(2);
            cmbTickAnimation.SelectedIndexChanged += cmbTickAnimation_SelectedIndexChanged;
            // 
            // nudTick
            // 
            nudTick.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudTick.ForeColor = System.Drawing.Color.Gainsboro;
            nudTick.Location = new System.Drawing.Point(100, 55);
            nudTick.Margin = new Padding(4, 3, 4, 3);
            nudTick.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            nudTick.Name = "nudTick";
            nudTick.Size = new Size(132, 23);
            nudTick.TabIndex = 40;
            nudTick.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudTick.ValueChanged += nudTick_ValueChanged;
            // 
            // chkHOTDOT
            // 
            chkHOTDOT.Location = new System.Drawing.Point(10, 18);
            chkHOTDOT.Margin = new Padding(4, 3, 4, 3);
            chkHOTDOT.Name = "chkHOTDOT";
            chkHOTDOT.RightToLeft = RightToLeft.No;
            chkHOTDOT.Size = new Size(100, 28);
            chkHOTDOT.TabIndex = 22;
            chkHOTDOT.Text = "HOT/DOT?";
            chkHOTDOT.CheckedChanged += chkHOTDOT_CheckedChanged;
            // 
            // lblTick
            // 
            lblTick.AutoSize = true;
            lblTick.Location = new System.Drawing.Point(7, 58);
            lblTick.Margin = new Padding(4, 0, 4, 0);
            lblTick.Name = "lblTick";
            lblTick.Size = new Size(58, 15);
            lblTick.TabIndex = 38;
            lblTick.Text = "Tick (ms):";
            // 
            // grpEffect
            // 
            grpEffect.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpEffect.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpEffect.Controls.Add(lblEffect);
            grpEffect.Controls.Add(cmbExtraEffect);
            grpEffect.Controls.Add(picSprite);
            grpEffect.Controls.Add(cmbTransform);
            grpEffect.Controls.Add(lblSprite);
            grpEffect.ForeColor = System.Drawing.Color.Gainsboro;
            grpEffect.Location = new System.Drawing.Point(261, 370);
            grpEffect.Margin = new Padding(4, 3, 4, 3);
            grpEffect.Name = "grpEffect";
            grpEffect.Padding = new Padding(4, 3, 4, 3);
            grpEffect.Size = new Size(276, 260);
            grpEffect.TabIndex = 52;
            grpEffect.TabStop = false;
            grpEffect.Text = "Effect";
            // 
            // lblEffect
            // 
            lblEffect.AutoSize = true;
            lblEffect.Location = new System.Drawing.Point(5, 17);
            lblEffect.Margin = new Padding(4, 0, 4, 0);
            lblEffect.Name = "lblEffect";
            lblEffect.Size = new Size(69, 15);
            lblEffect.TabIndex = 35;
            lblEffect.Text = "Extra Effect:";
            // 
            // cmbExtraEffect
            // 
            cmbExtraEffect.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbExtraEffect.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbExtraEffect.BorderStyle = ButtonBorderStyle.Solid;
            cmbExtraEffect.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbExtraEffect.DrawDropdownHoverOutline = false;
            cmbExtraEffect.DrawFocusRectangle = false;
            cmbExtraEffect.DrawMode = DrawMode.OwnerDrawFixed;
            cmbExtraEffect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExtraEffect.FlatStyle = FlatStyle.Flat;
            cmbExtraEffect.ForeColor = System.Drawing.Color.Gainsboro;
            cmbExtraEffect.FormattingEnabled = true;
            cmbExtraEffect.Items.AddRange(new object[] { "None", "Silence", "Stun", "Snare", "Blind", "Stealth", "Transform", "Cleanse", "Invulnerable", "Shield", "Sleep", "On Hit", "Taunt" });
            cmbExtraEffect.Location = new System.Drawing.Point(6, 36);
            cmbExtraEffect.Margin = new Padding(4, 3, 4, 3);
            cmbExtraEffect.Name = "cmbExtraEffect";
            cmbExtraEffect.Size = new Size(93, 24);
            cmbExtraEffect.TabIndex = 36;
            cmbExtraEffect.Text = "None";
            cmbExtraEffect.TextPadding = new Padding(2);
            cmbExtraEffect.SelectedIndexChanged += cmbExtraEffect_SelectedIndexChanged;
            // 
            // picSprite
            // 
            picSprite.BackColor = System.Drawing.Color.Black;
            picSprite.BackgroundImageLayout = ImageLayout.None;
            picSprite.Location = new System.Drawing.Point(7, 70);
            picSprite.Margin = new Padding(4, 3, 4, 3);
            picSprite.Name = "picSprite";
            picSprite.Size = new Size(259, 179);
            picSprite.TabIndex = 43;
            picSprite.TabStop = false;
            // 
            // cmbTransform
            // 
            cmbTransform.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbTransform.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbTransform.BorderStyle = ButtonBorderStyle.Solid;
            cmbTransform.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbTransform.DrawDropdownHoverOutline = false;
            cmbTransform.DrawFocusRectangle = false;
            cmbTransform.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTransform.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTransform.FlatStyle = FlatStyle.Flat;
            cmbTransform.ForeColor = System.Drawing.Color.Gainsboro;
            cmbTransform.FormattingEnabled = true;
            cmbTransform.Items.AddRange(new object[] { "None" });
            cmbTransform.Location = new System.Drawing.Point(160, 36);
            cmbTransform.Margin = new Padding(4, 3, 4, 3);
            cmbTransform.Name = "cmbTransform";
            cmbTransform.Size = new Size(93, 24);
            cmbTransform.TabIndex = 44;
            cmbTransform.Text = "None";
            cmbTransform.TextPadding = new Padding(2);
            cmbTransform.SelectedIndexChanged += cmbTransform_SelectedIndexChanged;
            // 
            // lblSprite
            // 
            lblSprite.AutoSize = true;
            lblSprite.Location = new System.Drawing.Point(156, 17);
            lblSprite.Margin = new Padding(4, 0, 4, 0);
            lblSprite.Name = "lblSprite";
            lblSprite.Size = new Size(40, 15);
            lblSprite.TabIndex = 40;
            lblSprite.Text = "Sprite:";
            // 
            // grpEffectDuration
            // 
            grpEffectDuration.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpEffectDuration.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpEffectDuration.Controls.Add(nudBuffDuration);
            grpEffectDuration.Controls.Add(lblBuffDuration);
            grpEffectDuration.ForeColor = System.Drawing.Color.Gainsboro;
            grpEffectDuration.Location = new System.Drawing.Point(261, 312);
            grpEffectDuration.Margin = new Padding(4, 3, 4, 3);
            grpEffectDuration.Name = "grpEffectDuration";
            grpEffectDuration.Padding = new Padding(4, 3, 4, 3);
            grpEffectDuration.Size = new Size(276, 47);
            grpEffectDuration.TabIndex = 51;
            grpEffectDuration.TabStop = false;
            grpEffectDuration.Text = "Stat Boost/Effect Duration";
            // 
            // nudBuffDuration
            // 
            nudBuffDuration.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudBuffDuration.ForeColor = System.Drawing.Color.Gainsboro;
            nudBuffDuration.Location = new System.Drawing.Point(160, 16);
            nudBuffDuration.Margin = new Padding(4, 3, 4, 3);
            nudBuffDuration.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            nudBuffDuration.Name = "nudBuffDuration";
            nudBuffDuration.Size = new Size(93, 23);
            nudBuffDuration.TabIndex = 39;
            nudBuffDuration.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudBuffDuration.ValueChanged += nudBuffDuration_ValueChanged;
            // 
            // lblBuffDuration
            // 
            lblBuffDuration.AutoSize = true;
            lblBuffDuration.Location = new System.Drawing.Point(7, 18);
            lblBuffDuration.Margin = new Padding(4, 0, 4, 0);
            lblBuffDuration.Name = "lblBuffDuration";
            lblBuffDuration.Size = new Size(83, 15);
            lblBuffDuration.TabIndex = 33;
            lblBuffDuration.Text = "Duration (ms):";
            // 
            // grpDamage
            // 
            grpDamage.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpDamage.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpDamage.Controls.Add(nudCritMultiplier);
            grpDamage.Controls.Add(lblCritMultiplier);
            grpDamage.Controls.Add(nudCritChance);
            grpDamage.Controls.Add(nudScaling);
            grpDamage.Controls.Add(nudMPDamage);
            grpDamage.Controls.Add(nudHPDamage);
            grpDamage.Controls.Add(cmbScalingStat);
            grpDamage.Controls.Add(lblScalingStat);
            grpDamage.Controls.Add(chkFriendly);
            grpDamage.Controls.Add(lblCritChance);
            grpDamage.Controls.Add(lblScaling);
            grpDamage.Controls.Add(cmbDamageType);
            grpDamage.Controls.Add(lblDamageType);
            grpDamage.Controls.Add(lblHPDamage);
            grpDamage.Controls.Add(lblManaDamage);
            grpDamage.ForeColor = System.Drawing.Color.Gainsboro;
            grpDamage.Location = new System.Drawing.Point(7, 20);
            grpDamage.Margin = new Padding(4, 3, 4, 3);
            grpDamage.Name = "grpDamage";
            grpDamage.Padding = new Padding(4, 3, 4, 3);
            grpDamage.Size = new Size(246, 426);
            grpDamage.TabIndex = 49;
            grpDamage.TabStop = false;
            grpDamage.Text = "Damage";
            // 
            // nudCritMultiplier
            // 
            nudCritMultiplier.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCritMultiplier.DecimalPlaces = 2;
            nudCritMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            nudCritMultiplier.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudCritMultiplier.Location = new System.Drawing.Point(10, 392);
            nudCritMultiplier.Margin = new Padding(4, 3, 4, 3);
            nudCritMultiplier.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCritMultiplier.Name = "nudCritMultiplier";
            nudCritMultiplier.Size = new Size(222, 23);
            nudCritMultiplier.TabIndex = 63;
            nudCritMultiplier.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCritMultiplier.ValueChanged += nudCritMultiplier_ValueChanged;
            // 
            // lblCritMultiplier
            // 
            lblCritMultiplier.AutoSize = true;
            lblCritMultiplier.Location = new System.Drawing.Point(7, 376);
            lblCritMultiplier.Margin = new Padding(4, 0, 4, 0);
            lblCritMultiplier.Name = "lblCritMultiplier";
            lblCritMultiplier.Size = new Size(156, 15);
            lblCritMultiplier.TabIndex = 62;
            lblCritMultiplier.Text = "Crit Multiplier (Default 1.5x):";
            // 
            // nudCritChance
            // 
            nudCritChance.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            nudCritChance.Location = new System.Drawing.Point(8, 339);
            nudCritChance.Margin = new Padding(4, 3, 4, 3);
            nudCritChance.Name = "nudCritChance";
            nudCritChance.Size = new Size(224, 23);
            nudCritChance.TabIndex = 61;
            nudCritChance.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCritChance.ValueChanged += nudCritChance_ValueChanged;
            // 
            // nudScaling
            // 
            nudScaling.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudScaling.ForeColor = System.Drawing.Color.Gainsboro;
            nudScaling.Location = new System.Drawing.Point(9, 287);
            nudScaling.Margin = new Padding(4, 3, 4, 3);
            nudScaling.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudScaling.Name = "nudScaling";
            nudScaling.Size = new Size(223, 23);
            nudScaling.TabIndex = 60;
            nudScaling.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudScaling.ValueChanged += nudScaling_ValueChanged;
            // 
            // nudMPDamage
            // 
            nudMPDamage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMPDamage.ForeColor = System.Drawing.Color.Gainsboro;
            nudMPDamage.Location = new System.Drawing.Point(9, 123);
            nudMPDamage.Margin = new Padding(4, 3, 4, 3);
            nudMPDamage.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudMPDamage.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            nudMPDamage.Name = "nudMPDamage";
            nudMPDamage.Size = new Size(223, 23);
            nudMPDamage.TabIndex = 59;
            nudMPDamage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMPDamage.ValueChanged += nudMPDamage_ValueChanged;
            // 
            // nudHPDamage
            // 
            nudHPDamage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudHPDamage.ForeColor = System.Drawing.Color.Gainsboro;
            nudHPDamage.Location = new System.Drawing.Point(9, 72);
            nudHPDamage.Margin = new Padding(4, 3, 4, 3);
            nudHPDamage.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudHPDamage.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            nudHPDamage.Name = "nudHPDamage";
            nudHPDamage.Size = new Size(223, 23);
            nudHPDamage.TabIndex = 58;
            nudHPDamage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudHPDamage.ValueChanged += nudHPDamage_ValueChanged;
            // 
            // cmbScalingStat
            // 
            cmbScalingStat.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbScalingStat.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbScalingStat.BorderStyle = ButtonBorderStyle.Solid;
            cmbScalingStat.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbScalingStat.DrawDropdownHoverOutline = false;
            cmbScalingStat.DrawFocusRectangle = false;
            cmbScalingStat.DrawMode = DrawMode.OwnerDrawFixed;
            cmbScalingStat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScalingStat.FlatStyle = FlatStyle.Flat;
            cmbScalingStat.ForeColor = System.Drawing.Color.Gainsboro;
            cmbScalingStat.FormattingEnabled = true;
            cmbScalingStat.Location = new System.Drawing.Point(12, 233);
            cmbScalingStat.Margin = new Padding(4, 3, 4, 3);
            cmbScalingStat.Name = "cmbScalingStat";
            cmbScalingStat.Size = new Size(220, 24);
            cmbScalingStat.TabIndex = 57;
            cmbScalingStat.Text = null;
            cmbScalingStat.TextPadding = new Padding(2);
            cmbScalingStat.SelectedIndexChanged += cmbScalingStat_SelectedIndexChanged;
            // 
            // lblScalingStat
            // 
            lblScalingStat.AutoSize = true;
            lblScalingStat.Location = new System.Drawing.Point(7, 212);
            lblScalingStat.Margin = new Padding(4, 0, 4, 0);
            lblScalingStat.Name = "lblScalingStat";
            lblScalingStat.Size = new Size(71, 15);
            lblScalingStat.TabIndex = 56;
            lblScalingStat.Text = "Scaling Stat:";
            // 
            // chkFriendly
            // 
            chkFriendly.AutoSize = true;
            chkFriendly.Location = new System.Drawing.Point(10, 24);
            chkFriendly.Margin = new Padding(4, 3, 4, 3);
            chkFriendly.Name = "chkFriendly";
            chkFriendly.Size = new Size(68, 19);
            chkFriendly.TabIndex = 55;
            chkFriendly.Text = "Friendly";
            chkFriendly.CheckedChanged += chkFriendly_CheckedChanged;
            // 
            // lblCritChance
            // 
            lblCritChance.AutoSize = true;
            lblCritChance.Location = new System.Drawing.Point(7, 321);
            lblCritChance.Margin = new Padding(4, 0, 4, 0);
            lblCritChance.Name = "lblCritChance";
            lblCritChance.Size = new Size(93, 15);
            lblCritChance.TabIndex = 54;
            lblCritChance.Text = "Crit Chance (%):";
            // 
            // lblScaling
            // 
            lblScaling.AutoSize = true;
            lblScaling.Location = new System.Drawing.Point(7, 269);
            lblScaling.Margin = new Padding(4, 0, 4, 0);
            lblScaling.Name = "lblScaling";
            lblScaling.Size = new Size(95, 15);
            lblScaling.TabIndex = 52;
            lblScaling.Text = "Scaling Amount:";
            // 
            // cmbDamageType
            // 
            cmbDamageType.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbDamageType.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbDamageType.BorderStyle = ButtonBorderStyle.Solid;
            cmbDamageType.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbDamageType.DrawDropdownHoverOutline = false;
            cmbDamageType.DrawFocusRectangle = false;
            cmbDamageType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDamageType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDamageType.FlatStyle = FlatStyle.Flat;
            cmbDamageType.ForeColor = System.Drawing.Color.Gainsboro;
            cmbDamageType.FormattingEnabled = true;
            cmbDamageType.Items.AddRange(new object[] { "Physical", "Magic", "True" });
            cmbDamageType.Location = new System.Drawing.Point(10, 175);
            cmbDamageType.Margin = new Padding(4, 3, 4, 3);
            cmbDamageType.Name = "cmbDamageType";
            cmbDamageType.Size = new Size(221, 24);
            cmbDamageType.TabIndex = 50;
            cmbDamageType.Text = "Physical";
            cmbDamageType.TextPadding = new Padding(2);
            cmbDamageType.SelectedIndexChanged += cmbDamageType_SelectedIndexChanged;
            // 
            // lblDamageType
            // 
            lblDamageType.AutoSize = true;
            lblDamageType.Location = new System.Drawing.Point(6, 156);
            lblDamageType.Margin = new Padding(4, 0, 4, 0);
            lblDamageType.Name = "lblDamageType";
            lblDamageType.Size = new Size(81, 15);
            lblDamageType.TabIndex = 49;
            lblDamageType.Text = "Damage Type:";
            // 
            // lblHPDamage
            // 
            lblHPDamage.AutoSize = true;
            lblHPDamage.Location = new System.Drawing.Point(7, 53);
            lblHPDamage.Margin = new Padding(4, 0, 4, 0);
            lblHPDamage.Name = "lblHPDamage";
            lblHPDamage.Size = new Size(73, 15);
            lblHPDamage.TabIndex = 46;
            lblHPDamage.Text = "HP Damage:";
            // 
            // lblManaDamage
            // 
            lblManaDamage.AutoSize = true;
            lblManaDamage.Location = new System.Drawing.Point(7, 106);
            lblManaDamage.Margin = new Padding(4, 0, 4, 0);
            lblManaDamage.Name = "lblManaDamage";
            lblManaDamage.Size = new Size(87, 15);
            lblManaDamage.TabIndex = 47;
            lblManaDamage.Text = "Mana Damage:";
            // 
            // grpEvent
            // 
            grpEvent.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpEvent.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpEvent.Controls.Add(cmbEvent);
            grpEvent.ForeColor = System.Drawing.Color.Gainsboro;
            grpEvent.Location = new System.Drawing.Point(603, 6);
            grpEvent.Margin = new Padding(4, 3, 4, 3);
            grpEvent.Name = "grpEvent";
            grpEvent.Padding = new Padding(4, 3, 4, 3);
            grpEvent.Size = new Size(537, 55);
            grpEvent.TabIndex = 40;
            grpEvent.TabStop = false;
            grpEvent.Text = "Event";
            grpEvent.Visible = false;
            // 
            // cmbEvent
            // 
            cmbEvent.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbEvent.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbEvent.BorderStyle = ButtonBorderStyle.Solid;
            cmbEvent.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbEvent.DrawDropdownHoverOutline = false;
            cmbEvent.DrawFocusRectangle = false;
            cmbEvent.DrawMode = DrawMode.OwnerDrawFixed;
            cmbEvent.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEvent.FlatStyle = FlatStyle.Flat;
            cmbEvent.ForeColor = System.Drawing.Color.Gainsboro;
            cmbEvent.FormattingEnabled = true;
            cmbEvent.Location = new System.Drawing.Point(10, 20);
            cmbEvent.Margin = new Padding(4, 3, 4, 3);
            cmbEvent.Name = "cmbEvent";
            cmbEvent.Size = new Size(518, 24);
            cmbEvent.TabIndex = 17;
            cmbEvent.Text = null;
            cmbEvent.TextPadding = new Padding(2);
            cmbEvent.SelectedIndexChanged += cmbEvent_SelectedIndexChanged;
            // 
            // grpDash
            // 
            grpDash.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpDash.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpDash.Controls.Add(grpDashCollisions);
            grpDash.Controls.Add(lblRange);
            grpDash.Controls.Add(scrlRange);
            grpDash.ForeColor = System.Drawing.Color.Gainsboro;
            grpDash.Location = new System.Drawing.Point(603, 6);
            grpDash.Margin = new Padding(4, 3, 4, 3);
            grpDash.Name = "grpDash";
            grpDash.Padding = new Padding(4, 3, 4, 3);
            grpDash.Size = new Size(233, 209);
            grpDash.TabIndex = 38;
            grpDash.TabStop = false;
            grpDash.Text = "Dash";
            grpDash.Visible = false;
            // 
            // grpDashCollisions
            // 
            grpDashCollisions.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpDashCollisions.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpDashCollisions.Controls.Add(chkIgnoreInactiveResources);
            grpDashCollisions.Controls.Add(chkIgnoreZDimensionBlocks);
            grpDashCollisions.Controls.Add(chkIgnoreMapBlocks);
            grpDashCollisions.Controls.Add(chkIgnoreActiveResources);
            grpDashCollisions.ForeColor = System.Drawing.Color.Gainsboro;
            grpDashCollisions.Location = new System.Drawing.Point(14, 72);
            grpDashCollisions.Margin = new Padding(4, 3, 4, 3);
            grpDashCollisions.Name = "grpDashCollisions";
            grpDashCollisions.Padding = new Padding(4, 3, 4, 3);
            grpDashCollisions.Size = new Size(210, 122);
            grpDashCollisions.TabIndex = 41;
            grpDashCollisions.TabStop = false;
            grpDashCollisions.Text = "Ignore Collision:";
            // 
            // chkIgnoreInactiveResources
            // 
            chkIgnoreInactiveResources.AutoSize = true;
            chkIgnoreInactiveResources.Location = new System.Drawing.Point(7, 72);
            chkIgnoreInactiveResources.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreInactiveResources.Name = "chkIgnoreInactiveResources";
            chkIgnoreInactiveResources.Size = new Size(123, 19);
            chkIgnoreInactiveResources.TabIndex = 38;
            chkIgnoreInactiveResources.Text = "Inactive Resources";
            chkIgnoreInactiveResources.CheckedChanged += chkIgnoreInactiveResources_CheckedChanged;
            // 
            // chkIgnoreZDimensionBlocks
            // 
            chkIgnoreZDimensionBlocks.AutoSize = true;
            chkIgnoreZDimensionBlocks.Location = new System.Drawing.Point(7, 98);
            chkIgnoreZDimensionBlocks.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreZDimensionBlocks.Name = "chkIgnoreZDimensionBlocks";
            chkIgnoreZDimensionBlocks.Size = new Size(132, 19);
            chkIgnoreZDimensionBlocks.TabIndex = 37;
            chkIgnoreZDimensionBlocks.Text = "Z-Dimension Blocks";
            chkIgnoreZDimensionBlocks.CheckedChanged += chkIgnoreZDimensionBlocks_CheckedChanged;
            // 
            // chkIgnoreMapBlocks
            // 
            chkIgnoreMapBlocks.AutoSize = true;
            chkIgnoreMapBlocks.Location = new System.Drawing.Point(7, 18);
            chkIgnoreMapBlocks.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreMapBlocks.Name = "chkIgnoreMapBlocks";
            chkIgnoreMapBlocks.Size = new Size(87, 19);
            chkIgnoreMapBlocks.TabIndex = 33;
            chkIgnoreMapBlocks.Text = "Map Blocks";
            chkIgnoreMapBlocks.CheckedChanged += chkIgnoreMapBlocks_CheckedChanged;
            // 
            // chkIgnoreActiveResources
            // 
            chkIgnoreActiveResources.AutoSize = true;
            chkIgnoreActiveResources.Location = new System.Drawing.Point(7, 45);
            chkIgnoreActiveResources.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreActiveResources.Name = "chkIgnoreActiveResources";
            chkIgnoreActiveResources.Size = new Size(115, 19);
            chkIgnoreActiveResources.TabIndex = 36;
            chkIgnoreActiveResources.Text = "Active Resources";
            chkIgnoreActiveResources.CheckedChanged += chkIgnoreActiveResources_CheckedChanged;
            // 
            // lblRange
            // 
            lblRange.AutoSize = true;
            lblRange.Location = new System.Drawing.Point(13, 29);
            lblRange.Margin = new Padding(4, 0, 4, 0);
            lblRange.Name = "lblRange";
            lblRange.Size = new Size(52, 15);
            lblRange.TabIndex = 40;
            lblRange.Text = "Range: 0";
            // 
            // scrlRange
            // 
            scrlRange.Location = new System.Drawing.Point(16, 44);
            scrlRange.Margin = new Padding(4, 3, 4, 3);
            scrlRange.Maximum = 10;
            scrlRange.Name = "scrlRange";
            scrlRange.ScrollOrientation = DarkScrollOrientation.Horizontal;
            scrlRange.Size = new Size(196, 21);
            scrlRange.TabIndex = 39;
            scrlRange.ValueChanged += scrlRange_Scroll;
            // 
            // grpWarp
            // 
            grpWarp.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpWarp.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpWarp.Controls.Add(nudWarpY);
            grpWarp.Controls.Add(nudWarpX);
            grpWarp.Controls.Add(btnVisualMapSelector);
            grpWarp.Controls.Add(cmbWarpMap);
            grpWarp.Controls.Add(cmbDirection);
            grpWarp.Controls.Add(lblWarpDir);
            grpWarp.Controls.Add(lblY);
            grpWarp.Controls.Add(lblX);
            grpWarp.Controls.Add(lblMap);
            grpWarp.ForeColor = System.Drawing.Color.Gainsboro;
            grpWarp.Location = new System.Drawing.Point(603, 6);
            grpWarp.Margin = new Padding(4, 3, 4, 3);
            grpWarp.Name = "grpWarp";
            grpWarp.Padding = new Padding(4, 3, 4, 3);
            grpWarp.Size = new Size(288, 210);
            grpWarp.TabIndex = 35;
            grpWarp.TabStop = false;
            grpWarp.Text = "Warp Caster:";
            grpWarp.Visible = false;
            // 
            // nudWarpY
            // 
            nudWarpY.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudWarpY.ForeColor = System.Drawing.Color.Gainsboro;
            nudWarpY.Location = new System.Drawing.Point(49, 105);
            nudWarpY.Margin = new Padding(4, 3, 4, 3);
            nudWarpY.Name = "nudWarpY";
            nudWarpY.Size = new Size(222, 23);
            nudWarpY.TabIndex = 35;
            nudWarpY.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudWarpY.ValueChanged += nudWarpY_ValueChanged;
            // 
            // nudWarpX
            // 
            nudWarpX.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudWarpX.ForeColor = System.Drawing.Color.Gainsboro;
            nudWarpX.Location = new System.Drawing.Point(49, 73);
            nudWarpX.Margin = new Padding(4, 3, 4, 3);
            nudWarpX.Name = "nudWarpX";
            nudWarpX.Size = new Size(222, 23);
            nudWarpX.TabIndex = 34;
            nudWarpX.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudWarpX.ValueChanged += nudWarpX_ValueChanged;
            // 
            // btnVisualMapSelector
            // 
            btnVisualMapSelector.Location = new System.Drawing.Point(10, 174);
            btnVisualMapSelector.Margin = new Padding(4, 3, 4, 3);
            btnVisualMapSelector.Name = "btnVisualMapSelector";
            btnVisualMapSelector.Padding = new Padding(6);
            btnVisualMapSelector.Size = new Size(259, 27);
            btnVisualMapSelector.TabIndex = 33;
            btnVisualMapSelector.Text = "Open Visual Interface";
            btnVisualMapSelector.Click += btnVisualMapSelector_Click;
            // 
            // cmbWarpMap
            // 
            cmbWarpMap.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbWarpMap.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbWarpMap.BorderStyle = ButtonBorderStyle.Solid;
            cmbWarpMap.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbWarpMap.DrawDropdownHoverOutline = false;
            cmbWarpMap.DrawFocusRectangle = false;
            cmbWarpMap.DrawMode = DrawMode.OwnerDrawFixed;
            cmbWarpMap.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWarpMap.FlatStyle = FlatStyle.Flat;
            cmbWarpMap.ForeColor = System.Drawing.Color.Gainsboro;
            cmbWarpMap.FormattingEnabled = true;
            cmbWarpMap.Location = new System.Drawing.Point(12, 39);
            cmbWarpMap.Margin = new Padding(4, 3, 4, 3);
            cmbWarpMap.Name = "cmbWarpMap";
            cmbWarpMap.Size = new Size(257, 24);
            cmbWarpMap.TabIndex = 30;
            cmbWarpMap.Text = null;
            cmbWarpMap.TextPadding = new Padding(2);
            cmbWarpMap.SelectedIndexChanged += cmbWarpMap_SelectedIndexChanged;
            // 
            // cmbDirection
            // 
            cmbDirection.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbDirection.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbDirection.BorderStyle = ButtonBorderStyle.Solid;
            cmbDirection.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbDirection.DrawDropdownHoverOutline = false;
            cmbDirection.DrawFocusRectangle = false;
            cmbDirection.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.FlatStyle = FlatStyle.Flat;
            cmbDirection.ForeColor = System.Drawing.Color.Gainsboro;
            cmbDirection.FormattingEnabled = true;
            cmbDirection.Items.AddRange(new object[] { "Retain Direction", "Up", "Down", "Left", "Right" });
            cmbDirection.Location = new System.Drawing.Point(49, 141);
            cmbDirection.Margin = new Padding(4, 3, 4, 3);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(220, 24);
            cmbDirection.TabIndex = 32;
            cmbDirection.Text = "Retain Direction";
            cmbDirection.TextPadding = new Padding(2);
            cmbDirection.SelectedIndexChanged += cmbDirection_SelectedIndexChanged;
            // 
            // lblWarpDir
            // 
            lblWarpDir.AutoSize = true;
            lblWarpDir.Location = new System.Drawing.Point(7, 144);
            lblWarpDir.Margin = new Padding(4, 0, 4, 0);
            lblWarpDir.Name = "lblWarpDir";
            lblWarpDir.Size = new Size(25, 15);
            lblWarpDir.TabIndex = 31;
            lblWarpDir.Text = "Dir:";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Location = new System.Drawing.Point(8, 107);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(17, 15);
            lblY.TabIndex = 29;
            lblY.Text = "Y:";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Location = new System.Drawing.Point(8, 75);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(17, 15);
            lblX.TabIndex = 28;
            lblX.Text = "X:";
            // 
            // lblMap
            // 
            lblMap.AutoSize = true;
            lblMap.Location = new System.Drawing.Point(7, 21);
            lblMap.Margin = new Padding(4, 0, 4, 0);
            lblMap.Name = "lblMap";
            lblMap.Size = new Size(34, 15);
            lblMap.TabIndex = 27;
            lblMap.Text = "Map:";
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
            toolStrip.Size = new Size(1416, 29);
            toolStrip.TabIndex = 51;
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
            toolStripItemNew.Click += toolStripItemNew_Click;
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
            toolStripItemDelete.Click += toolStripItemDelete_Click;
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
            btnAlphabetical.Click += btnAlphabetical_Click;
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
            toolStripItemCopy.Click += toolStripItemCopy_Click;
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
            toolStripItemPaste.Click += toolStripItemPaste_Click;
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
            toolStripItemUndo.Click += toolStripItemUndo_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(1186, 665);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6);
            btnCancel.Size = new Size(222, 31);
            btnCancel.TabIndex = 49;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(958, 665);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6);
            btnSave.Size = new Size(222, 31);
            btnSave.TabIndex = 46;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // grpSpells
            // 
            grpSpells.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpSpells.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpSpells.Controls.Add(btnClearSearch);
            grpSpells.Controls.Add(txtSearch);
            grpSpells.Controls.Add(lstGameObjects);
            grpSpells.ForeColor = System.Drawing.Color.Gainsboro;
            grpSpells.Location = new System.Drawing.Point(4, 32);
            grpSpells.Margin = new Padding(4, 3, 4, 3);
            grpSpells.Name = "grpSpells";
            grpSpells.Padding = new Padding(4, 3, 4, 3);
            grpSpells.Size = new Size(233, 620);
            grpSpells.TabIndex = 16;
            grpSpells.TabStop = false;
            grpSpells.Text = "Spells";
            // 
            // btnClearSearch
            // 
            btnClearSearch.Location = new System.Drawing.Point(204, 22);
            btnClearSearch.Margin = new Padding(4, 3, 4, 3);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Padding = new Padding(6);
            btnClearSearch.Size = new Size(21, 23);
            btnClearSearch.TabIndex = 34;
            btnClearSearch.Text = "X";
            btnClearSearch.Click += btnClearSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtSearch.Location = new System.Drawing.Point(6, 22);
            txtSearch.Margin = new Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(192, 23);
            txtSearch.TabIndex = 33;
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
            lstGameObjects.Location = new System.Drawing.Point(6, 53);
            lstGameObjects.Margin = new Padding(4, 3, 4, 3);
            lstGameObjects.Name = "lstGameObjects";
            lstGameObjects.SelectedImageIndex = 0;
            lstGameObjects.Size = new Size(222, 553);
            lstGameObjects.TabIndex = 32;
            // 
            // FrmSpell
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1416, 705);
            Controls.Add(toolStrip);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(pnlContainer);
            Controls.Add(grpSpells);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSpell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Spell Editor                       ";
            FormClosed += frmSpell_FormClosed;
            Load += frmSpell_Load;
            KeyDown += form_KeyDown;
            pnlContainer.ResumeLayout(false);
            grpGeneral.ResumeLayout(false);
            grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picSpell).EndInit();
            grpSpellCost.ResumeLayout(false);
            grpSpellCost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCooldownDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCastDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMpCost).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHPCost).EndInit();
            grpRequirements.ResumeLayout(false);
            grpRequirements.PerformLayout();
            grpTargetInfo.ResumeLayout(false);
            grpTargetInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHitRadius).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCastRange).EndInit();
            grpCombat.ResumeLayout(false);
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCurPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDmgPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAgiPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCur).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDmg).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAgi).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpdPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMRPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDefPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMagPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStrPercentage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpd).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMR).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDef).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMag).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStr).EndInit();
            grpHotDot.ResumeLayout(false);
            grpHotDot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTick).EndInit();
            grpEffect.ResumeLayout(false);
            grpEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picSprite).EndInit();
            grpEffectDuration.ResumeLayout(false);
            grpEffectDuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBuffDuration).EndInit();
            grpDamage.ResumeLayout(false);
            grpDamage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCritMultiplier).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCritChance).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudScaling).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMPDamage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHPDamage).EndInit();
            grpEvent.ResumeLayout(false);
            grpDash.ResumeLayout(false);
            grpDash.PerformLayout();
            grpDashCollisions.ResumeLayout(false);
            grpDashCollisions.PerformLayout();
            grpWarp.ResumeLayout(false);
            grpWarp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWarpY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWarpX).EndInit();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            grpSpells.ResumeLayout(false);
            grpSpells.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpSpells;
        private DarkGroupBox grpGeneral;
        private System.Windows.Forms.Label lblCooldownDuration;
        private DarkComboBox cmbSprite;
        private System.Windows.Forms.Label lblCastDuration;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.PictureBox picSpell;
        private System.Windows.Forms.Label lblName;
        private DarkTextBox txtName;
        private DarkGroupBox grpRequirements;
        private System.Windows.Forms.Label lblHitAnimation;
        private System.Windows.Forms.Label lblCastAnimation;
        private System.Windows.Forms.Label lblType;
        private DarkComboBox cmbType;
        private DarkGroupBox grpTargetInfo;
        private System.Windows.Forms.Label lblHitRadius;
        private DarkComboBox cmbTargetType;
        private System.Windows.Forms.Label lblCastRange;
        private System.Windows.Forms.Label lblTargetType;
        private DarkGroupBox grpWarp;
        private DarkGroupBox grpSpellCost;
        private System.Windows.Forms.Label lblMPCost;
        private System.Windows.Forms.Label lblHPCost;
        private System.Windows.Forms.Label lblDesc;
        private DarkTextBox txtDesc;
        private DarkGroupBox grpDash;
        private System.Windows.Forms.Label lblRange;
        private DarkScrollBar scrlRange;
        private System.Windows.Forms.Label lblProjectile;
        private DarkGroupBox grpDashCollisions;
        private DarkCheckBox chkIgnoreInactiveResources;
        private DarkCheckBox chkIgnoreZDimensionBlocks;
        private DarkCheckBox chkIgnoreMapBlocks;
        private DarkCheckBox chkIgnoreActiveResources;
        private DarkGroupBox grpEvent;
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
        private DarkButton btnDynamicRequirements;
        private DarkComboBox cmbHitAnimation;
        private DarkComboBox cmbCastAnimation;
        private DarkComboBox cmbProjectile;
        private DarkComboBox cmbEvent;
        private DarkNumericUpDown nudWarpY;
        private DarkNumericUpDown nudWarpX;
        private DarkButton btnVisualMapSelector;
        private DarkComboBox cmbWarpMap;
        private DarkComboBox cmbDirection;
        private System.Windows.Forms.Label lblWarpDir;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblMap;
        private DarkNumericUpDown nudCooldownDuration;
        private DarkNumericUpDown nudCastDuration;
        private DarkNumericUpDown nudMpCost;
        private DarkNumericUpDown nudHPCost;
        private DarkNumericUpDown nudHitRadius;
        private DarkNumericUpDown nudCastRange;
        private DarkNumericUpDown nudDuration;
        private System.Windows.Forms.Label lblDuration;
        private DarkCheckBox chkBound;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkComboBox cmbFolder;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DarkButton btnAddCooldownGroup;
        private DarkComboBox cmbCooldownGroup;
        private System.Windows.Forms.Label lblCooldownGroup;
        private DarkCheckBox chkIgnoreGlobalCooldown;
        private DarkCheckBox chkIgnoreCdr;
        private Controls.GameObjectList lstGameObjects;
        private System.Windows.Forms.Label lblCannotCast;
        private DarkTextBox txtCannotCast;
        private DarkGroupBox grpCombat;
        private DarkGroupBox grpStats;
        private System.Windows.Forms.Label lblPercentage5;
        private System.Windows.Forms.Label lblPercentage4;
        private System.Windows.Forms.Label lblPercentage3;
        private System.Windows.Forms.Label lblPercentage2;
        private System.Windows.Forms.Label lblPercentage1;
        private DarkNumericUpDown nudSpdPercentage;
        private DarkNumericUpDown nudMRPercentage;
        private DarkNumericUpDown nudDefPercentage;
        private DarkNumericUpDown nudMagPercentage;
        private DarkNumericUpDown nudStrPercentage;
        private System.Windows.Forms.Label lblPlus5;
        private System.Windows.Forms.Label lblPlus4;
        private System.Windows.Forms.Label lblPlus3;
        private System.Windows.Forms.Label lblPlus2;
        private System.Windows.Forms.Label lblPlus1;
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
        private DarkGroupBox grpHotDot;
        private DarkNumericUpDown nudTick;
        private DarkCheckBox chkHOTDOT;
        private System.Windows.Forms.Label lblTick;
        private DarkGroupBox grpEffect;
        private System.Windows.Forms.Label lblEffect;
        private DarkComboBox cmbExtraEffect;
        private System.Windows.Forms.PictureBox picSprite;
        private DarkComboBox cmbTransform;
        private System.Windows.Forms.Label lblSprite;
        private DarkGroupBox grpEffectDuration;
        private DarkNumericUpDown nudBuffDuration;
        private System.Windows.Forms.Label lblBuffDuration;
        private DarkGroupBox grpDamage;
        private DarkNumericUpDown nudCritMultiplier;
        private System.Windows.Forms.Label lblCritMultiplier;
        private DarkNumericUpDown nudCritChance;
        private DarkNumericUpDown nudScaling;
        private DarkNumericUpDown nudMPDamage;
        private DarkNumericUpDown nudHPDamage;
        private DarkComboBox cmbScalingStat;
        private System.Windows.Forms.Label lblScalingStat;
        private DarkCheckBox chkFriendly;
        private System.Windows.Forms.Label lblCritChance;
        private System.Windows.Forms.Label lblScaling;
        private DarkComboBox cmbDamageType;
        private System.Windows.Forms.Label lblDamageType;
        private System.Windows.Forms.Label lblHPDamage;
        private System.Windows.Forms.Label lblManaDamage;
        private System.Windows.Forms.Label lblTickAnimation;
        private DarkComboBox cmbTickAnimation;
        private System.Windows.Forms.Label lblSpriteCastAnimation;
        private DarkComboBox cmbCastSprite;
        private Label label3;
        private Label label4;
        private Label label5;
        private DarkNumericUpDown nudCurPercentage;
        private DarkNumericUpDown nudDmgPercentage;
        private DarkNumericUpDown nudAgiPercentage;
        private Label label6;
        private Label label7;
        private Label label8;
        private DarkNumericUpDown nudCur;
        private DarkNumericUpDown nudDmg;
        private DarkNumericUpDown nudAgi;
        private Label Curlabel;
        private Label dmgLabel;
        private Label label11;
        private DarkComboBox cmbTrapAnimation;
        private System.Windows.Forms.Label lblTrapAnimation;
    }
}
