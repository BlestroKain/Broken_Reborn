using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Bestiary
{
    public partial class BestiaryWindow : Window
    {
        private readonly Label _lblStats;
        private readonly Label _lblMagic;
        private readonly Label _lblLoot;
        private readonly Label _lblSpellCombat;

        private readonly BestiaryStatsComponent _stats;
        private readonly BestiaryMagicComponent _magic;
        private readonly BestiaryLootComponent _loot;
        private readonly BestiarySpellCombatComponent _spellCombat;

        public BestiaryWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Bestiary.Title, false, nameof(BestiaryWindow))
        {
            IsResizable = false;
            SetSize(600, 420);
            SetPosition(100, 100);
            IsVisibleInTree = false;
            IsClosable = true;

            // --- Labels (mismo estilo que tu referencia: set font + override color por control) ---
            _lblStats = new Label(this, "StatsLabel")
            {
                Text = "Stats",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            _lblStats.SetPosition(20, 20);
            _lblStats.SetSize(260, 24);
            _lblStats.SetTextColor(Color.White, ComponentState.Normal);

            _lblMagic = new Label(this, "MagicLabel")
            {
                Text = "Magic",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            _lblMagic.SetPosition(20, 100);
            _lblMagic.SetSize(260, 24);
            _lblMagic.SetTextColor(Color.White, ComponentState.Normal);

            _lblLoot = new Label(this, "LootLabel")
            {
                Text = "Loot",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            _lblLoot.SetPosition(20, 180);
            _lblLoot.SetSize(260, 24);
            _lblLoot.SetTextColor(Color.White, ComponentState.Normal);

            _lblSpellCombat = new Label(this, "SpellCombatLabel")
            {
                Text = "Spell Combat",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            _lblSpellCombat.SetPosition(20, 260);
            _lblSpellCombat.SetSize(260, 24);
            _lblSpellCombat.SetTextColor(Color.White, ComponentState.Normal);

            // --- Componentes (creados y posicionados explícitamente, como en tu referencia) ---
            _stats = new BestiaryStatsComponent(this, "StatsComponent");
            _stats.SetPosition(20, 50);
            _stats.SetSize(560, 40);

            _magic = new BestiaryMagicComponent(this, "MagicComponent");
            _magic.SetPosition(20, 130);
            _magic.SetSize(560, 40);

            _loot = new BestiaryLootComponent(this, "LootComponent");
            _loot.SetPosition(20, 210);
            _loot.SetSize(560, 40);

            _spellCombat = new BestiarySpellCombatComponent(this, "SpellCombatComponent");
            _spellCombat.SetPosition(20, 290);
            _spellCombat.SetSize(560, 40);

            // Cargar UI JSON al final (igual que en la referencia)
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            Reset();
        }

        protected override void EnsureInitialized()
        {
            // Igual que tu referencia: recargar layout JSON aquí también
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            // Reubicar por si el skin ajusta algo
            _stats.SetPosition(20, 50);
            _magic.SetPosition(20, 130);
            _loot.SetPosition(20, 210);
            _spellCombat.SetPosition(20, 290);

            _stats.SetSize(560, 40);
            _magic.SetSize(560, 40);
            _loot.SetSize(560, 40);
            _spellCombat.SetSize(560, 40);

            // Si tus componentes exponen CorrectWidth como antes, puedes llamarlo aquí:
            _stats.CorrectWidth();
            _magic.CorrectWidth();
            _loot.CorrectWidth();
            _spellCombat.CorrectWidth();

            Reset();
        }
        public void Update()
        {
            if (!IsVisibleInParent)
                return;

            // Mantener layout y ancho correcto de cada bloque
            // (igual que en EnsureInitialized, pero sin recargar el JSON)
            _stats.SetPosition(20, 50);
            _magic.SetPosition(20, 130);
            _loot.SetPosition(20, 210);
            _spellCombat.SetPosition(20, 290);

            _stats.SetSize(560, 40);
            _magic.SetSize(560, 40);
            _loot.SetSize(560, 40);
            _spellCombat.SetSize(560, 40);

            _stats.CorrectWidth();
            _magic.CorrectWidth();
            _loot.CorrectWidth();
            _spellCombat.CorrectWidth();
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
            _stats.Lock();
            _magic.Lock();
            _loot.Lock();
            _spellCombat.Lock();
        }
    }
}
