using System;
using Intersect.Enums;
using Intersect.GameObjects;
using NUnit.Framework;

namespace Intersect.Tests.GameObjects;

public class SetDescriptorTests
{
    [Test]
    public void ValidateExpandsAndInitializesArrays()
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
}
