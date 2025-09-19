using System;
using System.Drawing;
using System.Windows.Forms;
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
            lblExp = new Label();
            nudExp = new DarkNumericUpDown();
            lblLevel = new Label();
            nudLevel = new DarkNumericUpDown();
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
            toolStrip.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)nudExp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPet).BeginInit();
            grpSpells.SuspendLayout();
            SuspendLayout();

            // toolStrip
            toolStrip.AutoSize = false;
            toolStrip.BackColor = Color.FromArgb(45, 45, 48);
            toolStrip.Items.AddRange(new ToolStripItem[]
            {
                toolStripItemNew,
                toolStripSeparator1,
                toolStripItemDelete,
                toolStripSeparator2,
                btnAlphabetical,
                toolStripSeparator4,
                toolStripItemCopy,
                toolStripItemPaste,
                toolStripSeparator3,
                toolStripItemUndo
            });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(6, 0, 1, 0);
            toolStrip.Size = new Size(1000, 30);
            toolStrip.TabIndex = 0;

            // toolStripItemNew
            toolStripItemNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemNew.Name = "toolStripItemNew";
            toolStripItemNew.Size = new Size(24, 27);
            toolStripItemNew.Text = "New";
            toolStripItemNew.Click += toolStripItemNew_Click;

            // toolStripItemDelete
            toolStripItemDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemDelete.Name = "toolStripItemDelete";
            toolStripItemDelete.Size = new Size(24, 27);
            toolStripItemDelete.Text = "Delete";
            toolStripItemDelete.Click += toolStripItemDelete_Click;

            // btnAlphabetical
            btnAlphabetical.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAlphabetical.Name = "btnAlphabetical";
            btnAlphabetical.Size = new Size(24, 27);
            btnAlphabetical.Text = "Sort";
            btnAlphabetical.Click += btnAlphabetical_Click;

            // toolStripItemCopy
            toolStripItemCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemCopy.Name = "toolStripItemCopy";
            toolStripItemCopy.Size = new Size(24, 27);
            toolStripItemCopy.Text = "Copy";
            toolStripItemCopy.Click += toolStripItemCopy_Click;

            // toolStripItemPaste
            toolStripItemPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemPaste.Name = "toolStripItemPaste";
            toolStripItemPaste.Size = new Size(24, 27);
            toolStripItemPaste.Text = "Paste";
            toolStripItemPaste.Click += toolStripItemPaste_Click;

            // toolStripItemUndo
            toolStripItemUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItemUndo.Name = "toolStripItemUndo";
            toolStripItemUndo.Size = new Size(24, 27);
            toolStripItemUndo.Text = "Undo";
            toolStripItemUndo.Click += toolStripItemUndo_Click;

            // grpPets
            grpPets.BackColor = Color.FromArgb(45, 45, 48);
            grpPets.BorderColor = Color.FromArgb(90, 90, 90);
            grpPets.Controls.Add(btnClearSearch);
            grpPets.Controls.Add(txtSearch);
            grpPets.Controls.Add(lstGameObjects);
            grpPets.ForeColor = Color.Gainsboro;
            grpPets.Location = new Point(12, 42);
            grpPets.Name = "grpPets";
            grpPets.Size = new Size(260, 640);
            grpPets.TabIndex = 1;
            grpPets.TabStop = false;
            grpPets.Text = "Pets";

            // btnClearSearch
            btnClearSearch.Location = new Point(216, 20);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Padding = new Padding(6);
            btnClearSearch.Size = new Size(32, 26);
            btnClearSearch.TabIndex = 2;
            btnClearSearch.Text = "X";
            btnClearSearch.Click += btnClearSearch_Click;

            // txtSearch
            txtSearch.Location = new Point(8, 21);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(202, 22);
            txtSearch.TabIndex = 1;
            txtSearch.Text = "Search...";
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;

            // lstGameObjects
            lstGameObjects.BackColor = Color.FromArgb(60, 63, 65);
            lstGameObjects.BorderStyle = BorderStyle.None;
            lstGameObjects.ForeColor = Color.Gainsboro;
            lstGameObjects.Location = new Point(8, 55);
            lstGameObjects.Name = "lstGameObjects";
            lstGameObjects.Size = new Size(240, 570);
            lstGameObjects.TabIndex = 0;

            // pnlContainer
            pnlContainer.AutoScroll = true;
            pnlContainer.Controls.Add(grpAnimation);
            pnlContainer.Controls.Add(grpImmunities);
            pnlContainer.Controls.Add(grpCombat);
            pnlContainer.Controls.Add(grpRegen);
            pnlContainer.Controls.Add(grpStats);
            pnlContainer.Controls.Add(grpGeneral);
            pnlContainer.Controls.Add(grpSpells);
            pnlContainer.Location = new Point(280, 42);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(700, 620);
            pnlContainer.TabIndex = 2;

            // grpAnimation
            grpAnimation.BackColor = Color.FromArgb(45, 45, 48);
            grpAnimation.BorderColor = Color.FromArgb(90, 90, 90);
            grpAnimation.Controls.Add(lblDeathAnimation);
            grpAnimation.Controls.Add(cmbDeathAnimation);
            grpAnimation.ForeColor = Color.Gainsboro;
            grpAnimation.Location = new Point(8, 520);
            grpAnimation.Name = "grpAnimation";
            grpAnimation.Size = new Size(320, 80);
            grpAnimation.TabIndex = 5;
            grpAnimation.TabStop = false;
            grpAnimation.Text = "Animations";

            lblDeathAnimation.AutoSize = true;
            lblDeathAnimation.Location = new Point(12, 28);
            lblDeathAnimation.Name = "lblDeathAnimation";
            lblDeathAnimation.Size = new Size(96, 16);
            lblDeathAnimation.Text = "Death Animation";

            cmbDeathAnimation.Location = new Point(15, 47);
            cmbDeathAnimation.Name = "cmbDeathAnimation";
            cmbDeathAnimation.Size = new Size(280, 24);
            cmbDeathAnimation.TabIndex = 1;
            cmbDeathAnimation.SelectedIndexChanged += cmbDeathAnimation_SelectedIndexChanged;

            // grpImmunities
            grpImmunities.BackColor = Color.FromArgb(45, 45, 48);
            grpImmunities.BorderColor = Color.FromArgb(90, 90, 90);
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
            grpImmunities.ForeColor = Color.Gainsboro;
            grpImmunities.Location = new Point(340, 360);
            grpImmunities.Name = "grpImmunities";
            grpImmunities.Size = new Size(340, 240);
            grpImmunities.TabIndex = 4;
            grpImmunities.TabStop = false;
            grpImmunities.Text = "Immunities";

            lblTenacity.AutoSize = true;
            lblTenacity.Location = new Point(15, 32);
            lblTenacity.Name = "lblTenacity";
            lblTenacity.Size = new Size(61, 16);
            lblTenacity.Text = "Tenacity";

            nudTenacity.Location = new Point(18, 52);
            nudTenacity.Maximum = 100;
            nudTenacity.Name = "nudTenacity";
            nudTenacity.Size = new Size(90, 22);
            nudTenacity.TabIndex = 0;
            nudTenacity.ValueChanged += nudTenacity_ValueChanged;

            chkKnockback.Location = new Point(18, 88);
            chkKnockback.Name = "chkKnockback";
            chkKnockback.Size = new Size(120, 20);
            chkKnockback.Text = "Knockback";
            chkKnockback.CheckedChanged += chkKnockback_CheckedChanged;

            chkSilence.Location = new Point(180, 88);
            chkSilence.Name = "chkSilence";
            chkSilence.Size = new Size(120, 20);
            chkSilence.Text = "Silence";
            chkSilence.CheckedChanged += chkSilence_CheckedChanged;

            chkStun.Location = new Point(18, 116);
            chkStun.Name = "chkStun";
            chkStun.Size = new Size(120, 20);
            chkStun.Text = "Stun";
            chkStun.CheckedChanged += chkStun_CheckedChanged;

            chkSnare.Location = new Point(180, 116);
            chkSnare.Name = "chkSnare";
            chkSnare.Size = new Size(120, 20);
            chkSnare.Text = "Snare";
            chkSnare.CheckedChanged += chkSnare_CheckedChanged;

            chkBlind.Location = new Point(18, 144);
            chkBlind.Name = "chkBlind";
            chkBlind.Size = new Size(120, 20);
            chkBlind.Text = "Blind";
            chkBlind.CheckedChanged += chkBlind_CheckedChanged;

            chkTransform.Location = new Point(180, 144);
            chkTransform.Name = "chkTransform";
            chkTransform.Size = new Size(120, 20);
            chkTransform.Text = "Transform";
            chkTransform.CheckedChanged += chkTransform_CheckedChanged;

            chkSleep.Location = new Point(18, 172);
            chkSleep.Name = "chkSleep";
            chkSleep.Size = new Size(120, 20);
            chkSleep.Text = "Sleep";
            chkSleep.CheckedChanged += chkSleep_CheckedChanged;

            chkTaunt.Location = new Point(180, 172);
            chkTaunt.Name = "chkTaunt";
            chkTaunt.Size = new Size(120, 20);
            chkTaunt.Text = "Taunt";
            chkTaunt.CheckedChanged += chkTaunt_CheckedChanged;

            // grpCombat
            grpCombat.BackColor = Color.FromArgb(45, 45, 48);
            grpCombat.BorderColor = Color.FromArgb(90, 90, 90);
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
            grpCombat.ForeColor = Color.Gainsboro;
            grpCombat.Location = new Point(340, 8);
            grpCombat.Name = "grpCombat";
            grpCombat.Size = new Size(340, 340);
            grpCombat.TabIndex = 3;
            grpCombat.TabStop = false;
            grpCombat.Text = "Combat";

            lblDamage.AutoSize = true;
            lblDamage.Location = new Point(15, 28);
            lblDamage.Name = "lblDamage";
            lblDamage.Size = new Size(89, 16);
            lblDamage.Text = "Base Damage";

            nudDamage.Location = new Point(18, 48);
            nudDamage.Maximum = 10000;
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new Size(140, 22);
            nudDamage.TabIndex = 0;
            nudDamage.ValueChanged += nudDamage_ValueChanged;

            lblCritChance.AutoSize = true;
            lblCritChance.Location = new Point(180, 28);
            lblCritChance.Name = "lblCritChance";
            lblCritChance.Size = new Size(93, 16);
            lblCritChance.Text = "Crit Chance %";

            nudCritChance.Location = new Point(183, 48);
            nudCritChance.Maximum = 100;
            nudCritChance.Name = "nudCritChance";
            nudCritChance.Size = new Size(140, 22);
            nudCritChance.TabIndex = 1;
            nudCritChance.ValueChanged += nudCritChance_ValueChanged;

            lblCritMultiplier.AutoSize = true;
            lblCritMultiplier.Location = new Point(15, 80);
            lblCritMultiplier.Name = "lblCritMultiplier";
            lblCritMultiplier.Size = new Size(86, 16);
            lblCritMultiplier.Text = "Crit Mult";

            nudCritMultiplier.DecimalPlaces = 2;
            nudCritMultiplier.Increment = 0.1M;
            nudCritMultiplier.Location = new Point(18, 100);
            nudCritMultiplier.Name = "nudCritMultiplier";
            nudCritMultiplier.Size = new Size(140, 22);
            nudCritMultiplier.TabIndex = 2;
            nudCritMultiplier.ValueChanged += nudCritMultiplier_ValueChanged;

            lblScaling.AutoSize = true;
            lblScaling.Location = new Point(180, 80);
            lblScaling.Name = "lblScaling";
            lblScaling.Size = new Size(92, 16);
            lblScaling.Text = "Scaling Amt";

            nudScaling.Location = new Point(183, 100);
            nudScaling.Maximum = 1000;
            nudScaling.Name = "nudScaling";
            nudScaling.Size = new Size(140, 22);
            nudScaling.TabIndex = 3;
            nudScaling.ValueChanged += nudScaling_ValueChanged;

            lblScalingStat.AutoSize = true;
            lblScalingStat.Location = new Point(15, 134);
            lblScalingStat.Name = "lblScalingStat";
            lblScalingStat.Size = new Size(76, 16);
            lblScalingStat.Text = "Scaling Stat";

            cmbScalingStat.Location = new Point(18, 154);
            cmbScalingStat.Name = "cmbScalingStat";
            cmbScalingStat.Size = new Size(140, 24);
            cmbScalingStat.TabIndex = 4;
            cmbScalingStat.SelectedIndexChanged += cmbScalingStat_SelectedIndexChanged;

            lblDamageType.AutoSize = true;
            lblDamageType.Location = new Point(180, 134);
            lblDamageType.Name = "lblDamageType";
            lblDamageType.Size = new Size(88, 16);
            lblDamageType.Text = "Damage Type";

            cmbDamageType.Location = new Point(183, 154);
            cmbDamageType.Name = "cmbDamageType";
            cmbDamageType.Size = new Size(140, 24);
            cmbDamageType.TabIndex = 5;
            cmbDamageType.SelectedIndexChanged += cmbDamageType_SelectedIndexChanged;

            lblAttackAnimation.AutoSize = true;
            lblAttackAnimation.Location = new Point(15, 190);
            lblAttackAnimation.Name = "lblAttackAnimation";
            lblAttackAnimation.Size = new Size(104, 16);
            lblAttackAnimation.Text = "Attack Animation";

            cmbAttackAnimation.Location = new Point(18, 210);
            cmbAttackAnimation.Name = "cmbAttackAnimation";
            cmbAttackAnimation.Size = new Size(305, 24);
            cmbAttackAnimation.TabIndex = 6;
            cmbAttackAnimation.SelectedIndexChanged += cmbAttackAnimation_SelectedIndexChanged;

            lblAttackSpeedModifier.AutoSize = true;
            lblAttackSpeedModifier.Location = new Point(15, 246);
            lblAttackSpeedModifier.Name = "lblAttackSpeedModifier";
            lblAttackSpeedModifier.Size = new Size(121, 16);
            lblAttackSpeedModifier.Text = "Attack Speed Mod";

            cmbAttackSpeedModifier.Location = new Point(18, 266);
            cmbAttackSpeedModifier.Name = "cmbAttackSpeedModifier";
            cmbAttackSpeedModifier.Size = new Size(140, 24);
            cmbAttackSpeedModifier.TabIndex = 7;
            cmbAttackSpeedModifier.SelectedIndexChanged += cmbAttackSpeedModifier_SelectedIndexChanged;

            lblAttackSpeedValue.AutoSize = true;
            lblAttackSpeedValue.Location = new Point(180, 246);
            lblAttackSpeedValue.Name = "lblAttackSpeedValue";
            lblAttackSpeedValue.Size = new Size(121, 16);
            lblAttackSpeedValue.Text = "Attack Speed Value";

            nudAttackSpeedValue.Location = new Point(183, 266);
            nudAttackSpeedValue.Maximum = 100000;
            nudAttackSpeedValue.Name = "nudAttackSpeedValue";
            nudAttackSpeedValue.Size = new Size(140, 22);
            nudAttackSpeedValue.TabIndex = 8;
            nudAttackSpeedValue.ValueChanged += nudAttackSpeedValue_ValueChanged;

            // grpRegen
            grpRegen.BackColor = Color.FromArgb(45, 45, 48);
            grpRegen.BorderColor = Color.FromArgb(90, 90, 90);
            grpRegen.Controls.Add(lblManaRegen);
            grpRegen.Controls.Add(nudMpRegen);
            grpRegen.Controls.Add(lblHpRegen);
            grpRegen.Controls.Add(nudHpRegen);
            grpRegen.ForeColor = Color.Gainsboro;
            grpRegen.Location = new Point(8, 360);
            grpRegen.Name = "grpRegen";
            grpRegen.Size = new Size(320, 120);
            grpRegen.TabIndex = 2;
            grpRegen.TabStop = false;
            grpRegen.Text = "Regen";

            lblHpRegen.AutoSize = true;
            lblHpRegen.Location = new Point(15, 30);
            lblHpRegen.Name = "lblHpRegen";
            lblHpRegen.Size = new Size(64, 16);
            lblHpRegen.Text = "HP Regen";

            nudHpRegen.Location = new Point(18, 50);
            nudHpRegen.Maximum = 1000;
            nudHpRegen.Name = "nudHpRegen";
            nudHpRegen.Size = new Size(120, 22);
            nudHpRegen.TabIndex = 0;
            nudHpRegen.ValueChanged += nudHpRegen_ValueChanged;

            lblManaRegen.AutoSize = true;
            lblManaRegen.Location = new Point(170, 30);
            lblManaRegen.Name = "lblManaRegen";
            lblManaRegen.Size = new Size(79, 16);
            lblManaRegen.Text = "Mana Regen";

            nudMpRegen.Location = new Point(173, 50);
            nudMpRegen.Maximum = 1000;
            nudMpRegen.Name = "nudMpRegen";
            nudMpRegen.Size = new Size(120, 22);
            nudMpRegen.TabIndex = 1;
            nudMpRegen.ValueChanged += nudMpRegen_ValueChanged;

            // grpStats
            grpStats.BackColor = Color.FromArgb(45, 45, 48);
            grpStats.BorderColor = Color.FromArgb(90, 90, 90);
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
            grpStats.ForeColor = Color.Gainsboro;
            grpStats.Location = new Point(8, 160);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(320, 190);
            grpStats.TabIndex = 1;
            grpStats.TabStop = false;
            grpStats.Text = "Stats";

            lblHP.AutoSize = true;
            lblHP.Location = new Point(15, 30);
            lblHP.Name = "lblHP";
            lblHP.Size = new Size(30, 16);
            lblHP.Text = "HP";

            nudHp.Location = new Point(18, 50);
            nudHp.Maximum = 1000000;
            nudHp.Name = "nudHp";
            nudHp.Size = new Size(120, 22);
            nudHp.TabIndex = 0;
            nudHp.ValueChanged += nudHp_ValueChanged;

            lblMana.AutoSize = true;
            lblMana.Location = new Point(170, 30);
            lblMana.Name = "lblMana";
            lblMana.Size = new Size(40, 16);
            lblMana.Text = "Mana";

            nudMana.Location = new Point(173, 50);
            nudMana.Maximum = 1000000;
            nudMana.Name = "nudMana";
            nudMana.Size = new Size(120, 22);
            nudMana.TabIndex = 1;
            nudMana.ValueChanged += nudMana_ValueChanged;

            lblStr.AutoSize = true;
            lblStr.Location = new Point(15, 82);
            lblStr.Name = "lblStr";
            lblStr.Size = new Size(46, 16);
            lblStr.Text = "Strength";

            nudStr.Location = new Point(18, 102);
            nudStr.Maximum = 1000;
            nudStr.Name = "nudStr";
            nudStr.Size = new Size(120, 22);
            nudStr.TabIndex = 2;
            nudStr.ValueChanged += nudStr_ValueChanged;

            lblMag.AutoSize = true;
            lblMag.Location = new Point(170, 82);
            lblMag.Name = "lblMag";
            lblMag.Size = new Size(41, 16);
            lblMag.Text = "Magic";

            nudMag.Location = new Point(173, 102);
            nudMag.Maximum = 1000;
            nudMag.Name = "nudMag";
            nudMag.Size = new Size(120, 22);
            nudMag.TabIndex = 3;
            nudMag.ValueChanged += nudMag_ValueChanged;

            lblDef.AutoSize = true;
            lblDef.Location = new Point(15, 134);
            lblDef.Name = "lblDef";
            lblDef.Size = new Size(55, 16);
            lblDef.Text = "Defense";

            nudDef.Location = new Point(18, 154);
            nudDef.Maximum = 1000;
            nudDef.Name = "nudDef";
            nudDef.Size = new Size(120, 22);
            nudDef.TabIndex = 4;
            nudDef.ValueChanged += nudDef_ValueChanged;

            lblMR.AutoSize = true;
            lblMR.Location = new Point(170, 134);
            lblMR.Name = "lblMR";
            lblMR.Size = new Size(85, 16);
            lblMR.Text = "Magic Resist";

            nudMR.Location = new Point(173, 154);
            nudMR.Maximum = 1000;
            nudMR.Name = "nudMR";
            nudMR.Size = new Size(120, 22);
            nudMR.TabIndex = 5;
            nudMR.ValueChanged += nudMR_ValueChanged;

            lblSpd.AutoSize = true;
            lblSpd.Location = new Point(15, 186);
            lblSpd.Name = "lblSpd";
            lblSpd.Size = new Size(74, 16);
            lblSpd.Text = "Move Speed";

            nudSpd.Location = new Point(18, 206);
            nudSpd.Maximum = 1000;
            nudSpd.Name = "nudSpd";
            nudSpd.Size = new Size(120, 22);
            nudSpd.TabIndex = 6;
            nudSpd.ValueChanged += nudSpd_ValueChanged;

            label1.AutoSize = true;
            label1.Location = new Point(170, 186);
            label1.Name = "label1";
            label1.Size = new Size(42, 16);
            label1.Text = "Agility";

            nudAgi.Location = new Point(173, 206);
            nudAgi.Maximum = 1000;
            nudAgi.Name = "nudAgi";
            nudAgi.Size = new Size(120, 22);
            nudAgi.TabIndex = 7;
            nudAgi.ValueChanged += nudAgi_ValueChanged;

            // grpGeneral
            grpGeneral.BackColor = Color.FromArgb(45, 45, 48);
            grpGeneral.BorderColor = Color.FromArgb(90, 90, 90);
            grpGeneral.Controls.Add(lblExp);
            grpGeneral.Controls.Add(nudExp);
            grpGeneral.Controls.Add(lblLevel);
            grpGeneral.Controls.Add(nudLevel);
            grpGeneral.Controls.Add(lblPic);
            grpGeneral.Controls.Add(cmbSprite);
            grpGeneral.Controls.Add(picPet);
            grpGeneral.Controls.Add(btnAddFolder);
            grpGeneral.Controls.Add(lblFolder);
            grpGeneral.Controls.Add(cmbFolder);
            grpGeneral.Controls.Add(lblName);
            grpGeneral.Controls.Add(txtName);
            grpGeneral.ForeColor = Color.Gainsboro;
            grpGeneral.Location = new Point(8, 8);
            grpGeneral.Name = "grpGeneral";
            grpGeneral.Size = new Size(320, 140);
            grpGeneral.TabIndex = 0;
            grpGeneral.TabStop = false;
            grpGeneral.Text = "General";

            lblName.AutoSize = true;
            lblName.Location = new Point(15, 28);
            lblName.Name = "lblName";
            lblName.Size = new Size(44, 16);
            lblName.Text = "Name";

            txtName.Location = new Point(68, 26);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 22);
            txtName.TabIndex = 0;
            txtName.TextChanged += txtName_TextChanged;

            lblFolder.AutoSize = true;
            lblFolder.Location = new Point(15, 60);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new Size(47, 16);
            lblFolder.Text = "Folder";

            cmbFolder.Location = new Point(68, 58);
            cmbFolder.Name = "cmbFolder";
            cmbFolder.Size = new Size(150, 24);
            cmbFolder.TabIndex = 1;
            cmbFolder.SelectedIndexChanged += cmbFolder_SelectedIndexChanged;

            btnAddFolder.Location = new Point(224, 57);
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.Padding = new Padding(4);
            btnAddFolder.Size = new Size(30, 26);
            btnAddFolder.TabIndex = 2;
            btnAddFolder.Text = "+";
            btnAddFolder.Click += btnAddFolder_Click;

            lblPic.AutoSize = true;
            lblPic.Location = new Point(15, 92);
            lblPic.Name = "lblPic";
            lblPic.Size = new Size(41, 16);
            lblPic.Text = "Sprite";

            cmbSprite.Location = new Point(68, 90);
            cmbSprite.Name = "cmbSprite";
            cmbSprite.Size = new Size(180, 24);
            cmbSprite.TabIndex = 3;
            cmbSprite.SelectedIndexChanged += cmbSprite_SelectedIndexChanged;

            picPet.BackColor = Color.Black;
            picPet.Location = new Point(260, 22);
            picPet.Name = "picPet";
            picPet.Size = new Size(48, 48);
            picPet.TabStop = false;

            lblLevel.AutoSize = true;
            lblLevel.Location = new Point(15, 124);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(39, 16);
            lblLevel.Text = "Level";

            nudLevel.Location = new Point(68, 122);
            nudLevel.Minimum = 1;
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new Size(80, 22);
            nudLevel.TabIndex = 4;
            nudLevel.Value = 1;
            nudLevel.ValueChanged += nudLevel_ValueChanged;

            lblExp.AutoSize = true;
            lblExp.Location = new Point(160, 124);
            lblExp.Name = "lblExp";
            lblExp.Size = new Size(60, 16);
            lblExp.Text = "Experience";

            nudExp.Location = new Point(226, 122);
            nudExp.Maximum = 1410065407;
            nudExp.Name = "nudExp";
            nudExp.Size = new Size(82, 22);
            nudExp.TabIndex = 5;
            nudExp.ValueChanged += nudExp_ValueChanged;

            // grpSpells
            grpSpells.BackColor = Color.FromArgb(45, 45, 48);
            grpSpells.BorderColor = Color.FromArgb(90, 90, 90);
            grpSpells.Controls.Add(btnRemove);
            grpSpells.Controls.Add(btnAdd);
            grpSpells.Controls.Add(cmbSpell);
            grpSpells.Controls.Add(lstSpells);
            grpSpells.ForeColor = Color.Gainsboro;
            grpSpells.Location = new Point(8, 600);
            grpSpells.Name = "grpSpells";
            grpSpells.Size = new Size(672, 140);
            grpSpells.TabIndex = 6;
            grpSpells.TabStop = false;
            grpSpells.Text = "Spells";

            lstSpells.BackColor = Color.FromArgb(60, 63, 65);
            lstSpells.ForeColor = Color.Gainsboro;
            lstSpells.ItemHeight = 16;
            lstSpells.Location = new Point(15, 28);
            lstSpells.Name = "lstSpells";
            lstSpells.Size = new Size(300, 84);
            lstSpells.TabIndex = 0;
            lstSpells.SelectedIndexChanged += lstSpells_SelectedIndexChanged;

            cmbSpell.Location = new Point(330, 28);
            cmbSpell.Name = "cmbSpell";
            cmbSpell.Size = new Size(200, 24);
            cmbSpell.TabIndex = 1;
            cmbSpell.SelectedIndexChanged += cmbSpell_SelectedIndexChanged;

            btnAdd.Location = new Point(540, 28);
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new Padding(4);
            btnAdd.Size = new Size(100, 26);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;

            btnRemove.Location = new Point(540, 64);
            btnRemove.Name = "btnRemove";
            btnRemove.Padding = new Padding(4);
            btnRemove.Size = new Size(100, 26);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Remove";
            btnRemove.Click += btnRemove_Click;

            // btnSave
            btnSave.Location = new Point(300, 670);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6);
            btnSave.Size = new Size(200, 32);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.Location = new Point(520, 670);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6);
            btnCancel.Size = new Size(200, 32);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            // FrmPet
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1000, 720);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(pnlContainer);
            Controls.Add(grpPets);
            Controls.Add(toolStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmPet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pet Editor";
            KeyPreview = true;
            FormClosed += frmPet_FormClosed;
            Load += frmPet_Load;
            KeyDown += form_KeyDown;

            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)nudExp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPet).EndInit();
            grpSpells.ResumeLayout(false);
            ResumeLayout(false);
        }

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
        private Label lblExp;
        private DarkNumericUpDown nudExp;
        private Label lblLevel;
        private DarkNumericUpDown nudLevel;
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
    }
}
