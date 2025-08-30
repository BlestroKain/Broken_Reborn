using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Editor.Forms;

namespace Intersect.Editor.Forms.Editors;

public partial class FrmPrisms : Form
{
    public FrmPrisms()
    {
        InitializeComponent();
        Icon = Program.Icon;
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
        if (p != null)
        {
            if (Guid.TryParse(txtMapId.Text, out var mapId))
            {
                p.MapId = mapId;
            }

            p.X = (int)nudX.Value;
            p.Y = (int)nudY.Value;
            p.Level = (int)nudLevel.Value;

            p.Windows = dgvWindows.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Select(r =>
                {
                    var start = TimeSpan.TryParse(r.Cells["colStart"].Value?.ToString(), out var s)
                        ? s
                        : TimeSpan.Zero;
                    var duration = TimeSpan.TryParse(r.Cells["colDuration"].Value?.ToString(), out var d)
                        ? d
                        : TimeSpan.Zero;
                    var end = start + duration;
                    if (end.TotalHours >= 24)
                    {
                        end -= TimeSpan.FromDays(1);
                    }

                    return new VulnerabilityWindow
                    {
                        Day = r.Cells["colDay"].Value is DayOfWeek day ? day : DayOfWeek.Monday,
                        Start = start,
                        End = end
                    };
                })
                .ToList();

            p.Modules = dgvModules.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Select(r => new PrismModule
                {
                    Type = r.Cells["colType"].Value is PrismModuleType t ? t : PrismModuleType.Vision,
                    Level = int.TryParse(r.Cells["colLevel"].Value?.ToString(), out var lvl) ? lvl : 1
                })
                .ToList();

            p.Area = new PrismArea
            {
                X = (int)nudAreaX.Value,
                Y = (int)nudAreaY.Value,
                Width = (int)nudAreaW.Value,
                Height = (int)nudAreaH.Value
            };
        }

        PrismConfig.Save();
        LoadList();
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
}
