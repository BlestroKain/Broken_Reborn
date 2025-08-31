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

    [TestCase((byte)10, (byte)50, (byte)5, (byte)10)]
    [TestCase((byte)10, (byte)50, (byte)55, (byte)50)]
    public void Validate_ClampsDefaultZoom(byte minimum, byte maximum, byte defaultZoom, byte expected)
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
}

