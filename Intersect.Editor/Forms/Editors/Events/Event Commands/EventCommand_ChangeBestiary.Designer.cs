using DarkUI.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands;

partial class EventCommandChangeBestiary
{
    private IContainer components = null!;
    private DarkGroupBox grpChangeBestiary;
    private Label lblNpc;
    private DarkComboBox cmbNpc;
    private Label lblUnlock;
    private DarkComboBox cmbUnlock;
    private Label lblOperation;
    private DarkComboBox cmbOperation;
    private Label lblAmount;
    private DarkNumericUpDown nudAmount;
    private DarkButton btnSave;
    private DarkButton btnCancel;

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
        grpChangeBestiary = new DarkGroupBox();
        lblNpc = new Label();
        cmbNpc = new DarkComboBox();
        lblUnlock = new Label();
        cmbUnlock = new DarkComboBox();
        lblOperation = new Label();
        cmbOperation = new DarkComboBox();
        lblAmount = new Label();
        nudAmount = new DarkNumericUpDown();
        btnSave = new DarkButton();
        btnCancel = new DarkButton();
        grpChangeBestiary.SuspendLayout();
        ((ISupportInitialize)nudAmount).BeginInit();
        SuspendLayout();
        grpChangeBestiary.Controls.Add(lblNpc);
        grpChangeBestiary.Controls.Add(cmbNpc);
        grpChangeBestiary.Controls.Add(lblUnlock);
        grpChangeBestiary.Controls.Add(cmbUnlock);
        grpChangeBestiary.Controls.Add(lblOperation);
        grpChangeBestiary.Controls.Add(cmbOperation);
        grpChangeBestiary.Controls.Add(lblAmount);
        grpChangeBestiary.Controls.Add(nudAmount);
        grpChangeBestiary.Location = new System.Drawing.Point(0, 0);
        grpChangeBestiary.Name = "grpChangeBestiary";
        grpChangeBestiary.Size = new System.Drawing.Size(350, 150);
        grpChangeBestiary.TabIndex = 0;
        grpChangeBestiary.TabStop = false;
        grpChangeBestiary.Text = "Change Bestiary";
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
        lblOperation.AutoSize = true;
        lblOperation.Location = new System.Drawing.Point(6, 82);
        lblOperation.Name = "lblOperation";
        lblOperation.Size = new System.Drawing.Size(63, 15);
        lblOperation.TabIndex = 4;
        lblOperation.Text = "Operation:";
        cmbOperation.DrawMode = DrawMode.OwnerDrawFixed;
        cmbOperation.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOperation.Location = new System.Drawing.Point(120, 78);
        cmbOperation.Name = "cmbOperation";
        cmbOperation.Size = new System.Drawing.Size(210, 24);
        cmbOperation.TabIndex = 5;
        lblAmount.AutoSize = true;
        lblAmount.Location = new System.Drawing.Point(6, 112);
        lblAmount.Name = "lblAmount";
        lblAmount.Size = new System.Drawing.Size(56, 15);
        lblAmount.TabIndex = 6;
        lblAmount.Text = "Amount:";
        nudAmount.Location = new System.Drawing.Point(120, 110);
        nudAmount.Maximum = new decimal(new int[] {9999, 0, 0, 0});
        nudAmount.Name = "nudAmount";
        nudAmount.Size = new System.Drawing.Size(80, 23);
        nudAmount.TabIndex = 7;
        btnSave.Location = new System.Drawing.Point(120, 160);
        btnSave.Name = "btnSave";
        btnSave.Padding = new Padding(5);
        btnSave.Size = new System.Drawing.Size(100, 27);
        btnSave.TabIndex = 8;
        btnSave.Text = "Ok";
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new System.Drawing.Point(230, 160);
        btnCancel.Name = "btnCancel";
        btnCancel.Padding = new Padding(5);
        btnCancel.Size = new System.Drawing.Size(100, 27);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        Controls.Add(grpChangeBestiary);
        Name = "EventCommandChangeBestiary";
        Size = new System.Drawing.Size(350, 200);
        grpChangeBestiary.ResumeLayout(false);
        grpChangeBestiary.PerformLayout();
        ((ISupportInitialize)nudAmount).EndInit();
        ResumeLayout(false);
    }
}
