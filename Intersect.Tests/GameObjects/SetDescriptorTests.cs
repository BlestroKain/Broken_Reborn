using System;
using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using NUnit.Framework;

namespace Intersect.Tests.GameObjects;

public class SetDescriptorTests
{
    [Test]
    public void ValidateEnsuresArrayLengths()
    {
        var descriptor = new SetDescriptor(Guid.NewGuid())
        {
            Stats = new int[1],
            PercentageStats = new int[1],
            Vitals = new long[1],
            VitalsRegen = new long[1],
            PercentageVitals = new int[1]
        };

        descriptor.Validate();

        Assert.That(descriptor.Stats.Length, Is.EqualTo(Enum.GetValues<Stat>().Length));
        Assert.That(descriptor.PercentageStats.Length, Is.EqualTo(Enum.GetValues<Stat>().Length));
        Assert.That(descriptor.Vitals.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
        Assert.That(descriptor.VitalsRegen.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
        Assert.That(descriptor.PercentageVitals.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
    }

    [Test]
    public void ValidateTruncatesArrays()
    {
        var statLen = Enum.GetValues<Stat>().Length;
        var vitalLen = Enum.GetValues<Vital>().Length;
        var descriptor = new SetDescriptor(Guid.NewGuid())
        {
            Stats = new int[statLen + 1],
            PercentageStats = new int[statLen + 1],
            Vitals = new long[vitalLen + 1],
            VitalsRegen = new long[vitalLen + 1],
            PercentageVitals = new int[vitalLen + 1]
        };

        descriptor.Stats[^1] = 42;
        descriptor.PercentageStats[^1] = 42;
        descriptor.Vitals[^1] = 42;
        descriptor.VitalsRegen[^1] = 42;
        descriptor.PercentageVitals[^1] = 42;

        descriptor.Validate();

        Assert.That(descriptor.Stats.Length, Is.EqualTo(statLen));
        Assert.That(descriptor.PercentageStats.Length, Is.EqualTo(statLen));
        Assert.That(descriptor.Vitals.Length, Is.EqualTo(vitalLen));
        Assert.That(descriptor.VitalsRegen.Length, Is.EqualTo(vitalLen));
        Assert.That(descriptor.PercentageVitals.Length, Is.EqualTo(vitalLen));

        Assert.That(descriptor.Stats, Does.Not.Contain(42));
        Assert.That(descriptor.PercentageStats, Does.Not.Contain(42));
        Assert.That(descriptor.Vitals, Does.Not.Contain(42L));
        Assert.That(descriptor.VitalsRegen, Does.Not.Contain(42L));
        Assert.That(descriptor.PercentageVitals, Does.Not.Contain(42));
    }

    [Test]
    public void GetBonusesScalesWithPieces()
    {
        var items = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var descriptor = new SetDescriptor(Guid.NewGuid())
        {
            ItemIds = items,
            Stats = new[] { 40, 0, 0, 0, 0, 0, 0, 0 },
            Effects = new List<EffectData> { new(ItemEffect.Luck, 20) }
        };

        descriptor.Validate();

        var (stats, _, _, _, _, effects) = descriptor.GetBonuses(2);
        Assert.That(stats[(int)Stat.Attack], Is.EqualTo(20));
        Assert.That(effects[0].Percentage, Is.EqualTo(10));
    }
}
