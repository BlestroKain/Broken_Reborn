using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;

namespace Intersect.Client.Interface.Game.Minigames
{
    public class MinigameBackdrop
    {
        private static float ViewX => Graphics.CurrentView.X;
        private static float ViewCenterX => Graphics.CurrentView.CenterX;
        private static float ViewCenterY => Graphics.CurrentView.CenterY;
        private static float ViewTop => Graphics.CurrentView.Top;
        private static float ViewBottom => Graphics.CurrentView.Bottom;
        private static float ViewWidth => Graphics.CurrentView.Width;

        private GameTexture BackgroundTexture { get; set; }

        public FloatRect BackgroundSrc { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float GameX { get; set; }
        public float GameY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public MinigameBackdrop()
        {
            BackgroundTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Minigame, "minigame_bg.png");
            Width = BackgroundTexture.Width;
            Height = BackgroundTexture.Height;
            BackgroundSrc = new FloatRect(0, 0, Width, Height);
        }

        public void Draw()
        {
            X = ViewCenterX - (BackgroundTexture.Width / 2);
            Y = ViewCenterY - (BackgroundTexture.Height / 2);
            GameX = X + 16;
            GameY = Y + 16;
            var dest = new FloatRect(X, Y, BackgroundTexture.Width, BackgroundTexture.Height);
            Graphics.DrawGameTexture(BackgroundTexture, BackgroundSrc, dest, Color.White);
        }
    }
}
