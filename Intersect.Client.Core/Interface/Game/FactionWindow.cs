using System;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;

namespace Intersect.Client.Interface.Game;

/// <summary>
/// Simple window displaying faction related information for the local player.
/// </summary>
public partial class FactionWindow : Window
{
    private readonly Label _honorLabel;
    private readonly Label _gradeLabel;
    private readonly Label _rankingLabel;

    public FactionWindow(Canvas gameCanvas) : base(gameCanvas, nameof(FactionWindow), false, nameof(FactionWindow))
    {
        IsResizable = false;
        SetSize(250, 120);

        _honorLabel = new Label(this, "HonorLabel");
        _honorLabel.SetPosition(10, 10);

        _gradeLabel = new Label(this, "GradeLabel");
        _gradeLabel.SetPosition(10, 40);

        _rankingLabel = new Label(this, "RankingLabel");
        _rankingLabel.SetPosition(10, 70);
    }

    /// <summary>
    /// Refreshes the labels with the current player information.
    /// </summary>
    public void Refresh()
    {
        var me = Globals.Me;
        if (me == null)
        {
            return;
        }

        _honorLabel.Text = $"Honor: {me.Honor}";
        _gradeLabel.Text = $"Grade: {me.Grade}";

        // Ranking name derived from grade; this is a simple placeholder mapping.
        string[] rankNames = { "Recruit", "Soldier", "Officer", "Commander", "Legend" };
        var rankIndex = Math.Clamp(me.Grade, 0, rankNames.Length - 1);
        _rankingLabel.Text = $"Ranking: {rankNames[rankIndex]}";
    }
}

