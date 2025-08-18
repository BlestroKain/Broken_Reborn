using System;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.Framework.Core.Services;
using Intersect.GameObjects;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace Intersect.Services;

[TestFixture]
public class SpellLevelingServiceTests
{
    private static SpellDescriptor CreateBaseDescriptor()
    {
        return new SpellDescriptor
        {
            CastDuration = 1000,
            CooldownDuration = 2000,
            VitalCost = new long[] { 10, 20 },
            Combat = new SpellCombatDescriptor { HitRadius = 2 }
        };
    }

    [Test]
    public void BuildAdjusted_Level1_NoChanges()
    {
        var baseDesc = CreateBaseDescriptor();
        var row = new SpellProperties { Level = 1 };

        var adjusted = SpellLevelingService.BuildAdjusted(baseDesc, row);

        Assert.AreEqual(1000, adjusted.CastTimeMs);
        Assert.AreEqual(2000, adjusted.CooldownTimeMs);
        CollectionAssert.AreEqual(new long[] { 10, 20 }, adjusted.VitalCosts);
        Assert.AreEqual(0, adjusted.PowerBonusFlat);
        Assert.AreEqual(0f, adjusted.PowerScalingBonus);
        Assert.AreEqual(0f, adjusted.BuffStrengthFactor);
        Assert.AreEqual(0f, adjusted.BuffDurationFactor);
        Assert.AreEqual(0f, adjusted.DebuffStrengthFactor);
        Assert.AreEqual(0f, adjusted.DebuffDurationFactor);
        Assert.IsFalse(adjusted.UnlocksAoE);
        Assert.AreEqual(2, adjusted.AoERadius);
    }

    [Test]
    public void BuildAdjusted_Level5_AdjustsValues()
    {
        var baseDesc = CreateBaseDescriptor();
        var row = new SpellProperties
        {
            Level = 5,
            CastTimeDeltaMs = -100,
            CooldownDeltaMs = -200,
            VitalCostDeltas = new long[] { -5, 5 },
            PowerBonusFlat = 10,
            PowerScalingBonus = 0.5f,
            BuffStrengthFactor = 1.5f,
            BuffDurationFactor = 2f,
            DebuffStrengthFactor = 0.5f,
            DebuffDurationFactor = 1.5f,
            UnlocksAoE = true,
            AoERadiusDelta = 3
        };

        var adjusted = SpellLevelingService.BuildAdjusted(baseDesc, row);

        Assert.AreEqual(900, adjusted.CastTimeMs);
        Assert.AreEqual(1800, adjusted.CooldownTimeMs);
        CollectionAssert.AreEqual(new long[] { 5, 25 }, adjusted.VitalCosts);
        Assert.AreEqual(10, adjusted.PowerBonusFlat);
        Assert.AreEqual(0.5f, adjusted.PowerScalingBonus);
        Assert.AreEqual(1.5f, adjusted.BuffStrengthFactor);
        Assert.AreEqual(2f, adjusted.BuffDurationFactor);
        Assert.AreEqual(0.5f, adjusted.DebuffStrengthFactor);
        Assert.AreEqual(1.5f, adjusted.DebuffDurationFactor);
        Assert.IsTrue(adjusted.UnlocksAoE);
        Assert.AreEqual(5, adjusted.AoERadius);
    }
}
