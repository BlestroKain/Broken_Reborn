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
        components = new Container();
        grpBeasts = new DarkGroupBox();
        lblUnlock = new Label();
        cmbUnlock = new DarkComboBox();
        lblCount = new Label();
        nudCount = new DarkNumericUpDown();
        grpBeasts.SuspendLayout();
        ((ISupportInitialize)nudCount).BeginInit();
        SuspendLayout();
        grpBeasts.Controls.Add(lblUnlock);
        grpBeasts.Controls.Add(cmbUnlock);
        grpBeasts.Controls.Add(lblCount);
        grpBeasts.Controls.Add(nudCount);
        grpBeasts.Location = new System.Drawing.Point(0, 0);
        grpBeasts.Name = "grpBeasts";
        grpBeasts.Size = new System.Drawing.Size(350, 90);
        grpBeasts.TabIndex = 0;
        grpBeasts.TabStop = false;
        grpBeasts.Text = "Beasts Completed";
        lblUnlock.AutoSize = true;
        lblUnlock.Location = new System.Drawing.Point(6, 22);
        lblUnlock.Name = "lblUnlock";
        lblUnlock.Size = new System.Drawing.Size(46, 15);
        lblUnlock.TabIndex = 0;
        lblUnlock.Text = "Unlock:";
        cmbUnlock.DrawMode = DrawMode.OwnerDrawFixed;
        cmbUnlock.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbUnlock.Location = new System.Drawing.Point(120, 18);
        cmbUnlock.Name = "cmbUnlock";
        cmbUnlock.Size = new System.Drawing.Size(210, 24);
        cmbUnlock.TabIndex = 1;
        lblCount.AutoSize = true;
        lblCount.Location = new System.Drawing.Point(6, 52);
        lblCount.Name = "lblCount";
        lblCount.Size = new System.Drawing.Size(43, 15);
        lblCount.TabIndex = 2;
        lblCount.Text = "Count:";
        nudCount.Location = new System.Drawing.Point(120, 50);
        nudCount.Maximum = new decimal(new int[] {9999, 0, 0, 0});
        nudCount.Name = "nudCount";
        nudCount.Size = new System.Drawing.Size(80, 23);
        nudCount.TabIndex = 3;
        Controls.Add(grpBeasts);
        Name = "ConditionControl_BeastsCompleted";
        Size = new System.Drawing.Size(350, 90);
        grpBeasts.ResumeLayout(false);
        grpBeasts.PerformLayout();
        ((ISupportInitialize)nudCount).EndInit();
        ResumeLayout(false);
    }
}
