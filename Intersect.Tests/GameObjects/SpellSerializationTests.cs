using System;
using System.Collections.Generic;
using Intersect.GameObjects;
using Intersect.Framework.Core.GameObjects.Spells;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Intersect.Tests.GameObjects;

public class SpellSerializationTests
{
    [Test]
    public void SpellDescriptorLevelUpgradesRoundTrip()
    {
        var descriptor = new SpellDescriptor(Guid.NewGuid())
        {
            LevelUpgrades = new Dictionary<int, SpellProperties>
            {
                [2] = new SpellProperties { Level = 2 }
            }
        };

        var json = JsonConvert.SerializeObject(descriptor);
        var result = JsonConvert.DeserializeObject<SpellDescriptor>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.LevelUpgrades);
        Assert.That(result.LevelUpgrades.ContainsKey(2));
        Assert.That(result.LevelUpgrades[2].Level, Is.EqualTo(2));
    }
}
