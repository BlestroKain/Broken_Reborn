using System;
using System.Drawing;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Controls;

/// <summary>
/// Simple control allowing the user to draw and resize a rectangle to represent an area.
/// </summary>
public class AreaEditor : Control
{
    private Rectangle _area = new();
    private bool _dragging;
    private bool _moving;
    private Point _start;
    private Point _offset;

    /// <summary>
    ///     Raised when the area changes.
    /// </summary>
    public event EventHandler? AreaChanged;

    public AreaEditor()
    {
        DoubleBuffered = true;
        BackColor = Color.Black;
        Size = new Size(200, 150);
    }

    /// <summary>
    ///     Gets or sets the edited area.
    /// </summary>
    public Rectangle Area
    {
        get => _area;
        set
        {
            _area = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(Color.Lime, 2);
        e.Graphics.DrawRectangle(pen, Area);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (Area.Contains(e.Location))
        {
            _moving = true;
            _offset = new Point(e.X - Area.X, e.Y - Area.Y);
        }
        else
        {
            _dragging = true;
            _start = e.Location;
            Area = new Rectangle(e.Location, Size.Empty);
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (_moving)
        {
            Area = new Rectangle(e.X - _offset.X, e.Y - _offset.Y, Area.Width, Area.Height);
            AreaChanged?.Invoke(this, EventArgs.Empty);
        }
        else if (_dragging)
        {
            var x = Math.Min(_start.X, e.X);
            var y = Math.Min(_start.Y, e.Y);
            var w = Math.Abs(e.X - _start.X);
            var h = Math.Abs(e.Y - _start.Y);
            Area = new Rectangle(x, y, w, h);
            AreaChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        _dragging = false;
        _moving = false;
    }
}
