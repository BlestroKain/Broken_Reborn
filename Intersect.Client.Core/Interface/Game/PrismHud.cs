using System;
using System.Drawing;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Config;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game;

/// <summary>
/// Displays prism ownership and health information for the current map.
/// </summary>
public class PrismHud : ImagePanel
{
    private readonly Label _icon;
    private readonly ProgressBar _hpBar;
    private readonly Label _stateLabel;

    private const int BarWidth = 100;
    private const int BarHeight = 12;

    public PrismHud(Canvas gameCanvas) : base(gameCanvas, nameof(PrismHud))
    {
        SetPosition(10, 10);
        SetSize(140, 40);
        IsHidden = true;

        _icon = new Label(this, "Icon")
        {
            Text = "⚑",
        };
        _icon.SetPosition(0, 0);

        _hpBar = new ProgressBar(this)
        {
            Name = "HpBar",
            AutoLabel = false,
            IsHorizontal = true,
        };
        _hpBar.SetBounds(30, 5, BarWidth, BarHeight);

        _stateLabel = new Label(this, "StateLabel");
        _stateLabel.SetPosition(30, 20);
    }

    /// <summary>
    /// Refresh the HUD contents using the provided map's prism data.
    /// </summary>
    public void Refresh(MapInstance? map)
    {
        if (map == null || map.PrismMaxHp <= 0)
        {
            Hide();
            return;
        }

        // Prism positions are now provided by the server and the client no longer
        // relies on local prism configuration data. Without direct access to the
        // prism coordinates we cannot perform a radius check here, so simply
        // display the HUD whenever the map contains prism information.
        Show();

        var color = map.PrismOwner switch
        {
            Intersect.Enums.Factions.Serolf => Color.Blue,
            Intersect.Enums.Factions.Nidraj => Color.Red,
            _ => Color.Gray
        };

        if (map.PrismUnderAttack)
        {
            color = Color.Orange;
        }

        _icon.TextColor = color;
        _hpBar.RenderColor = color;
        _hpBar.Value = map.PrismMaxHp > 0 ? map.PrismHp / (float)map.PrismMaxHp : 0f;
        var vuln = map.PrismNextVulnerabilityStart?.ToLocalTime().ToString("g") ?? "N/A";
        _hpBar.SetToolTipText($"Next window: {vuln}");
        _stateLabel.Text = $"{map.PrismOwner} - {map.PrismState}";
    }
}

