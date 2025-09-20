using System.Collections.Generic;
using Intersect.Client.General;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Localization;
using Intersect.Localization;
using Intersect.Shared.Pets;
using Intersect.Client.Core;

namespace Intersect.Client.Interface.Game.Pets;

public sealed class PetBehaviorWidget : RadioButtonGroup
{
    private readonly Dictionary<PetBehavior, LabeledRadioButton> _options = new();
    private bool _suppressSelectionChanged;

    public PetBehaviorWidget(Base parent) : base(parent)
    {
        Name = nameof(PetBehaviorWidget);

        // Establecer tamaño y posición del widget principal
        SetSize(220, 120); // ejemplo de tamaño
        SetPosition(20, 20); // ejemplo de posición

        Alignment = [Alignments.Bottom, Alignments.Left];
        AlignmentPadding = new Padding { Bottom = 4, Left = 4 };
        Padding = new Padding(4);
        RestrictToParent = true;
        ShouldCacheToTexture = true;
        Text = Strings.Pets.WidgetTitle.ToString();

        // Establecer fuente y tamaño de fuente del widget principal
        FontName = "Arial";
        FontSize = 14;

        CreateOption(PetBehavior.Follow, Strings.Pets.BehaviorFollow, 0, 0);
        CreateOption(PetBehavior.Stay, Strings.Pets.BehaviorStay, 0, 30);
        CreateOption(PetBehavior.Defend, Strings.Pets.BehaviorDefend, 0, 60);
        CreateOption(PetBehavior.Passive, Strings.Pets.BehaviorPassive, 0, 90);

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

    private void CreateOption(PetBehavior behavior, LocalizedString label, int x, int y)
    {
        var option = AddOption(label.ToString(), behavior.ToString());
        option.UserData = behavior;
        option.IsTabable = false;
        option.Margin = new Margin(0, 0, 0, 2);

        // Establecer tamaño, posición, fuente y tamaño de fuente de cada opción
        option.SetSize(200, 28);
        option.SetPosition(x, y);
      

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

        if (args.SelectedItem is not LabeledRadioButton option || option.UserData is not PetBehavior behavior)
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
            var hasPet = Globals.PetHub.HasActivePet;
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
