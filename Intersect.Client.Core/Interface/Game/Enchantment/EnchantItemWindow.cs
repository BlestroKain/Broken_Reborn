using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.Extensions;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;


namespace Intersect.Client.Interface.Game.Enchanting
{
    public partial class EnchantItemWindow : Window
    {
        public List<SlotItem> Items { get; set; } = [];
        private readonly ScrollControl _projectionContainer;
        private readonly ScrollControl _inventoryScroll;
        private readonly ImagePanel _itemSlot;
        private readonly ImagePanel _currencySlot;
        private readonly Checkbox _useAmuletCheckbox;
        private readonly Button _enchantButton;
        private readonly Button _closeButton;

      
        private readonly Label _lblProjection;

        private Item? _selectedItem;
        private Item? _selectedCurrency;

        public EnchantItemWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Enchanting.WindowTitle, false, nameof(EnchantItemWindow))
        {
           IsResizable = false;
            SetSize(600, 500);
            SetPosition(100, 100);
            IsVisibleInTree = false;
            IsResizable = false;
            IsClosable = true;

            _lblProjection = new Label(this, "ProjectionLabel")
            {
                Text = Strings.Enchanting.Projection
            };
            _lblProjection.SetPosition(20, 20);
            _lblProjection.SetSize(260, 30);

            _projectionContainer = new ScrollControl(this, "ProjectionContainer");
            _projectionContainer.SetPosition(20, 60);
            _projectionContainer.SetSize(260, 400);
            _projectionContainer.EnableScroll(false, true);

            _inventoryScroll = new ScrollControl(this, "ItemContainer");
            _inventoryScroll.SetPosition(300, 20);
            _inventoryScroll.SetSize(260, 200);
            _inventoryScroll.EnableScroll(false, true);

            _itemSlot = new ImagePanel(this, "ItemSlot");
            _itemSlot.SetPosition(300, 240);
            _itemSlot.SetSize(80, 80);
            _itemSlot.Texture = Graphics.Renderer.WhitePixel;

            _currencySlot = new ImagePanel(this, "CurrencySlot");
            _currencySlot.SetPosition(400, 240);
            _currencySlot.SetSize(80, 80);
            _currencySlot.Texture = Graphics.Renderer.WhitePixel;

            _useAmuletCheckbox = new Checkbox(this, "UseAmuletCheckbox");
            _useAmuletCheckbox.SetPosition(300, 340);
            _useAmuletCheckbox.SetSize(260, 30);
            _useAmuletCheckbox.Text = Strings.Enchanting.UseAmulet;

            _enchantButton = new Button(this, "EnchantButton");
            _enchantButton.SetPosition(300, 380);
            _enchantButton.SetSize(120, 40);
            _enchantButton.Text = Strings.Enchanting.Enchant;
            _enchantButton.Clicked += OnEnchantButtonClicked;

            _closeButton = new Button(this, "CloseButton");
            _closeButton.SetPosition(440, 380);
            _closeButton.SetSize(120, 40);
            _closeButton.Text = Strings.Enchanting.Close;
            _closeButton.Clicked += (sender, args) => Hide();

            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitItemContainer();
        }

        protected override void EnsureInitialized()
        {
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitItemContainer();
        }

        private void InitItemContainer()
        {
    
            for (int i = 0; i < Options.Instance.Player.MaxInventory; i++)
            {
                var item = new EnchantInventoryItem(this, _inventoryScroll, i,null);
                Items.Add(item);
                PopulateSlotContainer.Populate(_inventoryScroll, Items);
                Update();
            }
        }
        public void SelectItem(Item? item)
        {
            if (item == null || item.Descriptor == null) return;

            _selectedItem = item;

            foreach (var slot in Items)
            {
                if (slot is EnchantInventoryItem enchantSlot)
                {
                    enchantSlot.SetSelected(enchantSlot.SlotIndex == Globals.Me.Inventory.IndexOf(item));
                }
            }

            var itemTexture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Item, item.Descriptor.Icon);
            _itemSlot.Texture = itemTexture ?? Graphics.Renderer.WhitePixel;

