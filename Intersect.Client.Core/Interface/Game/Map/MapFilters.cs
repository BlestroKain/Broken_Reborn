using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
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
    /// Inverted index mapping search tokens to matching map entries.
    /// </summary>
    private readonly Dictionary<string, HashSet<MapSearchEntry>> _searchIndex = new();

    /// <summary>
    /// Collection of all indexed map entries.
    /// </summary>
    private readonly List<MapSearchEntry> _entries = new();

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

        // Apply stored preference if available.
        if (MapPreferences.Instance.ActiveFilters.TryGetValue(key, out var enabled))
        {
            cb.IsChecked = enabled;
        }
        else
        {
            MapPreferences.Instance.ActiveFilters[key] = cb.IsChecked;
            MapPreferences.Save();
        }

        cb.CheckChanged += (_, args) =>
        {
            MapPreferences.Instance.ActiveFilters[key] = cb.IsChecked;
            MapPreferences.Save();
        };

        _filters[key] = cb;
    }

    public bool IsFilterEnabled(string key)
    {
        return _filters.TryGetValue(key, out var cb) && cb.IsChecked;
    }

    public string SearchText => _searchBox.Text;

    /// <summary>
    /// Represents an item that can be searched for on the world map.
    /// </summary>
    public record MapSearchEntry(string Name, string Type, IReadOnlyList<string> Tags, Point Position);

    private static IEnumerable<string> Tokenize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Enumerable.Empty<string>();
        }

        return text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(t => t.ToLowerInvariant());
    }

    private void IndexToken(string token, MapSearchEntry entry)
    {
        if (!_searchIndex.TryGetValue(token, out var set))
        {
            set = [];
            _searchIndex[token] = set;
        }

        set.Add(entry);
    }

    /// <summary>
    /// Adds an entry to the search index, indexing by name, type, and tags.
    /// </summary>
    public void IndexEntry(MapSearchEntry entry)
    {
        _entries.Add(entry);

        foreach (var token in Tokenize(entry.Name))
        {
            IndexToken(token, entry);
        }

        foreach (var token in Tokenize(entry.Type))
        {
            IndexToken(token, entry);
        }

        foreach (var tag in entry.Tags ?? Array.Empty<string>())
        {
            foreach (var token in Tokenize(tag))
            {
                IndexToken(token, entry);
            }
        }
    }

    /// <summary>
    /// Searches the index and returns all matching entries.
    /// </summary>
    public IEnumerable<MapSearchEntry> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Enumerable.Empty<MapSearchEntry>();
        }

        var tokens = Tokenize(query).ToArray();
        if (tokens.Length == 0)
        {
            return Enumerable.Empty<MapSearchEntry>();
        }

        List<HashSet<MapSearchEntry>> sets = [];
        foreach (var token in tokens)
        {
            if (_searchIndex.TryGetValue(token, out var set))
            {
                sets.Add(set);
            }
        }

        if (sets.Count == 0)
        {
            return Enumerable.Empty<MapSearchEntry>();
        }

        var result = new HashSet<MapSearchEntry>(sets[0]);
        foreach (var set in sets.Skip(1))
        {
            result.IntersectWith(set);
        }

        return result;
    }
}

