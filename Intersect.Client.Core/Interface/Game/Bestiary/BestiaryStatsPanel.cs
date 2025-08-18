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
using System.Linq;

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
        // Puedes usar LoadJsonUi si defines el layout visual en JSON
        SetSize(300, 300);

        _levelLabel = new Label(this, "LevelLabel");
        _healthLabel = new Label(this, "HealthLabel");
        _manaLabel = new Label(this, "ManaLabel");
        _minDamageLabel = new Label(this, "MinDamageLabel");
        _maxDamageLabel = new Label(this, "MaxDamageLabel");

        // Crear 6 labels para stats base (Fuerza, Defensa, etc.)
        for (int i = 0; i < (int)Stat.Cures+1; i++)
        {
            var lbl = new Label(this, $"Stat_{i}");
            _statLabels.Add(lbl);
        }

        // Estilo base
        foreach (var lbl in _statLabels.Append(_levelLabel).Append(_healthLabel).Append(_manaLabel).Append(_minDamageLabel).Append(_maxDamageLabel))
        {
            lbl.FontSize = 10;
            lbl.TextColorOverride = Color.White;
            lbl.AutoSizeToContents = true;
        }
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    
    }

    public void UpdateData(NPCDescriptor? desc)
    {
        if (desc == null)
        {
            Hide();
            return;
        }

        int yOffset = 0;

        // Aseguramos que todas las etiquetas comiencen ocultas para evitar
        // que queden escondidas al cambiar entre distintos NPCs.
        foreach (var lbl in _statLabels)
        {
            lbl.Hide();
        }

        for (int i = 0; i < _statLabels.Count; i++)
        {
            var stat = (Stat)i;
            var value = desc.StatsLookup.TryGetValue(stat, out var v) ? v : 0;

            var lbl = _statLabels[i];
            lbl.Text = $"{Strings.ItemDescription.StatCounts[i]}: {value}";

            if (value > 0)
            {
                lbl.SetPosition(0, yOffset);
                lbl.Show();
                yOffset += lbl.Height + 4;
            }
        }

        _levelLabel.Text = $"Nivel: {desc.Level}";
        _levelLabel.SetPosition(0, yOffset);
        yOffset += _levelLabel.Height + 4;

        var health = desc.MaxVitalsLookup.TryGetValue(Vital.Health, out var h) ? h : 0;
        _healthLabel.Text = $"Vida: {health}";
        _healthLabel.SetPosition(0, yOffset);
        yOffset += _healthLabel.Height + 4;

        var mana = desc.MaxVitalsLookup.TryGetValue(Vital.Mana, out var m) ? m : 0;
        _manaLabel.Text = $"Mana: {mana}";
        _manaLabel.SetPosition(0, yOffset);
        yOffset += _manaLabel.Height + 4;

        var scalingValue = desc.Stats.Length > desc.ScalingStat ? desc.Stats[desc.ScalingStat] : 0;
        var baseDamage = desc.Damage + scalingValue * (desc.Scaling / 100f);
        var minAttack = (int)Math.Round(baseDamage * 0.975f);
        var maxAttack = (int)Math.Round(baseDamage * 1.025f);

        _minDamageLabel.Text = $"Daño Mínimo: {minAttack}";
        _minDamageLabel.SetPosition(0, yOffset);
        yOffset += _minDamageLabel.Height + 4;

        _maxDamageLabel.Text = $"Daño Máximo: {maxAttack}";
        _maxDamageLabel.SetPosition(0, yOffset);
        yOffset += _maxDamageLabel.Height + 4;

        SetSize(300, yOffset);
    }

}
