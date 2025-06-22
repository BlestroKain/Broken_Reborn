using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Graphics = Intersect.Client.Core.Graphics;

namespace Intersect.Client.Interface.Game.Guilds;

public partial class GuildCreationWindow : Window
{
    private  TextBox _guildNameTextbox;

    // Panel principal para elegir símbolo/fondo
    private  ImagePanel _logoPanel;
    private Button _symbolButton;
    private  Button _backgroundButton;
    private ScrollControl _symbolPanel;
    private  ScrollControl _backgroundPanel;
    private Button _createGuildButton;

    // Panel de previsualización
    private ImagePanel _logoCompositionPanel;
    private ImagePanel _backgroundPreview;
    private readonly ImagePanel _symbolPreview;

    // Use a non-static type or a wrapper class/interface for GameTexture to resolve the issue.
    private readonly List<IGameTexture?> _logoElements;
    private readonly List<IGameTexture?> _originalLogoElements;

    private bool _initializedSymbols;
    private bool _initializedBackgrounds;

    // Sliders de color para el FONDO
    private Label _backgroundColorLabel;
    private Slider _bgRedSlider;
    private Slider _bgGreenSlider;
    private  Slider _bgBlueSlider;
    private TextBoxNumeric _bgRedText;
    private TextBoxNumeric _bgGreenText;
    private  TextBoxNumeric _bgBlueText;

    private byte _bgR = 255;
    private byte _bgG = 255;
    private byte _bgB = 255;

    // Sliders de color para el SÍMBOLO
    private Label _symbolColorLabel;
    private Slider _symRedSlider;
    private Slider _symGreenSlider;
    private Slider _symBlueSlider;
    private TextBoxNumeric _symRedText;
    private TextBoxNumeric _symGreenText;
    private TextBoxNumeric _symBlueText;

    private byte _symR;
    private byte _symG;
    private byte _symB;

    private string _selectedBackgroundFile = string.Empty;
    private string _selectedSymbolFile = string.Empty;

    private const int CompositionSize = 100;

