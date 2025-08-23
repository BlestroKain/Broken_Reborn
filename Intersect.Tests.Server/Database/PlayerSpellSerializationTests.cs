using System;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Server.Database.PlayerData.Players;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Intersect.Tests.Server.Database;

public class PlayerSpellSerializationTests
{
    [Test]
    public void PlayerSpellLevelRoundTrip()
    {
        var spell = new PlayerSpell(0)
        {
            SpellId = Guid.NewGuid(),
            SpellPointsSpent = 3
        };
        spell.Level = 5;

        var json = JsonConvert.SerializeObject(spell);
        var result = JsonConvert.DeserializeObject<PlayerSpell>(json);

        Assert.NotNull(result);
        Assert.That(result.Level, Is.EqualTo(5));
        Assert.That(result.SpellPointsSpent, Is.EqualTo(3));
    }
}
