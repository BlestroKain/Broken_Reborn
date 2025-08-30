using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmPrismOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private DarkNumericUpDown nudBaseHp;
        private DarkNumericUpDown nudHpPerLevel;
        private DarkNumericUpDown nudMaturationSeconds;
        private DarkNumericUpDown nudAttackCooldown;
        private DarkNumericUpDown nudDamageCapPerTick;
        private DarkNumericUpDown nudSchedulerIntervalSeconds;
        private DarkButton btnSave;
        private System.Windows.Forms.Label lblBaseHp;
        private System.Windows.Forms.Label lblHpPerLevel;
        private System.Windows.Forms.Label lblMaturation;
        private System.Windows.Forms.Label lblCooldown;
        private System.Windows.Forms.Label lblDamageCapPerTick;
        private System.Windows.Forms.Label lblSchedulerInterval;

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
            nudBaseHp = new DarkNumericUpDown();
            nudHpPerLevel = new DarkNumericUpDown();
            nudMaturationSeconds = new DarkNumericUpDown();
            nudAttackCooldown = new DarkNumericUpDown();
            nudDamageCapPerTick = new DarkNumericUpDown();
            nudSchedulerIntervalSeconds = new DarkNumericUpDown();
            btnSave = new DarkButton();
            lblBaseHp = new Label();
            lblHpPerLevel = new Label();
            lblMaturation = new Label();
            lblCooldown = new Label();
            lblDamageCapPerTick = new Label();
            lblSchedulerInterval = new Label();
            ((System.ComponentModel.ISupportInitialize)nudBaseHp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHpPerLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaturationSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAttackCooldown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamageCapPerTick).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSchedulerIntervalSeconds).BeginInit();
            SuspendLayout();
            // 
            // nudBaseHp
            // 
            nudBaseHp.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudBaseHp.ForeColor = System.Drawing.Color.Gainsboro;
            nudBaseHp.Location = new System.Drawing.Point(14, 14);
            nudBaseHp.Margin = new Padding(4, 3, 4, 3);
            nudBaseHp.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudBaseHp.Name = "nudBaseHp";
            nudBaseHp.Size = new Size(117, 23);
            nudBaseHp.TabIndex = 0;
            nudBaseHp.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudHpPerLevel
            // 
            nudHpPerLevel.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudHpPerLevel.ForeColor = System.Drawing.Color.Gainsboro;
            nudHpPerLevel.Location = new System.Drawing.Point(14, 48);
            nudHpPerLevel.Margin = new Padding(4, 3, 4, 3);
            nudHpPerLevel.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudHpPerLevel.Name = "nudHpPerLevel";
            nudHpPerLevel.Size = new Size(117, 23);
            nudHpPerLevel.TabIndex = 2;
            nudHpPerLevel.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudMaturationSeconds
            // 
            nudMaturationSeconds.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudMaturationSeconds.ForeColor = System.Drawing.Color.Gainsboro;
            nudMaturationSeconds.Location = new System.Drawing.Point(14, 83);
            nudMaturationSeconds.Margin = new Padding(4, 3, 4, 3);
            nudMaturationSeconds.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMaturationSeconds.Name = "nudMaturationSeconds";
            nudMaturationSeconds.Size = new Size(117, 23);
            nudMaturationSeconds.TabIndex = 4;
            nudMaturationSeconds.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // nudAttackCooldown
            // 
            nudAttackCooldown.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudAttackCooldown.ForeColor = System.Drawing.Color.Gainsboro;
            nudAttackCooldown.Location = new System.Drawing.Point(14, 118);
            nudAttackCooldown.Margin = new Padding(4, 3, 4, 3);
            nudAttackCooldown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudAttackCooldown.Name = "nudAttackCooldown";
            nudAttackCooldown.Size = new Size(117, 23);
            nudAttackCooldown.TabIndex = 6;
            nudAttackCooldown.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // nudDamageCapPerTick
            //
            nudDamageCapPerTick.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudDamageCapPerTick.ForeColor = System.Drawing.Color.Gainsboro;
            nudDamageCapPerTick.Location = new System.Drawing.Point(14, 152);
            nudDamageCapPerTick.Margin = new Padding(4, 3, 4, 3);
            nudDamageCapPerTick.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudDamageCapPerTick.Name = "nudDamageCapPerTick";
            nudDamageCapPerTick.Size = new Size(117, 23);
            nudDamageCapPerTick.TabIndex = 8;
            nudDamageCapPerTick.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // nudSchedulerIntervalSeconds
            //
            nudSchedulerIntervalSeconds.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            nudSchedulerIntervalSeconds.ForeColor = System.Drawing.Color.Gainsboro;
            nudSchedulerIntervalSeconds.Location = new System.Drawing.Point(14, 187);
            nudSchedulerIntervalSeconds.Margin = new Padding(4, 3, 4, 3);
            nudSchedulerIntervalSeconds.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudSchedulerIntervalSeconds.Name = "nudSchedulerIntervalSeconds";
            nudSchedulerIntervalSeconds.Size = new Size(117, 23);
            nudSchedulerIntervalSeconds.TabIndex = 10;
            nudSchedulerIntervalSeconds.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // btnSave
            //
            btnSave.Location = new System.Drawing.Point(14, 221);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6, 6, 6, 6);
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // lblBaseHp
            // 
            lblBaseHp.AutoSize = true;
            lblBaseHp.Location = new System.Drawing.Point(138, 16);
            lblBaseHp.Margin = new Padding(4, 0, 4, 0);
            lblBaseHp.Name = "lblBaseHp";
            lblBaseHp.Size = new Size(50, 15);
            lblBaseHp.TabIndex = 1;
            lblBaseHp.Text = "Base HP";
            // 
            // lblHpPerLevel
            // 
            lblHpPerLevel.AutoSize = true;
            lblHpPerLevel.Location = new System.Drawing.Point(138, 51);
            lblHpPerLevel.Margin = new Padding(4, 0, 4, 0);
            lblHpPerLevel.Name = "lblHpPerLevel";
            lblHpPerLevel.Size = new Size(55, 15);
            lblHpPerLevel.TabIndex = 3;
            lblHpPerLevel.Text = "HP/Level";
            // 
            // lblMaturation
            // 
            lblMaturation.AutoSize = true;
            lblMaturation.Location = new System.Drawing.Point(138, 85);
            lblMaturation.Margin = new Padding(4, 0, 4, 0);
            lblMaturation.Name = "lblMaturation";
            lblMaturation.Size = new Size(61, 15);
            lblMaturation.TabIndex = 5;
            lblMaturation.Text = "Mature (s)";
            // 
            // lblCooldown
            // 
            lblCooldown.AutoSize = true;
            lblCooldown.Location = new System.Drawing.Point(138, 120);
            lblCooldown.Margin = new Padding(4, 0, 4, 0);
            lblCooldown.Name = "lblCooldown";
            lblCooldown.Size = new Size(78, 15);
            lblCooldown.TabIndex = 7;
            lblCooldown.Text = "Cooldown (s)";
            //
            // lblDamageCapPerTick
            //
            lblDamageCapPerTick.AutoSize = true;
            lblDamageCapPerTick.Location = new System.Drawing.Point(138, 154);
            lblDamageCapPerTick.Margin = new Padding(4, 0, 4, 0);
            lblDamageCapPerTick.Name = "lblDamageCapPerTick";
            lblDamageCapPerTick.Size = new Size(111, 15);
            lblDamageCapPerTick.TabIndex = 9;
            lblDamageCapPerTick.Text = "Damage Cap/Tick";
            //
            // lblSchedulerInterval
            //
            lblSchedulerInterval.AutoSize = true;
            lblSchedulerInterval.Location = new System.Drawing.Point(138, 189);
            lblSchedulerInterval.Margin = new Padding(4, 0, 4, 0);
            lblSchedulerInterval.Name = "lblSchedulerInterval";
            lblSchedulerInterval.Size = new Size(133, 15);
            lblSchedulerInterval.TabIndex = 11;
            lblSchedulerInterval.Text = "Scheduler Interval (s)";
            //
            // FrmPrismOptions
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize = new Size(280, 266);
            Controls.Add(btnSave);
            Controls.Add(lblSchedulerInterval);
            Controls.Add(nudSchedulerIntervalSeconds);
            Controls.Add(lblDamageCapPerTick);
            Controls.Add(nudDamageCapPerTick);
            Controls.Add(lblCooldown);
            Controls.Add(nudAttackCooldown);
            Controls.Add(lblMaturation);
            Controls.Add(nudMaturationSeconds);
            Controls.Add(lblHpPerLevel);
            Controls.Add(nudHpPerLevel);
            Controls.Add(lblBaseHp);
            Controls.Add(nudBaseHp);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPrismOptions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Prism Options";
            ((System.ComponentModel.ISupportInitialize)nudBaseHp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHpPerLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaturationSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAttackCooldown).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDamageCapPerTick).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSchedulerIntervalSeconds).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
