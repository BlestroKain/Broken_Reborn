using DarkUI.Controls;
using DarkUI.Forms;
using Intersect.Editor.Content;
using Intersect.Editor.Core;
using Intersect.Editor.General;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.PlayerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Editor.Forms.Controls;

namespace Intersect.Editor.Forms.Editors;

public partial class FrmSets : EditorForm
{
    private readonly List<SetBase> mChanged = new();
    private SetBase? mEditorItem;

    public FrmSets()
    {
        ApplyHooks();
        InitializeComponent();
        Icon = Program.Icon;

        lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);

        cmbAddItem.Items.Clear();
        foreach (var item in ItemDescriptor.Lookup.Values.OrderBy(i => i.Name))
        {
            cmbAddItem.Items.Add(item.Name);
        }

        cmbEffect.Items.Clear();
        foreach (var eff in Enum.GetValues<ItemEffect>())
        {
            cmbEffect.Items.Add(eff.ToString());
        }

        GenerateStatControls();
        GenerateVitalControls();
        InitEditor();
    }

    private void GenerateStatControls()
    {
        flpStats.Controls.Clear();
        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            var lbl = new DarkLabel { Text = stat.ToString(), Width = 90 };
            var nud = new DarkNumericUpDown { Width = 60, Minimum = -999, Maximum = 999, Tag = stat };
            flpStats.Controls.Add(lbl);
            flpStats.Controls.Add(nud);
        }
    }

    private void GenerateVitalControls()
    {
        flpVitals.Controls.Clear();
        foreach (Vital vital in Enum.GetValues(typeof(Vital)))
        {
            var lbl = new DarkLabel { Text = vital.ToString(), Width = 90 };
            var nud = new DarkNumericUpDown { Width = 60, Minimum = -999999, Maximum = 999999, Tag = vital };
            flpVitals.Controls.Add(lbl);
            flpVitals.Controls.Add(nud);
        }
    }

    private void AssignEditorItem(Guid id)
    {
        mEditorItem = SetBase.Get(id);
        UpdateEditor();
    }

    protected override void GameObjectUpdatedDelegate(GameObjectType type)
    {
        if (type == GameObjectType.Set)
        {
            InitEditor();
            if (mEditorItem != null && !SetBase.Lookup.Values.Contains(mEditorItem))
            {
                mEditorItem = null;
                UpdateEditor();
            }
        }
    }

    private void InitEditor()
    {
        lstGameObjects.ClearList();
        foreach (var set in SetBase.Lookup.Values.OrderBy(s => s.Name))
        {
            lstGameObjects.AddItem(set.Id, set.Name, set.Folder);
        }
        if (mEditorItem != null)
        {
            lstGameObjects.SelectItem(mEditorItem.Id);
        }
    }

    private void UpdateEditor()
    {
        if (mEditorItem != null)
        {
            pnlContainer.Show();
            txtName.Text = mEditorItem.Name;
            lstItems.Items.Clear();
            foreach (var id in mEditorItem.ItemIds)
            {
                lstItems.Items.Add(ItemDescriptor.GetName(id));
            }
            foreach (DarkNumericUpDown nud in flpStats.Controls.OfType<DarkNumericUpDown>())
            {
                var stat = (Stat)nud.Tag;
                nud.Value = mEditorItem.StatsGiven[(int)stat];
            }
            foreach (DarkNumericUpDown nud in flpVitals.Controls.OfType<DarkNumericUpDown>())
            {
                var vital = (Vital)nud.Tag;
                nud.Value = mEditorItem.VitalsGiven[(int)vital];
            }
            lstEffects.Items.Clear();
            foreach (var effect in mEditorItem.Effects)
            {
                lstEffects.Items.Add($"{effect.Type} {effect.Percentage}%");
            }
        }
        else
        {
            pnlContainer.Hide();
        }
    }

    private void btnAddItem_Click(object? sender, EventArgs e)
    {
        if (mEditorItem == null) return;
        var index = cmbAddItem.SelectedIndex;
        if (index < 0) return;
        var id = ItemDescriptor.IdFromList(index);
        if (!mEditorItem.ItemIds.Contains(id))
        {
            mEditorItem.ItemIds.Add(id);
            lstItems.Items.Add(ItemDescriptor.GetName(id));
        }
    }

    private void btnRemoveItem_Click(object? sender, EventArgs e)
    {
        if (mEditorItem == null) return;
        var idx = lstItems.SelectedIndex;
        if (idx < 0 || idx >= mEditorItem.ItemIds.Count) return;
        mEditorItem.ItemIds.RemoveAt(idx);
        lstItems.Items.RemoveAt(idx);
    }

    private void btnAddEffect_Click(object? sender, EventArgs e)
    {
        if (mEditorItem == null) return;
        var effIndex = cmbEffect.SelectedIndex;
        if (effIndex < 0) return;
        var effType = (ItemEffect)Enum.GetValues(typeof(ItemEffect)).GetValue(effIndex)!;
        var pct = (int)nudEffectPercent.Value;
        var data = new EffectData(effType, pct);
        mEditorItem.Effects.Add(data);
        lstEffects.Items.Add($"{effType} {pct}%");
    }

    private void btnRemoveEffect_Click(object? sender, EventArgs e)
    {
        if (mEditorItem == null) return;
        var idx = lstEffects.SelectedIndex;
        if (idx < 0 || idx >= mEditorItem.Effects.Count) return;
        mEditorItem.Effects.RemoveAt(idx);
        lstEffects.Items.RemoveAt(idx);
    }

    private void txtName_TextChanged(object? sender, EventArgs e)
    {
        if (mEditorItem == null) return;
        mEditorItem.Name = txtName.Text;
        lstGameObjects.UpdateText(txtName.Text);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (mEditorItem != null && !mChanged.Contains(mEditorItem))
        {
            mChanged.Add(mEditorItem);
        }

        foreach (DarkNumericUpDown nud in flpStats.Controls.OfType<DarkNumericUpDown>())
        {
            var stat = (Stat)nud.Tag;
            mEditorItem!.StatsGiven[(int)stat] = (int)nud.Value;
        }
        foreach (DarkNumericUpDown nud in flpVitals.Controls.OfType<DarkNumericUpDown>())
        {
            var vital = (Vital)nud.Tag;
            mEditorItem!.VitalsGiven[(int)vital] = (long)nud.Value;
        }

        foreach (var item in mChanged)
        {
            PacketSender.SendSaveObject(item);
            item.DeleteBackup();
        }

        Hide();
        Globals.CurrentEditor = -1;
        Dispose();
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        foreach (var item in mChanged)
        {
            item.RestoreBackup();
            item.DeleteBackup();
        }

        Hide();
        Globals.CurrentEditor = -1;
        Dispose();
    }

    private void UpdateToolStripItems(bool enable) { }
    private void toolStripItemNew_Click(object? sender, EventArgs e)
    {
        var item = new SetBase();
        SetBase.Lookup.Add(item.Id, item);
        mChanged.Add(item);
        InitEditor();
        lstGameObjects.SelectItem(item.Id);
    }
    private void toolStripItemCopy_Click(object? sender, EventArgs e) { }
    private void toolStripItemUndo_Click(object? sender, EventArgs e) { }
    private void toolStripItemPaste_Click(object? sender, EventArgs e) { }
    private void toolStripItemDelete_Click(object? sender, EventArgs e)
    {
        if (mEditorItem == null) return;
        SetBase.Lookup.Remove(mEditorItem.Id);
        mEditorItem = null;
        InitEditor();
    }
}
