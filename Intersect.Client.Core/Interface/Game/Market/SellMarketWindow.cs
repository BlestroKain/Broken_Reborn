using System;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Market;

/// <summary>
/// Minimal placeholder window for selling items on the market.
/// </summary>
public sealed class SellMarketWindow
{
    private readonly WindowControl _window;

    public SellMarketWindow(Canvas canvas)
    {
        _window = new WindowControl(canvas, Strings.Market.Sell, false, nameof(SellMarketWindow));
        _window.DisableResizing();
        _window.SetSize(300, 200);
        _window.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        _window.IsHidden = true;

        var label = new Label(_window)
        {
            Text = "Selling items is not implemented yet.",
            Alignment = [Alignments.Center],
        };
        label.SetBounds(10, 80, 280, 20);
    }

    public void Show() => _window.Show();
    public void Close() => _window.Close();
    public bool IsVisible() => !_window.IsHidden;
}
