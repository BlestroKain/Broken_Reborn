using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Editor.Forms;
using Intersect.Editor.General;
using Intersect.Editor.Core;


namespace Intersect.Editor.Forms.Editors;

public partial class FrmPrisms : Form
{
    private const int MaxModules = 3;
    public FrmPrisms()
    {
        InitializeComponent();
        Icon = Program.Icon;
        mapPicker.TileSelected += MapPickerOnTileSelected;
        mapPicker.SetMap(Globals.CurrentMap?.Id ?? Guid.Empty);
        nudX.Maximum = Options.Instance.Map.MapWidth - 1;
        nudY.Maximum = Options.Instance.Map.MapHeight - 1;
        LoadList();
    }

    private void LoadList()
    {
        lstPrisms.Items.Clear();
        foreach (var p in PrismConfig.Prisms)
        {
            lstPrisms.Items.Add($"{p.MapId} ({p.X},{p.Y})");
        }
    }

    private AlignmentPrism? SelectedPrism =>
        lstPrisms.SelectedIndex >= 0 && lstPrisms.SelectedIndex < PrismConfig.Prisms.Count
            ? PrismConfig.Prisms[lstPrisms.SelectedIndex]
            : null;

    private void lstPrisms_SelectedIndexChanged(object sender, EventArgs e) => LoadSelected();

