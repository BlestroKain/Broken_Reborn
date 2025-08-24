using Intersect.Editor.Forms.Controls;

namespace Intersect.Editor.Forms.Editors;

partial class FrmSets
{
    private GameObjectList lstGameObjects;
    private System.Windows.Forms.Panel pnlContainer;
    private DarkUI.Controls.DarkTextBox txtName;
    private DarkUI.Controls.DarkLabel lblName;
    private DarkUI.Controls.DarkListBox lstItems;
    private DarkUI.Controls.DarkComboBox cmbAddItem;
    private DarkUI.Controls.DarkButton btnAddItem;
    private DarkUI.Controls.DarkButton btnRemoveItem;
    private System.Windows.Forms.FlowLayoutPanel flpStats;
    private System.Windows.Forms.FlowLayoutPanel flpVitals;
    private DarkUI.Controls.DarkListBox lstEffects;
    private DarkUI.Controls.DarkComboBox cmbEffect;
    private DarkUI.Controls.DarkNumericUpDown nudEffectPercent;
    private DarkUI.Controls.DarkButton btnAddEffect;
    private DarkUI.Controls.DarkButton btnRemoveEffect;
    private DarkUI.Controls.DarkButton btnSave;
    private DarkUI.Controls.DarkButton btnCancel;

    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private void InitializeComponent()
    {
        lstGameObjects = new GameObjectList();
        pnlContainer = new System.Windows.Forms.Panel();
        lblName = new DarkUI.Controls.DarkLabel();
        txtName = new DarkUI.Controls.DarkTextBox();
        lstItems = new DarkUI.Controls.DarkListBox();
        cmbAddItem = new DarkUI.Controls.DarkComboBox();
        btnAddItem = new DarkUI.Controls.DarkButton();
        btnRemoveItem = new DarkUI.Controls.DarkButton();
        flpStats = new System.Windows.Forms.FlowLayoutPanel();
        flpVitals = new System.Windows.Forms.FlowLayoutPanel();
        lstEffects = new DarkUI.Controls.DarkListBox();
        cmbEffect = new DarkUI.Controls.DarkComboBox();
        nudEffectPercent = new DarkUI.Controls.DarkNumericUpDown();
        btnAddEffect = new DarkUI.Controls.DarkButton();
        btnRemoveEffect = new DarkUI.Controls.DarkButton();
        btnSave = new DarkUI.Controls.DarkButton();
        btnCancel = new DarkUI.Controls.DarkButton();
        pnlContainer.SuspendLayout();
        SuspendLayout();
        //
        // lstGameObjects
        //
        lstGameObjects.Location = new System.Drawing.Point(12, 12);
        lstGameObjects.Name = "lstGameObjects";
        lstGameObjects.Size = new System.Drawing.Size(200, 400);
        //
        // pnlContainer
        //
        pnlContainer.Controls.Add(lblName);
        pnlContainer.Controls.Add(txtName);
        pnlContainer.Controls.Add(lstItems);
        pnlContainer.Controls.Add(cmbAddItem);
        pnlContainer.Controls.Add(btnAddItem);
        pnlContainer.Controls.Add(btnRemoveItem);
        pnlContainer.Controls.Add(flpStats);
        pnlContainer.Controls.Add(flpVitals);
        pnlContainer.Controls.Add(lstEffects);
        pnlContainer.Controls.Add(cmbEffect);
        pnlContainer.Controls.Add(nudEffectPercent);
        pnlContainer.Controls.Add(btnAddEffect);
        pnlContainer.Controls.Add(btnRemoveEffect);
        pnlContainer.Controls.Add(btnSave);
        pnlContainer.Controls.Add(btnCancel);
        pnlContainer.Location = new System.Drawing.Point(218, 12);
        pnlContainer.Name = "pnlContainer";
        pnlContainer.Size = new System.Drawing.Size(570, 400);
        //
        // lblName
        //
        lblName.Location = new System.Drawing.Point(6, 9);
        lblName.Name = "lblName";
        lblName.Size = new System.Drawing.Size(48, 23);
        lblName.Text = "Name:";
        //
        // txtName
        //
        txtName.Location = new System.Drawing.Point(60, 6);
        txtName.Name = "txtName";
        txtName.Size = new System.Drawing.Size(200, 23);
        txtName.TextChanged += txtName_TextChanged;
        //
        // lstItems
        //
        lstItems.Location = new System.Drawing.Point(6, 35);
        lstItems.Name = "lstItems";
        lstItems.Size = new System.Drawing.Size(200, 100);
        //
        // cmbAddItem
        //
        cmbAddItem.Location = new System.Drawing.Point(212, 35);
        cmbAddItem.Name = "cmbAddItem";
        cmbAddItem.Size = new System.Drawing.Size(150, 23);
        //
        // btnAddItem
        //
        btnAddItem.Location = new System.Drawing.Point(368, 35);
        btnAddItem.Name = "btnAddItem";
        btnAddItem.Size = new System.Drawing.Size(75, 23);
        btnAddItem.Text = "Add";
        btnAddItem.Click += btnAddItem_Click;
        //
        // btnRemoveItem
        //
        btnRemoveItem.Location = new System.Drawing.Point(368, 64);
        btnRemoveItem.Name = "btnRemoveItem";
        btnRemoveItem.Size = new System.Drawing.Size(75, 23);
        btnRemoveItem.Text = "Remove";
        btnRemoveItem.Click += btnRemoveItem_Click;
        //
        // flpStats
        //
        flpStats.Location = new System.Drawing.Point(6, 141);
        flpStats.Name = "flpStats";
        flpStats.Size = new System.Drawing.Size(260, 100);
        //
        // flpVitals
        //
        flpVitals.Location = new System.Drawing.Point(272, 141);
        flpVitals.Name = "flpVitals";
        flpVitals.Size = new System.Drawing.Size(260, 100);
        //
        // lstEffects
        //
        lstEffects.Location = new System.Drawing.Point(6, 247);
        lstEffects.Name = "lstEffects";
        lstEffects.Size = new System.Drawing.Size(200, 100);
        //
        // cmbEffect
        //
        cmbEffect.Location = new System.Drawing.Point(212, 247);
        cmbEffect.Name = "cmbEffect";
        cmbEffect.Size = new System.Drawing.Size(150, 23);
        //
        // nudEffectPercent
        //
        nudEffectPercent.Location = new System.Drawing.Point(368, 247);
        nudEffectPercent.Name = "nudEffectPercent";
        nudEffectPercent.Size = new System.Drawing.Size(96, 23);
        //
        // btnAddEffect
        //
        btnAddEffect.Location = new System.Drawing.Point(470, 247);
        btnAddEffect.Name = "btnAddEffect";
        btnAddEffect.Size = new System.Drawing.Size(75, 23);
        btnAddEffect.Text = "Add";
        btnAddEffect.Click += btnAddEffect_Click;
        //
        // btnRemoveEffect
        //
        btnRemoveEffect.Location = new System.Drawing.Point(470, 276);
        btnRemoveEffect.Name = "btnRemoveEffect";
        btnRemoveEffect.Size = new System.Drawing.Size(75, 23);
        btnRemoveEffect.Text = "Remove";
        btnRemoveEffect.Click += btnRemoveEffect_Click;
        //
        // btnSave
        //
        btnSave.Location = new System.Drawing.Point(408, 360);
        btnSave.Name = "btnSave";
        btnSave.Size = new System.Drawing.Size(75, 23);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new System.Drawing.Point(489, 360);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(75, 23);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        //
        // FrmSets
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 423);
        Controls.Add(lstGameObjects);
        Controls.Add(pnlContainer);
        Name = "FrmSets";
        Text = "Set Editor";
        pnlContainer.ResumeLayout(false);
        pnlContainer.PerformLayout();
        ResumeLayout(false);
    }
}
