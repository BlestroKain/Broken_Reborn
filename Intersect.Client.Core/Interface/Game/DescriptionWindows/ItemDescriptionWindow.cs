using Intersect;
using Intersect.Enums;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Core;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.GameObjects;
using Intersect.GameObjects.Ranges;
using Intersect.Utilities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Interface.Game.DescriptionWindows.Components;

namespace Intersect.Client.Interface.Game.DescriptionWindows;

public partial class ItemDescriptionWindow() : DescriptionWindowBase(Interface.GameUi.GameCanvas, "DescriptionWindow")
{
    private ItemDescriptor? _itemDescriptor;
    private ItemProperties? _itemProperties;
    private int _amount;
    private string? _valueLabel;

    public void Show(
        ItemDescriptor item,
        int amount,
        ItemProperties? itemProperties = default,
        string valueLabel = ""
    )
    {
        _itemDescriptor = item;
        _amount = amount;
        _itemProperties = itemProperties;
        _valueLabel = valueLabel;

        SetupDescriptionWindow();
        PositionToHoveredControl();

        // If a spell, also display the spell description!
        if (_itemDescriptor.ItemType == ItemType.Spell && _itemDescriptor.SpellId != Guid.Empty)
        {
            if (Canvas == default || InputHandler.HoveredControl is not { } hoveredControl)
            {
                return;
            }

            var spellDesc = Interface.GameUi.SpellDescriptionWindow;
            spellDesc.Show(_itemDescriptor.SpellId);

            // we need to control the spell desc window position here
            var hoveredPos = InputHandler.HoveredControl.ToCanvas(new Point(0, 0));
            var windowX = 0;
            var windowY = Y;

            // if spell desc is out of screen
            if (Y + spellDesc.Height > Canvas.Height)
            {
                windowY = Canvas.Height - spellDesc.Height;
            }

            // let consider some situations
            // item desc is on right side of hovered icon
            if (X >= hoveredPos.X + hoveredControl.Width)
            {
                // lets try to put spell desc on left side of hovered icon
                windowX = hoveredPos.X - spellDesc.Width;

                // ops, our spell desc is out of screen
                if (windowX < 0)
                {
                    windowX = X + Width;
                }
            }
            else
            {
                // lets try to put spell desc on right side of hovered icon
                windowX = hoveredPos.X + hoveredControl.Width;

                // ops, our spell desc is out of screen
                if (windowX + spellDesc.Width > Canvas.Width)
                {
                    windowX = X - spellDesc.Width;
                }
            }

            spellDesc.SetPosition(windowX, windowY);
        }

        base.Show();
    }

    public override void Hide()
    {
        if (Interface.GameUi.ItemDescriptionWindow == this)
        {
            Interface.GameUi.GameCanvas.RemoveChild(Interface.GameUi.ItemDescriptionWindow, true);
            Interface.GameUi.ItemDescriptionWindow = default;
        }

        if (Interface.GameUi.SpellDescriptionWindow != default)
        {
            Interface.GameUi.GameCanvas.RemoveChild(Interface.GameUi.SpellDescriptionWindow, true);
            Interface.GameUi.SpellDescriptionWindow = default;
        }
    }

