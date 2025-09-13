using System.Collections.Generic;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Config;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game;

public partial class QuestRewardExp : Base
{
    public QuestRewardExp(
        IQuestWindow window,
        long playerExp,
        Dictionary<JobType, long>? jobExp,
        long guildExp,
        Dictionary<Factions, int>? factionHonor
    ) : base(null)
    {
        window.AddRewardWidget(this);

        Dock = Pos.Top;

        if (playerExp > 0)
        {
            AddLabel($"+{playerExp} EXP");
        }

        if (jobExp != null)
        {
            foreach (var pair in jobExp)
            {
                AddLabel($"+{pair.Value} {pair.Key} EXP");
            }
        }

        if (guildExp > 0)
        {
            AddLabel($"+{guildExp} Guild EXP");
        }

        if (factionHonor != null)
        {
            foreach (var pair in factionHonor)
            {
                AddLabel($"+{pair.Value} {pair.Key} Honor");
            }
        }

        SizeToChildren(false, true);
    }

    private void AddLabel(string text)
    {
        var label = new Label(this)
        {
            Text = text,
            Dock = Pos.Top,
        };
    }
}

