using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmPrisms
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstPrisms;
        private System.Windows.Forms.TextBox txtMapId;
        private DarkNumericUpDown nudX;
        private DarkNumericUpDown nudY;
        private DarkNumericUpDown nudLevel;
        private System.Windows.Forms.TextBox txtWindows;
        private System.Windows.Forms.TextBox txtModules;
        private DarkNumericUpDown nudAreaX;
        private DarkNumericUpDown nudAreaY;
        private DarkNumericUpDown nudAreaW;
        private DarkNumericUpDown nudAreaH;
        private DarkButton btnAdd;
        private DarkButton btnDelete;
        private DarkButton btnSave;
        private System.Windows.Forms.Label lblMapId;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblWindows;
        private System.Windows.Forms.Label lblModules;
        private System.Windows.Forms.Label lblAreaX;
        private System.Windows.Forms.Label lblAreaY;
        private System.Windows.Forms.Label lblAreaW;
        private System.Windows.Forms.Label lblAreaH;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lstPrisms = new System.Windows.Forms.ListBox();
            this.txtMapId = new System.Windows.Forms.TextBox();
            this.nudX = new DarkNumericUpDown();
            this.nudY = new DarkNumericUpDown();
            this.nudLevel = new DarkNumericUpDown();
            this.txtWindows = new System.Windows.Forms.TextBox();
            this.txtModules = new System.Windows.Forms.TextBox();
            this.nudAreaX = new DarkNumericUpDown();
            this.nudAreaY = new DarkNumericUpDown();
            this.nudAreaW = new DarkNumericUpDown();
            this.nudAreaH = new DarkNumericUpDown();
            this.btnAdd = new DarkButton();
            this.btnDelete = new DarkButton();
            this.btnSave = new DarkButton();
            this.lblMapId = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblWindows = new System.Windows.Forms.Label();
            this.lblModules = new System.Windows.Forms.Label();
            this.lblAreaX = new System.Windows.Forms.Label();
            this.lblAreaY = new System.Windows.Forms.Label();
            this.lblAreaW = new System.Windows.Forms.Label();
            this.lblAreaH = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaH)).BeginInit();
            this.SuspendLayout();
            // 
            // lstPrisms
            // 
            this.lstPrisms.Location = new System.Drawing.Point(10, 10);
            this.lstPrisms.Name = "lstPrisms";
            this.lstPrisms.Size = new System.Drawing.Size(200, 300);
            this.lstPrisms.TabIndex = 0;
            this.lstPrisms.SelectedIndexChanged += new System.EventHandler(this.lstPrisms_SelectedIndexChanged);
            // 
            // txtMapId
            // 
            this.txtMapId.Location = new System.Drawing.Point(220, 10);
            this.txtMapId.Name = "txtMapId";
            this.txtMapId.Size = new System.Drawing.Size(200, 20);
            this.txtMapId.TabIndex = 1;
            // 
            // lblMapId
            // 
            this.lblMapId.AutoSize = true;
            this.lblMapId.Location = new System.Drawing.Point(430, 13);
            this.lblMapId.Name = "lblMapId";
            this.lblMapId.Size = new System.Drawing.Size(44, 13);
            this.lblMapId.TabIndex = 2;
            this.lblMapId.Text = "Map Id";
            // 
            // nudX
            // 
            this.nudX.Location = new System.Drawing.Point(220, 40);
            this.nudX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(80, 20);
            this.nudX.TabIndex = 3;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(310, 42);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 4;
            this.lblX.Text = "X";
            // 
            // nudY
            // 
            this.nudY.Location = new System.Drawing.Point(220, 70);
            this.nudY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(80, 20);
            this.nudY.TabIndex = 5;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(310, 72);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 6;
            this.lblY.Text = "Y";
            // 
            // nudLevel
            // 
            this.nudLevel.Location = new System.Drawing.Point(220, 100);
            this.nudLevel.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(80, 20);
            this.nudLevel.TabIndex = 7;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(310, 102);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(33, 13);
            this.lblLevel.TabIndex = 8;
            this.lblLevel.Text = "Level";
            // 
            // txtWindows
            // 
            this.txtWindows.Location = new System.Drawing.Point(220, 130);
            this.txtWindows.Multiline = true;
            this.txtWindows.Name = "txtWindows";
            this.txtWindows.Size = new System.Drawing.Size(200, 80);
            this.txtWindows.TabIndex = 9;
            // 
            // lblWindows
            // 
            this.lblWindows.AutoSize = true;
            this.lblWindows.Location = new System.Drawing.Point(430, 130);
            this.lblWindows.Name = "lblWindows";
            this.lblWindows.Size = new System.Drawing.Size(140, 13);
            this.lblWindows.TabIndex = 10;
            this.lblWindows.Text = "Windows (Day|HH:mm|HH:mm)";
            // 
            // txtModules
            // 
            this.txtModules.Location = new System.Drawing.Point(220, 220);
            this.txtModules.Multiline = true;
            this.txtModules.Name = "txtModules";
            this.txtModules.Size = new System.Drawing.Size(200, 80);
            this.txtModules.TabIndex = 11;
            // 
            // lblModules
            // 
            this.lblModules.AutoSize = true;
            this.lblModules.Location = new System.Drawing.Point(430, 220);
            this.lblModules.Name = "lblModules";
            this.lblModules.Size = new System.Drawing.Size(47, 13);
            this.lblModules.TabIndex = 12;
            this.lblModules.Text = "Modules";
            // 
            // nudAreaX
            // 
            this.nudAreaX.Location = new System.Drawing.Point(220, 310);
            this.nudAreaX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAreaX.Name = "nudAreaX";
            this.nudAreaX.Size = new System.Drawing.Size(80, 20);
            this.nudAreaX.TabIndex = 13;
            // 
            // lblAreaX
            // 
            this.lblAreaX.AutoSize = true;
            this.lblAreaX.Location = new System.Drawing.Point(310, 312);
            this.lblAreaX.Name = "lblAreaX";
            this.lblAreaX.Size = new System.Drawing.Size(40, 13);
            this.lblAreaX.TabIndex = 14;
            this.lblAreaX.Text = "Area X";
            // 
            // nudAreaY
            // 
            this.nudAreaY.Location = new System.Drawing.Point(220, 340);
            this.nudAreaY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAreaY.Name = "nudAreaY";
            this.nudAreaY.Size = new System.Drawing.Size(80, 20);
            this.nudAreaY.TabIndex = 15;
            // 
            // lblAreaY
            // 
            this.lblAreaY.AutoSize = true;
            this.lblAreaY.Location = new System.Drawing.Point(310, 342);
            this.lblAreaY.Name = "lblAreaY";
            this.lblAreaY.Size = new System.Drawing.Size(40, 13);
            this.lblAreaY.TabIndex = 16;
            this.lblAreaY.Text = "Area Y";
            // 
            // nudAreaW
            // 
            this.nudAreaW.Location = new System.Drawing.Point(220, 370);
            this.nudAreaW.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAreaW.Name = "nudAreaW";
            this.nudAreaW.Size = new System.Drawing.Size(80, 20);
            this.nudAreaW.TabIndex = 17;
            // 
            // lblAreaW
            // 
            this.lblAreaW.AutoSize = true;
            this.lblAreaW.Location = new System.Drawing.Point(310, 372);
            this.lblAreaW.Name = "lblAreaW";
            this.lblAreaW.Size = new System.Drawing.Size(43, 13);
            this.lblAreaW.TabIndex = 18;
            this.lblAreaW.Text = "Area W";
            // 
            // nudAreaH
            // 
            this.nudAreaH.Location = new System.Drawing.Point(220, 400);
            this.nudAreaH.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAreaH.Name = "nudAreaH";
            this.nudAreaH.Size = new System.Drawing.Size(80, 20);
            this.nudAreaH.TabIndex = 19;
            // 
            // lblAreaH
            // 
            this.lblAreaH.AutoSize = true;
            this.lblAreaH.Location = new System.Drawing.Point(310, 402);
            this.lblAreaH.Name = "lblAreaH";
            this.lblAreaH.Size = new System.Drawing.Size(43, 13);
            this.lblAreaH.TabIndex = 20;
            this.lblAreaH.Text = "Area H";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 320);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(80, 320);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmPrisms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 430);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblAreaH);
            this.Controls.Add(this.nudAreaH);
            this.Controls.Add(this.lblAreaW);
            this.Controls.Add(this.nudAreaW);
            this.Controls.Add(this.lblAreaY);
            this.Controls.Add(this.nudAreaY);
            this.Controls.Add(this.lblAreaX);
            this.Controls.Add(this.nudAreaX);
            this.Controls.Add(this.lblModules);
            this.Controls.Add(this.txtModules);
            this.Controls.Add(this.lblWindows);
            this.Controls.Add(this.txtWindows);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.nudLevel);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.lblMapId);
            this.Controls.Add(this.txtMapId);
            this.Controls.Add(this.lstPrisms);
            this.Name = "FrmPrisms";
            this.Text = "Prisms";
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
