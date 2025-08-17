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
        // 
        // grpBestiary
        // 
        grpBestiary.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpBestiary.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpBestiary.Controls.Add(lblNpc);
        grpBestiary.Controls.Add(cmbNpc);
        grpBestiary.Controls.Add(lblUnlock);
        grpBestiary.Controls.Add(cmbUnlock);
        grpBestiary.Controls.Add(lblValue);
        grpBestiary.Controls.Add(nudValue);
        grpBestiary.ForeColor = System.Drawing.Color.White;
        grpBestiary.Location = new System.Drawing.Point(0, 3);
        grpBestiary.Name = "grpBestiary";
        grpBestiary.Size = new Size(350, 117);
        grpBestiary.TabIndex = 0;
        grpBestiary.TabStop = false;
        grpBestiary.Text = "Bestiary";
        // 
        // lblNpc
        // 
        lblNpc.AutoSize = true;
        lblNpc.Location = new System.Drawing.Point(6, 22);
        lblNpc.Name = "lblNpc";
        lblNpc.Size = new Size(38, 15);
        lblNpc.TabIndex = 0;
        lblNpc.Text = "Beast:";
        // 
        // cmbNpc
        // 
        cmbNpc.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        cmbNpc.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        cmbNpc.BorderStyle = ButtonBorderStyle.Solid;
        cmbNpc.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
        cmbNpc.DrawDropdownHoverOutline = false;
        cmbNpc.DrawFocusRectangle = false;
        cmbNpc.DrawMode = DrawMode.OwnerDrawFixed;
        cmbNpc.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbNpc.FlatStyle = FlatStyle.Flat;
        cmbNpc.ForeColor = System.Drawing.Color.Gainsboro;
        cmbNpc.Location = new System.Drawing.Point(120, 18);
        cmbNpc.Name = "cmbNpc";
        cmbNpc.Size = new Size(210, 24);
        cmbNpc.TabIndex = 1;
        cmbNpc.Text = null;
        cmbNpc.TextPadding = new Padding(2);
        // 
        // lblUnlock
        // 
        lblUnlock.AutoSize = true;
        lblUnlock.Location = new System.Drawing.Point(6, 52);
        lblUnlock.Name = "lblUnlock";
        lblUnlock.Size = new Size(47, 15);
        lblUnlock.TabIndex = 2;
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
        cmbUnlock.Location = new System.Drawing.Point(120, 48);
        cmbUnlock.Name = "cmbUnlock";
        cmbUnlock.Size = new Size(210, 24);
        cmbUnlock.TabIndex = 3;
        cmbUnlock.Text = null;
        cmbUnlock.TextPadding = new Padding(2);
        // 
        // lblValue
        // 
        lblValue.AutoSize = true;
        lblValue.Location = new System.Drawing.Point(6, 82);
        lblValue.Name = "lblValue";
        lblValue.Size = new Size(38, 15);
        lblValue.TabIndex = 4;
        lblValue.Text = "Value:";
        // 
        // nudValue
        // 
        nudValue.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudValue.ForeColor = System.Drawing.Color.Gainsboro;
        nudValue.Location = new System.Drawing.Point(120, 80);
        nudValue.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
        nudValue.Name = "nudValue";
        nudValue.Size = new Size(80, 23);
        nudValue.TabIndex = 5;
        nudValue.Value = new decimal(new int[] { 0, 0, 0, 0 });
        // 
        // ConditionControl_BeastHasUnlock
        // 
        BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        Controls.Add(grpBestiary);
        Name = "ConditionControl_BeastHasUnlock";
        Size = new Size(350, 120);
        grpBestiary.ResumeLayout(false);
        grpBestiary.PerformLayout();
        ((ISupportInitialize)nudValue).EndInit();
        ResumeLayout(false);
    }
}