    protected void SetupDescriptionWindow()
    {
        ClearComponents();

        if (_itemDescriptor == default)
        {
            return;
        }

        // Set up our header information.
        SetupHeader();

        // Set up our item limit information.
        SetupItemLimits();

        // if we have a description, set that up.
        if (!string.IsNullOrWhiteSpace(_itemDescriptor.Description))
        {
            SetupDescription();
        }

        // Set up information depending on the item type.
        switch (_itemDescriptor.ItemType)
        {
            case ItemType.Equipment:
                SetupEquipmentInfo();
                break;

            case ItemType.Consumable:
                SetupConsumableInfo();
                break;

            case ItemType.Spell:
                SetupSpellInfo();
                break;

            case ItemType.Bag:
                SetupBagInfo();
                break;
            case ItemType.Resource:
                SetupResourceInfo();
                break;
        }

        // If this item belongs to a set, display the set progress.
        if (_itemDescriptor.SetId != Guid.Empty)
        {
            SetupSetInfo();
        }

        // Set up additional information such as amounts and shop values.
        SetupExtraInfo();

        // Resize the container, correct the display and position our window.
        FinalizeWindow();
    }
    protected void SetupResourceInfo()
    {
        AddDivider();
        var rows = AddRowContainer();

        AppendPetInfo(rows);

        if (_itemDescriptor.Subtype == "Rune")
        {
           
            var amount = _itemDescriptor.AmountModifier;

            if (amount != 0)
            {
                if (Enum.IsDefined(typeof(Stat), _itemDescriptor.TargetStat))
                {
                    rows.AddKeyValueRow("Stat Modified", _itemDescriptor.TargetStat.ToString());
                    rows.AddKeyValueRow("Bonus", $"{(amount > 0 ? "+" : "")}{amount}");
                }
                else if (Enum.IsDefined(typeof(Vital), _itemDescriptor.TargetVital))
                {
                    rows.AddKeyValueRow("Vital Modified", _itemDescriptor.TargetVital.ToString());
                    rows.AddKeyValueRow("Bonus", $"{(amount > 0 ? "+" : "")}{amount}");
                }
            }
        }

        rows.SizeToChildren(true, true);
    }

    private static bool PlayerHasSetItem(Guid itemId)
    {
        if (Globals.Me is not { } player)
        {
            return false;
        }

        foreach (var slots in player.MyEquipment.Values)
        {
            foreach (var slot in slots)
            {
                if (slot >= 0 && slot < player.Inventory.Length)
                {
                    var inv = player.Inventory[slot];
                    if (inv != null && inv.ItemId == itemId)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    protected void SetupSetInfo()
    {
        if (_itemDescriptor?.SetId == Guid.Empty)
        {
            return;
        }

        var set = _itemDescriptor.Set;
        if (set == null || set.ItemIds.Count == 0)
        {
            return;
        }

        var equippedCount = set.ItemIds.Count(PlayerHasSetItem);

        AddDivider();
        var desc = AddDescription();
        desc.AddText($"{set.Name} ({equippedCount}/{set.ItemIds.Count})", CustomColors.ItemDesc.Primary);
        if (!string.IsNullOrWhiteSpace(set.Description))
        {
            desc.AddLineBreak();
            desc.AddText(set.Description, CustomColors.ItemDesc.Muted);
        }

        if (equippedCount > 1 && set.HasBonuses)
        {
            var bonusDesc = AddDescription();
            var bonusRows = new RowContainerComponent(bonusDesc, "SetBonusRows");

            var (stats, percentStats, vitals, vitalsRegen, percentVitals, effects) = set.GetBonuses(equippedCount);

            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i] != 0 || percentStats[i] != 0)
                {
                    Strings.ItemDescription.StatCounts.TryGetValue(i, out var statName);
                    var parts = new List<string>();
                    if (stats[i] != 0)
                    {
                        parts.Add($"{(stats[i] > 0 ? "+" : string.Empty)}{stats[i]}");
                    }
                    if (percentStats[i] != 0)
                    {
                        parts.Add($"{percentStats[i]}%");
                    }

                    bonusRows.AddKeyValueRow(statName, string.Join(" / ", parts), Color.Magenta, Color.White);
                }
            }

            for (int i = 0; i < vitals.Length; i++)
            {
                if (vitals[i] != 0 || percentVitals[i] != 0 || vitalsRegen[i] != 0)
                {
                    Strings.ItemDescription.Vitals.TryGetValue(i, out var vitName);
                    var parts = new List<string>();
                    if (vitals[i] != 0)
                    {
                        parts.Add($"{(vitals[i] > 0 ? "+" : string.Empty)}{vitals[i]}");
                    }
                    if (percentVitals[i] != 0)
                    {
                        parts.Add($"{percentVitals[i]}%");
                    }
                    if (vitalsRegen[i] != 0)
                    {
                        parts.Add($"Regen {vitalsRegen[i]}%");
                    }

                    bonusRows.AddKeyValueRow(vitName, string.Join(" / ", parts), Color.Cyan, Color.White);
                }
            }

            foreach (var effect in effects)
            {
                if (effect.Type == ItemEffect.None || effect.Percentage == 0)
                {
                    continue;
                }

                if (!Strings.ItemDescription.BonusEffects.TryGetValue((int)effect.Type, out var effectName))
                {
                    continue;
                }

                bonusRows.AddKeyValueRow(effectName, $"+{effect.Percentage}%", Color.Yellow, Color.White);
            }

            bonusRows.SizeToChildren(true, true);
            bonusDesc.SizeToChildren(true, true);
        }

        AddDivider();
        var container = AddComponent(new ComponentBase(this, "SetItemsContainer"));
        int x = 0;
        foreach (var id in set.ItemIds)
        {
            if (!ItemDescriptor.TryGet(id, out var setDesc) || setDesc == null)
            {
                continue;
            }

            var comp = new SetItemComponent(container, $"SetItem_{id}");
            var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, setDesc.Icon);
            if (tex != null)
            {
                comp.SetIcon(tex, setDesc.Color);
            }

            comp.SetStatus(PlayerHasSetItem(id));
            comp.SetPosition(x, 0);
            comp.SizeToChildren(true, true);
            x += comp.Width + 4;
        }

        container.SetSize(x, 32);
    }

