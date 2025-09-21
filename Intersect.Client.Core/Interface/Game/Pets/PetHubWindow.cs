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


namespace Intersect.Client.Interface.Game.Pets
{
    public sealed class PetHubWindow : Window
    {
        // Layout
        private const int WindowWidth = 360;
        private const int WindowHeight = 560;

        private const int Margin = 16;
        private const int Gap = 8;

        private const int DetailLineHeight = 22;
        private const int StatColumnWidth = 150;
        private const int StatRowHeight = 20;

        // Fonts
        private const string FontName = "sourceproblack";
        private const int TitleSize = 18;
        private const int HeaderSize = 16;
        private const int BodySize = 13;
        private const int StatSize = 12;
        private const int ButtonSize = 14;

        // Controls
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

        private readonly PetBehaviorWidget _behaviorWidget;

        private readonly Button _invokeButton;
        private readonly Button _dismissButton;

        public PetHubWindow(Canvas gameCanvas)
            : base(gameCanvas, Strings.Pets.HubTitle, modal: false, name: nameof(PetHubWindow))
        {
            // Window chrome
            DisableResizing();
            IsClosable = true;
            RestrictToParent = true;

            Alignment = new[] { Alignments.Bottom, Alignments.Left };
            AlignmentPadding = new Padding { Left = 8, Bottom = 8 };

            SetSize(WindowWidth, WindowHeight);
            SetPosition(16, 16);

            TitleLabel.FontName = FontName;
            TitleLabel.FontSize = TitleSize;
            TitleLabel.TextColorOverride = Color.White;

            // Status
            _statusLabel = new Label(this, "StatusLabel")
            {
                FontName = FontName,
                FontSize = BodySize,
                TextColor = Color.White,
                Text = Strings.Pets.StatusNoPet.ToString()
            };
            _statusLabel.SetPosition(Margin, 32);
            _statusLabel.SetSize(WindowWidth - Margin * 2, 24);

            // Panel de detalles (nombres, nivel, comportamiento, vitals)
            _detailsPanel = new ImagePanel(this, "DetailsPanel");
            _detailsPanel.SetPosition(Margin, 64);
            _detailsPanel.SetSize(WindowWidth - Margin * 2, 220);

            _customNameLabel = CreateDetailLabel(_detailsPanel, "CustomNameLabel", 0);
            _descriptorNameLabel = CreateDetailLabel(_detailsPanel, "DescriptorNameLabel", DetailLineHeight);
            _levelLabel = CreateDetailLabel(_detailsPanel, "LevelLabel", DetailLineHeight * 2);
            _behaviorLabel = CreateDetailLabel(_detailsPanel, "BehaviorLabel", DetailLineHeight * 3);

            _vitalsHeader = CreateDetailLabel(_detailsPanel, "VitalsHeaderLabel", DetailLineHeight * 4);
            _vitalsHeader.FontSize = HeaderSize;
            _vitalsHeader.Text = Strings.Pets.VitalsHeader.ToString();

            // Vitals debajo del header
            var firstVitalY = DetailLineHeight * 5;
            var vitalIndex = 0;
            foreach (var vital in Enum.GetValues<Vital>())
            {
                var label = CreateDetailLabel(_detailsPanel, $"Vital{vital}Label", firstVitalY + (vitalIndex * DetailLineHeight));
                _vitalLabels[vital] = label;
                vitalIndex++;
            }

            // Header stats
            _statsHeader = new Label(this, "StatsHeader")
            {
                FontName = FontName,
                FontSize = HeaderSize,
                TextColor = Color.White,
                Text = Strings.Pets.StatsHeader.ToString(),
            };
            _statsHeader.SetPosition(Margin, 64 + 220 + Gap);
            _statsHeader.SetSize(WindowWidth - Margin * 2, 22);

            // Panel stats (2 columnas)
            _statsPanel = new Base(this, "StatsPanel");
            _statsPanel.SetPosition(Margin, 64 + 220 + Gap + 24);
            _statsPanel.SetSize(WindowWidth - Margin * 2, 110);

            var idx = 0;
            foreach (var stat in Enum.GetValues<Stat>())
            {
                var label = new Label(_statsPanel, $"Stat{stat}Label")
                {
                    FontName = FontName,
                    FontSize = StatSize,
                    TextColor = Color.White,
                    AutoSizeToContents = false,
                };

                var col = idx % 2;
                var row = idx / 2;

                label.SetSize(StatColumnWidth, StatRowHeight);
                label.SetPosition(col * (StatColumnWidth + 8), row * StatRowHeight);

                _statLabels[stat] = label;
                idx++;
            }

            _noStatsLabel = new Label(_statsPanel, "NoStatsLabel")
            {
                FontName = FontName,
                FontSize = StatSize,
                TextColor = Color.White,
                AutoSizeToContents = false,
                Text = Strings.Pets.NoStats.ToString(),
            };
            _noStatsLabel.SetSize(StatColumnWidth * 2 + 8, StatRowHeight);
            _noStatsLabel.SetPosition(0, 0);

            // Selector de comportamiento
            _behaviorWidget = new PetBehaviorWidget(this)
            {
                FontName = FontName,
                FontSize = BodySize,
                TextColor = Color.White,
            };
            // Debajo de stats, ocupando ancho con margen
            _behaviorWidget.SetPosition(Margin, _statsPanel.Y + _statsPanel.Height + Gap);
            _behaviorWidget.SetSize(WindowWidth - Margin * 2, 96);

            // Botones (Invoke / Dismiss)
            var buttonsY = _behaviorWidget.Y + _behaviorWidget.Height + Gap;
            _invokeButton = CreateActionButton(
                "InvokeButton",
                Margin,
                buttonsY,
                Strings.Pets.InvokeButton.ToString(),
                OnInvokeClicked
            );

            _dismissButton = CreateActionButton(
                "DismissButton",
                Margin + 8 + 136,
                buttonsY,
                Strings.Pets.DismissButton.ToString(),
                OnDismissClicked
            );

            // Eventos del Hub
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
            var pet = Globals.PetHub.ActivePet as Pet;
            var hasPet = Globals.PetHub.HasActivePet && pet != null;
            var isSpawnRequested = Globals.PetHub.IsSpawnRequested;

            _invokeButton.Text = Strings.Pets.InvokeButton.ToString();
            _dismissButton.Text = Strings.Pets.DismissButton.ToString();

            _statusLabel.Text = hasPet && pet != null
                ? Strings.Pets.StatusWithPet.ToString(pet.Name)
                : Strings.Pets.StatusNoPet.ToString();

            _statusLabel.IsHidden = hasPet;

            _detailsPanel.IsHidden = !hasPet;
            _statsHeader.IsHidden = !hasPet;
            _statsPanel.IsHidden = !hasPet;
            _behaviorWidget.IsHidden = !hasPet;

            // Según tu semántica previa: invocar deshabilitado si ya está solicitada/activa; dismiss habilitado cuando está activa
            _invokeButton.IsDisabled = isSpawnRequested;
            _dismissButton.IsDisabled = !isSpawnRequested;

            if (!hasPet || pet == null)
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
                label.SetPosition(column * (StatColumnWidth + 8), row * StatRowHeight);
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
            PetState    .Stay => Strings.Pets.BehaviorStay.ToString(),
            PetState.Defend => Strings.Pets.BehaviorDefend.ToString(),
            PetState.Passive => Strings.Pets.BehaviorPassive.ToString(),
            _ => Strings.Pets.BehaviorUnknown.ToString(),
        };

        private Label CreateDetailLabel(Base parent, string name, int y)
        {
            var label = new Label(parent, name)
            {
                FontName = FontName,
                FontSize = BodySize,
                TextColor = Color.White,
                AutoSizeToContents = false,
            };

            label.SetPosition(0, y);
            label.SetSize(parent.Width, DetailLineHeight);
            return label;
        }

        private Button CreateActionButton(string name, int x, int y, string text, Base.GwenEventHandler<MouseButtonState> onClick)
        {
            var button = new Button(this, name)
            {
                FontName = FontName,
                FontSize = ButtonSize,
                TextColor = Color.White,
                Text = text,
            };

            button.SetPosition(x, y);
            button.SetSize(136, 34);
            button.Clicked += onClick;
            return button;
        }

        private void OnInvokeClicked(Base sender, MouseButtonState arguments)
        {
            if (Globals.PetHub.InvokePet())
            {
                _invokeButton.IsDisabled = true;
                _dismissButton.IsDisabled = false;
            }
        }

        private void OnDismissClicked(Base sender, MouseButtonState arguments)
        {
            if (Globals.PetHub.DismissPet())
            {
                _dismissButton.IsDisabled = true;
                _invokeButton.IsDisabled = false;
            }
        }
    }
}
