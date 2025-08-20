using System.Collections.Generic;
using Intersect.Client.Utilities;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using NUnit.Framework;

namespace Intersect.Tests.Client.Utilities
{
    [TestFixture]
    public class SpellMathTests
    {
        [Test]
        public void GetEffective_SumsBaseAndDeltasAndClamps()
        {
            var descriptor = new SpellDescriptor
            {
                CastDuration = 1000,
                CooldownDuration = 2000,
                VitalCost = new long[] { 10, 20 },
                Combat = new SpellCombatDescriptor { HitRadius = 2 },
                Progression = new List<SpellProgressionRow>
                {
                    new SpellProgressionRow
                    {
                        CastTimeDeltaMs = -1500,
                        CooldownDeltaMs = -2500,
                        VitalCostDeltas = new long[] { -5, 5 },
                        PowerBonusFlat = 10,
                        PowerScalingBonus = 0.5f,
                        BuffStrengthFactor = 1.5f,
                        BuffDurationFactor = 2f,
                        DebuffStrengthFactor = 0.5f,
                        DebuffDurationFactor = 1.5f,
                        UnlocksAoE = true,
                        AoERadiusDelta = 3
                    }
                }
            };

            SpellDescriptor.Lookup.Add(descriptor);
            var state = new PlayerSpellbookState();
            state.SpellLevels[descriptor.Id] = 1;

            var stats = SpellMath.GetEffective(null, descriptor.Id, state);

            Assert.That(stats.CastTimeMs, Is.EqualTo(0));
            Assert.That(stats.CooldownTimeMs, Is.EqualTo(0));
            Assert.That(stats.VitalCosts, Is.EquivalentTo(new long[] { 5, 25 }));
            Assert.That(stats.PowerBonusFlat, Is.EqualTo(10));
            Assert.That(stats.PowerScalingBonus, Is.EqualTo(0.5f));
            Assert.That(stats.BuffStrengthFactor, Is.EqualTo(1.5f));
            Assert.That(stats.BuffDurationFactor, Is.EqualTo(2f));
            Assert.That(stats.DebuffStrengthFactor, Is.EqualTo(0.5f));
            Assert.That(stats.DebuffDurationFactor, Is.EqualTo(1.5f));
            Assert.That(stats.UnlocksAoE, Is.True);
            Assert.That(stats.AoERadius, Is.EqualTo(5));

            SpellDescriptor.Lookup.Delete(descriptor);
        }
    }
}
