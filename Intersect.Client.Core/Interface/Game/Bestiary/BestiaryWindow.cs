using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Bestiary;

public partial class BestiaryWindow : Window
{
    private BestiaryStatsComponent? _stats;
    private BestiaryMagicComponent? _magic;
    private BestiaryLootComponent? _loot;
    private BestiarySpellCombatComponent? _spellCombat;

    public BestiaryWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Bestiary.Title, false, nameof(BestiaryWindow))
    {
        Alignment = [Alignments.Center];
        MinimumSize = new Point(x: 400, y: 300);
        IsResizable = true;
        IsClosable = true;
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        _stats = new BestiaryStatsComponent(this, "StatsComponent");
        _stats.SetPosition(10, 30);

        _magic = new BestiaryMagicComponent(this, "MagicComponent");
        _magic.SetPosition(10, 80);

        _loot = new BestiaryLootComponent(this, "LootComponent");
        _loot.SetPosition(10, 130);

        _spellCombat = new BestiarySpellCombatComponent(this, "SpellCombatComponent");
        _spellCombat.SetPosition(10, 180);

        Resized += (_, _) =>
        {
            _stats?.CorrectWidth();
            _magic?.CorrectWidth();
            _loot?.CorrectWidth();
            _spellCombat?.CorrectWidth();
        };

        Reset();
    }

    public override void Show()
    {
        Reset();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        Reset();
    }

    private void Reset()
    {
        _stats?.Lock();
        _magic?.Lock();
        _loot?.Lock();
        _spellCombat?.Lock();
    }
}