    protected void SetupHeader()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Create our header, but do not load our layout yet since we're adding components manually.
        var header = AddHeader();

        // Set up the icon, if we can load it.
        var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, _itemDescriptor.Icon);
        if (tex != null)
        {
            header.SetIcon(tex, _itemDescriptor.Color);
        }

        // Set up the header as the item name.
        CustomColors.Items.Rarities.TryGetValue(_itemDescriptor.Rarity, out var rarityColor);
        var itemLevel = _itemProperties?.EnchantmentLevel ?? 0;
        var name = _itemDescriptor.Name;
        if (itemLevel > 0)
        {
            name += $" +{itemLevel}";
        }
        header.SetTitle(name, rarityColor ?? Color.White);

        // Set up the description telling us what type of item this is.
        Strings.ItemDescription.ItemTypes.TryGetValue((int)_itemDescriptor.ItemType, out var typeDesc);

        if (_itemDescriptor.ItemType == ItemType.Equipment)
        {
            var equipSlot = Options.Instance.Equipment.Slots[_itemDescriptor.EquipmentSlot];

            if (_itemDescriptor.EquipmentSlot == Options.Instance.Equipment.WeaponSlot && !string.IsNullOrWhiteSpace(_itemDescriptor.Subtype))
            {
                // 🔥 Mostrar solo el subtipo si es arma
                header.SetSubtitle($"{_itemDescriptor.Subtype}", Color.White);
            }
            else
            {
                // 🔥 Mostrar info extra si no tiene subtipo o no es arma
                var extraInfo = _itemDescriptor.EquipmentSlot == Options.Instance.Equipment.WeaponSlot && _itemDescriptor.TwoHanded
                    ? $"{Strings.ItemDescription.TwoHand} {equipSlot}"
                    : equipSlot;

                header.SetSubtitle($"{extraInfo}", Color.White);
            }
        }
        else
        {
            // 🔥 Mostrar subtipo si lo tiene, si no solo el tipo
            var subtypeInfo = !string.IsNullOrWhiteSpace(_itemDescriptor.Subtype)
       ? _itemDescriptor.Subtype
       : typeDesc?.ToString() ?? "";
            header.SetSubtitle(subtypeInfo, Color.White);
        }

        // Set up the item rarity label.
        try
        {
            if (Options.Instance.Items.TryGetRarityName(_itemDescriptor.Rarity, out var rarityName))
            {
                _ = Strings.ItemDescription.Rarity.TryGetValue(rarityName, out var rarityLabel);
                header.SetDescription(rarityLabel, rarityColor ?? Color.White);
            }
        }
        catch (Exception exception)
        {
            ApplicationContext.Context.Value?.Logger.LogError(
                exception,
                "Error setting rarity description for rarity {Rarity}",
                _itemDescriptor.Rarity
            );
            throw;
        }

        header.SizeToChildren(true, false);
    }

    protected void SetupItemLimits()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Gather up what limitations apply to this item.
        var limits = new List<string>();
        if (!_itemDescriptor.CanBank)
        {
            limits.Add(Strings.ItemDescription.Banked);
        }
        if (!_itemDescriptor.CanGuildBank)
        {
            limits.Add(Strings.ItemDescription.GuildBanked);
        }
        if (!_itemDescriptor.CanBag)
        {
            limits.Add(Strings.ItemDescription.Bagged);
        }
        if (!_itemDescriptor.CanTrade)
        {
            limits.Add(Strings.ItemDescription.Traded);
        }
        if (!_itemDescriptor.CanDrop)
        {
            limits.Add(Strings.ItemDescription.Dropped);
        }
        if (!_itemDescriptor.CanSell)
        {
            limits.Add(Strings.ItemDescription.Sold);
        }

        // Do we have any limitations? If so, generate a display for it.
        if (limits.Count > 0)
        {
            // Add a divider.
            AddDivider();

            // Add the actual description.
            var description = AddDescription();

            // Commbine our lovely limitations to a single line and display them.
            description.AddText(Strings.ItemDescription.ItemLimits.ToString(string.Join(", ", limits)), Color.White);
        }
    }

    protected void SetupDescription()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Add a divider.
        AddDivider();

        // Add the actual description.
        var description = AddDescription();
        description.AddText(Strings.ItemDescription.Description.ToString(_itemDescriptor.Description), Color.White);
    }

    private int GetStatDifference(int index)
    {
        if (_itemDescriptor == null || index >= _itemDescriptor.StatsGiven.Length)
            return 0;

        var newValue = _itemDescriptor.StatsGiven[index];
        if (_itemProperties?.StatModifiers?.Length > index)
        {
            newValue += _itemProperties.StatModifiers[index];
        }

        var slot = _itemDescriptor.EquipmentSlot;
        if (slot >= 0 &&
            Globals.Me.MyEquipment.TryGetValue(slot, out var equippedSlots) &&
            equippedSlots.Count > 0)
        {
            var firstSlot = equippedSlots[0];
            if (firstSlot >= 0 && firstSlot < Globals.Me.Inventory.Length)
            {
                var equipped = Globals.Me.Inventory[firstSlot];
                if (equipped?.Descriptor != null && index < equipped.Descriptor.StatsGiven.Length)
                {
                    var oldValue = equipped.Descriptor.StatsGiven[index];
                    if (equipped.ItemProperties?.StatModifiers?.Length > index)
                    {
                        oldValue += equipped.ItemProperties.StatModifiers[index];
                    }

                    return newValue - oldValue;
                }
            }
        }

        return newValue;
    }

    private int GetVitalDifference(int index)
    {
        if (_itemDescriptor == null || index >= _itemDescriptor.VitalsGiven.Length)
            return 0;

        var baseValue = _itemDescriptor.VitalsGiven[index];
        var bonusValue = _itemProperties?.VitalModifiers?.Length > index ? _itemProperties.VitalModifiers[index] : 0;
        var newValue = baseValue + bonusValue;

        var slot = _itemDescriptor.EquipmentSlot;
        if (slot >= 0 &&
            Globals.Me.MyEquipment.TryGetValue(slot, out var equippedSlots) &&
            equippedSlots.Count > 0)
        {
            var firstSlot = equippedSlots[0];
            if (firstSlot >= 0 && firstSlot < Globals.Me.Inventory.Length)
            {
                var equipped = Globals.Me.Inventory[firstSlot];
                if (equipped?.Descriptor != null && index < equipped.Descriptor.VitalsGiven.Length)
                {
                    var equippedBase = equipped.Descriptor.VitalsGiven[index];
                    var equippedBonus = equipped.ItemProperties?.VitalModifiers?.Length > index ? equipped.ItemProperties.VitalModifiers[index] : 0;

                    return (int)(newValue - (equippedBase + equippedBonus));
                }
            }
        }

        return (int)newValue;
    }
    private int GetVitalRegenDifference(int index)
    {
        if (_itemDescriptor == null || index >= _itemDescriptor.VitalsRegen.Length)
            return 0;

        var newValue = _itemDescriptor.VitalsRegen[index];
        var slot = _itemDescriptor.EquipmentSlot;

        if (slot >= 0 &&
            Globals.Me.MyEquipment.TryGetValue(slot, out var equippedSlots) &&
            equippedSlots.Count > 0)
        {
            var firstSlot = equippedSlots[0];
            if (firstSlot >= 0 && firstSlot < Globals.Me.Inventory.Length)
            {
                var equipped = Globals.Me.Inventory[firstSlot];
                if (equipped?.Descriptor != null && index < equipped.Descriptor.VitalsRegen.Length)
                {
                    var oldValue = equipped.Descriptor.VitalsRegen[index];
                    return (int)(newValue - oldValue);
                }
            }
        }

        return (int)newValue;
    }
    private int GetDamageDifference()
    {
        if (_itemDescriptor == null) return 0;

        var newValue = _itemDescriptor.Damage;
        var slot = _itemDescriptor.EquipmentSlot;

        if (slot >= 0 &&
            Globals.Me.MyEquipment.TryGetValue(slot, out var equippedSlots) &&
            equippedSlots.Count > 0)
        {
            var firstSlot = equippedSlots[0];
            if (firstSlot >= 0 && firstSlot < Globals.Me.Inventory.Length)
            {
                var equipped = Globals.Me.Inventory[firstSlot];
                var oldValue = equipped?.Descriptor?.Damage ?? 0;
                return newValue - oldValue;
            }
        }

        return newValue;
    }
    private int GetAttackSpeedDifference()
    {
        if (_itemDescriptor == null) return 0;

        var newValue = _itemDescriptor.AttackSpeedValue;
        var slot = _itemDescriptor.EquipmentSlot;

        if (slot >= 0 &&
            Globals.Me.MyEquipment.TryGetValue(slot, out var equippedSlots) &&
            equippedSlots.Count > 0)
        {
            var firstSlot = equippedSlots[0];
            if (firstSlot >= 0 && firstSlot < Globals.Me.Inventory.Length)
            {
                var equipped = Globals.Me.Inventory[firstSlot];
                if (equipped?.Descriptor != null)
                {
                    var oldValue = equipped.Descriptor.AttackSpeedValue;
                    return newValue - oldValue;
                }
            }
        }

        return newValue;
    }
    private void AddRowWithDifference(RowContainerComponent rows, string key, string value, int diff)
    {
        if (diff != 0)
        {
            var diffText = diff > 0 ? $"+{diff}" : diff.ToString();
            var color = diff > 0 ? CustomColors.ItemDesc.Better : CustomColors.ItemDesc.Worse;
            rows.AddKeyValueRow(key, $"{value} ({diffText})", CustomColors.ItemDesc.Muted, color);
        }
        else
        {
            rows.AddKeyValueRow(key, value, CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Muted);
        }
    }
    private void AddRowWithDifferenceAndPercent(RowContainerComponent rows, string key, string value, int diff, int percentDiff)
    {
        if (diff != 0 || percentDiff != 0)
        {
            var diffText = diff > 0 ? $"+{diff}" : diff.ToString();
            var percentText = percentDiff > 0 ? $"+{percentDiff}%" : $"{percentDiff}%";
            var color = diff > 0 || percentDiff > 0 ? CustomColors.ItemDesc.Better : CustomColors.ItemDesc.Worse;
            rows.AddKeyValueRow(key, $"{value} ({diffText}, {percentText})", CustomColors.ItemDesc.Muted, color);
        }
        else
        {
            rows.AddKeyValueRow(key, value, CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Muted);
        }
    }

    protected void SetupEquipmentInfo()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        if (Globals.Me is not { } player)
        {
            return;
        }

        AddDivider();
        var rows = AddRowContainer();

        // ====== Weapon Info ======
        if (_itemDescriptor.EquipmentSlot == Options.Instance.Equipment.WeaponSlot)
        {
            // Base Damage
            var dmgDiff = GetDamageDifference();
            AddRowWithDifference(rows, Strings.ItemDescription.BaseDamage, _itemDescriptor.Damage.ToString(), dmgDiff);

            // Damage Type
            Strings.ItemDescription.DamageTypes.TryGetValue(_itemDescriptor.DamageType, out var damageType);
            rows.AddKeyValueRow(Strings.ItemDescription.BaseDamageType, damageType);

            if (_itemDescriptor.Scaling > 0)
            {
                Strings.ItemDescription.Stats.TryGetValue(_itemDescriptor.ScalingStat, out var stat);
                rows.AddKeyValueRow(Strings.ItemDescription.ScalingStat, stat);
                rows.AddKeyValueRow(Strings.ItemDescription.ScalingPercentage, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.Scaling));
            }

            // Crit Chance
            if (_itemDescriptor.CritChance > 0)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.CritChance, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.CritChance));
                rows.AddKeyValueRow(Strings.ItemDescription.CritMultiplier, Strings.ItemDescription.Multiplier.ToString(_itemDescriptor.CritMultiplier));
            }

            // Attack Speed
            if (_itemDescriptor.AttackSpeedModifier == 0)
            {
                // Calculate real attack speed considering current equipment
                var agility = player.Stat[(int)Stat.Agility];

                if (Globals.Me.MyEquipment.TryGetValue(Options.Instance.Equipment.WeaponSlot, out var equippedSlots) && equippedSlots.Count > 0)
                {
                    var firstWeaponSlot = equippedSlots[0];
                    if (firstWeaponSlot >= 0 && firstWeaponSlot < Options.Instance.Player.MaxInventory)
                    {
                        var equippedWeapon = player.Inventory[firstWeaponSlot];
                        var randomStats = equippedWeapon.ItemProperties?.StatModifiers;
                        if (randomStats != null)
                        {
                            agility = (int)Math.Round(agility / ((100 + equippedWeapon.Descriptor.PercentageStatsGiven[(int)Stat.Agility]) / 100f));
                            agility -= equippedWeapon.Descriptor.StatsGiven[(int)Stat.Agility];
                            agility -= randomStats[(int)Stat.Agility];
                        }
                    }
                }

                var statModifiers = _itemProperties?.StatModifiers;
                if (statModifiers != default)
                {
                    agility += _itemDescriptor.StatsGiven[(int)Stat.Agility];
                    agility += statModifiers[(int)Stat.Agility];
                    agility += (int)Math.Floor(agility * (_itemDescriptor.PercentageStatsGiven[(int)Stat.Agility] / 100f));
                }

                rows.AddKeyValueRow(Strings.ItemDescription.AttackSpeed, TimeSpan.FromMilliseconds(player.CalculateAttackTime(agility)).WithSuffix());
            }
            else if (_itemDescriptor.AttackSpeedModifier == 1)
            {
                var diff = GetAttackSpeedDifference();
                AddRowWithDifference(rows, Strings.ItemDescription.AttackSpeed, TimeSpan.FromMilliseconds(_itemDescriptor.AttackSpeedValue).WithSuffix(), diff);
            }
            else if (_itemDescriptor.AttackSpeedModifier == 2)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.AttackSpeed, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.AttackSpeedValue));
            }
        }

        // ====== Shield Info ======
        if (_itemDescriptor.EquipmentSlot == Options.Instance.Equipment.ShieldSlot)
        {
            if (_itemDescriptor.BlockChance > 0)
                rows.AddKeyValueRow(Strings.ItemDescription.BlockChance, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.BlockChance));
            if (_itemDescriptor.BlockAmount > 0)
                rows.AddKeyValueRow(Strings.ItemDescription.BlockAmount, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.BlockAmount));
            if (_itemDescriptor.BlockAbsorption > 0)
                rows.AddKeyValueRow(Strings.ItemDescription.BlockAbsorption, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.BlockAbsorption));
        }

        // ====== Vitals ======
        var vitalCount = Enum.GetValues<Vital>().Length;

        for (var i = 0; i < vitalCount; i++)
        {
            var baseValue = i < _itemDescriptor.VitalsGiven.Length ? _itemDescriptor.VitalsGiven[i] : 0;
            var percentValue = i < _itemDescriptor.PercentageVitalsGiven.Length ? _itemDescriptor.PercentageVitalsGiven[i] : 0;
            var enchantBonus = _itemProperties?.VitalModifiers?.Length > i ? _itemProperties.VitalModifiers[i] : 0;


            var totalFlat = baseValue + enchantBonus;
            var label = Strings.ItemDescription.Vitals[i];

            var diff = GetVitalDifference(i);
            if (totalFlat != 0 && percentValue != 0)
            {
                AddRowWithDifferenceAndPercent(rows, label, totalFlat.ToString(), diff, percentValue);
            }
            else if (totalFlat != 0)
            {
                AddRowWithDifference(rows, label, totalFlat.ToString(), diff);
            }
            else if (percentValue != 0)
            {
                rows.AddKeyValueRow(label, Strings.ItemDescription.Percentage.ToString(percentValue), CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Muted);
            }
        }

        // ====== Vitals Regen ======
        for (var i = 0; i < vitalCount; i++)
        {
            if (_itemDescriptor.VitalsRegen.Length > i && _itemDescriptor.VitalsRegen[i] != 0)
            {
                var diff = GetVitalRegenDifference(i);
                AddRowWithDifference(rows, Strings.ItemDescription.VitalsRegen[i], Strings.ItemDescription.Percentage.ToString(_itemDescriptor.VitalsRegen[i]), diff);
            }
        }


        // ====== Stats ======
        for (var statIndex = 0; statIndex < Enum.GetValues<Stat>().Length; statIndex++)
        {
            var statLabel = Strings.ItemDescription.StatCounts[statIndex];
            ItemRange? rangeForStat = default;
            var percentageGivenForStat = _itemDescriptor.PercentageStatsGiven[statIndex];

            var statModifiers = _itemProperties?.StatModifiers;
            if (statModifiers != default || !_itemDescriptor.TryGetRangeFor((Stat)statIndex, out rangeForStat) || rangeForStat?.LowRange == rangeForStat?.HighRange)
            {
                var flatValueGivenForStat = _itemDescriptor.StatsGiven[statIndex];
                if (statModifiers != default)
                    flatValueGivenForStat += statModifiers[statIndex];

                flatValueGivenForStat += rangeForStat?.LowRange ?? 0;

                var diff = GetStatDifference(statIndex);
                if (flatValueGivenForStat != 0 && percentageGivenForStat != 0)
                {
                    AddRowWithDifferenceAndPercent(rows, statLabel, flatValueGivenForStat.ToString(), diff, percentageGivenForStat);
                }
                else if (flatValueGivenForStat != 0)
                {
                    AddRowWithDifference(rows, statLabel, flatValueGivenForStat.ToString(), diff);
                }
                else if (percentageGivenForStat != 0)
                {
                    rows.AddKeyValueRow(statLabel, Strings.ItemDescription.Percentage.ToString(percentageGivenForStat), CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Muted);
                }
            }
            else if (_itemDescriptor.TryGetRangeFor((Stat)statIndex, out var range))
            {
                var statLow = _itemDescriptor.StatsGiven[statIndex] + range.LowRange;
                var statHigh = _itemDescriptor.StatsGiven[statIndex] + range.HighRange;

                var statMessage = Strings.ItemDescription.StatGrowthRange.ToString(statLow, statHigh);
                if (percentageGivenForStat != 0)
                {
                    statMessage = Strings.ItemDescription.RegularAndPercentage.ToString(statMessage, percentageGivenForStat);
                }

                rows.AddKeyValueRow(statLabel, statMessage);


                // ====== Bonus Effects ======
                foreach (var effect in _itemDescriptor.Effects)
                {
                    if (effect.Type != ItemEffect.None && effect.Percentage != 0)
                    {
                        rows.AddKeyValueRow(Strings.ItemDescription.BonusEffects[(int)effect.Type], Strings.ItemDescription.Percentage.ToString(effect.Percentage));
                    }
                }

                rows.SizeToChildren(true, true);
            }
        }
        rows.SizeToChildren(true, true);
    }

    private void AppendPetInfo(RowContainerComponent rows)
    {
        if (_itemDescriptor?.Pet is not { } petData)
        {
            return;
        }

        if (petData.PetDescriptorId == Guid.Empty
            && string.IsNullOrWhiteSpace(petData.PetNameOverride)
            && !petData.SummonOnEquip
            && !petData.DespawnOnUnequip
            && !petData.BindOnEquip)
        {
            return;
        }

        var petDescriptor = PetDescriptor.Get(petData.PetDescriptorId);
        var displayName = !string.IsNullOrWhiteSpace(petData.PetNameOverride)
            ? petData.PetNameOverride
            : petDescriptor?.Name ?? Strings.General.None;

        rows.AddKeyValueRow(Strings.ItemDescription.PetSummons, displayName);

        if (petData.SummonOnEquip)
        {
            rows.AddKeyValueRow(Strings.ItemDescription.PetSummonOnEquip, Strings.ItemDescription.Enabled);
        }

        if (petData.DespawnOnUnequip)
        {
            rows.AddKeyValueRow(Strings.ItemDescription.PetDespawnOnUnequip, Strings.ItemDescription.Enabled);
        }

        if (petData.BindOnEquip)
        {
            rows.AddKeyValueRow(Strings.ItemDescription.PetBindOnEquip, Strings.ItemDescription.Enabled);
        }

        if (!string.IsNullOrWhiteSpace(petData.PetNameOverride))
        {
            rows.AddKeyValueRow(Strings.ItemDescription.PetNameOverride, petData.PetNameOverride);
        }
    }
    protected void SetupConsumableInfo()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Add a divider.
        AddDivider();

        // Add a row component.
        var rows = AddRowContainer();

        // Consumable data.
        if (_itemDescriptor.Consumable != null)
        {
            if (_itemDescriptor.Consumable.Value > 0 && _itemDescriptor.Consumable.Percentage > 0)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.ConsumableTypes[(int)_itemDescriptor.Consumable.Type], Strings.ItemDescription.RegularAndPercentage.ToString(_itemDescriptor.Consumable.Value, _itemDescriptor.Consumable.Percentage));
            }
            else if (_itemDescriptor.Consumable.Value > 0)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.ConsumableTypes[(int)_itemDescriptor.Consumable.Type], _itemDescriptor.Consumable.Value.ToString());
            }
            else if (_itemDescriptor.Consumable.Percentage > 0)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.ConsumableTypes[(int)_itemDescriptor.Consumable.Type], Strings.ItemDescription.Percentage.ToString(_itemDescriptor.Consumable.Percentage));
            }
        }

        // Resize and position the container.
        rows.SizeToChildren(true, true);
    }

    protected void SetupSpellInfo()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Add a divider.
        AddDivider();

        // Add a row component.
        var rows = AddRowContainer();

        // Spell data.
        if (_itemDescriptor.Spell != null)
        {
            if (_itemDescriptor.QuickCast)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.CastSpell.ToString(_itemDescriptor.Spell.Name), string.Empty);
            }
            else
            {
                rows.AddKeyValueRow(Strings.ItemDescription.TeachSpell.ToString(_itemDescriptor.Spell.Name), string.Empty);
            }

            if (_itemDescriptor.SingleUse)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.SingleUse, string.Empty);
            }
        }

        // Resize and position the container.
        rows.SizeToChildren(true, true);
    }

    protected void SetupBagInfo()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Add a divider.
        AddDivider();

        // Add a row component.
        var rows = AddRowContainer();

        // Bag data.
        rows.AddKeyValueRow(Strings.ItemDescription.BagSlots, _itemDescriptor.SlotCount.ToString());

        // Resize and position the container.
        rows.SizeToChildren(true, true);
    }

    protected void SetupExtraInfo()
    {
        if (_itemDescriptor == default)
        {
            return;
        }

        // Our list of data to add, should we need to.
        var data = new List<Tuple<string, string>>();

        // Display our amount, but only if we are stackable and have more than one.
        if (_itemDescriptor.IsStackable && _amount > 1)
        {
            data.Add(new Tuple<string, string>(Strings.ItemDescription.Amount, _amount.ToString("N0").Replace(",", Strings.Numbers.Comma)));
        }

        // Display item drop chance if configured.
        if (_itemDescriptor.DropChanceOnDeath > 0)
        {
            data.Add(new Tuple<string, string>(Strings.ItemDescription.DropOnDeath, Strings.ItemDescription.Percentage.ToString(_itemDescriptor.DropChanceOnDeath)));
        }

        // Display shop value if we have one.
        if (!string.IsNullOrWhiteSpace(_valueLabel))
        {
            data.Add(new Tuple<string, string>(_valueLabel, string.Empty));
        }

        // Do we have any data to display? If so, generate the element and add the data to it.
        if (data.Count > 0)
        {
            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            foreach (var item in data)
            {
                rows.AddKeyValueRow(item.Item1, item.Item2);
            }

            // Resize and position the container.
            rows.SizeToChildren(true, true);
        }
    }
}
