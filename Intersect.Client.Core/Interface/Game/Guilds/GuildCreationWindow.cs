using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Configuration;
using Graphics = Intersect.Client.Core.Graphics;
using System.Text.RegularExpressions;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game.Guilds;

public partial class GuildCreationWindow:Window
{
 
    private TextBox _guildNameTextbox;

    private ImagePanel _logoPanel;
    private Button _symbolButton;
    private Button _backgroundButton;
    private ScrollControl _symbolPanel;
    private ScrollControl _backgroundPanel;
    private Button _createGuildButton;

    private ImagePanel _logoCompositionPanel;
    private ImagePanel _backgroundPreview;
    private readonly ImagePanel _symbolPreview;

    private readonly List<IGameTexture?> _logoElements;

    private bool _initializedSymbols;
    private bool _initializedBackgrounds;

    private string _selectedBackgroundFile = string.Empty;
    private string _selectedSymbolFile = string.Empty;
    private ImagePanel _backgroundContainer;
    private ImagePanel _symbolContainer;
    private ImagePanel BackgroundIconPanel;
    private ImagePanel SymbolIconPanel;
    private const int CompositionSize = 100;
    private ColorSliderGroup _backgroundColorGroup;
    private ColorSliderGroup _symbolColorGroup;
    public GuildCreationWindow(Canvas gameCanvas) : base(gameCanvas, null, false, "GuildCreationWindow")
    {
        
        IsResizable = false;
        SetSize(600, 480);

        // Guild Name TextBox  
        _guildNameTextbox = new TextBox(this, "GuildNameTextbox")
        {
            FontName = "sourcesansproblack",
            FontSize = 12,
            TextColor = Color.FromArgb(255, 0, 0, 0)
        };
        _guildNameTextbox.SetBounds(20, 10, 560, 30);
        Interface.FocusComponents.Add(_guildNameTextbox);

        // Logo selection panel  
        _logoPanel = new ImagePanel(this, "LogoPanel");
        _logoPanel.SetBounds(20, 50, 560, 160);

        _backgroundButton = new Button(_logoPanel, "BackgroundButton")
        {
            Text = "Fondos",
            FontName = "sourcesansproblack",
            FontSize = 12,
        };
        _backgroundButton.SetBounds(0, 0, 280, 30);

        _symbolButton = new Button(_logoPanel, "SymbolButton")
        {
            Text = "Símbolos",
            FontName = "sourcesansproblack",
            FontSize = 12,
        };
        _symbolButton.SetBounds(280, 0, 280, 30);

        _backgroundPanel = new ScrollControl(_logoPanel, "BackgroundPanel");
        _backgroundPanel.SetBounds(0, 30, 560, 130);
        _backgroundPanel.EnableScroll(false, true);

        _symbolPanel = new ScrollControl(_logoPanel, "SymbolPanel");
        _symbolPanel.SetBounds(0, 30, 560, 130);
        _symbolPanel.EnableScroll(false, true);
        _symbolPanel.Hide();

        _symbolContainer = new ImagePanel(_symbolPanel, "SymbolContainer");
        _backgroundContainer = new ImagePanel(_backgroundPanel, "BackgroundContainer");
        _backgroundButton.Clicked += (_, _) => { _backgroundPanel.Show(); _symbolPanel.Hide(); };
        _symbolButton.Clicked += (_, _) => { _symbolPanel.Show(); _backgroundPanel.Hide(); };

        // Logo composition preview  
        _logoCompositionPanel = new ImagePanel(this, "LogoCompositionPanel");
        _logoCompositionPanel.SetBounds(20, 230, CompositionSize + 5, CompositionSize + 5);

        _backgroundPreview = new ImagePanel(_logoCompositionPanel, "BackgroundPreview");
        _backgroundPreview.SetBounds(0, 0, CompositionSize, CompositionSize);
        _backgroundPreview.Show();

        _symbolPreview = new ImagePanel(_logoCompositionPanel, "SymbolPreview");
        _symbolPreview.SetBounds(0, 0, 56, 56);
        _symbolPreview.Show();
        BackgroundIconPanel = new ImagePanel(_backgroundContainer, "BackgroundImage");
        SymbolIconPanel = new ImagePanel(_symbolContainer, "SymbolImage");

        // Create Guild Button  
        _createGuildButton = new Button(this, "CreateGuildButton")
        {
            Text = "Crear Gremio",
            FontName = "sourcesansproblack",
            FontSize = 12,
        };
        // Shifted left  
        _createGuildButton.SetBounds(20, 380, 150, 40);
        _createGuildButton.Clicked += OnCreateGuildButtonClicked;

        _logoElements = new List<IGameTexture?> { null, null };

        CreateBackgroundColorSliders();
        CreateSymbolColorSliders();

        InitializeBackgroundPanel();
        InitializeSymbolPanel();

    }

