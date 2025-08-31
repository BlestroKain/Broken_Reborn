using Intersect;
using Intersect.Client.Interface.Game.Map;
using NUnit.Framework;

namespace Intersect.Tests.Client;

public class MinimapWindowTests
{
    [TestCase(0.5f, 12)]
    [TestCase(1f, 16)]
    [TestCase(2f, 32)]
    public void ClampIconScale_ScalesEntityIcons(float configScale, int expectedSize)
    {
        var scale = MinimapWindow.ClampIconScale(configScale);
        var tileSize = new Point(16, 16);
        var width = (int)(tileSize.X * scale);
        var height = (int)(tileSize.Y * scale);
        Assert.That(width, Is.EqualTo(expectedSize));
        Assert.That(height, Is.EqualTo(expectedSize));
    }

    [TestCase(0.5f, 8)]
    [TestCase(1.5f, 24)]
    public void PoiIconScale_ScalesIcons(float poiScale, int expectedSize)
    {
        var tileSize = new Point(16, 16);
        var width = (int)(tileSize.X * poiScale);
        var height = (int)(tileSize.Y * poiScale);
        Assert.That(width, Is.EqualTo(expectedSize));
        Assert.That(height, Is.EqualTo(expectedSize));
    }
}