    public GuildCreationWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Guilds.Guild, false, nameof(GuildCreationWindow))
    {
        DisableResizing();
        SetSize(width: 850, height: 400);

        _guildNameTextbox = new TextBox(this, "GuildNameTextbox");

        // Fixing the CS1739 error by replacing named parameters with positional arguments
        _guildNameTextbox.SetBounds(20, 10, 810, 30);
        Interface.FocusComponents.Add(_guildNameTextbox);

        _logoPanel = new ImagePanel(this, "LogoPanel");
        _logoPanel.SetBounds(20, 50, 810, 230);

        _symbolButton = new Button(_logoPanel, "SymbolButton")
        {
            Text = "Símbolos"
        };
        _symbolButton.SetBounds(0, 0, 405, 40);

        _backgroundButton = new Button(_logoPanel, "BackgroundButton")
        {
            Text = "Fondos"
        };
        _backgroundButton.SetBounds(405, 0, 405, 40);

        _symbolPanel = new ScrollControl(_logoPanel, "SymbolPanel");
        _symbolPanel.SetBounds(0, 40, 810, 190);
        _symbolPanel.EnableScroll(false, true);

        _backgroundPanel = new ScrollControl(_logoPanel, "BackgroundPanel");
        _backgroundPanel.SetBounds(0, 40, 810, 190);
        _backgroundPanel.EnableScroll(false, true);

        _symbolButton.Clicked += (_, _) =>
        {
            _symbolPanel.Show();
            _backgroundPanel.Hide();
        };
        _backgroundButton.Clicked += (_, _) =>
        {
            _symbolPanel.Hide();
            _backgroundPanel.Show();
        };

        _logoCompositionPanel = new ImagePanel(this, "LogoCompositionPanel");
        _logoCompositionPanel.SetBounds(20, 290, CompositionSize + 5, CompositionSize + 5);

        _backgroundPreview = new ImagePanel(_logoCompositionPanel, "BackgroundPreview");
        _backgroundPreview.SetBounds(0, 0, CompositionSize, CompositionSize);
        _backgroundPreview.Show();

        _symbolPreview = new ImagePanel(_logoCompositionPanel, "SymbolPreview");
        _symbolPreview.SetBounds(0, 0, 56, 56);
        _symbolPreview.Show();

        _createGuildButton = new Button(this, "CreateGuildButton") { Text = "Crear Gremio" };
        _createGuildButton.SetBounds(650, 620, 150, 40);
        _createGuildButton.Clicked += OnCreateGuildButtonClicked;
        // Fix for CS0029 and CS0718: Replace 'GameTexture' with 'IGameTexture' in the list initialization
        _logoElements = new List<IGameTexture?> { null, null };
        _originalLogoElements = new List<IGameTexture?> { null, null };


        InitializeBackgroundColorSliders();
        InitializeSymbolColorSliders();
    }

    protected override void EnsureInitialized()
    {
        if (!_initializedSymbols)
        {
            _initializedSymbols = true;
            InitializeSymbolPanel();
        }

        if (!_initializedBackgrounds)
        {
            _initializedBackgrounds = true;
            InitializeBackgroundPanel();
        }

        _symbolPanel.Show();
        _backgroundPanel.Hide();

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    private void OnCreateGuildButtonClicked(Base sender, MouseButtonState arguments)
    {
        var guildName = _guildNameTextbox.Text.Trim();
        if (string.IsNullOrEmpty(guildName))
        {
            PacketSender.SendChatMsg("El nombre del gremio está vacío.", 5);
            return;
        }

        if (string.IsNullOrEmpty(_selectedBackgroundFile))
        {
            PacketSender.SendChatMsg("No se ha seleccionado un fondo.", 5);
            return;
        }

        if (string.IsNullOrEmpty(_selectedSymbolFile))
        {
            PacketSender.SendChatMsg("No se ha seleccionado un símbolo.", 5);
            return;
        }

        PacketSender.SendCreateGuild(
            guildName,
            _selectedBackgroundFile,
            _bgR,
            _bgG,
            _bgB,
            _selectedSymbolFile,
            _symR,
            _symG,
            _symB
        );

        Close();
    }

    private void InitializeSymbolPanel()
    {
        const string symbolFolderPath = "resources/Guild/Symbols";
        if (!Directory.Exists(symbolFolderPath))
        {
            Directory.CreateDirectory(symbolFolderPath);
        }

        var files = Directory.GetFiles(symbolFolderPath, "*.png");
        var symbolItems = new List<SlotItem>();

        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var container = new ImagePanel(_symbolPanel, "SymbolContainer");
            container.SetSize(48, 48);

            var symbolImg = new ImagePanel(container, "SymbolImage")
            {
                Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fileName)
            };

            if (symbolImg.Texture != null)
            {
                var (scaledW, scaledH) = ScaleToFit(symbolImg.Texture.Width, symbolImg.Texture.Height, 48, 48);
                symbolImg.SetSize(scaledW, scaledH);
                Align.Center(symbolImg);
            }
            else
            {
                symbolImg.SetSize(48, 48);
            }

            symbolImg.Clicked += (_, _) =>
            {
                _originalLogoElements[1] = symbolImg.Texture;
                _logoElements[1] = symbolImg.Texture;
                _selectedSymbolFile = fileName;
                UpdateLogoPreview();
            };

            symbolItems.Add(new SlotItem(container, "SymbolContainer", 0, null));
        }

        PopulateSlotContainer.Populate(_symbolPanel, symbolItems);
    }


    private void InitializeBackgroundPanel()
    {
        const string backgroundFolderPath = "resources/Guild/Background";
        if (!Directory.Exists(backgroundFolderPath))
        {
            Directory.CreateDirectory(backgroundFolderPath);
        }

        var files = Directory.GetFiles(backgroundFolderPath, "*.png");
      
        const int containerSize = 48;
        const int spacing = 5;
        const int xPadding = 5;
        const int yPadding = 5;
        var columns = _backgroundPanel.Width / (containerSize + spacing + xPadding);
        if (columns < 1)
        {
            columns = 1;
        }

        var index = 0;
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var container = new ImagePanel(_backgroundPanel, "BackgroundContainer");
            container.SetSize(containerSize, containerSize);
            var posX = index % columns * (containerSize + spacing);
            var posY = index / columns * (containerSize + spacing);
            container.SetPosition(posX + xPadding, posY + yPadding);
            container.Show();

            var bgImg = new ImagePanel(container, "BackgroundImage")
            {
                Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fileName)
            };

            if (bgImg.Texture != null)
            {
                var (scaledW, scaledH) = ScaleToFit(bgImg.Texture.Width, bgImg.Texture.Height, containerSize, containerSize);
                bgImg.SetSize(scaledW, scaledH);
                Align.Center(bgImg);
            }
            else
            {
                bgImg.SetSize(containerSize, containerSize);
            }

            bgImg.Clicked += (_, _) =>
            {
                _originalLogoElements[0] = bgImg.Texture;
                _logoElements[0] = bgImg.Texture;
                _selectedBackgroundFile = fileName;
                UpdateLogoPreview();
            };

            index++;
        }
    }

    private void InitializeBackgroundColorSliders()
    {
        _backgroundColorLabel = new Label(this, "BackgroundColorLabel")
        {
            Text = "Color de Fondo:"
        };
        _backgroundColorLabel.SetBounds(220, 290, 200, 30);

        _bgRedSlider = new Slider(this, "BgRedSlider");
        _bgRedSlider.SetBounds(220, 320, 150, 20);
        _bgRedSlider.SetRange(0, 255);
        _bgRedSlider.Value = _bgR;
        _bgRedSlider.ValueChanged += OnBgColorSliderChanged;

        _bgRedText = new TextBoxNumeric(this, "BgRedText");
        _bgRedText.SetBounds(380, 320, 50, 20);
        _bgRedText.SetText(_bgR.ToString());
        _bgRedText.SetMaxLength(255);
        _bgRedText.SubmitPressed += OnBgColorTextChanged;

        _bgGreenSlider = new  Slider(this, "BgGreenSlider");
        _bgGreenSlider.SetBounds(220, 350, 150, 20);
        _bgGreenSlider.SetRange(0, 255);
        _bgGreenSlider.Value = _bgG;
        _bgGreenSlider.ValueChanged += OnBgColorSliderChanged;

        _bgGreenText = new TextBoxNumeric(this, "BgGreenText");
        _bgGreenText.SetBounds(380, 350, 50, 20);
        _bgGreenText.SetText(_bgG.ToString());
        _bgGreenText.SetMaxLength(255);
        _bgGreenText.SubmitPressed += OnBgColorTextChanged;

        _bgBlueSlider = new   Slider(this, "BgBlueSlider");
        _bgBlueSlider.SetBounds(220, 380, 150, 20);
        _bgBlueSlider.SetRange(0, 255);
        _bgBlueSlider.Value = _bgB;
        _bgBlueSlider.ValueChanged += OnBgColorSliderChanged;

        _bgBlueText = new TextBoxNumeric(this, "BgBlueText");
        _bgBlueText.SetBounds(380, 380, 50, 20);
        _bgBlueText.SetText(_bgB.ToString());
        _bgBlueText.SetMaxLength(255);
        _bgBlueText.SubmitPressed += OnBgColorTextChanged;
    }

    private void InitializeSymbolColorSliders()
    {
        _symbolColorLabel = new Label(this, "SymbolColorLabel")
        {
            Text = "Color del Símbolo:"
        };
        _symbolColorLabel.SetBounds(220, 410, 200, 30);

        _symRedSlider = new Slider(this, "SymRedSlider");
        _symRedSlider.SetBounds(220, 440, 150, 20);
        _symRedSlider.SetRange(0, 255);
        _symRedSlider.Value = _symR;
        _symRedSlider.ValueChanged += OnSymColorSliderChanged;

        _symRedText = new TextBoxNumeric(this, "SymRedText");
        _symRedText.SetBounds(380, 440, 50, 20);
        _symRedText.SetText(_symR.ToString());
        _symRedText.SetMaxLength(255);
        _symRedText.SubmitPressed += OnSymColorTextChanged;

        _symGreenSlider = new Slider(this, "SymGreenSlider");
        _symGreenSlider.SetBounds(220, 470, 150, 20);
        _symGreenSlider.SetRange(0, 255);
        _symGreenSlider.Value = _symG;
        _symGreenSlider.ValueChanged += OnSymColorSliderChanged;

        _symGreenText = new TextBoxNumeric(this, "SymGreenText");
        _symGreenText.SetBounds(380, 470, 50, 20);
        _symGreenText.SetText(_symG.ToString());
        _symGreenText.SetMaxLength(255);
        _symGreenText.SubmitPressed += OnSymColorTextChanged;

        _symBlueSlider = new Slider(this, "SymBlueSlider");
        _symBlueSlider.SetBounds(220, 500, 150, 20);
        _symBlueSlider.SetRange(0, 255);
        _symBlueSlider.Value = _symB;
        _symBlueSlider.ValueChanged += OnSymColorSliderChanged;

        _symBlueText = new TextBoxNumeric(this, "SymBlueText");
        _symBlueText.SetBounds(380, 500, 50, 20);
        _symBlueText.SetText(_symB.ToString());
        _symBlueText.SetMaxLength(255);
        _symBlueText.SubmitPressed += OnSymColorTextChanged;
    }

    private void OnBgColorSliderChanged(Base sender, EventArgs e)
    {
        if (sender == _bgRedSlider)
        {
            _bgRedText.Value = _bgRedSlider.Value;
        }
        else if (sender == _bgGreenSlider)
        {
            _bgGreenText.Value = _bgGreenSlider.Value;
        }
        else if (sender == _bgBlueSlider)
        {
            _bgBlueText.Value = _bgBlueSlider.Value;
        }

        UpdateBackgroundColor();
    }

    private void OnBgColorTextChanged(Base sender, EventArgs e)
    {
        if (sender == _bgRedText)
        {
            _bgRedSlider.Value = _bgRedText.Value;
        }
        else if (sender == _bgGreenText)
        {
            _bgGreenSlider.Value = _bgGreenText.Value;
        }
        else if (sender == _bgBlueText)
        {
            _bgBlueSlider.Value = _bgBlueText.Value;
        }

        UpdateBackgroundColor();
    }

    private void UpdateBackgroundColor()
    {
        _bgR = (byte)_bgRedSlider.Value;
        _bgG = (byte)_bgGreenSlider.Value;
        _bgB = (byte)_bgBlueSlider.Value;

        _backgroundPreview.RenderColor = Color.FromArgb(255, _bgR, _bgG, _bgB);
    }

    private void OnSymColorSliderChanged(Base sender, EventArgs e)
    {
        if (sender == _symRedSlider)
        {
            _symRedText.Value = _symRedSlider.Value;
        }
        else if (sender == _symGreenSlider)
        {
            _symGreenText.Value = _symGreenSlider.Value;
        }
        else if (sender == _symBlueSlider)
        {
            _symBlueText.Value = _symBlueSlider.Value;
        }

        UpdateSymbolColor();
    }

    private void OnSymColorTextChanged(Base sender, EventArgs e)
    {
        if (sender == _symRedText)
        {
            _symRedSlider.Value = _symRedText.Value;
        }
        else if (sender == _symGreenText)
        {
            _symGreenSlider.Value = _symGreenText.Value;
        }
        else if (sender == _symBlueText)
        {
            _symBlueSlider.Value = _symBlueText.Value;
        }

        UpdateSymbolColor();
    }

    private void UpdateSymbolColor()
    {
        _symR = (byte)_symRedSlider.Value;
        _symG = (byte)_symGreenSlider.Value;
        _symB = (byte)_symBlueSlider.Value;

        _symbolPreview.RenderColor = Color.FromArgb(255, _symR, _symG, _symB);
    }

    private void UpdateLogoPreview()
    {
        var previewSize = CompositionSize;

        if (_logoElements[0] != null)
        {
            _backgroundPreview.Texture = _logoElements[0];
            var (scaledW, scaledH) = ScaleToFit(_logoElements[0]!.Width, _logoElements[0]!.Height, previewSize, previewSize);
            _backgroundPreview.SetSize(scaledW, scaledH);
            Align.Center(_backgroundPreview);
        }
        else
        {
            _backgroundPreview.SetSize(0, 0);
        }

        if (_logoElements[1] != null)
        {
            _symbolPreview.Texture = _logoElements[1];
            var baseSize = (int)(previewSize * 0.6f);
            var (scaledW, scaledH) = ScaleToFit(_logoElements[1]!.Width, _logoElements[1]!.Height, baseSize, baseSize);
            _symbolPreview.SetSize(scaledW, scaledH);
            Align.Center(_symbolPreview);
        }
        else
        {
            _symbolPreview.SetSize(0, 0);
        }

        _backgroundPreview.RenderColor = Color.FromArgb(255, _bgR, _bgG, _bgB);
        _symbolPreview.RenderColor = Color.FromArgb(255, _symR, _symG, _symB);
    }

    public void Update()
    {
        if (IsHidden)
        {
            return;
        }

        if (!_initializedSymbols)
        {
            _initializedSymbols = true;
            InitializeSymbolPanel();
        }

        if (!_initializedBackgrounds)
        {
            _initializedBackgrounds = true;
            InitializeBackgroundPanel();
        }

        if (!string.IsNullOrEmpty(_guildNameTextbox.Text) && _guildNameTextbox.Text.Length > 20)
        {
            _guildNameTextbox.Text = _guildNameTextbox.Text.Substring(0, 20);
        }
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void Show()
    {
        IsHidden = false;
    }

    private static (int w, int h) ScaleToFit(int originalW, int originalH, int maxW, int maxH)
    {
        if (originalW <= 0 || originalH <= 0)
        {
            return (maxW, maxH);
        }

        var ratio = Math.Min(maxW / (float)originalW, maxH / (float)originalH);
        var width = (int)(originalW * ratio);
        var height = (int)(originalH * ratio);
        return (width, height);
    }
}
