using System;
using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;

namespace Intersect.Client.Interface.Game.Pets;

public sealed class PetHubWindow : Window
{
    private const int DetailLineHeight = 24;
    private const int StatColumnWidth = 140;
    private const int StatRowHeight = 20;

    private readonly Label _statusLabel;
    private readonly ImagePanel _detailsPanel;
    private readonly Label _customNameLabel;
    private readonly Label _descriptorNameLabel;
    private readonly Label _levelLabel;
    private readonly Label _behaviorLabel;
    private readonly Label _vitalsHeader;
    private readonly Dictionary<Vital, Label> _vitalLabels = new();

    private readonly Label _statsHeader;
    private readonly Base _statsPanel;
    private readonly Label _noStatsLabel;
    private readonly Dictionary<Stat, Label> _statLabels = new();

    private readonly Button _invokeButton;
    private readonly Button _dismissButton;
    private readonly PetBehaviorWidget _behaviorWidget;

    public PetHubWindow(Canvas gameCanvas)
        : base(gameCanvas, Strings.Pets.HubTitle, modal: false, name: nameof(PetHubWindow))
    {
        DisableResizing();
        IsClosable = true;
        RestrictToParent = true;

        Alignment = [Alignments.Bottom, Alignments.Left];
        AlignmentPadding = new Padding { Left = 8, Bottom = 8 };
        SetSize(320, 480);
        SetPosition(16, 16);

        TitleLabel.FontName = "Arial";
        TitleLabel.FontSize = 16;
        TitleLabel.TextColorOverride = Color.White;

        _statusLabel = new Label(this, "StatusLabel")
        {
            Text = Strings.Pets.StatusNoPet.ToString(),
            FontName = "Arial",
            FontSize = 14,
            TextColor = Color.White,
        };
        _statusLabel.SetPosition(16, 32);
        _statusLabel.SetSize(288, 24);

        _detailsPanel = new ImagePanel(this, "DetailsPanel");
        _detailsPanel.SetPosition(16, 64);
        _detailsPanel.SetSize(288, 176);

        _customNameLabel = CreateDetailLabel(_detailsPanel, "CustomNameLabel", 0);
        _descriptorNameLabel = CreateDetailLabel(_detailsPanel, "DescriptorNameLabel", DetailLineHeight);
        _levelLabel = CreateDetailLabel(_detailsPanel, "LevelLabel", DetailLineHeight * 2);
        _behaviorLabel = CreateDetailLabel(_detailsPanel, "BehaviorLabel", DetailLineHeight * 3);

        _vitalsHeader = CreateDetailLabel(_detailsPanel, "VitalsHeaderLabel", DetailLineHeight * 4);
        _vitalsHeader.FontSize = 14;
        _vitalsHeader.Text = Strings.Pets.VitalsHeader.ToString();

        foreach (var vital in Enum.GetValues<Vital>())
        {
            var offset = DetailLineHeight * (5 + (int)vital);
            var label = CreateDetailLabel(_detailsPanel, $"Vital{vital}Label", offset);
            _vitalLabels[vital] = label;
        }

        _statsHeader = new Label(this, "StatsHeader")
        {
            FontName = "Arial",
            FontSize = 14,
            TextColor = Color.White,
            Text = Strings.Pets.StatsHeader.ToString(),
        };
        _statsHeader.SetPosition(16, 248);
        _statsHeader.SetSize(288, 20);

        _statsPanel = new Base(this, "StatsPanel");
        _statsPanel.SetPosition(16, 272);
        _statsPanel.SetSize(288, 96);

        foreach (var stat in Enum.GetValues<Stat>())
        {
            var label = new Label(_statsPanel, $"Stat{stat}Label")
            {
                FontName = "Arial",
                FontSize = 12,
                TextColor = Color.White,
                AutoSizeToContents = false,
            };
            label.SetSize(StatColumnWidth, StatRowHeight);
            _statLabels[stat] = label;
        }

        _noStatsLabel = new Label(_statsPanel, "NoStatsLabel")
        {
            FontName = "Arial",
            FontSize = 12,
            TextColor = Color.White,
            AutoSizeToContents = false,
            Text = Strings.Pets.NoStats.ToString(),
        };
        _noStatsLabel.SetSize(StatColumnWidth * 2, StatRowHeight);

        _behaviorWidget = new PetBehaviorWidget(this)
        {
            FontName = "Arial",
            FontSize = 14,
            TextColor = Color.White,
        };
        _behaviorWidget.SetPosition(16, 380);
        _behaviorWidget.SetSize(288, 96);

        _invokeButton = CreateActionButton("InvokeButton", 16, 432, Strings.Pets.InvokeButton.ToString(), OnInvokeClicked);
        _dismissButton = CreateActionButton("DismissButton", 168, 432, Strings.Pets.DismissButton.ToString(), OnDismissClicked);

        Globals.PetHub.ActivePetChanged += OnPetHubStateChanged;
        Globals.PetHub.BehaviorChanged += OnPetHubStateChanged;
        Globals.PetHub.SpawnStateChanged += OnPetHubStateChanged;
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        RefreshState();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Globals.PetHub.ActivePetChanged -= OnPetHubStateChanged;
            Globals.PetHub.BehaviorChanged -= OnPetHubStateChanged;
            Globals.PetHub.SpawnStateChanged -= OnPetHubStateChanged;
        }

