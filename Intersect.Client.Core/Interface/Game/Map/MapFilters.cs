using System.Collections.Generic;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;

namespace Intersect.Client.Interface.Game.Map;

/// <summary>
/// Provides filter toggles and a search box for the world map.
/// </summary>
public class MapFilters
{
    private readonly ImagePanel _panel;
    private readonly Dictionary<string, Checkbox> _filters = new();
    private readonly TextBox _searchBox;

    /// <summary>
    /// Raised when the user submits a search query.
    /// </summary>
    public event System.Action<string>? SearchSubmitted;

    public MapFilters(Base parent)
    {
        _panel = new ImagePanel(parent, "MapFilters");
        _searchBox = new TextBox(_panel, "SearchBox");
        _searchBox.SubmitPressed += SearchBoxOnSubmitPressed;
    }

    private void SearchBoxOnSubmitPressed(Base sender, EventArgs args)
    {
        SearchSubmitted?.Invoke(_searchBox.Text);
    }

    public void AddFilter(string key, string label)
    {
        var cb = new Checkbox(_panel) { Text = label, IsChecked = true };
        cb.Dock = Pos.Top;
        _filters[key] = cb;
    }

    public bool IsFilterEnabled(string key)
    {
        return _filters.TryGetValue(key, out var cb) && cb.IsChecked;
    }

    public string SearchText => _searchBox.Text;
}

