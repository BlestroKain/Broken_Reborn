using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using Intersect.Framework.Core.GameObjects.Zones;
using Intersect.Editor.Networking;
using Intersect.Enums;

namespace Intersect.Editor.Forms.Editors;

public class FrmZoneEditor : EditorForm
{
    private readonly TreeView _tree;
    private readonly DarkGroupBox _grpFlags;
    private readonly DarkGroupBox _grpModifiers;
    private readonly DarkButton btnSave;
    private readonly DarkButton btnCancel;
    private readonly CheckBox _chkOverrideFlags;
    private readonly CheckBox _chkOverrideModifiers;

    private readonly Dictionary<ZoneFlags, CheckBox> _flagBoxes = new();
    private readonly Dictionary<string, NumericUpDown> _modifierControls = new();

    private Zone? _currentZone;
    private Subzone? _currentSubzone;
    private bool _loading;

    public FrmZoneEditor()
    {
        ApplyHooks();
        _tree = new TreeView { Dock = DockStyle.Left, Width = 200 };
        _tree.AfterSelect += TreeAfterSelect;

        _grpFlags = new DarkGroupBox { Text = "Flags", Left = 210, Top = 35, Width = 260, Height = 150 };
        _grpModifiers = new DarkGroupBox { Text = "Modifiers", Left = 210, Top = 215, Width = 260, Height = 120 };

        _chkOverrideFlags = new CheckBox { Text = "Override Flags", Left = 210, Top = 10, AutoSize = true };
        _chkOverrideFlags.CheckedChanged += OverrideFlagsChanged;
        _chkOverrideModifiers = new CheckBox { Text = "Override Modifiers", Left = 210, Top = 190, AutoSize = true };
        _chkOverrideModifiers.CheckedChanged += OverrideModifiersChanged;

        var y = 20;
        foreach (ZoneFlags flag in Enum.GetValues(typeof(ZoneFlags)))
        {
            if (flag == ZoneFlags.None) continue;
            var cb = new CheckBox { Text = flag.ToString(), Left = 5, Top = y, AutoSize = true };
            _grpFlags.Controls.Add(cb);
            _flagBoxes.Add(flag, cb);
            y += 25;
        }

        y = 20;
        _grpModifiers.Controls.Add(new Label { Text = "Experience Rate", Left = 5, Top = y, AutoSize = true });
        var numExp = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        _grpModifiers.Controls.Add(numExp);
        _modifierControls["ExperienceRate"] = numExp;
        y += 25;
        _grpModifiers.Controls.Add(new Label { Text = "Gold Rate", Left = 5, Top = y, AutoSize = true });
        var numGold = new NumericUpDown { Minimum = 0, Maximum = 1000, Left = 120, Top = y - 3 };
        _grpModifiers.Controls.Add(numGold);
        _modifierControls["GoldRate"] = numGold;

        btnSave = new DarkButton { Text = "Save", Left = 210, Width = 80, Top = 350 };
        btnSave.Click += SaveClick;
        btnCancel = new DarkButton { Text = "Cancel", Left = 300, Width = 80, Top = 350 };
        btnCancel.Click += CancelClick;

        Controls.AddRange(new Control[]
        {
            _tree,
            _chkOverrideFlags,
            _chkOverrideModifiers,
            _grpFlags,
            _grpModifiers,
            btnSave,
            btnCancel
        });

        Icon = Program.Icon;
        Text = "Zone Editor";
        Width = 500;
        Height = 420;

        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnSave.Enabled = false;
        btnCancel.Enabled = false;

        _btnSave = btnSave; // assign to base class
        _btnCancel = btnCancel;

        PopulateTree();
        UpdateEditorButtons(false);
    }

    private void PopulateTree()
    {
        _tree.Nodes.Clear();
        foreach (var pair in Zone.Lookup.OrderBy(p => p.Value?.Name))
        {
            var zoneNode = new TreeNode(pair.Value?.Name ?? string.Empty) { Tag = pair.Value };
            foreach (var sub in Subzone.Lookup.Where(z => z.Value?.ZoneId == pair.Key))
            {
                zoneNode.Nodes.Add(new TreeNode(sub.Value?.Name ?? string.Empty) { Tag = sub.Value });
            }
            _tree.Nodes.Add(zoneNode);
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
        }
        LoadValues();
        UpdateEditorButtons(true);
        _loading = false;
    }

    private void LoadValues()
    {
        if (_currentZone == null)
        {
            foreach (var cb in _flagBoxes.Values) cb.Enabled = false;
            foreach (var num in _modifierControls.Values) num.Enabled = false;
            _chkOverrideFlags.Visible = false;
            _chkOverrideModifiers.Visible = false;
            return;
        }

        ZoneFlags flags;
        ZoneModifiers modifiers;
        if (_currentSubzone != null)
        {
            flags = _currentSubzone.Flags ?? _currentZone.Flags;
            modifiers = _currentSubzone.Modifiers ?? _currentZone.Modifiers;
            _chkOverrideFlags.Visible = true;
            _chkOverrideModifiers.Visible = true;
            _chkOverrideFlags.Checked = _currentSubzone.Flags.HasValue;
            _chkOverrideModifiers.Checked = _currentSubzone.Modifiers != null;
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
            _chkOverrideFlags.Visible = false;
            _chkOverrideModifiers.Visible = false;
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
    }

    private void OverrideFlagsChanged(object? sender, EventArgs e)
    {
        if (_currentSubzone == null || _loading) return;
        _currentSubzone.Flags = _chkOverrideFlags.Checked ? _currentZone?.Flags ?? ZoneFlags.None : null;
        LoadValues();
    }

    private void OverrideModifiersChanged(object? sender, EventArgs e)
    {
        if (_currentSubzone == null || _loading) return;
        _currentSubzone.Modifiers = _chkOverrideModifiers.Checked ? new ZoneModifiers() : null;
        LoadValues();
    }

    private void SaveClick(object? sender, EventArgs e)
    {
        if (_currentZone == null) return;
        if (_currentSubzone != null)
        {
            if (_currentSubzone.Flags.HasValue)
            {
                ZoneFlags flags = ZoneFlags.None;
                foreach (var pair in _flagBoxes)
                {
                    if (pair.Value.Checked) flags |= pair.Key;
                }
                _currentSubzone.Flags = flags;
            }
            if (_currentSubzone.Modifiers != null)
            {
                _currentSubzone.Modifiers.ExperienceRate = (int)_modifierControls["ExperienceRate"].Value;
                _currentSubzone.Modifiers.GoldRate = (int)_modifierControls["GoldRate"].Value;
            }
            PacketSender.SendSaveObject(_currentSubzone);
        }
        else
        {
            ZoneFlags flags = ZoneFlags.None;
            foreach (var pair in _flagBoxes)
            {
                if (pair.Value.Checked) flags |= pair.Key;
            }
            _currentZone.Flags = flags;
            _currentZone.Modifiers.ExperienceRate = (int)_modifierControls["ExperienceRate"].Value;
            _currentZone.Modifiers.GoldRate = (int)_modifierControls["GoldRate"].Value;
            PacketSender.SendSaveObject(_currentZone);
        }
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
        }
    }
}
