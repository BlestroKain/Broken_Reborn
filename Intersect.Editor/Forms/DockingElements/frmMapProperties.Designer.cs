namespace Intersect.Editor.Forms.DockingElements
{
    partial class FrmMapProperties
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
            gridMapProperties = new PropertyGrid();
            pnlZones = new Panel();
            btnEditZones = new Button();
            cmbSubarea = new ComboBox();
            lblSubarea = new Label();
            cmbArea = new ComboBox();
            lblArea = new Label();
            pnlZones.SuspendLayout();
            SuspendLayout();
            // 
            // gridMapProperties
            // 
            gridMapProperties.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            gridMapProperties.CategoryForeColor = System.Drawing.Color.Gainsboro;
            gridMapProperties.CategorySplitterColor = System.Drawing.Color.FromArgb(51, 51, 51);
            gridMapProperties.CommandsBorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            gridMapProperties.CommandsForeColor = System.Drawing.Color.Gainsboro;
            gridMapProperties.CommandsVisibleIfAvailable = false;
            gridMapProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(133, 133, 133);
            gridMapProperties.Dock = DockStyle.Fill;
            gridMapProperties.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridMapProperties.HelpBackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            gridMapProperties.HelpBorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            gridMapProperties.HelpForeColor = System.Drawing.Color.Gainsboro;
            gridMapProperties.LineColor = System.Drawing.Color.FromArgb(49, 51, 53);
            gridMapProperties.Location = new System.Drawing.Point(0, 90);
            gridMapProperties.Name = "gridMapProperties";
            gridMapProperties.Size = new Size(154, 110);
            gridMapProperties.TabIndex = 0;
            gridMapProperties.ToolbarVisible = false;
            gridMapProperties.ViewBackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            gridMapProperties.ViewBorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            gridMapProperties.ViewForeColor = System.Drawing.Color.Gainsboro;
            // 
            // pnlZones
            // 
            pnlZones.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            pnlZones.Controls.Add(btnEditZones);
            pnlZones.Controls.Add(cmbSubarea);
            pnlZones.Controls.Add(lblSubarea);
            pnlZones.Controls.Add(cmbArea);
            pnlZones.Controls.Add(lblArea);
            pnlZones.Dock = DockStyle.Top;
            pnlZones.Location = new System.Drawing.Point(0, 0);
            pnlZones.Name = "pnlZones";
            pnlZones.Size = new Size(154, 90);
            pnlZones.TabIndex = 1;
            // 
            // btnEditZones
            // 
            btnEditZones.Location = new System.Drawing.Point(3, 62);
            btnEditZones.Name = "btnEditZones";
            btnEditZones.Size = new Size(148, 23);
            btnEditZones.TabIndex = 4;
            btnEditZones.Text = "Edit Zones";
            btnEditZones.UseVisualStyleBackColor = true;
            // 
            // cmbSubarea
            // 
            cmbSubarea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSubarea.FormattingEnabled = true;
            cmbSubarea.Location = new System.Drawing.Point(58, 33);
            cmbSubarea.Name = "cmbSubarea";
            cmbSubarea.Size = new Size(93, 21);
            cmbSubarea.TabIndex = 3;
            // 
            // lblSubarea
            // 
            lblSubarea.AutoSize = true;
            lblSubarea.ForeColor = System.Drawing.Color.Gainsboro;
            lblSubarea.Location = new System.Drawing.Point(3, 36);
            lblSubarea.Name = "lblSubarea";
            lblSubarea.Size = new Size(47, 13);
            lblSubarea.TabIndex = 2;
            lblSubarea.Text = "Subarea";
            // 
            // cmbArea
            // 
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArea.FormattingEnabled = true;
            cmbArea.Location = new System.Drawing.Point(58, 6);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(93, 21);
            cmbArea.TabIndex = 1;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.ForeColor = System.Drawing.Color.Gainsboro;
            lblArea.Location = new System.Drawing.Point(3, 9);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(29, 13);
            lblArea.TabIndex = 0;
            lblArea.Text = "Area";
            // 
            // FrmMapProperties
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(154, 200);
            CloseButton = false;
            CloseButtonVisible = false;
            Controls.Add(gridMapProperties);
            Controls.Add(pnlZones);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmMapProperties";
            Text = "Map Properties";
            pnlZones.ResumeLayout(false);
            pnlZones.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid gridMapProperties;
        private System.Windows.Forms.Panel pnlZones;
        private System.Windows.Forms.Button btnEditZones;
        private System.Windows.Forms.ComboBox cmbSubarea;
        private System.Windows.Forms.Label lblSubarea;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label lblArea;
    }
}