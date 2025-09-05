using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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

    public FrmZoneEditor()
    {
        ApplyHooks();
        InitializeComponent();
        Icon = Program.Icon;

        _btnSave = btnSave;
        _btnCancel = btnCancel;

        treeZones.AfterSelect += TreeAfterSelect;
        chkOverrideFlags.CheckedChanged += OverrideFlagsChanged;
        chkOverrideModifiers.CheckedChanged += OverrideModifiersChanged;
        btnSave.Click += SaveClick;
        btnCancel.Click += CancelClick;

        SetupFlagControls();
        SetupModifierControls();

        PopulateTree();
        UpdateEditorButtons(false);
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
        treeZones.Nodes.Clear();
        foreach (var pair in Zone.Lookup.OrderBy(p => p.Value?.Name))
        {
            var zoneNode = new TreeNode(pair.Value?.Name ?? string.Empty) { Tag = pair.Value };

            foreach (var subPair in Subzone.Lookup
                         .Where(z => z.Value is Subzone subzone && subzone.ZoneId == pair.Key))
            {
                if (subPair.Value is Subzone subzone)
                {
                    zoneNode.Nodes.Add(new TreeNode(subzone.Name) { Tag = subzone });
                }
            }

            treeZones.Nodes.Add(zoneNode);
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
            chkOverrideFlags.Visible = false;
            chkOverrideModifiers.Visible = false;
            return;
        }

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
            ZoneFlags flags = ZoneFlags.None;
            foreach (var pair in _flagBoxes)
            {
                if (pair.Value.Checked) flags |= pair.Key;
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