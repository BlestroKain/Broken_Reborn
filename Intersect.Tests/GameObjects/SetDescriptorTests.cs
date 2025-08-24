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
    public void ValidateExpandsAndInitializesArrays()
    {
        var descriptor = new SetDescriptor(Guid.NewGuid())
        {
            BonusTiers = new Dictionary<int, SetBonusTier>
            {
                [1] = new SetBonusTier
                {
                    Stats = new int[1],
                    PercentageStats = new int[1],
                    Vitals = new long[1],
                    VitalsRegen = new long[1],
                    PercentageVitals = new int[1]
                }
            }
        };

        descriptor.Validate();

        var tier = descriptor.BonusTiers[1];
        Assert.That(tier.Stats.Length, Is.EqualTo(Enum.GetValues<Stat>().Length));
        Assert.That(tier.PercentageStats.Length, Is.EqualTo(Enum.GetValues<Stat>().Length));
        Assert.That(tier.Vitals.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
        Assert.That(tier.VitalsRegen.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
        Assert.That(tier.PercentageVitals.Length, Is.EqualTo(Enum.GetValues<Vital>().Length));
    }

    [Test]
    public void ValidateTruncatesArrays()
    {
        var statLen = Enum.GetValues<Stat>().Length;
        var vitalLen = Enum.GetValues<Vital>().Length;
        var descriptor = new SetDescriptor(Guid.NewGuid())
        {
            BonusTiers = new Dictionary<int, SetBonusTier>
            {
                [1] = new SetBonusTier
                {
                    Stats = new int[statLen + 1],
                    PercentageStats = new int[statLen + 1],
                    Vitals = new long[vitalLen + 1],
                    VitalsRegen = new long[vitalLen + 1],
                    PercentageVitals = new int[vitalLen + 1]
                }
            }
        };

        var tier = descriptor.BonusTiers[1];
        tier.Stats[^1] = 42;
        tier.PercentageStats[^1] = 42;
        tier.Vitals[^1] = 42;
        tier.VitalsRegen[^1] = 42;
        tier.PercentageVitals[^1] = 42;

        descriptor.Validate();

        var validatedTier = descriptor.BonusTiers[1];
        Assert.That(validatedTier.Stats.Length, Is.EqualTo(statLen));
        Assert.That(validatedTier.PercentageStats.Length, Is.EqualTo(statLen));
        Assert.That(validatedTier.Vitals.Length, Is.EqualTo(vitalLen));
        Assert.That(validatedTier.VitalsRegen.Length, Is.EqualTo(vitalLen));
        Assert.That(validatedTier.PercentageVitals.Length, Is.EqualTo(vitalLen));

        Assert.That(validatedTier.Stats, Does.Not.Contain(42));
        Assert.That(validatedTier.PercentageStats, Does.Not.Contain(42));
        Assert.That(validatedTier.Vitals, Does.Not.Contain(42L));
        Assert.That(validatedTier.VitalsRegen, Does.Not.Contain(42L));
        Assert.That(validatedTier.PercentageVitals, Does.Not.Contain(42));
    }

    [Test]
    public void ValidateMigratesLegacyBonuses()
    {
        var descriptor = new SetDescriptor(Guid.NewGuid())
        {
            Stats = new[] { 1 },
            PercentageStats = new[] { 2 },
            Vitals = new long[] { 3 },
            VitalsRegen = new long[] { 4 },
            PercentageVitals = new[] { 5 },
            Effects = new List<EffectData> { new(ItemEffect.Luck, 10) },
            ItemIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }
        };

        descriptor.Validate();

        Assert.That(descriptor.BonusTiers.ContainsKey(3), Is.True);
        var tier = descriptor.BonusTiers[3];
        Assert.That(tier.Stats[0], Is.EqualTo(1));
        Assert.That(tier.PercentageStats[0], Is.EqualTo(2));
        Assert.That(tier.Vitals[0], Is.EqualTo(3));
        Assert.That(tier.VitalsRegen[0], Is.EqualTo(4));
        Assert.That(tier.PercentageVitals[0], Is.EqualTo(5));
        Assert.That(tier.Effects[0].Percentage, Is.EqualTo(10));
    }
}
