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

namespace Intersect.Client.Interface.Game.Guilds;

public partial class GuildCreationWindow : Window
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

    private ColorSliderGroup _backgroundSliders;
    private ColorSliderGroup _symbolSliders;

    private string _selectedBackgroundFile = string.Empty;
    private string _selectedSymbolFile = string.Empty;
    private ImagePanel _backgroundContainer;
    private ImagePanel _symbolContainer;
    private ImagePanel BackgroundIconPanel;
    private ImagePanel SymbolIconPanel;
    private const int CompositionSize = 100;

    public GuildCreationWindow(Canvas gameCanvas)
        : base(gameCanvas, Strings.Guilds.Guild, false, nameof(GuildCreationWindow))
    {
        DisableResizing();
        // Compact window width
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

        // Compact sliders: background at y=230, symbol at y=300
        _backgroundSliders = new ColorSliderGroup(this, "Fondo", 210, ApplyBackgroundColor);
        _symbolSliders = new ColorSliderGroup(this, "Símbolo", 320, ApplySymbolColor);
        InitializeBackgroundPanel();
        InitializeSymbolPanel();
    }
    private void InitializeBackgroundPanel()
    {
        if (_initializedBackgrounds) return;
        _initializedBackgrounds = true;

        var path = Path.Combine(ClientConfiguration.ResourcesDirectory, "Guild", "Background");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
        const int size = 48;
        int x = 5, y = 5;
        int maxW = _backgroundPanel.Width;

        foreach (var file in files)
        {
            var fn = Path.GetRelativePath(path, file).Replace("\\", "/");
         
            var tex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fn);
            if (tex == null) continue;

            if (x + size > maxW)
            {
                x = 5;
                y += size + 5;
            }

           _backgroundContainer = new ImagePanel(_backgroundPanel, "BackgroundContainer");
       
            _backgroundContainer.SetBounds(x, y, size, size);
            _backgroundContainer.RenderColor = Color.White;
            _backgroundContainer.Show();

           BackgroundIconPanel = new ImagePanel(_backgroundContainer, "BackgroundImage")
            {
               Texture = tex
            };

            var (w, h) = ScaleToFit(tex.Width, tex.Height, size, size);
            BackgroundIconPanel.SetSize(w, h);
            Align.Center(BackgroundIconPanel);

            BackgroundIconPanel.Clicked += (_, _) =>
            {
                _logoElements[0] = tex;
                _selectedBackgroundFile = fn;

                UpdateLogoPreview();
            };

            x += size + 5;
        }
    }

    private void InitializeSymbolPanel()
    {
        if (_initializedSymbols) return;
        _initializedSymbols = true;

        var path = Path.Combine(ClientConfiguration.ResourcesDirectory, "Guild", "Symbols");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
    
        const int size = 48;
        int x = 5, y = 5;
        int maxW = _symbolPanel.Width;

        foreach (var file in files)
        {
            var fn = Path.GetRelativePath(path, file).Replace("\\", "/");
        
            var tex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fn);
            if (tex == null) continue;

            if (x + size > maxW)
            {
                x = 5;
                y += size + 5;
            }

           _symbolContainer = new ImagePanel(_symbolPanel, "SymbolContainer");
            _symbolContainer.SetBounds(x, y, size, size);
            _symbolContainer.RenderColor = Color.White;
            _symbolContainer.Show();

          SymbolIconPanel = new ImagePanel(_symbolContainer, "SymbolImage")
            {
               Texture = tex
            };

            var (w, h) = ScaleToFit(tex.Width, tex.Height, size, size);
            SymbolIconPanel.SetSize(w, h);
            Align.Center(SymbolIconPanel);

            SymbolIconPanel.Clicked += (_, _) =>
            {
                _logoElements[1] = tex;
                _selectedSymbolFile = fn;

                UpdateLogoPreview();
            };

            x += size + 5;
        }
    }

    protected override void EnsureInitialized()
    {
        InitializeBackgroundPanel();  // 👈 carga solo los fondos
        InitializeSymbolPanel();      // 👈 carga solo los símbolos

        _backgroundPanel.Show();
        _symbolPanel.Hide();

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
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

        if (_guildNameTextbox != null && !string.IsNullOrEmpty(_guildNameTextbox.Text) && _guildNameTextbox.Text.Length > 20)
        {
            _guildNameTextbox.Text = _guildNameTextbox.Text.Substring(0, 20);
        }
    }


    private void OnCreateGuildButtonClicked(Base sender, MouseButtonState args)
    {
        var name = _guildNameTextbox.Text.Trim();
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(_selectedBackgroundFile) || string.IsNullOrEmpty(_selectedSymbolFile))
        {
            PacketSender.SendChatMsg("Falta completar el nombre o los íconos del gremio.", 5);
            return;
        }

        PacketSender.SendCreateGuild(
            name,
            _selectedBackgroundFile,
            _backgroundSliders.R, _backgroundSliders.G, _backgroundSliders.B,
            _selectedSymbolFile,
            _symbolSliders.R, _symbolSliders.G, _symbolSliders.B
        );
        Close();
    }

    public void Hide() => IsHidden = true;
    public void Show() => IsHidden = false;

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

        public ColorSliderGroup(Base parent, string label, int yOffset, Action<Color> applyColor)
        {
            var xOffset = 250;
            _label = new Label(parent)
            {
                Text = $"Color de {label}:",
                FontName = "sourcesansproblack",
                FontSize = 12
            };
            _label.SetBounds(xOffset, yOffset, 180, 20);

            AddSlider(parent, xOffset, yOffset + 25, "Red", val => R = val, applyColor);
            AddSlider(parent, xOffset, yOffset + 55, "Green", val => G = val, applyColor);
            AddSlider(parent, xOffset, yOffset + 85, "Blue", val => B = val, applyColor);
        }

        private void AddSlider(Base parent, int x, int y, string component, Action<byte> setter, Action<Color> applyColor)
        {
            var slider = new Slider(parent) { Name = component + "Slider" };
            slider.SetBounds(x, y, 280, 20);
            slider.SetRange(0, 255);
            slider.ValueChanged += (_, _) =>
            {
                setter((byte)slider.Value);
                applyColor(Color.FromArgb(255, R, G, B));
            };
        }
    }
}




