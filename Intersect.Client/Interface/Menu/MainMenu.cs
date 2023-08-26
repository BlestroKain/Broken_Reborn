using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Logging;
using Intersect.Network;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Menu
{

    public partial class MainMenu : MutableInterface
    {

        public delegate void NetworkStatusHandler();

        public static NetworkStatus ActiveNetworkStatus;

        public static NetworkStatusHandler NetworkStatusChanged;

        private readonly CreateCharacterWindow mCreateCharacterWindow;

        private readonly Button mCreditsButton;

        private readonly CreditsWindow mCreditsWindow;

        private readonly Button mExitButton;

        private readonly ForgotPasswordWindow mForgotPasswordWindow;

        private readonly Button mLoginButton;

        private readonly Button mDiscordButton;

        //Controls
        private readonly Canvas mMenuCanvas;

        private readonly Label mMenuHeader;

        private readonly ImagePanel mMenuWindow;

        private readonly Button mSettingsButton;

        private readonly SettingsWindow mSettingsWindow;

        private readonly Button mRegisterButton;

        private readonly RegisterWindow mRegisterWindow;

        private readonly ResetPasswordWindow mResetPasswordWindow;

        private readonly SelectCharacterWindow mSelectCharacterWindow;

        private readonly Label mServerStatusLabel;
        private readonly ImagePanel mLoginContainer;

        //Character creation feild check
        private bool mHasMadeCharacterCreation;

        private bool mShouldOpenCharacterCreation;

        private bool mShouldOpenCharacterSelection;

        private Button mForgotPassswordButton;
      
        private Label mLoginHeader;

        //Parent
        private ImagePanel mPasswordBackground;

        private Label mPasswordLabel;

        private TextBoxPassword mPasswordTextbox;

        private string mSavedPass = "";

        private LabeledCheckBox mSavePassChk;

        //Controls
        private ImagePanel mUsernameBackground;

        private Label mUsernameLabel;

        private TextBox mUsernameTextbox;

        private bool mUseSavedPass;


        //Init
        public MainMenu(Canvas menuCanvas) : base(menuCanvas)
        {
            mMenuCanvas = menuCanvas;

            var logo = new ImagePanel(menuCanvas, "Logo");
            logo.LoadJsonUi(GameContentManager.UI.Menu, Graphics.Renderer.GetResolutionString());

            //Main Menu Window
            mMenuWindow = new ImagePanel(menuCanvas, "MenuWindow");

            mServerStatusLabel = new Label(mMenuWindow, "ServerStatusLabel")
            {
                AutoSizeToContents = true,
                ShouldDrawBackground = true,
                Text = Strings.Server.StatusLabel.ToString(ActiveNetworkStatus.ToLocalizedString()),
            };

            mServerStatusLabel.SetTextColor(Color.White, Label.ControlState.Normal);
            mServerStatusLabel.AddAlignment(Alignments.Bottom);
            mServerStatusLabel.AddAlignment(Alignments.Left);
            mServerStatusLabel.ProcessAlignments();

            NetworkStatusChanged += HandleNetworkStatusChanged;

            //Menu Header
            mMenuHeader = new Label(mMenuWindow, "Title");
            mMenuHeader.SetText(Strings.MainMenu.Title);          

            //Register Button
            mRegisterButton = new Button(mMenuWindow, "RegisterButton");
            mRegisterButton.SetText(Strings.MainMenu.Register);
            mRegisterButton.Clicked += RegisterButton_Clicked;

            //Credits Button
            mCreditsButton = new Button(mMenuWindow, "CreditsButton");
            mCreditsButton.SetText(Strings.MainMenu.Credits);
            mCreditsButton.Clicked += CreditsButton_Clicked;

            //Exit Button
            mExitButton = new Button(mMenuWindow, "ExitButton");
            mExitButton.SetText(Strings.MainMenu.Exit);
            mExitButton.Clicked += ExitButton_Clicked;

            //Settings Button
            mSettingsButton = new Button(mMenuWindow, "SettingsButton");
            mSettingsButton.Clicked += SettingsButton_Clicked;
            mSettingsButton.SetText(Strings.MainMenu.Settings);
            if (!string.IsNullOrEmpty(Strings.MainMenu.SettingsTooltip))
            {
                mSettingsButton.SetToolTipText(Strings.MainMenu.SettingsTooltip);
            }
            mDiscordButton = new Button(mMenuWindow, "DiscordButton");
            mDiscordButton.SetText(Strings.MainMenu.Discord);
            mDiscordButton.Clicked += DiscordButton_Clicked;
            mDiscordButton.SetPosition(338, 54);
            mDiscordButton.SetSize(96, 35);

            mLoginContainer = new ImagePanel(mMenuWindow, "LoginContainer");

            // Set the position and size of the login container
            mLoginContainer.SetPosition(457, 317);
            mLoginContainer.SetSize(452, 158);

            // Login Button
            mLoginButton = new Button(mLoginContainer, "LoginButton");
            mLoginButton.SetText(Strings.MainMenu.Login);
            mLoginButton.Clicked += LoginButton_Clicked;
            mLoginButton.SetPosition(338, 54);
            mLoginButton.SetSize(96, 35);

            // Username Panel
            mUsernameBackground = new ImagePanel(mLoginContainer, "UsernamePanel");
            mUsernameBackground.SetPosition(14, 54);
            mUsernameBackground.SetSize(308, 28);

            // Username Label
            mUsernameLabel = new Label(mUsernameBackground, "UsernameLabel");
            mUsernameLabel.SetText(Strings.Login.username);
            mUsernameLabel.SetPosition(0, -4);
            mUsernameLabel.SetSize(176, 20);

            // Username Textbox
            mUsernameTextbox = new TextBox(mUsernameBackground, "UsernameField");
            mUsernameTextbox.SubmitPressed += UsernameTextbox_SubmitPressed;
            mUsernameTextbox.Clicked += _usernameTextbox_Clicked;
            mUsernameTextbox.SetPosition(4, 10);
            mUsernameTextbox.SetSize(302, 16);

            // Password Panel
            mPasswordBackground = new ImagePanel(mLoginContainer, "PasswordPanel");
            mPasswordBackground.SetPosition(14, 90 );
            mPasswordBackground.SetSize(308, 28);

            // Password Label
            mPasswordLabel = new Label(mPasswordBackground, "PasswordLabel");
            mPasswordLabel.SetText(Strings.Login.password);
            mPasswordLabel.SetPosition(0, -4);
            mPasswordLabel.SetSize(176, 20);

            // Password Textbox
            mPasswordTextbox = new TextBoxPassword(mPasswordBackground, "PasswordField");
            mPasswordTextbox.SubmitPressed += PasswordTextbox_SubmitPressed;
            mPasswordTextbox.TextChanged += _passwordTextbox_TextChanged;
            mPasswordTextbox.SetPosition(4, 10);
            mPasswordTextbox.SetSize(302, 16);

            // Save Pass Checkbox
            mSavePassChk = new LabeledCheckBox(mLoginContainer, "SavePassCheckbox") { Text = Strings.Login.savepass };
            mSavePassChk.SetPosition(13, 124);
            mSavePassChk.SetSize(160, 24);

            // Forgot Password Button
            mForgotPassswordButton = new Button(mLoginContainer, "ForgotPasswordButton");
            mForgotPassswordButton.IsHidden = true;
            mForgotPassswordButton.SetText(Strings.Login.forgot);
            mForgotPassswordButton.Clicked += mForgotPassswordButton_Clicked;
            mForgotPassswordButton.SetPosition(214, 130);
            mForgotPassswordButton.SetSize(104, 16);



            LoadCredentials();
            mMenuWindow.LoadJsonUi(GameContentManager.UI.Menu, Graphics.Renderer.GetResolutionString());

            //Settings Controls
            mSettingsWindow = new SettingsWindow(menuCanvas, this, null);

           //Register Controls
            mRegisterWindow = new RegisterWindow(menuCanvas, this, mMenuWindow);

            //Forgot Password Controls
            mForgotPasswordWindow = new ForgotPasswordWindow(menuCanvas, this, mMenuWindow);

            //Reset Password Controls
            mResetPasswordWindow = new ResetPasswordWindow(menuCanvas, this, mMenuWindow);

            //Character Selection Controls
            mSelectCharacterWindow = new SelectCharacterWindow(mMenuCanvas, this, mMenuWindow);

            //Character Creation Controls
            mCreateCharacterWindow = new CreateCharacterWindow(mMenuCanvas, this, mMenuWindow, mSelectCharacterWindow);

            //Credits Controls
            mCreditsWindow = new CreditsWindow(mMenuCanvas, this);

            UpdateDisabled();
        }

        private void DiscordButton_Clicked(object sender, EventArgs e)
        {
            // Launch browser to ascensiongamedev...
            System.Diagnostics.Process.Start("https://discord.gg/k2sdk7hFYS");
        }

        ~MainMenu()
        {
            // ReSharper disable once DelegateSubtraction
            NetworkStatusChanged -= HandleNetworkStatusChanged;
        }
        private void mForgotPassswordButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Interface.MenuUi.MainMenu.NotifyOpenForgotPassword();
        }

        private void _usernameTextbox_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Globals.InputManager.OpenKeyboard(
                KeyboardType.Normal, mUsernameTextbox.Text, false, false, false
            );
        }

        //Methods
        public void Update()
        {
            if (mShouldOpenCharacterSelection)
            {
                CreateCharacterSelection();
            }

            if (mShouldOpenCharacterCreation)
            {
                CreateCharacterCreation();
            }

           

            // Re-Enable our buttons button if we're not waiting for the server anymore with it disabled.
            if (!Globals.WaitingOnServer && mLoginButton.IsDisabled)
            {
                mLoginButton.Enable();
            }

            if (!mCreateCharacterWindow.IsHidden)
            {
                mCreateCharacterWindow.Update();
            }

            if (!mRegisterWindow.IsHidden)
            {
                mRegisterWindow.Update();
            }

            if (!mSelectCharacterWindow.IsHidden)
            {
                mSelectCharacterWindow.Update();
            }

            mSettingsWindow.Update();
        }

        public void Reset()
        {
        
            mRegisterWindow.Hide();
            mSettingsWindow.Hide();
            mCreditsWindow.Hide();
            mForgotPasswordWindow.Hide();
            mResetPasswordWindow.Hide();
            if (mCreateCharacterWindow != null)
            {
                mCreateCharacterWindow.Hide();
            }

            if (mSelectCharacterWindow != null)
            {
                mSelectCharacterWindow.Hide();
            }

            mMenuWindow.Show();
            mSettingsButton.Show();
        }

        public void Show()
        {
            mMenuWindow.IsHidden = false;
            mSettingsButton.IsHidden = false;
            if (!mForgotPassswordButton.IsHidden)
            {
                mForgotPassswordButton.IsHidden = !Options.Instance.SmtpValid;
            }

            // Set focus to the appropriate elements.
            if (!string.IsNullOrWhiteSpace(mUsernameTextbox.Text))
            {
                mPasswordTextbox.Focus();
            }
            else
            {
                mUsernameTextbox.Focus();
            }
        }

        public void Hide()
        {
            mMenuWindow.IsHidden = true;
            mSettingsButton.IsHidden = true;
        }

        public void NotifyOpenCharacterSelection(List<Character> characters)
        {
            mShouldOpenCharacterSelection = true;
            mSelectCharacterWindow.Characters = characters;
        }

        public void NotifyOpenForgotPassword()
        {
            Reset();
            Hide();
            mForgotPasswordWindow.Show();
        }

        public void OpenResetPassword(string nameEmail)
        {
            Reset();
            Hide();
            mResetPasswordWindow.Target = nameEmail;
            mResetPasswordWindow.Show();
        }

        public void CreateCharacterSelection()
        {
            Hide();
          
            mRegisterWindow.Hide();
            mSettingsWindow.Hide();
            mCreateCharacterWindow.Hide();
            mSelectCharacterWindow.Show();
            mShouldOpenCharacterSelection = false;
        }

        public void NotifyOpenCharacterCreation()
        {
            mShouldOpenCharacterCreation = true;
        }

        public void CreateCharacterCreation()
        {
            Hide();
        
            mRegisterWindow.Hide();
            mSettingsWindow.Hide();
            mSelectCharacterWindow.Hide();
            mCreateCharacterWindow.Show();
            mCreateCharacterWindow.Init();
            mHasMadeCharacterCreation = true;
            mShouldOpenCharacterCreation = false;
        }

        //Input Handlers
        void LoginButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            TryLogin();
        }
        //Input Handlers
        void _passwordTextbox_TextChanged(Base sender, EventArgs arguments)
        {
            mUseSavedPass = false;
        }

       void UsernameTextbox_SubmitPressed(Base sender, EventArgs arguments)
        {
            TryLogin();
        }

        void PasswordTextbox_SubmitPressed(Base sender, EventArgs arguments)
        {
            TryLogin();
        }
        public void TryLogin()
        {
            if (Globals.WaitingOnServer)
            {
                return;
            }

           
            if (!FieldChecking.IsValidUsername(mUsernameTextbox?.Text, Strings.Regex.username))
            {
                Interface.MsgboxErrors.Add(new KeyValuePair<string, string>("", Strings.Errors.usernameinvalid));

                return;
            }

            if (!FieldChecking.IsValidPassword(mPasswordTextbox?.Text, Strings.Regex.password))
            {
                if (!mUseSavedPass)
                {
                    Interface.MsgboxErrors.Add(new KeyValuePair<string, string>("", Strings.Errors.passwordinvalid));

                    return;
                }
            }

            var password = mSavedPass;
            if (!mUseSavedPass)
            {
                password = ComputePasswordHash(mPasswordTextbox?.Text?.Trim());
            }

            PacketSender.SendLogin(mUsernameTextbox?.Text, password);
            SaveCredentials();
            Globals.WaitingOnServer = true;
            mLoginButton.Disable();
            ChatboxMsg.ClearMessages();
        }

        private void LoadCredentials()
        {
            var name = Globals.Database.LoadPreference("Username");
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            mUsernameTextbox.Text = name;
            var pass = Globals.Database.LoadPreference("Password");
            if (string.IsNullOrEmpty(pass))
            {
                return;
            }

            mPasswordTextbox.Text = "****************";
            mSavedPass = pass;
            mUseSavedPass = true;
            mSavePassChk.IsChecked = true;
        }

        private static string ComputePasswordHash(string password)
        {
            using (var sha = new SHA256Managed())
            {
                return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""))).Replace("-", "");
            }
        }

        private void SaveCredentials()
        {
            var username = "";
            var password = "";

            if (mSavePassChk.IsChecked)
            {
                username = mUsernameTextbox?.Text?.Trim();
                password = mUseSavedPass ? mSavedPass : ComputePasswordHash(mPasswordTextbox?.Text?.Trim());
            }

            Globals.Database.SavePreference("Username", username);
            Globals.Database.SavePreference("Password", password);
        }

        void RegisterButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Hide();
            mRegisterWindow.Show();
        }

        void CreditsButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Hide();
            mCreditsWindow.Show();
        }

        void SettingsButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Hide();
            mSettingsWindow.Show(true);
        }

        void ExitButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Log.Info("User clicked exit button.");
            Globals.IsRunning = false;
        }

        private void HandleNetworkStatusChanged()
        {
            mServerStatusLabel.Text = Strings.Server.StatusLabel.ToString(ActiveNetworkStatus.ToLocalizedString());
            UpdateDisabled();
        }

        private void UpdateDisabled()
        {
            mLoginButton.IsDisabled = ActiveNetworkStatus != NetworkStatus.Online;
            mRegisterButton.IsDisabled = ActiveNetworkStatus != NetworkStatus.Online ||
                                         Options.Loaded && Options.BlockClientRegistrations;
        }

        public static void OnNetworkConnecting()
        {
            ActiveNetworkStatus = NetworkStatus.Connecting;
        }

        public static void SetNetworkStatus(NetworkStatus networkStatus)
        {
            ActiveNetworkStatus = networkStatus;
            NetworkStatusChanged?.Invoke();
        }

    }

}
