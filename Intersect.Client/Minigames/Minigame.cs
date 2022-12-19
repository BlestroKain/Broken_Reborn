using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Minigames;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Minigames
{
    public interface IMinigame
    {
        MinigameType Type { get; }

        MinigameBackdrop Background { get; set; }

        GameTexture GameBackground { get; set; }

        float X { get; set; }
        float Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }

        bool Done { get; set; }

        long LastUpdate { get; set; }

        void Start();
        
        void Update(long timeMs);
        
        void Draw(long timeMs);

        void Run(long timeMs);

        void End();
    }

    public abstract class Minigame : IMinigame
    {
        public abstract MinigameType Type { get; }
        public abstract bool Done { get; set; }
        public abstract MinigameBackdrop Background { get; set; }
        public long LastUpdate { get; set; }

        private long _delta;
        public float Delta
        {
            get => _delta / (float)1000; 
        }

        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public abstract GameTexture GameBackground { get; set; }

        public abstract void Start();
        public abstract void Update(long timeMs);
        public abstract void Draw(long timeMs);
        public void Run(long timeMs)
        {
            // I have no idea why this is necessary - but some times this function gets called twice at the same time,
            // which fucks up the delta calc. Soooo this is a way around it :>)
            var delta = timeMs - LastUpdate;
            if (delta != 0)
            {
                _delta = timeMs - LastUpdate;
            }

            Update(timeMs);
            Draw(timeMs);
            LastUpdate = timeMs;
        }

        public abstract void End();

        public void Kill()
        {
            Done = true;
        }

        public static GameTexture LoadMinigameTexture(string txtName, ref FloatRect srcRect)
        {
            var texture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Minigame, txtName);
            srcRect = Graphics.GetSourceRect(texture);

            return texture;
        }

        public static void DebugLog(string message)
        {
            ChatboxMsg.AddMessage(new ChatboxMsg(message, Color.Gray, Enums.ChatMessageType.Local));
        }
    }
}
