using System.Collections.Generic;
using Intersect.Client.General;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Localization;
using Intersect.Localization;
using Intersect.Shared.Pets;

namespace Intersect.Client.Interface.Game.Pets;

public sealed class PetBehaviorWidget : RadioButtonGroup
{
    private readonly Dictionary<PetBehavior, LabeledRadioButton> _options = new();
    private bool _suppressSelectionChanged;

    public PetBehaviorWidget(Canvas parent) : base(parent)
    {
        Name = nameof(PetBehaviorWidget);
        Alignment = [Alignments.Bottom, Alignments.Left];
        AlignmentPadding = new Padding { Bottom = 4, Left = 4 };
        Padding = new Padding(4);
        RestrictToParent = true;
        ShouldCacheToTexture = true;
        Text = Strings.Pets.WidgetTitle.ToString();

        CreateOption(PetBehavior.Follow, Strings.Pets.BehaviorFollow);
        CreateOption(PetBehavior.Stay, Strings.Pets.BehaviorStay);
        CreateOption(PetBehavior.Defend, Strings.Pets.BehaviorDefend);
        CreateOption(PetBehavior.Passive, Strings.Pets.BehaviorPassive);

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

    private void CreateOption(PetBehavior behavior, LocalizedString label)
    {
        var option = AddOption(label.ToString(), behavior.ToString());
        option.UserData = behavior;
        option.IsTabable = false;
        option.Margin = new Margin(0, 0, 0, 2);
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

        if (args.Selected is not LabeledRadioButton option || option.UserData is not PetBehavior behavior)
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
                option.RadioButton.IsChecked = hasPet && activeBehavior == behavior;
            }

            if (!hasPet)
            {
                foreach (var option in _options.Values)
                {
                    option.RadioButton.IsChecked = false;
                }
            }
        }
        finally
        {
            _suppressSelectionChanged = false;
        }
    }
}