        base.Dispose(disposing);
    }

    private void OnPetHubStateChanged()
    {
        RefreshState();
    }

    private void RefreshState()
    {
        var hasPet = Globals.PetHub.HasActivePet && Globals.PetHub.ActivePet is { } pet;
        var isSpawnRequested = Globals.PetHub.IsSpawnRequested;

        _invokeButton.Text = Strings.Pets.InvokeButton.ToString();
        _dismissButton.Text = Strings.Pets.DismissButton.ToString();

        _statusLabel.Text = hasPet
            ? Strings.Pets.StatusWithPet.ToString(pet.Name)
            : Strings.Pets.StatusNoPet.ToString();
        _statusLabel.IsHidden = hasPet;

        _detailsPanel.IsHidden = !hasPet;
        _statsHeader.IsHidden = !hasPet;
        _statsPanel.IsHidden = !hasPet;
        _behaviorWidget.IsHidden = !hasPet;
        _invokeButton.IsDisabled = isSpawnRequested;
        _dismissButton.IsDisabled = !isSpawnRequested;

        if (!hasPet)
        {
            foreach (var label in _statLabels.Values)
            {
                label.IsHidden = true;
            }

            foreach (var vitalLabel in _vitalLabels.Values)
            {
                vitalLabel.IsHidden = true;
            }

            _vitalsHeader.IsHidden = true;
            _noStatsLabel.IsHidden = false;

            return;
        }

        var descriptor = pet.Descriptor;

        _customNameLabel.Text = Strings.Pets.CustomNameLabel.ToString(pet.Name);
        var descriptorName = descriptor?.Name;
        if (string.IsNullOrWhiteSpace(descriptorName))
        {
            descriptorName = Strings.Pets.UnknownDescriptorName.ToString();
        }

        _descriptorNameLabel.Text = Strings.Pets.DescriptorNameLabel.ToString(descriptorName);
        _levelLabel.Text = Strings.Pets.LevelLabel.ToString(pet.Level);

        var behaviorText = GetBehaviorLabel(Globals.PetHub.Behavior);
        _behaviorLabel.Text = Strings.Pets.BehaviorLabel.ToString(behaviorText);

        UpdateVitals(pet);
        UpdateStats(descriptor);
    }

    private void UpdateVitals(Pet pet)
    {
        var hasVitals = false;
        foreach (var (vital, label) in _vitalLabels)
        {
            var max = pet.MaxVital[(int)vital];
            var current = pet.Vital[(int)vital];

            var vitalName = GetVitalName(vital);
            label.Text = Strings.Pets.VitalFormat.ToString(vitalName, current, max);
            label.IsHidden = false;

            hasVitals = true;
        }

        _vitalsHeader.IsHidden = !hasVitals;
    }

    private void UpdateStats(PetDescriptor? descriptor)
    {
        var statIndex = 0;

        if (descriptor?.StatsLookup == null || descriptor.StatsLookup.Count == 0)
        {
            foreach (var label in _statLabels.Values)
            {
                label.IsHidden = true;
            }

            _noStatsLabel.IsHidden = false;
            _noStatsLabel.SetPosition(0, 0);
            return;
        }

        foreach (var stat in Enum.GetValues<Stat>())
        {
            var label = _statLabels[stat];
            var statValue = descriptor.StatsLookup.TryGetValue(stat, out var value)
                ? value
                : 0;

            var statName = GetStatName(stat);
            label.Text = Strings.Pets.StatFormat.ToString(statName, statValue);

            var column = statIndex % 2;
            var row = statIndex / 2;
            label.SetPosition(column * StatColumnWidth, row * StatRowHeight);
            label.IsHidden = false;

            statIndex++;
        }

        _noStatsLabel.IsHidden = statIndex != 0;
    }

    private static string GetStatName(Stat stat) =>
        Strings.Combat.Stats.TryGetValue(stat, out var label)
            ? label.ToString()
            : stat.ToString();

    private static string GetVitalName(Vital vital) =>
        Strings.ItemDescription.Vitals.TryGetValue((int)vital, out var label)
            ? label.ToString().TrimEnd(':')
            : vital.ToString();

    private static string GetBehaviorLabel(PetState behavior) => behavior switch
    {
        PetState.Follow => Strings.Pets.BehaviorFollow.ToString(),
        PetState.Stay => Strings.Pets.BehaviorStay.ToString(),
        PetState.Defend => Strings.Pets.BehaviorDefend.ToString(),
        PetState.Passive => Strings.Pets.BehaviorPassive.ToString(),
        _ => Strings.Pets.BehaviorUnknown.ToString(),
    };

    private Label CreateDetailLabel(Base parent, string name, int y)
    {
        var label = new Label(parent, name)
        {
            FontName = "Arial",
            FontSize = 12,
            TextColor = Color.White,
            AutoSizeToContents = false,
        };

        label.SetPosition(0, y);
        label.SetSize(288, 20);
        return label;
    }

    private Button CreateActionButton(string name, int x, int y, string text, EventHandler<ClickedEventArgs> onClick)
    {
        var button = new Button(this, name)
        {
            FontName = "Arial",
            FontSize = 14,
            TextColor = Color.White,
            Text = text,
        };

        button.SetPosition(x, y);
        button.SetSize(136, 32);
        button.Clicked += onClick;
        return button;
    }

    private void OnInvokeClicked(Base sender, ClickedEventArgs args)
    {
        if (Globals.PetHub.InvokePet())
        {
            _invokeButton.IsDisabled = true;
        }
    }

    private void OnDismissClicked(Base sender, ClickedEventArgs args)
    {
        if (Globals.PetHub.DismissPet())
        {
            _dismissButton.IsDisabled = true;
        }
    }
}
