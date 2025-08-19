using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors;

public partial class FrmSpell
{
    private DarkGroupBox grpProgression = null!;
    private DataGridView grdProgression = null!;

    private void InitializeProgressionTab()
    {
        // Group box container
        grpProgression = new DarkGroupBox
        {
            Text = "Progresión (5 niveles)",
            ForeColor = Color.FromArgb(220, 220, 220),
            BackColor = Color.FromArgb(45, 45, 48),
            BorderColor = Color.FromArgb(90, 90, 90),
            Location = new Point(603, 6),
            Size = new Size(542, 210)
        };

        // Data grid setup
        grdProgression = new DataGridView
        {
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeColumns = false,
            AllowUserToResizeRows = false,
            BackgroundColor = Color.FromArgb(45, 45, 48),
            BorderStyle = BorderStyle.None,
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
            EnableHeadersVisualStyles = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            Location = new Point(7, 18),
            Size = new Size(528, 185)
        };

        // Columns for progression properties
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nivel", ReadOnly = true });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "HP Δ" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "MP Δ" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Poder+" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Escalado%" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cast Δ" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CD Δ" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Buff Str%" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Buff Dur%" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Debuff Str%" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Debuff Dur%" });
        grdProgression.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Unlocks AoE" });
        grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "AoE Δ" });

        // Pre-populate five rows for levels 1-5
        for (var i = 1; i <= 5; i++)
        {
            grdProgression.Rows.Add(i, 0, 0, 0, 0f, 0, 0, 0f, 0f, 0f, 0f, false, 0);
        }

        grpProgression.Controls.Add(grdProgression);
        pnlContainer.Controls.Add(grpProgression);
    }
}

