using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Forms;
using Intersect.Framework.Core.GameObjects.Zones;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.Editor.Core;

namespace Intersect.Editor.Forms.Editors;

public partial class FrmZoneEditor : EditorForm
{
    private readonly Dictionary<ZoneFlags, CheckBox> _flagBoxes = new();
    private readonly Dictionary<string, NumericUpDown> _modifierControls = new();

    private Zone? _currentZone;
    private Subzone? _currentSubzone;
    private bool _loading;

    private string? _copiedData;
    private bool _copiedIsSubzone;

    public FrmZoneEditor()
    {
        ApplyHooks();
        InitializeComponent();
        Icon = Program.Icon;

        _btnSave = btnSave;
        _btnCancel = btnCancel;

        treeZones.AfterSelect += TreeAfterSelect;
        treeZones.GotFocus += TreeFocusChanged;
        treeZones.LostFocus += TreeFocusChanged;
        treeZones.KeyDown += TreeZones_KeyDown;

        toolStripItemNew.Click += ToolStripItemNew_Click;
        toolStripItemDelete.Click += ToolStripItemDelete_Click;
        toolStripItemCopy.Click += ToolStripItemCopy_Click;
        toolStripItemPaste.Click += ToolStripItemPaste_Click;
        toolStripItemUndo.Click += ToolStripItemUndo_Click;
        chkOverrideFlags.CheckedChanged += OverrideFlagsChanged;
        chkOverrideModifiers.CheckedChanged += OverrideModifiersChanged;
        btnSave.Click += SaveClick;
        btnCancel.Click += CancelClick;

        SetupFlagControls();
        SetupModifierControls();

        PopulateTree();
        UpdateEditorButtons(false);
        UpdateToolStripItems();
    }

    private void SetupFlagControls()
    {
        var y = 20;
        foreach (ZoneFlags flag in Enum.GetValues(typeof(ZoneFlags)))
        {
            if (flag == ZoneFlags.None)
            {
                continue;
            }

            var cb = new CheckBox
            {
                Text = flag.ToString(),
                Left = 5,
                Top = y,
                AutoSize = true
            };
            grpFlags.Controls.Add(cb);
            _flagBoxes.Add(flag, cb);
            y += 25;
        }
    }

    private void SetupModifierControls()
    {
        var y = 20;
        grpModifiers.Controls.Add(new Label { Text = "Experience Rate", Left = 5, Top = y, AutoSize = true });
        var numExp = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numExp);
        _modifierControls["ExperienceRate"] = numExp;
        y += 25;

