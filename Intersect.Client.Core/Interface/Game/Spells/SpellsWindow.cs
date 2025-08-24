using Intersect.Client.Core;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.Utilities;
using Intersect.Framework.Core.GameObjects.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Interface;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellsWindow : Window
{
    public List<SpellItem> Items { get; private set; } = [];

    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly Label _lblSpellPoints;
    private Base _innerSlotPanel;

    // Panel de detalle
    private ScrollControl _detailsScroll;
    private Base _levelTabs;
    private SpellPreviewWindow? _previewWin;
    private int _previewLevel;

    private int _selectedSlot = -1;
    private int _lastSpellCount;
    private int _lastSpellPoints;
    private int _lastMaxSpells;
    private bool _needsDetailsUpdate;

    public SpellsWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Spells.Title, false, nameof(SpellsWindow))
    {
        Alignment = [Alignments.Bottom, Alignments.Right];
        MinimumSize = new Point(560, 400);
        Margin = new Margin(0, 0, 15, 60);
        IsVisibleInTree = false;
        IsResizable = false;
        IsClosable = true;

        // LISTA IZQUIERDA
        _slotContainer = new ScrollControl(this, "SpellsContainer")
        {
            Dock = Pos.Left,
            Width = 240,
            OverflowX = OverflowBehavior.Hidden,
            OverflowY = OverflowBehavior.Auto,
            Margin = new Margin(6, 6, 4, 6),
        };
        _slotContainer.SetSize(240, 340);
        _slotContainer.SetPosition(6, 6);
        _slotContainer.EnableScroll(false, true);

        // PANEL DETALLE DERECHA
        _levelTabs = new Base(this)
        {
            IsVisibleInParent = false,
        };
        _levelTabs.SetPosition(250, 6);
        _levelTabs.SetSize(300, 20);

        _detailsScroll = new ScrollControl(this)
        {
            AutoHideBars = true,
            IsVisibleInParent = false,
        };
        _detailsScroll.EnableScroll(false, true);
        _detailsScroll.SetPosition(250, 30);
        _detailsScroll.SetSize(300, 316);

        Interface.EnqueueInGame(() => _previewWin = new SpellPreviewWindow(_detailsScroll));

        // CONTEXT MENU
        _contextMenu = new ContextMenu(gameCanvas, "SpellContextMenu")
        {
            IsVisibleInParent = false,
            IconMarginDisabled = true,
            ItemFont = GameContentManager.Current.GetFont("sourcesansproblack"),
            ItemFontSize = 10,
        };

        // PUNTOS
        _lblSpellPoints = new Label(this, "LblSpellPoints")
        {
            Dock = Pos.Bottom,
            Padding = new Padding(6),
            AutoSizeToContents = true,
            Text = "Puntos de hechizo: 0",
        };
        _lblSpellPoints.SetSize(200, 18); // ancho arbitrario, no afecta por auto-size
        _lblSpellPoints.SetPosition(10, 360); // justo abajo

        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        _lastSpellCount = -1;
        _lastSpellPoints = -1;
        _lastMaxSpells = -1;
        _needsDetailsUpdate = true;
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        InitItemContainer();
        Refresh();
    }
    private void InitItemContainer()
    {
        // Limpia lista lógica
        Items.Clear();

        // Si ya existe el panel interno, elimínalo correctamente
        if (_innerSlotPanel != null)
        {
            // En esta GWEN usamos ClearChildren() y luego Dispose()
            _innerSlotPanel.ClearChildren();
            _innerSlotPanel.Dispose();
            _innerSlotPanel = null;
        }

        // Recrea el panel interno dentro del ScrollControl
        _innerSlotPanel = new Base(_slotContainer, "SpellsInnerPanel");

        // Calcula un ancho interno que no choque con la barra vertical
        var innerWidth = Math.Max(0, _slotContainer.Width - _slotContainer.VerticalScrollBar.Width - 2);

        _innerSlotPanel.SetPosition(0, 0);
        _innerSlotPanel.SetSize(innerWidth, 1); // altura real se fija más abajo

        // Crea y posiciona cada SpellItem dentro del panel interno
        var max = Options.Instance.Player.MaxSpells;
        const int slotHeight = 40;
        for (var i = 0; i < max; i++)
        {
            var item = new SpellItem(this, _innerSlotPanel, i, _contextMenu);

            var y = 4 + i * slotHeight;
            item.SetBounds(4, y, innerWidth - 8, slotHeight - 4);

            // (opcional) selección visual inicial si hace falta
            if (_selectedSlot == i) item.SetSelected(true);

            Items.Add(item);
        }

        // Altura total del contenido para que el Scroll se active
        var innerHeight = 4 + max * slotHeight;
        _innerSlotPanel.SetSize(innerWidth, innerHeight);

        // MUY IMPORTANTE: actualizar el área “interna” del Scroll
        _slotContainer.SetInnerSize(innerWidth, innerHeight);
        _slotContainer.UpdateScrollBars();

        // Corrige selección si quedó fuera de rango
        if (_selectedSlot >= Items.Count) _selectedSlot = -1;
    }
    public void SelectSlot(int slotIndex)
    {
        if (_selectedSlot == slotIndex)
        {
            return;
        }

        _selectedSlot = slotIndex;
        for (var i = 0; i < Items.Count; i++)
        {
            Items[i].SetSelected(i == _selectedSlot);
        }

        _needsDetailsUpdate = true;
    }

    public void Update()
    {
        if (!IsVisibleInTree)
            return;

        var me = Globals.Me;
        var spells = me?.Spells;
        var spellCount = spells?.Length ?? 0;
        var maxSpells = Options.Instance.Player.MaxSpells;

        if (spellCount != _lastSpellCount || maxSpells != _lastMaxSpells)
        {
            InitItemContainer();
            _lastSpellCount = spellCount;
            _lastMaxSpells = maxSpells;
            _needsDetailsUpdate = true;
        }

        var spellPoints = me?.SpellPoints ?? 0;
        if (spellPoints != _lastSpellPoints)
        {
            _lblSpellPoints.Text = $"Puntos de hechizo: {spellPoints}";
            _lastSpellPoints = spellPoints;
            _needsDetailsUpdate = true;
        }

        var slotCount = Math.Min(Items.Count, maxSpells);
        for (var i = 0; i < slotCount; i++)
            Items[i].Update();

        if (_selectedSlot < 0)
        {
            if (spells != null)
            {
                for (var i = 0; i < Math.Min(spells.Length, Items.Count); i++)
                {
                    if (spells[i].Id != Guid.Empty)
                    {
                        _selectedSlot = i;
                        Items[i].SetSelected(true);
                        _needsDetailsUpdate = true;
                        break;
                    }
                }
            }
        }

        if (_needsDetailsUpdate)
        {
            UpdateDetails();
            _needsDetailsUpdate = false;
        }
    }

    public void Refresh()
    {
        // Re-layaout por si cambió la resolución o MaxSpells
        InitItemContainer();
        _needsDetailsUpdate = true;
    }

    private void UpdateDetails()
    {
        var me = Globals.Me;
        var spells = me?.Spells;

        if (me == null || spells == null || _selectedSlot < 0 || _selectedSlot >= spells.Length)
        {
            _detailsScroll.IsVisibleInParent = false;
            _levelTabs.IsVisibleInParent = false;
            return;
        }

        var id = spells[_selectedSlot].Id;
        if (id == Guid.Empty)
        {
            _detailsScroll.IsVisibleInParent = false;
            _levelTabs.IsVisibleInParent = false;
            return;
        }

        _detailsScroll.IsVisibleInParent = true;
        _levelTabs.IsVisibleInParent = true;

        OnSelectSpell(id);
    }

    private void BuildLevelTabs(SpellDescriptor desc, int currentLevel)
    {
        foreach (var c in _levelTabs.Children.ToArray())
        {
            c.Dispose();
        }

        var max = Options.Instance.Player.MaxSpellLevel;
        var x = 0;

        for (int l = 1; l <= max; l++)
        {
            var btn = new Button(_levelTabs) { Text = l.ToString() };
            btn.SetPosition(x, 0);
            btn.SetSize(24, 20);
            x += 26;

            if (l == currentLevel)
            {
                btn.TextColor = new Color(30, 200, 90, 255);
            }
            if (l > currentLevel)
            {
                btn.TextColor = new Color(170, 170, 170, 255);
            }

            var level = l;
            btn.Clicked += (_, __) =>
            {
                if (_previewWin == null)
                {
                    return;
                }

                _previewLevel = level;
                _previewWin.ShowPreview(desc.Id, _previewLevel);
                _detailsScroll.SetInnerSize(_previewWin.Width, _previewWin.Height);
                _detailsScroll.UpdateScrollBars();
            };
        }

        _levelTabs.SizeToChildren(true, true);
        _levelTabs.Invalidate();
    }

    private void OnSelectSpell(Guid spellId)
    {
        var desc = SpellDescriptor.Get(spellId);
        if (desc == null)
        {
            return;
        }

        var currentLevel = Globals.Me?.Spells.FirstOrDefault(s => s.Id == spellId)?.Properties?.Level ?? 1;

        BuildLevelTabs(desc, currentLevel);

        if (_previewWin == null)
        {
            return;
        }

        _previewLevel = currentLevel;
        _previewWin.ShowPreview(spellId, _previewLevel);
        _detailsScroll.SetInnerSize(_previewWin.Width, _previewWin.Height);
        _detailsScroll.UpdateScrollBars();
    }

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