    protected override void EnsureInitialized()
    {
        // Solo recargar si no está ya cargada
        if (!_initializedSymbols || !_initializedBackgrounds)
        {
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer?.GetResolutionString());
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

        UpdateLogoPreview(); // Reaplica la composición visual
    }


    private void CreateBackgroundColorSliders()
    {
        _backgroundColorGroup = new ColorSliderGroup(this, "Fondo", 210, ApplyBackgroundColor);
    }

    private void CreateSymbolColorSliders()
    {
        _symbolColorGroup = new ColorSliderGroup(this, "Símbolo", 320, ApplySymbolColor);
    }

    private void InitializeBackgroundPanel()
    {
        if (_initializedBackgrounds) return;
        _initializedBackgrounds = true;

        var backgroundFolderPath = Path.Combine(ClientConfiguration.ResourcesDirectory, "Guild", "Background");
        if (!Directory.Exists(backgroundFolderPath))
            Directory.CreateDirectory(backgroundFolderPath);

        var files = Directory.GetFiles(backgroundFolderPath, "*.png");
  
        const int containerSize = 48;
        const int spacing = 5;
        const int xPadding = 5, yPadding = 5;
        int columns = Math.Max(1, _backgroundPanel.Width / (containerSize + spacing + xPadding));

        int index = 0;
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);

            var container = new ImagePanel(_backgroundPanel, "BackgroundContainer");
            container.SetSize(containerSize, containerSize);

            int posX = (index % columns) * (containerSize + spacing) + xPadding;
            int posY = (index / columns) * (containerSize + spacing) + yPadding;
            container.SetPosition(posX, posY);
            container.RenderColor = Color.White;
            container.Show();

