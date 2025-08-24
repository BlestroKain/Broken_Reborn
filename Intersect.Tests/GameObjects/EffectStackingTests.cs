using System.Collections.Generic;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using NUnit.Framework;

namespace Intersect.Tests.GameObjects;

public class EffectStackingTests
{
    [Test]
    public void ApplyEffect_StacksDuplicates()
    {
        var bonuses = new Dictionary<ItemEffect, int>();
        var effect1 = new EffectData(ItemEffect.Luck, 10, true, EffectStacking.Stack);
        var effect2 = new EffectData(ItemEffect.Luck, 5, true, EffectStacking.Stack);
        bonuses.ApplyEffect(effect1);
        bonuses.ApplyEffect(effect2);
        Assert.That(bonuses[ItemEffect.Luck], Is.EqualTo(15));
    }

    [Test]
    public void ApplyEffect_IgnoresDuplicates()
    {
        var bonuses = new Dictionary<ItemEffect, int>();
        var effect1 = new EffectData(ItemEffect.Luck, 10, true, EffectStacking.Ignore);
        var effect2 = new EffectData(ItemEffect.Luck, 5, true, EffectStacking.Ignore);
        bonuses.ApplyEffect(effect1);
        bonuses.ApplyEffect(effect2);
        Assert.That(bonuses[ItemEffect.Luck], Is.EqualTo(10));
    }

    [Test]
    public void ApplyEffect_RenewsDuplicates()
    {
        var bonuses = new Dictionary<ItemEffect, int>();
        var effect1 = new EffectData(ItemEffect.Luck, 10, true, EffectStacking.Renew);
        var effect2 = new EffectData(ItemEffect.Luck, 5, true, EffectStacking.Renew);
        bonuses.ApplyEffect(effect1);
        bonuses.ApplyEffect(effect2);
        Assert.That(bonuses[ItemEffect.Luck], Is.EqualTo(5));
    }

    [Test]
    public void ApplyEffect_SkipsNonPassive()
    {
        var bonuses = new Dictionary<ItemEffect, int>();
        var effect = new EffectData(ItemEffect.Luck, 10, false, EffectStacking.Stack);
        bonuses.ApplyEffect(effect);
        Assert.That(bonuses.ContainsKey(ItemEffect.Luck), Is.False);
    }
}
