using System;
using System.Drawing;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Editor.Core;
using Intersect.Editor.General;

namespace Intersect.Editor.Forms.Controls;

public class MapPicker : UserControl
{
    private readonly AutoDragPanel _container;
    private readonly PictureBox _picture;
    private Image? _mapImage;
    private float _zoom = 1f;
    private int _hoverX = -1;
    private int _hoverY = -1;

    public MapPicker()
    {
        _container = new AutoDragPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true
        };

        _picture = new PictureBox
        {
            Location = new Point(0, 0)
        };

        _picture.Paint += Picture_Paint;
        _picture.MouseMove += Picture_MouseMove;
        _picture.MouseDown += Picture_MouseDown;
        _picture.MouseWheel += Picture_MouseWheel;
        _picture.MouseEnter += (_, _) => _picture.Focus();

        _container.Controls.Add(_picture);
        Controls.Add(_container);
    }

    public Guid MapId { get; private set; } = Guid.Empty;

    public event Action<Guid, int, int>? TileSelected;

    public void SetMap(Guid mapId)
    {
        MapId = mapId;
        _mapImage?.Dispose();
        _mapImage = mapId != Guid.Empty ? Database.LoadMapCacheLegacy(mapId, -1) : null;
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (_mapImage == null)
        {
            _picture.Image = null;
            return;
        }

        _picture.Image = _mapImage;
        _picture.Width = (int)(_mapImage.Width * _zoom);
        _picture.Height = (int)(_mapImage.Height * _zoom);
        _picture.Invalidate();
    }

    private void Picture_Paint(object? sender, PaintEventArgs e)
    {
        if (_hoverX < 0 || _hoverY < 0)
        {
            return;
        }

        var tileW = Options.Instance.Map.TileWidth;
        var tileH = Options.Instance.Map.TileHeight;
        var rect = new Rectangle(
            (int)(_hoverX * tileW * _zoom),
            (int)(_hoverY * tileH * _zoom),
            (int)(tileW * _zoom),
            (int)(tileH * _zoom));

        using var brush = new SolidBrush(Color.FromArgb(80, Color.White));
        e.Graphics.FillRectangle(brush, rect);
        e.Graphics.DrawRectangle(Pens.White, rect);
    }

    private void Picture_MouseMove(object? sender, MouseEventArgs e)
    {
        if (_mapImage == null)
        {
            _hoverX = _hoverY = -1;
            return;
        }

        var tileW = Options.Instance.Map.TileWidth * _zoom;
        var tileH = Options.Instance.Map.TileHeight * _zoom;
        var x = (int)(e.X / tileW);
        var y = (int)(e.Y / tileH);
        if (x < 0 || y < 0 || x >= Options.Instance.Map.MapWidth || y >= Options.Instance.Map.MapHeight)
        {
            _hoverX = _hoverY = -1;
        }
        else
        {
            _hoverX = x;
            _hoverY = y;
        }

        _picture.Invalidate();
    }

    private void Picture_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        if (_hoverX >= 0 && _hoverY >= 0)
        {
            TileSelected?.Invoke(MapId, _hoverX, _hoverY);
        }
    }

    private void Picture_MouseWheel(object? sender, MouseEventArgs e)
    {
        if (_mapImage == null)
        {
            return;
        }

        var oldZoom = _zoom;
        if (e.Delta > 0)
        {
            _zoom = Math.Min(4f, _zoom + 0.1f);
        }
        else if (e.Delta < 0)
        {
            _zoom = Math.Max(0.2f, _zoom - 0.1f);
        }

        if (Math.Abs(_zoom - oldZoom) > 0.001f)
        {
            UpdateImage();
        }
    }
}
