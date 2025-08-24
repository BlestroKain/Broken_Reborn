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
using System.Collections.Generic;
using System.Text;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellsWindow : Window
{
    public List<SpellItem> Items { get; private set; } = [];

    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly Label _lblSpellPoints;
    private Base _innerSlotPanel;

    // Panel de detalle
    private readonly ImagePanel _detailPanel;
    private readonly ImagePanel _iconPanel;
    private readonly Label _nameLabel;
    private readonly Label _levelLabel;
    private readonly ScrollControl _descScroll;
    private readonly Label _descLabel;

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
        _detailPanel = new ImagePanel(this, "DetailPanel")
        {
            Dock = Pos.Fill,
            Margin = new Margin(4, 6, 6, 30),
            IsVisibleInParent = false,
        };
        _detailPanel.SetSize(300, 340);
        _detailPanel.SetPosition(250, 6);

        _iconPanel = new ImagePanel(_detailPanel, "SpellIcon");
        _iconPanel.SetBounds(8, 8, 48, 48);

        _nameLabel = new Label(_detailPanel, "SpellName")
        {
            FontName = "sourcesansproblack",
            FontSize = 14,
            TextColor = Color.White,
        };
        _nameLabel.SetSize(200, 20);
        _nameLabel.SetPosition(64, 8);

        _levelLabel = new Label(_detailPanel, "SpellLevel")
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
            TextColor = Color.Yellow,
        };
        _levelLabel.SetSize(200, 16);
        _levelLabel.SetPosition(64, 30);

        _descScroll = new ScrollControl(_detailPanel, "SpellDescScroll")
        {
            OverflowY = OverflowBehavior.Scroll,
            OverflowX = OverflowBehavior.Hidden,
        };
        _descScroll.EnableScroll(false, true);
        _descScroll.SetBounds(8, 70, 270, 200);

        _descLabel = new Label(_descScroll, "SpellDesc")
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
            AutoSizeToContents = true,
        };
        _descLabel.SetPosition(0, 0);

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
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        var id = spells[_selectedSlot].Id;
        var level = spells[_selectedSlot].Level;

        if (id == Guid.Empty || !SpellDescriptor.TryGet(id, out var desc))
        {
            _detailPanel.IsVisibleInParent = false;
            return;
        }

        _detailPanel.IsVisibleInParent = true;

        // Icono
        var tex = GameContentManager.Current.GetTexture(TextureType.Spell, desc.Icon);
        if (tex != null)
        {
            _iconPanel.Texture = tex;
            _iconPanel.IsVisibleInParent = true;
        }
        else
        {
            _iconPanel.IsVisibleInParent = false;
        }

        // Nombre y nivel
        _nameLabel.Text = desc.Name;
        _levelLabel.Text = Strings.EntityBox.Level.ToString(level);

        // Descripción/estadísticas básicas
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(desc.Description))
        {
            sb.AppendLine(desc.Description);
        }

        if (desc.CooldownDuration > 0)
        {
            sb.AppendLine($"{Strings.SpellDescription.Cooldown} {TimeSpan.FromMilliseconds(desc.CooldownDuration).WithSuffix()}");
        }

        if (desc.CastDuration > 0)
        {
            sb.AppendLine($"{Strings.SpellDescription.CastTime} {TimeSpan.FromMilliseconds(desc.CastDuration).WithSuffix()}");
        }

        // Detalles por nivel usando LevelUpgrades y CustomUpgrades
        var currentProps = desc.GetPropertiesForLevel(level);
        if (desc.LevelUpgrades.TryGetValue(level, out var levelUpgrades) &&
            levelUpgrades?.CustomUpgrades?.Count > 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.AppendLine($"Nivel {level}:");
            foreach (var kv in levelUpgrades.CustomUpgrades)
            {
                sb.AppendLine($"{kv.Key}: {kv.Value}");
            }
        }

        SpellProperties? nextProps = null;
        if (level < Options.Instance.Player.MaxSpellLevel &&
            desc.LevelUpgrades.TryGetValue(level + 1, out var nextLevelUpgrades))
        {
            nextProps = desc.GetPropertiesForLevel(level + 1);

            if (nextLevelUpgrades.CustomUpgrades.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine($"Nivel {level + 1}:");
                foreach (var kv in nextLevelUpgrades.CustomUpgrades)
                {
                    sb.AppendLine($"{kv.Key}: {kv.Value}");
                }
            }

            sb.AppendLine();
            sb.AppendLine($"Costo para subir al nivel {level + 1}: {level} punto(s) de hechizo");
        }

        _descLabel.Text = sb.ToString().Trim();
        _descLabel.Width = _descScroll.Width - _descScroll.VerticalScrollBar.Width;
        _descLabel.SizeToContents();
        _descScroll.SetInnerSize(_descScroll.Width, _descLabel.Height);

        // Tooltip para próxima progresión basada en diferencias
        var tooltip = string.Empty;
        if (nextProps != null)
        {
            var tooltipSb = new StringBuilder();
            tooltipSb.AppendLine($"Nivel {level + 1}:");
            foreach (var kv in nextProps.CustomUpgrades)
            {
                var currentVal = currentProps.CustomUpgrades.TryGetValue(kv.Key, out var val) ? val : 0;
                var diff = kv.Value - currentVal;
                if (diff != 0)
                {
                    tooltipSb.AppendLine($"{kv.Key}: {(diff >= 0 ? "+" : string.Empty)}{diff}");
                }
            }

            tooltipSb.AppendLine($"Costo: {level} punto(s) de hechizo");
            tooltip = tooltipSb.ToString().Trim();
        }

        _levelLabel.SetToolTipText(tooltip);
    }

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
