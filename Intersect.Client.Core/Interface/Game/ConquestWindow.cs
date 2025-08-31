using System;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Prisms;

namespace Intersect.Client.Interface.Game;

/// <summary>
/// Conquest window: lists prisms with owner, state, and next vulnerability (with countdown).
/// </summary>
public sealed class ConquestWindow : Window
{
    // Layout base
    private const int W = 520;
    private const int H = 380;
    private const int PAD = 10;

    // UI
    private readonly Label _title;
    private readonly ComboBox _filter;
    private readonly ComboBox _sort;
    private readonly Button _refreshBtn;
    private readonly ScrollControl _list;
    private Label _empty;

    private enum Filter
    {
        All,
        Own,
        Rival,
        Neutral,
    }

    private enum SortBy
    {
        NextVulnAsc,
        NextVulnDesc,
        NameAsc,
        NameDesc
    }

    public ConquestWindow(Canvas gameCanvas)
        : base(gameCanvas, nameof(ConquestWindow), false, nameof(ConquestWindow))
    {
        IsResizable = false;

        // Ventana
        SetSize(W, H);
        SetPosition(40, 60);
        Title = "Conquista";

        // Contenedor raíz
        var root = new Base(this, "ConquestRoot");
        root.SetPosition(0, 0);
        root.SetSize(W, H);

        // Título interno
        _title = new Label(root, "ConquestTitle")
        {
            Text = "Prismas conocidos",
            TextColor = Color.White
        };
        _title.SetPosition(PAD, PAD);
        _title.SetSize(W - (PAD * 2), 18);

        // Filtro por dueño
        _filter = new ComboBox(root, "OwnerFilter");
        _filter.SetBounds(PAD, PAD + 24, 140, 22);
        _filter.AddItem("Todos", userData: Filter.All);
        _filter.AddItem("Propios", userData: Filter.Own);
        _filter.AddItem("Rivales", userData: Filter.Rival);
        _filter.AddItem("Neutral", userData: Filter.Neutral);
        _filter.SelectByUserData(Filter.All);
        _filter.ItemSelected += (_, __) => Refresh();

        // Orden
        _sort = new ComboBox(root, "SortBy");
        _sort.SetBounds(PAD + 150, PAD + 24, 160, 22);
        _sort.AddItem("Vulnerable ↑", userData: SortBy.NextVulnAsc);
        _sort.AddItem("Vulnerable ↓", userData: SortBy.NextVulnDesc);
        _sort.AddItem("Nombre A–Z", userData: SortBy.NameAsc);
        _sort.AddItem("Nombre Z–A", userData: SortBy.NameDesc);
        _sort.SelectByUserData(SortBy.NextVulnAsc);
        _sort.ItemSelected += (_, __) => Refresh();

        // Botón Refresh
        _refreshBtn = new Button(root, "RefreshButton");
        _refreshBtn.SetText("Actualizar");
        _refreshBtn.SetBounds(PAD + 320, PAD + 24, 90, 22);
        _refreshBtn.Clicked += (_, __) => Refresh();

        // Cabecera columnas
        var header = new Base(root, "ListHeader");
        header.SetBounds(PAD, PAD + 54, W - (PAD * 2), 22);
        header.ShouldDrawBackground = true;
      

        MakeHeaderLabel(header, "Mapa", 0, 8, 180);
        MakeHeaderLabel(header, "Facción", 180, 8, 90);
        MakeHeaderLabel(header, "Estado", 270, 8, 90);
        MakeHeaderLabel(header, "Próx. ventana", 360, 8, 140);

        // Lista (scroll vertical)
        _list = new ScrollControl(root, "PrismList");
        _list.EnableScroll(false, true);
        _list.SetBounds(PAD, PAD + 78, W - (PAD * 2), H - (PAD + 78 + PAD));

        // Mensaje “vacío”
        _empty = new Label(_list, "EmptyLabel")
        {
            Text = "No hay prismas que coincidan.",
            TextColor = Color.FromArgb(200, 200, 200)
        };
        _empty.SetPosition(8, 8);
        _empty.SetSize(_list.Width - 16, 20);
        _empty.IsHidden = true;

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        Refresh();
    }

    private static void MakeHeaderLabel(Base parent, string text, int x, int y, int w)
    {
        var lbl = new Label(parent, $"Hdr_{text.Replace(" ", "")}") { Text = text, TextColor = Color.FromArgb(210, 210, 210) };
        lbl.SetPosition(x + 8, y);
        lbl.SetSize(w - 8, 16);
    }

