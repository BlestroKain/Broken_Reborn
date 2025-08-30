using System;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game;

/// <summary>
/// Window listing known prisms and their status.
/// </summary>
public class ConquestWindow : Window
{
    private readonly ScrollControl _list;
    private readonly ComboBox _filter;

    private enum Filter
    {
        All,
        Own,
        Rival,
        Neutral,
    }

    public ConquestWindow(Canvas gameCanvas) : base(gameCanvas, nameof(ConquestWindow), false, nameof(ConquestWindow))
    {
        IsResizable = false;
        SetSize(400, 300);

        _filter = new ComboBox(this, "OwnerFilter");
        _filter.SetBounds(10, 10, 120, 20);
        _filter.AddItem("All", userData: Filter.All);
        _filter.AddItem("Own", userData: Filter.Own);
        _filter.AddItem("Rival", userData: Filter.Rival);
        _filter.AddItem("Neutral", userData: Filter.Neutral);
        _filter.SelectByUserData(Filter.All);
        _filter.ItemSelected += (_, _) => Refresh();

        _list = new ScrollControl(this, "PrismList");
        _list.EnableScroll(false, true);
        _list.SetBounds(10, 40, 380, 250);
    }

    /// <summary>
    /// Rebuild the list of prisms.
    /// </summary>
    public void Refresh()
    {
        _list.DeleteAllChildren();
        var meFaction = Globals.Me?.Faction ?? Intersect.Enums.Factions.Neutral;
        var filter = (Filter)(_filter.SelectedItem?.UserData ?? Filter.All);

        var maps = MapInstance.Lookup.Values
            .OfType<MapInstance>()
            .Where(map => map.PrismMaxHp > 0)
            .Where(map => filter switch
            {
                Filter.Own => map.PrismOwner == meFaction,
                Filter.Rival =>
                    map.PrismOwner != meFaction && map.PrismOwner != Intersect.Enums.Factions.Neutral,
                Filter.Neutral => map.PrismOwner == Intersect.Enums.Factions.Neutral,
                _ => true,
            })
            .OrderBy(map => map.PrismNextVulnerabilityStart ?? DateTime.MaxValue);

        foreach (var map in maps)
        {
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

