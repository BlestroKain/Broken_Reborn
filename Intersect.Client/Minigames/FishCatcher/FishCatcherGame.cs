using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Minigames;
using Intersect.Client.Minigames.FishCatcher;
using Intersect.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Minigames
{
    public class FishCatcherGame : Minigame
    {
        public override MinigameType Type { get => MinigameType.FishCatcher; }
        public override MinigameBackdrop Background { get; set; }
        public override bool Done { get; set; }

        public GameTexture GameBackground { get; set; }
        private FloatRect GameBgSrc;

        private Hook Hook;

        public Pointf MousePos { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public FishCatcherGame()
        {
            GameBackground = LoadMinigameTexture("fishing_bg.png", ref GameBgSrc);
            MousePos = new Pointf();
            Hook = new Hook(this);
        }

        public override void Draw(long timeMs)
        {
            Background.Draw();

            X = Background.GameX;
            Y = Background.GameY;
            Width = GameBackground.Width;
            Height = GameBackground.Height;

            var bgDest = new FloatRect(X, Y, Width, Height);
            Graphics.DrawGameTexture(GameBackground, GameBgSrc, bgDest, Color.White);

            Hook.Draw(timeMs);
        }

        public override void Start()
        {
            Background = new MinigameBackdrop();
        }

        public override void Update(long timeMs)
        {
            MousePos = UiHelper.GetViewMousePos();
        }

        public override void End()
        {
            
        }
    }
}
