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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMapProperties));
            this.gridMapProperties = new System.Windows.Forms.PropertyGrid();
            this.pnlZones = new System.Windows.Forms.Panel();
            this.btnEditZones = new System.Windows.Forms.Button();
            this.cmbSubarea = new System.Windows.Forms.ComboBox();
            this.lblSubarea = new System.Windows.Forms.Label();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.pnlZones.SuspendLayout();
            this.SuspendLayout();
            //
            // gridMapProperties
            //
            this.gridMapProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.gridMapProperties.CategoryForeColor = System.Drawing.Color.Gainsboro;
            this.gridMapProperties.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gridMapProperties.CommandsBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gridMapProperties.CommandsForeColor = System.Drawing.Color.Gainsboro;
            this.gridMapProperties.CommandsVisibleIfAvailable = false;
            this.gridMapProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(133)))));
            this.gridMapProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMapProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridMapProperties.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.gridMapProperties.HelpBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gridMapProperties.HelpForeColor = System.Drawing.Color.Gainsboro;
            this.gridMapProperties.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(53)))));
            this.gridMapProperties.Location = new System.Drawing.Point(0, 90);
            this.gridMapProperties.Name = "gridMapProperties";
            this.gridMapProperties.Size = new System.Drawing.Size(154, 110);
            this.gridMapProperties.TabIndex = 0;
            this.gridMapProperties.ToolbarVisible = false;
            this.gridMapProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.gridMapProperties.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gridMapProperties.ViewForeColor = System.Drawing.Color.Gainsboro;
            //
            // pnlZones
            //
            this.pnlZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlZones.Controls.Add(this.btnEditZones);
            this.pnlZones.Controls.Add(this.cmbSubarea);
            this.pnlZones.Controls.Add(this.lblSubarea);
            this.pnlZones.Controls.Add(this.cmbArea);
            this.pnlZones.Controls.Add(this.lblArea);
            this.pnlZones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlZones.Location = new System.Drawing.Point(0, 0);
            this.pnlZones.Name = "pnlZones";
            this.pnlZones.Size = new System.Drawing.Size(154, 90);
            this.pnlZones.TabIndex = 1;
            //
            // btnEditZones
            //
            this.btnEditZones.Location = new System.Drawing.Point(3, 62);
            this.btnEditZones.Name = "btnEditZones";
            this.btnEditZones.Size = new System.Drawing.Size(148, 23);
            this.btnEditZones.TabIndex = 4;
            this.btnEditZones.Text = "Edit Zones";
            this.btnEditZones.UseVisualStyleBackColor = true;
            //
            // cmbSubarea
            //
            this.cmbSubarea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubarea.FormattingEnabled = true;
            this.cmbSubarea.Location = new System.Drawing.Point(58, 33);
            this.cmbSubarea.Name = "cmbSubarea";
            this.cmbSubarea.Size = new System.Drawing.Size(93, 21);
            this.cmbSubarea.TabIndex = 3;
            //
            // lblSubarea
            //
            this.lblSubarea.AutoSize = true;
            this.lblSubarea.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSubarea.Location = new System.Drawing.Point(3, 36);
            this.lblSubarea.Name = "lblSubarea";
            this.lblSubarea.Size = new System.Drawing.Size(52, 13);
            this.lblSubarea.TabIndex = 2;
            this.lblSubarea.Text = "Subarea";
            //
            // cmbArea
            //
            this.cmbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(58, 6);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(93, 21);
            this.cmbArea.TabIndex = 1;
            //
            // lblArea
            //
            this.lblArea.AutoSize = true;
            this.lblArea.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblArea.Location = new System.Drawing.Point(3, 9);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(32, 13);
            this.lblArea.TabIndex = 0;
            this.lblArea.Text = "Area";
            //
            // frmMapProperties
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 200);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.gridMapProperties);
            this.Controls.Add(this.pnlZones);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmMapProperties";
            this.Text = "Map Properties";
            this.pnlZones.ResumeLayout(false);
            this.pnlZones.PerformLayout();
            this.ResumeLayout(false);

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