            var bgImg = new ImagePanel(container, "BackgroundImage");
            var tex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fileName);
            bgImg.Texture = tex;

            if (tex != null)
            {
                var (scaledW, scaledH) = ScaleToFit(tex.Width, tex.Height, containerSize, containerSize);
                bgImg.SetSize(scaledW, scaledH);
                Align.Center(bgImg);
            }
            else
            {
                bgImg.SetSize(containerSize, containerSize);
            }

            bgImg.Clicked += (_, _) =>
            {
                _logoElements[0] = tex;
                _selectedBackgroundFile = fileName;
                container.RenderColor = Color.Yellow; // resalta el seleccionado

                UpdateLogoPreview();
            };

            index++;
        }
    }

    private void InitializeSymbolPanel()
    {
        if (_initializedSymbols) return;
        _initializedSymbols = true;

        var symbolFolderPath = Path.Combine(ClientConfiguration.ResourcesDirectory, "Guild", "Symbols");
        if (!Directory.Exists(symbolFolderPath))
            Directory.CreateDirectory(symbolFolderPath);

        var files = Directory.GetFiles(symbolFolderPath, "*.png");

        const int containerSize = 48;
        const int spacing = 5;
        const int xPadding = 5, yPadding = 5;
        int columns = Math.Max(1, _symbolPanel.Width / (containerSize + spacing + xPadding));

        int index = 0;
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);

            var container = new ImagePanel(_symbolPanel, "SymbolContainer");
            container.SetSize(containerSize, containerSize);

            int posX = (index % columns) * (containerSize + spacing) + xPadding;
            int posY = (index / columns) * (containerSize + spacing) + yPadding;
            container.SetPosition(posX, posY);
            container.RenderColor = Color.White;
            container.Show();

            var symbolImg = new ImagePanel(container, "SymbolImage");
            var tex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fileName);
            symbolImg.Texture = tex;

            if (tex != null)
            {
                var (scaledW, scaledH) = ScaleToFit(tex.Width, tex.Height, containerSize, containerSize);
                symbolImg.SetSize(scaledW, scaledH);
                Align.Center(symbolImg);
            }
            else
            {
                symbolImg.SetSize(containerSize, containerSize);
            }

            symbolImg.Clicked += (_, _) =>
            {
                _logoElements[1] = tex;
                _selectedSymbolFile = fileName;
                container.RenderColor = Color.Yellow; // resalta el seleccionado

                UpdateLogoPreview();
            };

            index++;
        }
    }

    private void ApplyBackgroundColor(Color color) => _backgroundPreview.RenderColor = color;
    private void ApplySymbolColor(Color color) => _symbolPreview.RenderColor = color;

    private void UpdateLogoPreview()
    {
        var previewSize = CompositionSize;
        if (_logoElements[0] != null)
        {
            _backgroundPreview.Texture = _logoElements[0];
            var (w, h) = ScaleToFit(_logoElements[0]!.Width, _logoElements[0]!.Height, previewSize, previewSize);
            _backgroundPreview.SetSize(w, h);
            Align.Center(_backgroundPreview);
        }
        else _backgroundPreview.SetSize(0, 0);

        if (_logoElements[1] != null)
        {
            _symbolPreview.Texture = _logoElements[1];
            var baseSize = (int)(previewSize * 0.6f);
            var (w, h) = ScaleToFit(_logoElements[1]!.Width, _logoElements[1]!.Height, baseSize, baseSize);
            _symbolPreview.SetSize(w, h);
            Align.Center(_symbolPreview);
        }
        else _symbolPreview.SetSize(0, 0);
    }

    public void Update()
    {
        if (this.IsHidden) return;

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

        if (_guildNameTextbox != null && !string.IsNullOrEmpty(_guildNameTextbox.Text) && _guildNameTextbox.Text.Length > 20)
        {
            _guildNameTextbox.Text = _guildNameTextbox.Text.Substring(0, 20);
        }
    }


    private void OnCreateGuildButtonClicked(Base sender, MouseButtonState args)
    {
        var name = _guildNameTextbox.Text.Trim();

        if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 20 ||
            !Regex.IsMatch(name, @"^[a-zA-Z0-9\s]+$") ||
            string.IsNullOrEmpty(_selectedBackgroundFile) || string.IsNullOrEmpty(_selectedSymbolFile))
        {
            PacketSender.SendChatMsg("Nombre inválido o faltan íconos del gremio.", 5);
            return;
        }

        PacketSender.SendCreateGuild(
            name,
            _selectedBackgroundFile,
           _backgroundColorGroup.R, _backgroundColorGroup.G, _backgroundColorGroup.B,
            _selectedSymbolFile,
          _symbolColorGroup.R, _symbolColorGroup.G, _symbolColorGroup.B
        );
        this.Close();
    }

    public void Hide() => this.IsHidden = true;
    public void Show()
    {
        EnsureInitialized();
        this.IsHidden = false;
    }


    private static (int w, int h) ScaleToFit(int originalW, int originalH, int maxW, int maxH)
    {
        if (originalW <= 0 || originalH <= 0) return (maxW, maxH);
        var ratio = Math.Min(maxW / (float)originalW, maxH / (float)originalH);
        return ((int)(originalW * ratio), (int)(originalH * ratio));
    }

    internal class ColorSliderGroup
    {
        public byte R, G, B;

        private readonly Label _label;
        private readonly Slider _rSlider, _gSlider, _bSlider;
        private readonly TextBoxNumeric _rText, _gText, _bText;

        public ColorSliderGroup(Base parent, string labelText, int yOffset, Action<Color> applyColor)
        {
            _label = new Label(parent)
            {
                Text = $"Color de {labelText}:",
                FontName = "sourcesansproblack",
                FontSize = 12
            };
            _label.SetBounds(220, yOffset, 200, 30);

            _rSlider = CreateSlider(parent, 220, yOffset + 30, val => { R = val; UpdateColor(applyColor); });
            _rText = CreateText(parent, 380, yOffset + 30, val => { _rSlider.Value = val; });

            _gSlider = CreateSlider(parent, 220, yOffset + 60, val => { G = val; UpdateColor(applyColor); });
            _gText = CreateText(parent, 380, yOffset + 60, val => { _gSlider.Value = val; });

            _bSlider = CreateSlider(parent, 220, yOffset + 90, val => { B = val; UpdateColor(applyColor); });
            _bText = CreateText(parent, 380, yOffset + 90, val => { _bSlider.Value = val; });

            R = G = B = 255;
            _rSlider.Value = _gSlider.Value = _bSlider.Value = 255;
            _rText.SetText("255");
            _gText.SetText("255");
            _bText.SetText("255");
        }

        private Slider CreateSlider(Base parent, int x, int y, Action<byte> onChanged)
        {
            var slider = new Slider(parent);
            slider.SetBounds(x, y, 150, 20);
            slider.SetRange(0, 255);
            slider.ValueChanged += (_, _) =>
            {
                onChanged((byte)slider.Value);
            };
            return slider;
        }

        private TextBoxNumeric CreateText(Base parent, int x, int y, Action<int> onSubmit)
        {
            var textbox = new TextBoxNumeric(parent);
            textbox.SetBounds(x, y, 50, 20);
            textbox.SetMaxLength(3);
            textbox.SubmitPressed += (_, _) =>
            {
                if (int.TryParse(textbox.Text, out var val))
                {
                    val = Math.Clamp(val, 0, 255);
                    onSubmit(val);
                    textbox.SetText(val.ToString());
                }
            };
            return textbox;
        }

        private void UpdateColor(Action<Color> applyColor)
        {
            applyColor(Color.FromArgb(255, R, G, B));
        }
    }

}




