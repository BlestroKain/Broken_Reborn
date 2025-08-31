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
}

