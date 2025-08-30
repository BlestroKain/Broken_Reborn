using System;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game;

/// <summary>
/// Simple window displaying faction related information for the local player.
/// </summary>
public partial class FactionWindow : Window
{
    private readonly Label _honorLabel;
    private readonly Label _gradeLabel;
    private readonly Button _wingsButton;

    public FactionWindow(Canvas gameCanvas) : base(gameCanvas, nameof(FactionWindow), false, nameof(FactionWindow))
    {
        IsResizable = false;
        SetSize(250, 120);

        _honorLabel = new Label(this, "HonorLabel");
        _honorLabel.SetPosition(10, 10);

        _gradeLabel = new Label(this, "GradeLabel");
        _gradeLabel.SetPosition(10, 40);

        _wingsButton = new Button(this, "WingsButton");
        _wingsButton.SetPosition(10, 70);
        _wingsButton.SetText("Toggle Wings");
        _wingsButton.Clicked += (s, e) =>
        {
            var me = Globals.Me;
            if (me == null)
            {
                return;
            }

            var newState = me.Wings == WingState.On ? WingState.Off : WingState.On;
            me.Wings = newState;
            PacketSender.SendToggleWings(newState);
        };
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
        var grade = (FactionGrade)Math.Clamp(me.Grade, 0, (int)FactionGrade.Legend);
        _gradeLabel.Text = $"Grade: {grade}";
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

    }
}

