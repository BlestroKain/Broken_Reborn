using Intersect.Client.Framework.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intersect.Client.MonoGame.Graphics
{

    public partial class MonoRenderTexture : GameRenderTexture
    {

        private GraphicsDevice mGraphicsDevice;

        private int mHeight;

        private RenderTarget2D mRenderTexture;

        private int mWidth;

        public MonoRenderTexture(GraphicsDevice graphicsDevice, int width, int height) : base(width, height)
        {
            mRenderTexture = new RenderTarget2D(
                graphicsDevice, width, height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                graphicsDevice.PresentationParameters.DepthStencilFormat,
                graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents
            );

            RenderTextureCount++;
            mGraphicsDevice = graphicsDevice;
            mWidth = width;
            mHeight = height;
        }

        public override string GetName()
        {
            return "";
        }

        public override int GetWidth()
        {
            return mWidth;
        }

        public override int GetHeight()
        {
            return mHeight;
        }

        public override object GetTexture()
        {
            return mRenderTexture;
        }

        public override Color GetPixel(int x1, int y1)
        {
            var pixel = new Microsoft.Xna.Framework.Color[1];
            mRenderTexture.GetData(0, new Rectangle(x1, y1, 1, 1), pixel, 0, 1);

            return new Color(pixel[0].A, pixel[0].R, pixel[0].G, pixel[0].B);
        }

        public override GameTexturePackFrame GetTexturePackFrame()
        {
            return null;
        }

        public override bool Begin()
        {
            return true;
        }

        public override void End()
        {
            ((MonoRenderer) Core.Graphics.Renderer).EndSpriteBatch();
        }

        public override void Clear(Color color)
        {
            ((MonoRenderer) Core.Graphics.Renderer).EndSpriteBatch();
            mGraphicsDevice.SetRenderTarget(mRenderTexture);
            mGraphicsDevice.Clear(MonoRenderer.ConvertColor(color));
            mGraphicsDevice.SetRenderTarget(null);
        }

        public override void Dispose()
        {
            if (mRenderTexture != null)
            {
                mRenderTexture.Dispose();
                mRenderTexture = null;
                RenderTextureCount--;
            }
        }

        public override void SetColor(int x, int y, Color color)
        {
            // Asegurarte de que las coordenadas del pixel están dentro de los límites de la textura.
            if (x < 0 || x >= mWidth || y < 0 || y >= mHeight)
                return;

            // Extraer todos los datos de color de la textura.
            var colorData = new Microsoft.Xna.Framework.Color[mWidth * mHeight];
            mRenderTexture.GetData(colorData);

            // Calcular el índice del pixel y cambiar su color.
            int index = x + y * mWidth;
            colorData[index] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);

            // Volver a aplicar los datos de color modificados a la textura.
            mRenderTexture.SetData(colorData);
        }
    }

}
