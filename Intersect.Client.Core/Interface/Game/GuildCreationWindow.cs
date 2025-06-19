using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game;

public partial class GuildCreationWindow : Window
{
    private readonly ImagePanel _preview;
    private readonly LabeledSlider _bgR;
    private readonly LabeledSlider _bgG;
    private readonly LabeledSlider _bgB;
    private readonly LabeledSlider _symbolR;
    private readonly LabeledSlider _symbolG;
    private readonly LabeledSlider _symbolB;
    private readonly Button _createButton;
    private readonly Button _cancelButton;

    private IGameTexture? _background;
    private IGameTexture? _symbol;

    public GuildCreationWindow(Canvas gameCanvas) : base(gameCanvas, "Create Guild", false, nameof(GuildCreationWindow))
    {
        IsResizable = false;
        Alignment = [Alignments.Center];
        MinimumSize = new Point(360, 240);
        IsClosable = true;

        _preview = new ImagePanel(this, nameof(_preview));

        _bgR = CreateSlider(nameof(_bgR), "Red");
        _bgG = CreateSlider(nameof(_bgG), "Green");
        _bgB = CreateSlider(nameof(_bgB), "Blue");

        _symbolR = CreateSlider(nameof(_symbolR), "Red");
        _symbolG = CreateSlider(nameof(_symbolG), "Green");
        _symbolB = CreateSlider(nameof(_symbolB), "Blue");

        _createButton = new Button(this, nameof(_createButton))
        {
            Text = "Create"
        };
        _cancelButton = new Button(this, nameof(_cancelButton))
        {
            Text = "Cancel"
        };

        _createButton.Clicked += (s, e) => OnCreate?.Invoke(this, EventArgs.Empty);
        _cancelButton.Clicked += (s, e) => Close();
    }

    public event EventHandler? OnCreate;

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer?.GetResolutionString());
    }

    public void SetTextures(IGameTexture background, IGameTexture symbol)
    {
        _background = background;
        _symbol = symbol;
        UpdatePreview();
    }

    private Color SliderColor(LabeledSlider r, LabeledSlider g, LabeledSlider b)
    {
        return GraphicsHelper.SetColor((byte)r.Value, (byte)g.Value, (byte)b.Value);
    }

    private void UpdatePreview()
    {
        if (_background == null || _symbol == null)
        {
            return;
        }

        var composed = GraphicsHelper.Compose(
            _background,
            _symbol,
            SliderColor(_bgR, _bgG, _bgB),
            SliderColor(_symbolR, _symbolG, _symbolB)
        );

        _preview.Texture = composed;
    }

    private LabeledSlider CreateSlider(string name, string label)
    {
        var slider = new LabeledSlider(this, name)
        {
            Label = label,
            Orientation = Orientation.LeftToRight,
            Minimum = 0,
            Maximum = 255,
            Rounding = 0
        };
        slider.ValueChanged += (_, _) => UpdatePreview();
        return slider;
    }
}
