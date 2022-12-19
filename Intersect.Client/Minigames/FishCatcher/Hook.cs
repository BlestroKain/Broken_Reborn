using Intersect.Client.Core;
using Intersect.Client.Core.Controls;
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
    sealed class Hook : MinigameEntity<FishCatcherGame>
    {
        enum State
        {
            Inactive,
            Idle,
            Moving,
            Reeling,
        }

        private State CurrentState;

        private GameTexture HookTexture { get; set; }
        private FloatRect HookSrc;

        private Pointf Destination;
        private Queue<Pointf> DestinationQueue = new Queue<Pointf>();
        private const int DestinationQueueSize = 5;

        public float X { get; set; }
        public float Y { get; set; }
        public double Dir { get; set; }
        long DirChangeTime;

        public long LastUpdate { get; set; }
        public long Delta { get; set; }

        public float Traction = 125.0f;

        public float MaxSpeed { get; set; } = 800.0f;

        public float ReelingSpeed { get; set; } = 1200.0f;

        public float Velocity;

        public Hook(FishCatcherGame game) : base(game)
        {
            HookTexture = Minigame.LoadMinigameTexture("fishing_hook.png", ref HookSrc);
            X = Graphics.CurrentView.CenterX - HookTexture.Center.X;
            Y = Graphics.CurrentView.CenterY - HookTexture.Center.Y;
            Velocity = 0f;
            Dir = 0;
            CurrentState = State.Idle;

            var startPoint = new Pointf(Graphics.CurrentView.CenterX, Graphics.CurrentView.CenterY);
            for (var i = 0; i < DestinationQueueSize; i++)
            {
                DestinationQueue.Enqueue(startPoint);
            }
            Destination = DestinationQueue.Peek();
            X = Destination.X;
            Y = Destination.Y;
        }

        public override void Update(long timeMs)
        {
            var mousePos = Game.MousePos;
            if (timeMs > DirChangeTime)
            {
                SetDestination(mousePos);
                DirChangeTime = timeMs + 50;
            }
            StateUpdate();
        }

        private void StateUpdate()
        {
            switch(CurrentState)
            {
                case State.Inactive:
                    break;
                case State.Idle:
                    State_Idle();
                    break;
                case State.Moving:
                    State_Moving();
                    break;
                case State.Reeling:
                    State_Reeling();
                    break;
            }
            RefreshState();
        }

        private void RefreshState()
        {
            switch(CurrentState)
            {
                case State.Inactive:
                    break;
                case State.Idle:
                    if (Destination.X != X || Destination.Y != Y)
                    {
                        CurrentState = State.Moving;
                    }
                    if (CanReel)
                    {
                        CurrentState = State.Reeling;
                    }
                    break;
                case State.Moving:
                    if (Velocity == 0)
                    {
                        CurrentState = State.Idle;
                    }
                    if (CanReel)
                    {
                        CurrentState = State.Reeling;
                    }
                    break;
                case State.Reeling:
                    if (Velocity == 0)
                    {
                        CurrentState = State.Idle;
                    }
                    else if (!Controls.KeyDown(Control.AttackInteract))
                    {
                        CurrentState = State.Moving;
                    }
                    break;
            }
        }

        public bool CanReel => Controls.KeyDown(Control.AttackInteract) && Y - Game.Y > 64;

        public override void Draw(long timeMs)
        {
            var drawDest = new FloatRect(X, Y, HookTexture.Width, HookTexture.Height);
            Graphics.DrawGameTexture(HookTexture, HookSrc, drawDest, Color.White);
        }

        private void State_Idle()
        {
            Velocity = 0;
        }

        private void State_Reeling()
        {
            Velocity = ReelingSpeed * Game.Delta;

            var desiredY = ClampToGameY(Y - Velocity, HookTexture.Height);

            if (desiredY == Game.Y)
            {
                Velocity = 0;
            }

            Y -= Velocity;
        }

        private void State_Moving()
        {
            var destination = Destination;

            Dir = MathHelper.AngleBetween(X, Y, destination.X, destination.Y);

            var distanceDeltas = new Pointf(destination.X - X, destination.Y - Y);
            var distanceRemaining = Math.Sqrt(Math.Pow(distanceDeltas.X, 2.0) + Math.Pow(distanceDeltas.Y, 2.0));
            var decelDistance = Math.Pow(Velocity, 2) / (2 * Traction);

            if (distanceRemaining > decelDistance)
            {
                Velocity = Math.Min(Velocity + Traction * Game.Delta, MaxSpeed);
            }
            else
            {
                Velocity = Math.Max(Velocity - Traction * Game.Delta, 0);
            }

            var xVel = Velocity * (float)MathHelper.DCos(Dir) * Game.Delta;
            var yVel = Velocity * (float)MathHelper.DSin(Dir) * Game.Delta;

            var desiredX = ClampToGameX(X + xVel, HookTexture.Width);
            var desiredY = ClampToGameY(Y + yVel, HookTexture.Height);

            if ((Math.Sign(xVel) > 0 && desiredX > destination.X) ||
                (Math.Sign(xVel) < 0 && desiredX < destination.X))
            {
                desiredX = destination.X;
            }

            if ((Math.Sign(yVel) > 0 && desiredY > destination.Y) ||
                (Math.Sign(yVel) < 0 && desiredY < destination.Y))
            {
                desiredY = destination.Y;
            }

            X = desiredX;
            Y = desiredY;
        }

        private void SetDestination(Pointf mousePos)
        {
            if (DestinationQueue.Count > 0)
            {
                Destination = DestinationQueue.Dequeue();
            }

            var nextDestination = new Pointf(
                ClampToGameX(mousePos.X - HookTexture.Center.X, HookTexture.Width),
                ClampToGameY(mousePos.Y - HookTexture.Center.Y, HookTexture.Height)
            );

            DestinationQueue.Enqueue(nextDestination);
        }
    }
}
