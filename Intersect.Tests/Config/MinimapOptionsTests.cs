using Intersect.Config;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Intersect.Tests.Config;

public class MinimapOptionsTests
{
    [Test]
    public void DeserializingWithoutRenderLayers_RestoresDefaultLayers()
    {
        var json = "{}";
        var options = JsonConvert.DeserializeObject<MinimapOptions>(json);

        Assert.That(options.RenderLayers, Is.EqualTo(new[]
        {
            "Ground",
            "Mask 1",
            "Mask 2",
            "Fringe 1",
            "Fringe 2",
        }));
    }

    [TestCase(20, 220, 10, 20)]
    [TestCase(20, 220, 250, 220)]
    public void Validate_ClampsDefaultZoom(int minimum, int maximum, int defaultZoom, int expected)
    {
        var options = new MinimapOptions
        {
            MinimumZoom = minimum,
            MaximumZoom = maximum,
            DefaultZoom = defaultZoom,
        };

        options.Validate();

        Assert.That(options.DefaultZoom, Is.EqualTo(expected));
    }

    [TestCase(96f, 8)]   // 1080p
    [TestCase(144f, 12)] // 1440p
    [TestCase(192f, 16)] // 4K
    [TestCase(48f, 4)]   // Below minimum
    [TestCase(384f, 32)] // Above maximum
    public void GetScaledTileSize_ScalesAndClamps(float dpi, int expected)
    {
        var options = new MinimapOptions();

        var size = options.GetScaledTileSize(dpi);

        Assert.That(size.X, Is.EqualTo(expected));
        Assert.That(size.Y, Is.EqualTo(expected));
    }
}

