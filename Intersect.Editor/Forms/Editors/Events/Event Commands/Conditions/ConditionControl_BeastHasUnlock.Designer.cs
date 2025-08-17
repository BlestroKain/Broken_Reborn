using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands.Conditions;

partial class ConditionControl_BeastHasUnlock
{
    private IContainer components = null!;
    private DarkGroupBox grpBestiary;
    private Label lblNpc;
    private DarkComboBox cmbNpc;
    private Label lblUnlock;
    private DarkComboBox cmbUnlock;
    private Label lblValue;
    private DarkNumericUpDown nudValue;

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
        grpBestiary = new DarkGroupBox();
        lblNpc = new Label();
        cmbNpc = new DarkComboBox();
        lblUnlock = new Label();
        cmbUnlock = new DarkComboBox();
        lblValue = new Label();
        nudValue = new DarkNumericUpDown();
        grpBestiary.SuspendLayout();
        ((ISupportInitialize)nudValue).BeginInit();
        SuspendLayout();
        grpBestiary.Controls.Add(lblNpc);
        grpBestiary.Controls.Add(cmbNpc);
        grpBestiary.Controls.Add(lblUnlock);
        grpBestiary.Controls.Add(cmbUnlock);
        grpBestiary.Controls.Add(lblValue);
        grpBestiary.Controls.Add(nudValue);
        grpBestiary.Location = new System.Drawing.Point(0, 0);
        grpBestiary.Name = "grpBestiary";
        grpBestiary.Size = new System.Drawing.Size(350, 120);
        grpBestiary.TabIndex = 0;
        grpBestiary.TabStop = false;
        grpBestiary.Text = "Bestiary";
        lblNpc.AutoSize = true;
        lblNpc.Location = new System.Drawing.Point(6, 22);
        lblNpc.Name = "lblNpc";
        lblNpc.Size = new System.Drawing.Size(38, 15);
        lblNpc.TabIndex = 0;
        lblNpc.Text = "Beast:";
        cmbNpc.DrawMode = DrawMode.OwnerDrawFixed;
        cmbNpc.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbNpc.Location = new System.Drawing.Point(120, 18);
        cmbNpc.Name = "cmbNpc";
        cmbNpc.Size = new System.Drawing.Size(210, 24);
        cmbNpc.TabIndex = 1;
        lblUnlock.AutoSize = true;
        lblUnlock.Location = new System.Drawing.Point(6, 52);
        lblUnlock.Name = "lblUnlock";
        lblUnlock.Size = new System.Drawing.Size(46, 15);
        lblUnlock.TabIndex = 2;
        lblUnlock.Text = "Unlock:";
        cmbUnlock.DrawMode = DrawMode.OwnerDrawFixed;
        cmbUnlock.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbUnlock.Location = new System.Drawing.Point(120, 48);
        cmbUnlock.Name = "cmbUnlock";
        cmbUnlock.Size = new System.Drawing.Size(210, 24);
        cmbUnlock.TabIndex = 3;
        lblValue.AutoSize = true;
        lblValue.Location = new System.Drawing.Point(6, 82);
        lblValue.Name = "lblValue";
        lblValue.Size = new System.Drawing.Size(36, 15);
        lblValue.TabIndex = 4;
        lblValue.Text = "Value:";
        nudValue.Location = new System.Drawing.Point(120, 80);
        nudValue.Maximum = new decimal(new int[] {9999, 0, 0, 0});
        nudValue.Name = "nudValue";
        nudValue.Size = new System.Drawing.Size(80, 23);
        nudValue.TabIndex = 5;
        Controls.Add(grpBestiary);
        Name = "ConditionControl_BeastHasUnlock";
        Size = new System.Drawing.Size(350, 120);
        grpBestiary.ResumeLayout(false);
        grpBestiary.PerformLayout();
        ((ISupportInitialize)nudValue).EndInit();
        ResumeLayout(false);
    }
}