            UpdateProjection();
        }

        public void SelectCurrencyItem(Item? item)
        {
            if (item == null || item.Descriptor == null) return;

            _selectedCurrency = item;

            foreach (var slot in Items)
            {
                if (slot is EnchantInventoryItem enchantSlot)
                {
                    enchantSlot.SetSelected(enchantSlot.SlotIndex == Globals.Me.Inventory.IndexOf(item));
                }
            }

            var itemTexture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Item, item.Descriptor.Icon);
            _currencySlot.Texture = itemTexture ?? Graphics.Renderer.WhitePixel;
        }

        public void UpdateProjection()
        {
            _projectionContainer.DeleteAllChildren();

            if (_selectedItem == null || _selectedItem.Descriptor == null)
            {
                _projectionContainer.Hide();
                return;
            }

            _projectionContainer.Show();

            var projectedLevel = _selectedItem.ItemProperties.EnchantmentLevel + 1;
            var successRate = _selectedItem.Descriptor.GetUpgradeSuccessRate(projectedLevel);
            var upgradeCost = _selectedItem.Descriptor.GetUpgradeCost(projectedLevel);

            int yOffset = 0;
            int spacing = 25;
            int labelWidth = 240;
            int labelHeight = 20;

            var lblCurrentLevel = new Label(_projectionContainer, "CurrentLevelLabel")
            {
                Text = $"Nivel Actual: {_selectedItem.ItemProperties.EnchantmentLevel}",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            lblCurrentLevel.SetPosition(10, yOffset);
            lblCurrentLevel.SetSize(labelWidth, labelHeight);
            lblCurrentLevel.SetTextColor(Color.Yellow, ComponentState.Normal);
            yOffset += spacing;

            var lblProjectedLevel = new Label(_projectionContainer, "ProjectedLevelLabel")
            {
                Text = $"Nivel Proyectado: {projectedLevel}",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            lblProjectedLevel.SetPosition(10, yOffset);
            lblProjectedLevel.SetSize(labelWidth, labelHeight);
            lblProjectedLevel.SetTextColor(Color.Green, ComponentState.Normal);
            yOffset += spacing;

            var lblSuccessRate = new Label(_projectionContainer, "SuccessRateLabel")
            {
                Text = $"Tasa de éxito: {successRate * 100:F1}%",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            lblSuccessRate.SetPosition(10, yOffset);
            lblSuccessRate.SetSize(labelWidth, labelHeight);
            lblSuccessRate.SetTextColor(Color.Yellow, ComponentState.Normal);
            yOffset += spacing;

            var lblCost = new Label(_projectionContainer, "UpgradeCostLabel")
            {
                Text = $"Costo: {upgradeCost} monedas",
                FontName = "sourcesansproblack",
                FontSize = 10
            };
            lblCost.SetPosition(10, yOffset);
            lblCost.SetSize(labelWidth, labelHeight);
            lblCost.SetTextColor(Color.Yellow, ComponentState.Normal);
            yOffset += spacing * 2;

            for (var i = 0; i < Enum.GetValues<Stat>().Length; i++)
            {
                var statName = Strings.ItemDescription.StatCounts[i];
                int baseStat = _selectedItem.Descriptor.StatsGiven[i];
                int modStat = _selectedItem.ItemProperties?.StatModifiers[i] ?? 0;
                int currentStat = baseStat + modStat;
                int projectedStat = currentStat;
                double bonusFactor = 0.05;

                for (int lvl = _selectedItem.ItemProperties.EnchantmentLevel + 1; lvl <= projectedLevel; lvl++)
                {
                    int bonus = (int)Math.Ceiling(projectedStat * bonusFactor);
                    projectedStat += bonus;
                }

                if (currentStat == 0 && projectedStat == 0)
                    continue;

                var statColor = projectedStat > currentStat ? Color.Green :
                                (projectedStat < currentStat ? Color.Red : Color.White);

                var lblStat = new Label(_projectionContainer, $"StatLabel_{i}")
                {
                    Text = $"{statName}: {currentStat} → {projectedStat}",
                    FontName = "sourcesansproblack",
                    FontSize = 10
                };
                lblStat.SetPosition(10, yOffset);
                lblStat.SetSize(labelWidth, labelHeight);
                lblStat.SetTextColor(statColor, ComponentState.Normal);
                yOffset += spacing;
            }
            for (var i = 0; i < Enum.GetValues<Vital>().Length; i++)
            {
                var vitalName = Strings.ItemDescription.Vitals[i];

                int baseVital = (int)_selectedItem.Descriptor.VitalsGiven[i];
                int modVital = _selectedItem.ItemProperties?.VitalModifiers[i] ?? 0;
                int currentVital = baseVital + modVital;
                int projectedVital = currentVital;

                double bonusFactor = 0.05;

                for (int lvl = _selectedItem.ItemProperties.EnchantmentLevel + 1; lvl <= projectedLevel; lvl++)
                {
                    int bonus = (int)Math.Ceiling(projectedVital * bonusFactor);
                    projectedVital += bonus;
                }

                if (currentVital == 0 && projectedVital == 0)
                    continue;

                var vitalColor = projectedVital > currentVital ? Color.Green :
                                 (projectedVital < currentVital ? Color.Red : Color.White);

                var lblVital = new Label(_projectionContainer, $"VitalLabel_{i}")
                {
                    Text = $"{vitalName}: {currentVital} → {projectedVital}",
                    FontName = "sourcesansproblack",
                    FontSize = 10
                };
                lblVital.SetPosition(10, yOffset);
                lblVital.SetSize(labelWidth, labelHeight);
                lblVital.SetTextColor(vitalColor, ComponentState.Normal);
                yOffset += spacing;
            }

            _projectionContainer.SizeToChildren(true, true);
        }

        public void Update()
        {
            if (!IsVisibleInParent)
                return;

            if (Globals.Me?.Inventory == default)
                return;

            var slotCount = Math.Min(Options.Instance.Player.MaxInventory, Items.Count);
            for (var slotIndex = 0; slotIndex < slotCount; slotIndex++)
            {
                Items[slotIndex].Update();
            }
        }

        private void OnEnchantButtonClicked(Base sender, MouseButtonState arguments)
        {
            if (_selectedItem == null)
            {
                PacketSender.SendChatMsg("No item selected for enchanting.", 4);
                return;
            }

            if (_selectedCurrency == null)
            {
                PacketSender.SendChatMsg("No currency item selected for enchanting.", 4);
                return;
            }

            var itemIndex = Globals.Me.Inventory.IndexOf(_selectedItem);
            if (itemIndex < 0)
            {
                PacketSender.SendChatMsg("Error identifying selected item.", 4);
                return;
            }

            var currencyId = _selectedCurrency.ItemId;
            var targetLevel = _selectedItem.ItemProperties.EnchantmentLevel + 1;
            var currencyAmount = _selectedItem.Descriptor.GetUpgradeCost(targetLevel);
            var useAmulet = _useAmuletCheckbox.IsChecked;

            if (targetLevel > 8)
            {
                PacketSender.SendChatMsg("Item is already at max enchantment level.", 4);
                return;
            }

            PacketSender.SendEnchantItem(itemIndex, targetLevel, currencyId, currencyAmount, useAmulet);
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}
