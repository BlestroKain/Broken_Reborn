using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandConditionalBranch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpConditional = new DarkUI.Controls.DarkGroupBox();
            this.chkHasElse = new DarkUI.Controls.DarkCheckBox();
            this.chkNegated = new DarkUI.Controls.DarkCheckBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.cmbConditionType = new DarkUI.Controls.DarkComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpLevelStat = new DarkUI.Controls.DarkGroupBox();
            this.chkStatIgnoreBuffs = new DarkUI.Controls.DarkCheckBox();
            this.nudLevelStatValue = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbLevelStat = new DarkUI.Controls.DarkComboBox();
            this.lblLevelOrStat = new System.Windows.Forms.Label();
            this.lblLvlStatValue = new System.Windows.Forms.Label();
            this.cmbLevelComparator = new DarkUI.Controls.DarkComboBox();
            this.lblLevelComparator = new System.Windows.Forms.Label();
            this.grpMapIs = new DarkUI.Controls.DarkGroupBox();
            this.btnSelectMap = new DarkUI.Controls.DarkButton();
            this.grpGender = new DarkUI.Controls.DarkGroupBox();
            this.cmbGender = new DarkUI.Controls.DarkComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.grpEquippedItem = new DarkUI.Controls.DarkGroupBox();
            this.cmbEquippedItem = new DarkUI.Controls.DarkComboBox();
            this.lblEquippedItem = new System.Windows.Forms.Label();
            this.grpCheckEquippedSlot = new DarkUI.Controls.DarkGroupBox();
            this.cmbCheckEquippedSlot = new DarkUI.Controls.DarkComboBox();
            this.lblCheckEquippedSlot = new System.Windows.Forms.Label();
            this.grpNpc = new DarkUI.Controls.DarkGroupBox();
            this.chkNpc = new DarkUI.Controls.DarkCheckBox();
            this.cmbNpcs = new DarkUI.Controls.DarkComboBox();
            this.lblNpc = new System.Windows.Forms.Label();
            this.grpInventoryConditions = new DarkUI.Controls.DarkGroupBox();
            this.chkBank = new DarkUI.Controls.DarkCheckBox();
            this.grpVariableAmount = new DarkUI.Controls.DarkGroupBox();
            this.cmbInvVariable = new DarkUI.Controls.DarkComboBox();
            this.lblInvVariable = new System.Windows.Forms.Label();
            this.rdoInvGuildVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoInvGlobalVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoInvPlayerVariable = new DarkUI.Controls.DarkRadioButton();
            this.grpManualAmount = new DarkUI.Controls.DarkGroupBox();
            this.nudItemAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.lblItemQuantity = new System.Windows.Forms.Label();
            this.grpAmountType = new DarkUI.Controls.DarkGroupBox();
            this.rdoVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoManual = new DarkUI.Controls.DarkRadioButton();
            this.cmbItem = new DarkUI.Controls.DarkComboBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.grpVariable = new DarkUI.Controls.DarkGroupBox();
            this.grpSelectVariable = new DarkUI.Controls.DarkGroupBox();
            this.rdoUserVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoPlayerVariable = new DarkUI.Controls.DarkRadioButton();
            this.cmbVariable = new DarkUI.Controls.DarkComboBox();
            this.rdoGlobalVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoGuildVariable = new DarkUI.Controls.DarkRadioButton();
            this.grpNumericVariable = new DarkUI.Controls.DarkGroupBox();
            this.rdoTimeSystem = new DarkUI.Controls.DarkRadioButton();
            this.cmbCompareUserVar = new DarkUI.Controls.DarkComboBox();
            this.rdoVarCompareUserVar = new DarkUI.Controls.DarkRadioButton();
            this.cmbNumericComparitor = new DarkUI.Controls.DarkComboBox();
            this.nudVariableValue = new DarkUI.Controls.DarkNumericUpDown();
            this.lblNumericComparator = new System.Windows.Forms.Label();
            this.cmbCompareGuildVar = new DarkUI.Controls.DarkComboBox();
            this.rdoVarCompareStaticValue = new DarkUI.Controls.DarkRadioButton();
            this.cmbComparePlayerVar = new DarkUI.Controls.DarkComboBox();
            this.rdoVarComparePlayerVar = new DarkUI.Controls.DarkRadioButton();
            this.rdoVarCompareGuildVar = new DarkUI.Controls.DarkRadioButton();
            this.cmbCompareGlobalVar = new DarkUI.Controls.DarkComboBox();
            this.rdoVarCompareGlobalVar = new DarkUI.Controls.DarkRadioButton();
            this.grpStringVariable = new DarkUI.Controls.DarkGroupBox();
            this.lblStringTextVariables = new System.Windows.Forms.Label();
            this.lblStringComparatorValue = new System.Windows.Forms.Label();
            this.txtStringValue = new DarkUI.Controls.DarkTextBox();
            this.cmbStringComparitor = new DarkUI.Controls.DarkComboBox();
            this.lblStringComparator = new System.Windows.Forms.Label();
            this.grpBooleanVariable = new DarkUI.Controls.DarkGroupBox();
            this.cmbBooleanUserVariable = new DarkUI.Controls.DarkComboBox();
            this.optBooleanUserVariable = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanTrue = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanFalse = new DarkUI.Controls.DarkRadioButton();
            this.cmbBooleanComparator = new DarkUI.Controls.DarkComboBox();
            this.lblBooleanComparator = new System.Windows.Forms.Label();
            this.cmbBooleanGuildVariable = new DarkUI.Controls.DarkComboBox();
            this.cmbBooleanPlayerVariable = new DarkUI.Controls.DarkComboBox();
            this.optBooleanPlayerVariable = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanGuildVariable = new DarkUI.Controls.DarkRadioButton();
            this.cmbBooleanGlobalVariable = new DarkUI.Controls.DarkComboBox();
            this.optBooleanGlobalVariable = new DarkUI.Controls.DarkRadioButton();
            this.grpMapZoneType = new DarkUI.Controls.DarkGroupBox();
            this.lblMapZoneType = new System.Windows.Forms.Label();
            this.cmbMapZoneType = new DarkUI.Controls.DarkComboBox();
            this.grpInGuild = new DarkUI.Controls.DarkGroupBox();
            this.lblRank = new System.Windows.Forms.Label();
            this.cmbRank = new DarkUI.Controls.DarkComboBox();
            this.grpQuestCompleted = new DarkUI.Controls.DarkGroupBox();
            this.lblQuestCompleted = new System.Windows.Forms.Label();
            this.cmbCompletedQuest = new DarkUI.Controls.DarkComboBox();
            this.grpQuestInProgress = new DarkUI.Controls.DarkGroupBox();
            this.lblQuestTask = new System.Windows.Forms.Label();
            this.cmbQuestTask = new DarkUI.Controls.DarkComboBox();
            this.cmbTaskModifier = new DarkUI.Controls.DarkComboBox();
            this.lblQuestIs = new System.Windows.Forms.Label();
            this.lblQuestProgress = new System.Windows.Forms.Label();
            this.cmbQuestInProgress = new DarkUI.Controls.DarkComboBox();
            this.grpStartQuest = new DarkUI.Controls.DarkGroupBox();
            this.lblStartQuest = new System.Windows.Forms.Label();
            this.cmbStartQuest = new DarkUI.Controls.DarkComboBox();
            this.grpTime = new DarkUI.Controls.DarkGroupBox();
            this.lblEndRange = new System.Windows.Forms.Label();
            this.lblStartRange = new System.Windows.Forms.Label();
            this.cmbTime2 = new DarkUI.Controls.DarkComboBox();
            this.cmbTime1 = new DarkUI.Controls.DarkComboBox();
            this.lblAnd = new System.Windows.Forms.Label();
            this.grpPowerIs = new DarkUI.Controls.DarkGroupBox();
            this.cmbPower = new DarkUI.Controls.DarkComboBox();
            this.lblPower = new System.Windows.Forms.Label();
            this.grpSelfSwitch = new DarkUI.Controls.DarkGroupBox();
            this.cmbSelfSwitchVal = new DarkUI.Controls.DarkComboBox();
            this.lblSelfSwitchIs = new System.Windows.Forms.Label();
            this.cmbSelfSwitch = new DarkUI.Controls.DarkComboBox();
            this.lblSelfSwitch = new System.Windows.Forms.Label();
            this.grpSpell = new DarkUI.Controls.DarkGroupBox();
            this.cmbSpell = new DarkUI.Controls.DarkComboBox();
            this.lblSpell = new System.Windows.Forms.Label();
            this.grpClass = new DarkUI.Controls.DarkGroupBox();
            this.cmbClass = new DarkUI.Controls.DarkComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.grpJobLevel = new DarkUI.Controls.DarkGroupBox();
            this.cmbJobLevel = new DarkUI.Controls.DarkComboBox();
            this.nudJobValue = new DarkUI.Controls.DarkNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbJobComparator = new DarkUI.Controls.DarkComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpConditional.SuspendLayout();
            this.grpLevelStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelStatValue)).BeginInit();
            this.grpMapIs.SuspendLayout();
            this.grpGender.SuspendLayout();
            this.grpEquippedItem.SuspendLayout();
            this.grpCheckEquippedSlot.SuspendLayout();
            this.grpNpc.SuspendLayout();
            this.grpInventoryConditions.SuspendLayout();
            this.grpVariableAmount.SuspendLayout();
            this.grpManualAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemAmount)).BeginInit();
            this.grpAmountType.SuspendLayout();
            this.grpVariable.SuspendLayout();
            this.grpSelectVariable.SuspendLayout();
            this.grpNumericVariable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariableValue)).BeginInit();
            this.grpStringVariable.SuspendLayout();
            this.grpBooleanVariable.SuspendLayout();
            this.grpMapZoneType.SuspendLayout();
            this.grpInGuild.SuspendLayout();
            this.grpQuestCompleted.SuspendLayout();
            this.grpQuestInProgress.SuspendLayout();
            this.grpStartQuest.SuspendLayout();
            this.grpTime.SuspendLayout();
            this.grpPowerIs.SuspendLayout();
            this.grpSelfSwitch.SuspendLayout();
            this.grpSpell.SuspendLayout();
            this.grpClass.SuspendLayout();
            this.grpJobLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudJobValue)).BeginInit();
            this.SuspendLayout();
            // 
            // grpConditional
            // 
            this.grpConditional.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpConditional.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpConditional.Controls.Add(this.grpJobLevel);
            this.grpConditional.Controls.Add(this.chkHasElse);
            this.grpConditional.Controls.Add(this.chkNegated);
            this.grpConditional.Controls.Add(this.btnSave);
            this.grpConditional.Controls.Add(this.cmbConditionType);
            this.grpConditional.Controls.Add(this.lblType);
            this.grpConditional.Controls.Add(this.btnCancel);
            this.grpConditional.Controls.Add(this.grpLevelStat);
            this.grpConditional.Controls.Add(this.grpMapIs);
            this.grpConditional.Controls.Add(this.grpGender);
            this.grpConditional.Controls.Add(this.grpEquippedItem);
            this.grpConditional.Controls.Add(this.grpCheckEquippedSlot);
            this.grpConditional.Controls.Add(this.grpNpc);
            this.grpConditional.Controls.Add(this.grpInventoryConditions);
            this.grpConditional.Controls.Add(this.grpVariable);
            this.grpConditional.Controls.Add(this.grpMapZoneType);
            this.grpConditional.Controls.Add(this.grpInGuild);
            this.grpConditional.Controls.Add(this.grpQuestCompleted);
            this.grpConditional.Controls.Add(this.grpQuestInProgress);
            this.grpConditional.Controls.Add(this.grpStartQuest);
            this.grpConditional.Controls.Add(this.grpTime);
            this.grpConditional.Controls.Add(this.grpPowerIs);
            this.grpConditional.Controls.Add(this.grpSelfSwitch);
            this.grpConditional.Controls.Add(this.grpSpell);
            this.grpConditional.Controls.Add(this.grpClass);
            this.grpConditional.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpConditional.Location = new System.Drawing.Point(4, 4);
            this.grpConditional.Margin = new System.Windows.Forms.Padding(4);
            this.grpConditional.Name = "grpConditional";
            this.grpConditional.Padding = new System.Windows.Forms.Padding(4);
            this.grpConditional.Size = new System.Drawing.Size(371, 578);
            this.grpConditional.TabIndex = 17;
            this.grpConditional.TabStop = false;
            this.grpConditional.Text = "Conditional";
            // 
            // chkHasElse
            // 
            this.chkHasElse.Location = new System.Drawing.Point(145, 508);
            this.chkHasElse.Margin = new System.Windows.Forms.Padding(4);
            this.chkHasElse.Name = "chkHasElse";
            this.chkHasElse.Size = new System.Drawing.Size(96, 21);
            this.chkHasElse.TabIndex = 56;
            this.chkHasElse.Text = "Has Else";
            // 
            // chkNegated
            // 
            this.chkNegated.Location = new System.Drawing.Point(261, 508);
            this.chkNegated.Margin = new System.Windows.Forms.Padding(4);
            this.chkNegated.Name = "chkNegated";
            this.chkNegated.Size = new System.Drawing.Size(96, 21);
            this.chkNegated.TabIndex = 34;
            this.chkNegated.Text = "Negated";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 543);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbConditionType
            // 
            this.cmbConditionType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbConditionType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbConditionType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbConditionType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbConditionType.DrawDropdownHoverOutline = false;
            this.cmbConditionType.DrawFocusRectangle = false;
            this.cmbConditionType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbConditionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConditionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbConditionType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbConditionType.FormattingEnabled = true;
            this.cmbConditionType.Items.AddRange(new object[] {
            "Variable is...",
            "Has item...",
            "Class is...",
            "Knows spell...",
            "Level is....",
            "Self Switch is....",
            "Power level is....",
            "Time is between....",
            "Can Start Quest....",
            "Quest In Progress....",
            "Quest Completed....",
            "Player death...",
            "No NPCs on the map...",
            "Gender is...",
            "Item Equipped Is...",
            "Has X free Inventory slots...",
            "In Guild With At Least Rank...",
            "Check Equipped Slot...",
            "Job Level Is.."});
            this.cmbConditionType.Location = new System.Drawing.Point(117, 16);
            this.cmbConditionType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbConditionType.Name = "cmbConditionType";
            this.cmbConditionType.Size = new System.Drawing.Size(243, 23);
            this.cmbConditionType.TabIndex = 22;
            this.cmbConditionType.Text = "Variable is...";
            this.cmbConditionType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbConditionType.SelectedIndexChanged += new System.EventHandler(this.cmbConditionType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(8, 20);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(101, 16);
            this.lblType.TabIndex = 21;
            this.lblType.Text = "Condition Type:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(147, 543);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpLevelStat
            // 
            this.grpLevelStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpLevelStat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpLevelStat.Controls.Add(this.chkStatIgnoreBuffs);
            this.grpLevelStat.Controls.Add(this.nudLevelStatValue);
            this.grpLevelStat.Controls.Add(this.cmbLevelStat);
            this.grpLevelStat.Controls.Add(this.lblLevelOrStat);
            this.grpLevelStat.Controls.Add(this.lblLvlStatValue);
            this.grpLevelStat.Controls.Add(this.cmbLevelComparator);
            this.grpLevelStat.Controls.Add(this.lblLevelComparator);
            this.grpLevelStat.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpLevelStat.Location = new System.Drawing.Point(12, 49);
            this.grpLevelStat.Margin = new System.Windows.Forms.Padding(4);
            this.grpLevelStat.Name = "grpLevelStat";
            this.grpLevelStat.Padding = new System.Windows.Forms.Padding(4);
            this.grpLevelStat.Size = new System.Drawing.Size(349, 172);
            this.grpLevelStat.TabIndex = 28;
            this.grpLevelStat.TabStop = false;
            this.grpLevelStat.Text = "Level or Stat is...";
            // 
            // chkStatIgnoreBuffs
            // 
            this.chkStatIgnoreBuffs.Location = new System.Drawing.Point(17, 142);
            this.chkStatIgnoreBuffs.Margin = new System.Windows.Forms.Padding(4);
            this.chkStatIgnoreBuffs.Name = "chkStatIgnoreBuffs";
            this.chkStatIgnoreBuffs.Size = new System.Drawing.Size(281, 21);
            this.chkStatIgnoreBuffs.TabIndex = 32;
            this.chkStatIgnoreBuffs.Text = "Ignore equipment & spell buffs.";
            // 
            // nudLevelStatValue
            // 
            this.nudLevelStatValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudLevelStatValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudLevelStatValue.Location = new System.Drawing.Point(105, 107);
            this.nudLevelStatValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudLevelStatValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudLevelStatValue.Name = "nudLevelStatValue";
            this.nudLevelStatValue.Size = new System.Drawing.Size(237, 22);
            this.nudLevelStatValue.TabIndex = 8;
            this.nudLevelStatValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbLevelStat
            // 
            this.cmbLevelStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbLevelStat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbLevelStat.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbLevelStat.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbLevelStat.DrawDropdownHoverOutline = false;
            this.cmbLevelStat.DrawFocusRectangle = false;
            this.cmbLevelStat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevelStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevelStat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLevelStat.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbLevelStat.FormattingEnabled = true;
            this.cmbLevelStat.Items.AddRange(new object[] {
            "Level",
            "Attack",
            "Defense",
            "Speed",
            "Ability Power",
            "Magic Resist"});
            this.cmbLevelStat.Location = new System.Drawing.Point(105, 28);
            this.cmbLevelStat.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLevelStat.Name = "cmbLevelStat";
            this.cmbLevelStat.Size = new System.Drawing.Size(235, 23);
            this.cmbLevelStat.TabIndex = 7;
            this.cmbLevelStat.Text = "Level";
            this.cmbLevelStat.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblLevelOrStat
            // 
            this.lblLevelOrStat.AutoSize = true;
            this.lblLevelOrStat.Location = new System.Drawing.Point(9, 31);
            this.lblLevelOrStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevelOrStat.Name = "lblLevelOrStat";
            this.lblLevelOrStat.Size = new System.Drawing.Size(84, 16);
            this.lblLevelOrStat.TabIndex = 6;
            this.lblLevelOrStat.Text = "Level or Stat:";
            // 
            // lblLvlStatValue
            // 
            this.lblLvlStatValue.AutoSize = true;
            this.lblLvlStatValue.Location = new System.Drawing.Point(13, 110);
            this.lblLvlStatValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLvlStatValue.Name = "lblLvlStatValue";
            this.lblLvlStatValue.Size = new System.Drawing.Size(45, 16);
            this.lblLvlStatValue.TabIndex = 4;
            this.lblLvlStatValue.Text = "Value:";
            // 
            // cmbLevelComparator
            // 
            this.cmbLevelComparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbLevelComparator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbLevelComparator.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbLevelComparator.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbLevelComparator.DrawDropdownHoverOutline = false;
            this.cmbLevelComparator.DrawFocusRectangle = false;
            this.cmbLevelComparator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevelComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevelComparator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLevelComparator.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbLevelComparator.FormattingEnabled = true;
            this.cmbLevelComparator.Location = new System.Drawing.Point(105, 65);
            this.cmbLevelComparator.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLevelComparator.Name = "cmbLevelComparator";
            this.cmbLevelComparator.Size = new System.Drawing.Size(235, 23);
            this.cmbLevelComparator.TabIndex = 3;
            this.cmbLevelComparator.Text = null;
            this.cmbLevelComparator.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblLevelComparator
            // 
            this.lblLevelComparator.AutoSize = true;
            this.lblLevelComparator.Location = new System.Drawing.Point(9, 68);
            this.lblLevelComparator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevelComparator.Name = "lblLevelComparator";
            this.lblLevelComparator.Size = new System.Drawing.Size(81, 16);
            this.lblLevelComparator.TabIndex = 2;
            this.lblLevelComparator.Text = "Comparator:";
            // 
            // grpMapIs
            // 
            this.grpMapIs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpMapIs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpMapIs.Controls.Add(this.btnSelectMap);
            this.grpMapIs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpMapIs.Location = new System.Drawing.Point(12, 49);
            this.grpMapIs.Margin = new System.Windows.Forms.Padding(4);
            this.grpMapIs.Name = "grpMapIs";
            this.grpMapIs.Padding = new System.Windows.Forms.Padding(4);
            this.grpMapIs.Size = new System.Drawing.Size(348, 75);
            this.grpMapIs.TabIndex = 35;
            this.grpMapIs.TabStop = false;
            this.grpMapIs.Text = "Map Is...";
            // 
            // btnSelectMap
            // 
            this.btnSelectMap.Location = new System.Drawing.Point(12, 26);
            this.btnSelectMap.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectMap.Name = "btnSelectMap";
            this.btnSelectMap.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSelectMap.Size = new System.Drawing.Size(325, 28);
            this.btnSelectMap.TabIndex = 21;
            this.btnSelectMap.Text = "Select Map";
            this.btnSelectMap.Click += new System.EventHandler(this.btnSelectMap_Click);
            // 
            // grpGender
            // 
            this.grpGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpGender.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGender.Controls.Add(this.cmbGender);
            this.grpGender.Controls.Add(this.lblGender);
            this.grpGender.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGender.Location = new System.Drawing.Point(12, 49);
            this.grpGender.Margin = new System.Windows.Forms.Padding(4);
            this.grpGender.Name = "grpGender";
            this.grpGender.Padding = new System.Windows.Forms.Padding(4);
            this.grpGender.Size = new System.Drawing.Size(348, 63);
            this.grpGender.TabIndex = 33;
            this.grpGender.TabStop = false;
            this.grpGender.Text = "Gender Is...";
            // 
            // cmbGender
            // 
            this.cmbGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbGender.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbGender.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbGender.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbGender.DrawDropdownHoverOutline = false;
            this.cmbGender.DrawFocusRectangle = false;
            this.cmbGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGender.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(105, 21);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(231, 23);
            this.cmbGender.TabIndex = 1;
            this.cmbGender.Text = null;
            this.cmbGender.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(9, 25);
            this.lblGender.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(55, 16);
            this.lblGender.TabIndex = 0;
            this.lblGender.Text = "Gender:";
            // 
            // grpEquippedItem
            // 
            this.grpEquippedItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpEquippedItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEquippedItem.Controls.Add(this.cmbEquippedItem);
            this.grpEquippedItem.Controls.Add(this.lblEquippedItem);
            this.grpEquippedItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEquippedItem.Location = new System.Drawing.Point(12, 49);
            this.grpEquippedItem.Margin = new System.Windows.Forms.Padding(4);
            this.grpEquippedItem.Name = "grpEquippedItem";
            this.grpEquippedItem.Padding = new System.Windows.Forms.Padding(4);
            this.grpEquippedItem.Size = new System.Drawing.Size(349, 71);
            this.grpEquippedItem.TabIndex = 26;
            this.grpEquippedItem.TabStop = false;
            this.grpEquippedItem.Text = "Has Equipped Item";
            // 
            // cmbEquippedItem
            // 
            this.cmbEquippedItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEquippedItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEquippedItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEquippedItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEquippedItem.DrawDropdownHoverOutline = false;
            this.cmbEquippedItem.DrawFocusRectangle = false;
            this.cmbEquippedItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEquippedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquippedItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEquippedItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEquippedItem.FormattingEnabled = true;
            this.cmbEquippedItem.Location = new System.Drawing.Point(140, 33);
            this.cmbEquippedItem.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEquippedItem.Name = "cmbEquippedItem";
            this.cmbEquippedItem.Size = new System.Drawing.Size(199, 23);
            this.cmbEquippedItem.TabIndex = 3;
            this.cmbEquippedItem.Text = null;
            this.cmbEquippedItem.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblEquippedItem
            // 
            this.lblEquippedItem.AutoSize = true;
            this.lblEquippedItem.Location = new System.Drawing.Point(8, 30);
            this.lblEquippedItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEquippedItem.Name = "lblEquippedItem";
            this.lblEquippedItem.Size = new System.Drawing.Size(35, 16);
            this.lblEquippedItem.TabIndex = 2;
            this.lblEquippedItem.Text = "Item:";
            // 
            // grpCheckEquippedSlot
            // 
            this.grpCheckEquippedSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpCheckEquippedSlot.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCheckEquippedSlot.Controls.Add(this.cmbCheckEquippedSlot);
            this.grpCheckEquippedSlot.Controls.Add(this.lblCheckEquippedSlot);
            this.grpCheckEquippedSlot.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCheckEquippedSlot.Location = new System.Drawing.Point(12, 49);
            this.grpCheckEquippedSlot.Margin = new System.Windows.Forms.Padding(4);
            this.grpCheckEquippedSlot.Name = "grpCheckEquippedSlot";
            this.grpCheckEquippedSlot.Padding = new System.Windows.Forms.Padding(4);
            this.grpCheckEquippedSlot.Size = new System.Drawing.Size(349, 71);
            this.grpCheckEquippedSlot.TabIndex = 27;
            this.grpCheckEquippedSlot.TabStop = false;
            this.grpCheckEquippedSlot.Text = "Check Equipped Slot:";
            // 
            // cmbCheckEquippedSlot
            // 
            this.cmbCheckEquippedSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCheckEquippedSlot.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCheckEquippedSlot.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCheckEquippedSlot.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCheckEquippedSlot.DrawDropdownHoverOutline = false;
            this.cmbCheckEquippedSlot.DrawFocusRectangle = false;
            this.cmbCheckEquippedSlot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCheckEquippedSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckEquippedSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCheckEquippedSlot.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCheckEquippedSlot.FormattingEnabled = true;
            this.cmbCheckEquippedSlot.Location = new System.Drawing.Point(140, 33);
            this.cmbCheckEquippedSlot.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCheckEquippedSlot.Name = "cmbCheckEquippedSlot";
            this.cmbCheckEquippedSlot.Size = new System.Drawing.Size(199, 23);
            this.cmbCheckEquippedSlot.TabIndex = 3;
            this.cmbCheckEquippedSlot.Text = null;
            this.cmbCheckEquippedSlot.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblCheckEquippedSlot
            // 
            this.lblCheckEquippedSlot.AutoSize = true;
            this.lblCheckEquippedSlot.Location = new System.Drawing.Point(8, 37);
            this.lblCheckEquippedSlot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheckEquippedSlot.Name = "lblCheckEquippedSlot";
            this.lblCheckEquippedSlot.Size = new System.Drawing.Size(33, 16);
            this.lblCheckEquippedSlot.TabIndex = 2;
            this.lblCheckEquippedSlot.Text = "Slot:";
            // 
            // grpNpc
            // 
            this.grpNpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpNpc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNpc.Controls.Add(this.chkNpc);
            this.grpNpc.Controls.Add(this.cmbNpcs);
            this.grpNpc.Controls.Add(this.lblNpc);
            this.grpNpc.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNpc.Location = new System.Drawing.Point(13, 59);
            this.grpNpc.Margin = new System.Windows.Forms.Padding(4);
            this.grpNpc.Name = "grpNpc";
            this.grpNpc.Padding = new System.Windows.Forms.Padding(4);
            this.grpNpc.Size = new System.Drawing.Size(347, 91);
            this.grpNpc.TabIndex = 40;
            this.grpNpc.TabStop = false;
            this.grpNpc.Text = "NPCs";
            this.grpNpc.Visible = false;
            // 
            // chkNpc
            // 
            this.chkNpc.Location = new System.Drawing.Point(9, 27);
            this.chkNpc.Margin = new System.Windows.Forms.Padding(4);
            this.chkNpc.Name = "chkNpc";
            this.chkNpc.Size = new System.Drawing.Size(131, 21);
            this.chkNpc.TabIndex = 60;
            this.chkNpc.Text = "Specify NPC?";
            this.chkNpc.CheckedChanged += new System.EventHandler(this.chkNpc_CheckedChanged);
            // 
            // cmbNpcs
            // 
            this.cmbNpcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNpcs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNpcs.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNpcs.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNpcs.DrawDropdownHoverOutline = false;
            this.cmbNpcs.DrawFocusRectangle = false;
            this.cmbNpcs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNpcs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNpcs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNpcs.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNpcs.FormattingEnabled = true;
            this.cmbNpcs.Location = new System.Drawing.Point(89, 55);
            this.cmbNpcs.Margin = new System.Windows.Forms.Padding(4);
            this.cmbNpcs.Name = "cmbNpcs";
            this.cmbNpcs.Size = new System.Drawing.Size(235, 23);
            this.cmbNpcs.TabIndex = 39;
            this.cmbNpcs.Text = null;
            this.cmbNpcs.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblNpc
            // 
            this.lblNpc.AutoSize = true;
            this.lblNpc.Location = new System.Drawing.Point(8, 60);
            this.lblNpc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNpc.Name = "lblNpc";
            this.lblNpc.Size = new System.Drawing.Size(38, 16);
            this.lblNpc.TabIndex = 38;
            this.lblNpc.Text = "NPC:";
            // 
            // grpInventoryConditions
            // 
            this.grpInventoryConditions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpInventoryConditions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpInventoryConditions.Controls.Add(this.chkBank);
            this.grpInventoryConditions.Controls.Add(this.grpVariableAmount);
            this.grpInventoryConditions.Controls.Add(this.grpManualAmount);
            this.grpInventoryConditions.Controls.Add(this.grpAmountType);
            this.grpInventoryConditions.Controls.Add(this.cmbItem);
            this.grpInventoryConditions.Controls.Add(this.lblItem);
            this.grpInventoryConditions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInventoryConditions.Location = new System.Drawing.Point(12, 49);
            this.grpInventoryConditions.Margin = new System.Windows.Forms.Padding(4);
            this.grpInventoryConditions.Name = "grpInventoryConditions";
            this.grpInventoryConditions.Padding = new System.Windows.Forms.Padding(4);
            this.grpInventoryConditions.Size = new System.Drawing.Size(349, 292);
            this.grpInventoryConditions.TabIndex = 25;
            this.grpInventoryConditions.TabStop = false;
            this.grpInventoryConditions.Text = "Has Item";
            // 
            // chkBank
            // 
            this.chkBank.Location = new System.Drawing.Point(203, 260);
            this.chkBank.Margin = new System.Windows.Forms.Padding(4);
            this.chkBank.Name = "chkBank";
            this.chkBank.Size = new System.Drawing.Size(131, 21);
            this.chkBank.TabIndex = 59;
            this.chkBank.Text = "Check Bank?";
            // 
            // grpVariableAmount
            // 
            this.grpVariableAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpVariableAmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVariableAmount.Controls.Add(this.cmbInvVariable);
            this.grpVariableAmount.Controls.Add(this.lblInvVariable);
            this.grpVariableAmount.Controls.Add(this.rdoInvGuildVariable);
            this.grpVariableAmount.Controls.Add(this.rdoInvGlobalVariable);
            this.grpVariableAmount.Controls.Add(this.rdoInvPlayerVariable);
            this.grpVariableAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVariableAmount.Location = new System.Drawing.Point(8, 84);
            this.grpVariableAmount.Margin = new System.Windows.Forms.Padding(4);
            this.grpVariableAmount.Name = "grpVariableAmount";
            this.grpVariableAmount.Padding = new System.Windows.Forms.Padding(4);
            this.grpVariableAmount.Size = new System.Drawing.Size(333, 121);
            this.grpVariableAmount.TabIndex = 39;
            this.grpVariableAmount.TabStop = false;
            this.grpVariableAmount.Text = "Variable Amount:";
            this.grpVariableAmount.Visible = false;
            // 
            // cmbInvVariable
            // 
            this.cmbInvVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbInvVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbInvVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbInvVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbInvVariable.DrawDropdownHoverOutline = false;
            this.cmbInvVariable.DrawFocusRectangle = false;
            this.cmbInvVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInvVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbInvVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbInvVariable.FormattingEnabled = true;
            this.cmbInvVariable.Location = new System.Drawing.Point(89, 85);
            this.cmbInvVariable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbInvVariable.Name = "cmbInvVariable";
            this.cmbInvVariable.Size = new System.Drawing.Size(235, 23);
            this.cmbInvVariable.TabIndex = 39;
            this.cmbInvVariable.Text = null;
            this.cmbInvVariable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblInvVariable
            // 
            this.lblInvVariable.AutoSize = true;
            this.lblInvVariable.Location = new System.Drawing.Point(11, 87);
            this.lblInvVariable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInvVariable.Name = "lblInvVariable";
            this.lblInvVariable.Size = new System.Drawing.Size(58, 16);
            this.lblInvVariable.TabIndex = 38;
            this.lblInvVariable.Text = "Variable";
            // 
            // rdoInvGuildVariable
            // 
            this.rdoInvGuildVariable.AutoSize = true;
            this.rdoInvGuildVariable.Location = new System.Drawing.Point(8, 50);
            this.rdoInvGuildVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoInvGuildVariable.Name = "rdoInvGuildVariable";
            this.rdoInvGuildVariable.Size = new System.Drawing.Size(113, 20);
            this.rdoInvGuildVariable.TabIndex = 37;
            this.rdoInvGuildVariable.Text = "Guild Variable";
            this.rdoInvGuildVariable.CheckedChanged += new System.EventHandler(this.rdoInvGuildVariable_CheckedChanged);
            // 
            // rdoInvGlobalVariable
            // 
            this.rdoInvGlobalVariable.AutoSize = true;
            this.rdoInvGlobalVariable.Location = new System.Drawing.Point(197, 23);
            this.rdoInvGlobalVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoInvGlobalVariable.Name = "rdoInvGlobalVariable";
            this.rdoInvGlobalVariable.Size = new System.Drawing.Size(122, 20);
            this.rdoInvGlobalVariable.TabIndex = 37;
            this.rdoInvGlobalVariable.Text = "Global Variable";
            this.rdoInvGlobalVariable.CheckedChanged += new System.EventHandler(this.rdoInvGlobalVariable_CheckedChanged);
            // 
            // rdoInvPlayerVariable
            // 
            this.rdoInvPlayerVariable.AutoSize = true;
            this.rdoInvPlayerVariable.Checked = true;
            this.rdoInvPlayerVariable.Location = new System.Drawing.Point(8, 23);
            this.rdoInvPlayerVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoInvPlayerVariable.Name = "rdoInvPlayerVariable";
            this.rdoInvPlayerVariable.Size = new System.Drawing.Size(121, 20);
            this.rdoInvPlayerVariable.TabIndex = 36;
            this.rdoInvPlayerVariable.TabStop = true;
            this.rdoInvPlayerVariable.Text = "Player Variable";
            this.rdoInvPlayerVariable.CheckedChanged += new System.EventHandler(this.rdoInvPlayerVariable_CheckedChanged);
            // 
            // grpManualAmount
            // 
            this.grpManualAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpManualAmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpManualAmount.Controls.Add(this.nudItemAmount);
            this.grpManualAmount.Controls.Add(this.lblItemQuantity);
            this.grpManualAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpManualAmount.Location = new System.Drawing.Point(8, 84);
            this.grpManualAmount.Margin = new System.Windows.Forms.Padding(4);
            this.grpManualAmount.Name = "grpManualAmount";
            this.grpManualAmount.Padding = new System.Windows.Forms.Padding(4);
            this.grpManualAmount.Size = new System.Drawing.Size(333, 87);
            this.grpManualAmount.TabIndex = 38;
            this.grpManualAmount.TabStop = false;
            this.grpManualAmount.Text = "Manual Amount:";
            // 
            // nudItemAmount
            // 
            this.nudItemAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudItemAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudItemAmount.Location = new System.Drawing.Point(115, 31);
            this.nudItemAmount.Margin = new System.Windows.Forms.Padding(4);
            this.nudItemAmount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudItemAmount.Name = "nudItemAmount";
            this.nudItemAmount.Size = new System.Drawing.Size(200, 22);
            this.nudItemAmount.TabIndex = 6;
            this.nudItemAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudItemAmount.ValueChanged += new System.EventHandler(this.NudItemAmount_ValueChanged);
            // 
            // lblItemQuantity
            // 
            this.lblItemQuantity.AutoSize = true;
            this.lblItemQuantity.Location = new System.Drawing.Point(19, 33);
            this.lblItemQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemQuantity.Name = "lblItemQuantity";
            this.lblItemQuantity.Size = new System.Drawing.Size(81, 16);
            this.lblItemQuantity.TabIndex = 5;
            this.lblItemQuantity.Text = "Has at least:";
            // 
            // grpAmountType
            // 
            this.grpAmountType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpAmountType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAmountType.Controls.Add(this.rdoVariable);
            this.grpAmountType.Controls.Add(this.rdoManual);
            this.grpAmountType.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAmountType.Location = new System.Drawing.Point(8, 17);
            this.grpAmountType.Margin = new System.Windows.Forms.Padding(4);
            this.grpAmountType.Name = "grpAmountType";
            this.grpAmountType.Padding = new System.Windows.Forms.Padding(4);
            this.grpAmountType.Size = new System.Drawing.Size(333, 59);
            this.grpAmountType.TabIndex = 37;
            this.grpAmountType.TabStop = false;
            this.grpAmountType.Text = "Amount Type:";
            // 
            // rdoVariable
            // 
            this.rdoVariable.AutoSize = true;
            this.rdoVariable.Location = new System.Drawing.Point(241, 23);
            this.rdoVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoVariable.Name = "rdoVariable";
            this.rdoVariable.Size = new System.Drawing.Size(79, 20);
            this.rdoVariable.TabIndex = 36;
            this.rdoVariable.Text = "Variable";
            this.rdoVariable.CheckedChanged += new System.EventHandler(this.rdoVariable_CheckedChanged);
            // 
            // rdoManual
            // 
            this.rdoManual.AutoSize = true;
            this.rdoManual.Checked = true;
            this.rdoManual.Location = new System.Drawing.Point(12, 23);
            this.rdoManual.Margin = new System.Windows.Forms.Padding(4);
            this.rdoManual.Name = "rdoManual";
            this.rdoManual.Size = new System.Drawing.Size(72, 20);
            this.rdoManual.TabIndex = 35;
            this.rdoManual.TabStop = true;
            this.rdoManual.Text = "Manual";
            this.rdoManual.CheckedChanged += new System.EventHandler(this.rdoManual_CheckedChanged);
            // 
            // cmbItem
            // 
            this.cmbItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbItem.DrawDropdownHoverOutline = false;
            this.cmbItem.DrawFocusRectangle = false;
            this.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(56, 224);
            this.cmbItem.Margin = new System.Windows.Forms.Padding(4);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(281, 23);
            this.cmbItem.TabIndex = 3;
            this.cmbItem.Text = null;
            this.cmbItem.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(8, 226);
            this.lblItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(35, 16);
            this.lblItem.TabIndex = 2;
            this.lblItem.Text = "Item:";
            // 
            // grpVariable
            // 
            this.grpVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVariable.Controls.Add(this.grpSelectVariable);
            this.grpVariable.Controls.Add(this.grpNumericVariable);
            this.grpVariable.Controls.Add(this.grpStringVariable);
            this.grpVariable.Controls.Add(this.grpBooleanVariable);
            this.grpVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVariable.Location = new System.Drawing.Point(12, 49);
            this.grpVariable.Margin = new System.Windows.Forms.Padding(4);
            this.grpVariable.Name = "grpVariable";
            this.grpVariable.Padding = new System.Windows.Forms.Padding(4);
            this.grpVariable.Size = new System.Drawing.Size(349, 452);
            this.grpVariable.TabIndex = 24;
            this.grpVariable.TabStop = false;
            this.grpVariable.Text = "Variable is...";
            // 
            // grpSelectVariable
            // 
            this.grpSelectVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSelectVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSelectVariable.Controls.Add(this.rdoUserVariable);
            this.grpSelectVariable.Controls.Add(this.rdoPlayerVariable);
            this.grpSelectVariable.Controls.Add(this.cmbVariable);
            this.grpSelectVariable.Controls.Add(this.rdoGlobalVariable);
            this.grpSelectVariable.Controls.Add(this.rdoGuildVariable);
            this.grpSelectVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSelectVariable.Location = new System.Drawing.Point(9, 20);
            this.grpSelectVariable.Margin = new System.Windows.Forms.Padding(4);
            this.grpSelectVariable.Name = "grpSelectVariable";
            this.grpSelectVariable.Padding = new System.Windows.Forms.Padding(4);
            this.grpSelectVariable.Size = new System.Drawing.Size(329, 143);
            this.grpSelectVariable.TabIndex = 50;
            this.grpSelectVariable.TabStop = false;
            this.grpSelectVariable.Text = "Select Variable";
            // 
            // rdoUserVariable
            // 
            this.rdoUserVariable.AutoSize = true;
            this.rdoUserVariable.Location = new System.Drawing.Point(155, 50);
            this.rdoUserVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoUserVariable.Name = "rdoUserVariable";
            this.rdoUserVariable.Size = new System.Drawing.Size(130, 20);
            this.rdoUserVariable.TabIndex = 36;
            this.rdoUserVariable.Text = "Account Variable";
            this.rdoUserVariable.CheckedChanged += new System.EventHandler(this.rdoUserVariable_CheckedChanged);
            // 
            // rdoPlayerVariable
            // 
            this.rdoPlayerVariable.AutoSize = true;
            this.rdoPlayerVariable.Checked = true;
            this.rdoPlayerVariable.Location = new System.Drawing.Point(8, 23);
            this.rdoPlayerVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoPlayerVariable.Name = "rdoPlayerVariable";
            this.rdoPlayerVariable.Size = new System.Drawing.Size(121, 20);
            this.rdoPlayerVariable.TabIndex = 34;
            this.rdoPlayerVariable.TabStop = true;
            this.rdoPlayerVariable.Text = "Player Variable";
            this.rdoPlayerVariable.CheckedChanged += new System.EventHandler(this.rdoPlayerVariable_CheckedChanged);
            // 
            // cmbVariable
            // 
            this.cmbVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVariable.DrawDropdownHoverOutline = false;
            this.cmbVariable.DrawFocusRectangle = false;
            this.cmbVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVariable.FormattingEnabled = true;
            this.cmbVariable.Location = new System.Drawing.Point(8, 95);
            this.cmbVariable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(312, 23);
            this.cmbVariable.TabIndex = 22;
            this.cmbVariable.Text = null;
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariable.SelectedIndexChanged += new System.EventHandler(this.cmbVariable_SelectedIndexChanged);
            // 
            // rdoGlobalVariable
            // 
            this.rdoGlobalVariable.AutoSize = true;
            this.rdoGlobalVariable.Location = new System.Drawing.Point(155, 23);
            this.rdoGlobalVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoGlobalVariable.Name = "rdoGlobalVariable";
            this.rdoGlobalVariable.Size = new System.Drawing.Size(122, 20);
            this.rdoGlobalVariable.TabIndex = 35;
            this.rdoGlobalVariable.Text = "Global Variable";
            this.rdoGlobalVariable.CheckedChanged += new System.EventHandler(this.rdoGlobalVariable_CheckedChanged);
            // 
            // rdoGuildVariable
            // 
            this.rdoGuildVariable.AutoSize = true;
            this.rdoGuildVariable.Location = new System.Drawing.Point(8, 50);
            this.rdoGuildVariable.Margin = new System.Windows.Forms.Padding(4);
            this.rdoGuildVariable.Name = "rdoGuildVariable";
            this.rdoGuildVariable.Size = new System.Drawing.Size(113, 20);
            this.rdoGuildVariable.TabIndex = 35;
            this.rdoGuildVariable.Text = "Guild Variable";
            this.rdoGuildVariable.CheckedChanged += new System.EventHandler(this.rdoGuildVariable_CheckedChanged);
            // 
            // grpNumericVariable
            // 
            this.grpNumericVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpNumericVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNumericVariable.Controls.Add(this.rdoTimeSystem);
            this.grpNumericVariable.Controls.Add(this.cmbCompareUserVar);
            this.grpNumericVariable.Controls.Add(this.rdoVarCompareUserVar);
            this.grpNumericVariable.Controls.Add(this.cmbNumericComparitor);
            this.grpNumericVariable.Controls.Add(this.nudVariableValue);
            this.grpNumericVariable.Controls.Add(this.lblNumericComparator);
            this.grpNumericVariable.Controls.Add(this.cmbCompareGuildVar);
            this.grpNumericVariable.Controls.Add(this.rdoVarCompareStaticValue);
            this.grpNumericVariable.Controls.Add(this.cmbComparePlayerVar);
            this.grpNumericVariable.Controls.Add(this.rdoVarComparePlayerVar);
            this.grpNumericVariable.Controls.Add(this.rdoVarCompareGuildVar);
            this.grpNumericVariable.Controls.Add(this.cmbCompareGlobalVar);
            this.grpNumericVariable.Controls.Add(this.rdoVarCompareGlobalVar);
            this.grpNumericVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNumericVariable.Location = new System.Drawing.Point(11, 171);
            this.grpNumericVariable.Margin = new System.Windows.Forms.Padding(4);
            this.grpNumericVariable.Name = "grpNumericVariable";
            this.grpNumericVariable.Padding = new System.Windows.Forms.Padding(4);
            this.grpNumericVariable.Size = new System.Drawing.Size(329, 273);
            this.grpNumericVariable.TabIndex = 51;
            this.grpNumericVariable.TabStop = false;
            this.grpNumericVariable.Text = "Numeric Variable:";
            // 
            // rdoTimeSystem
            // 
            this.rdoTimeSystem.AutoSize = true;
            this.rdoTimeSystem.Location = new System.Drawing.Point(13, 229);
            this.rdoTimeSystem.Margin = new System.Windows.Forms.Padding(4);
            this.rdoTimeSystem.Name = "rdoTimeSystem";
            this.rdoTimeSystem.Size = new System.Drawing.Size(107, 20);
            this.rdoTimeSystem.TabIndex = 52;
            this.rdoTimeSystem.Text = "Time System";
            this.rdoTimeSystem.CheckedChanged += new System.EventHandler(this.rdoTimeSystem_CheckedChanged);
            // 
            // cmbCompareUserVar
            // 
            this.cmbCompareUserVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCompareUserVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCompareUserVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCompareUserVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCompareUserVar.DrawDropdownHoverOutline = false;
            this.cmbCompareUserVar.DrawFocusRectangle = false;
            this.cmbCompareUserVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompareUserVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompareUserVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompareUserVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCompareUserVar.FormattingEnabled = true;
            this.cmbCompareUserVar.Location = new System.Drawing.Point(195, 192);
            this.cmbCompareUserVar.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompareUserVar.Name = "cmbCompareUserVar";
            this.cmbCompareUserVar.Size = new System.Drawing.Size(124, 23);
            this.cmbCompareUserVar.TabIndex = 51;
            this.cmbCompareUserVar.Text = null;
            this.cmbCompareUserVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // rdoVarCompareUserVar
            // 
            this.rdoVarCompareUserVar.AutoSize = true;
            this.rdoVarCompareUserVar.Location = new System.Drawing.Point(13, 193);
            this.rdoVarCompareUserVar.Margin = new System.Windows.Forms.Padding(4);
            this.rdoVarCompareUserVar.Name = "rdoVarCompareUserVar";
            this.rdoVarCompareUserVar.Size = new System.Drawing.Size(171, 20);
            this.rdoVarCompareUserVar.TabIndex = 50;
            this.rdoVarCompareUserVar.Text = "Account Variable Value:";
            this.rdoVarCompareUserVar.CheckedChanged += new System.EventHandler(this.rdoVarCompareUserVar_CheckedChanged);
            // 
            // cmbNumericComparitor
            // 
            this.cmbNumericComparitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNumericComparitor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNumericComparitor.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNumericComparitor.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNumericComparitor.DrawDropdownHoverOutline = false;
            this.cmbNumericComparitor.DrawFocusRectangle = false;
            this.cmbNumericComparitor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNumericComparitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumericComparitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNumericComparitor.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNumericComparitor.FormattingEnabled = true;
            this.cmbNumericComparitor.Items.AddRange(new object[] {
            "Equal To",
            "Greater Than or Equal To",
            "Less Than or Equal To",
            "Greater Than",
            "Less Than",
            "Does Not Equal"});
            this.cmbNumericComparitor.Location = new System.Drawing.Point(153, 25);
            this.cmbNumericComparitor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbNumericComparitor.Name = "cmbNumericComparitor";
            this.cmbNumericComparitor.Size = new System.Drawing.Size(165, 23);
            this.cmbNumericComparitor.TabIndex = 3;
            this.cmbNumericComparitor.Text = "Equal To";
            this.cmbNumericComparitor.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // nudVariableValue
            // 
            this.nudVariableValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudVariableValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudVariableValue.Location = new System.Drawing.Point(153, 59);
            this.nudVariableValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudVariableValue.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudVariableValue.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudVariableValue.Name = "nudVariableValue";
            this.nudVariableValue.Size = new System.Drawing.Size(167, 22);
            this.nudVariableValue.TabIndex = 49;
            this.nudVariableValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblNumericComparator
            // 
            this.lblNumericComparator.AutoSize = true;
            this.lblNumericComparator.Location = new System.Drawing.Point(12, 28);
            this.lblNumericComparator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumericComparator.Name = "lblNumericComparator";
            this.lblNumericComparator.Size = new System.Drawing.Size(78, 16);
            this.lblNumericComparator.TabIndex = 2;
            this.lblNumericComparator.Text = "Comparator";
            // 
            // cmbCompareGuildVar
            // 
            this.cmbCompareGuildVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCompareGuildVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCompareGuildVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCompareGuildVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCompareGuildVar.DrawDropdownHoverOutline = false;
            this.cmbCompareGuildVar.DrawFocusRectangle = false;
            this.cmbCompareGuildVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompareGuildVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompareGuildVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompareGuildVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCompareGuildVar.FormattingEnabled = true;
            this.cmbCompareGuildVar.Location = new System.Drawing.Point(195, 159);
            this.cmbCompareGuildVar.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompareGuildVar.Name = "cmbCompareGuildVar";
            this.cmbCompareGuildVar.Size = new System.Drawing.Size(124, 23);
            this.cmbCompareGuildVar.TabIndex = 48;
            this.cmbCompareGuildVar.Text = null;
            this.cmbCompareGuildVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // rdoVarCompareStaticValue
            // 
            this.rdoVarCompareStaticValue.Location = new System.Drawing.Point(13, 59);
            this.rdoVarCompareStaticValue.Margin = new System.Windows.Forms.Padding(4);
            this.rdoVarCompareStaticValue.Name = "rdoVarCompareStaticValue";
            this.rdoVarCompareStaticValue.Size = new System.Drawing.Size(128, 21);
            this.rdoVarCompareStaticValue.TabIndex = 44;
            this.rdoVarCompareStaticValue.Text = "Static Value:";
            this.rdoVarCompareStaticValue.CheckedChanged += new System.EventHandler(this.rdoVarCompareStaticValue_CheckedChanged);
            // 
            // cmbComparePlayerVar
            // 
            this.cmbComparePlayerVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbComparePlayerVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbComparePlayerVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbComparePlayerVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbComparePlayerVar.DrawDropdownHoverOutline = false;
            this.cmbComparePlayerVar.DrawFocusRectangle = false;
            this.cmbComparePlayerVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbComparePlayerVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComparePlayerVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbComparePlayerVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbComparePlayerVar.FormattingEnabled = true;
            this.cmbComparePlayerVar.Location = new System.Drawing.Point(195, 92);
            this.cmbComparePlayerVar.Margin = new System.Windows.Forms.Padding(4);
            this.cmbComparePlayerVar.Name = "cmbComparePlayerVar";
            this.cmbComparePlayerVar.Size = new System.Drawing.Size(124, 23);
            this.cmbComparePlayerVar.TabIndex = 47;
            this.cmbComparePlayerVar.Text = null;
            this.cmbComparePlayerVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // rdoVarComparePlayerVar
            // 
            this.rdoVarComparePlayerVar.AutoSize = true;
            this.rdoVarComparePlayerVar.Location = new System.Drawing.Point(13, 94);
            this.rdoVarComparePlayerVar.Margin = new System.Windows.Forms.Padding(4);
            this.rdoVarComparePlayerVar.Name = "rdoVarComparePlayerVar";
            this.rdoVarComparePlayerVar.Size = new System.Drawing.Size(162, 20);
            this.rdoVarComparePlayerVar.TabIndex = 45;
            this.rdoVarComparePlayerVar.Text = "Player Variable Value:";
            this.rdoVarComparePlayerVar.CheckedChanged += new System.EventHandler(this.rdoVarComparePlayerVar_CheckedChanged);
            // 
            // rdoVarCompareGuildVar
            // 
            this.rdoVarCompareGuildVar.AutoSize = true;
            this.rdoVarCompareGuildVar.Location = new System.Drawing.Point(13, 160);
            this.rdoVarCompareGuildVar.Margin = new System.Windows.Forms.Padding(4);
            this.rdoVarCompareGuildVar.Name = "rdoVarCompareGuildVar";
            this.rdoVarCompareGuildVar.Size = new System.Drawing.Size(154, 20);
            this.rdoVarCompareGuildVar.TabIndex = 46;
            this.rdoVarCompareGuildVar.Text = "Guild Variable Value:";
            this.rdoVarCompareGuildVar.CheckedChanged += new System.EventHandler(this.rdoVarCompareGuildVar_CheckedChanged);
            // 
            // cmbCompareGlobalVar
            // 
            this.cmbCompareGlobalVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCompareGlobalVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCompareGlobalVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCompareGlobalVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCompareGlobalVar.DrawDropdownHoverOutline = false;
            this.cmbCompareGlobalVar.DrawFocusRectangle = false;
            this.cmbCompareGlobalVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompareGlobalVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompareGlobalVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompareGlobalVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCompareGlobalVar.FormattingEnabled = true;
            this.cmbCompareGlobalVar.Location = new System.Drawing.Point(195, 126);
            this.cmbCompareGlobalVar.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompareGlobalVar.Name = "cmbCompareGlobalVar";
            this.cmbCompareGlobalVar.Size = new System.Drawing.Size(124, 23);
            this.cmbCompareGlobalVar.TabIndex = 48;
            this.cmbCompareGlobalVar.Text = null;
            this.cmbCompareGlobalVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // rdoVarCompareGlobalVar
            // 
            this.rdoVarCompareGlobalVar.AutoSize = true;
            this.rdoVarCompareGlobalVar.Location = new System.Drawing.Point(13, 127);
            this.rdoVarCompareGlobalVar.Margin = new System.Windows.Forms.Padding(4);
            this.rdoVarCompareGlobalVar.Name = "rdoVarCompareGlobalVar";
            this.rdoVarCompareGlobalVar.Size = new System.Drawing.Size(163, 20);
            this.rdoVarCompareGlobalVar.TabIndex = 46;
            this.rdoVarCompareGlobalVar.Text = "Global Variable Value:";
            this.rdoVarCompareGlobalVar.CheckedChanged += new System.EventHandler(this.rdoVarCompareGlobalVar_CheckedChanged);
            // 
            // grpStringVariable
            // 
            this.grpStringVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStringVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStringVariable.Controls.Add(this.lblStringTextVariables);
            this.grpStringVariable.Controls.Add(this.lblStringComparatorValue);
            this.grpStringVariable.Controls.Add(this.txtStringValue);
            this.grpStringVariable.Controls.Add(this.cmbStringComparitor);
            this.grpStringVariable.Controls.Add(this.lblStringComparator);
            this.grpStringVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStringVariable.Location = new System.Drawing.Point(8, 180);
            this.grpStringVariable.Margin = new System.Windows.Forms.Padding(4);
            this.grpStringVariable.Name = "grpStringVariable";
            this.grpStringVariable.Padding = new System.Windows.Forms.Padding(4);
            this.grpStringVariable.Size = new System.Drawing.Size(329, 215);
            this.grpStringVariable.TabIndex = 53;
            this.grpStringVariable.TabStop = false;
            this.grpStringVariable.Text = "String Variable:";
            // 
            // lblStringTextVariables
            // 
            this.lblStringTextVariables.AutoSize = true;
            this.lblStringTextVariables.BackColor = System.Drawing.Color.Transparent;
            this.lblStringTextVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStringTextVariables.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblStringTextVariables.Location = new System.Drawing.Point(11, 177);
            this.lblStringTextVariables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringTextVariables.Name = "lblStringTextVariables";
            this.lblStringTextVariables.Size = new System.Drawing.Size(289, 17);
            this.lblStringTextVariables.TabIndex = 69;
            this.lblStringTextVariables.Text = "Text variables work here. Click here for a list!";
            this.lblStringTextVariables.Click += new System.EventHandler(this.lblStringTextVariables_Click);
            // 
            // lblStringComparatorValue
            // 
            this.lblStringComparatorValue.AutoSize = true;
            this.lblStringComparatorValue.Location = new System.Drawing.Point(12, 64);
            this.lblStringComparatorValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringComparatorValue.Name = "lblStringComparatorValue";
            this.lblStringComparatorValue.Size = new System.Drawing.Size(45, 16);
            this.lblStringComparatorValue.TabIndex = 63;
            this.lblStringComparatorValue.Text = "Value:";
            // 
            // txtStringValue
            // 
            this.txtStringValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtStringValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStringValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtStringValue.Location = new System.Drawing.Point(116, 62);
            this.txtStringValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(203, 22);
            this.txtStringValue.TabIndex = 62;
            // 
            // cmbStringComparitor
            // 
            this.cmbStringComparitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStringComparitor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStringComparitor.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStringComparitor.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStringComparitor.DrawDropdownHoverOutline = false;
            this.cmbStringComparitor.DrawFocusRectangle = false;
            this.cmbStringComparitor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStringComparitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStringComparitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStringComparitor.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStringComparitor.FormattingEnabled = true;
            this.cmbStringComparitor.Items.AddRange(new object[] {
            "Equal To",
            "Contains"});
            this.cmbStringComparitor.Location = new System.Drawing.Point(116, 25);
            this.cmbStringComparitor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStringComparitor.Name = "cmbStringComparitor";
            this.cmbStringComparitor.Size = new System.Drawing.Size(203, 23);
            this.cmbStringComparitor.TabIndex = 3;
            this.cmbStringComparitor.Text = "Equal To";
            this.cmbStringComparitor.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblStringComparator
            // 
            this.lblStringComparator.AutoSize = true;
            this.lblStringComparator.Location = new System.Drawing.Point(12, 28);
            this.lblStringComparator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringComparator.Name = "lblStringComparator";
            this.lblStringComparator.Size = new System.Drawing.Size(81, 16);
            this.lblStringComparator.TabIndex = 2;
            this.lblStringComparator.Text = "Comparator:";
            // 
            // grpBooleanVariable
            // 
            this.grpBooleanVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpBooleanVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanUserVariable);
            this.grpBooleanVariable.Controls.Add(this.optBooleanUserVariable);
            this.grpBooleanVariable.Controls.Add(this.optBooleanTrue);
            this.grpBooleanVariable.Controls.Add(this.optBooleanFalse);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanComparator);
            this.grpBooleanVariable.Controls.Add(this.lblBooleanComparator);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanGuildVariable);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanPlayerVariable);
            this.grpBooleanVariable.Controls.Add(this.optBooleanPlayerVariable);
            this.grpBooleanVariable.Controls.Add(this.optBooleanGuildVariable);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanGlobalVariable);
            this.grpBooleanVariable.Controls.Add(this.optBooleanGlobalVariable);
            this.grpBooleanVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBooleanVariable.Location = new System.Drawing.Point(11, 171);
            this.grpBooleanVariable.Margin = new System.Windows.Forms.Padding(4);
            this.grpBooleanVariable.Name = "grpBooleanVariable";
            this.grpBooleanVariable.Padding = new System.Windows.Forms.Padding(4);
            this.grpBooleanVariable.Size = new System.Drawing.Size(329, 224);
            this.grpBooleanVariable.TabIndex = 52;
            this.grpBooleanVariable.TabStop = false;
            this.grpBooleanVariable.Text = "Boolean Variable:";
            // 
            // cmbBooleanUserVariable
            // 
            this.cmbBooleanUserVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanUserVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanUserVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanUserVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanUserVariable.DrawDropdownHoverOutline = false;
            this.cmbBooleanUserVariable.DrawFocusRectangle = false;
            this.cmbBooleanUserVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanUserVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanUserVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanUserVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanUserVariable.FormattingEnabled = true;
            this.cmbBooleanUserVariable.Location = new System.Drawing.Point(195, 186);
            this.cmbBooleanUserVariable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBooleanUserVariable.Name = "cmbBooleanUserVariable";
            this.cmbBooleanUserVariable.Size = new System.Drawing.Size(124, 23);
            this.cmbBooleanUserVariable.TabIndex = 52;
            this.cmbBooleanUserVariable.Text = null;
            this.cmbBooleanUserVariable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optBooleanUserVariable
            // 
            this.optBooleanUserVariable.AutoSize = true;
            this.optBooleanUserVariable.Location = new System.Drawing.Point(13, 187);
            this.optBooleanUserVariable.Margin = new System.Windows.Forms.Padding(4);
            this.optBooleanUserVariable.Name = "optBooleanUserVariable";
            this.optBooleanUserVariable.Size = new System.Drawing.Size(171, 20);
            this.optBooleanUserVariable.TabIndex = 51;
            this.optBooleanUserVariable.Text = "Account Variable Value:";
            // 
            // optBooleanTrue
            // 
            this.optBooleanTrue.AutoSize = true;
            this.optBooleanTrue.Location = new System.Drawing.Point(13, 59);
            this.optBooleanTrue.Margin = new System.Windows.Forms.Padding(4);
            this.optBooleanTrue.Name = "optBooleanTrue";
            this.optBooleanTrue.Size = new System.Drawing.Size(56, 20);
            this.optBooleanTrue.TabIndex = 50;
            this.optBooleanTrue.Text = "True";
            // 
            // optBooleanFalse
            // 
            this.optBooleanFalse.AutoSize = true;
            this.optBooleanFalse.Location = new System.Drawing.Point(96, 59);
            this.optBooleanFalse.Margin = new System.Windows.Forms.Padding(4);
            this.optBooleanFalse.Name = "optBooleanFalse";
            this.optBooleanFalse.Size = new System.Drawing.Size(62, 20);
            this.optBooleanFalse.TabIndex = 49;
            this.optBooleanFalse.Text = "False";
            // 
            // cmbBooleanComparator
            // 
            this.cmbBooleanComparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanComparator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanComparator.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanComparator.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanComparator.DrawDropdownHoverOutline = false;
            this.cmbBooleanComparator.DrawFocusRectangle = false;
            this.cmbBooleanComparator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanComparator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanComparator.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanComparator.FormattingEnabled = true;
            this.cmbBooleanComparator.Items.AddRange(new object[] {
            "Equal To",
            "Not Equal To"});
            this.cmbBooleanComparator.Location = new System.Drawing.Point(153, 25);
            this.cmbBooleanComparator.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBooleanComparator.Name = "cmbBooleanComparator";
            this.cmbBooleanComparator.Size = new System.Drawing.Size(165, 23);
            this.cmbBooleanComparator.TabIndex = 3;
            this.cmbBooleanComparator.Text = "Equal To";
            this.cmbBooleanComparator.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblBooleanComparator
            // 
            this.lblBooleanComparator.AutoSize = true;
            this.lblBooleanComparator.Location = new System.Drawing.Point(12, 28);
            this.lblBooleanComparator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBooleanComparator.Name = "lblBooleanComparator";
            this.lblBooleanComparator.Size = new System.Drawing.Size(78, 16);
            this.lblBooleanComparator.TabIndex = 2;
            this.lblBooleanComparator.Text = "Comparator";
            // 
            // cmbBooleanGuildVariable
            // 
            this.cmbBooleanGuildVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanGuildVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanGuildVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanGuildVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanGuildVariable.DrawDropdownHoverOutline = false;
            this.cmbBooleanGuildVariable.DrawFocusRectangle = false;
            this.cmbBooleanGuildVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanGuildVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanGuildVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanGuildVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanGuildVariable.FormattingEnabled = true;
            this.cmbBooleanGuildVariable.Location = new System.Drawing.Point(195, 153);
            this.cmbBooleanGuildVariable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBooleanGuildVariable.Name = "cmbBooleanGuildVariable";
            this.cmbBooleanGuildVariable.Size = new System.Drawing.Size(124, 23);
            this.cmbBooleanGuildVariable.TabIndex = 48;
            this.cmbBooleanGuildVariable.Text = null;
            this.cmbBooleanGuildVariable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbBooleanPlayerVariable
            // 
            this.cmbBooleanPlayerVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanPlayerVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanPlayerVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanPlayerVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanPlayerVariable.DrawDropdownHoverOutline = false;
            this.cmbBooleanPlayerVariable.DrawFocusRectangle = false;
            this.cmbBooleanPlayerVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanPlayerVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanPlayerVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanPlayerVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanPlayerVariable.FormattingEnabled = true;
            this.cmbBooleanPlayerVariable.Location = new System.Drawing.Point(195, 86);
            this.cmbBooleanPlayerVariable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBooleanPlayerVariable.Name = "cmbBooleanPlayerVariable";
            this.cmbBooleanPlayerVariable.Size = new System.Drawing.Size(124, 23);
            this.cmbBooleanPlayerVariable.TabIndex = 47;
            this.cmbBooleanPlayerVariable.Text = null;
            this.cmbBooleanPlayerVariable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optBooleanPlayerVariable
            // 
            this.optBooleanPlayerVariable.AutoSize = true;
            this.optBooleanPlayerVariable.Location = new System.Drawing.Point(13, 87);
            this.optBooleanPlayerVariable.Margin = new System.Windows.Forms.Padding(4);
            this.optBooleanPlayerVariable.Name = "optBooleanPlayerVariable";
            this.optBooleanPlayerVariable.Size = new System.Drawing.Size(162, 20);
            this.optBooleanPlayerVariable.TabIndex = 45;
            this.optBooleanPlayerVariable.Text = "Player Variable Value:";
            // 
            // optBooleanGuildVariable
            // 
            this.optBooleanGuildVariable.AutoSize = true;
            this.optBooleanGuildVariable.Location = new System.Drawing.Point(13, 154);
            this.optBooleanGuildVariable.Margin = new System.Windows.Forms.Padding(4);
            this.optBooleanGuildVariable.Name = "optBooleanGuildVariable";
            this.optBooleanGuildVariable.Size = new System.Drawing.Size(154, 20);
            this.optBooleanGuildVariable.TabIndex = 46;
            this.optBooleanGuildVariable.Text = "Guild Variable Value:";
            // 
            // cmbBooleanGlobalVariable
            // 
            this.cmbBooleanGlobalVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanGlobalVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanGlobalVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanGlobalVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanGlobalVariable.DrawDropdownHoverOutline = false;
            this.cmbBooleanGlobalVariable.DrawFocusRectangle = false;
            this.cmbBooleanGlobalVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanGlobalVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanGlobalVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanGlobalVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanGlobalVariable.FormattingEnabled = true;
            this.cmbBooleanGlobalVariable.Location = new System.Drawing.Point(195, 119);
            this.cmbBooleanGlobalVariable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBooleanGlobalVariable.Name = "cmbBooleanGlobalVariable";
            this.cmbBooleanGlobalVariable.Size = new System.Drawing.Size(124, 23);
            this.cmbBooleanGlobalVariable.TabIndex = 48;
            this.cmbBooleanGlobalVariable.Text = null;
            this.cmbBooleanGlobalVariable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optBooleanGlobalVariable
            // 
            this.optBooleanGlobalVariable.AutoSize = true;
            this.optBooleanGlobalVariable.Location = new System.Drawing.Point(13, 121);
            this.optBooleanGlobalVariable.Margin = new System.Windows.Forms.Padding(4);
            this.optBooleanGlobalVariable.Name = "optBooleanGlobalVariable";
            this.optBooleanGlobalVariable.Size = new System.Drawing.Size(163, 20);
            this.optBooleanGlobalVariable.TabIndex = 46;
            this.optBooleanGlobalVariable.Text = "Global Variable Value:";
            // 
            // grpMapZoneType
            // 
            this.grpMapZoneType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpMapZoneType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpMapZoneType.Controls.Add(this.lblMapZoneType);
            this.grpMapZoneType.Controls.Add(this.cmbMapZoneType);
            this.grpMapZoneType.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpMapZoneType.Location = new System.Drawing.Point(11, 48);
            this.grpMapZoneType.Margin = new System.Windows.Forms.Padding(4);
            this.grpMapZoneType.Name = "grpMapZoneType";
            this.grpMapZoneType.Padding = new System.Windows.Forms.Padding(4);
            this.grpMapZoneType.Size = new System.Drawing.Size(349, 87);
            this.grpMapZoneType.TabIndex = 58;
            this.grpMapZoneType.TabStop = false;
            this.grpMapZoneType.Text = "Map Zone Type Is:";
            this.grpMapZoneType.Visible = false;
            // 
            // lblMapZoneType
            // 
            this.lblMapZoneType.AutoSize = true;
            this.lblMapZoneType.Location = new System.Drawing.Point(8, 26);
            this.lblMapZoneType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMapZoneType.Name = "lblMapZoneType";
            this.lblMapZoneType.Size = new System.Drawing.Size(106, 16);
            this.lblMapZoneType.TabIndex = 5;
            this.lblMapZoneType.Text = "Map Zone Type:";
            // 
            // cmbMapZoneType
            // 
            this.cmbMapZoneType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMapZoneType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMapZoneType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMapZoneType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMapZoneType.DrawDropdownHoverOutline = false;
            this.cmbMapZoneType.DrawFocusRectangle = false;
            this.cmbMapZoneType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMapZoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapZoneType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMapZoneType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMapZoneType.FormattingEnabled = true;
            this.cmbMapZoneType.Location = new System.Drawing.Point(123, 22);
            this.cmbMapZoneType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMapZoneType.Name = "cmbMapZoneType";
            this.cmbMapZoneType.Size = new System.Drawing.Size(215, 23);
            this.cmbMapZoneType.TabIndex = 3;
            this.cmbMapZoneType.Text = null;
            this.cmbMapZoneType.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpInGuild
            // 
            this.grpInGuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpInGuild.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpInGuild.Controls.Add(this.lblRank);
            this.grpInGuild.Controls.Add(this.cmbRank);
            this.grpInGuild.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInGuild.Location = new System.Drawing.Point(12, 49);
            this.grpInGuild.Margin = new System.Windows.Forms.Padding(4);
            this.grpInGuild.Name = "grpInGuild";
            this.grpInGuild.Padding = new System.Windows.Forms.Padding(4);
            this.grpInGuild.Size = new System.Drawing.Size(349, 87);
            this.grpInGuild.TabIndex = 33;
            this.grpInGuild.TabStop = false;
            this.grpInGuild.Text = "In Guild With At Least Rank:";
            this.grpInGuild.Visible = false;
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Location = new System.Drawing.Point(8, 26);
            this.lblRank.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(42, 16);
            this.lblRank.TabIndex = 5;
            this.lblRank.Text = "Rank:";
            // 
            // cmbRank
            // 
            this.cmbRank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbRank.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbRank.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbRank.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbRank.DrawDropdownHoverOutline = false;
            this.cmbRank.DrawFocusRectangle = false;
            this.cmbRank.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRank.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbRank.FormattingEnabled = true;
            this.cmbRank.Location = new System.Drawing.Point(123, 22);
            this.cmbRank.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRank.Name = "cmbRank";
            this.cmbRank.Size = new System.Drawing.Size(215, 23);
            this.cmbRank.TabIndex = 3;
            this.cmbRank.Text = null;
            this.cmbRank.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpQuestCompleted
            // 
            this.grpQuestCompleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpQuestCompleted.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestCompleted.Controls.Add(this.lblQuestCompleted);
            this.grpQuestCompleted.Controls.Add(this.cmbCompletedQuest);
            this.grpQuestCompleted.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestCompleted.Location = new System.Drawing.Point(12, 49);
            this.grpQuestCompleted.Margin = new System.Windows.Forms.Padding(4);
            this.grpQuestCompleted.Name = "grpQuestCompleted";
            this.grpQuestCompleted.Padding = new System.Windows.Forms.Padding(4);
            this.grpQuestCompleted.Size = new System.Drawing.Size(349, 87);
            this.grpQuestCompleted.TabIndex = 32;
            this.grpQuestCompleted.TabStop = false;
            this.grpQuestCompleted.Text = "Quest Completed:";
            this.grpQuestCompleted.Visible = false;
            // 
            // lblQuestCompleted
            // 
            this.lblQuestCompleted.AutoSize = true;
            this.lblQuestCompleted.Location = new System.Drawing.Point(8, 26);
            this.lblQuestCompleted.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestCompleted.Name = "lblQuestCompleted";
            this.lblQuestCompleted.Size = new System.Drawing.Size(45, 16);
            this.lblQuestCompleted.TabIndex = 5;
            this.lblQuestCompleted.Text = "Quest:";
            // 
            // cmbCompletedQuest
            // 
            this.cmbCompletedQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCompletedQuest.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCompletedQuest.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCompletedQuest.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCompletedQuest.DrawDropdownHoverOutline = false;
            this.cmbCompletedQuest.DrawFocusRectangle = false;
            this.cmbCompletedQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompletedQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompletedQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompletedQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCompletedQuest.FormattingEnabled = true;
            this.cmbCompletedQuest.Location = new System.Drawing.Point(123, 22);
            this.cmbCompletedQuest.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompletedQuest.Name = "cmbCompletedQuest";
            this.cmbCompletedQuest.Size = new System.Drawing.Size(215, 23);
            this.cmbCompletedQuest.TabIndex = 3;
            this.cmbCompletedQuest.Text = null;
            this.cmbCompletedQuest.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpQuestInProgress
            // 
            this.grpQuestInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpQuestInProgress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestInProgress.Controls.Add(this.lblQuestTask);
            this.grpQuestInProgress.Controls.Add(this.cmbQuestTask);
            this.grpQuestInProgress.Controls.Add(this.cmbTaskModifier);
            this.grpQuestInProgress.Controls.Add(this.lblQuestIs);
            this.grpQuestInProgress.Controls.Add(this.lblQuestProgress);
            this.grpQuestInProgress.Controls.Add(this.cmbQuestInProgress);
            this.grpQuestInProgress.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestInProgress.Location = new System.Drawing.Point(12, 49);
            this.grpQuestInProgress.Margin = new System.Windows.Forms.Padding(4);
            this.grpQuestInProgress.Name = "grpQuestInProgress";
            this.grpQuestInProgress.Padding = new System.Windows.Forms.Padding(4);
            this.grpQuestInProgress.Size = new System.Drawing.Size(351, 150);
            this.grpQuestInProgress.TabIndex = 32;
            this.grpQuestInProgress.TabStop = false;
            this.grpQuestInProgress.Text = "Quest In Progress:";
            this.grpQuestInProgress.Visible = false;
            // 
            // lblQuestTask
            // 
            this.lblQuestTask.AutoSize = true;
            this.lblQuestTask.Location = new System.Drawing.Point(8, 106);
            this.lblQuestTask.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestTask.Name = "lblQuestTask";
            this.lblQuestTask.Size = new System.Drawing.Size(41, 16);
            this.lblQuestTask.TabIndex = 9;
            this.lblQuestTask.Text = "Task:";
            // 
            // cmbQuestTask
            // 
            this.cmbQuestTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuestTask.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuestTask.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuestTask.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuestTask.DrawDropdownHoverOutline = false;
            this.cmbQuestTask.DrawFocusRectangle = false;
            this.cmbQuestTask.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestTask.Enabled = false;
            this.cmbQuestTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestTask.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuestTask.FormattingEnabled = true;
            this.cmbQuestTask.Location = new System.Drawing.Point(123, 102);
            this.cmbQuestTask.Margin = new System.Windows.Forms.Padding(4);
            this.cmbQuestTask.Name = "cmbQuestTask";
            this.cmbQuestTask.Size = new System.Drawing.Size(216, 23);
            this.cmbQuestTask.TabIndex = 8;
            this.cmbQuestTask.Text = null;
            this.cmbQuestTask.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbTaskModifier
            // 
            this.cmbTaskModifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTaskModifier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTaskModifier.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTaskModifier.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTaskModifier.DrawDropdownHoverOutline = false;
            this.cmbTaskModifier.DrawFocusRectangle = false;
            this.cmbTaskModifier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTaskModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTaskModifier.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTaskModifier.FormattingEnabled = true;
            this.cmbTaskModifier.Location = new System.Drawing.Point(123, 62);
            this.cmbTaskModifier.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTaskModifier.Name = "cmbTaskModifier";
            this.cmbTaskModifier.Size = new System.Drawing.Size(216, 23);
            this.cmbTaskModifier.TabIndex = 7;
            this.cmbTaskModifier.Text = null;
            this.cmbTaskModifier.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTaskModifier.SelectedIndexChanged += new System.EventHandler(this.cmbTaskModifier_SelectedIndexChanged);
            // 
            // lblQuestIs
            // 
            this.lblQuestIs.AutoSize = true;
            this.lblQuestIs.Location = new System.Drawing.Point(8, 64);
            this.lblQuestIs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestIs.Name = "lblQuestIs";
            this.lblQuestIs.Size = new System.Drawing.Size(20, 16);
            this.lblQuestIs.TabIndex = 6;
            this.lblQuestIs.Text = "Is:";
            // 
            // lblQuestProgress
            // 
            this.lblQuestProgress.AutoSize = true;
            this.lblQuestProgress.Location = new System.Drawing.Point(8, 26);
            this.lblQuestProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestProgress.Name = "lblQuestProgress";
            this.lblQuestProgress.Size = new System.Drawing.Size(45, 16);
            this.lblQuestProgress.TabIndex = 5;
            this.lblQuestProgress.Text = "Quest:";
            // 
            // cmbQuestInProgress
            // 
            this.cmbQuestInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuestInProgress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuestInProgress.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuestInProgress.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuestInProgress.DrawDropdownHoverOutline = false;
            this.cmbQuestInProgress.DrawFocusRectangle = false;
            this.cmbQuestInProgress.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestInProgress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestInProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestInProgress.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuestInProgress.FormattingEnabled = true;
            this.cmbQuestInProgress.Location = new System.Drawing.Point(123, 22);
            this.cmbQuestInProgress.Margin = new System.Windows.Forms.Padding(4);
            this.cmbQuestInProgress.Name = "cmbQuestInProgress";
            this.cmbQuestInProgress.Size = new System.Drawing.Size(216, 23);
            this.cmbQuestInProgress.TabIndex = 3;
            this.cmbQuestInProgress.Text = null;
            this.cmbQuestInProgress.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbQuestInProgress.SelectedIndexChanged += new System.EventHandler(this.cmbQuestInProgress_SelectedIndexChanged);
            // 
            // grpStartQuest
            // 
            this.grpStartQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStartQuest.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStartQuest.Controls.Add(this.lblStartQuest);
            this.grpStartQuest.Controls.Add(this.cmbStartQuest);
            this.grpStartQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStartQuest.Location = new System.Drawing.Point(12, 49);
            this.grpStartQuest.Margin = new System.Windows.Forms.Padding(4);
            this.grpStartQuest.Name = "grpStartQuest";
            this.grpStartQuest.Padding = new System.Windows.Forms.Padding(4);
            this.grpStartQuest.Size = new System.Drawing.Size(349, 87);
            this.grpStartQuest.TabIndex = 31;
            this.grpStartQuest.TabStop = false;
            this.grpStartQuest.Text = "Can Start Quest:";
            this.grpStartQuest.Visible = false;
            // 
            // lblStartQuest
            // 
            this.lblStartQuest.AutoSize = true;
            this.lblStartQuest.Location = new System.Drawing.Point(8, 26);
            this.lblStartQuest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartQuest.Name = "lblStartQuest";
            this.lblStartQuest.Size = new System.Drawing.Size(45, 16);
            this.lblStartQuest.TabIndex = 5;
            this.lblStartQuest.Text = "Quest:";
            // 
            // cmbStartQuest
            // 
            this.cmbStartQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStartQuest.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStartQuest.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStartQuest.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStartQuest.DrawDropdownHoverOutline = false;
            this.cmbStartQuest.DrawFocusRectangle = false;
            this.cmbStartQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStartQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStartQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStartQuest.FormattingEnabled = true;
            this.cmbStartQuest.Location = new System.Drawing.Point(123, 22);
            this.cmbStartQuest.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStartQuest.Name = "cmbStartQuest";
            this.cmbStartQuest.Size = new System.Drawing.Size(215, 23);
            this.cmbStartQuest.TabIndex = 3;
            this.cmbStartQuest.Text = null;
            this.cmbStartQuest.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpTime
            // 
            this.grpTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTime.Controls.Add(this.lblEndRange);
            this.grpTime.Controls.Add(this.lblStartRange);
            this.grpTime.Controls.Add(this.cmbTime2);
            this.grpTime.Controls.Add(this.cmbTime1);
            this.grpTime.Controls.Add(this.lblAnd);
            this.grpTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTime.Location = new System.Drawing.Point(12, 49);
            this.grpTime.Margin = new System.Windows.Forms.Padding(4);
            this.grpTime.Name = "grpTime";
            this.grpTime.Padding = new System.Windows.Forms.Padding(4);
            this.grpTime.Size = new System.Drawing.Size(348, 129);
            this.grpTime.TabIndex = 30;
            this.grpTime.TabStop = false;
            this.grpTime.Text = "Time is between:";
            this.grpTime.Visible = false;
            // 
            // lblEndRange
            // 
            this.lblEndRange.AutoSize = true;
            this.lblEndRange.Location = new System.Drawing.Point(12, 90);
            this.lblEndRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndRange.Name = "lblEndRange";
            this.lblEndRange.Size = new System.Drawing.Size(78, 16);
            this.lblEndRange.TabIndex = 6;
            this.lblEndRange.Text = "End Range:";
            // 
            // lblStartRange
            // 
            this.lblStartRange.AutoSize = true;
            this.lblStartRange.Location = new System.Drawing.Point(8, 26);
            this.lblStartRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartRange.Name = "lblStartRange";
            this.lblStartRange.Size = new System.Drawing.Size(81, 16);
            this.lblStartRange.TabIndex = 5;
            this.lblStartRange.Text = "Start Range:";
            // 
            // cmbTime2
            // 
            this.cmbTime2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTime2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTime2.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTime2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTime2.DrawDropdownHoverOutline = false;
            this.cmbTime2.DrawFocusRectangle = false;
            this.cmbTime2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTime2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTime2.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTime2.FormattingEnabled = true;
            this.cmbTime2.Location = new System.Drawing.Point(123, 86);
            this.cmbTime2.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTime2.Name = "cmbTime2";
            this.cmbTime2.Size = new System.Drawing.Size(213, 23);
            this.cmbTime2.TabIndex = 4;
            this.cmbTime2.Text = null;
            this.cmbTime2.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbTime1
            // 
            this.cmbTime1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTime1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTime1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTime1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTime1.DrawDropdownHoverOutline = false;
            this.cmbTime1.DrawFocusRectangle = false;
            this.cmbTime1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTime1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTime1.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTime1.FormattingEnabled = true;
            this.cmbTime1.Location = new System.Drawing.Point(123, 22);
            this.cmbTime1.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTime1.Name = "cmbTime1";
            this.cmbTime1.Size = new System.Drawing.Size(213, 23);
            this.cmbTime1.TabIndex = 3;
            this.cmbTime1.Text = null;
            this.cmbTime1.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblAnd
            // 
            this.lblAnd.AutoSize = true;
            this.lblAnd.Location = new System.Drawing.Point(133, 60);
            this.lblAnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(31, 16);
            this.lblAnd.TabIndex = 2;
            this.lblAnd.Text = "And";
            // 
            // grpPowerIs
            // 
            this.grpPowerIs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpPowerIs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPowerIs.Controls.Add(this.cmbPower);
            this.grpPowerIs.Controls.Add(this.lblPower);
            this.grpPowerIs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPowerIs.Location = new System.Drawing.Point(12, 49);
            this.grpPowerIs.Margin = new System.Windows.Forms.Padding(4);
            this.grpPowerIs.Name = "grpPowerIs";
            this.grpPowerIs.Padding = new System.Windows.Forms.Padding(4);
            this.grpPowerIs.Size = new System.Drawing.Size(349, 63);
            this.grpPowerIs.TabIndex = 25;
            this.grpPowerIs.TabStop = false;
            this.grpPowerIs.Text = "Power Is...";
            // 
            // cmbPower
            // 
            this.cmbPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbPower.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPower.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbPower.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbPower.DrawDropdownHoverOutline = false;
            this.cmbPower.DrawFocusRectangle = false;
            this.cmbPower.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPower.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbPower.FormattingEnabled = true;
            this.cmbPower.Location = new System.Drawing.Point(105, 21);
            this.cmbPower.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPower.Name = "cmbPower";
            this.cmbPower.Size = new System.Drawing.Size(232, 23);
            this.cmbPower.TabIndex = 1;
            this.cmbPower.Text = null;
            this.cmbPower.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(9, 25);
            this.lblPower.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(48, 16);
            this.lblPower.TabIndex = 0;
            this.lblPower.Text = "Power:";
            // 
            // grpSelfSwitch
            // 
            this.grpSelfSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSelfSwitch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSelfSwitch.Controls.Add(this.cmbSelfSwitchVal);
            this.grpSelfSwitch.Controls.Add(this.lblSelfSwitchIs);
            this.grpSelfSwitch.Controls.Add(this.cmbSelfSwitch);
            this.grpSelfSwitch.Controls.Add(this.lblSelfSwitch);
            this.grpSelfSwitch.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSelfSwitch.Location = new System.Drawing.Point(12, 49);
            this.grpSelfSwitch.Margin = new System.Windows.Forms.Padding(4);
            this.grpSelfSwitch.Name = "grpSelfSwitch";
            this.grpSelfSwitch.Padding = new System.Windows.Forms.Padding(4);
            this.grpSelfSwitch.Size = new System.Drawing.Size(349, 110);
            this.grpSelfSwitch.TabIndex = 29;
            this.grpSelfSwitch.TabStop = false;
            this.grpSelfSwitch.Text = "Self Switch";
            // 
            // cmbSelfSwitchVal
            // 
            this.cmbSelfSwitchVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSelfSwitchVal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSelfSwitchVal.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSelfSwitchVal.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSelfSwitchVal.DrawDropdownHoverOutline = false;
            this.cmbSelfSwitchVal.DrawFocusRectangle = false;
            this.cmbSelfSwitchVal.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSelfSwitchVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelfSwitchVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSelfSwitchVal.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSelfSwitchVal.FormattingEnabled = true;
            this.cmbSelfSwitchVal.Location = new System.Drawing.Point(105, 64);
            this.cmbSelfSwitchVal.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSelfSwitchVal.Name = "cmbSelfSwitchVal";
            this.cmbSelfSwitchVal.Size = new System.Drawing.Size(235, 23);
            this.cmbSelfSwitchVal.TabIndex = 3;
            this.cmbSelfSwitchVal.Text = null;
            this.cmbSelfSwitchVal.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblSelfSwitchIs
            // 
            this.lblSelfSwitchIs.AutoSize = true;
            this.lblSelfSwitchIs.Location = new System.Drawing.Point(13, 68);
            this.lblSelfSwitchIs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelfSwitchIs.Name = "lblSelfSwitchIs";
            this.lblSelfSwitchIs.Size = new System.Drawing.Size(23, 16);
            this.lblSelfSwitchIs.TabIndex = 2;
            this.lblSelfSwitchIs.Text = "Is: ";
            // 
            // cmbSelfSwitch
            // 
            this.cmbSelfSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSelfSwitch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSelfSwitch.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSelfSwitch.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSelfSwitch.DrawDropdownHoverOutline = false;
            this.cmbSelfSwitch.DrawFocusRectangle = false;
            this.cmbSelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSelfSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelfSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSelfSwitch.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSelfSwitch.FormattingEnabled = true;
            this.cmbSelfSwitch.Location = new System.Drawing.Point(105, 21);
            this.cmbSelfSwitch.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSelfSwitch.Name = "cmbSelfSwitch";
            this.cmbSelfSwitch.Size = new System.Drawing.Size(235, 23);
            this.cmbSelfSwitch.TabIndex = 1;
            this.cmbSelfSwitch.Text = null;
            this.cmbSelfSwitch.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblSelfSwitch
            // 
            this.lblSelfSwitch.AutoSize = true;
            this.lblSelfSwitch.Location = new System.Drawing.Point(9, 25);
            this.lblSelfSwitch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelfSwitch.Name = "lblSelfSwitch";
            this.lblSelfSwitch.Size = new System.Drawing.Size(77, 16);
            this.lblSelfSwitch.TabIndex = 0;
            this.lblSelfSwitch.Text = "Self Switch: ";
            // 
            // grpSpell
            // 
            this.grpSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpell.Controls.Add(this.cmbSpell);
            this.grpSpell.Controls.Add(this.lblSpell);
            this.grpSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpell.Location = new System.Drawing.Point(12, 49);
            this.grpSpell.Margin = new System.Windows.Forms.Padding(4);
            this.grpSpell.Name = "grpSpell";
            this.grpSpell.Padding = new System.Windows.Forms.Padding(4);
            this.grpSpell.Size = new System.Drawing.Size(349, 64);
            this.grpSpell.TabIndex = 26;
            this.grpSpell.TabStop = false;
            this.grpSpell.Text = "Knows Spell";
            // 
            // cmbSpell
            // 
            this.cmbSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpell.DrawDropdownHoverOutline = false;
            this.cmbSpell.DrawFocusRectangle = false;
            this.cmbSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpell.FormattingEnabled = true;
            this.cmbSpell.Location = new System.Drawing.Point(105, 22);
            this.cmbSpell.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpell.Name = "cmbSpell";
            this.cmbSpell.Size = new System.Drawing.Size(232, 23);
            this.cmbSpell.TabIndex = 3;
            this.cmbSpell.Text = null;
            this.cmbSpell.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(9, 25);
            this.lblSpell.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(41, 16);
            this.lblSpell.TabIndex = 2;
            this.lblSpell.Text = "Spell:";
            // 
            // grpClass
            // 
            this.grpClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpClass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpClass.Controls.Add(this.cmbClass);
            this.grpClass.Controls.Add(this.lblClass);
            this.grpClass.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpClass.Location = new System.Drawing.Point(12, 49);
            this.grpClass.Margin = new System.Windows.Forms.Padding(4);
            this.grpClass.Name = "grpClass";
            this.grpClass.Padding = new System.Windows.Forms.Padding(4);
            this.grpClass.Size = new System.Drawing.Size(349, 64);
            this.grpClass.TabIndex = 27;
            this.grpClass.TabStop = false;
            this.grpClass.Text = "Class is";
            // 
            // cmbClass
            // 
            this.cmbClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbClass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbClass.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbClass.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbClass.DrawDropdownHoverOutline = false;
            this.cmbClass.DrawFocusRectangle = false;
            this.cmbClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClass.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(105, 22);
            this.cmbClass.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(232, 23);
            this.cmbClass.TabIndex = 3;
            this.cmbClass.Text = null;
            this.cmbClass.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(9, 25);
            this.lblClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(44, 16);
            this.lblClass.TabIndex = 2;
            this.lblClass.Text = "Class:";
            // 
            // grpJobLevel
            // 
            this.grpJobLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpJobLevel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpJobLevel.Controls.Add(this.cmbJobLevel);
            this.grpJobLevel.Controls.Add(this.nudJobValue);
            this.grpJobLevel.Controls.Add(this.label1);
            this.grpJobLevel.Controls.Add(this.label2);
            this.grpJobLevel.Controls.Add(this.cmbJobComparator);
            this.grpJobLevel.Controls.Add(this.label3);
            this.grpJobLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpJobLevel.Location = new System.Drawing.Point(11, 203);

            this.grpJobLevel.Margin = new System.Windows.Forms.Padding(4);
            this.grpJobLevel.Name = "grpJobLevel";
            this.grpJobLevel.Padding = new System.Windows.Forms.Padding(4);
            this.grpJobLevel.Size = new System.Drawing.Size(349, 172);
            this.grpJobLevel.TabIndex = 59;
            this.grpJobLevel.TabStop = false;
            this.grpJobLevel.Text = "Job Level is";
            // 
            // cmbJobLevel
            // 
            this.cmbJobLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbJobLevel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbJobLevel.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbJobLevel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbJobLevel.DrawDropdownHoverOutline = false;
            this.cmbJobLevel.DrawFocusRectangle = false;
            this.cmbJobLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbJobLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJobLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbJobLevel.FormattingEnabled = true;
            this.cmbJobLevel.Location = new System.Drawing.Point(105, 28);
            this.cmbJobLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbJobLevel.Name = "cmbJobLevel";
            this.cmbJobLevel.Size = new System.Drawing.Size(235, 23);
            this.cmbJobLevel.TabIndex = 7;
            this.cmbJobLevel.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // nudJobValue
            // 
            this.nudJobValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudJobValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudJobValue.Location = new System.Drawing.Point(105, 107);
            this.nudJobValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudJobValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudJobValue.Name = "nudJobValue";
            this.nudJobValue.Size = new System.Drawing.Size(237, 22);
            this.nudJobValue.TabIndex = 8;
            this.nudJobValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Job:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value:";
            // 
            // cmbJobComparator
            // 
            this.cmbJobComparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbJobComparator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbJobComparator.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbJobComparator.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbJobComparator.DrawDropdownHoverOutline = false;
            this.cmbJobComparator.DrawFocusRectangle = false;
            this.cmbJobComparator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbJobComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobComparator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJobComparator.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbJobComparator.FormattingEnabled = true;
            this.cmbJobComparator.Location = new System.Drawing.Point(105, 65);
            this.cmbJobComparator.Margin = new System.Windows.Forms.Padding(4);
            this.cmbJobComparator.Name = "cmbJobComparator";
            this.cmbJobComparator.Size = new System.Drawing.Size(235, 23);
            this.cmbJobComparator.TabIndex = 3;
            this.cmbJobComparator.Text = null;
            this.cmbJobComparator.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Comparator:";
            // 
            // EventCommandConditionalBranch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpConditional);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EventCommandConditionalBranch";
            this.Size = new System.Drawing.Size(380, 590);
            this.grpConditional.ResumeLayout(false);
            this.grpConditional.PerformLayout();
            this.grpLevelStat.ResumeLayout(false);
            this.grpLevelStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelStatValue)).EndInit();
            this.grpMapIs.ResumeLayout(false);
            this.grpGender.ResumeLayout(false);
            this.grpGender.PerformLayout();
            this.grpEquippedItem.ResumeLayout(false);
            this.grpEquippedItem.PerformLayout();
            this.grpCheckEquippedSlot.ResumeLayout(false);
            this.grpCheckEquippedSlot.PerformLayout();
            this.grpNpc.ResumeLayout(false);
            this.grpNpc.PerformLayout();
            this.grpInventoryConditions.ResumeLayout(false);
            this.grpInventoryConditions.PerformLayout();
            this.grpVariableAmount.ResumeLayout(false);
            this.grpVariableAmount.PerformLayout();
            this.grpManualAmount.ResumeLayout(false);
            this.grpManualAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemAmount)).EndInit();
            this.grpAmountType.ResumeLayout(false);
            this.grpAmountType.PerformLayout();
            this.grpVariable.ResumeLayout(false);
            this.grpSelectVariable.ResumeLayout(false);
            this.grpSelectVariable.PerformLayout();
            this.grpNumericVariable.ResumeLayout(false);
            this.grpNumericVariable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariableValue)).EndInit();
            this.grpStringVariable.ResumeLayout(false);
            this.grpStringVariable.PerformLayout();
            this.grpBooleanVariable.ResumeLayout(false);
            this.grpBooleanVariable.PerformLayout();
            this.grpMapZoneType.ResumeLayout(false);
            this.grpMapZoneType.PerformLayout();
            this.grpInGuild.ResumeLayout(false);
            this.grpInGuild.PerformLayout();
            this.grpQuestCompleted.ResumeLayout(false);
            this.grpQuestCompleted.PerformLayout();
            this.grpQuestInProgress.ResumeLayout(false);
            this.grpQuestInProgress.PerformLayout();
            this.grpStartQuest.ResumeLayout(false);
            this.grpStartQuest.PerformLayout();
            this.grpTime.ResumeLayout(false);
            this.grpTime.PerformLayout();
            this.grpPowerIs.ResumeLayout(false);
            this.grpPowerIs.PerformLayout();
            this.grpSelfSwitch.ResumeLayout(false);
            this.grpSelfSwitch.PerformLayout();
            this.grpSpell.ResumeLayout(false);
            this.grpSpell.PerformLayout();
            this.grpClass.ResumeLayout(false);
            this.grpClass.PerformLayout();
            this.grpJobLevel.ResumeLayout(false);
            this.grpJobLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudJobValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpConditional;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkComboBox cmbConditionType;
        private System.Windows.Forms.Label lblType;
        private DarkGroupBox grpVariable;
        private DarkComboBox cmbNumericComparitor;
        private System.Windows.Forms.Label lblNumericComparator;
        private DarkGroupBox grpInventoryConditions;
        private DarkComboBox cmbItem;
        private System.Windows.Forms.Label lblItem;
        private DarkGroupBox grpSpell;
        private DarkComboBox cmbSpell;
        private System.Windows.Forms.Label lblSpell;
        private DarkGroupBox grpClass;
        private DarkComboBox cmbClass;
        private System.Windows.Forms.Label lblClass;
        private DarkGroupBox grpLevelStat;
        private System.Windows.Forms.Label lblLvlStatValue;
        private DarkComboBox cmbLevelComparator;
        private System.Windows.Forms.Label lblLevelComparator;
        private DarkGroupBox grpSelfSwitch;
        private DarkComboBox cmbSelfSwitchVal;
        private System.Windows.Forms.Label lblSelfSwitchIs;
        private DarkComboBox cmbSelfSwitch;
        private System.Windows.Forms.Label lblSelfSwitch;
        private DarkGroupBox grpPowerIs;
        private DarkComboBox cmbPower;
        private System.Windows.Forms.Label lblPower;
        private DarkGroupBox grpTime;
        private DarkComboBox cmbTime2;
        private DarkComboBox cmbTime1;
        private System.Windows.Forms.Label lblAnd;
        private System.Windows.Forms.Label lblEndRange;
        private System.Windows.Forms.Label lblStartRange;
        private DarkGroupBox grpQuestInProgress;
        private System.Windows.Forms.Label lblQuestTask;
        private DarkComboBox cmbQuestTask;
        private DarkComboBox cmbTaskModifier;
        private System.Windows.Forms.Label lblQuestIs;
        private System.Windows.Forms.Label lblQuestProgress;
        private DarkComboBox cmbQuestInProgress;
        private DarkGroupBox grpStartQuest;
        private System.Windows.Forms.Label lblStartQuest;
        private DarkComboBox cmbStartQuest;
        private DarkGroupBox grpQuestCompleted;
        private System.Windows.Forms.Label lblQuestCompleted;
        private DarkComboBox cmbCompletedQuest;
        private DarkComboBox cmbLevelStat;
        private System.Windows.Forms.Label lblLevelOrStat;
        private DarkGroupBox grpGender;
        private DarkComboBox cmbGender;
        private System.Windows.Forms.Label lblGender;
        private DarkNumericUpDown nudLevelStatValue;
        private DarkCheckBox chkStatIgnoreBuffs;
        private DarkCheckBox chkNegated;
        private DarkGroupBox grpMapIs;
        private DarkButton btnSelectMap;
        internal DarkComboBox cmbCompareGlobalVar;
        internal DarkComboBox cmbCompareGuildVar;
        internal DarkComboBox cmbComparePlayerVar;
        internal DarkRadioButton rdoVarCompareGlobalVar;
        internal DarkRadioButton rdoVarCompareGuildVar;
        internal DarkRadioButton rdoVarComparePlayerVar;
        internal DarkRadioButton rdoVarCompareStaticValue;
        private DarkNumericUpDown nudVariableValue;
        private DarkGroupBox grpEquippedItem;
        private DarkComboBox cmbEquippedItem;
        private System.Windows.Forms.Label lblEquippedItem;
        private DarkGroupBox grpBooleanVariable;
        private DarkComboBox cmbBooleanComparator;
        private System.Windows.Forms.Label lblBooleanComparator;
        internal DarkComboBox cmbBooleanGlobalVariable;
        internal DarkComboBox cmbBooleanGuildVariable;
        internal DarkComboBox cmbBooleanPlayerVariable;
        internal DarkRadioButton optBooleanPlayerVariable;
        internal DarkRadioButton optBooleanGlobalVariable;
        internal DarkRadioButton optBooleanGuildVariable;
        private DarkGroupBox grpNumericVariable;
        private DarkGroupBox grpSelectVariable;
        private DarkRadioButton rdoPlayerVariable;
        internal DarkComboBox cmbVariable;
        private DarkRadioButton rdoGlobalVariable;
        private DarkRadioButton rdoGuildVariable;
        internal DarkRadioButton optBooleanTrue;
        internal DarkRadioButton optBooleanFalse;
        private DarkGroupBox grpStringVariable;
        private DarkComboBox cmbStringComparitor;
        private System.Windows.Forms.Label lblStringComparator;
        private DarkTextBox txtStringValue;
        private System.Windows.Forms.Label lblStringComparatorValue;
        private System.Windows.Forms.Label lblStringTextVariables;
        private DarkGroupBox grpAmountType;
        private DarkRadioButton rdoVariable;
        private DarkRadioButton rdoManual;
        private DarkGroupBox grpManualAmount;
        private DarkNumericUpDown nudItemAmount;
        private System.Windows.Forms.Label lblItemQuantity;
        private DarkGroupBox grpVariableAmount;
        private DarkComboBox cmbInvVariable;
        private System.Windows.Forms.Label lblInvVariable;
        private DarkRadioButton rdoInvGlobalVariable;
        private DarkRadioButton rdoInvGuildVariable;
        private DarkRadioButton rdoInvPlayerVariable;
        private DarkCheckBox chkHasElse;
        private DarkGroupBox grpInGuild;
        private System.Windows.Forms.Label lblRank;
        private DarkComboBox cmbRank;
        private DarkGroupBox grpMapZoneType;
        private System.Windows.Forms.Label lblMapZoneType;
        private DarkComboBox cmbMapZoneType;
        private DarkCheckBox chkBank;
        private DarkGroupBox grpCheckEquippedSlot;
        private DarkComboBox cmbCheckEquippedSlot;
        private System.Windows.Forms.Label lblCheckEquippedSlot;
        private DarkGroupBox grpNpc;
        private DarkCheckBox chkNpc;
        private DarkComboBox cmbNpcs;
        private System.Windows.Forms.Label lblNpc;
        private DarkRadioButton rdoUserVariable;
        internal DarkComboBox cmbCompareUserVar;
        internal DarkRadioButton rdoVarCompareUserVar;
        internal DarkComboBox cmbBooleanUserVariable;
        internal DarkRadioButton optBooleanUserVariable;
        internal DarkRadioButton rdoTimeSystem;
        private DarkGroupBox grpJobLevel;
        private DarkComboBox cmbJobLevel;
        private DarkNumericUpDown nudJobValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DarkComboBox cmbJobComparator;
        private System.Windows.Forms.Label label3;
    }
}
