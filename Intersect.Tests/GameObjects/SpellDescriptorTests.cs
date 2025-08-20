using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Intersect.GameObjects.Tests
{
    [TestFixture]
    public class SpellDescriptorTests
    {
        [Test]
        public void ProgressionJson_EmptyYieldsFiveDefaultRows()
        {
            var descriptor = new SpellDescriptor();
            descriptor.ProgressionJson = string.Empty;

            Assert.AreEqual(5, descriptor.Progression.Count);
            foreach (var row in descriptor.Progression)
            {
                Assert.IsNotNull(row);
                Assert.AreEqual(Enum.GetValues<Vital>().Length, row.VitalCostDeltas.Length);
                Assert.AreEqual(0, row.CastTimeDeltaMs);
                Assert.AreEqual(0, row.CooldownDeltaMs);
            }
        }

        [Test]
        public void GetProgressionLevel_ReturnsRowOrEmpty()
        {
            var row1 = new SpellProgressionRow { CastTimeDeltaMs = 1 };
            var row2 = new SpellProgressionRow { CastTimeDeltaMs = 2 };
            var descriptor = new SpellDescriptor
            {
                Progression = new List<SpellProgressionRow> { row1, row2 }
            };

            var first = descriptor.GetProgressionLevel(1);
            Assert.AreSame(row1, first);

            var second = descriptor.GetProgressionLevel(2);
            Assert.AreSame(row2, second);

            var below = descriptor.GetProgressionLevel(0);
            Assert.IsNotNull(below);
            Assert.AreNotSame(row1, below);
            Assert.AreEqual(0, below.CastTimeDeltaMs);

            var above = descriptor.GetProgressionLevel(3);
            Assert.IsNotNull(above);
            Assert.AreNotSame(row2, above);
            Assert.AreEqual(0, above.CastTimeDeltaMs);
        }
    }
}
