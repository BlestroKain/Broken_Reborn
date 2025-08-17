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
        // 
        // grpChangeBestiary
        // 
        grpChangeBestiary.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        grpChangeBestiary.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        grpChangeBestiary.Controls.Add(lblNpc);
        grpChangeBestiary.Controls.Add(cmbNpc);
        grpChangeBestiary.Controls.Add(lblUnlock);
        grpChangeBestiary.Controls.Add(cmbUnlock);
        grpChangeBestiary.Controls.Add(lblOperation);
        grpChangeBestiary.Controls.Add(cmbOperation);
        grpChangeBestiary.Controls.Add(lblAmount);
        grpChangeBestiary.Controls.Add(nudAmount);
        grpChangeBestiary.ForeColor = System.Drawing.Color.Gainsboro;
        grpChangeBestiary.Location = new System.Drawing.Point(0, 0);
        grpChangeBestiary.Name = "grpChangeBestiary";
        grpChangeBestiary.Size = new Size(350, 150);
        grpChangeBestiary.TabIndex = 0;
        grpChangeBestiary.TabStop = false;
        grpChangeBestiary.Text = "Change Bestiary";
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
        // lblOperation
        // 
        lblOperation.AutoSize = true;
        lblOperation.Location = new System.Drawing.Point(6, 82);
        lblOperation.Name = "lblOperation";
        lblOperation.Size = new Size(63, 15);
        lblOperation.TabIndex = 4;
        lblOperation.Text = "Operation:";
        // 
        // cmbOperation
        // 
        cmbOperation.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        cmbOperation.BorderColor = System.Drawing.Color.FromArgb(90, 90, 90);
        cmbOperation.BorderStyle = ButtonBorderStyle.Solid;
        cmbOperation.ButtonColor = System.Drawing.Color.FromArgb(43, 43, 43);
        cmbOperation.DrawDropdownHoverOutline = false;
        cmbOperation.DrawFocusRectangle = false;
        cmbOperation.DrawMode = DrawMode.OwnerDrawFixed;
        cmbOperation.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOperation.FlatStyle = FlatStyle.Flat;
        cmbOperation.ForeColor = System.Drawing.Color.Gainsboro;
        cmbOperation.Location = new System.Drawing.Point(120, 78);
        cmbOperation.Name = "cmbOperation";
        cmbOperation.Size = new Size(210, 24);
        cmbOperation.TabIndex = 5;
        cmbOperation.Text = null;
        cmbOperation.TextPadding = new Padding(2);
        // 
        // lblAmount
        // 
        lblAmount.AutoSize = true;
        lblAmount.Location = new System.Drawing.Point(6, 112);
        lblAmount.Name = "lblAmount";
        lblAmount.Size = new Size(54, 15);
        lblAmount.TabIndex = 6;
        lblAmount.Text = "Amount:";
        // 
        // nudAmount
        // 
        nudAmount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
        nudAmount.ForeColor = System.Drawing.Color.Gainsboro;
        nudAmount.Location = new System.Drawing.Point(120, 110);
        nudAmount.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
        nudAmount.Name = "nudAmount";
        nudAmount.Size = new Size(80, 23);
        nudAmount.TabIndex = 7;
        nudAmount.Value = new decimal(new int[] { 0, 0, 0, 0 });
        // 
        // btnSave
        // 
        btnSave.Location = new System.Drawing.Point(120, 160);
        btnSave.Name = "btnSave";
        btnSave.Padding = new Padding(5);
        btnSave.Size = new Size(100, 27);
        btnSave.TabIndex = 8;
        btnSave.Text = "Ok";
        btnSave.Click += btnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new System.Drawing.Point(230, 160);
        btnCancel.Name = "btnCancel";
        btnCancel.Padding = new Padding(5);
        btnCancel.Size = new Size(100, 27);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // EventCommandChangeBestiary
        // 
        BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        Controls.Add(grpChangeBestiary);
        Name = "EventCommandChangeBestiary";
        Size = new Size(350, 200);
        grpChangeBestiary.ResumeLayout(false);
        grpChangeBestiary.PerformLayout();
        ((ISupportInitialize)nudAmount).EndInit();
        ResumeLayout(false);
    }
}
