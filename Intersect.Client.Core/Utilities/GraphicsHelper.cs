using Intersect.Client.Framework.Graphics;
using Intersect.Client.Core;

namespace Intersect.Client.Utilities;

public static partial class GraphicsHelper
{
    public static IGameTexture Compose(
        IGameTexture background,
        IGameTexture symbol,
        Color backgroundColor,
        Color symbolColor
    )
    {
        var renderer = Graphics.Renderer;
        var renderTexture = renderer.CreateRenderTexture(background.Width, background.Height);

        if (renderTexture.Begin())
        {
            renderTexture.Clear(Color.Transparent);
            renderer.DrawTexture(
                background,
                0,
                0,
                background.Width,
                background.Height,
                0,
                0,
                background.Width,
                background.Height,
                backgroundColor,
                renderTexture
            );

            renderer.DrawTexture(
                symbol,
                0,
                0,
                symbol.Width,
                symbol.Height,
                0,
                0,
                symbol.Width,
                symbol.Height,
                symbolColor,
                renderTexture
            );

            renderTexture.End();
        }

        return renderTexture;
    }

    public static Color SetColor(byte r, byte g, byte b, byte a = 255)
    {
        return new Color(a, r, g, b);
    }

    public static IGameTexture Recolor(IGameTexture texture, Color color)
    {
        return Compose(texture, texture, color, Color.White);
    }
}
