using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Pets;

public sealed class PetHubWindow : Window
{
    private readonly Label _statusLabel;
    private readonly PetBehaviorWidget _behaviorWidget;

    public PetHubWindow(Canvas gameCanvas)
        : base(gameCanvas, Strings.Pets.HubTitle, modal: false, name: nameof(PetHubWindow))
    {
        DisableResizing();
        IsClosable = true;
        RestrictToParent = true;

        Alignment = [Alignments.Bottom, Alignments.Left];
        AlignmentPadding = new Padding { Left = 8, Bottom = 8 };

        _statusLabel = new Label(this, "StatusLabel")
        {
            Text = Strings.Pets.StatusNoPet.ToString(),
            FontName = "Arial",
            FontSize = 14,
        };
        _statusLabel.SetPosition(16, 32);
        _statusLabel.SetSize(200, 32);

        _behaviorWidget = new PetBehaviorWidget(this);
        _behaviorWidget.Font = _statusLabel.Font;
        _behaviorWidget.FontSize = 14;
        _behaviorWidget.SetPosition(16, 72);
        _behaviorWidget.SetSize(200, 48);

        Globals.PetHub.ActivePetChanged += OnPetHubStateChanged;
        Globals.PetHub.BehaviorChanged += OnPetHubStateChanged;
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
        }

        base.Dispose(disposing);
    }

    private void OnPetHubStateChanged()
    {
        RefreshState();
    }

    private void RefreshState()
    {
        if (!Globals.PetHub.HasActivePet || Globals.PetHub.ActivePet is not { } pet)
        {
            _statusLabel.Text = Strings.Pets.StatusNoPet.ToString();
            return;
        }

        _statusLabel.Text = Strings.Pets.StatusWithPet.ToString(pet.Name);
    }
}
