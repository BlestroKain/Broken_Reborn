using DarkUI.Forms;
using Intersect.Editor.Networking;
using Intersect.GameObjects;

using Intersect.Editor.Forms.Controls;
using Intersect.Editor.Localization;
using Intersect.Utilities;
using Intersect.Editor.Core;
using Intersect.Enums;
using Intersect.Editor.General;
using Intersect.Framework.Core.Config;
using Intersect.Models;
using Intersect.Framework.Core.GameObjects.Items;
using System.Linq;

namespace Intersect.Editor.Forms.Editors;

public partial class frmSets : EditorForm
{
    private List<SetDescriptor> mChanged = new();
    private string mCopiedItem;
    private SetDescriptor mEditorSet;
    private List<string> mKnownFolders = new();

    private bool EffectValueUpdating = false;

    public frmSets()
    {
        InitializeComponent();
        Icon = Program.Icon;

        lstGameObjects.LostFocus += itemList_FocusChanged;
        lstGameObjects.GotFocus += itemList_FocusChanged;
        lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem,
            toolStripItemNew_Click, toolStripItemCopy_Click,
            toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
    }

    private void AssignEditorItem(Guid id)
    {
        mEditorSet = SetDescriptor.Get(id);
        UpdateEditor();
    }


    protected override void GameObjectUpdatedDelegate(GameObjectType type)
    {
        if (type == GameObjectType.Sets)
        {
            InitEditor();
            if (mEditorSet != null && !DatabaseObject<SetDescriptor>.Lookup.Values.Contains(mEditorSet))
            {
                mEditorSet = null;
                UpdateEditor();
            }
        }
    }
    private void UpdateEditor()
    {
        if (mEditorSet != null)
        {
            txtName.Text = mEditorSet.Name;
            cmbFolder.Text = mEditorSet.Folder;

            // Stats
            nudStr.Value = mEditorSet.Stats[(int)Stat.Attack];
            nudStrPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Attack];

            nudAgi.Value = mEditorSet.Stats[(int)Stat.Agility];
            nudAgiPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Agility];

