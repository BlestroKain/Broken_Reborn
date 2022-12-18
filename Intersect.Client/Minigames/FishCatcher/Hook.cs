using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Minigames;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Minigames.FishCatcher
{
    sealed class Hook : MinigameEntity
    {
        private FishCatcherGame Game { get; set; }

        private GameTexture HookTexture { get; set; }
        private FloatRect HookSrc;

        private Pointf Destination;

        public float X { get; set; }
        public float Y { get; set; }
        public double Dir { get; set; }

        public float MinSpeed { get; set; } = 0.5f;

        public long LastUpdate { get; set; }
        public long Delta { get; set; }

        public float Accel = 0.2f;

        public float MaxSpeed { get; set; } = 1.5f;

        public Hook(FishCatcherGame game)
        {
            Game = game;
            HookTexture = Minigame.LoadMinigameTexture("fishing_hook.png", ref HookSrc);
            X = Graphics.CurrentView.CenterX - HookTexture.Center.X;
            Y = Graphics.CurrentView.CenterY - HookTexture.Center.Y;
        }

        public override void Draw(long timeMs)
        {
            var mousePos = Game.MousePos;
            var delta = Game.Delta / 5f;

            SetDestination(mousePos);

            Dir = MathHelper.AngleBetween(X, Y, mousePos.X, mousePos.Y);
            var xVel = MaxSpeed * (float)MathHelper.DCos(Dir) * delta;
            var yVel = MaxSpeed * (float)MathHelper.DSin(Dir) * delta;

            var desiredX = X + xVel;
            var desiredY = Y + yVel;

            desiredX = ClampToGameX(desiredX);
            desiredY = ClampToGameY(desiredY);

            X = desiredX;
            Y = desiredY;

            var drawDest = new FloatRect(X, Y, HookTexture.Width, HookTexture.Height);
            Graphics.DrawGameTexture(HookTexture, HookSrc, drawDest, Color.White);
        }

        private float ClampToGameX(float val)
        {
            return (float)MathHelper.Clamp(val, Game.X, Game.X + Game.Width - HookTexture.Width);
        }

        private float ClampToGameY(float val)
        {
            return (float)MathHelper.Clamp(val, Game.Y, Game.Y + Game.Height - HookTexture.Height);
        }

        private void SetDestination(Pointf mousePos)
        {
            Destination = new Pointf(mousePos.X - HookTexture.Width / 2, mousePos.Y - HookTexture.Height / 2);
            Destination.X = ClampToGameX(Destination.X);
            Destination.Y = ClampToGameY(Destination.Y);
        }
    }
}
