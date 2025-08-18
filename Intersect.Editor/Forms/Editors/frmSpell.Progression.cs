using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    public partial class FrmSpell
    {
        private DarkGroupBox grpProgression = null!;
        private DataGridView grdProgression = null!;

        private void InitializeProgressionTab()
        {
            // Group box container (WinForms/DarkUI -> System.Drawing.Color)
            grpProgression = new DarkGroupBox
            {
                Text = "Progresión (5 niveles)",
                ForeColor = System.Drawing.Color.FromArgb(220, 220, 220),
                BackColor = System.Drawing.Color.FromArgb(45, 45, 48),
                // BorderColor NO existe en DarkGroupBox estándar. Si tenías una subclase que lo añade, déjala; si no, quítala.
                // BorderColor = Color.FromArgb(90, 90, 90),

                Location = new System.Drawing.Point(603, 6),
                Size = new Size(542, 210)
            };

            // Data grid setup
            grdProgression = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 48),
                BorderStyle = BorderStyle.None,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Location = new System.Drawing.Point(7, 18),
                Size = new Size(528, 185),
                GridColor = System.Drawing.Color.FromArgb(90, 90, 90)
            };

            // Colores de celdas/headers (opcional, para que se vea Dark)
            grdProgression.GridColor = System.Drawing.Color.FromArgb(90, 90, 90);
            grdProgression.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grdProgression.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            grdProgression.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            grdProgression.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            grdProgression.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            grdProgression.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(70, 73, 75);
            grdProgression.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;


            // Columns for progression properties
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nivel", ReadOnly = true, FillWeight = 60 });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "HP Δ" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "MP Δ" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Poder+" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Escalado%" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cast Δ (ms)" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CD Δ (ms)" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Buff Str%" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Buff Dur%" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Debuff Str%" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Debuff Dur%" });
            grdProgression.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Unlocks AoE" });
            grdProgression.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "AoE Δ" });

            // Pre-populate five rows for levels 1-5
            for (var i = 1; i <= 5; i++)
                grdProgression.Rows.Add(i, 0, 0, 0, 0f, 0, 0, 0f, 0f, 0f, 0f, false, 0);

            grpProgression.Controls.Add(grdProgression);
            pnlContainer.Controls.Add(grpProgression);
        }
    }
}
