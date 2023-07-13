using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Localization;
using Intersect.Client.Networking;

namespace Intersect.Client.Interface.Game
{
    public class DeathWindow
    {
        private WindowControl mDeathWindow;

        private Button mRespawnButton;

        private Label mLabel;

        private long mDisplayUntil = 0;

        /// <summary>
        /// Indicates whether the control is hidden.
        /// </summary>
        public bool IsHidden
        {
            get { return mDeathWindow.IsHidden; }
            set { mDeathWindow.IsHidden = value; }
        }

        /// <summary>
        /// Create a new instance of the <see cref="DeathWindow"/> class.
        /// </summary>
        /// <param name="gameCanvas">The <see cref="Canvas"/> to render this control on.</param>
        public DeathWindow(Canvas gameCanvas)
        {
            mDeathWindow = new WindowControl(gameCanvas, Strings.Death.DeathWindowTitle, false, "DeathWindow");
            mRespawnButton = new Button(mDeathWindow, "RespawnButton");
            mLabel = new Label(mDeathWindow, "DeathLabel");

            mDeathWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            mDeathWindow.SetPosition(
                Graphics.Renderer.GetScreenWidth() / 2 - mDeathWindow.Width / 2,
                Graphics.Renderer.GetScreenHeight() / 2 - mDeathWindow.Height / 2
            );
            mDeathWindow.DisableResizing();

            mLabel.Text = Strings.Death.DeathMsg;

            mRespawnButton.Text = Strings.Death.RespawnButton;
            mRespawnButton.Clicked += _ressurectButton_Clicked;

            Hide();
        }

        void _ressurectButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendRespawn();
            Hide();
        }

        /// <summary>
        /// Update this control..
        /// </summary>
        public void Update()
        {
            if (mDeathWindow.IsHidden)
            {
                return;
            }
        }

        /// <summary>
        /// Hides the control.
        /// </summary>
        public void Hide()
        {
            mDeathWindow.Hide();
        }

        /// <summary>
        /// Shows the control.
        /// </summary>
        public void Show()
        {
            mDeathWindow.Show();
        }
    }
}