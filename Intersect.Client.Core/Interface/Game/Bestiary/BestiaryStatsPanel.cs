using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Intersect.Client.Interface.Game.Bestiary;

public class BestiaryStatsPanel : Base
{
    private readonly Label _levelLabel;
    private readonly Label _healthLabel;
    private readonly Label _manaLabel;
    private readonly Label _minDamageLabel;
    private readonly Label _maxDamageLabel;

    private readonly List<Label> _statLabels = new();

    public BestiaryStatsPanel(Base parent) : base(parent, nameof(BestiaryStatsPanel))
    {
        SetSize(300, 300);

        _levelLabel = CreateLabel("LevelLabel");
        _healthLabel = CreateLabel("HealthLabel");
        _manaLabel = CreateLabel("ManaLabel");
        _minDamageLabel = CreateLabel("MinDamageLabel");
        _maxDamageLabel = CreateLabel("MaxDamageLabel");

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    private static void ApplyStyle(Label lbl)
    {
        lbl.FontSize = 10;
        lbl.TextColorOverride = Color.White;
        lbl.AutoSizeToContents = true;
    }

    private Label CreateLabel(string name)
    {
        var lbl = new Label(this, name);
        ApplyStyle(lbl);
        return lbl;
    }

    private void ClearStatLabels()
    {
        foreach (var lbl in _statLabels)
        {
            lbl.Delete();
        }
        _statLabels.Clear();
    }

    public void UpdateData(NPCDescriptor? desc)
    {
        ClearStatLabels();

        if (desc == null)
        {
            SetSize(300, 0);
            return;
        }

        int yOffset = 0;

        var maxStatIndex = (int)Stat.Cures;
        for (int i = 0; i <= maxStatIndex; i++)
        {
            var stat = (Stat)i;
            if (!desc.StatsLookup.TryGetValue(stat, out var value) || value <= 0)
            {
                continue;
            }

            var lbl = CreateLabel($"Stat_{i}");
            lbl.Text = $"{Strings.ItemDescription.StatCounts[i]}: {value}";
            lbl.SetPosition(0, yOffset);
            yOffset += lbl.Height + 4;
            _statLabels.Add(lbl);
        }

        _levelLabel.Text = $"Nivel: {desc.Level}";
        _levelLabel.SetPosition(0, yOffset); yOffset += _levelLabel.Height + 4;

        if (desc.MaxVitalsLookup.TryGetValue(Vital.Health, out var health))
        {
            _healthLabel.Text = $"Vida: {health}";
            _healthLabel.SetPosition(0, yOffset); yOffset += _healthLabel.Height + 4;
        }

        if (desc.MaxVitalsLookup.TryGetValue(Vital.Mana, out var mana))
        {
            _manaLabel.Text = $"Mana: {mana}";
            _manaLabel.SetPosition(0, yOffset); yOffset += _manaLabel.Height + 4;
        }

        var scalingValue = desc.Stats.Length > desc.ScalingStat ? desc.Stats[desc.ScalingStat] : 0;
        var baseDamage = desc.Damage + scalingValue * (desc.Scaling / 100f);
        var minAttack = (int)Math.Round(baseDamage * 0.975f);
        var maxAttack = (int)Math.Round(baseDamage * 1.025f);

        _minDamageLabel.Text = $"Daño Mínimo: {minAttack}";
        _minDamageLabel.SetPosition(0, yOffset); yOffset += _minDamageLabel.Height + 4;

        _maxDamageLabel.Text = $"Daño Máximo: {maxAttack}";
        _maxDamageLabel.SetPosition(0, yOffset); yOffset += _maxDamageLabel.Height + 4;

        SetSize(300, yOffset);
    }

}
