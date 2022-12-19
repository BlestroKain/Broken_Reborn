using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Minigames
{
    public abstract class MinigameEntity<T> where T : Minigame
    {
        protected T Game { get; set; }
        public abstract void Draw(long timeMs);
        public abstract void Update(long timeMs);

        protected float ClampToGameX(float val, int extraWidth)
        {
            return (float)MathHelper.Clamp(val, Game.X, Game.X + Game.Width - extraWidth);
        }

        protected float ClampToGameY(float val, int extraHeight)
        {
            return (float)MathHelper.Clamp(val, Game.Y, Game.Y + Game.Height - extraHeight);
        }

        protected MinigameEntity(T game)
        {
            Game = game;
        }
    }
}
