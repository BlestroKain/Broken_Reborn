using System;
using System.Linq;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Maps;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game;

/// <summary>
/// Window listing known prisms and their status.
/// </summary>
public class ConquestWindow : Window
{
    private readonly ScrollControl _list;

    public ConquestWindow(Canvas gameCanvas) : base(gameCanvas, nameof(ConquestWindow), false, nameof(ConquestWindow))
    {
        IsResizable = false;
        SetSize(400, 300);

        _list = new ScrollControl(this, "PrismList");
        _list.EnableScroll(false, true);
        _list.SetBounds(10, 10, 380, 280);
    }

    /// <summary>
    /// Rebuild the list of prisms.
    /// </summary>
    public void Refresh()
    {
        _list.DeleteAllChildren();

        foreach (var (_, obj) in MapInstance.Lookup)
        {
            if (obj is not MapInstance map || map.PrismMaxHp <= 0)
            {
                continue;
            }

            var vuln = map.PrismNextVulnerabilityStart?.ToLocalTime().ToString("g") ?? "N/A";
            var label = new Label(_list)
            {
                Text = $"{map.Name} - {map.PrismOwner} - {map.PrismState} - {vuln}",
                Dock = Pos.Top,
            };
            label.Margin = new Margin(0, 0, 0, 4);
        }
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }
}