    private void LoadSelected()
    {
        var p = SelectedPrism;
        if (p == null)
        {
            return;
        }

        txtMapId.Text = p.MapId.ToString();
        nudX.Value = p.X;
        nudY.Value = p.Y;
        mapPicker.SetMap(p.MapId);
        nudLevel.Value = p.Level;

        dgvWindows.Rows.Clear();
        foreach (var w in p.Windows)
        {
            var duration = w.End - w.Start;
            if (duration < TimeSpan.Zero)
            {
                duration += TimeSpan.FromDays(1);
            }
            dgvWindows.Rows.Add(w.Day, w.Start.ToString(@"hh\:mm"), duration.ToString(@"hh\:mm"));
        }

        dgvModules.Rows.Clear();
        foreach (var m in p.Modules)
        {
            dgvModules.Rows.Add(m.Type, m.Level);
        }

        nudAreaX.Value = p.Area.X;
        nudAreaY.Value = p.Area.Y;
        nudAreaW.Value = p.Area.Width;
        nudAreaH.Value = p.Area.Height;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var prism = new AlignmentPrism { Id = Guid.NewGuid() };
        PrismConfig.Prisms.Add(prism);
        LoadList();
        lstPrisms.SelectedIndex = PrismConfig.Prisms.Count - 1;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var p = SelectedPrism;
        if (p == null)
        {
            return;
        }

        PrismConfig.Prisms.Remove(p);
        LoadList();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var p = SelectedPrism;
        if (p == null)
        {
            return;
        }

        var errors = new List<string>();

        var areaW = (int)nudAreaW.Value;
        var areaH = (int)nudAreaH.Value;
        if (areaW <= 0 || areaH <= 0)
        {
            errors.Add("El área debe tener un ancho y alto mayores que 0.");
        }

        var windows = dgvWindows.Rows
            .Cast<DataGridViewRow>()
            .Where(r => !r.IsNewRow)
            .Select(r =>
            {
                var start = TimeSpan.TryParse(r.Cells["colStart"].Value?.ToString(), out var s) ? s : TimeSpan.Zero;
                var duration = TimeSpan.TryParse(r.Cells["colDuration"].Value?.ToString(), out var d) ? d : TimeSpan.Zero;
                var end = start + duration;
                if (end.TotalHours >= 24)
                {
                    end -= TimeSpan.FromDays(1);
                }

                return new
                {
                    Duration = duration,
                    Window = new VulnerabilityWindow
                    {
                        Day = r.Cells["colDay"].Value is DayOfWeek day ? day : DayOfWeek.Monday,
                        Start = start,
                        End = end
                    }
                };
            })
            .ToList();

        foreach (var w in windows.Where(w => w.Duration <= TimeSpan.Zero))
        {
            errors.Add($"La ventana que inicia a {w.Window.Start:hh\\:mm} debe tener duración mayor que 0.");
        }

        var intervalsByDay = new Dictionary<DayOfWeek, List<(int start, int end)>>();
        void AddInterval(DayOfWeek day, int start, int end)
        {
            if (!intervalsByDay.TryGetValue(day, out var list))
            {
                list = new List<(int, int)>();
                intervalsByDay[day] = list;
            }
            list.Add((start, end));
        }

        foreach (var w in windows.Where(w => w.Duration > TimeSpan.Zero))
        {
            var start = (int)w.Window.Start.TotalMinutes;
            var end = (int)w.Window.End.TotalMinutes;
            if (start < end)
            {
                AddInterval(w.Window.Day, start, end);
            }
            else
            {
                AddInterval(w.Window.Day, start, 24 * 60);
                var nextDay = (DayOfWeek)(((int)w.Window.Day + 1) % 7);
                AddInterval(nextDay, 0, end);
            }
        }

        foreach (var kvp in intervalsByDay)
        {
            var list = kvp.Value.OrderBy(i => i.start).ToList();
            for (var i = 1; i < list.Count; i++)
            {
                if (list[i - 1].end > list[i].start)
                {
                    errors.Add($"Las ventanas de vulnerabilidad se solapan el {kvp.Key}.");
                    break;
                }
            }
        }

        var modules = dgvModules.Rows
            .Cast<DataGridViewRow>()
            .Where(r => !r.IsNewRow)
            .Select(r => new PrismModule
            {
                Type = r.Cells["colType"].Value is PrismModuleType t ? t : PrismModuleType.Vision,
                Level = int.TryParse(r.Cells["colLevel"].Value?.ToString(), out var lvl) ? lvl : 1
            })
            .ToList();

        if (modules.Any(m => m.Level < 1 || m.Level > 3))
        {
            errors.Add("El nivel de cada módulo debe estar entre 1 y 3.");
        }

        if (modules.Count > MaxModules)
        {
            errors.Add($"No se pueden agregar más de {MaxModules} módulos.");
        }

        if (modules.GroupBy(m => m.Type).Any(g => g.Count() > 1))
        {
            errors.Add("No se permiten módulos duplicados.");
        }

        var hasErrors = errors.Any();
        btnSave.Enabled = !hasErrors;
        if (hasErrors)
        {
            MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (Guid.TryParse(txtMapId.Text, out var mapId))
        {
            p.MapId = mapId;
        }

        p.X = (int)nudX.Value;
        p.Y = (int)nudY.Value;
        p.Level = (int)nudLevel.Value;
        p.Windows = windows.Select(w => w.Window).ToList();
        p.Modules = modules;
        p.Area = new PrismArea
        {
            X = (int)nudAreaX.Value,
            Y = (int)nudAreaY.Value,
            Width = areaW,
            Height = areaH
        };

        var index = lstPrisms.SelectedIndex;
        PrismConfig.Save();
        PrismConfig.Load();
        LoadList();
        if (index >= 0 && index < lstPrisms.Items.Count)
        {
            lstPrisms.SelectedIndex = index;
        }
    }
    private void btnWindowAdd_Click(object sender, EventArgs e)
    {
        dgvWindows.Rows.Add(DayOfWeek.Monday, "00:00", "01:00");
    }

    private void btnWindowEdit_Click(object sender, EventArgs e)
    {
        if (dgvWindows.CurrentCell != null)
        {
            dgvWindows.BeginEdit(true);
        }
    }

    private void btnWindowDelete_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in dgvWindows.SelectedRows)
        {
            if (!row.IsNewRow)
            {
                dgvWindows.Rows.Remove(row);
            }
        }
    }

    private void btnModuleAdd_Click(object sender, EventArgs e)
    {
        dgvModules.Rows.Add(PrismModuleType.Vision, 1);
    }

    private void btnModuleDelete_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in dgvModules.SelectedRows)
        {
            if (!row.IsNewRow)
            {
                dgvModules.Rows.Remove(row);
            }
        }
    }

    private void btnAreaSelect_Click(object sender, EventArgs e)
    {
        if (!Guid.TryParse(txtMapId.Text, out var mapId))
        {
            return;
        }

        var selector = new FrmWarpSelection();
        selector.InitForm(true, new List<Guid> { mapId });
        selector.SelectTile(mapId, (int)nudAreaX.Value, (int)nudAreaY.Value);
        selector.ShowDialog();

        if (selector.GetResult())
        {
            nudAreaX.Value = selector.GetX();
            nudAreaY.Value = selector.GetY();
        }
}
    private void MapPickerOnTileSelected(Guid mapId, int x, int y)
    {
        txtMapId.Text = mapId.ToString();
        nudX.Value = Math.Max(nudX.Minimum, Math.Min(nudX.Maximum, x));
        nudY.Value = Math.Max(nudY.Minimum, Math.Min(nudY.Maximum, y));

    }
}
