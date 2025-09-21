using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Enums;
using Intersect.Localization;

namespace Intersect.Client.Interface.Game.Pets;

public sealed class PetBehaviorWidget : RadioButtonGroup
{
    private const string DefaultFontName = "Arial";
    private const int DefaultFontSize = 14;
    private static readonly Color NormalTextColor = Color.White;
    private static readonly Color HoveredTextColor = Color.FromArgb(230, 230, 230);
    private static readonly Color ActiveTextColor = Color.FromArgb(200, 200, 200);
    private static readonly Color DisabledTextColor = Color.FromArgb(140, 140, 140);

    private readonly Dictionary<PetState, LabeledRadioButton> _options = new();
    private bool _suppressSelectionChanged;

    public PetBehaviorWidget(Base parent) : base(parent)
    {
        Name = nameof(PetBehaviorWidget);

        // Establecer tamaño y posición del widget principal
        SetSize(248, 120);
        SetPosition(16, 0);

        Alignment = [Alignments.Bottom, Alignments.Left];
        AlignmentPadding = new Padding { Bottom = 4, Left = 4 };
        Padding = new Padding(4);
        RestrictToParent = true;
        ShouldCacheToTexture = true;
        Text = Strings.Pets.WidgetTitle.ToString();

        // Establecer fuente y tamaño de fuente del widget principal
        FontName = DefaultFontName;
        FontSize = DefaultFontSize;
        TextColor = NormalTextColor;

        CreateOption(PetState.Follow, Strings.Pets.BehaviorFollow, 0, 0);
        CreateOption(PetState.Stay, Strings.Pets.BehaviorStay, 0, 30);
        CreateOption(PetState.Defend, Strings.Pets.BehaviorDefend, 0, 60);
        CreateOption(PetState.Passive, Strings.Pets.BehaviorPassive, 0, 90);

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        SelectionChanged += OnSelectionChanged;
        Globals.PetHub.ActivePetChanged += OnPetHubStateChanged;
        Globals.PetHub.BehaviorChanged += OnPetHubStateChanged;

        RefreshState();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Globals.PetHub.ActivePetChanged -= OnPetHubStateChanged;
            Globals.PetHub.BehaviorChanged -= OnPetHubStateChanged;
            SelectionChanged -= OnSelectionChanged;
        }

        base.Dispose(disposing);
    }

    private void CreateOption(PetState behavior, LocalizedString label, int x, int y)
    {
        var option = AddOption(label.ToString(), behavior.ToString());
        option.UserData = behavior;
        option.IsTabable = false;
        option.Margin = new Margin(0, 0, 0, 2);

        // Establecer tamaño, posición, fuente, tamaño de fuente y color de texto de cada opción
        option.SetSize(248, 28);
        option.SetPosition(x, y);
        option.FontName = DefaultFontName;
        option.FontSize = DefaultFontSize;
        option.TextColor = NormalTextColor;
        option.SetTextColor(NormalTextColor, ComponentState.Normal);
        option.SetTextColor(HoveredTextColor, ComponentState.Hovered);
        option.SetTextColor(ActiveTextColor, ComponentState.Active);
        option.SetTextColor(DisabledTextColor, ComponentState.Disabled);


        _options[behavior] = option;
    }

    private void OnPetHubStateChanged()
    {
        RefreshState();
    }

    private void OnSelectionChanged(Base sender, ItemSelectedEventArgs args)
    {
        if (_suppressSelectionChanged)
        {
            return;
        }

        if (args.SelectedItem is not LabeledRadioButton option || option.UserData is not PetState behavior)
        {
            return;
        }

        _ = Globals.PetHub.SetBehavior(behavior);
        RefreshState();
    }

    private void RefreshState()
    {
        _suppressSelectionChanged = true;

        try
        {
            var hasPet = Globals.PetHub.ActivePet != null;
            var activeBehavior = Globals.PetHub.Behavior;

            foreach (var (behavior, option) in _options)
            {
                option.IsDisabled = !hasPet;
                option.IsChecked = hasPet && activeBehavior == behavior;
            }

            if (!hasPet)
            {
                foreach (var option in _options.Values)
                {
                    option.IsChecked = false;
                }
            }
        }
        finally
        {
            _suppressSelectionChanged = false;
        }
    }
}
