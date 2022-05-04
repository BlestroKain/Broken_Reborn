using System;
using System.Collections.Generic;
using Intersect.Editor.Core;
using Intersect.Editor.Entities;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Maps;
using Intersect.Editor.General;
using Intersect.GameObjects;
using Intersect.Time;

namespace Intersect.Editor.Maps
{

    public partial class WeatherParticle : IWeatherParticle
    {

        private List<IWeatherParticle> _RemoveParticle;

        private Animation animInstance;

        private Rectangle bounds;

        private float cameraSpawnX;

        private float cameraSpawnY;

        private int originalX;

        private int originalY;

        private Point partSize;

        private long TransmittionTimer;

        public float X { get; set; }

        private int xVelocity;

        public float Y { get; set; }

        private int yVelocity;

        public WeatherParticle(List<IWeatherParticle> RemoveParticle, int xvelocity, int yvelocity, AnimationBase anim)
        {
            TransmittionTimer = Timing.Global.Milliseconds;
            bounds = new Rectangle(0, 0, Core.Graphics.Renderer.GetScreenWidth(), Core.Graphics.Renderer.GetScreenHeight());

            xVelocity = xvelocity;
            yVelocity = yvelocity;

            animInstance = new Animation(anim, true, false);
            var animSize = animInstance.CalculateAnimationSize();
            partSize = animSize;

            if (xVelocity > 0)
            {
                originalX = Globals.Random.Next(
                    -Core.Graphics.Renderer.GetScreenWidth() / 4 - animSize.X,
                    Core.Graphics.Renderer.GetScreenWidth() + animSize.X
                );
            }
            else if (xVelocity < 0)
            {
                originalX = Globals.Random.Next(
                    animSize.X, (int)(Core.Graphics.Renderer.GetScreenWidth() * 1.25f) + animSize.X
                );
            }
            else
            {
                originalX = Globals.Random.Next(-animSize.X, Core.Graphics.Renderer.GetScreenWidth() + animSize.X);
            }

            if (yVelocity > 0)
            {
                originalY = -animSize.Y;
            }
            else if (yVelocity < 0)
            {
                originalY = Core.Graphics.Renderer.GetScreenHeight() + animSize.Y;
            }
            else
            {
                originalY = Globals.Random.Next(-animSize.Y, Core.Graphics.Renderer.GetScreenHeight() + animSize.Y);
                if (xVelocity > 0)
                {
                    originalX = -animSize.X;
                }
                else if (xVelocity < 0)
                {
                    originalX = Core.Graphics.Renderer.GetScreenWidth();
                }
            }

            if (originalX < 0)
            {
                bounds.X = originalX;
                bounds.Width += Math.Abs(originalX);
            }

            if (originalY < 0)
            {
                bounds.Y = originalY;
                bounds.Height += Math.Abs(originalY);
            }

            if (originalY > Core.Graphics.Renderer.GetScreenHeight())
            {
                bounds.Height = originalY;
            }

            if (originalX > Core.Graphics.Renderer.GetScreenWidth())
            {
                bounds.Width = originalX;
            }

            bounds.X -= animSize.X;
            bounds.Width += animSize.X * 2;
            bounds.Y -= animSize.Y;
            bounds.Height += animSize.Y * 2;

            X = originalX;
            Y = originalY;
            cameraSpawnX = Core.Graphics.Renderer.GetView().Left;
            cameraSpawnY = Core.Graphics.Renderer.GetView().Top;
            _RemoveParticle = RemoveParticle;
        }

        public void Update()
        {
            //Check if out of bounds
            var newBounds = new Rectangle(
                bounds.X + ((int)Core.Graphics.Renderer.GetView().Left - (int)cameraSpawnX),
                bounds.Y + ((int)Core.Graphics.Renderer.GetView().Top - (int)cameraSpawnY), bounds.Width, bounds.Height
            );

            if (!newBounds.IntersectsWith(new Rectangle((int)X, (int)Y, partSize.X, partSize.Y)))
            {
                _RemoveParticle.Add(this);
            }
            else
            {
                var timeScale = (Timing.Global.Milliseconds - TransmittionTimer) / 10f;
                X = originalX + xVelocity * timeScale;
                Y = originalY + yVelocity * timeScale;
                animInstance.SetPosition(cameraSpawnX + X, cameraSpawnY + Y, -1, -1, Guid.Empty, -1, 0);
                animInstance.Update();
            }
        }

        public void Dispose()
        {
            animInstance.Dispose();
        }

        ~WeatherParticle()
        {
            Dispose();
        }

    }

}
