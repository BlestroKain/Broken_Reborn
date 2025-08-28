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
        private DarkButton btnSave;
        private System.Windows.Forms.Label lblBaseHp;
        private System.Windows.Forms.Label lblHpPerLevel;
        private System.Windows.Forms.Label lblMaturation;
        private System.Windows.Forms.Label lblCooldown;

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
            this.nudBaseHp = new DarkNumericUpDown();
            this.nudHpPerLevel = new DarkNumericUpDown();
            this.nudMaturationSeconds = new DarkNumericUpDown();
            this.nudAttackCooldown = new DarkNumericUpDown();
            this.btnSave = new DarkButton();
            this.lblBaseHp = new System.Windows.Forms.Label();
            this.lblHpPerLevel = new System.Windows.Forms.Label();
            this.lblMaturation = new System.Windows.Forms.Label();
            this.lblCooldown = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseHp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpPerLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaturationSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackCooldown)).BeginInit();
            this.SuspendLayout();
            // 
            // nudBaseHp
            // 
            this.nudBaseHp.Location = new System.Drawing.Point(12, 12);
            this.nudBaseHp.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudBaseHp.Name = "nudBaseHp";
            this.nudBaseHp.Size = new System.Drawing.Size(100, 20);
            this.nudBaseHp.TabIndex = 0;
            // 
            // lblBaseHp
            // 
            this.lblBaseHp.AutoSize = true;
            this.lblBaseHp.Location = new System.Drawing.Point(118, 14);
            this.lblBaseHp.Name = "lblBaseHp";
            this.lblBaseHp.Size = new System.Drawing.Size(48, 13);
            this.lblBaseHp.TabIndex = 1;
            this.lblBaseHp.Text = "Base HP";
            // 
            // nudHpPerLevel
            // 
            this.nudHpPerLevel.Location = new System.Drawing.Point(12, 42);
            this.nudHpPerLevel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudHpPerLevel.Name = "nudHpPerLevel";
            this.nudHpPerLevel.Size = new System.Drawing.Size(100, 20);
            this.nudHpPerLevel.TabIndex = 2;
            // 
            // lblHpPerLevel
            // 
            this.lblHpPerLevel.AutoSize = true;
            this.lblHpPerLevel.Location = new System.Drawing.Point(118, 44);
            this.lblHpPerLevel.Name = "lblHpPerLevel";
            this.lblHpPerLevel.Size = new System.Drawing.Size(54, 13);
            this.lblHpPerLevel.TabIndex = 3;
            this.lblHpPerLevel.Text = "HP/Level";
            // 
            // nudMaturationSeconds
            // 
            this.nudMaturationSeconds.Location = new System.Drawing.Point(12, 72);
            this.nudMaturationSeconds.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMaturationSeconds.Name = "nudMaturationSeconds";
            this.nudMaturationSeconds.Size = new System.Drawing.Size(100, 20);
            this.nudMaturationSeconds.TabIndex = 4;
            // 
            // lblMaturation
            // 
            this.lblMaturation.AutoSize = true;
            this.lblMaturation.Location = new System.Drawing.Point(118, 74);
            this.lblMaturation.Name = "lblMaturation";
            this.lblMaturation.Size = new System.Drawing.Size(60, 13);
            this.lblMaturation.TabIndex = 5;
            this.lblMaturation.Text = "Mature (s)";
            // 
            // nudAttackCooldown
            // 
            this.nudAttackCooldown.Location = new System.Drawing.Point(12, 102);
            this.nudAttackCooldown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudAttackCooldown.Name = "nudAttackCooldown";
            this.nudAttackCooldown.Size = new System.Drawing.Size(100, 20);
            this.nudAttackCooldown.TabIndex = 6;
            // 
            // lblCooldown
            // 
            this.lblCooldown.AutoSize = true;
            this.lblCooldown.Location = new System.Drawing.Point(118, 104);
            this.lblCooldown.Name = "lblCooldown";
            this.lblCooldown.Size = new System.Drawing.Size(69, 13);
            this.lblCooldown.TabIndex = 7;
            this.lblCooldown.Text = "Cooldown (s)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmPrismOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 170);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblCooldown);
            this.Controls.Add(this.nudAttackCooldown);
            this.Controls.Add(this.lblMaturation);
            this.Controls.Add(this.nudMaturationSeconds);
            this.Controls.Add(this.lblHpPerLevel);
            this.Controls.Add(this.nudHpPerLevel);
            this.Controls.Add(this.lblBaseHp);
            this.Controls.Add(this.nudBaseHp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrismOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prism Options";
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseHp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpPerLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaturationSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackCooldown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
