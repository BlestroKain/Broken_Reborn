using Intersect.Client.Framework.Graphics;
using System;
using System.Drawing;

namespace Intersect.Utilities
{
    public static class GraphicsHelper
    {
        public static GameTexture Compose(GameTexture symbol, GameTexture background)
        {
            if (symbol == null || background == null)
                return null; // O maneja este caso como prefieras

            using (Bitmap symbolBitmap = ConvertToBitmap(symbol))
            using (Bitmap backgroundBitmap = ConvertToBitmap(background))
            using (Bitmap composedBitmap = new Bitmap(backgroundBitmap.Width, backgroundBitmap.Height))
            using (Graphics g = Graphics.FromImage(composedBitmap))
            {
                g.DrawImage(backgroundBitmap, 0, 0);

                // Asumiendo que quieres centrar el símbolo en el fondo.
                int x = (backgroundBitmap.Width - symbolBitmap.Width) / 2;
                int y = (backgroundBitmap.Height - symbolBitmap.Height) / 2;
                g.DrawImage(symbolBitmap, x, y);

                return ConvertToGameTexture(composedBitmap);
            }
        }

        private static Bitmap ConvertToBitmap(GameTexture texture)
        {
            int width = texture.GetWidth();
            int height = texture.GetHeight();
            Bitmap bitmap = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color intersectColor = texture.GetPixel(x, y); // Asumiendo que tienes un método para obtener el color de un píxel en GameTexture.

                    // Convertir de Intersect.Color a System.Drawing.Color.
                    System.Drawing.Color systemColor = System.Drawing.Color.FromArgb(intersectColor.A, intersectColor.R, intersectColor.G, intersectColor.B);

                    bitmap.SetPixel(x, y, systemColor);
                }
            }
            return bitmap;
        }
        private static GameTexture ConvertToGameTexture(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            GameTexture texture = new ConcreteGameTexture(width, height); // Usa una clase concreta que extiende GameTexture

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    System.Drawing.Color systemColor = bitmap.GetPixel(x, y);

                    // Convertir de System.Drawing.Color a Intersect.Color.
                    Color intersectColor = new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A);

                    texture.SetColor(x, y, intersectColor);
                }
            }
            return texture;
        }

    }
}