using System;
using DarkUI.Controls;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Editor.Core;

namespace Intersect.Editor.Forms.Editors;

public partial class FrmPrismOptions : Form
{
    public FrmPrismOptions()
    {
        InitializeComponent();
        Icon = Program.Icon;

        nudBaseHp.Value = Options.Instance.Prism.BaseHp;
        nudHpPerLevel.Value = Options.Instance.Prism.HpPerLevel;
        nudMaturationSeconds.Value = Options.Instance.Prism.MaturationSeconds;
        nudAttackCooldown.Value = Options.Instance.Prism.AttackCooldownSeconds;
        nudDamageCapPerTick.Value = Options.Instance.Prism.DamageCapPerTick;
        nudSchedulerIntervalSeconds.Value = Options.Instance.Prism.SchedulerIntervalSeconds;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        Options.Instance.Prism.BaseHp = (int)nudBaseHp.Value;
        Options.Instance.Prism.HpPerLevel = (int)nudHpPerLevel.Value;
        Options.Instance.Prism.MaturationSeconds = (int)nudMaturationSeconds.Value;
        Options.Instance.Prism.AttackCooldownSeconds = (int)nudAttackCooldown.Value;
        Options.Instance.Prism.DamageCapPerTick = (int)nudDamageCapPerTick.Value;
        Options.Instance.Prism.SchedulerIntervalSeconds = (int)nudSchedulerIntervalSeconds.Value;
        Options.SaveToDisk();
        Close();
    }
}
