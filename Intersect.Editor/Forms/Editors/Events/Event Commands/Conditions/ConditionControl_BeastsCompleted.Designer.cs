using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands.Conditions;

partial class ConditionControl_BeastsCompleted
{
    private IContainer components = null!;
    private DarkGroupBox grpBeasts;
    private Label lblUnlock;
    private DarkComboBox cmbUnlock;
    private Label lblCount;
    private DarkNumericUpDown nudCount;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpBeasts = new DarkGroupBox();
        lblUnlock = new Label();
        cmbUnlock = new DarkComboBox();
        lblCount = new Label();
        nudCount = new DarkNumericUpDown();
        grpBeasts.SuspendLayout();
        ((ISupportInitialize)nudCount).BeginInit();
        SuspendLayout();
        // 
        // grpBeasts
        // 
        grpBeasts.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpBeasts.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpBeasts.Controls.Add(lblUnlock);
        grpBeasts.Controls.Add(cmbUnlock);
        grpBeasts.Controls.Add(lblCount);
        grpBeasts.Controls.Add(nudCount);
        grpBeasts.ForeColor = System.Drawing.Color.Gainsboro;
        grpBeasts.Location = new System.Drawing.Point(0, 0);
        grpBeasts.Name = "grpBeasts";
        grpBeasts.Size = new Size(350, 90);
        grpBeasts.TabIndex = 0;
        grpBeasts.TabStop = false;
        grpBeasts.Text = "Beasts Completed";
        // 
        // lblUnlock
        // 
        lblUnlock.AutoSize = true;
        lblUnlock.Location = new System.Drawing.Point(6, 22);
        lblUnlock.Name = "lblUnlock";
        lblUnlock.Size = new Size(47, 15);
        lblUnlock.TabIndex = 0;
        lblUnlock.Text = "Unlock:";
        // 
        // cmbUnlock
        // 
        cmbUnlock.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        cmbUnlock.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        cmbUnlock.BorderStyle = ButtonBorderStyle.Solid;
        cmbUnlock.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
        cmbUnlock.DrawDropdownHoverOutline = false;
        cmbUnlock.DrawFocusRectangle = false;
        cmbUnlock.DrawMode = DrawMode.OwnerDrawFixed;
        cmbUnlock.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbUnlock.FlatStyle = FlatStyle.Flat;
        cmbUnlock.ForeColor = System.Drawing.Color.Gainsboro;
        cmbUnlock.Location = new System.Drawing.Point(120, 18);
        cmbUnlock.Name = "cmbUnlock";
        cmbUnlock.Size = new Size(210, 24);
        cmbUnlock.TabIndex = 1;
        cmbUnlock.Text = null;
        cmbUnlock.TextPadding = new Padding(2);
        // 
        // lblCount
        // 
        lblCount.AutoSize = true;
        lblCount.Location = new System.Drawing.Point(6, 52);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(43, 15);
        lblCount.TabIndex = 2;
        lblCount.Text = "Count:";
        // 
        // nudCount
        // 
        nudCount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudCount.ForeColor = System.Drawing.Color.Gainsboro;
        nudCount.Location = new System.Drawing.Point(120, 50);
        nudCount.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
        nudCount.Name = "nudCount";
        nudCount.Size = new Size(80, 23);
        nudCount.TabIndex = 3;
        nudCount.Value = new decimal(new int[] { 0, 0, 0, 0 });
        // 
        // ConditionControl_BeastsCompleted
        // 
        BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        Controls.Add(grpBeasts);
        Name = "ConditionControl_BeastsCompleted";
        Size = new Size(350, 90);
        grpBeasts.ResumeLayout(false);
        grpBeasts.PerformLayout();
        ((ISupportInitialize)nudCount).EndInit();
        ResumeLayout(false);
    }
}