            nudVit.Value = mEditorSet.Stats[(int)Stat.Vitality];
            nudVitPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Vitality];

            nudInt.Value = mEditorSet.Stats[(int)Stat.Intelligence];
            nudIntPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Intelligence];

            nudDmg.Value = mEditorSet.Stats[(int)Stat.Damages];
            nudDmgPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Damages];

            nudDef.Value = mEditorSet.Stats[(int)Stat.Defense];
            nudDefPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Defense];

            nudCur.Value = mEditorSet.Stats[(int)Stat.Cures];
            nudCurPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Cures];

            nudSpd.Value = mEditorSet.Stats[(int)Stat.Speed];
            nudSpdPercentage.Value = mEditorSet.PercentageStats[(int)Stat.Speed];

            // Vitals
            nudHPRegen.Value = mEditorSet.VitalsRegen[(int)Vital.Health];
            nudMpRegen.Value = mEditorSet.VitalsRegen[(int)Vital.Mana];

            nudHealthBonus.Value = (decimal)mEditorSet.Vitals[(int)Vital.Health];
            nudManaBonus.Value = (decimal)mEditorSet.Vitals[(int)Vital.Mana];

            nudHPPercentage.Value = mEditorSet.PercentageVitals[(int)Vital.Health];
            nudMPPercentage.Value = mEditorSet.PercentageVitals[(int)Vital.Mana];

            // Refrescar listado de ítems del set
            lstItems.Items.Clear();
            foreach (var id in mEditorSet.ItemIds)
            {
                var item =ItemDescriptor.Get(id);
                if (item != null)
                {
                    lstItems.Items.Add(new ItemDisplay { Name = item.Name, Id = item.Id });
                }
            }
            lstBonusEffects.Items.Clear();
            RefreshBonusList();
            // Refrescar combo solo con ítems de tipo equipamiento
            cmbItems.BeginUpdate();
            cmbItems.Items.Clear();
            cmbItems.Items.Add(Strings.General.None); // Index 0 = ninguno
            foreach (var item in ItemDescriptor.Lookup.Values.OfType<ItemDescriptor>())
            {
                if (item.ItemType == ItemType.Equipment)
                {
                    cmbItems.Items.Add(new ComboBoxItem { Name = item.Name, Id = item.Id });
                }
            }
            cmbItems.SelectedIndex = 0;
            cmbItems.EndUpdate();

            // Orbes, efectos adicionales, etc. (si tienes controles/rows para orbes, puedes agregarlos aquí)

            if (!mChanged.Contains(mEditorSet))
            {
                mChanged.Add(mEditorSet);
                mEditorSet.MakeBackup();
            }
        }
        UpdateTierCountLabel();
        UpdateSaveButton();
        UpdateToolStripItems();
    }
    private void form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control)
        {
            if (e.KeyCode == Keys.N)
            {
                toolStripItemNew_Click(null, null);
            }
        }
    }

    private void RefreshBonusList()
    {
        lstBonusEffects.Items.Clear();
        // Skip the "none" value - we don't care about that anymore, that's legacy
        var idx = 1;
        foreach (var effectName in Strings.ItemEditor.bonuseffects.Skip(1))
        {
            lstBonusEffects.Items.Add(GetBonusEffectRow((ItemEffect)idx));
            idx++;
        }
    }

    private void UpdateSaveButton()
    {
        if (mEditorSet == null)
        {
            btnSave.Enabled = false;
            return;
        }

        var ids = mEditorSet.ItemIds;
        var hasItems = ids.Count > 0;
        var hasDuplicates = ids.Count != ids.Distinct().Count;
        btnSave.Enabled = hasItems && !hasDuplicates;
    }

    private void UpdateTierCountLabel()
    {
        if (mEditorSet == null)
        {
            lblTierCount.Text = "Defined/Equipped: 0/0";
            return;
        }

        lblTierCount.Text = $"Defined/Equipped: {mEditorSet.BonusTiers.Count}/{mEditorSet.ItemIds.Count}";
    }
    private void btnSave_Click(object sender, EventArgs e)
    {
        foreach (var set in mChanged)
        {
            PacketSender.SendSaveObject(set);
            set.DeleteBackup();
        }

        Hide();
        Globals.CurrentEditor = -1;
        Dispose();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        foreach (var set in mChanged)
        {
            set.RestoreBackup();
            set.DeleteBackup();
        }

        Hide();
        Globals.CurrentEditor = -1;
        Dispose();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (cmbItems.SelectedItem is ComboBoxItem selectedItem && mEditorSet != null)
        {
            var item = ItemDescriptor.Get(selectedItem.Id);
            if (item != null && item.ItemType == ItemType.Equipment && item.SetId != mEditorSet.Id)
            {
                item.SetId = mEditorSet.Id;
                PacketSender.SendSaveObject(item);

                if (!mEditorSet.ItemIds.Contains(item.Id))
                    mEditorSet.ItemIds.Add(item.Id);

                lstItems.Items.Add(new ItemDisplay { Name = item.Name, Id = item.Id });
            }

            cmbItems.SelectedIndex = 0;
        }

        UpdateTierCountLabel();
        UpdateSaveButton();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstItems.SelectedItem is ItemDisplay selectedItem && mEditorSet != null)
        {
            var item = ItemDescriptor.Get(selectedItem.Id);
            if (item != null && item.SetId == mEditorSet.Id)
            {
                item.SetId = Guid.Empty;
                PacketSender.SendSaveObject(item);
            }

            lstItems.Items.Remove(selectedItem);
            mEditorSet.ItemIds.Remove(selectedItem.Id);
        }

        UpdateTierCountLabel();
        UpdateSaveButton();
    }

    private void toolStripItemNew_Click(object sender, EventArgs e)
    {
        PacketSender.SendCreateObject(GameObjectType.Sets);
    }

    private void toolStripItemDelete_Click(object sender, EventArgs e)
    {
        if (mEditorSet != null && lstGameObjects.Focused)
        {
            if (DarkMessageBox.ShowWarning(
                    Strings.CraftsEditor.deleteprompt, Strings.CraftsEditor.deletetitle, DarkDialogButton.YesNo,
                    Icon
                ) ==
                DialogResult.Yes)
            {
                PacketSender.SendDeleteObject(mEditorSet);
            }
        }
    }

    private void toolStripItemCopy_Click(object sender, EventArgs e)
    {
        if (mEditorSet != null && lstGameObjects.Focused)
        {
            mCopiedItem = mEditorSet.JsonData;
            toolStripItemPaste.Enabled = true;
        }
    }

    private void toolStripItemPaste_Click(object sender, EventArgs e)
    {
        if (mEditorSet != null && mCopiedItem != null && lstGameObjects.Focused)
        {
            mEditorSet.Load(mCopiedItem, true);
            UpdateEditor();
        }
    }

    private void toolStripItemUndo_Click(object sender, EventArgs e)
    {
        if (mChanged.Contains(mEditorSet) && mEditorSet != null)
        {
            if (DarkMessageBox.ShowWarning(
                    Strings.CraftsEditor.undoprompt, Strings.CraftsEditor.undotitle, DarkDialogButton.YesNo,
                    Icon
                ) ==
                DialogResult.Yes)
            {
                mEditorSet.RestoreBackup();
                UpdateEditor();
            }
        }
    }

    public void InitEditor()
    {
        //Collect folders
        var mFolders = new List<string>();
        foreach (var itm in SetDescriptor.Lookup)
        {
            if (!string.IsNullOrEmpty(((SetDescriptor)itm.Value).Folder) &&
                !mFolders.Contains(((SetDescriptor)itm.Value).Folder))
            {
                mFolders.Add(((SetDescriptor)itm.Value).Folder);
                if (!mKnownFolders.Contains(((SetDescriptor)itm.Value).Folder))
                {
                    mKnownFolders.Add(((SetDescriptor)itm.Value).Folder);
                }
            }
        }

        mFolders.Sort();
        mKnownFolders.Sort();
        cmbFolder.Items.Clear();
        cmbFolder.Items.Add("");
        cmbFolder.Items.AddRange(mKnownFolders.ToArray());
        cmbItems.Items.Add("");
        cmbItems.Items.AddRange(ItemDescriptor.Names);

        var items = SetDescriptor.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
            new KeyValuePair<string, string>(((SetDescriptor)pair.Value)?.Name ?? Models.DatabaseObject<SetDescriptor>.Deleted, ((SetDescriptor)pair.Value)?.Folder ?? ""))).ToArray();
        lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
    }

    private void btnAddFolder_Click(object sender, EventArgs e)
    {
        var folderName = "";
        var result = DarkInputBox.ShowInformation(
            Strings.CraftsEditor.folderprompt, Strings.CraftsEditor.foldertitle, ref folderName,
            DarkDialogButton.OkCancel
        );

        if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
        {
            if (!cmbFolder.Items.Contains(folderName))
            {
                mEditorSet.Folder = folderName;
                lstGameObjects.ExpandFolder(folderName);
                InitEditor();
                cmbFolder.Text = folderName;
            }
        }
    }

    private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        mEditorSet.Folder = cmbFolder.Text;
        InitEditor();
    }

    private void btnAlphabetical_Click(object sender, EventArgs e)
    {
        btnAlphabetical.Checked = !btnAlphabetical.Checked;
        InitEditor();
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        InitEditor();
    }

    private void txtSearch_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            txtSearch.Text = Strings.CraftsEditor.searchplaceholder;
        }
    }

    private void txtSearch_Enter(object sender, EventArgs e)
    {
        txtSearch.SelectAll();
        txtSearch.Focus();
    }

    private void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Text = Strings.CraftsEditor.searchplaceholder;
    }

    private bool CustomSearch()
    {
        return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
               txtSearch.Text != Strings.CraftsEditor.searchplaceholder;
    }

    private void txtSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text == Strings.CraftsEditor.searchplaceholder)
        {
            txtSearch.SelectAll();
        }
    }
    private void UpdateToolStripItems()
    {
        toolStripItemCopy.Enabled = mEditorSet != null && lstGameObjects.Focused;
        toolStripItemPaste.Enabled = mEditorSet != null && mCopiedItem != null && lstGameObjects.Focused;
        toolStripItemDelete.Enabled = mEditorSet != null && lstGameObjects.Focused;
        toolStripItemUndo.Enabled = mEditorSet != null && lstGameObjects.Focused;
    }

    private void itemList_FocusChanged(object sender, EventArgs e) => UpdateToolStripItems();
    private void txtName_TextChanged(object sender, EventArgs e)
    {
        mEditorSet.Name = txtName.Text;
        lstGameObjects.UpdateText(txtName.Text);
    }

    private void nudStrPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Attack] = (int)nudStrPercentage.Value;
    }

    private void nudWis_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Agility] = (int)nudAgi.Value;
    }

    private void nudVit_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Vitality] = (int)nudVit.Value;
    }

    private void nudARP_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Intelligence] = (int)nudInt.Value;
    }

    private void nudStr_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Attack] = (int)nudStr.Value;
    }

    private void nudMag_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Damages] = (int)nudDmg.Value;
    }

    private void nudDef_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Defense] = (int)nudDef.Value;
    }

    private void nudMR_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Cures] = (int)nudCur.Value;
    }

    private void nudSpd_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Stats[(int)Stat.Speed] = (int)nudSpd.Value;
    }

    private void nudSpdPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Speed] = (int)nudSpdPercentage.Value;
    }

    private void nudHPRegen_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.VitalsRegen[(int)Vital.Health] = (long)nudHPRegen.Value;
    }

    private void nudEffectPercent_ValueChanged(object sender, EventArgs e)
    {
        if (!IsValidBonusSelection || EffectValueUpdating)
        {
            return;
        }

        mEditorSet.SetEffectOfType(SelectedEffect, (int)nudEffectPercent.Value);
        lstBonusEffects.Items[lstBonusEffects.SelectedIndex] = GetBonusEffectRow(SelectedEffect);
    }

    private void nudMpRegen_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.VitalsRegen[(int)Vital.Mana] = (long)nudMpRegen.Value;
    }

    private void nudHealthBonus_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Vitals[(int)Vital.Health] = (long)nudHealthBonus.Value;
    }

    private void nudManaBonus_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.Vitals[(int)Vital.Mana] = (long)nudManaBonus.Value;
    }

    private void nudHPPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageVitals[(int)Vital.Health] = (int)nudHPPercentage.Value;
    }

    private void nudMPPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageVitals[(int)Vital.Mana] = (int)nudMPPercentage.Value;
    }

    private void nudWisPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Agility] = (int)nudAgiPercentage.Value;
    }

    private void nudVitPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Vitality] = (int)nudVitPercentage.Value;
    }

    private void nudARPPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Intelligence] = (int)nudIntPercentage.Value;
    }

    private void nudMagPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Damages] = (int)nudDmgPercentage.Value;
    }

    private void nudDefPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Defense] = (int)nudDefPercentage.Value;
    }

    private void nudMRPercentage_ValueChanged(object sender, EventArgs e)
    {
        mEditorSet.PercentageStats[(int)Stat.Cures] = (int)nudCurPercentage.Value;
    }
    private void cmbItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbItems.SelectedIndex <= 0)
            return;

        if (cmbItems.SelectedItem is ComboBoxItem selected)
        {
            lblItemSet.Show();
            // Si necesitas previsualizar, aquí ya tienes el ID real:
            // var item = ItemDescriptor.Get(selected.Id);
        }
    }


    private ItemEffect SelectedEffect
    {
        get => IsValidBonusSelection ? (ItemEffect)(lstBonusEffects.SelectedIndex + 1) : ItemEffect.None;
    }

    private bool IsValidBonusSelection
    {
        get => lstBonusEffects.SelectedIndex > -1 && lstBonusEffects.SelectedIndex < lstBonusEffects.Items.Count;
    }
    private string GetBonusEffectRow(ItemEffect itemEffect)
    {
        var effectName = Strings.ItemEditor.bonuseffects[(int)itemEffect];
        var effectAmt = mEditorSet.GetEffectPercentage(itemEffect);
        return Strings.ItemEditor.BonusEffectItem.ToString(effectName, effectAmt);
    }
    private void lstBonusEffects_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!IsValidBonusSelection)
        {
            return;
        }

        var selected = SelectedEffect;
        if (!mEditorSet.EffectsEnabled.Contains(selected))
        {
            mEditorSet.Effects.Add(new EffectData(selected, 0));
        }

        EffectValueUpdating = true;
        nudEffectPercent.Value = mEditorSet.GetEffectPercentage(selected);
        EffectValueUpdating = false;
    }
    private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstItems.SelectedItem is ItemDisplay display)
        {
            // Buscar el ComboBoxItem que tenga el mismo Id y seleccionarlo
            for (int i = 1; i < cmbItems.Items.Count; i++) // empieza en 1 para saltar "None"
            {
                if (cmbItems.Items[i] is ComboBoxItem cb && cb.Id == display.Id)
                {
                    cmbItems.SelectedIndex = i;
                    break;
                }
            }
        }
    }


}
class ItemDisplay
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public override string ToString() => Name;
}

class ComboBoxItem
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public override string ToString() => Name;
}