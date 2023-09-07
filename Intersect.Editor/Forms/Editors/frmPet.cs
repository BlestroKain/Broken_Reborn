using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;

using Intersect.Editor.Content;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors
{

    public partial class FrmPet : EditorForm
    {

        private List<PetBase> mChanged = new List<PetBase>();

        private string mCopiedItem;

        private PetBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        public FrmPet()
        {
            ApplyHooks();
            InitializeComponent();
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }
        private void AssignEditorItem(Guid id)
        {
            mEditorItem = PetBase.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.Pet)
            {
                InitEditor();
                if (mEditorItem != null && !PetBase.Lookup.Values.Contains(mEditorItem))
                {
                    mEditorItem = null;
                    UpdateEditor();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Send Changed items
            foreach (var item in mChanged)
            {
                // Sort immunities to keep change checker consistent
                item.Immunities.Sort();

                PacketSender.SendSaveObject(item);
                item.DeleteBackup();
            }

            Hide();
            Globals.CurrentEditor = -1;
            Dispose();
        }

        private void frmPet_Load(object sender, EventArgs e)
        {
            cmbSprite.Items.Clear();
            cmbSprite.Items.Add(Strings.General.None);
            cmbSprite.Items.AddRange(
                GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Entity)
            );

            cmbSpell.Items.Clear();
            cmbSpell.Items.AddRange(SpellBase.Names);
            cmbAttackAnimation.Items.Clear();
            cmbAttackAnimation.Items.Add(Strings.General.None);
            cmbAttackAnimation.Items.AddRange(AnimationBase.Names);
            cmbScalingStat.Items.Clear();
            for (var x = 0; x < (int)Stat.StatCount; x++)
            {
                cmbScalingStat.Items.Add(Globals.GetStatName(x));
            }

            nudStr.Maximum = Options.MaxStatValue;
            nudMag.Maximum = Options.MaxStatValue;
            nudDef.Maximum = Options.MaxStatValue;
            nudMR.Maximum = Options.MaxStatValue;
            nudSpd.Maximum = Options.MaxStatValue;

            cmbDeathAnimation.Items.Clear();
            cmbDeathAnimation.Items.Add(Strings.General.None);
            cmbDeathAnimation.Items.AddRange(AnimationBase.Names);

            InitLocalization();
            UpdateEditor();
        }

        private void InitLocalization()
        {
            Text = Strings.PetEditor.title;
            toolStripItemNew.Text = Strings.PetEditor.New;
            toolStripItemDelete.Text = Strings.PetEditor.delete;
            toolStripItemCopy.Text = Strings.PetEditor.copy;
            toolStripItemPaste.Text = Strings.PetEditor.paste;
            toolStripItemUndo.Text = Strings.PetEditor.undo;

            grpPets.Text = Strings.PetEditor.pets;

            grpGeneral.Text = Strings.PetEditor.general;
            lblName.Text = Strings.PetEditor.name;
            lblPic.Text = Strings.PetEditor.sprite;
                  
            grpStats.Text = Strings.PetEditor.stats;
            lblHP.Text = Strings.PetEditor.hp;
            lblMana.Text = Strings.PetEditor.mana;
            lblStr.Text = Strings.PetEditor.attack;
            lblDef.Text = Strings.PetEditor.defense;
            lblSpd.Text = Strings.PetEditor.speed;
            lblMag.Text = Strings.PetEditor.abilitypower;
            lblMR.Text = Strings.PetEditor.magicresist;
          

            grpRegen.Text = Strings.PetEditor.regen;
            lblHpRegen.Text = Strings.PetEditor.hpregen;
            lblManaRegen.Text = Strings.PetEditor.mpregen;
            lblRegenHint.Text = Strings.PetEditor.regenhint;

            grpAttackSpeed.Text = Strings.PetEditor.attackspeed;
            lblAttackSpeedModifier.Text = Strings.PetEditor.attackspeedmodifier;
            lblAttackSpeedValue.Text = Strings.PetEditor.attackspeedvalue;
            cmbAttackSpeedModifier.Items.Clear();
            foreach (var val in Strings.PetEditor.attackspeedmodifiers.Values)
            {
                cmbAttackSpeedModifier.Items.Add(val.ToString());
            }
                    
            grpCombat.Text = Strings.PetEditor.combat;
            lblDamage.Text = Strings.PetEditor.basedamage;
            lblCritChance.Text = Strings.PetEditor.critchance;
            lblCritMultiplier.Text = Strings.PetEditor.critmultiplier;
            lblDamageType.Text = Strings.PetEditor.damagetype;
            cmbDamageType.Items.Clear();
            for (var i = 0; i < Strings.Combat.damagetypes.Count; i++)
            {
                cmbDamageType.Items.Add(Strings.Combat.damagetypes[i]);
            }

            lblScalingStat.Text = Strings.PetEditor.scalingstat;
            lblScaling.Text = Strings.PetEditor.scalingamount;
            lblAttackAnimation.Text = Strings.PetEditor.attackanimation;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.PetEditor.sortalphabetically;
            txtSearch.Text = Strings.PetEditor.searchplaceholder;
            lblFolder.Text = Strings.PetEditor.folderlabel;

            grpImmunities.Text = Strings.PetEditor.ImmunitiesTitle;
            chkKnockback.Text = Strings.PetEditor.Immunities[SpellEffect.Knockback];
            chkSilence.Text = Strings.PetEditor.Immunities[SpellEffect.Silence];
            chkStun.Text = Strings.PetEditor.Immunities[SpellEffect.Stun];
            chkSnare.Text = Strings.PetEditor.Immunities[SpellEffect.Snare];
            chkBlind.Text = Strings.PetEditor.Immunities[SpellEffect.Blind];
            chkTransform.Text = Strings.PetEditor.Immunities[SpellEffect.Transform];
            chkTaunt.Text = Strings.PetEditor.Immunities[SpellEffect.Taunt];
            chkSleep.Text = Strings.PetEditor.Immunities[SpellEffect.Sleep];
            grpAnimation.Text = Strings.PetEditor.additionalanimationgroup;
            lblDeathAnimation.Text = Strings.PetEditor.deathanimation;
            btnSave.Text = Strings.PetEditor.save;
            btnCancel.Text = Strings.PetEditor.cancel;
        }

        private void UpdateEditor()
        {
            if (mEditorItem != null)
            {
                pnlContainer.Show();

                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                cmbSprite.SelectedIndex = cmbSprite.FindString(TextUtils.NullToNone(mEditorItem.Sprite));
                                  
                nudStr.Value = mEditorItem.Stats[(int) Stat.Attack];
                nudMag.Value = mEditorItem.Stats[(int) Stat.Intelligence];
                nudDef.Value = mEditorItem.Stats[(int) Stat.Defense];
                nudMR.Value = mEditorItem.Stats[(int) Stat.Vitality];
                nudSpd.Value = mEditorItem.Stats[(int) Stat.Speed];
                nudAgi.Value = mEditorItem.Stats[(int)Stat.Agility];
                nudHp.Value = mEditorItem.MaxVital[(int) Vital.Health];
                nudMana.Value = mEditorItem.MaxVital[(int) Vital.Mana];
                   

                //Combat
                nudDamage.Value = mEditorItem.Damage;
                nudCritChance.Value = mEditorItem.CritChance;
                nudCritMultiplier.Value = (decimal) mEditorItem.CritMultiplier;
                nudScaling.Value = mEditorItem.Scaling;
                cmbDamageType.SelectedIndex = (int)mEditorItem.DamageType;
                cmbScalingStat.SelectedIndex = (int)mEditorItem.ScalingStat;
                cmbAttackAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.AttackAnimationId) + 1;
                cmbAttackSpeedModifier.SelectedIndex = mEditorItem.AttackSpeedModifier;
                nudAttackSpeedValue.Value = mEditorItem.AttackSpeedValue;

                //Regen
                nudHpRegen.Value = mEditorItem.VitalRegen[(int) Vital.Health];
                nudMpRegen.Value = mEditorItem.VitalRegen[(int) Vital.Mana];

                // Add the spells to the list
                lstSpells.Items.Clear();
                for (var i = 0; i < mEditorItem.Spells.Count; i++)
                {
                    if (mEditorItem.Spells[i] != Guid.Empty)
                    {
                        lstSpells.Items.Add(SpellBase.GetName(mEditorItem.Spells[i]));
                    }
                    else
                    {
                        lstSpells.Items.Add(Strings.General.None);
                    }
                }

                if (lstSpells.Items.Count > 0)
                {
                    lstSpells.SelectedIndex = 0;
                    cmbSpell.SelectedIndex = SpellBase.ListIndex(mEditorItem.Spells[lstSpells.SelectedIndex]);
                }
                                             
                DrawPetSprite();
                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }
            

                UpdateImmunities();

                cmbDeathAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.DeathAnimationId) + 1;
            }
            else
            {
                pnlContainer.Hide();
            }

            UpdateToolStripItems();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
        }

        private void cmbSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Sprite = TextUtils.SanitizeNone(cmbSprite.Text);
            DrawPetSprite();
        }

        private void DrawPetSprite()
        {
            var picSpriteBmp = new Bitmap(picPet.Width, picPet.Height);
            var gfx = Graphics.FromImage(picSpriteBmp);
            gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, picPet.Width, picPet.Height));
            if (cmbSprite.SelectedIndex > 0)
            {
                var img = Image.FromFile("resources/entities/" + cmbSprite.Text);
                var imgAttributes = new ImageAttributes();
                                
                gfx.DrawImage(
                    img, new Rectangle(0, 0, img.Width / Options.Instance.Sprites.NormalFrames, img.Height / Options.Instance.Sprites.Directions),
                    0, 0, img.Width / Options.Instance.Sprites.NormalFrames, img.Height / Options.Instance.Sprites.Directions, GraphicsUnit.Pixel, imgAttributes
                );

                img.Dispose();
                imgAttributes.Dispose();
            }

            gfx.Dispose();

            picPet.BackgroundImage = picSpriteBmp;
        }

        private void frmPet_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.CurrentEditor = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mEditorItem.Spells.Add(SpellBase.IdFromList(cmbSpell.SelectedIndex));
            var n = lstSpells.SelectedIndex;
            lstSpells.Items.Clear();
            for (var i = 0; i < mEditorItem.Spells.Count; i++)
            {
                lstSpells.Items.Add(SpellBase.GetName(mEditorItem.Spells[i]));
            }

            lstSpells.SelectedIndex = n;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex > -1)
            {
                var i = lstSpells.SelectedIndex;
                lstSpells.Items.RemoveAt(i);
                mEditorItem.Spells.RemoveAt(i);
            }
        }
        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.Pet);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.PetEditor.deleteprompt, Strings.PetEditor.deletetitle, DarkDialogButton.YesNo,
                        Icon
                    ) ==
                    DialogResult.Yes)
                {
                    PacketSender.SendDeleteObject(mEditorItem);
                }
            }
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                mCopiedItem = mEditorItem.JsonData;
                toolStripItemPaste.Enabled = true;
            }
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused)
            {
                mEditorItem.Load(mCopiedItem, true);
                UpdateEditor();
            }
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            if (mChanged.Contains(mEditorItem) && mEditorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.PetEditor.undoprompt, Strings.PetEditor.undotitle, DarkDialogButton.YesNo,
                        Icon
                    ) ==
                    DialogResult.Yes)
                {
                    mEditorItem.RestoreBackup();
                    UpdateEditor();
                }
            }
        }

        private void UpdateToolStripItems()
        {
            toolStripItemCopy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemPaste.Enabled = mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused;
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        private void UpdateImmunities()
        {
            chkKnockback.Checked = mEditorItem.Immunities.Contains(SpellEffect.Knockback);
            chkSilence.Checked = mEditorItem.Immunities.Contains(SpellEffect.Silence);
            chkSnare.Checked = mEditorItem.Immunities.Contains(SpellEffect.Snare);
            chkStun.Checked = mEditorItem.Immunities.Contains(SpellEffect.Stun);
            chkSleep.Checked = mEditorItem.Immunities.Contains(SpellEffect.Sleep);
            chkTransform.Checked = mEditorItem.Immunities.Contains(SpellEffect.Transform);
            chkTaunt.Checked = mEditorItem.Immunities.Contains(SpellEffect.Taunt);
            chkBlind.Checked = mEditorItem.Immunities.Contains(SpellEffect.Blind);
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

        private void cmbAttackAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackAnimation =
                AnimationBase.Get(AnimationBase.IdFromList(cmbAttackAnimation.SelectedIndex - 1));
        }

        private void cmbDamageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.DamageType = (DamageType)cmbDamageType.SelectedIndex;
        }

        private void cmbScalingStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ScalingStat = (Stat)cmbScalingStat.SelectedIndex;
        }

        private void lstSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex > -1)
            {
                cmbSpell.SelectedIndex = SpellBase.ListIndex(mEditorItem.Spells[lstSpells.SelectedIndex]);
            }
        }

        private void cmbSpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex > -1 && lstSpells.SelectedIndex < mEditorItem.Spells.Count)
            {
                mEditorItem.Spells[lstSpells.SelectedIndex] = SpellBase.IdFromList(cmbSpell.SelectedIndex);
            }

            var n = lstSpells.SelectedIndex;
            lstSpells.Items.Clear();
            for (var i = 0; i < mEditorItem.Spells.Count; i++)
            {
                lstSpells.Items.Add(SpellBase.GetName(mEditorItem.Spells[i]));
            }

            lstSpells.SelectedIndex = n;
        }

        private void nudScaling_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Scaling = (int) nudScaling.Value;
        }

        private void nudStr_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stat.Attack] = (int) nudStr.Value;
        }

        private void nudMag_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stat.Intelligence] = (int) nudMag.Value;
        }

        private void nudDef_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stat.Defense] = (int) nudDef.Value;
        }

        private void nudMR_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stat.MagicResist] = (int) nudMR.Value;
        }

        private void nudSpd_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stat.Speed] = (int) nudSpd.Value;
        }

        private void nudDamage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Damage = (int) nudDamage.Value;
        }

        private void nudCritChance_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CritChance = (int) nudCritChance.Value;
        }

        private void nudHp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MaxVital[(int) Vital.Health] = (int) nudHp.Value;
        }

        private void nudMana_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MaxVital[(int) Vital.Mana] = (int) nudMana.Value;
        }
                     
        private void nudLevel_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Level = (int) nudLevel.Value;
        }

        private void nudHpRegen_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalRegen[(int) Vital.Health] = (int) nudHpRegen.Value;
        }

        private void nudMpRegen_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalRegen[(int) Vital.Mana] = (int) nudMpRegen.Value;
        }
                
       
       
        private void nudCritMultiplier_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CritMultiplier = (double) nudCritMultiplier.Value;
        }

        private void cmbAttackSpeedModifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackSpeedModifier = cmbAttackSpeedModifier.SelectedIndex;
            nudAttackSpeedValue.Enabled = cmbAttackSpeedModifier.SelectedIndex > 0;
        }

        private void nudAttackSpeedValue_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackSpeedValue = (int) nudAttackSpeedValue.Value;
        }

      

        #region "Item List - Folders, Searching, Sorting, Etc"

        public void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in PetBase.Lookup)
            {
                if (!string.IsNullOrEmpty(((PetBase) itm.Value).Folder) &&
                    !mFolders.Contains(((PetBase) itm.Value).Folder))
                {
                    mFolders.Add(((PetBase) itm.Value).Folder);
                    if (!mKnownFolders.Contains(((PetBase) itm.Value).Folder))
                    {
                        mKnownFolders.Add(((PetBase) itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            var items = PetBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((PetBase)pair.Value)?.Name ?? Models.DatabaseObject<PetBase>.Deleted, ((PetBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.PetEditor.folderprompt, Strings.PetEditor.foldertitle, ref folderName, DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
            {
                if (!cmbFolder.Items.Contains(folderName))
                {
                    mEditorItem.Folder = folderName;
                    lstGameObjects.UpdateText(folderName);
                    InitEditor();
                    cmbFolder.Text = folderName;
                }
            }
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Folder = cmbFolder.Text;
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
                txtSearch.Text = Strings.PetEditor.searchplaceholder;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.PetEditor.searchplaceholder;
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != Strings.PetEditor.searchplaceholder;
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == Strings.PetEditor.searchplaceholder)
            {
                txtSearch.SelectAll();
            }
        }

        #endregion

        private void ChangeImmunity(SpellEffect status, bool isImmune)
        {
            if (isImmune && !mEditorItem.Immunities.Contains(status))
            {
                mEditorItem.Immunities.Add(status);
            }
            else if (!isImmune)
            {
                mEditorItem.Immunities.Remove(status);
            }
        }

        private void chkKnockback_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Knockback, chkKnockback.Checked);
        }

        private void chkSilence_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Silence, chkSilence.Checked);
        }

        private void chkStun_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Stun, chkStun.Checked);
        }

        private void chkSnare_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Snare, chkSnare.Checked);
        }

        private void chkBlind_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Blind, chkBlind.Checked);
        }

        private void chkTransform_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Transform, chkTransform.Checked);
        }

        private void chkSleep_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Sleep, chkSleep.Checked);
        }

        private void chkTaunt_CheckedChanged(object sender, EventArgs e)
        {
            ChangeImmunity(SpellEffect.Taunt, chkTaunt.Checked);
        }
        private void cmbDeathAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.DeathAnimation =
                AnimationBase.Get(AnimationBase.IdFromList(cmbDeathAnimation.SelectedIndex - 1));
        }

        private void cmbDeathAnimation_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            mEditorItem.DeathAnimation =
                AnimationBase.Get(AnimationBase.IdFromList(cmbDeathAnimation.SelectedIndex - 1));
        }

        private void nudAgi_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stat.Agility] = (int)nudAgi.Value;
        }
    }

}
