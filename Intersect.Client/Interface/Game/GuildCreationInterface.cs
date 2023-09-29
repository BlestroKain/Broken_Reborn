using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;

using Intersect.Utilities;
using Graphics = Intersect.Client.Core.Graphics;

namespace Intersect.Client.Interface.Game
{
    class GuildCreationInterface
    {
        private WindowControl mCreateGuildWindow;
        private TextBox mGuildNameTextbox;
        private ImagePanel mSymbolPanel;
        private ImagePanel mBackgroundPanel;
        //private ColorPicker mColorPicker;
        private Button mSymbolButton;
        private Button mBackgroundButton;
        private ImagePanel mLogoPanel;
        private List<GameTexture> mLogoElements;  // Lista para almacenar símbolo y fondo
        private ImagePanel mLogoCompositionPanel;  // Panel para mostrar la composición
        public Player Me { get; }

        public GuildCreationInterface(Canvas gameCanvas)
        {
            mCreateGuildWindow = new WindowControl(gameCanvas, Strings.Inventory.Title, false, "CreateGuildWindow");
            mCreateGuildWindow.DisableResizing();
            mCreateGuildWindow.SetSize(600, 500);

            mGuildNameTextbox = new TextBox(mCreateGuildWindow, "GuildName");
            mGuildNameTextbox.SetBounds(50, 10, 200, 30);

            mLogoPanel = new ImagePanel(mCreateGuildWindow, "LogoPanel");
            mLogoPanel.SetBounds(50, 50, 450, 200);

            mSymbolPanel = new ImagePanel(mLogoPanel, "SymbolsPanel");
            mSymbolPanel.SetBounds(0, 40, 450, 160);

            mBackgroundPanel = new ImagePanel(mLogoPanel, "BackgroundPanel");
            mBackgroundPanel.SetBounds(0, 40, 450, 160);

            mSymbolButton = new Button(mLogoPanel, "SymbolButton");
            mSymbolButton.SetText("Símbolo");
            mSymbolButton.SetBounds(0, 0, 225, 40);
            mSymbolButton.Clicked += SymbolButton_Clicked;

            mBackgroundButton = new Button(mLogoPanel, "BackgroundButton");
            mBackgroundButton.SetText("Fondo");
            mBackgroundButton.SetBounds(225, 0, 225, 40);
            mBackgroundButton.Clicked += BackgroundButton_Clicked;

            mLogoCompositionPanel = new ImagePanel(mCreateGuildWindow, "LogoCompositionPanel");
            mLogoCompositionPanel.SetBounds(300, 260, 50, 50);

            mLogoElements = new List<GameTexture>(2) { null, null };

            //mColorPicker = new ColorPicker(mCreateGuildWindow);
            //mColorPicker.SetBounds(50, 260, 200, 200);
            InitializeSymbolPanel();
            InitializeBackgroundPanel();
            // Configuración inicial de los paneles
            mSymbolPanel.Show();
            mBackgroundPanel.Hide();

            // Cargar la UI
            mCreateGuildWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public GuildCreationInterface(Player me)
        {
            Me = me;
        }

        private void SymbolButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mSymbolPanel.Show();
            mBackgroundPanel.Hide();
        }

        private void BackgroundButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mSymbolPanel.Hide();
            mBackgroundPanel.Show();
        }

        private void InitializeSymbolPanel()
        {
            string symbolFolderPath = "resources/Guild/Symbols";

            if (!Directory.Exists(symbolFolderPath))
            {
                // Crear el directorio si no existe
                Directory.CreateDirectory(symbolFolderPath);
                Console.WriteLine($"Folder {symbolFolderPath} has been created!");
            }

            var files = Directory.GetFiles(symbolFolderPath, "*.png");

            int x = 5, y = 5;
            int spacing = 5;
           
            var symbols = new List<ImagePanel>();

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var symbolImage = new ImagePanel(mSymbolPanel, "SymbolImage");
                symbolImage.Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fileName);
                if (symbolImage.Texture != null)
                {
                    symbolImage.SetSize(symbolImage.Texture.GetWidth(), symbolImage.Texture.GetHeight());
                    Align.Center(symbolImage);  // Centrar si lo necesitas

                    // ... (puedes continuar con el resto del código de 'Setup' si es necesario)
                }
                symbolImage.SetBounds(x, y, 32, 32);

                symbolImage.Show(); // Muestra la imagen

                symbolImage.Clicked += (sender, args) =>
                {
                    OnSymbolSelected(symbolImage.Texture);
                };

                symbols.Add(symbolImage);

                x += symbolImage.Width + spacing;
                if (x + symbolImage.Width > mSymbolPanel.Width)
                {
                    x = 0;
                    y += symbolImage.Height + spacing;
                }
            }

            // Aquí puedes configurar las propiedades adicionales de symbols si lo necesitas
        }


        private void InitializeBackgroundPanel()
        {
            string backgroundFolderPath = "resources/Guild/Background";

            if (!Directory.Exists(backgroundFolderPath))
            {
                // Crear el directorio si no existe
                Directory.CreateDirectory(backgroundFolderPath);
                Console.WriteLine($"Folder {backgroundFolderPath} has been created!");
            }

            var files = Directory.GetFiles(backgroundFolderPath, "*.png");

            int x = 5, y = 5;
            int spacing = 5;

            var backgrounds = new List<ImagePanel>();
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
            var backgroundImage = new ImagePanel(mBackgroundPanel, "BackgroundImage");
                backgroundImage.Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Guild, fileName);
                backgroundImage.SetBounds(x, y, 32, 32);

                backgroundImage.Clicked += (sender, args) =>
                {
                    OnBackgroundSelected(backgroundImage.Texture);
                };

                backgrounds.Add(backgroundImage);

                x += backgroundImage.Width + spacing;
                if (x + backgroundImage.Width > mBackgroundPanel.Width)
                {
                    x = 0;
                    y += backgroundImage.Height + spacing;
                }
            }

            // Aquí puedes configurar las propiedades adicionales de backgrounds si lo necesitas
        }

        public void AssignGameTextureToImagePanel(GameTexture gameTexture, ImagePanel imagePanel)
        {
            if (imagePanel != null && gameTexture != null)
            {
                // Suponiendo que ImagePanel tiene un método o una propiedad para asignar la textura
                imagePanel.Texture = gameTexture;
            }
            else
            {
                // Manejar casos en los que imagePanel o gameTexture es null
                Console.WriteLine("ImagePanel or GameTexture is null!");
            }
        }

        private void ComposeLogo()
        {
            if (mLogoElements[0] != null && mLogoElements[1] != null)
            {
                var composedTexture = GraphicsHelper.Compose(mLogoElements[0], mLogoElements[1]);

                // Asegúrate de que mLogoCompositionPanel está inicializado
                if (mLogoCompositionPanel == null)
                {
                    Console.WriteLine("mLogoCompositionPanel is null!");
                    return;
                }

                // Asignar composedTexture a mLogoCompositionPanel
                AssignGameTextureToImagePanel(composedTexture, mLogoCompositionPanel);
            }
        }



        // Cuando se selecciona un símbolo o un fondo, actualiza mLogoElements y llama a ComposeLogo.
        private void OnSymbolSelected(GameTexture symbol)
        {
            mLogoElements[1] = symbol;  
            ComposeLogo();
        }

        private void OnBackgroundSelected(GameTexture background)
        {
            mLogoElements[0] = background;  
            ComposeLogo();
        }

        public void Hide()
        {
            mCreateGuildWindow.IsHidden = true;
        }

        public void Show()
        {
            mCreateGuildWindow.IsHidden = false;
        }
    }
}
