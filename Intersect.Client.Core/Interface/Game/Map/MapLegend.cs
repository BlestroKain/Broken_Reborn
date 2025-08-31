using System.Collections.Generic;
using Intersect.Client.Framework.Gwen.Control;
using Microsoft.Xna.Framework;

namespace Intersect.Client.Interface.Game.Map;

/// <summary>
/// Simple legend panel for the world map showing color coded entries.
/// </summary>
public class MapLegend
{
    private readonly ImagePanel _panel;
    private readonly List<Label> _entries = new();

    public MapLegend(Base parent)
    {
        _panel = new ImagePanel(parent, "MapLegend");
        var title = new Label(_panel, "Title") { Text = "Legend" };
    }

    /// <summary>
    /// Adds an entry to the legend.
    /// </summary>
    public void AddEntry(string text, Color color)
    {
        var label = new Label(_panel) { Text = text };
        label.TextColor = color;
        label.Dock = Pos.Top;
        _entries.Add(label);
    }
}