        grpModifiers.Controls.Add(new Label { Text = "Gold Rate", Left = 5, Top = y, AutoSize = true });
        var numGold = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numGold);
        _modifierControls["GoldRate"] = numGold;
        y += 25;

        grpModifiers.Controls.Add(new Label { Text = "Drop Rate", Left = 5, Top = y, AutoSize = true });
        var numDrop = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numDrop);
        _modifierControls["DropRate"] = numDrop;
        y += 25;

        grpModifiers.Controls.Add(new Label { Text = "Damage Rate", Left = 5, Top = y, AutoSize = true });
        var numDamage = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numDamage);
        _modifierControls["DamageRate"] = numDamage;
        y += 25;

        grpModifiers.Controls.Add(new Label { Text = "Movement Speed", Left = 5, Top = y, AutoSize = true });
        var numMove = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numMove);
        _modifierControls["MovementSpeed"] = numMove;
        y += 25;

        grpModifiers.Controls.Add(new Label { Text = "Mount Speed", Left = 5, Top = y, AutoSize = true });
        var numMount = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numMount);
        _modifierControls["MountSpeed"] = numMount;
        y += 25;

        grpModifiers.Controls.Add(new Label { Text = "Regeneration Rate", Left = 5, Top = y, AutoSize = true });
        var numRegen = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        grpModifiers.Controls.Add(numRegen);
        _modifierControls["RegenerationRate"] = numRegen;
    }

    private void PopulateTree()
    {
        var selectedZoneId = _currentZone?.Id;
        var selectedSubzoneId = _currentSubzone?.Id;
        TreeNode? selectedNode = null;

        treeZones.BeginUpdate();
        treeZones.Nodes.Clear();
        foreach (var pair in Zone.Lookup.OrderBy(p => p.Value?.Name))
        {
            var zone = pair.Value;
            var zoneNode = new TreeNode(zone?.Name ?? string.Empty) { Tag = zone };
            if (zone != null && zone.Id == selectedZoneId && selectedSubzoneId == null)
            {
                selectedNode = zoneNode;
            }

            foreach (var subPair in Subzone.Lookup
                         .Where(z => z.Value is Subzone subzone && subzone.ZoneId == pair.Key))
            {
                if (subPair.Value is Subzone subzone)
                {
                    var subNode = new TreeNode(subzone.Name) { Tag = subzone };
                    zoneNode.Nodes.Add(subNode);
                    if (subzone.Id == selectedSubzoneId)
                    {
                        selectedNode = subNode;
                    }
                }
            }

            treeZones.Nodes.Add(zoneNode);
        }
        treeZones.EndUpdate();

        if (selectedNode != null)
        {
            treeZones.SelectedNode = selectedNode;
            selectedNode.EnsureVisible();
        }
    }

    public void InitEditor() => PopulateTree();

    private void TreeAfterSelect(object? sender, TreeViewEventArgs e)
    {
        _loading = true;
        _currentZone = e.Node.Tag as Zone;
        _currentSubzone = e.Node.Tag as Subzone;
        if (_currentSubzone != null)
        {
            _currentZone = Zone.Get(_currentSubzone.ZoneId);
            _currentSubzone.MakeBackup();
        }
        else
        {
            _currentZone?.MakeBackup();
        }
        LoadValues();
        UpdateEditorButtons(true);
        _loading = false;
        UpdateToolStripItems();
    }

    private void LoadValues()
    {
        if (_currentZone == null)
        {
            txtName.Enabled = false;
            txtName.Text = string.Empty;
            foreach (var cb in _flagBoxes.Values) cb.Enabled = false;
            foreach (var num in _modifierControls.Values) num.Enabled = false;
            chkOverrideFlags.Visible = false;
            chkOverrideModifiers.Visible = false;
            return;
        }

        txtName.Enabled = true;
        txtName.Text = _currentSubzone?.Name ?? _currentZone.Name;

        ZoneFlags flags;
        ZoneModifiers modifiers;
        if (_currentSubzone != null)
        {
            flags = _currentSubzone.Flags ?? _currentZone.Flags;
            modifiers = _currentSubzone.Modifiers ?? _currentZone.Modifiers;
            chkOverrideFlags.Visible = true;
            chkOverrideModifiers.Visible = true;
            chkOverrideFlags.Checked = _currentSubzone.Flags.HasValue;
            chkOverrideModifiers.Checked = _currentSubzone.Modifiers != null;
            foreach (var cb in _flagBoxes.Values)
            {
                cb.Enabled = _currentSubzone.Flags.HasValue;
            }
            foreach (var num in _modifierControls.Values)
            {
                num.Enabled = _currentSubzone.Modifiers != null;
            }
        }
        else
        {
            flags = _currentZone.Flags;
            modifiers = _currentZone.Modifiers;
            chkOverrideFlags.Visible = false;
            chkOverrideModifiers.Visible = false;
            foreach (var cb in _flagBoxes.Values)
            {
                cb.Enabled = true;
            }
            foreach (var num in _modifierControls.Values)
            {
                num.Enabled = true;
            }
        }

        foreach (var pair in _flagBoxes)
        {
            pair.Value.Checked = flags.HasFlag(pair.Key);
        }

        _modifierControls["ExperienceRate"].Value = modifiers.ExperienceRate;
        _modifierControls["GoldRate"].Value = modifiers.GoldRate;
        _modifierControls["DropRate"].Value = modifiers.DropRate;
        _modifierControls["DamageRate"].Value = modifiers.DamageRate;
        _modifierControls["MovementSpeed"].Value = modifiers.MovementSpeed;
        _modifierControls["MountSpeed"].Value = modifiers.MountSpeed;
        _modifierControls["RegenerationRate"].Value = modifiers.RegenerationRate;
    }

    private void OverrideFlagsChanged(object? sender, EventArgs e)
    {
        if (_currentSubzone == null || _loading) return;
        _currentSubzone.Flags = chkOverrideFlags.Checked ? _currentZone?.Flags ?? ZoneFlags.None : null;
        LoadValues();
    }

    private void OverrideModifiersChanged(object? sender, EventArgs e)
    {
        if (_currentSubzone == null || _loading) return;
        _currentSubzone.Modifiers = chkOverrideModifiers.Checked ? new ZoneModifiers() : null;
        foreach (var num in _modifierControls.Values)
        {
            num.Enabled = chkOverrideModifiers.Checked;
        }
        LoadValues();
    }

    private void SaveClick(object? sender, EventArgs e)
    {
        if (_currentZone == null)
        {
            return;
        }

        var name = txtName.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            DarkMessageBox.ShowError("Name cannot be empty.", "Error");
            return;
        }

        if (_currentSubzone != null)
        {
            if (Subzone.Lookup.Any(p => p.Value != null && p.Value.Id != _currentSubzone.Id &&
                                       string.Equals(p.Value.Name, name, StringComparison.OrdinalIgnoreCase)))
            {
                DarkMessageBox.ShowError("A subzone with that name already exists.", "Error");
                return;
            }

            _currentSubzone.Name = name;

            if (_currentSubzone.Flags.HasValue)
            {
                ZoneFlags flags = ZoneFlags.None;
                foreach (var pair in _flagBoxes)
                {
                    if (pair.Value.Checked)
                    {
                        flags |= pair.Key;
                    }
                }
                _currentSubzone.Flags = flags;
            }
            if (_currentSubzone.Modifiers != null)
            {
                _currentSubzone.Modifiers.ExperienceRate = (int)_modifierControls["ExperienceRate"].Value;
                _currentSubzone.Modifiers.GoldRate = (int)_modifierControls["GoldRate"].Value;
                _currentSubzone.Modifiers.DropRate = (int)_modifierControls["DropRate"].Value;
                _currentSubzone.Modifiers.DamageRate = (int)_modifierControls["DamageRate"].Value;
                _currentSubzone.Modifiers.MovementSpeed = (int)_modifierControls["MovementSpeed"].Value;
                _currentSubzone.Modifiers.MountSpeed = (int)_modifierControls["MountSpeed"].Value;
                _currentSubzone.Modifiers.RegenerationRate = (int)_modifierControls["RegenerationRate"].Value;
            }
            PacketSender.SendSaveObject(_currentSubzone);
        }
        else
        {
            if (Zone.Lookup.Any(p => p.Value != null && p.Value.Id != _currentZone.Id &&
                                     string.Equals(p.Value.Name, name, StringComparison.OrdinalIgnoreCase)))
            {
                DarkMessageBox.ShowError("A zone with that name already exists.", "Error");
                return;
            }

            _currentZone.Name = name;

            ZoneFlags flags = ZoneFlags.None;
            foreach (var pair in _flagBoxes)
            {
                if (pair.Value.Checked)
                {
                    flags |= pair.Key;
                }
            }
            _currentZone.Flags = flags;
            _currentZone.Modifiers.ExperienceRate = (int)_modifierControls["ExperienceRate"].Value;
            _currentZone.Modifiers.GoldRate = (int)_modifierControls["GoldRate"].Value;
            _currentZone.Modifiers.DropRate = (int)_modifierControls["DropRate"].Value;
            _currentZone.Modifiers.DamageRate = (int)_modifierControls["DamageRate"].Value;
            _currentZone.Modifiers.MovementSpeed = (int)_modifierControls["MovementSpeed"].Value;
            _currentZone.Modifiers.MountSpeed = (int)_modifierControls["MountSpeed"].Value;
            _currentZone.Modifiers.RegenerationRate = (int)_modifierControls["RegenerationRate"].Value;
            PacketSender.SendSaveObject(_currentZone);
        }

        PopulateTree();
        UpdateToolStripItems();
    }

    private void CancelClick(object? sender, EventArgs e)
    {
        LoadValues();
    }

    protected override void GameObjectUpdatedDelegate(GameObjectType type)
    {
        if (type == GameObjectType.Zone || type == GameObjectType.Subzone)
        {
            PopulateTree();
            UpdateToolStripItems();
        }
    }

    private void TreeFocusChanged(object? sender, EventArgs e) => UpdateToolStripItems();

    private void TreeZones_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control)
        {
            if (e.KeyCode == Keys.N)
            {
                ToolStripItemNew_Click(null, null);
            }
            else if (e.KeyCode == Keys.C)
            {
                ToolStripItemCopy_Click(null, null);
            }
            else if (e.KeyCode == Keys.V)
            {
                ToolStripItemPaste_Click(null, null);
            }
            else if (e.KeyCode == Keys.Z)
            {
                ToolStripItemUndo_Click(null, null);
            }
        }
        else if (e.KeyCode == Keys.Delete)
        {
            ToolStripItemDelete_Click(null, null);
        }
    }

    private void ToolStripItemNew_Click(object? sender, EventArgs e)
    {
        if (_currentZone != null)
        {
            PacketSender.SendCreateObject(GameObjectType.Subzone);
        }
        else
        {
            PacketSender.SendCreateObject(GameObjectType.Zone);
        }

        PopulateTree();
        UpdateToolStripItems();
    }

    private void ToolStripItemDelete_Click(object? sender, EventArgs? e)
    {
        if (!treeZones.Focused)
        {
            return;
        }

        if (_currentSubzone != null)
        {
            PacketSender.SendDeleteObject(_currentSubzone);
            _currentSubzone = null;
            _currentZone = null;
        }
        else if (_currentZone != null)
        {
            PacketSender.SendDeleteObject(_currentZone);
            _currentZone = null;
        }

        PopulateTree();
        LoadValues();
        UpdateEditorButtons(false);
        UpdateToolStripItems();
    }

    private void ToolStripItemCopy_Click(object? sender, EventArgs? e)
    {
        if (!treeZones.Focused)
        {
            return;
        }

        if (_currentSubzone != null)
        {
            _copiedData = _currentSubzone.JsonData;
            _copiedIsSubzone = true;
        }
        else if (_currentZone != null)
        {
            _copiedData = _currentZone.JsonData;
            _copiedIsSubzone = false;
        }

        PopulateTree();
        UpdateToolStripItems();
    }

    private void ToolStripItemPaste_Click(object? sender, EventArgs? e)
    {
        if (!treeZones.Focused || _copiedData == null)
        {
            return;
        }

        if (_copiedIsSubzone && _currentSubzone != null)
        {
            _currentSubzone.Load(_copiedData, true);
            LoadValues();
        }
        else if (!_copiedIsSubzone && _currentZone != null && _currentSubzone == null)
        {
            _currentZone.Load(_copiedData, true);
            LoadValues();
        }

        PopulateTree();
        UpdateToolStripItems();
    }

    private void ToolStripItemUndo_Click(object? sender, EventArgs? e)
    {
        if (!treeZones.Focused)
        {
            return;
        }

        if (_currentSubzone != null)
        {
            _currentSubzone.RestoreBackup();
            LoadValues();
        }
        else if (_currentZone != null)
        {
            _currentZone.RestoreBackup();
            LoadValues();
        }

        PopulateTree();
        UpdateToolStripItems();
    }

    private void UpdateToolStripItems()
    {
        var focused = treeZones.Focused;
        var hasSelection = treeZones.SelectedNode != null;

        toolStripItemNew.Enabled = focused;
        toolStripItemCopy.Enabled = focused && hasSelection;
        toolStripItemPaste.Enabled = focused && hasSelection && _copiedData != null;
        toolStripItemDelete.Enabled = focused && hasSelection;
        toolStripItemUndo.Enabled = focused && hasSelection;
    }
}