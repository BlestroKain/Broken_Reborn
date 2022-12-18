using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Minigames
{
    public enum MinigameType
    {
        FishCatcher,
    }

    public static class MinigameService
    {
        public static Minigame CurrentGame { get; set; }

        public static bool IsActive { get; set; }

        public static void StartGame(Minigame minigame)
        {
            CurrentGame = minigame;
            CurrentGame.Start();
            IsActive = true;
        }

        public static void ToggleGame(Minigame minigame)
        {
            if (CurrentGame == null)
            {
                StartGame(minigame);
            }
            else
            {
                EndGame();
            }
        }

        public static void EndGame()
        {
            CurrentGame?.Kill();
        }

        public static void RunGame()
        {
            CurrentGame?.Run(Timing.Global.Milliseconds);
            if (CurrentGame.Done)
            {
                CurrentGame?.End();
                CurrentGame = null;
            }

            IsActive = CurrentGame != null;
        }
    }
}
