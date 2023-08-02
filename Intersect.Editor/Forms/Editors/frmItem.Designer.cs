using System.ComponentModel;
using System.Windows.Forms;
using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItem));
            this.grpItems = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.cmbEquipmentAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblEquipmentAnimation = new System.Windows.Forms.Label();
            this.grpStack = new DarkUI.Controls.DarkGroupBox();
            this.chkStackable = new DarkUI.Controls.DarkCheckBox();
            this.lblInvStackLimit = new System.Windows.Forms.Label();
            this.nudInvStackLimit = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBankStackLimit = new System.Windows.Forms.Label();
            this.nudBankStackLimit = new DarkUI.Controls.DarkNumericUpDown();
            this.grpCooldown = new DarkUI.Controls.DarkGroupBox();
            this.lblCooldown = new System.Windows.Forms.Label();
            this.nudCooldown = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCooldownGroup = new System.Windows.Forms.Label();
            this.cmbCooldownGroup = new DarkUI.Controls.DarkComboBox();
            this.btnAddCooldownGroup = new DarkUI.Controls.DarkButton();
            this.chkIgnoreGlobalCooldown = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreCdr = new DarkUI.Controls.DarkCheckBox();
            this.nudItemDespawnTime = new DarkUI.Controls.DarkNumericUpDown();
            this.lblDespawnTime = new System.Windows.Forms.Label();
            this.grpRequirements = new DarkUI.Controls.DarkGroupBox();
            this.lblCannotUse = new System.Windows.Forms.Label();
            this.txtCannotUse = new DarkUI.Controls.DarkTextBox();
            this.btnEditRequirements = new DarkUI.Controls.DarkButton();
            this.chkCanGuildBank = new DarkUI.Controls.DarkCheckBox();
            this.nudDeathDropChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblDeathDropChance = new System.Windows.Forms.Label();
            this.chkCanSell = new DarkUI.Controls.DarkCheckBox();
            this.chkCanTrade = new DarkUI.Controls.DarkCheckBox();
            this.chkCanBag = new DarkUI.Controls.DarkCheckBox();
            this.chkCanBank = new DarkUI.Controls.DarkCheckBox();
            this.lblAlpha = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.nudRgbaA = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRgbaB = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRgbaG = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRgbaR = new DarkUI.Controls.DarkNumericUpDown();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.cmbRarity = new DarkUI.Controls.DarkComboBox();
            this.lblRarity = new System.Windows.Forms.Label();
            this.nudPrice = new DarkUI.Controls.DarkNumericUpDown();
            this.chkCanDrop = new DarkUI.Controls.DarkCheckBox();
            this.cmbAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new DarkUI.Controls.DarkTextBox();
            this.cmbPic = new DarkUI.Controls.DarkComboBox();
            this.lblAnim = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblPic = new System.Windows.Forms.Label();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.grpConsumable = new DarkUI.Controls.DarkGroupBox();
            this.lblPercentage3 = new System.Windows.Forms.Label();
            this.nudIntervalPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPlus3 = new System.Windows.Forms.Label();
            this.nudInterval = new DarkUI.Controls.DarkNumericUpDown();
            this.lblVital = new System.Windows.Forms.Label();
            this.cmbConsume = new DarkUI.Controls.DarkComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.grpEvent = new DarkUI.Controls.DarkGroupBox();
            this.chkSingleUseEvent = new DarkUI.Controls.DarkCheckBox();
            this.cmbEvent = new DarkUI.Controls.DarkComboBox();
            this.grpBags = new DarkUI.Controls.DarkGroupBox();
            this.nudBag = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBag = new System.Windows.Forms.Label();
            this.grpSpell = new DarkUI.Controls.DarkGroupBox();
            this.chkSingleUseSpell = new DarkUI.Controls.DarkCheckBox();
            this.chkQuickCast = new DarkUI.Controls.DarkCheckBox();
            this.cmbTeachSpell = new DarkUI.Controls.DarkComboBox();
            this.lblSpell = new System.Windows.Forms.Label();
            this.grpEquipment = new DarkUI.Controls.DarkGroupBox();
            this.grpPaperdoll = new DarkUI.Controls.DarkGroupBox();
            this.picMalePaperdoll = new System.Windows.Forms.PictureBox();
            this.lblMalePaperdoll = new System.Windows.Forms.Label();
            this.cmbMalePaperdoll = new DarkUI.Controls.DarkComboBox();
            this.picFemalePaperdoll = new System.Windows.Forms.PictureBox();
            this.lblFemalePaperdoll = new System.Windows.Forms.Label();
            this.cmbFemalePaperdoll = new DarkUI.Controls.DarkComboBox();
            this.grpEffects = new DarkUI.Controls.DarkGroupBox();
            this.lstBonusEffects = new System.Windows.Forms.ListBox();
            this.lblEffectPercent = new System.Windows.Forms.Label();
            this.nudEffectPercent = new DarkUI.Controls.DarkNumericUpDown();
            this.grpRegen = new DarkUI.Controls.DarkGroupBox();
            this.nudMpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHPRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.lblHpRegen = new System.Windows.Forms.Label();
            this.lblManaRegen = new System.Windows.Forms.Label();
            this.lblRegenHint = new System.Windows.Forms.Label();
            this.grpVitalBonuses = new DarkUI.Controls.DarkGroupBox();
            this.lblPercentage2 = new System.Windows.Forms.Label();
            this.lblPercentage1 = new System.Windows.Forms.Label();
            this.nudMPPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHPPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPlus2 = new System.Windows.Forms.Label();
            this.lblPlus1 = new System.Windows.Forms.Label();
            this.nudManaBonus = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHealthBonus = new DarkUI.Controls.DarkNumericUpDown();
            this.lblManaBonus = new System.Windows.Forms.Label();
            this.lblHealthBonus = new System.Windows.Forms.Label();
            this.grpStatBonuses = new DarkUI.Controls.DarkGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudVitPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudVit = new DarkUI.Controls.DarkNumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.CurLbl = new System.Windows.Forms.Label();
            this.DmgLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudCurPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudCur = new DarkUI.Controls.DarkNumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudDmgPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudDmg = new DarkUI.Controls.DarkNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudAgiPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAgi = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPercentage5 = new System.Windows.Forms.Label();
            this.lblPercentage4 = new System.Windows.Forms.Label();
            this.lblPercentage8 = new System.Windows.Forms.Label();
            this.lblPercentage7 = new System.Windows.Forms.Label();
            this.lblPercentage6 = new System.Windows.Forms.Label();
            this.nudSpdPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMRPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDefPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMagPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudStrPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPlus5 = new System.Windows.Forms.Label();
            this.lblPlus4 = new System.Windows.Forms.Label();
            this.lblPlus8 = new System.Windows.Forms.Label();
            this.lblPlus7 = new System.Windows.Forms.Label();
            this.lblPlus6 = new System.Windows.Forms.Label();
            this.nudRange = new DarkUI.Controls.DarkNumericUpDown();
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
            this.lblRange = new System.Windows.Forms.Label();
            this.cmbEquipmentSlot = new DarkUI.Controls.DarkComboBox();
            this.lblEquipmentSlot = new System.Windows.Forms.Label();
            this.grpWeaponProperties = new DarkUI.Controls.DarkGroupBox();
            this.cmbWeaponSprite = new DarkUI.Controls.DarkComboBox();
            this.lblSpriteAttack = new System.Windows.Forms.Label();
            this.nudCritMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCritMultiplier = new System.Windows.Forms.Label();
            this.grpAttackSpeed = new DarkUI.Controls.DarkGroupBox();
            this.nudAttackSpeedValue = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAttackSpeedValue = new System.Windows.Forms.Label();
            this.cmbAttackSpeedModifier = new DarkUI.Controls.DarkComboBox();
            this.lblAttackSpeedModifier = new System.Windows.Forms.Label();
            this.nudScaling = new DarkUI.Controls.DarkNumericUpDown();
            this.nudCritChance = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbProjectile = new DarkUI.Controls.DarkComboBox();
            this.cmbScalingStat = new DarkUI.Controls.DarkComboBox();
            this.lblScalingStat = new System.Windows.Forms.Label();
            this.lblScalingAmount = new System.Windows.Forms.Label();
            this.cmbDamageType = new DarkUI.Controls.DarkComboBox();
            this.lblDamageType = new System.Windows.Forms.Label();
            this.lblCritChance = new System.Windows.Forms.Label();
            this.cmbAttackAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblAttackAnimation = new System.Windows.Forms.Label();
            this.chk2Hand = new DarkUI.Controls.DarkCheckBox();
            this.lblToolType = new System.Windows.Forms.Label();
            this.cmbToolType = new DarkUI.Controls.DarkComboBox();
            this.lblProjectile = new System.Windows.Forms.Label();
            this.lblDamage = new System.Windows.Forms.Label();
            this.grpShieldProperties = new DarkUI.Controls.DarkGroupBox();
            this.nudBlockDmgAbs = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBlockDmgAbs = new System.Windows.Forms.Label();
            this.nudBlockAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBlockAmount = new System.Windows.Forms.Label();
            this.nudBlockChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBlockChance = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
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
            this.tooltips = new System.Windows.Forms.ToolTip(this.components);
            this.grpItems.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.grpStack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvStackLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankStackLimit)).BeginInit();
            this.grpCooldown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemDespawnTime)).BeginInit();
            this.grpRequirements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeathDropChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.grpConsumable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.grpEvent.SuspendLayout();
            this.grpBags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBag)).BeginInit();
            this.grpSpell.SuspendLayout();
            this.grpEquipment.SuspendLayout();
            this.grpPaperdoll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMalePaperdoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFemalePaperdoll)).BeginInit();
            this.grpEffects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEffectPercent)).BeginInit();
            this.grpRegen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPRegen)).BeginInit();
            this.grpVitalBonuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMPPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudManaBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealthBonus)).BeginInit();
            this.grpStatBonuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVitPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDmgPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDmg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgiPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpdPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMRPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            this.grpWeaponProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).BeginInit();
            this.grpAttackSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).BeginInit();
            this.grpShieldProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockDmgAbs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockChance)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpItems
            // 
            this.grpItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpItems.Controls.Add(this.btnClearSearch);
            this.grpItems.Controls.Add(this.txtSearch);
            this.grpItems.Controls.Add(this.lstGameObjects);
            this.grpItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpItems.Location = new System.Drawing.Point(4, 34);
            this.grpItems.Margin = new System.Windows.Forms.Padding(4);
            this.grpItems.Name = "grpItems";
            this.grpItems.Padding = new System.Windows.Forms.Padding(4);
            this.grpItems.Size = new System.Drawing.Size(267, 684);
            this.grpItems.TabIndex = 1;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Items";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(233, 23);
            this.btnClearSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnClearSearch.Size = new System.Drawing.Size(24, 25);
            this.btnClearSearch.TabIndex = 31;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(7, 23);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(219, 22);
            this.txtSearch.TabIndex = 30;
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
            this.lstGameObjects.Location = new System.Drawing.Point(7, 57);
            this.lstGameObjects.Margin = new System.Windows.Forms.Padding(4);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(253, 615);
            this.lstGameObjects.TabIndex = 29;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1152, 729);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancel.Size = new System.Drawing.Size(253, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(891, 729);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSave.Size = new System.Drawing.Size(253, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.cmbEquipmentAnimation);
            this.grpGeneral.Controls.Add(this.lblEquipmentAnimation);
            this.grpGeneral.Controls.Add(this.grpStack);
            this.grpGeneral.Controls.Add(this.grpCooldown);
            this.grpGeneral.Controls.Add(this.nudItemDespawnTime);
            this.grpGeneral.Controls.Add(this.lblDespawnTime);
            this.grpGeneral.Controls.Add(this.grpRequirements);
            this.grpGeneral.Controls.Add(this.chkCanGuildBank);
            this.grpGeneral.Controls.Add(this.nudDeathDropChance);
            this.grpGeneral.Controls.Add(this.lblDeathDropChance);
            this.grpGeneral.Controls.Add(this.chkCanSell);
            this.grpGeneral.Controls.Add(this.chkCanTrade);
            this.grpGeneral.Controls.Add(this.chkCanBag);
            this.grpGeneral.Controls.Add(this.chkCanBank);
            this.grpGeneral.Controls.Add(this.lblAlpha);
            this.grpGeneral.Controls.Add(this.lblBlue);
            this.grpGeneral.Controls.Add(this.lblGreen);
            this.grpGeneral.Controls.Add(this.lblRed);
            this.grpGeneral.Controls.Add(this.nudRgbaA);
            this.grpGeneral.Controls.Add(this.nudRgbaB);
            this.grpGeneral.Controls.Add(this.nudRgbaG);
            this.grpGeneral.Controls.Add(this.nudRgbaR);
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.cmbRarity);
            this.grpGeneral.Controls.Add(this.lblRarity);
            this.grpGeneral.Controls.Add(this.nudPrice);
            this.grpGeneral.Controls.Add(this.chkCanDrop);
            this.grpGeneral.Controls.Add(this.cmbAnimation);
            this.grpGeneral.Controls.Add(this.lblDesc);
            this.grpGeneral.Controls.Add(this.txtDesc);
            this.grpGeneral.Controls.Add(this.cmbPic);
            this.grpGeneral.Controls.Add(this.lblAnim);
            this.grpGeneral.Controls.Add(this.lblPrice);
            this.grpGeneral.Controls.Add(this.lblPic);
            this.grpGeneral.Controls.Add(this.picItem);
            this.grpGeneral.Controls.Add(this.lblType);
            this.grpGeneral.Controls.Add(this.cmbType);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.Controls.Add(this.grpConsumable);
            this.grpGeneral.Controls.Add(this.grpEvent);
            this.grpGeneral.Controls.Add(this.grpBags);
            this.grpGeneral.Controls.Add(this.grpSpell);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(3, 1);
            this.grpGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.grpGeneral.Size = new System.Drawing.Size(1077, 583);
            this.grpGeneral.TabIndex = 2;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // cmbEquipmentAnimation
            // 
            this.cmbEquipmentAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEquipmentAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEquipmentAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEquipmentAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEquipmentAnimation.DrawDropdownHoverOutline = false;
            this.cmbEquipmentAnimation.DrawFocusRectangle = false;
            this.cmbEquipmentAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEquipmentAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipmentAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEquipmentAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEquipmentAnimation.FormattingEnabled = true;
            this.cmbEquipmentAnimation.Items.AddRange(new object[] {
            "None",
            "None"});
            this.cmbEquipmentAnimation.Location = new System.Drawing.Point(366, 283);
            this.cmbEquipmentAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEquipmentAnimation.Name = "cmbEquipmentAnimation";
            this.cmbEquipmentAnimation.Size = new System.Drawing.Size(290, 23);
            this.cmbEquipmentAnimation.TabIndex = 104;
            this.cmbEquipmentAnimation.Text = "None";
            this.cmbEquipmentAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblEquipmentAnimation
            // 
            this.lblEquipmentAnimation.AutoSize = true;
            this.lblEquipmentAnimation.Location = new System.Drawing.Point(364, 257);
            this.lblEquipmentAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEquipmentAnimation.Name = "lblEquipmentAnimation";
            this.lblEquipmentAnimation.Size = new System.Drawing.Size(136, 16);
            this.lblEquipmentAnimation.TabIndex = 103;
            this.lblEquipmentAnimation.Text = "Equipment Animation:";
            // 
            // grpStack
            // 
            this.grpStack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStack.Controls.Add(this.chkStackable);
            this.grpStack.Controls.Add(this.lblInvStackLimit);
            this.grpStack.Controls.Add(this.nudInvStackLimit);
            this.grpStack.Controls.Add(this.lblBankStackLimit);
            this.grpStack.Controls.Add(this.nudBankStackLimit);
            this.grpStack.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStack.Location = new System.Drawing.Point(681, 327);
            this.grpStack.Margin = new System.Windows.Forms.Padding(4);
            this.grpStack.Name = "grpStack";
            this.grpStack.Padding = new System.Windows.Forms.Padding(4);
            this.grpStack.Size = new System.Drawing.Size(376, 130);
            this.grpStack.TabIndex = 88;
            this.grpStack.TabStop = false;
            this.grpStack.Text = "Stack Options:";
            this.grpStack.UseWaitCursor = true;
            // 
            // chkStackable
            // 
            this.chkStackable.AutoSize = true;
            this.chkStackable.Location = new System.Drawing.Point(15, 30);
            this.chkStackable.Margin = new System.Windows.Forms.Padding(4);
            this.chkStackable.Name = "chkStackable";
            this.chkStackable.Size = new System.Drawing.Size(97, 20);
            this.chkStackable.TabIndex = 27;
            this.chkStackable.Text = "Stackable?";
            this.chkStackable.UseWaitCursor = true;
            this.chkStackable.CheckedChanged += new System.EventHandler(this.chkStackable_CheckedChanged);
            // 
            // lblInvStackLimit
            // 
            this.lblInvStackLimit.AutoSize = true;
            this.lblInvStackLimit.Location = new System.Drawing.Point(11, 64);
            this.lblInvStackLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInvStackLimit.Name = "lblInvStackLimit";
            this.lblInvStackLimit.Size = new System.Drawing.Size(131, 16);
            this.lblInvStackLimit.TabIndex = 95;
            this.lblInvStackLimit.Text = "Inventory Stack Limit:";
            this.lblInvStackLimit.UseWaitCursor = true;
            // 
            // nudInvStackLimit
            // 
            this.nudInvStackLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudInvStackLimit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudInvStackLimit.Location = new System.Drawing.Point(164, 55);
            this.nudInvStackLimit.Margin = new System.Windows.Forms.Padding(4);
            this.nudInvStackLimit.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudInvStackLimit.Name = "nudInvStackLimit";
            this.nudInvStackLimit.Size = new System.Drawing.Size(201, 22);
            this.nudInvStackLimit.TabIndex = 97;
            this.nudInvStackLimit.UseWaitCursor = true;
            this.nudInvStackLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudInvStackLimit.ValueChanged += new System.EventHandler(this.nudInvStackLimit_ValueChanged);
            // 
            // lblBankStackLimit
            // 
            this.lblBankStackLimit.AutoSize = true;
            this.lblBankStackLimit.Location = new System.Drawing.Point(11, 94);
            this.lblBankStackLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBankStackLimit.Name = "lblBankStackLimit";
            this.lblBankStackLimit.Size = new System.Drawing.Size(108, 16);
            this.lblBankStackLimit.TabIndex = 96;
            this.lblBankStackLimit.Text = "Bank Stack Limit:";
            this.lblBankStackLimit.UseWaitCursor = true;
            // 
            // nudBankStackLimit
            // 
            this.nudBankStackLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBankStackLimit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBankStackLimit.Location = new System.Drawing.Point(164, 91);
            this.nudBankStackLimit.Margin = new System.Windows.Forms.Padding(4);
            this.nudBankStackLimit.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudBankStackLimit.Name = "nudBankStackLimit";
            this.nudBankStackLimit.Size = new System.Drawing.Size(201, 22);
            this.nudBankStackLimit.TabIndex = 98;
            this.nudBankStackLimit.UseWaitCursor = true;
            this.nudBankStackLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBankStackLimit.ValueChanged += new System.EventHandler(this.nudBankStackLimit_ValueChanged);
            // 
            // grpCooldown
            // 
            this.grpCooldown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCooldown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCooldown.Controls.Add(this.lblCooldown);
            this.grpCooldown.Controls.Add(this.nudCooldown);
            this.grpCooldown.Controls.Add(this.lblCooldownGroup);
            this.grpCooldown.Controls.Add(this.cmbCooldownGroup);
            this.grpCooldown.Controls.Add(this.btnAddCooldownGroup);
            this.grpCooldown.Controls.Add(this.chkIgnoreGlobalCooldown);
            this.grpCooldown.Controls.Add(this.chkIgnoreCdr);
            this.grpCooldown.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCooldown.Location = new System.Drawing.Point(681, 151);
            this.grpCooldown.Margin = new System.Windows.Forms.Padding(4);
            this.grpCooldown.Name = "grpCooldown";
            this.grpCooldown.Padding = new System.Windows.Forms.Padding(4);
            this.grpCooldown.Size = new System.Drawing.Size(376, 160);
            this.grpCooldown.TabIndex = 74;
            this.grpCooldown.TabStop = false;
            this.grpCooldown.Text = "Cooldown Options:";
            this.grpCooldown.UseWaitCursor = true;
            // 
            // lblCooldown
            // 
            this.lblCooldown.AutoSize = true;
            this.lblCooldown.Location = new System.Drawing.Point(11, 25);
            this.lblCooldown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCooldown.Name = "lblCooldown";
            this.lblCooldown.Size = new System.Drawing.Size(99, 16);
            this.lblCooldown.TabIndex = 38;
            this.lblCooldown.Text = "Cooldown (ms):";
            this.lblCooldown.UseWaitCursor = true;
            // 
            // nudCooldown
            // 
            this.nudCooldown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCooldown.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCooldown.Location = new System.Drawing.Point(137, 22);
            this.nudCooldown.Margin = new System.Windows.Forms.Padding(4);
            this.nudCooldown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudCooldown.Name = "nudCooldown";
            this.nudCooldown.Size = new System.Drawing.Size(228, 22);
            this.nudCooldown.TabIndex = 39;
            this.nudCooldown.UseWaitCursor = true;
            this.nudCooldown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCooldown.ValueChanged += new System.EventHandler(this.nudCooldown_ValueChanged);
            // 
            // lblCooldownGroup
            // 
            this.lblCooldownGroup.AutoSize = true;
            this.lblCooldownGroup.Location = new System.Drawing.Point(11, 55);
            this.lblCooldownGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCooldownGroup.Name = "lblCooldownGroup";
            this.lblCooldownGroup.Size = new System.Drawing.Size(110, 16);
            this.lblCooldownGroup.TabIndex = 50;
            this.lblCooldownGroup.Text = "Cooldown Group:";
            this.lblCooldownGroup.UseWaitCursor = true;
            // 
            // cmbCooldownGroup
            // 
            this.cmbCooldownGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCooldownGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCooldownGroup.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCooldownGroup.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCooldownGroup.DrawDropdownHoverOutline = false;
            this.cmbCooldownGroup.DrawFocusRectangle = false;
            this.cmbCooldownGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCooldownGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCooldownGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCooldownGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCooldownGroup.FormattingEnabled = true;
            this.cmbCooldownGroup.Location = new System.Drawing.Point(137, 54);
            this.cmbCooldownGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCooldownGroup.Name = "cmbCooldownGroup";
            this.cmbCooldownGroup.Size = new System.Drawing.Size(191, 23);
            this.cmbCooldownGroup.TabIndex = 51;
            this.cmbCooldownGroup.Text = null;
            this.cmbCooldownGroup.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCooldownGroup.UseWaitCursor = true;
            this.cmbCooldownGroup.SelectedIndexChanged += new System.EventHandler(this.cmbCooldownGroup_SelectedIndexChanged);
            // 
            // btnAddCooldownGroup
            // 
            this.btnAddCooldownGroup.Location = new System.Drawing.Point(337, 54);
            this.btnAddCooldownGroup.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddCooldownGroup.Name = "btnAddCooldownGroup";
            this.btnAddCooldownGroup.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnAddCooldownGroup.Size = new System.Drawing.Size(28, 26);
            this.btnAddCooldownGroup.TabIndex = 52;
            this.btnAddCooldownGroup.Text = "+";
            this.btnAddCooldownGroup.UseWaitCursor = true;
            this.btnAddCooldownGroup.Click += new System.EventHandler(this.btnAddCooldownGroup_Click);
            // 
            // chkIgnoreGlobalCooldown
            // 
            this.chkIgnoreGlobalCooldown.AutoSize = true;
            this.chkIgnoreGlobalCooldown.Location = new System.Drawing.Point(15, 89);
            this.chkIgnoreGlobalCooldown.Margin = new System.Windows.Forms.Padding(4);
            this.chkIgnoreGlobalCooldown.Name = "chkIgnoreGlobalCooldown";
            this.chkIgnoreGlobalCooldown.Size = new System.Drawing.Size(180, 20);
            this.chkIgnoreGlobalCooldown.TabIndex = 53;
            this.chkIgnoreGlobalCooldown.Text = "Ignore Global Cooldown?";
            this.chkIgnoreGlobalCooldown.UseWaitCursor = true;
            this.chkIgnoreGlobalCooldown.CheckedChanged += new System.EventHandler(this.chkIgnoreGlobalCooldown_CheckedChanged);
            // 
            // chkIgnoreCdr
            // 
            this.chkIgnoreCdr.AutoSize = true;
            this.chkIgnoreCdr.Location = new System.Drawing.Point(15, 119);
            this.chkIgnoreCdr.Margin = new System.Windows.Forms.Padding(4);
            this.chkIgnoreCdr.Name = "chkIgnoreCdr";
            this.chkIgnoreCdr.Size = new System.Drawing.Size(201, 20);
            this.chkIgnoreCdr.TabIndex = 87;
            this.chkIgnoreCdr.Text = "Ignore Cooldown Reduction?";
            this.chkIgnoreCdr.UseWaitCursor = true;
            this.chkIgnoreCdr.CheckedChanged += new System.EventHandler(this.chkIgnoreCdr_CheckedChanged);
            // 
            // nudItemDespawnTime
            // 
            this.nudItemDespawnTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudItemDespawnTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudItemDespawnTime.Location = new System.Drawing.Point(15, 534);
            this.nudItemDespawnTime.Margin = new System.Windows.Forms.Padding(4);
            this.nudItemDespawnTime.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudItemDespawnTime.Name = "nudItemDespawnTime";
            this.nudItemDespawnTime.Size = new System.Drawing.Size(309, 22);
            this.nudItemDespawnTime.TabIndex = 102;
            this.nudItemDespawnTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudItemDespawnTime.ValueChanged += new System.EventHandler(this.nudItemDespawnTime_ValueChanged);
            // 
            // lblDespawnTime
            // 
            this.lblDespawnTime.AutoSize = true;
            this.lblDespawnTime.Location = new System.Drawing.Point(11, 511);
            this.lblDespawnTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDespawnTime.Name = "lblDespawnTime";
            this.lblDespawnTime.Size = new System.Drawing.Size(290, 16);
            this.lblDespawnTime.TabIndex = 101;
            this.lblDespawnTime.Text = "Item Despawn Time (ms):     [0 for server default]";
            // 
            // grpRequirements
            // 
            this.grpRequirements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRequirements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRequirements.Controls.Add(this.lblCannotUse);
            this.grpRequirements.Controls.Add(this.txtCannotUse);
            this.grpRequirements.Controls.Add(this.btnEditRequirements);
            this.grpRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRequirements.Location = new System.Drawing.Point(681, 22);
            this.grpRequirements.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRequirements.Name = "grpRequirements";
            this.grpRequirements.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRequirements.Size = new System.Drawing.Size(376, 113);
            this.grpRequirements.TabIndex = 100;
            this.grpRequirements.TabStop = false;
            this.grpRequirements.Text = "Requirements";
            // 
            // lblCannotUse
            // 
            this.lblCannotUse.AutoSize = true;
            this.lblCannotUse.Location = new System.Drawing.Point(7, 58);
            this.lblCannotUse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCannotUse.Name = "lblCannotUse";
            this.lblCannotUse.Size = new System.Drawing.Size(140, 16);
            this.lblCannotUse.TabIndex = 54;
            this.lblCannotUse.Text = "Cannot Use Message:";
            // 
            // txtCannotUse
            // 
            this.txtCannotUse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCannotUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCannotUse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCannotUse.Location = new System.Drawing.Point(11, 78);
            this.txtCannotUse.Margin = new System.Windows.Forms.Padding(4);
            this.txtCannotUse.Name = "txtCannotUse";
            this.txtCannotUse.Size = new System.Drawing.Size(355, 22);
            this.txtCannotUse.TabIndex = 53;
            this.txtCannotUse.TextChanged += new System.EventHandler(this.txtCannotUse_TextChanged);
            // 
            // btnEditRequirements
            // 
            this.btnEditRequirements.Location = new System.Drawing.Point(11, 22);
            this.btnEditRequirements.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditRequirements.Name = "btnEditRequirements";
            this.btnEditRequirements.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnEditRequirements.Size = new System.Drawing.Size(356, 28);
            this.btnEditRequirements.TabIndex = 0;
            this.btnEditRequirements.Text = "Edit Usage Requirements";
            this.btnEditRequirements.Click += new System.EventHandler(this.btnEditRequirements_Click);
            // 
            // chkCanGuildBank
            // 
            this.chkCanGuildBank.AutoSize = true;
            this.chkCanGuildBank.Location = new System.Drawing.Point(183, 410);
            this.chkCanGuildBank.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanGuildBank.Name = "chkCanGuildBank";
            this.chkCanGuildBank.Size = new System.Drawing.Size(128, 20);
            this.chkCanGuildBank.TabIndex = 99;
            this.chkCanGuildBank.Text = "Can Guild Bank?";
            this.chkCanGuildBank.CheckedChanged += new System.EventHandler(this.chkCanGuildBank_CheckedChanged);
            // 
            // nudDeathDropChance
            // 
            this.nudDeathDropChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDeathDropChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDeathDropChance.Location = new System.Drawing.Point(200, 471);
            this.nudDeathDropChance.Margin = new System.Windows.Forms.Padding(4);
            this.nudDeathDropChance.Name = "nudDeathDropChance";
            this.nudDeathDropChance.Size = new System.Drawing.Size(124, 22);
            this.nudDeathDropChance.TabIndex = 94;
            this.nudDeathDropChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDeathDropChance.ValueChanged += new System.EventHandler(this.nudDeathDropChance_ValueChanged);
            // 
            // lblDeathDropChance
            // 
            this.lblDeathDropChance.AutoSize = true;
            this.lblDeathDropChance.Location = new System.Drawing.Point(11, 476);
            this.lblDeathDropChance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeathDropChance.Name = "lblDeathDropChance";
            this.lblDeathDropChance.Size = new System.Drawing.Size(167, 16);
            this.lblDeathDropChance.TabIndex = 93;
            this.lblDeathDropChance.Text = "Drop chance on Death (%):";
            // 
            // chkCanSell
            // 
            this.chkCanSell.AutoSize = true;
            this.chkCanSell.Location = new System.Drawing.Point(31, 437);
            this.chkCanSell.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanSell.Name = "chkCanSell";
            this.chkCanSell.Size = new System.Drawing.Size(86, 20);
            this.chkCanSell.TabIndex = 92;
            this.chkCanSell.Text = "Can Sell?";
            this.chkCanSell.CheckedChanged += new System.EventHandler(this.chkCanSell_CheckedChanged);
            // 
            // chkCanTrade
            // 
            this.chkCanTrade.AutoSize = true;
            this.chkCanTrade.Location = new System.Drawing.Point(31, 410);
            this.chkCanTrade.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanTrade.Name = "chkCanTrade";
            this.chkCanTrade.Size = new System.Drawing.Size(100, 20);
            this.chkCanTrade.TabIndex = 91;
            this.chkCanTrade.Text = "Can Trade?";
            this.chkCanTrade.CheckedChanged += new System.EventHandler(this.chkCanTrade_CheckedChanged);
            // 
            // chkCanBag
            // 
            this.chkCanBag.AutoSize = true;
            this.chkCanBag.Location = new System.Drawing.Point(183, 437);
            this.chkCanBag.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanBag.Name = "chkCanBag";
            this.chkCanBag.Size = new System.Drawing.Size(88, 20);
            this.chkCanBag.TabIndex = 90;
            this.chkCanBag.Text = "Can Bag?";
            this.chkCanBag.CheckedChanged += new System.EventHandler(this.chkCanBag_CheckedChanged);
            // 
            // chkCanBank
            // 
            this.chkCanBank.AutoSize = true;
            this.chkCanBank.Location = new System.Drawing.Point(183, 383);
            this.chkCanBank.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanBank.Name = "chkCanBank";
            this.chkCanBank.Size = new System.Drawing.Size(94, 20);
            this.chkCanBank.TabIndex = 89;
            this.chkCanBank.Text = "Can Bank?";
            this.chkCanBank.CheckedChanged += new System.EventHandler(this.chkCanBank_CheckedChanged);
            // 
            // lblAlpha
            // 
            this.lblAlpha.AutoSize = true;
            this.lblAlpha.Location = new System.Drawing.Point(516, 123);
            this.lblAlpha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(45, 16);
            this.lblAlpha.TabIndex = 86;
            this.lblAlpha.Text = "Alpha:";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(363, 123);
            this.lblBlue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(37, 16);
            this.lblBlue.TabIndex = 85;
            this.lblBlue.Text = "Blue:";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(516, 91);
            this.lblGreen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(47, 16);
            this.lblGreen.TabIndex = 84;
            this.lblGreen.Text = "Green:";
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(363, 91);
            this.lblRed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(36, 16);
            this.lblRed.TabIndex = 83;
            this.lblRed.Text = "Red:";
            // 
            // nudRgbaA
            // 
            this.nudRgbaA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaA.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaA.Location = new System.Drawing.Point(576, 121);
            this.nudRgbaA.Margin = new System.Windows.Forms.Padding(4);
            this.nudRgbaA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaA.Name = "nudRgbaA";
            this.nudRgbaA.Size = new System.Drawing.Size(80, 22);
            this.nudRgbaA.TabIndex = 82;
            this.nudRgbaA.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaA.ValueChanged += new System.EventHandler(this.nudRgbaA_ValueChanged);
            // 
            // nudRgbaB
            // 
            this.nudRgbaB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaB.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaB.Location = new System.Drawing.Point(412, 121);
            this.nudRgbaB.Margin = new System.Windows.Forms.Padding(4);
            this.nudRgbaB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaB.Name = "nudRgbaB";
            this.nudRgbaB.Size = new System.Drawing.Size(80, 22);
            this.nudRgbaB.TabIndex = 81;
            this.nudRgbaB.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaB.ValueChanged += new System.EventHandler(this.nudRgbaB_ValueChanged);
            // 
            // nudRgbaG
            // 
            this.nudRgbaG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaG.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaG.Location = new System.Drawing.Point(576, 89);
            this.nudRgbaG.Margin = new System.Windows.Forms.Padding(4);
            this.nudRgbaG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaG.Name = "nudRgbaG";
            this.nudRgbaG.Size = new System.Drawing.Size(80, 22);
            this.nudRgbaG.TabIndex = 80;
            this.nudRgbaG.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaG.ValueChanged += new System.EventHandler(this.nudRgbaG_ValueChanged);
            // 
            // nudRgbaR
            // 
            this.nudRgbaR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaR.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaR.Location = new System.Drawing.Point(412, 89);
            this.nudRgbaR.Margin = new System.Windows.Forms.Padding(4);
            this.nudRgbaR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaR.Name = "nudRgbaR";
            this.nudRgbaR.Size = new System.Drawing.Size(80, 22);
            this.nudRgbaR.TabIndex = 79;
            this.nudRgbaR.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaR.ValueChanged += new System.EventHandler(this.nudRgbaR_ValueChanged);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(300, 71);
            this.btnAddFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnAddFolder.Size = new System.Drawing.Size(24, 26);
            this.btnAddFolder.TabIndex = 49;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(11, 76);
            this.lblFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(49, 16);
            this.lblFolder.TabIndex = 48;
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
            this.cmbFolder.Location = new System.Drawing.Point(71, 71);
            this.cmbFolder.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(220, 23);
            this.cmbFolder.TabIndex = 47;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // cmbRarity
            // 
            this.cmbRarity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbRarity.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbRarity.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbRarity.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbRarity.DrawDropdownHoverOutline = false;
            this.cmbRarity.DrawFocusRectangle = false;
            this.cmbRarity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRarity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRarity.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbRarity.FormattingEnabled = true;
            this.cmbRarity.Items.AddRange(new object[] {
            "None",
            "Common",
            "Uncommon",
            "Rare",
            "Epic",
            "Legendary"});
            this.cmbRarity.Location = new System.Drawing.Point(71, 156);
            this.cmbRarity.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRarity.Name = "cmbRarity";
            this.cmbRarity.Size = new System.Drawing.Size(252, 23);
            this.cmbRarity.TabIndex = 41;
            this.cmbRarity.Text = "None";
            this.cmbRarity.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbRarity.SelectedIndexChanged += new System.EventHandler(this.cmbRarity_SelectedIndexChanged);
            // 
            // lblRarity
            // 
            this.lblRarity.AutoSize = true;
            this.lblRarity.Location = new System.Drawing.Point(11, 160);
            this.lblRarity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRarity.Name = "lblRarity";
            this.lblRarity.Size = new System.Drawing.Size(45, 16);
            this.lblRarity.TabIndex = 40;
            this.lblRarity.Text = "Rarity:";
            // 
            // nudPrice
            // 
            this.nudPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPrice.Location = new System.Drawing.Point(367, 178);
            this.nudPrice.Margin = new System.Windows.Forms.Padding(4);
            this.nudPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(289, 22);
            this.nudPrice.TabIndex = 37;
            this.nudPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice.ValueChanged += new System.EventHandler(this.nudPrice_ValueChanged);
            // 
            // chkCanDrop
            // 
            this.chkCanDrop.AutoSize = true;
            this.chkCanDrop.Location = new System.Drawing.Point(31, 383);
            this.chkCanDrop.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanDrop.Name = "chkCanDrop";
            this.chkCanDrop.Size = new System.Drawing.Size(93, 20);
            this.chkCanDrop.TabIndex = 26;
            this.chkCanDrop.Text = "Can Drop?";
            this.chkCanDrop.CheckedChanged += new System.EventHandler(this.chkBound_CheckedChanged);
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
            this.cmbAnimation.Location = new System.Drawing.Point(367, 230);
            this.cmbAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(288, 23);
            this.cmbAnimation.TabIndex = 14;
            this.cmbAnimation.Text = null;
            this.cmbAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAnimation_SelectedIndexChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(15, 187);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(42, 16);
            this.lblDesc.TabIndex = 13;
            this.lblDesc.Text = "Desc:";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDesc.Location = new System.Drawing.Point(16, 207);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(307, 163);
            this.txtDesc.TabIndex = 12;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // cmbPic
            // 
            this.cmbPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbPic.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPic.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbPic.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbPic.DrawDropdownHoverOutline = false;
            this.cmbPic.DrawFocusRectangle = false;
            this.cmbPic.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPic.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbPic.FormattingEnabled = true;
            this.cmbPic.Items.AddRange(new object[] {
            "None"});
            this.cmbPic.Location = new System.Drawing.Point(373, 48);
            this.cmbPic.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPic.Name = "cmbPic";
            this.cmbPic.Size = new System.Drawing.Size(209, 23);
            this.cmbPic.TabIndex = 11;
            this.cmbPic.Text = "None";
            this.cmbPic.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbPic.SelectedIndexChanged += new System.EventHandler(this.cmbPic_SelectedIndexChanged);
            // 
            // lblAnim
            // 
            this.lblAnim.AutoSize = true;
            this.lblAnim.Location = new System.Drawing.Point(363, 210);
            this.lblAnim.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnim.Name = "lblAnim";
            this.lblAnim.Size = new System.Drawing.Size(112, 16);
            this.lblAnim.TabIndex = 9;
            this.lblAnim.Text = "Animation on Use";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(363, 158);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(41, 16);
            this.lblPrice.TabIndex = 7;
            this.lblPrice.Text = "Price:";
            // 
            // lblPic
            // 
            this.lblPic.AutoSize = true;
            this.lblPic.Location = new System.Drawing.Point(369, 27);
            this.lblPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(29, 16);
            this.lblPic.TabIndex = 6;
            this.lblPic.Text = "Pic:";
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.Black;
            this.picItem.Location = new System.Drawing.Point(592, 22);
            this.picItem.Margin = new System.Windows.Forms.Padding(4);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(64, 59);
            this.picItem.TabIndex = 4;
            this.picItem.TabStop = false;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(11, 118);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(42, 16);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbType.DrawDropdownHoverOutline = false;
            this.cmbType.DrawFocusRectangle = false;
            this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "None",
            "Equipment",
            "Consumable",
            "Currency",
            "Spell",
            "Event",
            "Bag"});
            this.cmbType.Location = new System.Drawing.Point(71, 114);
            this.cmbType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(252, 23);
            this.cmbType.TabIndex = 2;
            this.cmbType.Text = "None";
            this.cmbType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(11, 33);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(71, 31);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(253, 22);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // grpConsumable
            // 
            this.grpConsumable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpConsumable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpConsumable.Controls.Add(this.lblPercentage3);
            this.grpConsumable.Controls.Add(this.nudIntervalPercentage);
            this.grpConsumable.Controls.Add(this.lblPlus3);
            this.grpConsumable.Controls.Add(this.nudInterval);
            this.grpConsumable.Controls.Add(this.lblVital);
            this.grpConsumable.Controls.Add(this.cmbConsume);
            this.grpConsumable.Controls.Add(this.lblInterval);
            this.grpConsumable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpConsumable.Location = new System.Drawing.Point(367, 327);
            this.grpConsumable.Margin = new System.Windows.Forms.Padding(4);
            this.grpConsumable.Name = "grpConsumable";
            this.grpConsumable.Padding = new System.Windows.Forms.Padding(4);
            this.grpConsumable.Size = new System.Drawing.Size(289, 160);
            this.grpConsumable.TabIndex = 12;
            this.grpConsumable.TabStop = false;
            this.grpConsumable.Text = "Consumable";
            this.grpConsumable.Visible = false;
            // 
            // lblPercentage3
            // 
            this.lblPercentage3.AutoSize = true;
            this.lblPercentage3.Location = new System.Drawing.Point(260, 112);
            this.lblPercentage3.Name = "lblPercentage3";
            this.lblPercentage3.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage3.TabIndex = 73;
            this.lblPercentage3.Text = "%";
            // 
            // nudIntervalPercentage
            // 
            this.nudIntervalPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudIntervalPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudIntervalPercentage.Location = new System.Drawing.Point(197, 111);
            this.nudIntervalPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudIntervalPercentage.Name = "nudIntervalPercentage";
            this.nudIntervalPercentage.Size = new System.Drawing.Size(57, 22);
            this.nudIntervalPercentage.TabIndex = 72;
            this.nudIntervalPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudIntervalPercentage.ValueChanged += new System.EventHandler(this.nudIntervalPercentage_ValueChanged);
            // 
            // lblPlus3
            // 
            this.lblPlus3.AutoSize = true;
            this.lblPlus3.Location = new System.Drawing.Point(173, 112);
            this.lblPlus3.Name = "lblPlus3";
            this.lblPlus3.Size = new System.Drawing.Size(14, 16);
            this.lblPlus3.TabIndex = 71;
            this.lblPlus3.Text = "+";
            // 
            // nudInterval
            // 
            this.nudInterval.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudInterval.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudInterval.Location = new System.Drawing.Point(25, 111);
            this.nudInterval.Margin = new System.Windows.Forms.Padding(4);
            this.nudInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(141, 22);
            this.nudInterval.TabIndex = 37;
            this.nudInterval.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // lblVital
            // 
            this.lblVital.AutoSize = true;
            this.lblVital.Location = new System.Drawing.Point(21, 21);
            this.lblVital.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVital.Name = "lblVital";
            this.lblVital.Size = new System.Drawing.Size(36, 16);
            this.lblVital.TabIndex = 12;
            this.lblVital.Text = "Vital:";
            // 
            // cmbConsume
            // 
            this.cmbConsume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbConsume.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbConsume.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbConsume.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbConsume.DrawDropdownHoverOutline = false;
            this.cmbConsume.DrawFocusRectangle = false;
            this.cmbConsume.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbConsume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConsume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbConsume.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbConsume.FormattingEnabled = true;
            this.cmbConsume.Items.AddRange(new object[] {
            "Health",
            "Mana",
            "Experience"});
            this.cmbConsume.Location = new System.Drawing.Point(25, 46);
            this.cmbConsume.Margin = new System.Windows.Forms.Padding(4);
            this.cmbConsume.Name = "cmbConsume";
            this.cmbConsume.Size = new System.Drawing.Size(233, 23);
            this.cmbConsume.TabIndex = 11;
            this.cmbConsume.Text = "Health";
            this.cmbConsume.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbConsume.SelectedIndexChanged += new System.EventHandler(this.cmbConsume_SelectedIndexChanged);
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(25, 87);
            this.lblInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(53, 16);
            this.lblInterval.TabIndex = 9;
            this.lblInterval.Text = "Interval:";
            // 
            // grpEvent
            // 
            this.grpEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEvent.Controls.Add(this.chkSingleUseEvent);
            this.grpEvent.Controls.Add(this.cmbEvent);
            this.grpEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEvent.Location = new System.Drawing.Point(367, 327);
            this.grpEvent.Margin = new System.Windows.Forms.Padding(4);
            this.grpEvent.Name = "grpEvent";
            this.grpEvent.Padding = new System.Windows.Forms.Padding(4);
            this.grpEvent.Size = new System.Drawing.Size(267, 80);
            this.grpEvent.TabIndex = 42;
            this.grpEvent.TabStop = false;
            this.grpEvent.Text = "Event";
            this.grpEvent.Visible = false;
            // 
            // chkSingleUseEvent
            // 
            this.chkSingleUseEvent.AutoSize = true;
            this.chkSingleUseEvent.Checked = true;
            this.chkSingleUseEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleUseEvent.Location = new System.Drawing.Point(12, 52);
            this.chkSingleUseEvent.Margin = new System.Windows.Forms.Padding(4);
            this.chkSingleUseEvent.Name = "chkSingleUseEvent";
            this.chkSingleUseEvent.Size = new System.Drawing.Size(131, 20);
            this.chkSingleUseEvent.TabIndex = 29;
            this.chkSingleUseEvent.Text = "Destroy On Use?";
            this.chkSingleUseEvent.CheckedChanged += new System.EventHandler(this.chkSingleUse_CheckedChanged);
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
            this.cmbEvent.Location = new System.Drawing.Point(12, 18);
            this.cmbEvent.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(245, 23);
            this.cmbEvent.TabIndex = 17;
            this.cmbEvent.Text = null;
            this.cmbEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
            // 
            // grpBags
            // 
            this.grpBags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBags.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBags.Controls.Add(this.nudBag);
            this.grpBags.Controls.Add(this.lblBag);
            this.grpBags.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBags.Location = new System.Drawing.Point(367, 327);
            this.grpBags.Margin = new System.Windows.Forms.Padding(4);
            this.grpBags.Name = "grpBags";
            this.grpBags.Padding = new System.Windows.Forms.Padding(4);
            this.grpBags.Size = new System.Drawing.Size(289, 70);
            this.grpBags.TabIndex = 44;
            this.grpBags.TabStop = false;
            this.grpBags.Text = "Bag:";
            this.grpBags.Visible = false;
            // 
            // nudBag
            // 
            this.nudBag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBag.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBag.Location = new System.Drawing.Point(92, 28);
            this.nudBag.Margin = new System.Windows.Forms.Padding(4);
            this.nudBag.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.nudBag.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBag.Name = "nudBag";
            this.nudBag.Size = new System.Drawing.Size(192, 22);
            this.nudBag.TabIndex = 38;
            this.nudBag.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBag.ValueChanged += new System.EventHandler(this.nudBag_ValueChanged);
            // 
            // lblBag
            // 
            this.lblBag.AutoSize = true;
            this.lblBag.Location = new System.Drawing.Point(11, 31);
            this.lblBag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBag.Name = "lblBag";
            this.lblBag.Size = new System.Drawing.Size(68, 16);
            this.lblBag.TabIndex = 11;
            this.lblBag.Text = "Bag Slots:";
            // 
            // grpSpell
            // 
            this.grpSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpell.Controls.Add(this.chkSingleUseSpell);
            this.grpSpell.Controls.Add(this.chkQuickCast);
            this.grpSpell.Controls.Add(this.cmbTeachSpell);
            this.grpSpell.Controls.Add(this.lblSpell);
            this.grpSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpell.Location = new System.Drawing.Point(367, 327);
            this.grpSpell.Margin = new System.Windows.Forms.Padding(4);
            this.grpSpell.Name = "grpSpell";
            this.grpSpell.Padding = new System.Windows.Forms.Padding(4);
            this.grpSpell.Size = new System.Drawing.Size(289, 160);
            this.grpSpell.TabIndex = 13;
            this.grpSpell.TabStop = false;
            this.grpSpell.Text = "Spell";
            this.grpSpell.Visible = false;
            // 
            // chkSingleUseSpell
            // 
            this.chkSingleUseSpell.AutoSize = true;
            this.chkSingleUseSpell.Checked = true;
            this.chkSingleUseSpell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleUseSpell.Location = new System.Drawing.Point(20, 117);
            this.chkSingleUseSpell.Margin = new System.Windows.Forms.Padding(4);
            this.chkSingleUseSpell.Name = "chkSingleUseSpell";
            this.chkSingleUseSpell.Size = new System.Drawing.Size(131, 20);
            this.chkSingleUseSpell.TabIndex = 29;
            this.chkSingleUseSpell.Text = "Destroy On Use?";
            this.chkSingleUseSpell.CheckedChanged += new System.EventHandler(this.chkSingleUse_CheckedChanged);
            // 
            // chkQuickCast
            // 
            this.chkQuickCast.AutoSize = true;
            this.chkQuickCast.Location = new System.Drawing.Point(20, 89);
            this.chkQuickCast.Margin = new System.Windows.Forms.Padding(4);
            this.chkQuickCast.Name = "chkQuickCast";
            this.chkQuickCast.Size = new System.Drawing.Size(134, 20);
            this.chkQuickCast.TabIndex = 28;
            this.chkQuickCast.Text = "Quick Cast Spell?";
            this.chkQuickCast.CheckedChanged += new System.EventHandler(this.chkQuickCast_CheckedChanged);
            // 
            // cmbTeachSpell
            // 
            this.cmbTeachSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTeachSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTeachSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTeachSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTeachSpell.DrawDropdownHoverOutline = false;
            this.cmbTeachSpell.DrawFocusRectangle = false;
            this.cmbTeachSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTeachSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeachSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTeachSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTeachSpell.FormattingEnabled = true;
            this.cmbTeachSpell.Location = new System.Drawing.Point(20, 49);
            this.cmbTeachSpell.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTeachSpell.Name = "cmbTeachSpell";
            this.cmbTeachSpell.Size = new System.Drawing.Size(239, 23);
            this.cmbTeachSpell.TabIndex = 12;
            this.cmbTeachSpell.Text = null;
            this.cmbTeachSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTeachSpell.SelectedIndexChanged += new System.EventHandler(this.cmbTeachSpell_SelectedIndexChanged);
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(16, 26);
            this.lblSpell.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(41, 16);
            this.lblSpell.TabIndex = 11;
            this.lblSpell.Text = "Spell:";
            // 
            // grpEquipment
            // 
            this.grpEquipment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEquipment.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEquipment.Controls.Add(this.grpPaperdoll);
            this.grpEquipment.Controls.Add(this.grpEffects);
            this.grpEquipment.Controls.Add(this.grpRegen);
            this.grpEquipment.Controls.Add(this.grpVitalBonuses);
            this.grpEquipment.Controls.Add(this.grpStatBonuses);
            this.grpEquipment.Controls.Add(this.cmbEquipmentSlot);
            this.grpEquipment.Controls.Add(this.lblEquipmentSlot);
            this.grpEquipment.Controls.Add(this.grpWeaponProperties);
            this.grpEquipment.Controls.Add(this.grpShieldProperties);
            this.grpEquipment.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEquipment.Location = new System.Drawing.Point(3, 592);
            this.grpEquipment.Margin = new System.Windows.Forms.Padding(4);
            this.grpEquipment.Name = "grpEquipment";
            this.grpEquipment.Padding = new System.Windows.Forms.Padding(4);
            this.grpEquipment.Size = new System.Drawing.Size(1077, 1057);
            this.grpEquipment.TabIndex = 12;
            this.grpEquipment.TabStop = false;
            this.grpEquipment.Text = "Equipment";
            this.grpEquipment.Visible = false;
            // 
            // grpPaperdoll
            // 
            this.grpPaperdoll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPaperdoll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPaperdoll.Controls.Add(this.picMalePaperdoll);
            this.grpPaperdoll.Controls.Add(this.lblMalePaperdoll);
            this.grpPaperdoll.Controls.Add(this.cmbMalePaperdoll);
            this.grpPaperdoll.Controls.Add(this.picFemalePaperdoll);
            this.grpPaperdoll.Controls.Add(this.lblFemalePaperdoll);
            this.grpPaperdoll.Controls.Add(this.cmbFemalePaperdoll);
            this.grpPaperdoll.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPaperdoll.Location = new System.Drawing.Point(387, 22);
            this.grpPaperdoll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPaperdoll.Name = "grpPaperdoll";
            this.grpPaperdoll.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPaperdoll.Size = new System.Drawing.Size(361, 505);
            this.grpPaperdoll.TabIndex = 60;
            this.grpPaperdoll.TabStop = false;
            this.grpPaperdoll.Text = "Paperdoll Options";
            // 
            // picMalePaperdoll
            // 
            this.picMalePaperdoll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picMalePaperdoll.Location = new System.Drawing.Point(16, 73);
            this.picMalePaperdoll.Margin = new System.Windows.Forms.Padding(4);
            this.picMalePaperdoll.Name = "picMalePaperdoll";
            this.picMalePaperdoll.Size = new System.Drawing.Size(328, 188);
            this.picMalePaperdoll.TabIndex = 16;
            this.picMalePaperdoll.TabStop = false;
            // 
            // lblMalePaperdoll
            // 
            this.lblMalePaperdoll.AutoSize = true;
            this.lblMalePaperdoll.Location = new System.Drawing.Point(15, 41);
            this.lblMalePaperdoll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMalePaperdoll.Name = "lblMalePaperdoll";
            this.lblMalePaperdoll.Size = new System.Drawing.Size(102, 16);
            this.lblMalePaperdoll.TabIndex = 21;
            this.lblMalePaperdoll.Text = "Male Paperdoll:";
            // 
            // cmbMalePaperdoll
            // 
            this.cmbMalePaperdoll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMalePaperdoll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMalePaperdoll.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMalePaperdoll.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMalePaperdoll.DrawDropdownHoverOutline = false;
            this.cmbMalePaperdoll.DrawFocusRectangle = false;
            this.cmbMalePaperdoll.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMalePaperdoll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMalePaperdoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMalePaperdoll.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMalePaperdoll.FormattingEnabled = true;
            this.cmbMalePaperdoll.Items.AddRange(new object[] {
            "None",
            "None"});
            this.cmbMalePaperdoll.Location = new System.Drawing.Point(141, 33);
            this.cmbMalePaperdoll.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMalePaperdoll.Name = "cmbMalePaperdoll";
            this.cmbMalePaperdoll.Size = new System.Drawing.Size(201, 23);
            this.cmbMalePaperdoll.TabIndex = 22;
            this.cmbMalePaperdoll.Text = "None";
            this.cmbMalePaperdoll.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbMalePaperdoll.SelectedIndexChanged += new System.EventHandler(this.cmbPaperdoll_SelectedIndexChanged);
            // 
            // picFemalePaperdoll
            // 
            this.picFemalePaperdoll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picFemalePaperdoll.Location = new System.Drawing.Point(16, 302);
            this.picFemalePaperdoll.Margin = new System.Windows.Forms.Padding(4);
            this.picFemalePaperdoll.Name = "picFemalePaperdoll";
            this.picFemalePaperdoll.Size = new System.Drawing.Size(328, 188);
            this.picFemalePaperdoll.TabIndex = 34;
            this.picFemalePaperdoll.TabStop = false;
            // 
            // lblFemalePaperdoll
            // 
            this.lblFemalePaperdoll.AutoSize = true;
            this.lblFemalePaperdoll.Location = new System.Drawing.Point(12, 272);
            this.lblFemalePaperdoll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFemalePaperdoll.Name = "lblFemalePaperdoll";
            this.lblFemalePaperdoll.Size = new System.Drawing.Size(118, 16);
            this.lblFemalePaperdoll.TabIndex = 35;
            this.lblFemalePaperdoll.Text = "Female Paperdoll:";
            // 
            // cmbFemalePaperdoll
            // 
            this.cmbFemalePaperdoll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFemalePaperdoll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFemalePaperdoll.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFemalePaperdoll.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFemalePaperdoll.DrawDropdownHoverOutline = false;
            this.cmbFemalePaperdoll.DrawFocusRectangle = false;
            this.cmbFemalePaperdoll.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFemalePaperdoll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFemalePaperdoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFemalePaperdoll.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFemalePaperdoll.FormattingEnabled = true;
            this.cmbFemalePaperdoll.Items.AddRange(new object[] {
            "None",
            "None"});
            this.cmbFemalePaperdoll.Location = new System.Drawing.Point(141, 268);
            this.cmbFemalePaperdoll.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFemalePaperdoll.Name = "cmbFemalePaperdoll";
            this.cmbFemalePaperdoll.Size = new System.Drawing.Size(201, 23);
            this.cmbFemalePaperdoll.TabIndex = 36;
            this.cmbFemalePaperdoll.Text = "None";
            this.cmbFemalePaperdoll.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFemalePaperdoll.SelectedIndexChanged += new System.EventHandler(this.cmbFemalePaperdoll_SelectedIndexChanged);
            // 
            // grpEffects
            // 
            this.grpEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEffects.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEffects.Controls.Add(this.lstBonusEffects);
            this.grpEffects.Controls.Add(this.lblEffectPercent);
            this.grpEffects.Controls.Add(this.nudEffectPercent);
            this.grpEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEffects.Location = new System.Drawing.Point(19, 357);
            this.grpEffects.Margin = new System.Windows.Forms.Padding(4);
            this.grpEffects.Name = "grpEffects";
            this.grpEffects.Padding = new System.Windows.Forms.Padding(4);
            this.grpEffects.Size = new System.Drawing.Size(351, 241);
            this.grpEffects.TabIndex = 57;
            this.grpEffects.TabStop = false;
            this.grpEffects.Text = "Bonus Effects";
            // 
            // lstBonusEffects
            // 
            this.lstBonusEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBonusEffects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBonusEffects.FormattingEnabled = true;
            this.lstBonusEffects.ItemHeight = 16;
            this.lstBonusEffects.Location = new System.Drawing.Point(17, 23);
            this.lstBonusEffects.Margin = new System.Windows.Forms.Padding(4);
            this.lstBonusEffects.Name = "lstBonusEffects";
            this.lstBonusEffects.Size = new System.Drawing.Size(325, 146);
            this.lstBonusEffects.TabIndex = 58;
            this.lstBonusEffects.SelectedIndexChanged += new System.EventHandler(this.lstBonusEffects_SelectedIndexChanged);
            // 
            // lblEffectPercent
            // 
            this.lblEffectPercent.AutoSize = true;
            this.lblEffectPercent.Location = new System.Drawing.Point(13, 180);
            this.lblEffectPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffectPercent.Name = "lblEffectPercent";
            this.lblEffectPercent.Size = new System.Drawing.Size(114, 16);
            this.lblEffectPercent.TabIndex = 31;
            this.lblEffectPercent.Text = "Effect Amount (%):";
            // 
            // nudEffectPercent
            // 
            this.nudEffectPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEffectPercent.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEffectPercent.Location = new System.Drawing.Point(17, 203);
            this.nudEffectPercent.Margin = new System.Windows.Forms.Padding(4);
            this.nudEffectPercent.Name = "nudEffectPercent";
            this.nudEffectPercent.Size = new System.Drawing.Size(323, 22);
            this.nudEffectPercent.TabIndex = 55;
            this.nudEffectPercent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEffectPercent.ValueChanged += new System.EventHandler(this.nudEffectPercent_ValueChanged);
            // 
            // grpRegen
            // 
            this.grpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRegen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRegen.Controls.Add(this.nudMpRegen);
            this.grpRegen.Controls.Add(this.nudHPRegen);
            this.grpRegen.Controls.Add(this.lblHpRegen);
            this.grpRegen.Controls.Add(this.lblManaRegen);
            this.grpRegen.Controls.Add(this.lblRegenHint);
            this.grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRegen.Location = new System.Drawing.Point(19, 209);
            this.grpRegen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRegen.Name = "grpRegen";
            this.grpRegen.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRegen.Size = new System.Drawing.Size(351, 142);
            this.grpRegen.TabIndex = 59;
            this.grpRegen.TabStop = false;
            this.grpRegen.Text = "Regen";
            // 
            // nudMpRegen
            // 
            this.nudMpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMpRegen.Location = new System.Drawing.Point(177, 43);
            this.nudMpRegen.Margin = new System.Windows.Forms.Padding(4);
            this.nudMpRegen.Name = "nudMpRegen";
            this.nudMpRegen.Size = new System.Drawing.Size(128, 22);
            this.nudMpRegen.TabIndex = 31;
            this.nudMpRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMpRegen.ValueChanged += new System.EventHandler(this.nudMpRegen_ValueChanged);
            // 
            // nudHPRegen
            // 
            this.nudHPRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHPRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHPRegen.Location = new System.Drawing.Point(15, 44);
            this.nudHPRegen.Margin = new System.Windows.Forms.Padding(4);
            this.nudHPRegen.Name = "nudHPRegen";
            this.nudHPRegen.Size = new System.Drawing.Size(128, 22);
            this.nudHPRegen.TabIndex = 30;
            this.nudHPRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHPRegen.ValueChanged += new System.EventHandler(this.nudHPRegen_ValueChanged);
            // 
            // lblHpRegen
            // 
            this.lblHpRegen.AutoSize = true;
            this.lblHpRegen.Location = new System.Drawing.Point(7, 23);
            this.lblHpRegen.Name = "lblHpRegen";
            this.lblHpRegen.Size = new System.Drawing.Size(52, 16);
            this.lblHpRegen.TabIndex = 26;
            this.lblHpRegen.Text = "HP: (%)";
            // 
            // lblManaRegen
            // 
            this.lblManaRegen.AutoSize = true;
            this.lblManaRegen.Location = new System.Drawing.Point(177, 23);
            this.lblManaRegen.Name = "lblManaRegen";
            this.lblManaRegen.Size = new System.Drawing.Size(67, 16);
            this.lblManaRegen.TabIndex = 27;
            this.lblManaRegen.Text = "Mana: (%)";
            // 
            // lblRegenHint
            // 
            this.lblRegenHint.Location = new System.Drawing.Point(9, 86);
            this.lblRegenHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegenHint.Name = "lblRegenHint";
            this.lblRegenHint.Size = new System.Drawing.Size(333, 38);
            this.lblRegenHint.TabIndex = 0;
            this.lblRegenHint.Text = "% of HP/Mana to restore per tick.\r\nTick timer saved in server config.json.";
            // 
            // grpVitalBonuses
            // 
            this.grpVitalBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpVitalBonuses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVitalBonuses.Controls.Add(this.lblPercentage2);
            this.grpVitalBonuses.Controls.Add(this.lblPercentage1);
            this.grpVitalBonuses.Controls.Add(this.nudMPPercentage);
            this.grpVitalBonuses.Controls.Add(this.nudHPPercentage);
            this.grpVitalBonuses.Controls.Add(this.lblPlus2);
            this.grpVitalBonuses.Controls.Add(this.lblPlus1);
            this.grpVitalBonuses.Controls.Add(this.nudManaBonus);
            this.grpVitalBonuses.Controls.Add(this.nudHealthBonus);
            this.grpVitalBonuses.Controls.Add(this.lblManaBonus);
            this.grpVitalBonuses.Controls.Add(this.lblHealthBonus);
            this.grpVitalBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVitalBonuses.Location = new System.Drawing.Point(19, 66);
            this.grpVitalBonuses.Margin = new System.Windows.Forms.Padding(4);
            this.grpVitalBonuses.Name = "grpVitalBonuses";
            this.grpVitalBonuses.Padding = new System.Windows.Forms.Padding(4);
            this.grpVitalBonuses.Size = new System.Drawing.Size(351, 135);
            this.grpVitalBonuses.TabIndex = 58;
            this.grpVitalBonuses.TabStop = false;
            this.grpVitalBonuses.Text = "Vital Bonuses";
            // 
            // lblPercentage2
            // 
            this.lblPercentage2.AutoSize = true;
            this.lblPercentage2.Location = new System.Drawing.Point(287, 101);
            this.lblPercentage2.Name = "lblPercentage2";
            this.lblPercentage2.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage2.TabIndex = 70;
            this.lblPercentage2.Text = "%";
            // 
            // lblPercentage1
            // 
            this.lblPercentage1.AutoSize = true;
            this.lblPercentage1.Location = new System.Drawing.Point(287, 48);
            this.lblPercentage1.Name = "lblPercentage1";
            this.lblPercentage1.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage1.TabIndex = 69;
            this.lblPercentage1.Text = "%";
            // 
            // nudMPPercentage
            // 
            this.nudMPPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMPPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMPPercentage.Location = new System.Drawing.Point(177, 98);
            this.nudMPPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudMPPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMPPercentage.Name = "nudMPPercentage";
            this.nudMPPercentage.Size = new System.Drawing.Size(103, 22);
            this.nudMPPercentage.TabIndex = 68;
            this.nudMPPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMPPercentage.ValueChanged += new System.EventHandler(this.nudMPPercentage_ValueChanged);
            // 
            // nudHPPercentage
            // 
            this.nudHPPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHPPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHPPercentage.Location = new System.Drawing.Point(177, 46);
            this.nudHPPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudHPPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHPPercentage.Name = "nudHPPercentage";
            this.nudHPPercentage.Size = new System.Drawing.Size(103, 22);
            this.nudHPPercentage.TabIndex = 67;
            this.nudHPPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHPPercentage.ValueChanged += new System.EventHandler(this.nudHPPercentage_ValueChanged);
            // 
            // lblPlus2
            // 
            this.lblPlus2.AutoSize = true;
            this.lblPlus2.Location = new System.Drawing.Point(152, 101);
            this.lblPlus2.Name = "lblPlus2";
            this.lblPlus2.Size = new System.Drawing.Size(14, 16);
            this.lblPlus2.TabIndex = 66;
            this.lblPlus2.Text = "+";
            // 
            // lblPlus1
            // 
            this.lblPlus1.AutoSize = true;
            this.lblPlus1.Location = new System.Drawing.Point(152, 48);
            this.lblPlus1.Name = "lblPlus1";
            this.lblPlus1.Size = new System.Drawing.Size(14, 16);
            this.lblPlus1.TabIndex = 65;
            this.lblPlus1.Text = "+";
            // 
            // nudManaBonus
            // 
            this.nudManaBonus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudManaBonus.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudManaBonus.Location = new System.Drawing.Point(16, 98);
            this.nudManaBonus.Margin = new System.Windows.Forms.Padding(4);
            this.nudManaBonus.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudManaBonus.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudManaBonus.Name = "nudManaBonus";
            this.nudManaBonus.Size = new System.Drawing.Size(127, 22);
            this.nudManaBonus.TabIndex = 49;
            this.nudManaBonus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudManaBonus.ValueChanged += new System.EventHandler(this.nudManaBonus_ValueChanged);
            // 
            // nudHealthBonus
            // 
            this.nudHealthBonus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHealthBonus.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHealthBonus.Location = new System.Drawing.Point(16, 46);
            this.nudHealthBonus.Margin = new System.Windows.Forms.Padding(4);
            this.nudHealthBonus.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudHealthBonus.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudHealthBonus.Name = "nudHealthBonus";
            this.nudHealthBonus.Size = new System.Drawing.Size(127, 22);
            this.nudHealthBonus.TabIndex = 48;
            this.nudHealthBonus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHealthBonus.ValueChanged += new System.EventHandler(this.nudHealthBonus_ValueChanged);
            // 
            // lblManaBonus
            // 
            this.lblManaBonus.AutoSize = true;
            this.lblManaBonus.Location = new System.Drawing.Point(12, 79);
            this.lblManaBonus.Name = "lblManaBonus";
            this.lblManaBonus.Size = new System.Drawing.Size(44, 16);
            this.lblManaBonus.TabIndex = 44;
            this.lblManaBonus.Text = "Mana:";
            // 
            // lblHealthBonus
            // 
            this.lblHealthBonus.AutoSize = true;
            this.lblHealthBonus.Location = new System.Drawing.Point(11, 26);
            this.lblHealthBonus.Name = "lblHealthBonus";
            this.lblHealthBonus.Size = new System.Drawing.Size(49, 16);
            this.lblHealthBonus.TabIndex = 43;
            this.lblHealthBonus.Text = "Health:";
            // 
            // grpStatBonuses
            // 
            this.grpStatBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStatBonuses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStatBonuses.Controls.Add(this.label6);
            this.grpStatBonuses.Controls.Add(this.nudVitPercentage);
            this.grpStatBonuses.Controls.Add(this.label9);
            this.grpStatBonuses.Controls.Add(this.nudVit);
            this.grpStatBonuses.Controls.Add(this.label10);
            this.grpStatBonuses.Controls.Add(this.CurLbl);
            this.grpStatBonuses.Controls.Add(this.DmgLbl);
            this.grpStatBonuses.Controls.Add(this.label3);
            this.grpStatBonuses.Controls.Add(this.label7);
            this.grpStatBonuses.Controls.Add(this.nudCurPercentage);
            this.grpStatBonuses.Controls.Add(this.label8);
            this.grpStatBonuses.Controls.Add(this.nudCur);
            this.grpStatBonuses.Controls.Add(this.label4);
            this.grpStatBonuses.Controls.Add(this.nudDmgPercentage);
            this.grpStatBonuses.Controls.Add(this.label5);
            this.grpStatBonuses.Controls.Add(this.nudDmg);
            this.grpStatBonuses.Controls.Add(this.label1);
            this.grpStatBonuses.Controls.Add(this.nudAgiPercentage);
            this.grpStatBonuses.Controls.Add(this.label2);
            this.grpStatBonuses.Controls.Add(this.nudAgi);
            this.grpStatBonuses.Controls.Add(this.lblPercentage5);
            this.grpStatBonuses.Controls.Add(this.lblPercentage4);
            this.grpStatBonuses.Controls.Add(this.lblPercentage8);
            this.grpStatBonuses.Controls.Add(this.lblPercentage7);
            this.grpStatBonuses.Controls.Add(this.lblPercentage6);
            this.grpStatBonuses.Controls.Add(this.nudSpdPercentage);
            this.grpStatBonuses.Controls.Add(this.nudMRPercentage);
            this.grpStatBonuses.Controls.Add(this.nudDefPercentage);
            this.grpStatBonuses.Controls.Add(this.nudMagPercentage);
            this.grpStatBonuses.Controls.Add(this.nudStrPercentage);
            this.grpStatBonuses.Controls.Add(this.lblPlus5);
            this.grpStatBonuses.Controls.Add(this.lblPlus4);
            this.grpStatBonuses.Controls.Add(this.lblPlus8);
            this.grpStatBonuses.Controls.Add(this.lblPlus7);
            this.grpStatBonuses.Controls.Add(this.lblPlus6);
            this.grpStatBonuses.Controls.Add(this.nudRange);
            this.grpStatBonuses.Controls.Add(this.nudSpd);
            this.grpStatBonuses.Controls.Add(this.nudMR);
            this.grpStatBonuses.Controls.Add(this.nudDef);
            this.grpStatBonuses.Controls.Add(this.nudMag);
            this.grpStatBonuses.Controls.Add(this.nudStr);
            this.grpStatBonuses.Controls.Add(this.lblSpd);
            this.grpStatBonuses.Controls.Add(this.lblMR);
            this.grpStatBonuses.Controls.Add(this.lblDef);
            this.grpStatBonuses.Controls.Add(this.lblMag);
            this.grpStatBonuses.Controls.Add(this.lblStr);
            this.grpStatBonuses.Controls.Add(this.lblRange);
            this.grpStatBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStatBonuses.Location = new System.Drawing.Point(19, 614);
            this.grpStatBonuses.Margin = new System.Windows.Forms.Padding(4);
            this.grpStatBonuses.Name = "grpStatBonuses";
            this.grpStatBonuses.Padding = new System.Windows.Forms.Padding(4);
            this.grpStatBonuses.Size = new System.Drawing.Size(351, 439);
            this.grpStatBonuses.TabIndex = 40;
            this.grpStatBonuses.TabStop = false;
            this.grpStatBonuses.Text = "Stat Bonuses";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 16);
            this.label6.TabIndex = 102;
            this.label6.Text = "%";
            // 
            // nudVitPercentage
            // 
            this.nudVitPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudVitPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudVitPercentage.Location = new System.Drawing.Point(291, 34);
            this.nudVitPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudVitPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudVitPercentage.Name = "nudVitPercentage";
            this.nudVitPercentage.Size = new System.Drawing.Size(50, 22);
            this.nudVitPercentage.TabIndex = 101;
            this.nudVitPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudVitPercentage.ValueChanged += new System.EventHandler(this.nudVitPercentage_ValueChanged_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(275, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 16);
            this.label9.TabIndex = 100;
            this.label9.Text = "+";
            // 
            // nudVit
            // 
            this.nudVit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudVit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudVit.Location = new System.Drawing.Point(187, 34);
            this.nudVit.Margin = new System.Windows.Forms.Padding(4);
            this.nudVit.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudVit.Name = "nudVit";
            this.nudVit.Size = new System.Drawing.Size(88, 22);
            this.nudVit.TabIndex = 99;
            this.nudVit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudVit.ValueChanged += new System.EventHandler(this.nudVit_ValueChanged_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(185, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 98;
            this.label10.Text = "Vitality:";
            // 
            // CurLbl
            // 
            this.CurLbl.AutoSize = true;
            this.CurLbl.Location = new System.Drawing.Point(21, 348);
            this.CurLbl.Name = "CurLbl";
            this.CurLbl.Size = new System.Drawing.Size(45, 16);
            this.CurLbl.TabIndex = 97;
            this.CurLbl.Text = "Cures:";
            // 
            // DmgLbl
            // 
            this.DmgLbl.AutoSize = true;
            this.DmgLbl.Location = new System.Drawing.Point(16, 306);
            this.DmgLbl.Name = "DmgLbl";
            this.DmgLbl.Size = new System.Drawing.Size(80, 16);
            this.DmgLbl.TabIndex = 96;
            this.DmgLbl.Text = "Dmg Bonus:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 95;
            this.label3.Text = "Agility:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(121, 344);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 16);
            this.label7.TabIndex = 94;
            this.label7.Text = "%";
            // 
            // nudCurPercentage
            // 
            this.nudCurPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCurPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCurPercentage.Location = new System.Drawing.Point(118, 364);
            this.nudCurPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudCurPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCurPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudCurPercentage.Name = "nudCurPercentage";
            this.nudCurPercentage.Size = new System.Drawing.Size(53, 22);
            this.nudCurPercentage.TabIndex = 93;
            this.nudCurPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCurPercentage.ValueChanged += new System.EventHandler(this.nudCurPercentage_ValueChanged_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 366);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 92;
            this.label8.Text = "+";
            // 
            // nudCur
            // 
            this.nudCur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCur.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCur.Location = new System.Drawing.Point(20, 364);
            this.nudCur.Margin = new System.Windows.Forms.Padding(4);
            this.nudCur.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCur.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudCur.Name = "nudCur";
            this.nudCur.Size = new System.Drawing.Size(85, 22);
            this.nudCur.TabIndex = 91;
            this.nudCur.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCur.ValueChanged += new System.EventHandler(this.nudCur_ValueChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 16);
            this.label4.TabIndex = 90;
            this.label4.Text = "%";
            // 
            // nudDmgPercentage
            // 
            this.nudDmgPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDmgPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDmgPercentage.Location = new System.Drawing.Point(118, 323);
            this.nudDmgPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudDmgPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDmgPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudDmgPercentage.Name = "nudDmgPercentage";
            this.nudDmgPercentage.Size = new System.Drawing.Size(53, 22);
            this.nudDmgPercentage.TabIndex = 89;
            this.nudDmgPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDmgPercentage.ValueChanged += new System.EventHandler(this.nudDmgPercentage_ValueChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 88;
            this.label5.Text = "+";
            // 
            // nudDmg
            // 
            this.nudDmg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDmg.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDmg.Location = new System.Drawing.Point(20, 323);
            this.nudDmg.Margin = new System.Windows.Forms.Padding(4);
            this.nudDmg.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDmg.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudDmg.Name = "nudDmg";
            this.nudDmg.Size = new System.Drawing.Size(85, 22);
            this.nudDmg.TabIndex = 87;
            this.nudDmg.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDmg.ValueChanged += new System.EventHandler(this.nudDmg_ValueChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 16);
            this.label1.TabIndex = 86;
            this.label1.Text = "%";
            // 
            // nudAgiPercentage
            // 
            this.nudAgiPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAgiPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAgiPercentage.Location = new System.Drawing.Point(118, 281);
            this.nudAgiPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudAgiPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAgiPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudAgiPercentage.Name = "nudAgiPercentage";
            this.nudAgiPercentage.Size = new System.Drawing.Size(53, 22);
            this.nudAgiPercentage.TabIndex = 85;
            this.nudAgiPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAgiPercentage.ValueChanged += new System.EventHandler(this.nudAgiPercentage_ValueChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 16);
            this.label2.TabIndex = 84;
            this.label2.Text = "+";
            // 
            // nudAgi
            // 
            this.nudAgi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAgi.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAgi.Location = new System.Drawing.Point(20, 281);
            this.nudAgi.Margin = new System.Windows.Forms.Padding(4);
            this.nudAgi.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAgi.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudAgi.Name = "nudAgi";
            this.nudAgi.Size = new System.Drawing.Size(85, 22);
            this.nudAgi.TabIndex = 83;
            this.nudAgi.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAgi.ValueChanged += new System.EventHandler(this.nudAgi_ValueChanged_1);
            // 
            // lblPercentage5
            // 
            this.lblPercentage5.AutoSize = true;
            this.lblPercentage5.Location = new System.Drawing.Point(121, 214);
            this.lblPercentage5.Name = "lblPercentage5";
            this.lblPercentage5.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage5.TabIndex = 82;
            this.lblPercentage5.Text = "%";
            // 
            // lblPercentage4
            // 
            this.lblPercentage4.AutoSize = true;
            this.lblPercentage4.Location = new System.Drawing.Point(121, 164);
            this.lblPercentage4.Name = "lblPercentage4";
            this.lblPercentage4.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage4.TabIndex = 81;
            this.lblPercentage4.Text = "%";
            // 
            // lblPercentage8
            // 
            this.lblPercentage8.AutoSize = true;
            this.lblPercentage8.Location = new System.Drawing.Point(127, 111);
            this.lblPercentage8.Name = "lblPercentage8";
            this.lblPercentage8.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage8.TabIndex = 80;
            this.lblPercentage8.Text = "%";
            // 
            // lblPercentage7
            // 
            this.lblPercentage7.AutoSize = true;
            this.lblPercentage7.Location = new System.Drawing.Point(124, 63);
            this.lblPercentage7.Name = "lblPercentage7";
            this.lblPercentage7.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage7.TabIndex = 79;
            this.lblPercentage7.Text = "%";
            // 
            // lblPercentage6
            // 
            this.lblPercentage6.AutoSize = true;
            this.lblPercentage6.Location = new System.Drawing.Point(124, 15);
            this.lblPercentage6.Name = "lblPercentage6";
            this.lblPercentage6.Size = new System.Drawing.Size(19, 16);
            this.lblPercentage6.TabIndex = 78;
            this.lblPercentage6.Text = "%";
            // 
            // nudSpdPercentage
            // 
            this.nudSpdPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpdPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpdPercentage.Location = new System.Drawing.Point(118, 233);
            this.nudSpdPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudSpdPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSpdPercentage.Name = "nudSpdPercentage";
            this.nudSpdPercentage.Size = new System.Drawing.Size(50, 22);
            this.nudSpdPercentage.TabIndex = 77;
            this.nudSpdPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpdPercentage.ValueChanged += new System.EventHandler(this.nudSpdPercentage_ValueChanged);
            // 
            // nudMRPercentage
            // 
            this.nudMRPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMRPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMRPercentage.Location = new System.Drawing.Point(118, 182);
            this.nudMRPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudMRPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMRPercentage.Name = "nudMRPercentage";
            this.nudMRPercentage.Size = new System.Drawing.Size(50, 22);
            this.nudMRPercentage.TabIndex = 76;
            this.nudMRPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMRPercentage.ValueChanged += new System.EventHandler(this.nudMRPercentage_ValueChanged);
            // 
            // nudDefPercentage
            // 
            this.nudDefPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDefPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDefPercentage.Location = new System.Drawing.Point(121, 130);
            this.nudDefPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudDefPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDefPercentage.Name = "nudDefPercentage";
            this.nudDefPercentage.Size = new System.Drawing.Size(50, 22);
            this.nudDefPercentage.TabIndex = 75;
            this.nudDefPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDefPercentage.ValueChanged += new System.EventHandler(this.nudDefPercentage_ValueChanged);
            // 
            // nudMagPercentage
            // 
            this.nudMagPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMagPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMagPercentage.Location = new System.Drawing.Point(121, 82);
            this.nudMagPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudMagPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMagPercentage.Name = "nudMagPercentage";
            this.nudMagPercentage.Size = new System.Drawing.Size(50, 22);
            this.nudMagPercentage.TabIndex = 74;
            this.nudMagPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMagPercentage.ValueChanged += new System.EventHandler(this.nudMagPercentage_ValueChanged);
            // 
            // nudStrPercentage
            // 
            this.nudStrPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStrPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStrPercentage.Location = new System.Drawing.Point(121, 34);
            this.nudStrPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.nudStrPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudStrPercentage.Name = "nudStrPercentage";
            this.nudStrPercentage.Size = new System.Drawing.Size(50, 22);
            this.nudStrPercentage.TabIndex = 73;
            this.nudStrPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStrPercentage.ValueChanged += new System.EventHandler(this.nudStrPercentage_ValueChanged);
            // 
            // lblPlus5
            // 
            this.lblPlus5.AutoSize = true;
            this.lblPlus5.Location = new System.Drawing.Point(105, 235);
            this.lblPlus5.Name = "lblPlus5";
            this.lblPlus5.Size = new System.Drawing.Size(14, 16);
            this.lblPlus5.TabIndex = 72;
            this.lblPlus5.Text = "+";
            // 
            // lblPlus4
            // 
            this.lblPlus4.AutoSize = true;
            this.lblPlus4.Location = new System.Drawing.Point(105, 185);
            this.lblPlus4.Name = "lblPlus4";
            this.lblPlus4.Size = new System.Drawing.Size(14, 16);
            this.lblPlus4.TabIndex = 71;
            this.lblPlus4.Text = "+";
            // 
            // lblPlus8
            // 
            this.lblPlus8.AutoSize = true;
            this.lblPlus8.Location = new System.Drawing.Point(108, 132);
            this.lblPlus8.Name = "lblPlus8";
            this.lblPlus8.Size = new System.Drawing.Size(14, 16);
            this.lblPlus8.TabIndex = 70;
            this.lblPlus8.Text = "+";
            // 
            // lblPlus7
            // 
            this.lblPlus7.AutoSize = true;
            this.lblPlus7.Location = new System.Drawing.Point(105, 84);
            this.lblPlus7.Name = "lblPlus7";
            this.lblPlus7.Size = new System.Drawing.Size(14, 16);
            this.lblPlus7.TabIndex = 69;
            this.lblPlus7.Text = "+";
            // 
            // lblPlus6
            // 
            this.lblPlus6.AutoSize = true;
            this.lblPlus6.Location = new System.Drawing.Point(105, 37);
            this.lblPlus6.Name = "lblPlus6";
            this.lblPlus6.Size = new System.Drawing.Size(14, 16);
            this.lblPlus6.TabIndex = 68;
            this.lblPlus6.Text = "+";
            // 
            // nudRange
            // 
            this.nudRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRange.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRange.Location = new System.Drawing.Point(224, 403);
            this.nudRange.Margin = new System.Windows.Forms.Padding(4);
            this.nudRange.Name = "nudRange";
            this.nudRange.Size = new System.Drawing.Size(120, 22);
            this.nudRange.TabIndex = 53;
            this.nudRange.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudRange.ValueChanged += new System.EventHandler(this.nudRange_ValueChanged);
            // 
            // nudSpd
            // 
            this.nudSpd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpd.Location = new System.Drawing.Point(20, 233);
            this.nudSpd.Margin = new System.Windows.Forms.Padding(4);
            this.nudSpd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpd.Name = "nudSpd";
            this.nudSpd.Size = new System.Drawing.Size(85, 22);
            this.nudSpd.TabIndex = 52;
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
            this.nudMR.Location = new System.Drawing.Point(17, 182);
            this.nudMR.Margin = new System.Windows.Forms.Padding(4);
            this.nudMR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMR.Name = "nudMR";
            this.nudMR.Size = new System.Drawing.Size(88, 22);
            this.nudMR.TabIndex = 51;
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
            this.nudDef.Location = new System.Drawing.Point(17, 130);
            this.nudDef.Margin = new System.Windows.Forms.Padding(4);
            this.nudDef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDef.Name = "nudDef";
            this.nudDef.Size = new System.Drawing.Size(88, 22);
            this.nudDef.TabIndex = 50;
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
            this.nudMag.Location = new System.Drawing.Point(17, 82);
            this.nudMag.Margin = new System.Windows.Forms.Padding(4);
            this.nudMag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMag.Name = "nudMag";
            this.nudMag.Size = new System.Drawing.Size(88, 22);
            this.nudMag.TabIndex = 49;
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
            this.nudStr.Location = new System.Drawing.Point(17, 34);
            this.nudStr.Margin = new System.Windows.Forms.Padding(4);
            this.nudStr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(88, 22);
            this.nudStr.TabIndex = 48;
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
            this.lblSpd.Location = new System.Drawing.Point(15, 213);
            this.lblSpd.Name = "lblSpd";
            this.lblSpd.Size = new System.Drawing.Size(51, 16);
            this.lblSpd.TabIndex = 47;
            this.lblSpd.Text = "Speed:";
            // 
            // lblMR
            // 
            this.lblMR.AutoSize = true;
            this.lblMR.Location = new System.Drawing.Point(13, 162);
            this.lblMR.Name = "lblMR";
            this.lblMR.Size = new System.Drawing.Size(88, 16);
            this.lblMR.TabIndex = 46;
            this.lblMR.Text = "Magic Resist:";
            // 
            // lblDef
            // 
            this.lblDef.AutoSize = true;
            this.lblDef.Location = new System.Drawing.Point(13, 111);
            this.lblDef.Name = "lblDef";
            this.lblDef.Size = new System.Drawing.Size(46, 16);
            this.lblDef.TabIndex = 45;
            this.lblDef.Text = "Armor:";
            // 
            // lblMag
            // 
            this.lblMag.AutoSize = true;
            this.lblMag.Location = new System.Drawing.Point(13, 66);
            this.lblMag.Name = "lblMag";
            this.lblMag.Size = new System.Drawing.Size(47, 16);
            this.lblMag.TabIndex = 44;
            this.lblMag.Text = "Magic:";
            // 
            // lblStr
            // 
            this.lblStr.AutoSize = true;
            this.lblStr.Location = new System.Drawing.Point(15, 15);
            this.lblStr.Name = "lblStr";
            this.lblStr.Size = new System.Drawing.Size(59, 16);
            this.lblStr.TabIndex = 43;
            this.lblStr.Text = "Strength:";
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(17, 405);
            this.lblRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(140, 16);
            this.lblRange.TabIndex = 20;
            this.lblRange.Text = "Stat Bonus Range (+-):";
            // 
            // cmbEquipmentSlot
            // 
            this.cmbEquipmentSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEquipmentSlot.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEquipmentSlot.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEquipmentSlot.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEquipmentSlot.DrawDropdownHoverOutline = false;
            this.cmbEquipmentSlot.DrawFocusRectangle = false;
            this.cmbEquipmentSlot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEquipmentSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipmentSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEquipmentSlot.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEquipmentSlot.FormattingEnabled = true;
            this.cmbEquipmentSlot.Location = new System.Drawing.Point(137, 30);
            this.cmbEquipmentSlot.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEquipmentSlot.Name = "cmbEquipmentSlot";
            this.cmbEquipmentSlot.Size = new System.Drawing.Size(231, 23);
            this.cmbEquipmentSlot.TabIndex = 24;
            this.cmbEquipmentSlot.Text = null;
            this.cmbEquipmentSlot.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEquipmentSlot.SelectedIndexChanged += new System.EventHandler(this.cmbEquipmentSlot_SelectedIndexChanged);
            // 
            // lblEquipmentSlot
            // 
            this.lblEquipmentSlot.AutoSize = true;
            this.lblEquipmentSlot.Location = new System.Drawing.Point(17, 33);
            this.lblEquipmentSlot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEquipmentSlot.Name = "lblEquipmentSlot";
            this.lblEquipmentSlot.Size = new System.Drawing.Size(100, 16);
            this.lblEquipmentSlot.TabIndex = 23;
            this.lblEquipmentSlot.Text = "Equipment Slot:";
            // 
            // grpWeaponProperties
            // 
            this.grpWeaponProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponProperties.Controls.Add(this.cmbWeaponSprite);
            this.grpWeaponProperties.Controls.Add(this.lblSpriteAttack);
            this.grpWeaponProperties.Controls.Add(this.nudCritMultiplier);
            this.grpWeaponProperties.Controls.Add(this.lblCritMultiplier);
            this.grpWeaponProperties.Controls.Add(this.grpAttackSpeed);
            this.grpWeaponProperties.Controls.Add(this.nudScaling);
            this.grpWeaponProperties.Controls.Add(this.nudCritChance);
            this.grpWeaponProperties.Controls.Add(this.nudDamage);
            this.grpWeaponProperties.Controls.Add(this.cmbProjectile);
            this.grpWeaponProperties.Controls.Add(this.cmbScalingStat);
            this.grpWeaponProperties.Controls.Add(this.lblScalingStat);
            this.grpWeaponProperties.Controls.Add(this.lblScalingAmount);
            this.grpWeaponProperties.Controls.Add(this.cmbDamageType);
            this.grpWeaponProperties.Controls.Add(this.lblDamageType);
            this.grpWeaponProperties.Controls.Add(this.lblCritChance);
            this.grpWeaponProperties.Controls.Add(this.cmbAttackAnimation);
            this.grpWeaponProperties.Controls.Add(this.lblAttackAnimation);
            this.grpWeaponProperties.Controls.Add(this.chk2Hand);
            this.grpWeaponProperties.Controls.Add(this.lblToolType);
            this.grpWeaponProperties.Controls.Add(this.cmbToolType);
            this.grpWeaponProperties.Controls.Add(this.lblProjectile);
            this.grpWeaponProperties.Controls.Add(this.lblDamage);
            this.grpWeaponProperties.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponProperties.Location = new System.Drawing.Point(755, 22);
            this.grpWeaponProperties.Margin = new System.Windows.Forms.Padding(4);
            this.grpWeaponProperties.Name = "grpWeaponProperties";
            this.grpWeaponProperties.Padding = new System.Windows.Forms.Padding(4);
            this.grpWeaponProperties.Size = new System.Drawing.Size(315, 639);
            this.grpWeaponProperties.TabIndex = 39;
            this.grpWeaponProperties.TabStop = false;
            this.grpWeaponProperties.Text = "Weapon Properties";
            this.grpWeaponProperties.Visible = false;
            // 
            // cmbWeaponSprite
            // 
            this.cmbWeaponSprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbWeaponSprite.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbWeaponSprite.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbWeaponSprite.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbWeaponSprite.DrawDropdownHoverOutline = false;
            this.cmbWeaponSprite.DrawFocusRectangle = false;
            this.cmbWeaponSprite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeaponSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeaponSprite.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbWeaponSprite.FormattingEnabled = true;
            this.cmbWeaponSprite.Location = new System.Drawing.Point(20, 384);
            this.cmbWeaponSprite.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWeaponSprite.Name = "cmbWeaponSprite";
            this.cmbWeaponSprite.Size = new System.Drawing.Size(281, 23);
            this.cmbWeaponSprite.TabIndex = 60;
            this.cmbWeaponSprite.Text = null;
            this.cmbWeaponSprite.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbWeaponSprite.SelectedIndexChanged += new System.EventHandler(this.cmbWeaponSprite_SelectedIndexChanged);
            // 
            // lblSpriteAttack
            // 
            this.lblSpriteAttack.AutoSize = true;
            this.lblSpriteAttack.Location = new System.Drawing.Point(16, 364);
            this.lblSpriteAttack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpriteAttack.Name = "lblSpriteAttack";
            this.lblSpriteAttack.Size = new System.Drawing.Size(147, 16);
            this.lblSpriteAttack.TabIndex = 59;
            this.lblSpriteAttack.Text = "Sprite Attack Animation:";
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
            this.nudCritMultiplier.Location = new System.Drawing.Point(20, 137);
            this.nudCritMultiplier.Margin = new System.Windows.Forms.Padding(4);
            this.nudCritMultiplier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCritMultiplier.Name = "nudCritMultiplier";
            this.nudCritMultiplier.Size = new System.Drawing.Size(283, 22);
            this.nudCritMultiplier.TabIndex = 58;
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
            this.lblCritMultiplier.Location = new System.Drawing.Point(16, 119);
            this.lblCritMultiplier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCritMultiplier.Name = "lblCritMultiplier";
            this.lblCritMultiplier.Size = new System.Drawing.Size(164, 16);
            this.lblCritMultiplier.TabIndex = 57;
            this.lblCritMultiplier.Text = "Crit Multiplier (Default 1.5x):";
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
            this.grpAttackSpeed.Location = new System.Drawing.Point(20, 521);
            this.grpAttackSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.grpAttackSpeed.Name = "grpAttackSpeed";
            this.grpAttackSpeed.Padding = new System.Windows.Forms.Padding(4);
            this.grpAttackSpeed.Size = new System.Drawing.Size(283, 106);
            this.grpAttackSpeed.TabIndex = 56;
            this.grpAttackSpeed.TabStop = false;
            this.grpAttackSpeed.Text = "Attack Speed";
            // 
            // nudAttackSpeedValue
            // 
            this.nudAttackSpeedValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAttackSpeedValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAttackSpeedValue.Location = new System.Drawing.Point(80, 71);
            this.nudAttackSpeedValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudAttackSpeedValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAttackSpeedValue.Name = "nudAttackSpeedValue";
            this.nudAttackSpeedValue.Size = new System.Drawing.Size(192, 22);
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
            this.lblAttackSpeedValue.Location = new System.Drawing.Point(12, 74);
            this.lblAttackSpeedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttackSpeedValue.Name = "lblAttackSpeedValue";
            this.lblAttackSpeedValue.Size = new System.Drawing.Size(45, 16);
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
            this.cmbAttackSpeedModifier.Location = new System.Drawing.Point(80, 30);
            this.cmbAttackSpeedModifier.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAttackSpeedModifier.Name = "cmbAttackSpeedModifier";
            this.cmbAttackSpeedModifier.Size = new System.Drawing.Size(191, 23);
            this.cmbAttackSpeedModifier.TabIndex = 28;
            this.cmbAttackSpeedModifier.Text = null;
            this.cmbAttackSpeedModifier.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackSpeedModifier.SelectedIndexChanged += new System.EventHandler(this.cmbAttackSpeedModifier_SelectedIndexChanged);
            // 
            // lblAttackSpeedModifier
            // 
            this.lblAttackSpeedModifier.AutoSize = true;
            this.lblAttackSpeedModifier.Location = new System.Drawing.Point(12, 33);
            this.lblAttackSpeedModifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttackSpeedModifier.Name = "lblAttackSpeedModifier";
            this.lblAttackSpeedModifier.Size = new System.Drawing.Size(58, 16);
            this.lblAttackSpeedModifier.TabIndex = 0;
            this.lblAttackSpeedModifier.Text = "Modifier:";
            // 
            // nudScaling
            // 
            this.nudScaling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudScaling.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudScaling.Location = new System.Drawing.Point(20, 288);
            this.nudScaling.Margin = new System.Windows.Forms.Padding(4);
            this.nudScaling.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudScaling.Name = "nudScaling";
            this.nudScaling.Size = new System.Drawing.Size(283, 22);
            this.nudScaling.TabIndex = 55;
            this.nudScaling.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudScaling.ValueChanged += new System.EventHandler(this.nudScaling_ValueChanged);
            // 
            // nudCritChance
            // 
            this.nudCritChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritChance.Location = new System.Drawing.Point(20, 90);
            this.nudCritChance.Margin = new System.Windows.Forms.Padding(4);
            this.nudCritChance.Name = "nudCritChance";
            this.nudCritChance.Size = new System.Drawing.Size(283, 22);
            this.nudCritChance.TabIndex = 54;
            this.nudCritChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritChance.ValueChanged += new System.EventHandler(this.nudCritChance_ValueChanged);
            // 
            // nudDamage
            // 
            this.nudDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDamage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDamage.Location = new System.Drawing.Point(20, 44);
            this.nudDamage.Margin = new System.Windows.Forms.Padding(4);
            this.nudDamage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDamage.Name = "nudDamage";
            this.nudDamage.Size = new System.Drawing.Size(283, 22);
            this.nudDamage.TabIndex = 49;
            this.nudDamage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDamage.ValueChanged += new System.EventHandler(this.nudDamage_ValueChanged);
            // 
            // cmbProjectile
            // 
            this.cmbProjectile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbProjectile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbProjectile.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbProjectile.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbProjectile.DrawDropdownHoverOutline = false;
            this.cmbProjectile.DrawFocusRectangle = false;
            this.cmbProjectile.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProjectile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjectile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProjectile.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbProjectile.FormattingEnabled = true;
            this.cmbProjectile.Location = new System.Drawing.Point(20, 332);
            this.cmbProjectile.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProjectile.Name = "cmbProjectile";
            this.cmbProjectile.Size = new System.Drawing.Size(281, 23);
            this.cmbProjectile.TabIndex = 47;
            this.cmbProjectile.Text = null;
            this.cmbProjectile.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbProjectile.SelectedIndexChanged += new System.EventHandler(this.cmbProjectile_SelectedIndexChanged);
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
            this.cmbScalingStat.Location = new System.Drawing.Point(20, 238);
            this.cmbScalingStat.Margin = new System.Windows.Forms.Padding(4);
            this.cmbScalingStat.Name = "cmbScalingStat";
            this.cmbScalingStat.Size = new System.Drawing.Size(283, 23);
            this.cmbScalingStat.TabIndex = 46;
            this.cmbScalingStat.Text = null;
            this.cmbScalingStat.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbScalingStat.SelectedIndexChanged += new System.EventHandler(this.cmbScalingStat_SelectedIndexChanged);
            // 
            // lblScalingStat
            // 
            this.lblScalingStat.AutoSize = true;
            this.lblScalingStat.Location = new System.Drawing.Point(17, 217);
            this.lblScalingStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScalingStat.Name = "lblScalingStat";
            this.lblScalingStat.Size = new System.Drawing.Size(81, 16);
            this.lblScalingStat.TabIndex = 45;
            this.lblScalingStat.Text = "Scaling Stat:";
            // 
            // lblScalingAmount
            // 
            this.lblScalingAmount.AutoSize = true;
            this.lblScalingAmount.Location = new System.Drawing.Point(16, 270);
            this.lblScalingAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScalingAmount.Name = "lblScalingAmount";
            this.lblScalingAmount.Size = new System.Drawing.Size(103, 16);
            this.lblScalingAmount.TabIndex = 44;
            this.lblScalingAmount.Text = "Scaling Amount:";
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
            "True",
            "Physical",
            "Magic",
            "True"});
            this.cmbDamageType.Location = new System.Drawing.Point(20, 187);
            this.cmbDamageType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDamageType.Name = "cmbDamageType";
            this.cmbDamageType.Size = new System.Drawing.Size(283, 23);
            this.cmbDamageType.TabIndex = 42;
            this.cmbDamageType.Text = "Physical";
            this.cmbDamageType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDamageType.SelectedIndexChanged += new System.EventHandler(this.cmbDamageType_SelectedIndexChanged);
            // 
            // lblDamageType
            // 
            this.lblDamageType.AutoSize = true;
            this.lblDamageType.Location = new System.Drawing.Point(17, 166);
            this.lblDamageType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDamageType.Name = "lblDamageType";
            this.lblDamageType.Size = new System.Drawing.Size(98, 16);
            this.lblDamageType.TabIndex = 41;
            this.lblDamageType.Text = "Damage Type:";
            // 
            // lblCritChance
            // 
            this.lblCritChance.AutoSize = true;
            this.lblCritChance.Location = new System.Drawing.Point(16, 73);
            this.lblCritChance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCritChance.Name = "lblCritChance";
            this.lblCritChance.Size = new System.Drawing.Size(101, 16);
            this.lblCritChance.TabIndex = 40;
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
            this.cmbAttackAnimation.Location = new System.Drawing.Point(20, 431);
            this.cmbAttackAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAttackAnimation.Name = "cmbAttackAnimation";
            this.cmbAttackAnimation.Size = new System.Drawing.Size(281, 23);
            this.cmbAttackAnimation.TabIndex = 38;
            this.cmbAttackAnimation.Text = null;
            this.cmbAttackAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAttackAnimation_SelectedIndexChanged);
            // 
            // lblAttackAnimation
            // 
            this.lblAttackAnimation.AutoSize = true;
            this.lblAttackAnimation.Location = new System.Drawing.Point(16, 412);
            this.lblAttackAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttackAnimation.Name = "lblAttackAnimation";
            this.lblAttackAnimation.Size = new System.Drawing.Size(142, 16);
            this.lblAttackAnimation.TabIndex = 37;
            this.lblAttackAnimation.Text = "Extra Attack Animation:";
            // 
            // chk2Hand
            // 
            this.chk2Hand.AutoSize = true;
            this.chk2Hand.Location = new System.Drawing.Point(179, 17);
            this.chk2Hand.Margin = new System.Windows.Forms.Padding(4);
            this.chk2Hand.Name = "chk2Hand";
            this.chk2Hand.Size = new System.Drawing.Size(72, 20);
            this.chk2Hand.TabIndex = 25;
            this.chk2Hand.Text = "2 Hand";
            this.chk2Hand.CheckedChanged += new System.EventHandler(this.chk2Hand_CheckedChanged);
            // 
            // lblToolType
            // 
            this.lblToolType.AutoSize = true;
            this.lblToolType.Location = new System.Drawing.Point(17, 464);
            this.lblToolType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolType.Name = "lblToolType";
            this.lblToolType.Size = new System.Drawing.Size(73, 16);
            this.lblToolType.TabIndex = 26;
            this.lblToolType.Text = "Tool Type:";
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
            this.cmbToolType.Location = new System.Drawing.Point(20, 481);
            this.cmbToolType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbToolType.Name = "cmbToolType";
            this.cmbToolType.Size = new System.Drawing.Size(283, 23);
            this.cmbToolType.TabIndex = 27;
            this.cmbToolType.Text = null;
            this.cmbToolType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbToolType.SelectedIndexChanged += new System.EventHandler(this.cmbToolType_SelectedIndexChanged);
            // 
            // lblProjectile
            // 
            this.lblProjectile.AutoSize = true;
            this.lblProjectile.Location = new System.Drawing.Point(16, 313);
            this.lblProjectile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProjectile.Name = "lblProjectile";
            this.lblProjectile.Size = new System.Drawing.Size(66, 16);
            this.lblProjectile.TabIndex = 33;
            this.lblProjectile.Text = "Projectile:";
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(16, 23);
            this.lblDamage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(98, 16);
            this.lblDamage.TabIndex = 11;
            this.lblDamage.Text = "Base Damage:";
            // 
            // grpShieldProperties
            // 
            this.grpShieldProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpShieldProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpShieldProperties.Controls.Add(this.nudBlockDmgAbs);
            this.grpShieldProperties.Controls.Add(this.lblBlockDmgAbs);
            this.grpShieldProperties.Controls.Add(this.nudBlockAmount);
            this.grpShieldProperties.Controls.Add(this.lblBlockAmount);
            this.grpShieldProperties.Controls.Add(this.nudBlockChance);
            this.grpShieldProperties.Controls.Add(this.lblBlockChance);
            this.grpShieldProperties.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpShieldProperties.Location = new System.Drawing.Point(755, 22);
            this.grpShieldProperties.Margin = new System.Windows.Forms.Padding(4);
            this.grpShieldProperties.Name = "grpShieldProperties";
            this.grpShieldProperties.Padding = new System.Windows.Forms.Padding(4);
            this.grpShieldProperties.Size = new System.Drawing.Size(315, 180);
            this.grpShieldProperties.TabIndex = 45;
            this.grpShieldProperties.TabStop = false;
            this.grpShieldProperties.Text = "Shield Properties";
            // 
            // nudBlockDmgAbs
            // 
            this.nudBlockDmgAbs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBlockDmgAbs.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBlockDmgAbs.Location = new System.Drawing.Point(20, 140);
            this.nudBlockDmgAbs.Margin = new System.Windows.Forms.Padding(4);
            this.nudBlockDmgAbs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBlockDmgAbs.Name = "nudBlockDmgAbs";
            this.nudBlockDmgAbs.Size = new System.Drawing.Size(283, 22);
            this.nudBlockDmgAbs.TabIndex = 64;
            this.nudBlockDmgAbs.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBlockDmgAbs.ValueChanged += new System.EventHandler(this.nudBlockDmgAbs_ValueChanged);
            // 
            // lblBlockDmgAbs
            // 
            this.lblBlockDmgAbs.AutoSize = true;
            this.lblBlockDmgAbs.Location = new System.Drawing.Point(16, 123);
            this.lblBlockDmgAbs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlockDmgAbs.Name = "lblBlockDmgAbs";
            this.lblBlockDmgAbs.Size = new System.Drawing.Size(191, 16);
            this.lblBlockDmgAbs.TabIndex = 63;
            this.lblBlockDmgAbs.Text = "Block Damage Absorption (%):";
            // 
            // nudBlockAmount
            // 
            this.nudBlockAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBlockAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBlockAmount.Location = new System.Drawing.Point(20, 92);
            this.nudBlockAmount.Margin = new System.Windows.Forms.Padding(4);
            this.nudBlockAmount.Name = "nudBlockAmount";
            this.nudBlockAmount.Size = new System.Drawing.Size(283, 22);
            this.nudBlockAmount.TabIndex = 62;
            this.nudBlockAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBlockAmount.ValueChanged += new System.EventHandler(this.nudBlockAmount_ValueChanged);
            // 
            // lblBlockAmount
            // 
            this.lblBlockAmount.AutoSize = true;
            this.lblBlockAmount.Location = new System.Drawing.Point(16, 75);
            this.lblBlockAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlockAmount.Name = "lblBlockAmount";
            this.lblBlockAmount.Size = new System.Drawing.Size(115, 16);
            this.lblBlockAmount.TabIndex = 61;
            this.lblBlockAmount.Text = "Block Amount (%):";
            // 
            // nudBlockChance
            // 
            this.nudBlockChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBlockChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBlockChance.Location = new System.Drawing.Point(20, 44);
            this.nudBlockChance.Margin = new System.Windows.Forms.Padding(4);
            this.nudBlockChance.Name = "nudBlockChance";
            this.nudBlockChance.Size = new System.Drawing.Size(283, 22);
            this.nudBlockChance.TabIndex = 60;
            this.nudBlockChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBlockChance.ValueChanged += new System.EventHandler(this.nudBlockChance_ValueChanged);
            // 
            // lblBlockChance
            // 
            this.lblBlockChance.AutoSize = true;
            this.lblBlockChance.Location = new System.Drawing.Point(16, 23);
            this.lblBlockChance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlockChance.Name = "lblBlockChance";
            this.lblBlockChance.Size = new System.Drawing.Size(116, 16);
            this.lblBlockChance.TabIndex = 59;
            this.lblBlockChance.Text = "Block Chance (%):";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.grpGeneral);
            this.pnlContainer.Controls.Add(this.grpEquipment);
            this.pnlContainer.Location = new System.Drawing.Point(285, 42);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1120, 686);
            this.pnlContainer.TabIndex = 43;
            this.pnlContainer.Visible = false;
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
            this.toolStrip.Size = new System.Drawing.Size(1419, 31);
            this.toolStrip.TabIndex = 44;
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
            // FrmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1419, 777);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmItem_FormClosed);
            this.Load += new System.EventHandler(this.frmItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
            this.grpItems.ResumeLayout(false);
            this.grpItems.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpStack.ResumeLayout(false);
            this.grpStack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvStackLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankStackLimit)).EndInit();
            this.grpCooldown.ResumeLayout(false);
            this.grpCooldown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemDespawnTime)).EndInit();
            this.grpRequirements.ResumeLayout(false);
            this.grpRequirements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeathDropChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.grpConsumable.ResumeLayout(false);
            this.grpConsumable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.grpEvent.ResumeLayout(false);
            this.grpEvent.PerformLayout();
            this.grpBags.ResumeLayout(false);
            this.grpBags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBag)).EndInit();
            this.grpSpell.ResumeLayout(false);
            this.grpSpell.PerformLayout();
            this.grpEquipment.ResumeLayout(false);
            this.grpEquipment.PerformLayout();
            this.grpPaperdoll.ResumeLayout(false);
            this.grpPaperdoll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMalePaperdoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFemalePaperdoll)).EndInit();
            this.grpEffects.ResumeLayout(false);
            this.grpEffects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEffectPercent)).EndInit();
            this.grpRegen.ResumeLayout(false);
            this.grpRegen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPRegen)).EndInit();
            this.grpVitalBonuses.ResumeLayout(false);
            this.grpVitalBonuses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMPPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudManaBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealthBonus)).EndInit();
            this.grpStatBonuses.ResumeLayout(false);
            this.grpStatBonuses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVitPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDmgPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDmg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgiPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpdPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMRPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            this.grpWeaponProperties.ResumeLayout(false);
            this.grpWeaponProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).EndInit();
            this.grpAttackSpeed.ResumeLayout(false);
            this.grpAttackSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).EndInit();
            this.grpShieldProperties.ResumeLayout(false);
            this.grpShieldProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockDmgAbs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockChance)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lblName;
        private Label lblType;
        private Label lblAnim;
        private Label lblPrice;
        private PictureBox picItem;
        private Label lblDamage;
        private PictureBox picMalePaperdoll;
        private Label lblInterval;
        private Label lblVital;
        private Label lblSpell;
        private Label lblRange;
        private Label lblPic;
        private Label lblMalePaperdoll;
        private Label lblDesc;
        private Label lblEquipmentSlot;
        private Label lblEffectPercent;
        private Label lblToolType;
        private Label lblProjectile;
        private Panel pnlContainer;
        private Label lblFemalePaperdoll;
        private PictureBox picFemalePaperdoll;
        private ToolStripButton toolStripItemNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripItemDelete;
        private ToolStripSeparator toolStripSeparator2;
        public ToolStripButton toolStripItemCopy;
        public ToolStripButton toolStripItemPaste;
        private ToolStripSeparator toolStripSeparator3;
        public ToolStripButton toolStripItemUndo;
        private Label lblAttackAnimation;
        private DarkUI.Controls.DarkGroupBox grpStatBonuses;
        private DarkUI.Controls.DarkGroupBox grpWeaponProperties;
        private Label lblScalingAmount;
        private Label lblDamageType;
        private Label lblCritChance;
        private Label lblScalingStat;
        private DarkGroupBox grpItems;
        private DarkButton btnSave;
        private DarkGroupBox grpGeneral;
        private DarkTextBox txtName;
        private DarkComboBox cmbType;
        private DarkGroupBox grpEquipment;
        private DarkGroupBox grpConsumable;
        private DarkComboBox cmbConsume;
        private DarkGroupBox grpSpell;
        private DarkButton btnCancel;
        private DarkComboBox cmbPic;
        private DarkComboBox cmbMalePaperdoll;
        private DarkTextBox txtDesc;
        private DarkCheckBox chk2Hand;
        private DarkComboBox cmbEquipmentSlot;
        private DarkComboBox cmbToolType;
        private DarkGroupBox grpEvent;
        private DarkComboBox cmbFemalePaperdoll;
        private DarkComboBox cmbAttackAnimation;
        private DarkComboBox cmbDamageType;
        private DarkComboBox cmbScalingStat;
        private DarkComboBox cmbProjectile;
        private DarkToolStrip toolStrip;
        private DarkButton btnEditRequirements;
        private DarkComboBox cmbAnimation;
        private DarkComboBox cmbTeachSpell;
        private DarkComboBox cmbEvent;
        private DarkGroupBox grpBags;
        private Label lblBag;
        private DarkNumericUpDown nudPrice;
        private DarkNumericUpDown nudBag;
        private DarkNumericUpDown nudInterval;
        private DarkNumericUpDown nudEffectPercent;
        private DarkNumericUpDown nudRange;
        private DarkNumericUpDown nudSpd;
        private DarkNumericUpDown nudMR;
        private DarkNumericUpDown nudDef;
        private DarkNumericUpDown nudMag;
        private DarkNumericUpDown nudStr;
        private Label lblSpd;
        private Label lblMR;
        private Label lblDef;
        private Label lblMag;
        private Label lblStr;
        private DarkNumericUpDown nudScaling;
        private DarkNumericUpDown nudCritChance;
        private DarkNumericUpDown nudDamage;
        private DarkCheckBox chkStackable;
        private DarkCheckBox chkCanDrop;
        private DarkGroupBox grpVitalBonuses;
        private DarkNumericUpDown nudManaBonus;
        private DarkNumericUpDown nudHealthBonus;
        private Label lblManaBonus;
        private Label lblHealthBonus;
        private DarkGroupBox grpAttackSpeed;
        private DarkNumericUpDown nudAttackSpeedValue;
        private Label lblAttackSpeedValue;
        private DarkComboBox cmbAttackSpeedModifier;
        private Label lblAttackSpeedModifier;
        private DarkNumericUpDown nudCritMultiplier;
        private Label lblCritMultiplier;
        private DarkNumericUpDown nudCooldown;
        private Label lblCooldown;
        private DarkCheckBox chkSingleUseSpell;
        private DarkCheckBox chkSingleUseEvent;
        private DarkCheckBox chkQuickCast;
        private DarkComboBox cmbRarity;
        private Label lblRarity;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private ToolStripButton btnAlphabetical;
        private ToolStripSeparator toolStripSeparator4;
        private DarkButton btnAddFolder;
        private Label lblFolder;
        private DarkComboBox cmbFolder;
        private Label lblPercentage2;
        private Label lblPercentage1;
        private DarkNumericUpDown nudMPPercentage;
        private DarkNumericUpDown nudHPPercentage;
        private Label lblPlus2;
        private Label lblPlus1;
        private Label lblPercentage3;
        private DarkNumericUpDown nudIntervalPercentage;
        private Label lblPlus3;
        private Label lblPercentage5;
        private Label lblPercentage4;
        private Label lblPercentage8;
        private Label lblPercentage7;
        private Label lblPercentage6;
        private DarkNumericUpDown nudSpdPercentage;
        private DarkNumericUpDown nudMRPercentage;
        private DarkNumericUpDown nudDefPercentage;
        private DarkNumericUpDown nudMagPercentage;
        private DarkNumericUpDown nudStrPercentage;
        private Label lblPlus5;
        private Label lblPlus4;
        private Label lblPlus8;
        private Label lblPlus7;
        private Label lblPlus6;
        private DarkGroupBox grpRegen;
        private DarkNumericUpDown nudMpRegen;
        private DarkNumericUpDown nudHPRegen;
        private Label lblHpRegen;
        private Label lblManaRegen;
        private Label lblRegenHint;
        private DarkComboBox cmbCooldownGroup;
        private Label lblCooldownGroup;
        private DarkButton btnAddCooldownGroup;
        private DarkCheckBox chkIgnoreGlobalCooldown;
        private Label lblAlpha;
        private Label lblBlue;
        private Label lblGreen;
        private Label lblRed;
        private DarkNumericUpDown nudRgbaA;
        private DarkNumericUpDown nudRgbaB;
        private DarkNumericUpDown nudRgbaG;
        private DarkNumericUpDown nudRgbaR;
        private DarkCheckBox chkIgnoreCdr;
        private Controls.GameObjectList lstGameObjects;
        private DarkCheckBox chkCanSell;
        private DarkCheckBox chkCanTrade;
        private DarkCheckBox chkCanBag;
        private DarkCheckBox chkCanBank;
        private DarkNumericUpDown nudDeathDropChance;
        private Label lblDeathDropChance;
        private DarkNumericUpDown nudBankStackLimit;
        private DarkNumericUpDown nudInvStackLimit;
        private Label lblBankStackLimit;
        private Label lblInvStackLimit;
        private DarkCheckBox chkCanGuildBank;
        private DarkGroupBox grpRequirements;
        private Label lblCannotUse;
        private DarkTextBox txtCannotUse;
        private DarkGroupBox grpShieldProperties;
        private DarkNumericUpDown nudBlockDmgAbs;
        private Label lblBlockDmgAbs;
        private DarkNumericUpDown nudBlockAmount;
        private Label lblBlockAmount;
        private DarkNumericUpDown nudBlockChance;
        private Label lblBlockChance;
        private Label lblSpriteAttack;
        private DarkComboBox cmbWeaponSprite;
        private DarkNumericUpDown nudItemDespawnTime;
        private Label lblDespawnTime;
        private ToolTip tooltips;
        private DarkGroupBox grpEffects;
        private ListBox lstBonusEffects;
        private DarkGroupBox grpCooldown;
        private DarkGroupBox grpStack;
        private DarkGroupBox grpPaperdoll;
        private Label label7;
        private DarkNumericUpDown nudCurPercentage;
        private Label label8;
        private DarkNumericUpDown nudCur;
        private Label label4;
        private DarkNumericUpDown nudDmgPercentage;
        private Label label5;
        private DarkNumericUpDown nudDmg;
        private Label label1;
        private DarkNumericUpDown nudAgiPercentage;
        private Label label2;
        private DarkNumericUpDown nudAgi;
        private Label CurLbl;
        private Label DmgLbl;
        private Label label3;
      
        private DarkComboBox cmbEquipmentAnimation;
        private Label lblEquipmentAnimation;
        private Label label6;
        private DarkNumericUpDown nudVitPercentage;
        private Label label9;
        private DarkNumericUpDown nudVit;
        private Label label10;
    }
}
