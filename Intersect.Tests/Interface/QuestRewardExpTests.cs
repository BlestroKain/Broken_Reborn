using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game;
using Intersect.Config;
using Intersect.Enums;
using NUnit.Framework;

namespace Intersect.Tests.Interface;

public class QuestRewardExpTests
{
    [SetUp]
    public void Setup()
    {
        Options.EnsureCreated();
    }

    private sealed class TestQuestWindow : IQuestWindow
    {
        public readonly List<Base> Widgets = new();

        public void AddRewardWidget(Base widget)
        {
            Widgets.Add(widget);
        }

        public void ClearRewardWidgets()
        {
            Widgets.Clear();
        }
    }

    [Test]
    public void JobExpZeroValueIsSkipped()
    {
        var window = new TestQuestWindow();
        Dictionary<JobType, long> jobExp = new()
        {
            [JobType.Farming] = 0,
            [JobType.Fishing] = 25,
        };

        var reward = new QuestRewardExp(window, 0, jobExp, 0, null);

        var labels = reward.Children.OfType<Label>().Select(l => l.Text).ToArray();

        Assert.That(labels, Is.EquivalentTo(new[] {"+25 Fishing EXP"}));
    }

    [Test]
    public void FactionHonorZeroValueIsSkipped()
    {
        var window = new TestQuestWindow();
        Dictionary<Factions, int> factionHonor = new()
        {
            [Factions.Neutral] = 0,
            [Factions.Serolf] = 5,
        };

        var reward = new QuestRewardExp(window, 0, null, 0, factionHonor);

        var labels = reward.Children.OfType<Label>().Select(l => l.Text).ToArray();

        Assert.That(labels, Is.EquivalentTo(new[] {"+5 Serolf Honor"}));
    }

    [Test]
    public void JobExpIconHasTooltip()
    {
        var window = new TestQuestWindow();
        Dictionary<JobType, long> jobExp = new()
        {
            [JobType.Farming] = 25,
        };

        var reward = new QuestRewardExp(window, 0, jobExp, 0, null);

        var icon = reward.Children.First(control => control.Name == "QuestRewardExpIcon");

        Assert.That(icon.TooltipText, Is.EqualTo("Farming EXP"));
    }
}

