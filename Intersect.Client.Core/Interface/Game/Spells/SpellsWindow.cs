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
using System.Text;

namespace Intersect.Client.Interface.Game.Spells;

public partial class SpellsWindow : Window
{
    public List<SpellItem> Items { get; private set; } = [];

    private readonly ScrollControl _slotContainer;
    private readonly ContextMenu _contextMenu;
    private readonly Label _lblSpellPoints;

    // Panel de detalle
    private readonly ImagePanel _detailPanel;
    private readonly ImagePanel _iconPanel;
    private readonly Label _nameLabel;
    private readonly Label _levelLabel;
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
            OverflowY = OverflowBehavior.Scroll,
            Margin = new Margin(6, 6, 4, 6),
        };
        _slotContainer.SetSize(240, 340);
        _slotContainer.SetPosition(6, 6);

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
        };
        _nameLabel.SetSize(200, 20);
        _nameLabel.SetPosition(64, 8);

        _levelLabel = new Label(_detailPanel, "SpellLevel")
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
        };
        _levelLabel.SetSize(200, 16);
        _levelLabel.SetPosition(64, 30);

        _descLabel = new Label(_detailPanel, "SpellDesc")
        {
            FontName = "sourcesansproblack",
            FontSize = 10,
            AutoSizeToContents = true,
        };
        _descLabel.SetSize(270, 200); // Altura flexible si tienes auto scroll más adelante
        _descLabel.SetPosition(8, 70);

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
        Items.Clear();
        _slotContainer.DeleteAllChildren();

        var max = Options.Instance.Player.MaxSpells;
        const int slotHeight = 92;
        for (var i = 0; i < max; i++)
        {
            var item = new SpellItem(this, _slotContainer, i, _contextMenu);
            var y = 4 + i * slotHeight;
            item.SetBounds(4, y, _slotContainer.Width - 8, slotHeight - 4);
            Items.Add(item);
        }

        var innerH = 4 + max * slotHeight;
        _slotContainer.SetInnerSize(_slotContainer.Width, innerH);

        if (_selectedSlot >= Items.Count)
        {
            _selectedSlot = -1;
        }

        if (_selectedSlot >= 0 && _selectedSlot < Items.Count)
        {
            Items[_selectedSlot].SetSelected(true);
        }
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
            sb.AppendLine(desc.Description);

        // Ejemplo rápido de tiempos (si tienes SpellMath/Efectivo, úsalo aquí)
        if (desc.CooldownDuration > 0)
            sb.AppendLine($"{Strings.SpellDescription.Cooldown} {TimeSpan.FromMilliseconds(desc.CooldownDuration).WithSuffix()}");
        if (desc.CastDuration > 0)
            sb.AppendLine($"{Strings.SpellDescription.CastTime} {TimeSpan.FromMilliseconds(desc.CastDuration).WithSuffix()}");

        _descLabel.Text = sb.ToString().Trim();
    }

    public override void Hide()
    {
        _contextMenu?.Close();
        base.Hide();
    }
}
