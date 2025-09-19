using System.Drawing;
using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmPet
    {
        private System.ComponentModel.IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPet));
            grpPets = new DarkGroupBox();
            btnClearSearch = new DarkButton();
            txtSearch = new DarkTextBox();
            lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            pnlContainer = new Panel();
            grpAnimation = new DarkGroupBox();
            lblDeathAnimation = new Label();
            cmbDeathAnimation = new DarkComboBox();
            grpImmunities = new DarkGroupBox();
            lblTenacity = new Label();
            nudTenacity = new DarkNumericUpDown();
            chkTaunt = new DarkCheckBox();
            chkSleep = new DarkCheckBox();
            chkTransform = new DarkCheckBox();
            chkBlind = new DarkCheckBox();
            chkSnare = new DarkCheckBox();
            chkStun = new DarkCheckBox();
            chkSilence = new DarkCheckBox();
            chkKnockback = new DarkCheckBox();
            grpCombat = new DarkGroupBox();
            lblAttackSpeedValue = new Label();
            nudAttackSpeedValue = new DarkNumericUpDown();
            lblAttackSpeedModifier = new Label();
            cmbAttackSpeedModifier = new DarkComboBox();
            lblCritMultiplier = new Label();
            nudCritMultiplier = new DarkNumericUpDown();
            lblCritChance = new Label();
            nudCritChance = new DarkNumericUpDown();
            lblDamage = new Label();
            nudDamage = new DarkNumericUpDown();
            lblScaling = new Label();
            nudScaling = new DarkNumericUpDown();
            lblScalingStat = new Label();
            cmbScalingStat = new DarkComboBox();
            lblDamageType = new Label();
            cmbDamageType = new DarkComboBox();
            lblAttackAnimation = new Label();
            cmbAttackAnimation = new DarkComboBox();
            grpRegen = new DarkGroupBox();
            lblManaRegen = new Label();
            nudMpRegen = new DarkNumericUpDown();
            lblHpRegen = new Label();
            nudHpRegen = new DarkNumericUpDown();
            grpStats = new DarkGroupBox();
            label1 = new Label();
            nudAgi = new DarkNumericUpDown();
            lblSpd = new Label();
            nudSpd = new DarkNumericUpDown();
            lblMR = new Label();
            nudMR = new DarkNumericUpDown();
            lblDef = new Label();
            nudDef = new DarkNumericUpDown();
            lblMag = new Label();
            nudMag = new DarkNumericUpDown();
            lblStr = new Label();
            nudStr = new DarkNumericUpDown();
            lblMana = new Label();
            nudMana = new DarkNumericUpDown();
            lblHP = new Label();
            nudHp = new DarkNumericUpDown();
            grpGeneral = new DarkGroupBox();
            lblPic = new Label();
            cmbSprite = new DarkComboBox();
            picPet = new PictureBox();
            btnAddFolder = new DarkButton();
            lblFolder = new Label();
            cmbFolder = new DarkComboBox();
            lblName = new Label();
            txtName = new DarkTextBox();
            grpSpells = new DarkGroupBox();
            btnRemove = new DarkButton();
            btnAdd = new DarkButton();
            cmbSpell = new DarkComboBox();
            lstSpells = new ListBox();
            btnSave = new DarkButton();
            btnCancel = new DarkButton();
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
            grpPets.SuspendLayout();
            pnlContainer.SuspendLayout();
            grpAnimation.SuspendLayout();
            grpImmunities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTenacity).BeginInit();
            grpCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAttackSpeedValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCritMultiplier).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCritChance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudScaling).BeginInit();
            grpRegen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMpRegen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHpRegen).BeginInit();
            grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAgi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDef).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMana).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHp).BeginInit();
            grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPet).BeginInit();
            grpSpells.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // grpPets
            // 
            grpPets.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpPets.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpPets.Controls.Add(btnClearSearch);
            grpPets.Controls.Add(txtSearch);
            grpPets.Controls.Add(lstGameObjects);
            grpPets.ForeColor = System.Drawing.Color.Gainsboro;
            grpPets.Location = new System.Drawing.Point(10, 39);
            grpPets.Name = "grpPets";
            grpPets.Size = new Size(228, 600);
            grpPets.TabIndex = 1;
            grpPets.TabStop = false;
            grpPets.Text = "Pets";
            // 
            // btnClearSearch
            // 
            btnClearSearch.Location = new System.Drawing.Point(189, 19);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Padding = new Padding(5, 6, 5, 6);
            btnClearSearch.Size = new Size(28, 24);
            btnClearSearch.TabIndex = 2;
            btnClearSearch.Text = "X";
            btnClearSearch.Click += btnClearSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtSearch.Location = new System.Drawing.Point(7, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(177, 23);
            txtSearch.TabIndex = 1;
            txtSearch.Text = "Search...";
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
            // 
            // lstGameObjects
            // 
            lstGameObjects.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lstGameObjects.BorderStyle = BorderStyle.None;
            lstGameObjects.ForeColor = System.Drawing.Color.Gainsboro;
            lstGameObjects.ImageIndex = 0;
            lstGameObjects.Location = new System.Drawing.Point(7, 52);
            lstGameObjects.Name = "lstGameObjects";
            lstGameObjects.SelectedImageIndex = 0;
            lstGameObjects.Size = new Size(210, 534);
            lstGameObjects.TabIndex = 0;
            // 
            // pnlContainer
            // 
            pnlContainer.AutoScroll = true;
            pnlContainer.Controls.Add(grpAnimation);
            pnlContainer.Controls.Add(grpImmunities);
            pnlContainer.Controls.Add(grpCombat);
            pnlContainer.Controls.Add(grpRegen);
            pnlContainer.Controls.Add(grpStats);
            pnlContainer.Controls.Add(grpGeneral);
            pnlContainer.Controls.Add(grpSpells);
            pnlContainer.Location = new System.Drawing.Point(245, 39);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(621, 581);
            pnlContainer.TabIndex = 2;
            // 
            // grpAnimation
            // 
            grpAnimation.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpAnimation.Controls.Add(lblDeathAnimation);
            grpAnimation.Controls.Add(cmbDeathAnimation);
            grpAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            grpAnimation.Location = new System.Drawing.Point(7, 486);
            grpAnimation.Name = "grpAnimation";
            grpAnimation.Size = new Size(280, 75);
            grpAnimation.TabIndex = 5;
            grpAnimation.TabStop = false;
            grpAnimation.Text = "Animations";
            // 
            // lblDeathAnimation
            // 
            lblDeathAnimation.AutoSize = true;
            lblDeathAnimation.Location = new System.Drawing.Point(10, 26);
            lblDeathAnimation.Name = "lblDeathAnimation";
            lblDeathAnimation.Size = new Size(97, 15);
            lblDeathAnimation.TabIndex = 0;
            lblDeathAnimation.Text = "Death Animation";
            // 
            // cmbDeathAnimation
            // 
            cmbDeathAnimation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbDeathAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbDeathAnimation.BorderStyle = ButtonBorderStyle.Solid;
            cmbDeathAnimation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbDeathAnimation.DrawDropdownHoverOutline = false;
            cmbDeathAnimation.DrawFocusRectangle = false;
            cmbDeathAnimation.DrawMode = DrawMode.OwnerDrawVariable;
            cmbDeathAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeathAnimation.FlatStyle = FlatStyle.Flat;
            cmbDeathAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            cmbDeathAnimation.Location = new System.Drawing.Point(13, 44);
            cmbDeathAnimation.Name = "cmbDeathAnimation";
            cmbDeathAnimation.Size = new Size(246, 24);
            cmbDeathAnimation.TabIndex = 1;
            cmbDeathAnimation.Text = null;
            cmbDeathAnimation.TextPadding = new Padding(2);
            cmbDeathAnimation.SelectedIndexChanged += cmbDeathAnimation_SelectedIndexChanged;
            // 
            // grpImmunities
            // 
            grpImmunities.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpImmunities.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpImmunities.Controls.Add(lblTenacity);
            grpImmunities.Controls.Add(nudTenacity);
            grpImmunities.Controls.Add(chkTaunt);
            grpImmunities.Controls.Add(chkSleep);
            grpImmunities.Controls.Add(chkTransform);
            grpImmunities.Controls.Add(chkBlind);
            grpImmunities.Controls.Add(chkSnare);
            grpImmunities.Controls.Add(chkStun);
            grpImmunities.Controls.Add(chkSilence);
            grpImmunities.Controls.Add(chkKnockback);
            grpImmunities.ForeColor = System.Drawing.Color.Gainsboro;
            grpImmunities.Location = new System.Drawing.Point(298, 338);
            grpImmunities.Name = "grpImmunities";
            grpImmunities.Size = new Size(298, 225);
            grpImmunities.TabIndex = 4;
            grpImmunities.TabStop = false;
            grpImmunities.Text = "Immunities";
            // 
            // lblTenacity
            // 
            lblTenacity.AutoSize = true;
            lblTenacity.Location = new System.Drawing.Point(13, 30);
            lblTenacity.Name = "lblTenacity";
            lblTenacity.Size = new Size(50, 15);
            lblTenacity.TabIndex = 0;
            lblTenacity.Text = "Tenacity";
            // 
            // nudTenacity
            // 
            nudTenacity.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudTenacity.ForeColor = System.Drawing.Color.Gainsboro;
            nudTenacity.Location = new System.Drawing.Point(16, 49);
            nudTenacity.Name = "nudTenacity";
            nudTenacity.Size = new Size(79, 23);
            nudTenacity.TabIndex = 0;
            nudTenacity.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudTenacity.ValueChanged += nudTenacity_ValueChanged;
            // 
            // chkTaunt
            // 
            chkTaunt.Location = new System.Drawing.Point(158, 161);
            chkTaunt.Name = "chkTaunt";
            chkTaunt.Size = new Size(105, 19);
            chkTaunt.TabIndex = 1;
            chkTaunt.Text = "Taunt";
            chkTaunt.CheckedChanged += chkTaunt_CheckedChanged;
            // 
            // chkSleep
            // 
            chkSleep.Location = new System.Drawing.Point(16, 161);
            chkSleep.Name = "chkSleep";
            chkSleep.Size = new Size(105, 19);
            chkSleep.TabIndex = 2;
            chkSleep.Text = "Sleep";
            chkSleep.CheckedChanged += chkSleep_CheckedChanged;
            // 
            // chkTransform
            // 
            chkTransform.Location = new System.Drawing.Point(158, 135);
            chkTransform.Name = "chkTransform";
            chkTransform.Size = new Size(105, 19);
            chkTransform.TabIndex = 3;
            chkTransform.Text = "Transform";
            chkTransform.CheckedChanged += chkTransform_CheckedChanged;
            // 
            // chkBlind
            // 
            chkBlind.Location = new System.Drawing.Point(16, 135);
            chkBlind.Name = "chkBlind";
            chkBlind.Size = new Size(105, 19);
            chkBlind.TabIndex = 4;
            chkBlind.Text = "Blind";
            chkBlind.CheckedChanged += chkBlind_CheckedChanged;
            // 
            // chkSnare
            // 
            chkSnare.Location = new System.Drawing.Point(158, 109);
            chkSnare.Name = "chkSnare";
            chkSnare.Size = new Size(105, 19);
            chkSnare.TabIndex = 5;
            chkSnare.Text = "Snare";
            chkSnare.CheckedChanged += chkSnare_CheckedChanged;
            // 
            // chkStun
            // 
            chkStun.Location = new System.Drawing.Point(16, 109);
            chkStun.Name = "chkStun";
            chkStun.Size = new Size(105, 19);
            chkStun.TabIndex = 6;
            chkStun.Text = "Stun";
            chkStun.CheckedChanged += chkStun_CheckedChanged;
            // 
            // chkSilence
            // 
            chkSilence.Location = new System.Drawing.Point(158, 82);
            chkSilence.Name = "chkSilence";
            chkSilence.Size = new Size(105, 19);
            chkSilence.TabIndex = 7;
            chkSilence.Text = "Silence";
            chkSilence.CheckedChanged += chkSilence_CheckedChanged;
            // 
            // chkKnockback
            // 
            chkKnockback.Location = new System.Drawing.Point(16, 82);
            chkKnockback.Name = "chkKnockback";
            chkKnockback.Size = new Size(105, 19);
            chkKnockback.TabIndex = 8;
            chkKnockback.Text = "Knockback";
            chkKnockback.CheckedChanged += chkKnockback_CheckedChanged;
            // 
            // grpCombat
            // 
            grpCombat.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpCombat.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpCombat.Controls.Add(lblAttackSpeedValue);
            grpCombat.Controls.Add(nudAttackSpeedValue);
            grpCombat.Controls.Add(lblAttackSpeedModifier);
            grpCombat.Controls.Add(cmbAttackSpeedModifier);
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
            grpCombat.ForeColor = System.Drawing.Color.Gainsboro;
            grpCombat.Location = new System.Drawing.Point(298, 8);
            grpCombat.Name = "grpCombat";
            grpCombat.Size = new Size(298, 319);
            grpCombat.TabIndex = 3;
            grpCombat.TabStop = false;
            grpCombat.Text = "Combat";
            // 
            // lblAttackSpeedValue
            // 
            lblAttackSpeedValue.AutoSize = true;
            lblAttackSpeedValue.Location = new System.Drawing.Point(158, 231);
            lblAttackSpeedValue.Name = "lblAttackSpeedValue";
            lblAttackSpeedValue.Size = new Size(107, 15);
            lblAttackSpeedValue.TabIndex = 0;
            lblAttackSpeedValue.Text = "Attack Speed Value";
            // 
            // nudAttackSpeedValue
            // 
            nudAttackSpeedValue.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAttackSpeedValue.ForeColor = System.Drawing.Color.Gainsboro;
            nudAttackSpeedValue.Location = new System.Drawing.Point(160, 249);
            nudAttackSpeedValue.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudAttackSpeedValue.Name = "nudAttackSpeedValue";
            nudAttackSpeedValue.Size = new Size(122, 23);
            nudAttackSpeedValue.TabIndex = 8;
            nudAttackSpeedValue.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudAttackSpeedValue.ValueChanged += nudAttackSpeedValue_ValueChanged;
            // 
            // lblAttackSpeedModifier
            // 
            lblAttackSpeedModifier.AutoSize = true;
            lblAttackSpeedModifier.Location = new System.Drawing.Point(13, 231);
            lblAttackSpeedModifier.Name = "lblAttackSpeedModifier";
            lblAttackSpeedModifier.Size = new Size(104, 15);
            lblAttackSpeedModifier.TabIndex = 9;
            lblAttackSpeedModifier.Text = "Attack Speed Mod";
            // 
            // cmbAttackSpeedModifier
            // 
            cmbAttackSpeedModifier.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbAttackSpeedModifier.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbAttackSpeedModifier.BorderStyle = ButtonBorderStyle.Solid;
            cmbAttackSpeedModifier.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbAttackSpeedModifier.DrawDropdownHoverOutline = false;
            cmbAttackSpeedModifier.DrawFocusRectangle = false;
            cmbAttackSpeedModifier.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAttackSpeedModifier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAttackSpeedModifier.FlatStyle = FlatStyle.Flat;
            cmbAttackSpeedModifier.ForeColor = System.Drawing.Color.Gainsboro;
            cmbAttackSpeedModifier.Location = new System.Drawing.Point(16, 249);
            cmbAttackSpeedModifier.Name = "cmbAttackSpeedModifier";
            cmbAttackSpeedModifier.Size = new Size(123, 24);
            cmbAttackSpeedModifier.TabIndex = 7;
            cmbAttackSpeedModifier.Text = null;
            cmbAttackSpeedModifier.TextPadding = new Padding(2);
            cmbAttackSpeedModifier.SelectedIndexChanged += cmbAttackSpeedModifier_SelectedIndexChanged;
            // 
            // lblCritMultiplier
            // 
            lblCritMultiplier.AutoSize = true;
            lblCritMultiplier.Location = new System.Drawing.Point(13, 75);
            lblCritMultiplier.Name = "lblCritMultiplier";
            lblCritMultiplier.Size = new Size(54, 15);
            lblCritMultiplier.TabIndex = 10;
            lblCritMultiplier.Text = "Crit Mult";
            // 
            // nudCritMultiplier
            // 
            nudCritMultiplier.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCritMultiplier.DecimalPlaces = 2;
            nudCritMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            nudCritMultiplier.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudCritMultiplier.Location = new System.Drawing.Point(16, 94);
            nudCritMultiplier.Name = "nudCritMultiplier";
            nudCritMultiplier.Size = new Size(122, 23);
            nudCritMultiplier.TabIndex = 2;
            nudCritMultiplier.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCritMultiplier.ValueChanged += nudCritMultiplier_ValueChanged;
            // 
            // lblCritChance
            // 
            lblCritChance.AutoSize = true;
            lblCritChance.Location = new System.Drawing.Point(158, 26);
            lblCritChance.Name = "lblCritChance";
            lblCritChance.Size = new Size(82, 15);
            lblCritChance.TabIndex = 11;
            lblCritChance.Text = "Crit Chance %";
            // 
            // nudCritChance
            // 
            nudCritChance.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            nudCritChance.Location = new System.Drawing.Point(160, 45);
            nudCritChance.Name = "nudCritChance";
            nudCritChance.Size = new Size(122, 23);
            nudCritChance.TabIndex = 1;
            nudCritChance.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCritChance.ValueChanged += nudCritChance_ValueChanged;
            // 
            // lblDamage
            // 
            lblDamage.AutoSize = true;
            lblDamage.Location = new System.Drawing.Point(13, 26);
            lblDamage.Name = "lblDamage";
            lblDamage.Size = new Size(78, 15);
            lblDamage.TabIndex = 12;
            lblDamage.Text = "Base Damage";
            // 
            // nudDamage
            // 
            nudDamage.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudDamage.ForeColor = System.Drawing.Color.Gainsboro;
            nudDamage.Location = new System.Drawing.Point(16, 45);
            nudDamage.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new Size(122, 23);
            nudDamage.TabIndex = 0;
            nudDamage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudDamage.ValueChanged += nudDamage_ValueChanged;
            // 
            // lblScaling
            // 
            lblScaling.AutoSize = true;
            lblScaling.Location = new System.Drawing.Point(158, 75);
            lblScaling.Name = "lblScaling";
            lblScaling.Size = new Size(71, 15);
            lblScaling.TabIndex = 13;
            lblScaling.Text = "Scaling Amt";
            // 
            // nudScaling
            // 
            nudScaling.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudScaling.ForeColor = System.Drawing.Color.Gainsboro;
            nudScaling.Location = new System.Drawing.Point(160, 94);
            nudScaling.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudScaling.Name = "nudScaling";
            nudScaling.Size = new Size(122, 23);
            nudScaling.TabIndex = 3;
            nudScaling.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudScaling.ValueChanged += nudScaling_ValueChanged;
            // 
            // lblScalingStat
            // 
            lblScalingStat.AutoSize = true;
            lblScalingStat.Location = new System.Drawing.Point(13, 126);
            lblScalingStat.Name = "lblScalingStat";
            lblScalingStat.Size = new Size(68, 15);
            lblScalingStat.TabIndex = 14;
            lblScalingStat.Text = "Scaling Stat";
            // 
            // cmbScalingStat
            // 
            cmbScalingStat.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbScalingStat.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbScalingStat.BorderStyle = ButtonBorderStyle.Solid;
            cmbScalingStat.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbScalingStat.DrawDropdownHoverOutline = false;
            cmbScalingStat.DrawFocusRectangle = false;
            cmbScalingStat.DrawMode = DrawMode.OwnerDrawVariable;
            cmbScalingStat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScalingStat.FlatStyle = FlatStyle.Flat;
            cmbScalingStat.ForeColor = System.Drawing.Color.Gainsboro;
            cmbScalingStat.Location = new System.Drawing.Point(16, 144);
            cmbScalingStat.Name = "cmbScalingStat";
            cmbScalingStat.Size = new Size(123, 24);
            cmbScalingStat.TabIndex = 4;
            cmbScalingStat.Text = null;
            cmbScalingStat.TextPadding = new Padding(2);
            cmbScalingStat.SelectedIndexChanged += cmbScalingStat_SelectedIndexChanged;
            // 
            // lblDamageType
            // 
            lblDamageType.AutoSize = true;
            lblDamageType.Location = new System.Drawing.Point(158, 126);
            lblDamageType.Name = "lblDamageType";
            lblDamageType.Size = new Size(78, 15);
            lblDamageType.TabIndex = 15;
            lblDamageType.Text = "Damage Type";
            // 
            // cmbDamageType
            // 
            cmbDamageType.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbDamageType.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbDamageType.BorderStyle = ButtonBorderStyle.Solid;
            cmbDamageType.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbDamageType.DrawDropdownHoverOutline = false;
            cmbDamageType.DrawFocusRectangle = false;
            cmbDamageType.DrawMode = DrawMode.OwnerDrawVariable;
            cmbDamageType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDamageType.FlatStyle = FlatStyle.Flat;
            cmbDamageType.ForeColor = System.Drawing.Color.Gainsboro;
            cmbDamageType.Location = new System.Drawing.Point(160, 144);
            cmbDamageType.Name = "cmbDamageType";
            cmbDamageType.Size = new Size(123, 24);
            cmbDamageType.TabIndex = 5;
            cmbDamageType.Text = null;
            cmbDamageType.TextPadding = new Padding(2);
            cmbDamageType.SelectedIndexChanged += cmbDamageType_SelectedIndexChanged;
            // 
            // lblAttackAnimation
            // 
            lblAttackAnimation.AutoSize = true;
            lblAttackAnimation.Location = new System.Drawing.Point(13, 178);
            lblAttackAnimation.Name = "lblAttackAnimation";
            lblAttackAnimation.Size = new Size(100, 15);
            lblAttackAnimation.TabIndex = 16;
            lblAttackAnimation.Text = "Attack Animation";
            // 
            // cmbAttackAnimation
            // 
            cmbAttackAnimation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbAttackAnimation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbAttackAnimation.BorderStyle = ButtonBorderStyle.Solid;
            cmbAttackAnimation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbAttackAnimation.DrawDropdownHoverOutline = false;
            cmbAttackAnimation.DrawFocusRectangle = false;
            cmbAttackAnimation.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAttackAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAttackAnimation.FlatStyle = FlatStyle.Flat;
            cmbAttackAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            cmbAttackAnimation.Location = new System.Drawing.Point(16, 197);
            cmbAttackAnimation.Name = "cmbAttackAnimation";
            cmbAttackAnimation.Size = new Size(267, 24);
            cmbAttackAnimation.TabIndex = 6;
            cmbAttackAnimation.Text = null;
            cmbAttackAnimation.TextPadding = new Padding(2);
            cmbAttackAnimation.SelectedIndexChanged += cmbAttackAnimation_SelectedIndexChanged;
            // 
            // grpRegen
            // 
            grpRegen.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpRegen.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpRegen.Controls.Add(lblManaRegen);
            grpRegen.Controls.Add(nudMpRegen);
            grpRegen.Controls.Add(lblHpRegen);
            grpRegen.Controls.Add(nudHpRegen);
            grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            grpRegen.Location = new System.Drawing.Point(7, 368);
            grpRegen.Name = "grpRegen";
            grpRegen.Size = new Size(280, 112);
            grpRegen.TabIndex = 2;
            grpRegen.TabStop = false;
            grpRegen.Text = "Regen";
            // 
            // lblManaRegen
            // 
            lblManaRegen.AutoSize = true;
            lblManaRegen.Location = new System.Drawing.Point(149, 28);
            lblManaRegen.Name = "lblManaRegen";
            lblManaRegen.Size = new Size(73, 15);
            lblManaRegen.TabIndex = 0;
            lblManaRegen.Text = "Mana Regen";
            // 
            // nudMpRegen
            // 
            nudMpRegen.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            nudMpRegen.Location = new System.Drawing.Point(151, 47);
            nudMpRegen.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudMpRegen.Name = "nudMpRegen";
            nudMpRegen.Size = new Size(105, 23);
            nudMpRegen.TabIndex = 1;
            nudMpRegen.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMpRegen.ValueChanged += nudMpRegen_ValueChanged;
            // 
            // lblHpRegen
            // 
            lblHpRegen.AutoSize = true;
            lblHpRegen.Location = new System.Drawing.Point(13, 28);
            lblHpRegen.Name = "lblHpRegen";
            lblHpRegen.Size = new Size(59, 15);
            lblHpRegen.TabIndex = 2;
            lblHpRegen.Text = "HP Regen";
            // 
            // nudHpRegen
            // 
            nudHpRegen.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudHpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            nudHpRegen.Location = new System.Drawing.Point(16, 47);
            nudHpRegen.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudHpRegen.Name = "nudHpRegen";
            nudHpRegen.Size = new Size(105, 23);
            nudHpRegen.TabIndex = 0;
            nudHpRegen.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudHpRegen.ValueChanged += nudHpRegen_ValueChanged;
            // 
            // grpStats
            // 
            grpStats.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpStats.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpStats.Controls.Add(label1);
            grpStats.Controls.Add(nudAgi);
            grpStats.Controls.Add(lblSpd);
            grpStats.Controls.Add(nudSpd);
            grpStats.Controls.Add(lblMR);
            grpStats.Controls.Add(nudMR);
            grpStats.Controls.Add(lblDef);
            grpStats.Controls.Add(nudDef);
            grpStats.Controls.Add(lblMag);
            grpStats.Controls.Add(nudMag);
            grpStats.Controls.Add(lblStr);
            grpStats.Controls.Add(nudStr);
            grpStats.Controls.Add(lblMana);
            grpStats.Controls.Add(nudMana);
            grpStats.Controls.Add(lblHP);
            grpStats.Controls.Add(nudHp);
            grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            grpStats.Location = new System.Drawing.Point(7, 138);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(280, 224);
            grpStats.TabIndex = 1;
            grpStats.TabStop = false;
            grpStats.Text = "Stats";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(149, 174);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 0;
            label1.Text = "Agility";
            // 
            // nudAgi
            // 
            nudAgi.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAgi.ForeColor = System.Drawing.Color.Gainsboro;
            nudAgi.Location = new System.Drawing.Point(151, 193);
            nudAgi.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudAgi.Name = "nudAgi";
            nudAgi.Size = new Size(105, 23);
            nudAgi.TabIndex = 7;
            nudAgi.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudAgi.ValueChanged += nudAgi_ValueChanged;
            // 
            // lblSpd
            // 
            lblSpd.AutoSize = true;
            lblSpd.Location = new System.Drawing.Point(13, 174);
            lblSpd.Name = "lblSpd";
            lblSpd.Size = new Size(72, 15);
            lblSpd.TabIndex = 8;
            lblSpd.Text = "Move Speed";
            // 
            // nudSpd
            // 
            nudSpd.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
            nudSpd.Location = new System.Drawing.Point(16, 193);
            nudSpd.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSpd.Name = "nudSpd";
            nudSpd.Size = new Size(105, 23);
            nudSpd.TabIndex = 6;
            nudSpd.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudSpd.ValueChanged += nudSpd_ValueChanged;
            // 
            // lblMR
            // 
            lblMR.AutoSize = true;
            lblMR.Location = new System.Drawing.Point(149, 126);
            lblMR.Name = "lblMR";
            lblMR.Size = new Size(73, 15);
            lblMR.TabIndex = 9;
            lblMR.Text = "Magic Resist";
            // 
            // nudMR
            // 
            nudMR.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMR.ForeColor = System.Drawing.Color.Gainsboro;
            nudMR.Location = new System.Drawing.Point(151, 144);
            nudMR.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudMR.Name = "nudMR";
            nudMR.Size = new Size(105, 23);
            nudMR.TabIndex = 5;
            nudMR.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMR.ValueChanged += nudMR_ValueChanged;
            // 
            // lblDef
            // 
            lblDef.AutoSize = true;
            lblDef.Location = new System.Drawing.Point(13, 126);
            lblDef.Name = "lblDef";
            lblDef.Size = new Size(49, 15);
            lblDef.TabIndex = 10;
            lblDef.Text = "Defense";
            // 
            // nudDef
            // 
            nudDef.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudDef.ForeColor = System.Drawing.Color.Gainsboro;
            nudDef.Location = new System.Drawing.Point(16, 144);
            nudDef.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudDef.Name = "nudDef";
            nudDef.Size = new Size(105, 23);
            nudDef.TabIndex = 4;
            nudDef.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudDef.ValueChanged += nudDef_ValueChanged;
            // 
            // lblMag
            // 
            lblMag.AutoSize = true;
            lblMag.Location = new System.Drawing.Point(149, 77);
            lblMag.Name = "lblMag";
            lblMag.Size = new Size(40, 15);
            lblMag.TabIndex = 11;
            lblMag.Text = "Magic";
            // 
            // nudMag
            // 
            nudMag.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMag.ForeColor = System.Drawing.Color.Gainsboro;
            nudMag.Location = new System.Drawing.Point(151, 96);
            nudMag.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudMag.Name = "nudMag";
            nudMag.Size = new Size(105, 23);
            nudMag.TabIndex = 3;
            nudMag.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMag.ValueChanged += nudMag_ValueChanged;
            // 
            // lblStr
            // 
            lblStr.AutoSize = true;
            lblStr.Location = new System.Drawing.Point(13, 77);
            lblStr.Name = "lblStr";
            lblStr.Size = new Size(52, 15);
            lblStr.TabIndex = 12;
            lblStr.Text = "Strength";
            // 
            // nudStr
            // 
            nudStr.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudStr.ForeColor = System.Drawing.Color.Gainsboro;
            nudStr.Location = new System.Drawing.Point(16, 96);
            nudStr.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudStr.Name = "nudStr";
            nudStr.Size = new Size(105, 23);
            nudStr.TabIndex = 2;
            nudStr.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudStr.ValueChanged += nudStr_ValueChanged;
            // 
            // lblMana
            // 
            lblMana.AutoSize = true;
            lblMana.Location = new System.Drawing.Point(149, 28);
            lblMana.Name = "lblMana";
            lblMana.Size = new Size(37, 15);
            lblMana.TabIndex = 13;
            lblMana.Text = "Mana";
            // 
            // nudMana
            // 
            nudMana.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMana.ForeColor = System.Drawing.Color.Gainsboro;
            nudMana.Location = new System.Drawing.Point(151, 47);
            nudMana.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudMana.Name = "nudMana";
            nudMana.Size = new Size(105, 23);
            nudMana.TabIndex = 1;
            nudMana.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudMana.ValueChanged += nudMana_ValueChanged;
            // 
            // lblHP
            // 
            lblHP.AutoSize = true;
            lblHP.Location = new System.Drawing.Point(13, 28);
            lblHP.Name = "lblHP";
            lblHP.Size = new Size(23, 15);
            lblHP.TabIndex = 14;
            lblHP.Text = "HP";
            // 
            // nudHp
            // 
            nudHp.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudHp.ForeColor = System.Drawing.Color.Gainsboro;
            nudHp.Location = new System.Drawing.Point(16, 47);
            nudHp.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudHp.Name = "nudHp";
            nudHp.Size = new Size(105, 23);
            nudHp.TabIndex = 0;
            nudHp.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudHp.ValueChanged += nudHp_ValueChanged;
            // 
            // grpGeneral
            // 
            grpGeneral.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpGeneral.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpGeneral.Controls.Add(lblPic);
            grpGeneral.Controls.Add(cmbSprite);
            grpGeneral.Controls.Add(picPet);
            grpGeneral.Controls.Add(btnAddFolder);
            grpGeneral.Controls.Add(lblFolder);
            grpGeneral.Controls.Add(cmbFolder);
            grpGeneral.Controls.Add(lblName);
            grpGeneral.Controls.Add(txtName);
            grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            grpGeneral.Location = new System.Drawing.Point(7, 8);
            grpGeneral.Name = "grpGeneral";
            grpGeneral.Size = new Size(280, 124);
            grpGeneral.TabIndex = 0;
            grpGeneral.TabStop = false;
            grpGeneral.Text = "General";
            // 
            // lblPic
            // 
            lblPic.AutoSize = true;
            lblPic.Location = new System.Drawing.Point(13, 86);
            lblPic.Name = "lblPic";
            lblPic.Size = new Size(37, 15);
            lblPic.TabIndex = 7;
            lblPic.Text = "Sprite";
            // 
            // cmbSprite
            // 
            cmbSprite.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbSprite.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbSprite.BorderStyle = ButtonBorderStyle.Solid;
            cmbSprite.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbSprite.DrawDropdownHoverOutline = false;
            cmbSprite.DrawFocusRectangle = false;
            cmbSprite.DrawMode = DrawMode.OwnerDrawVariable;
            cmbSprite.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSprite.FlatStyle = FlatStyle.Flat;
            cmbSprite.ForeColor = System.Drawing.Color.Gainsboro;
            cmbSprite.Location = new System.Drawing.Point(60, 84);
            cmbSprite.Name = "cmbSprite";
            cmbSprite.Size = new Size(122, 24);
            cmbSprite.TabIndex = 3;
            cmbSprite.Text = null;
            cmbSprite.TextPadding = new Padding(2);
            cmbSprite.SelectedIndexChanged += cmbSprite_SelectedIndexChanged;
            // 
            // picPet
            // 
            picPet.BackColor = System.Drawing.Color.Black;
            picPet.Location = new System.Drawing.Point(188, 22);
            picPet.Name = "picPet";
            picPet.Size = new Size(86, 92);
            picPet.TabIndex = 8;
            picPet.TabStop = false;
            // 
            // btnAddFolder
            // 
            btnAddFolder.Location = new System.Drawing.Point(156, 54);
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.Padding = new Padding(4);
            btnAddFolder.Size = new Size(26, 24);
            btnAddFolder.TabIndex = 2;
            btnAddFolder.Text = "+";
            btnAddFolder.Click += btnAddFolder_Click;
            // 
            // lblFolder
            // 
            lblFolder.AutoSize = true;
            lblFolder.Location = new System.Drawing.Point(13, 56);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new Size(40, 15);
            lblFolder.TabIndex = 9;
            lblFolder.Text = "Folder";
            // 
            // cmbFolder
            // 
            cmbFolder.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbFolder.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbFolder.BorderStyle = ButtonBorderStyle.Solid;
            cmbFolder.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbFolder.DrawDropdownHoverOutline = false;
            cmbFolder.DrawFocusRectangle = false;
            cmbFolder.DrawMode = DrawMode.OwnerDrawVariable;
            cmbFolder.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFolder.FlatStyle = FlatStyle.Flat;
            cmbFolder.ForeColor = System.Drawing.Color.Gainsboro;
            cmbFolder.Location = new System.Drawing.Point(60, 54);
            cmbFolder.Name = "cmbFolder";
            cmbFolder.Size = new Size(95, 24);
            cmbFolder.TabIndex = 1;
            cmbFolder.Text = null;
            cmbFolder.TextPadding = new Padding(2);
            cmbFolder.SelectedIndexChanged += cmbFolder_SelectedIndexChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(13, 26);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 10;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txtName.Location = new System.Drawing.Point(60, 24);
            txtName.Name = "txtName";
            txtName.Size = new Size(122, 23);
            txtName.TabIndex = 0;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // grpSpells
            // 
            grpSpells.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grpSpells.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grpSpells.Controls.Add(btnRemove);
            grpSpells.Controls.Add(btnAdd);
            grpSpells.Controls.Add(cmbSpell);
            grpSpells.Controls.Add(lstSpells);
            grpSpells.ForeColor = System.Drawing.Color.Gainsboro;
            grpSpells.Location = new System.Drawing.Point(7, 569);
            grpSpells.Name = "grpSpells";
            grpSpells.Size = new Size(280, 174);
            grpSpells.TabIndex = 6;
            grpSpells.TabStop = false;
            grpSpells.Text = "Spells";
            // 
            // btnRemove
            // 
            btnRemove.Location = new System.Drawing.Point(171, 141);
            btnRemove.Name = "btnRemove";
            btnRemove.Padding = new Padding(4);
            btnRemove.Size = new Size(88, 24);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Remove";
            btnRemove.Click += btnRemove_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(19, 141);
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new Padding(4);
            btnAdd.Size = new Size(88, 24);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // cmbSpell
            // 
            cmbSpell.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cmbSpell.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
            cmbSpell.BorderStyle = ButtonBorderStyle.Solid;
            cmbSpell.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
            cmbSpell.DrawDropdownHoverOutline = false;
            cmbSpell.DrawFocusRectangle = false;
            cmbSpell.DrawMode = DrawMode.OwnerDrawVariable;
            cmbSpell.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSpell.FlatStyle = FlatStyle.Flat;
            cmbSpell.ForeColor = System.Drawing.Color.Gainsboro;
            cmbSpell.Location = new System.Drawing.Point(13, 111);
            cmbSpell.Name = "cmbSpell";
            cmbSpell.Size = new Size(261, 24);
            cmbSpell.TabIndex = 1;
            cmbSpell.Text = null;
            cmbSpell.TextPadding = new Padding(2);
            cmbSpell.SelectedIndexChanged += cmbSpell_SelectedIndexChanged;
            // 
            // lstSpells
            // 
            lstSpells.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lstSpells.ForeColor = System.Drawing.Color.Gainsboro;
            lstSpells.ItemHeight = 15;
            lstSpells.Location = new System.Drawing.Point(13, 26);
            lstSpells.Name = "lstSpells";
            lstSpells.Size = new Size(261, 79);
            lstSpells.TabIndex = 0;
            lstSpells.SelectedIndexChanged += lstSpells_SelectedIndexChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(262, 628);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(5, 6, 5, 6);
            btnSave.Size = new Size(175, 30);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(455, 628);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(5, 6, 5, 6);
            btnCancel.Size = new Size(175, 30);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
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
            toolStrip.Size = new Size(867, 29);
            toolStrip.TabIndex = 46;
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
            // FrmPet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize = new Size(867, 675);
            Controls.Add(toolStrip);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(pnlContainer);
            Controls.Add(grpPets);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "FrmPet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pet Editor";
            FormClosed += frmPet_FormClosed;
            Load += frmPet_Load;
            KeyDown += form_KeyDown;
            grpPets.ResumeLayout(false);
            grpPets.PerformLayout();
            pnlContainer.ResumeLayout(false);
            grpAnimation.ResumeLayout(false);
            grpAnimation.PerformLayout();
            grpImmunities.ResumeLayout(false);
            grpImmunities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTenacity).EndInit();
            grpCombat.ResumeLayout(false);
            grpCombat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAttackSpeedValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCritMultiplier).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCritChance).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudScaling).EndInit();
            grpRegen.ResumeLayout(false);
            grpRegen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMpRegen).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHpRegen).EndInit();
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAgi).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpd).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMR).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDef).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMag).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStr).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMana).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHp).EndInit();
            grpGeneral.ResumeLayout(false);
            grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPet).EndInit();
            grpSpells.ResumeLayout(false);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
        }
        private DarkGroupBox grpPets;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private Intersect.Editor.Forms.Controls.GameObjectList lstGameObjects;
        private Panel pnlContainer;
        private DarkGroupBox grpAnimation;
        private Label lblDeathAnimation;
        private DarkComboBox cmbDeathAnimation;
        private DarkGroupBox grpImmunities;
        private Label lblTenacity;
        private DarkNumericUpDown nudTenacity;
        private DarkCheckBox chkTaunt;
        private DarkCheckBox chkSleep;
        private DarkCheckBox chkTransform;
        private DarkCheckBox chkBlind;
        private DarkCheckBox chkSnare;
        private DarkCheckBox chkStun;
        private DarkCheckBox chkSilence;
        private DarkCheckBox chkKnockback;
        private DarkGroupBox grpCombat;
        private Label lblAttackSpeedValue;
        private DarkNumericUpDown nudAttackSpeedValue;
        private Label lblAttackSpeedModifier;
        private DarkComboBox cmbAttackSpeedModifier;
        private Label lblCritMultiplier;
        private DarkNumericUpDown nudCritMultiplier;
        private Label lblCritChance;
        private DarkNumericUpDown nudCritChance;
        private Label lblDamage;
        private DarkNumericUpDown nudDamage;
        private Label lblScaling;
        private DarkNumericUpDown nudScaling;
        private Label lblScalingStat;
        private DarkComboBox cmbScalingStat;
        private Label lblDamageType;
        private DarkComboBox cmbDamageType;
        private Label lblAttackAnimation;
        private DarkComboBox cmbAttackAnimation;
        private DarkGroupBox grpRegen;
        private Label lblManaRegen;
        private DarkNumericUpDown nudMpRegen;
        private Label lblHpRegen;
        private DarkNumericUpDown nudHpRegen;
        private DarkGroupBox grpStats;
        private Label label1;
        private DarkNumericUpDown nudAgi;
        private Label lblSpd;
        private DarkNumericUpDown nudSpd;
        private Label lblMR;
        private DarkNumericUpDown nudMR;
        private Label lblDef;
        private DarkNumericUpDown nudDef;
        private Label lblMag;
        private DarkNumericUpDown nudMag;
        private Label lblStr;
        private DarkNumericUpDown nudStr;
        private Label lblMana;
        private DarkNumericUpDown nudMana;
        private Label lblHP;
        private DarkNumericUpDown nudHp;
        private DarkGroupBox grpGeneral;
        private Label lblPic;
        private DarkComboBox cmbSprite;
        private PictureBox picPet;
        private DarkButton btnAddFolder;
        private Label lblFolder;
        private DarkComboBox cmbFolder;
        private Label lblName;
        private DarkTextBox txtName;
        private DarkGroupBox grpSpells;
        private DarkButton btnRemove;
        private DarkButton btnAdd;
        private DarkComboBox cmbSpell;
        private ListBox lstSpells;
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
