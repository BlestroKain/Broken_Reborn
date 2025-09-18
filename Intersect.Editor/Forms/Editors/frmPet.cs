using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Intersect.Editor.Content;
using Intersect.Editor.Core;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Animations;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors;

public partial class FrmPet : EditorForm
{
    private readonly BindingList<string> _spellNames = new();
    private readonly Dictionary<SpellEffect, DarkCheckBox> _immunityCheckboxes = new();
    private readonly Dictionary<Stat, DarkNumericUpDown> _statControls = new();
    private readonly Dictionary<Vital, DarkNumericUpDown> _vitalControls = new();
    private readonly Dictionary<Vital, DarkNumericUpDown> _vitalRegenControls = new();

    private readonly List<PetDescriptor> _changed = new();
    private readonly List<string> _knownFolders = new();

    private string? _copiedItem;
    private PetDescriptor? _editorItem;
    private bool _isClosing;

    public FrmPet()
    {
        ApplyHooks();
        InitializeComponent();
        Icon = Program.Icon;
        _btnSave = btnSave;
        _btnCancel = btnCancel;

        lstGameObjects.Init(
            UpdateToolStripItems,
            AssignEditorItem,
            toolStripItemNew_Click,
            toolStripItemCopy_Click,
            toolStripItemUndo_Click,
            toolStripItemPaste_Click,
            toolStripItemDelete_Click
        );

        InitStatControls();
        InitVitalControls();
        InitImmunityControls();

        lstSpells.DataSource = _spellNames;
        pnlContainer.Hide();
        UpdateToolStripItems();
        UpdateEditorButtons(false);
    }

    private void frmPet_Load(object sender, EventArgs e)
    {
        cmbSprite.Items.Clear();
        cmbSprite.Items.Add(Strings.General.None);
        cmbSprite.Items.AddRange(
            GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Entity)
        );

        cmbAttackAnimation.Items.Clear();
        cmbAttackAnimation.Items.Add(Strings.General.None);
        cmbAttackAnimation.Items.AddRange(AnimationDescriptor.Names);

        cmbDeathAnimation.Items.Clear();
        cmbDeathAnimation.Items.Add(Strings.General.None);
        cmbDeathAnimation.Items.AddRange(AnimationDescriptor.Names);

        cmbIdleAnimation.Items.Clear();
        cmbIdleAnimation.Items.Add(Strings.General.None);
        cmbIdleAnimation.Items.AddRange(AnimationDescriptor.Names);

        cmbSpell.Items.Clear();
        cmbSpell.Items.AddRange(SpellDescriptor.Names);
        if (cmbSpell.Items.Count > 0)
        {
            cmbSpell.SelectedIndex = 0;
        }

        cmbDamageType.Items.Clear();
        for (var i = 0; i < Strings.Combat.damagetypes.Count; i++)
        {
            cmbDamageType.Items.Add(Strings.Combat.damagetypes[i]);
        }
        if (cmbDamageType.Items.Count > 0)
        {
            cmbDamageType.SelectedIndex = 0;
        }

        cmbAttackSpeedModifier.Items.Clear();
        foreach (var modifier in Strings.NpcEditor.attackspeedmodifiers)
        {
            cmbAttackSpeedModifier.Items.Add(modifier.Value.ToString());
        }
        if (cmbAttackSpeedModifier.Items.Count > 0)
        {
            cmbAttackSpeedModifier.SelectedIndex = 0;
        }

        cmbScalingStat.Items.Clear();
        for (var i = 0; i < Enum.GetValues<Stat>().Length; i++)
        {
            cmbScalingStat.Items.Add(Globals.GetStatName(i));
        }
        if (cmbScalingStat.Items.Count > 0)
        {
            cmbScalingStat.SelectedIndex = 0;
        }

        toolStripItemNew.ToolTipText = Strings.NpcEditor.New;
        toolStripItemDelete.ToolTipText = Strings.NpcEditor.delete;
        toolStripItemCopy.ToolTipText = Strings.NpcEditor.copy;
        toolStripItemPaste.ToolTipText = Strings.NpcEditor.paste;
        toolStripItemUndo.ToolTipText = Strings.NpcEditor.undo;
        btnAlphabetical.ToolTipText = Strings.NpcEditor.sortalphabetically;

