using System;
using System.Drawing;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Maps;
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

        Show();

        var color = map.PrismOwner switch
        {
            Intersect.Enums.Alignment.Serolf => Color.Blue,
            Intersect.Enums.Alignment.Nidraj => Color.Red,
            _ => Color.Gray
        };

        if (map.PrismUnderAttack)
        {
            color = Color.Orange;
        }

        _icon.TextColor = color;
        _hpBar.RenderColor = color;
        _hpBar.Value = map.PrismMaxHp > 0 ? map.PrismHp / (float)map.PrismMaxHp : 0f;
        _stateLabel.Text = $"{map.PrismOwner} - {map.PrismState}";
    }
}

