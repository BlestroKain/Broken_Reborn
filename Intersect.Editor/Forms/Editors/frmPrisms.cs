using System;
using System.Linq;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Framework.Core.GameObjects.Prisms;

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
        txtWindows.Lines = p.Windows.Select(w => $"{w.Day}|{w.Start:hh\\:mm}|{w.End:hh\\:mm}").ToArray();
        txtModules.Lines = p.Modules.Select(m => m.Name).ToArray();
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

            p.Windows = txtWindows.Lines
                .Select(line => line.Split('|'))
                .Where(parts => parts.Length == 3 && Enum.TryParse(parts[0], true, out DayOfWeek _))
                .Select(parts => new VulnerabilityWindow
                {
                    Day = Enum.Parse<DayOfWeek>(parts[0], true),
                    Start = TimeSpan.TryParse(parts[1], out var s) ? s : TimeSpan.Zero,
                    End = TimeSpan.TryParse(parts[2], out var e) ? e : TimeSpan.Zero
                })
                .ToList();

            p.Modules = txtModules.Lines
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => new PrismModule { Name = l.Trim() })
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
}