        nudDamage.Maximum = Options.Instance.Player.MaxStat;
        nudScaling.Maximum = 1000;
        nudCritChance.Maximum = 100;
        nudAttackSpeedValue.Maximum = 100000;

        InitLocalization();
        InitEditor();
    }

    private void InitLocalization()
    {
        Text = Strings.Pets.title;
        grpPets.Text = Strings.Pets.petlist;
        btnClearSearch.Text = Strings.Pets.clearsearch;
        txtSearch.Text = Strings.Pets.searchplaceholder;
        grpGeneral.Text = Strings.Pets.general;
        lblName.Text = Strings.Pets.name;
        lblFolder.Text = Strings.Pets.folderlabel;
        btnAddFolder.Text = Strings.Pets.addfolder;
        lblIdleSprite.Text = Strings.Pets.sprite;
        lblLevel.Text = Strings.Pets.level;
        lblExperience.Text = Strings.Pets.experience;
        grpStats.Text = Strings.Pets.stats;
        grpVitals.Text = Strings.Pets.vitals;
        lblVitals.Text = Strings.Pets.maxvitals;
        lblVitalRegen.Text = Strings.Pets.vitalregen;
        grpCombat.Text = Strings.Pets.combat;
        lblAttackAnimation.Text = Strings.Pets.attackanimation;
        lblDeathAnimation.Text = Strings.Pets.deathanimation;
        lblIdleAnimation.Text = Strings.Pets.idleanimation;
        lblDamageType.Text = Strings.Pets.damagetype;
        lblScalingStat.Text = Strings.Pets.scalingstat;
        lblScaling.Text = Strings.Pets.scalingamount;
        lblDamage.Text = Strings.Pets.damage;
        lblCritChance.Text = Strings.Pets.critchance;
        lblCritMultiplier.Text = Strings.Pets.critmultiplier;
        lblAttackSpeedModifier.Text = Strings.Pets.attackspeedmodifier;
        lblAttackSpeedValue.Text = Strings.Pets.attackspeedvalue;
        grpSpells.Text = Strings.Pets.spells;
        btnAddSpell.Text = Strings.Pets.addspell;
        btnRemoveSpell.Text = Strings.Pets.removespell;
        grpImmunities.Text = Strings.Pets.immunities;
        lblTenacity.Text = Strings.Pets.tenacity;
        btnSave.Text = Strings.Pets.save;
        btnCancel.Text = Strings.Pets.cancel;
    }

    private void InitStatControls()
    {
        flpStats.Controls.Clear();
        _statControls.Clear();

        foreach (var stat in Enum.GetValues<Stat>())
        {
            var panel = new Panel
            {
                Width = 120,
                Height = 50,
                Margin = new Padding(4)
            };

            var label = new Label
            {
                Text = Globals.GetStatName((int)stat),
                AutoSize = true,
                Location = new System.Drawing.Point(0, 0)
            };

            var numeric = new DarkNumericUpDown
            {
                Minimum = 0,
                Maximum = Options.Instance.Player.MaxStat,
                Location = new System.Drawing.Point(0, 20),
                Width = 100
            };

            numeric.ValueChanged += (_, _) =>
            {
                if (_editorItem == null)
                {
                    return;
                }

                _editorItem.Stats[(int)stat] = (int)numeric.Value;
            };

            panel.Controls.Add(label);
            panel.Controls.Add(numeric);
            flpStats.Controls.Add(panel);
            _statControls.Add(stat, numeric);
        }
    }

    private void InitVitalControls()
    {
        flpVitals.Controls.Clear();
        flpVitalRegen.Controls.Clear();
        _vitalControls.Clear();
        _vitalRegenControls.Clear();

        foreach (var vital in Enum.GetValues<Vital>())
        {
            var maxPanel = new Panel
            {
                Width = 200,
                Height = 45,
                Margin = new Padding(4)
            };

            var maxLabel = new Label
            {
                Text = vital.ToString(),
                AutoSize = true,
                Location = new System.Drawing.Point(0, 0)
            };

            var maxNumeric = new DarkNumericUpDown
            {
                Minimum = 0,
                Maximum = int.MaxValue,
                Location = new System.Drawing.Point(0, 18),
                Width = 180
            };

            maxNumeric.ValueChanged += (_, _) =>
            {
                if (_editorItem == null)
                {
                    return;
                }

                _editorItem.MaxVitals[(int)vital] = (long)maxNumeric.Value;
            };

            maxPanel.Controls.Add(maxLabel);
            maxPanel.Controls.Add(maxNumeric);
            flpVitals.Controls.Add(maxPanel);
            _vitalControls.Add(vital, maxNumeric);

            var regenPanel = new Panel
            {
                Width = 200,
                Height = 45,
                Margin = new Padding(4)
            };

            var regenLabel = new Label
            {
                Text = vital + " Regen",
                AutoSize = true,
                Location = new System.Drawing.Point(0, 0)
            };

            var regenNumeric = new DarkNumericUpDown
            {
                Minimum = 0,
                Maximum = int.MaxValue,
                Location = new System.Drawing.Point(0, 18),
                Width = 180
            };

            regenNumeric.ValueChanged += (_, _) =>
            {
                if (_editorItem == null)
                {
                    return;
                }

                _editorItem.VitalRegen[(int)vital] = (long)regenNumeric.Value;
            };

            regenPanel.Controls.Add(regenLabel);
            regenPanel.Controls.Add(regenNumeric);
            flpVitalRegen.Controls.Add(regenPanel);
            _vitalRegenControls.Add(vital, regenNumeric);
        }
    }

    private void InitImmunityControls()
    {
        flpImmunities.Controls.Clear();
        _immunityCheckboxes.Clear();

        foreach (var effect in Enum.GetValues<SpellEffect>())
        {
            if (effect == SpellEffect.None)
            {
                continue;
            }

            var checkbox = new DarkCheckBox
            {
                Text = Strings.NpcEditor.Immunities.ContainsKey(effect)
                    ? Strings.NpcEditor.Immunities[effect]
                    : effect.ToString(),
                AutoSize = true
            };

            checkbox.CheckedChanged += (_, _) =>
            {
                if (_editorItem == null)
                {
                    return;
                }

                if (checkbox.Checked)
                {
                    if (!_editorItem.Immunities.Contains(effect))
                    {
                        _editorItem.Immunities.Add(effect);
                    }
                }
                else
                {
                    _editorItem.Immunities.Remove(effect);
                }
            };

            flpImmunities.Controls.Add(checkbox);
            _immunityCheckboxes.Add(effect, checkbox);
        }
    }

    private void AssignEditorItem(Guid id)
    {
        _editorItem = PetDescriptor.Get(id);
        UpdateEditor();
    }

    protected override void GameObjectUpdatedDelegate(GameObjectType type)
    {
        if (type == GameObjectType.Pet)
        {
            InitEditor();
            if (_editorItem != null && !PetDescriptor.Lookup.Values.Contains(_editorItem))
            {
                _editorItem = null;
                UpdateEditor();
            }
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (_isClosing)
        {
            return;
        }

        _isClosing = true;
        foreach (var item in _changed)
        {
            item.Immunities.Sort();
            PacketSender.SendSaveObject(item);
            item.DeleteBackup();
        }

        Hide();
        Globals.CurrentEditor = -1;
        Dispose();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (_isClosing)
        {
            return;
        }

        _isClosing = true;
        foreach (var item in _changed)
        {
            item.RestoreBackup();
            item.DeleteBackup();
        }

        Hide();
        Globals.CurrentEditor = -1;
        Dispose();
    }

    private void frmPet_FormClosed(object sender, FormClosedEventArgs e)
    {
        btnCancel_Click(null, EventArgs.Empty);
    }

    private void UpdateEditor()
    {
        if (_editorItem != null)
        {
            pnlContainer.Show();
            txtName.Text = _editorItem.Name;
            cmbFolder.Text = _editorItem.Folder;
            var spriteIndex = cmbSprite.FindString(TextUtils.NullToNone(_editorItem.Sprite));
            if (spriteIndex >= 0)
            {
                cmbSprite.SelectedIndex = spriteIndex;
            }
            else if (cmbSprite.Items.Count > 0)
            {
                cmbSprite.SelectedIndex = 0;
            }
            nudLevel.Value = Math.Max(nudLevel.Minimum, Math.Min(nudLevel.Maximum, _editorItem.Level));
            nudExperience.Value = Math.Max(nudExperience.Minimum, Math.Min(nudExperience.Maximum, _editorItem.Experience));
            SetComboIndex(cmbAttackAnimation, AnimationDescriptor.ListIndex(_editorItem.AttackAnimationId) + 1, 0);
            SetComboIndex(cmbDeathAnimation, AnimationDescriptor.ListIndex(_editorItem.DeathAnimationId) + 1, 0);
            SetComboIndex(cmbIdleAnimation, AnimationDescriptor.ListIndex(_editorItem.IdleAnimationId) + 1, 0);
            SetComboIndex(cmbDamageType, _editorItem.DamageType);
            SetComboIndex(cmbScalingStat, _editorItem.ScalingStat);
            nudScaling.Value = Math.Max(nudScaling.Minimum, Math.Min(nudScaling.Maximum, _editorItem.Scaling));
            nudDamage.Value = Math.Max(nudDamage.Minimum, Math.Min(nudDamage.Maximum, _editorItem.Damage));
            nudCritChance.Value = Math.Max(nudCritChance.Minimum, Math.Min(nudCritChance.Maximum, _editorItem.CritChance));
            nudCritMultiplier.Value = Math.Max(nudCritMultiplier.Minimum, Math.Min(nudCritMultiplier.Maximum, (decimal)_editorItem.CritMultiplier));
            SetComboIndex(cmbAttackSpeedModifier, _editorItem.AttackSpeedModifier);
            nudAttackSpeedValue.Value = Math.Max(nudAttackSpeedValue.Minimum, Math.Min(nudAttackSpeedValue.Maximum, _editorItem.AttackSpeedValue));
            nudTenacity.Value = (decimal)_editorItem.Tenacity;

            foreach (var (stat, control) in _statControls)
            {
                control.Value = Math.Max(control.Minimum, Math.Min(control.Maximum, _editorItem.Stats[(int)stat]));
            }

            foreach (var (vital, control) in _vitalControls)
            {
                control.Value = Math.Max(control.Minimum, Math.Min(control.Maximum, _editorItem.MaxVitals[(int)vital]));
            }

            foreach (var (vital, control) in _vitalRegenControls)
            {
                control.Value = Math.Max(control.Minimum, Math.Min(control.Maximum, _editorItem.VitalRegen[(int)vital]));
            }

            foreach (var (effect, checkbox) in _immunityCheckboxes)
            {
                checkbox.Checked = _editorItem.Immunities.Contains(effect);
            }

            _spellNames.RaiseListChangedEvents = false;
            _spellNames.Clear();
            foreach (var spellId in _editorItem.Spells)
            {
                _spellNames.Add(spellId != Guid.Empty ? SpellDescriptor.GetName(spellId) : Strings.General.None);
            }

            _spellNames.RaiseListChangedEvents = true;
            _spellNames.ResetBindings();

            if (_editorItem.Spells.Count > 0)
            {
                lstSpells.SelectedIndex = 0;
                cmbSpell.SelectedIndex = SpellDescriptor.ListIndex(_editorItem.Spells[0]);
            }
            else if (cmbSpell.Items.Count > 0)
            {
                cmbSpell.SelectedIndex = 0;
            }

            if (!_changed.Contains(_editorItem))
            {
                _changed.Add(_editorItem);
                _editorItem.MakeBackup();
            }
        }
        else
        {
            pnlContainer.Hide();
        }

        var hasItem = _editorItem != null;
        UpdateEditorButtons(hasItem);
        UpdateToolStripItems();
    }

    private void txtName_TextChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Name = txtName.Text;
        lstGameObjects.UpdateText(txtName.Text);
    }

    private void cmbSprite_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Sprite = TextUtils.SanitizeNone(cmbSprite.Text);
    }

    private void nudLevel_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Level = (int)nudLevel.Value;
    }

    private void nudExperience_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Experience = (long)nudExperience.Value;
    }

    private void cmbAttackAnimation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.AttackAnimation = AnimationDescriptor.Get(AnimationDescriptor.IdFromList(cmbAttackAnimation.SelectedIndex - 1));
    }

    private void cmbDeathAnimation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.DeathAnimation = AnimationDescriptor.Get(AnimationDescriptor.IdFromList(cmbDeathAnimation.SelectedIndex - 1));
    }

    private void cmbIdleAnimation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.IdleAnimation = AnimationDescriptor.Get(AnimationDescriptor.IdFromList(cmbIdleAnimation.SelectedIndex - 1));
    }

    private void cmbDamageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.DamageType = cmbDamageType.SelectedIndex;
    }

    private void cmbScalingStat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.ScalingStat = cmbScalingStat.SelectedIndex;
    }

    private void nudScaling_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Scaling = (int)nudScaling.Value;
    }

    private void nudDamage_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Damage = (int)nudDamage.Value;
    }

    private void nudCritChance_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.CritChance = (int)nudCritChance.Value;
    }

    private void nudCritMultiplier_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.CritMultiplier = (double)nudCritMultiplier.Value;
    }

    private void cmbAttackSpeedModifier_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.AttackSpeedModifier = cmbAttackSpeedModifier.SelectedIndex;
    }

    private void nudAttackSpeedValue_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.AttackSpeedValue = (int)nudAttackSpeedValue.Value;
    }

    private void nudTenacity_ValueChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Tenacity = (double)nudTenacity.Value;
    }

    private void cmbSpell_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null || lstSpells.SelectedIndex < 0)
        {
            return;
        }

        if (lstSpells.SelectedIndex >= _editorItem.Spells.Count)
        {
            return;
        }

        var selectedSpell = SpellDescriptor.IdFromList(cmbSpell.SelectedIndex);
        _editorItem.Spells[lstSpells.SelectedIndex] = selectedSpell;
        var displayName = selectedSpell != Guid.Empty ? SpellDescriptor.GetName(selectedSpell) : Strings.General.None.ToString();
        _spellNames[lstSpells.SelectedIndex] = displayName;
        _spellNames.ResetItem(lstSpells.SelectedIndex);
    }

    private void lstSpells_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        if (lstSpells.SelectedIndex < 0 || lstSpells.SelectedIndex >= _editorItem.Spells.Count)
        {
            return;
        }

        cmbSpell.SelectedIndex = SpellDescriptor.ListIndex(_editorItem.Spells[lstSpells.SelectedIndex]);
    }

    private void btnAddSpell_Click(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        if (cmbSpell.SelectedIndex < 0)
        {
            return;
        }

        var spellId = SpellDescriptor.IdFromList(cmbSpell.SelectedIndex);
        _editorItem.Spells.Add(spellId);
        _spellNames.Add(spellId != Guid.Empty ? SpellDescriptor.GetName(spellId) : Strings.General.None);
    }

    private void btnRemoveSpell_Click(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        if (lstSpells.SelectedIndex < 0 || lstSpells.SelectedIndex >= _editorItem.Spells.Count)
        {
            return;
        }

        var index = lstSpells.SelectedIndex;
        _editorItem.Spells.RemoveAt(index);
        _spellNames.RemoveAt(index);
    }

    private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        _editorItem.Folder = cmbFolder.Text;
        InitEditor();
    }

    private void btnAddFolder_Click(object sender, EventArgs e)
    {
        if (_editorItem == null)
        {
            return;
        }

        var folderName = string.Empty;
        var result = DarkInputBox.ShowInformation(
            Strings.Pets.folderprompt,
            Strings.Pets.foldertitle,
            ref folderName,
            DarkDialogButton.OkCancel
        );

        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderName))
        {
            if (!cmbFolder.Items.Contains(folderName))
            {
                _editorItem.Folder = folderName;
                lstGameObjects.UpdateText(folderName);
                InitEditor();
                cmbFolder.Text = folderName;
            }
        }
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
            txtSearch.Text = Strings.Pets.searchplaceholder;
        }
    }

    private void txtSearch_Enter(object sender, EventArgs e)
    {
        txtSearch.SelectAll();
    }

    private void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Text = Strings.Pets.searchplaceholder;
        InitEditor();
    }

    private bool CustomSearch()
    {
        return !string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != Strings.Pets.searchplaceholder;
    }

    public void InitEditor()
    {
        var folders = new List<string>();
        foreach (var descriptor in PetDescriptor.Lookup)
        {
            if (descriptor.Value is not PetDescriptor pet)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(pet.Folder) && !folders.Contains(pet.Folder))
            {
                folders.Add(pet.Folder);
                if (!_knownFolders.Contains(pet.Folder))
                {
                    _knownFolders.Add(pet.Folder);
                }
            }
        }

        folders.Sort();
        _knownFolders.Sort();

        cmbFolder.Items.Clear();
        cmbFolder.Items.Add(string.Empty);
        cmbFolder.Items.AddRange(_knownFolders.Cast<object>().ToArray());

        var items = PetDescriptor.Lookup
            .OrderBy(p => p.Value?.Name)
            .Select(
                pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(
                    pair.Key,
                    new KeyValuePair<string, string>(
                        ((PetDescriptor)pair.Value)?.Name ?? Models.DatabaseObject<PetDescriptor>.Deleted,
                        ((PetDescriptor)pair.Value)?.Folder ?? string.Empty
                    )
                )
            )
            .ToArray();

        lstGameObjects.Repopulate(items, folders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
    }

    private void toolStripItemNew_Click(object sender, EventArgs e)
    {
        PacketSender.SendCreateObject(GameObjectType.Pet);
    }

    private void toolStripItemDelete_Click(object sender, EventArgs e)
    {
        if (_editorItem == null || !lstGameObjects.Focused)
        {
            return;
        }

        if (DarkMessageBox.ShowWarning(
                Strings.Pets.deleteprompt,
                Strings.Pets.deletetitle,
                DarkDialogButton.YesNo,
                Icon
            ) == DialogResult.Yes)
        {
            PacketSender.SendDeleteObject(_editorItem);
        }
    }

    private void toolStripItemCopy_Click(object sender, EventArgs e)
    {
        if (_editorItem == null || !lstGameObjects.Focused)
        {
            return;
        }

        _copiedItem = _editorItem.JsonData;
        toolStripItemPaste.Enabled = true;
    }

    private void toolStripItemPaste_Click(object sender, EventArgs e)
    {
        if (_editorItem == null || _copiedItem == null || !lstGameObjects.Focused)
        {
            return;
        }

        _editorItem.Load(_copiedItem, true);
        UpdateEditor();
    }

    private void toolStripItemUndo_Click(object sender, EventArgs e)
    {
        if (_editorItem == null || !_changed.Contains(_editorItem))
        {
            return;
        }

        if (DarkMessageBox.ShowWarning(
                Strings.Pets.undoprompt,
                Strings.Pets.undotitle,
                DarkDialogButton.YesNo,
                Icon
            ) == DialogResult.Yes)
        {
            _editorItem.RestoreBackup();
            UpdateEditor();
        }
    }

    private void UpdateToolStripItems()
    {
        toolStripItemCopy.Enabled = _editorItem != null && lstGameObjects.Focused;
        toolStripItemPaste.Enabled = _editorItem != null && _copiedItem != null && lstGameObjects.Focused;
        toolStripItemDelete.Enabled = _editorItem != null && lstGameObjects.Focused;
        toolStripItemUndo.Enabled = _editorItem != null && lstGameObjects.Focused;
    }

    private void form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            toolStripItemNew_Click(sender, e);
        }
    }

    private static void SetComboIndex(DarkComboBox combo, int index, int fallbackIndex = 0)
    {
        if (combo.Items.Count == 0)
        {
            combo.SelectedIndex = -1;
            return;
        }

        if (index < 0 || index >= combo.Items.Count)
        {
            index = Math.Min(Math.Max(fallbackIndex, 0), combo.Items.Count - 1);
        }

        combo.SelectedIndex = index;
    }
}
