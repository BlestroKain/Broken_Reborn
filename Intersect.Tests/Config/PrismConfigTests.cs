using System;
using System.IO;
using Intersect;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;
using NUnit.Framework;

namespace Intersect.Tests.Config;

public class PrismConfigTests
{
    [Test]
    public void SaveAndLoad_PreservesPrisms()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var previous = Options.ResourcesDirectory;
        Options.ResourcesDirectory = tempDir;

        try
        {
            PrismConfig.Prisms.Clear();
            var prism = new AlignmentPrism { Id = Guid.NewGuid(), MapId = Guid.NewGuid() };
            PrismConfig.Prisms.Add(prism);

            PrismConfig.Save();
            PrismConfig.Prisms.Clear();
            PrismConfig.Load();

            var path = Path.Combine(tempDir, "Config", "prisms.json");
            Assert.That(File.Exists(path), Is.True);
            Assert.That(PrismConfig.Prisms, Has.Count.EqualTo(1));
            Assert.That(PrismConfig.Prisms[0].Id, Is.EqualTo(prism.Id));
        }
        finally
        {
            Options.ResourcesDirectory = previous;
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}