    /// <summary>
    /// Reconstruye la lista de prismas.
    /// </summary>
    public void Refresh()
    {
        _list.DeleteAllChildren();
        var meFaction = Globals.Me?.Faction ?? Factions.Neutral;
        var filter = (Filter)(_filter.SelectedItem?.UserData ?? Filter.All);
        var sort = (SortBy)(_sort.SelectedItem?.UserData ?? SortBy.NextVulnAsc);

        var maps = MapInstance.Lookup.Values
            .OfType<MapInstance>()
            .Where(m => m.PrismMaxHp > 0)
            .Where(m => filter switch
            {
                Filter.Own => m.PrismOwner == meFaction,
                Filter.Rival => m.PrismOwner != meFaction && m.PrismOwner != Factions.Neutral,
                Filter.Neutral => m.PrismOwner == Factions.Neutral,
                _ => true,
            });

        maps = sort switch
        {
            SortBy.NextVulnAsc => maps.OrderBy(m => m.PrismNextVulnerabilityStart ?? DateTime.MaxValue),
            SortBy.NextVulnDesc => maps.OrderByDescending(m => m.PrismNextVulnerabilityStart ?? DateTime.MinValue),
            SortBy.NameAsc => maps.OrderBy(m => m.Name),
            SortBy.NameDesc => maps.OrderByDescending(m => m.Name),
            _ => maps
        };

        var y = 4;
        var any = false;

        foreach (var map in maps)
        {
            any = true;
            var row = new Base(_list, $"Row_{map.Id}");
            row.SetPosition(0, y);
            row.SetSize(_list.Width - 18, 28); // -18 ≈ ancho scrollbar
            row.ShouldDrawBackground = true;
           

            // Columna: Mapa (clicable → centrar mapa si lo deseas)
            var name = new Button(row, $"BtnMap_{map.Id}");
            name.SetText(map.Name ?? "¿Sin nombre?");
            name.SetPosition(8, 4);
            name.SetSize(170, 20);
            name.Clicked += (_, __) => FocusMap(map);

            // Facción (color)
            var fac = new Label(row, $"LblFaction_{map.Id}");
            fac.Text = FactionName(map.PrismOwner);
            fac.TextColor = FactionColor(map.PrismOwner);
            fac.SetPosition(186, 6);
            fac.SetSize(80, 18);

            // Estado (color por estado)
            var state = new Label(row, $"LblState_{map.Id}");
            state.Text = map.PrismState.ToString();
            state.TextColor = StateColor(map.PrismState);
            state.SetPosition(268, 6);
            state.SetSize(80, 18);

            // Próxima ventana + countdown
            var (vulnText, cdText) = VulnStrings(map.PrismNextVulnerabilityStart);
            var vuln = new Label(row, $"LblVuln_{map.Id}") { Text = $"{vulnText}  {cdText}" };
            vuln.SetPosition(350, 6);
            vuln.SetSize(row.Width - 350 - 8, 18);

            y += 30;
        }

        _empty.IsHidden = any;
        if (!any)
        {
            // Mostrar mensaje vacío
            _empty = _empty ?? new Label(_list, "EmptyLabel");
            _empty.Text = "No hay prismas que coincidan.";
            _empty.SetPosition(8, 8);
            _empty.SetSize(_list.Width - 16, 20);
            _empty.IsHidden = false;
        }
    }

    private static string FactionName(Factions f) => f switch
    {
        Factions.Serolf => "Serolf",
        Factions.Nidraj => "Nidraj",
        _ => "Neutral"
    };

    private static Color FactionColor(Factions f) => f switch
    {
        Factions.Serolf => Color.FromArgb(120, 180, 255),
        Factions.Nidraj => Color.FromArgb(255, 120, 120),
        _ => Color.FromArgb(200, 200, 200)
    };

    private static Color StateColor(PrismState s) => s switch
    {
        PrismState.UnderAttack => Color.FromArgb(255, 160, 120),
        PrismState.Vulnerable => Color.FromArgb(255, 220, 130),
        PrismState.Dominated => Color.FromArgb(160, 220, 160),
        PrismState.Placed => Color.FromArgb(180, 180, 200),
        PrismState.Destroyed => Color.FromArgb(200, 140, 140),
        _ => Color.FromArgb(210, 210, 210)
    };

    private static (string when, string countdown) VulnStrings(DateTime? nextUtc)
    {
        if (nextUtc is null) return ("N/A", "");
        var local = nextUtc.Value.ToLocalTime();
        var when = local.ToString("g");
        var delta = local - DateTime.Now;
        var cd =
            delta.TotalSeconds <= 0 ? "(en curso)" :
            delta.TotalDays >= 1 ? $"(en {Math.Floor(delta.TotalDays)}d {delta.Hours}h)" :
            delta.TotalHours >= 1 ? $"(en {delta.Hours}h {delta.Minutes}m)" :
                                       $"(en {Math.Max(0, delta.Minutes)}m)";
        return (when, cd);
    }

    private void FocusMap(MapInstance map)
    {
        // Opcional: centrar cámara al mapa/posición del prisma, si tu cliente lo soporta.
        // MapInstance.SetCurrent(map.Id) / Globals.Me?.Warp(...) / abrir minimapa, etc.
       // Globals.Me?.ShowNotification($"Centrar en: {map.Name}");
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }
}
