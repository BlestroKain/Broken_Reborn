using Intersect.Client.Core;
using Intersect.Client.Entities;
using System;
using System.Collections.Generic;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Breaking;
using Intersect.Client.Interface;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.PlayerClass;

namespace Intersect.Client.Interface.Game.Character;


public partial class CharacterWindow:Window
{

    //Equipment List
    public List<EquipmentItem> Items = new List<EquipmentItem>();

    Label mAbilityPwrLabel;

    Button mAddAbilityPwrBtn;

    Button mAddAttackBtn;

    Button mAddDefenseBtn;

    Button mAddMagicResistBtn;

    Button mAddAgilityBtn;
    Button mFactionButton;

    //Stats
    Label mAttackLabel;

    private ImagePanel mCharacterContainer;

    private Label mCharacterLevelAndClass;

    private Label mCharacterName;

    private ImagePanel mCharacterPortrait;

    private string mCharacterPortraitImg = string.Empty;

    private string mCurrentSprite = string.Empty;

    Label mDefenseLabel;

    private ItemProperties mItemProperties = null;

    Label mMagicRstLabel;

    Label mPointsLabel;

    Label mSpeedLabel;
    Label mAgilityLabel;
    Label mDamageLabel;
    Label mCureLabel;
    public ImagePanel[] PaperdollPanels;

    public string[] PaperdollTextures;

    //Location
    public int X;

    public int Y;

    //Extra Buffs
    Label mHpRegen;
    Label mManaRegen;
    Label mLifeSteal;
    Label mAttackSpeed;
    Label mExtraExp;
    Label mLuck;
    Label mTenacity;
    Label mCooldownReduction;
    Label mManaSteal;
    Label mSpeedBuff;
    Label mDamageBuff;
    Label mCureBuff;
    
    private Player? _player;

    ClassDescriptor mClassDescriptor;

    public Player? DisplayedPlayer => _player ?? Globals.Me;

    long HpRegenAmount;

    long ManaRegenAmount;

    int LifeStealAmount = 0;

    int ExtraExpAmount = 0;

    int LuckAmount = 0;

    int TenacityAmount = 0;

    int CooldownAmount = 0;

    int ManaStealAmount = 0;

    //Init
    public CharacterWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Character.Title, false, nameof(CharacterWindow))
    {
       
       SetSize(520, 340);

      IsResizable = false;

        // 📌 Nombre, Nivel y Clase
        mCharacterName = new Label(this, "CharacterNameLabel");
        mCharacterName.SetTextColor(Color.White, ComponentState.Normal);
        mCharacterName.SetPosition(16, 16);

        mCharacterLevelAndClass = new Label(this, "ChatacterInfoLabel");
        mCharacterLevelAndClass.SetPosition(16, 40);

        mFactionButton = new Button(this, "FactionInfoButton");
        mFactionButton.SetSize(32, 32);
        mFactionButton.SetPosition(470, 16);
        mFactionButton.SetStateTexture(ComponentState.Normal, "factionicon.png");
        mFactionButton.SetStateTexture(ComponentState.Hovered, "factionicon_hovered.png");
        mFactionButton.SetToolTipText("Faction");
        mFactionButton.Clicked += (s, e) => Interface.Interface.GameUi.GameMenu?.ToggleFactionWindow();

        // 🖼️ Contenedor y retrato
        mCharacterContainer = new ImagePanel(this, "CharacterContainer");
        mCharacterContainer.SetSize(96, 96);
        mCharacterContainer.SetPosition(16, 70);

        mCharacterPortrait = new ImagePanel(mCharacterContainer);
        mCharacterPortrait.SetSize(64, 64);
        mCharacterPortrait.SetPosition(16, 16);

        // 🧩 Slots visuales Paperdoll
        PaperdollPanels = new ImagePanel[Options.Instance.Equipment.Slots.Count + 1];
        PaperdollTextures = new string[Options.Instance.Equipment.Slots.Count + 1];
        for (var i = 0; i <= Options.Instance.Equipment.Slots.Count; i++)
        {
            PaperdollPanels[i] = new ImagePanel(mCharacterContainer);
            PaperdollTextures[i] = string.Empty;
            PaperdollPanels[i].Hide();
        }

        // 🧱 Equipamiento en grilla modular (4x4)
        var startX = 300;
        var startY = 40;
        var slotW = 32;
        var slotH = 32;
        var spacingX = 5;
        var spacingY = 5;
        var columns = 4;

        int itemIndex = 0;
        var multiSlotTracker = new Dictionary<string, int>();

        for (int slotIndex = 0; slotIndex < Options.Instance.Equipment.EquipmentSlots.Count; slotIndex++)
        {
            var slot = Options.Instance.Equipment.EquipmentSlots[slotIndex];

            for (int j = 0; j < slot.MaxItems; j++)
            {
                var item = new EquipmentItem(slotIndex, this);
                Items.Add(item);

                var slotName = slot.Name;

                if (slot.MaxItems <= 1)
                {
                    item.Pnl = new ImagePanel(this, slotName);
                }
                else
                {
                    if (!multiSlotTracker.ContainsKey(slotName))
                        multiSlotTracker[slotName] = 0;

                    var currentIndex = multiSlotTracker[slotName];
                    item.Pnl = new ImagePanel(this, $"{slotName}_{currentIndex}");
                    multiSlotTracker[slotName]++;
                }

                // 📐 Ubicación provisional
                int row = itemIndex / 4;
                int col = itemIndex % 4;

                item.Pnl.SetSize(slotW, slotH);
                item.Pnl.SetPosition(startX + col * (slotW + spacingX), startY + row * (slotH + spacingY));
                item.Setup();

                itemIndex++;
            }
        }


        // 📊 Etiquetas y botones de stats
        int statsX = 16;
        int statsY = 180;
        int statSpacing = 22;

        mAttackLabel = new Label(this, "AttackLabel");
        mAttackLabel.SetPosition(statsX, statsY);
        mAddAttackBtn = new Button(this, "IncreaseAttackButton");
        mAddAttackBtn.SetPosition(statsX + 130, statsY);
        mAddAttackBtn.Clicked += _addAttackBtn_Clicked;

        mDefenseLabel = new Label(this, "DefenseLabel");
        mDefenseLabel.SetPosition(statsX, statsY + statSpacing);
        mAddDefenseBtn = new Button(this, "IncreaseDefenseButton");
        mAddDefenseBtn.SetPosition(statsX + 130, statsY + statSpacing);
        mAddDefenseBtn.Clicked += _addDefenseBtn_Clicked;

        mSpeedLabel = new Label(this, "SpeedLabel");
        mSpeedLabel.SetPosition(statsX, statsY + statSpacing * 2);
        mAddAgilityBtn = new Button(this, "IncreaseSpeedButton");
        mAddAgilityBtn.SetPosition(statsX + 130, statsY + statSpacing * 2);
        mAddAgilityBtn.Clicked += _addSpeedBtn_Clicked;

        mAbilityPwrLabel = new Label(this, "AbilityPowerLabel");
        mAbilityPwrLabel.SetPosition(statsX, statsY + statSpacing * 3);
        mAddAbilityPwrBtn = new Button(this, "IncreaseAbilityPowerButton");
        mAddAbilityPwrBtn.SetPosition(statsX + 130, statsY + statSpacing * 3);
        mAddAbilityPwrBtn.Clicked += _addAbilityPwrBtn_Clicked;

        mMagicRstLabel = new Label(this, "MagicResistLabel");
        mMagicRstLabel.SetPosition(statsX, statsY + statSpacing * 4);
        mAddMagicResistBtn = new Button(    this, "IncreaseMagicResistButton");
        mAddMagicResistBtn.SetPosition(statsX + 130, statsY + statSpacing * 4);
        mAddMagicResistBtn.Clicked += _addMagicResistBtn_Clicked;

        mAgilityLabel = new Label(this, "AgilityLabel");
        mAgilityLabel.SetPosition(statsX, statsY + statSpacing * 5);

        mDamageLabel = new Label(this, "DamageLabel");
        mDamageLabel.SetPosition(statsX, statsY + statSpacing * 6);

        mCureLabel = new Label(this, "CureLabel");
        mCureLabel.SetPosition(statsX, statsY + statSpacing * 7);

        mPointsLabel = new Label(this, "PointsLabel");
        mPointsLabel.SetPosition(statsX, statsY + statSpacing * 8);

        var extraBuffsLabel = new Label(this, "ExtraBuffsLabel");
        extraBuffsLabel.SetText(Strings.Character.ExtraBuffs);
        extraBuffsLabel.SetPosition(statsX, statsY + statSpacing * 9);

        mHpRegen = new Label(this, "HpRegen");
        mHpRegen.SetPosition(statsX, statsY + statSpacing * 10);
        mManaRegen = new Label(this, "ManaRegen");
        mManaRegen.SetPosition(statsX, statsY + statSpacing * 11);
        mLifeSteal = new Label(this, "Lifesteal");
        mLifeSteal.SetPosition(statsX, statsY + statSpacing * 12);
        mAttackSpeed = new Label(this, "AttackSpeed");
        mAttackSpeed.SetPosition(statsX, statsY + statSpacing * 13);
        mSpeedBuff = new Label(this, "SpeedBuff");
        mSpeedBuff.SetPosition(statsX, statsY + statSpacing * 14);
        mDamageBuff = new Label(this, "DamageBuff");
        mDamageBuff.SetPosition(statsX, statsY + statSpacing * 15);
        mCureBuff = new Label(this, "CureBuff");
        mCureBuff.SetPosition(statsX, statsY + statSpacing * 16);
        mExtraExp = new Label(this, "ExtraExp");
        mExtraExp.SetPosition(statsX, statsY + statSpacing * 17);
        mLuck = new Label(this, "Luck");
        mLuck.SetPosition(statsX, statsY + statSpacing * 18);
        mTenacity = new Label(this, "Tenacity");
        mTenacity.SetPosition(statsX, statsY + statSpacing * 19);
        mCooldownReduction = new Label(this, "CooldownReduction");
        mCooldownReduction.SetPosition(statsX, statsY + statSpacing * 20);
        mManaSteal = new Label(this, "Manasteal");
        mManaSteal.SetPosition(statsX, statsY + statSpacing * 21);

        UpdateExtraBuffs();

       LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

    public void SetPlayer(Player? player)
    {
        _player = player;
    }

    //Update Button Event Handlers
    void _addMagicResistBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        PacketSender.SendUpgradeStat((int) Stat.Vitality);
    }

    void _addAbilityPwrBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        PacketSender.SendUpgradeStat((int) Stat.Intelligence);
    }

    void _addSpeedBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        PacketSender.SendUpgradeStat((int) Stat.Agility);
    }

    void _addDefenseBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        PacketSender.SendUpgradeStat((int) Stat.Defense);
    }

    void _addAttackBtn_Clicked(Base sender, MouseButtonState arguments)
    {
        PacketSender.SendUpgradeStat((int) Stat.Attack);
    }

    //Methods
    public void Update()
    {
        var player = DisplayedPlayer;
        if (IsHidden || player is null)
        {
            return;
        }

        mCharacterName.Text = player.Name;
        mCharacterLevelAndClass.Text = Strings.Character.LevelAndClass.ToString(
            player.Level, ClassDescriptor.GetName(player.Class)
        );

        //Load Portrait
        //UNCOMMENT THIS LINE IF YOU'D RATHER HAVE A FACE HERE IGameTexture faceTex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Face, player.Face);
        var entityTex = Globals.ContentManager.GetTexture(
            Framework.Content.TextureType.Entity, player.Sprite
        );

        /* UNCOMMENT THIS BLOCK IF YOU"D RATHER HAVE A FACE HERE if (player.Face != "" && player.Face != _currentSprite && faceTex != null)
         {
             _characterPortrait.Texture = faceTex;
             _characterPortrait.SetTextureRect(0, 0, faceTex.GetWidth(), faceTex.GetHeight());
             _characterPortrait.SizeToContents();
             Align.Center(_characterPortrait);
             _characterPortrait.IsHidden = false;
             for (int i = 0; i < Options.Instance.Equipment.Slots.Count; i++)
             {
                 _paperdollPanels[i].Hide();
             }
         }
         else */
        if (!string.IsNullOrWhiteSpace(player.Sprite) && player.Sprite != mCurrentSprite && entityTex != null)
        {
            for (var z = 0; z < Options.Instance.Equipment.Paperdoll.Directions[1].Count; z++)
            {
                var paperdoll = string.Empty;
                var slotName = Options.Instance.Equipment.Paperdoll.Directions[1][z];
                var slotIndex = Options.Instance.Equipment.Slots.IndexOf(slotName);

                if (slotIndex > -1)
                {
                    var equipment = player.MyEquipment;

                    // Intentar obtener la lista de ítems equipados en este slot
                    if (equipment.TryGetValue(slotIndex, out var equippedList) && equippedList.Count > 0)
                    {
                        var inventoryIndex = equippedList[0]; // Tomamos el primero para mostrar
                        if (inventoryIndex >= 0 && inventoryIndex < Options.Instance.Player.MaxInventory)
                        {
                            var itemNum = player.Inventory[inventoryIndex].ItemId;

                            if (ItemDescriptor.TryGet(itemNum, out var itemDescriptor))
                            {
                                paperdoll = player.Gender == 0
                                    ? itemDescriptor.MalePaperdoll
                                    : itemDescriptor.FemalePaperdoll;

                                PaperdollPanels[z].RenderColor = itemDescriptor.Color;
                            }
                        }
                    }
                }
                else if (slotName == "Player")
                {
                    PaperdollPanels[z].Show();
                    PaperdollPanels[z].Texture = entityTex;
                    PaperdollPanels[z].SetTextureRect(0, 0, entityTex.Width / Options.Instance.Sprites.NormalFrames, entityTex.Height / Options.Instance.Sprites.Directions);
                    PaperdollPanels[z].SizeToContents();
                    PaperdollPanels[z].RenderColor = player.Color;
                    Align.Center(PaperdollPanels[z]);
                }

                if (string.IsNullOrWhiteSpace(paperdoll) && !string.IsNullOrWhiteSpace(PaperdollTextures[z]) && slotName != "Player")
                {
                    PaperdollPanels[z].Texture = null;
                    PaperdollPanels[z].Hide();
                    PaperdollTextures[z] = string.Empty;
                }
                else if (!string.IsNullOrWhiteSpace(paperdoll) && paperdoll != PaperdollTextures[z])
                {
                    var paperdollTex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Paperdoll, paperdoll);

                    PaperdollPanels[z].Texture = paperdollTex;
                    if (paperdollTex != null)
                    {
                        PaperdollPanels[z].SetTextureRect(0, 0, paperdollTex.Width / Options.Instance.Sprites.NormalFrames, paperdollTex.Height / Options.Instance.Sprites.Directions);
                        PaperdollPanels[z].SetSize(paperdollTex.Width / Options.Instance.Sprites.NormalFrames, paperdollTex.Height / Options.Instance.Sprites.Directions);
                        PaperdollPanels[z].SetPosition(mCharacterContainer.Width / 2 - PaperdollPanels[z].Width / 2, mCharacterContainer.Height / 2 - PaperdollPanels[z].Height / 2);
                    }

                    PaperdollPanels[z].Show();
                    PaperdollTextures[z] = paperdoll;
                }
            }
        }

        else if (player.Sprite != mCurrentSprite && player.Face != mCurrentSprite)
        {
            mCharacterPortrait.IsHidden = true;
            for (var i = 0; i < Options.Instance.Equipment.Slots.Count; i++)
            {
                PaperdollPanels[i].Hide();
            }
        }

        mAttackLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Attack],
                player.Stat[(int)Stat.Attack]
            )
        );

        mAbilityPwrLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Intelligence],
                player.Stat[(int)Stat.Intelligence]
            )
        );

        mDefenseLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Defense],
                player.Stat[(int)Stat.Defense]
            )
        );

        mMagicRstLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Vitality],
                player.Stat[(int)Stat.Vitality]
            )
        );

        mSpeedLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Speed],
                player.Stat[(int)Stat.Speed]
            )
        );
        mAgilityLabel.SetText(
    Strings.Character.StatLabelValue.ToString(
        Strings.Combat.Stats[Stat.Agility],
        player.Stat[(int)Stat.Agility]
    )
);

        mDamageLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Damages],
                player.Stat[(int)Stat.Damages]
            )
        );

        mCureLabel.SetText(
            Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Cures],
                player.Stat[(int)Stat.Cures]
            )
        );
        mPointsLabel.SetText(Strings.Character.Points.ToString(player.StatPoints));
        mAddAbilityPwrBtn.IsHidden = player.StatPoints == 0 ||
                                     player.Stat[(int) Stat.Intelligence] == Options.Instance.Player.MaxStat;

        mAddAttackBtn.IsHidden =
            player.StatPoints == 0 || player.Stat[(int) Stat.Attack] == Options.Instance.Player.MaxStat;

        mAddDefenseBtn.IsHidden = player.StatPoints == 0 ||
                                  player.Stat[(int) Stat.Defense] == Options.Instance.Player.MaxStat;

        mAddMagicResistBtn.IsHidden = player.StatPoints == 0 ||
                                      player.Stat[(int) Stat.Vitality] == Options.Instance.Player.MaxStat;

        mAddAgilityBtn.IsHidden =
            player.StatPoints == 0 || player.Stat[(int) Stat.Agility] == Options.Instance.Player.MaxStat;

        UpdateExtraBuffs();
        UpdateEquippedItems(true);
    }

    private void UpdateEquippedItems(bool updateExtraBuffs = false)
    {
        var player = DisplayedPlayer;
        if (player is null)
        {
            return;
        }

        int itemIndex = 0;
        for (var slotIndex = 0; slotIndex < Options.Instance.Equipment.EquipmentSlots.Count; slotIndex++)
        {
            var slot = Options.Instance.Equipment.EquipmentSlots[slotIndex];

            if (player == Globals.Me)
            {
                var itemSlots = player.MyEquipment.GetValueOrDefault(slotIndex) ?? new List<int>();
                for (var i = 0; i < slot.MaxItems; i++)
                {
                    if (itemIndex >= Items.Count)
                        break;

                    var itemIds = new List<Guid>();
                    var props = new List<ItemProperties>();

                    if (i < itemSlots.Count && itemSlots[i] >= 0 && itemSlots[i] < Options.Instance.Player.MaxInventory)
                    {
                        var invItem = player.Inventory[itemSlots[i]];
                        if (invItem.ItemId != Guid.Empty)
                        {
                            itemIds.Add(invItem.ItemId);
                            props.Add(invItem.ItemProperties);
                            if (updateExtraBuffs)
                            {
                                UpdateExtraBuffs(invItem.ItemId);
                            }
                        }
                    }

                    Items[itemIndex].Update(itemIds, props);
                    itemIndex++;
                }
            }
            else
            {
                var equippedIds = player.Equipment.GetValueOrDefault(slotIndex) ?? new List<Guid>();
                for (var i = 0; i < slot.MaxItems; i++)
                {
                    if (itemIndex >= Items.Count)
                        break;

                    var itemIds = new List<Guid>();
                    if (i < equippedIds.Count && equippedIds[i] != Guid.Empty)
                    {
                        itemIds.Add(equippedIds[i]);
                        if (updateExtraBuffs)
                        {
                            UpdateExtraBuffs(equippedIds[i]);
                        }
                    }

                    Items[itemIndex].Update(itemIds, new List<ItemProperties>());
                    itemIndex++;
                }
            }
        }
    }

    public void UpdateExtraBuffs()
    {
        var player = DisplayedPlayer;
        mClassDescriptor = ClassDescriptor.Get(player?.Class ?? Guid.Empty);

        if (mClassDescriptor != null)
        {
            HpRegenAmount = mClassDescriptor.VitalRegen[(int)Vital.Health];
            mHpRegen.SetText(Strings.Character.HealthRegen.ToString(HpRegenAmount));
            ManaRegenAmount = mClassDescriptor.VitalRegen[(int)Vital.Mana];
            mManaRegen.SetText(Strings.Character.ManaRegen.ToString(ManaRegenAmount));
        }

        CooldownAmount = 0;
        LifeStealAmount = 0;
        TenacityAmount = 0;
        LuckAmount = 0;
        ExtraExpAmount = 0;
        ManaStealAmount = 0;

        mLifeSteal.SetText(Strings.Character.Lifesteal.ToString(0));
        mExtraExp.SetText(Strings.Character.ExtraExp.ToString(0));
        mLuck.SetText(Strings.Character.Luck.ToString(0));
        mTenacity.SetText(Strings.Character.Tenacity.ToString(0));
        mCooldownReduction.SetText(Strings.Character.CooldownReduction.ToString(0));
        mManaSteal.SetText(Strings.Character.Manasteal.ToString(0));

        if (player != null)
        {
            mAttackSpeed.SetText(Strings.Character.AttackSpeed.ToString(player.CalculateAttackTime() / 1000f));

            mSpeedBuff.SetText(Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Speed],
                player.Stat[(int)Stat.Speed]
            ));
            mDamageBuff.SetText(Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Damages],
                player.Stat[(int)Stat.Damages]
            ));
            mCureBuff.SetText(Strings.Character.StatLabelValue.ToString(
                Strings.Combat.Stats[Stat.Cures],
                player.Stat[(int)Stat.Cures]
            ));
        }
    }

    /// <summary>
    /// Update Extra Buffs Effects like hp/mana regen and items effect types
    /// </summary>
    /// <param name="itemId">Id of item to update extra buffs</param>
    private void UpdateExtraBuffs(Guid itemId)
    {
        var item = ItemDescriptor.Get(itemId);

        if (item == null)
        {
            return;
        }

        //Getting HP and Mana Regen from items
        if (item.VitalsRegen[(int)Vital.Health] != 0)
        {
            HpRegenAmount += item.VitalsRegen[(int)Vital.Health];
        }

        if (item.VitalsRegen[(int)Vital.Mana] != 0)
        {
            ManaRegenAmount += item.VitalsRegen[(int)Vital.Mana];
        }

        //Getting extra buffs from items
        if (item.Effects.Find(effect => effect.Type != ItemEffect.None && effect.Percentage > 0) != default)
        {
            foreach (var effect in item.Effects)
            {
                if (effect.Percentage <= 0)
                {
                    continue;
                }

                switch (effect.Type)
                {
                    case ItemEffect.CooldownReduction:
                        CooldownAmount += effect.Percentage;
                        break;
                    case ItemEffect.Lifesteal:
                        LifeStealAmount += effect.Percentage;
                        break;
                    case ItemEffect.Tenacity:
                        TenacityAmount += effect.Percentage;
                        break;
                    case ItemEffect.Luck:
                        LuckAmount += effect.Percentage;
                        break;
                    case ItemEffect.EXP:
                        ExtraExpAmount += effect.Percentage;
                        break;
                    case ItemEffect.Manasteal:
                        ManaStealAmount += effect.Percentage;
                        break;
                }
            }
        }
    }


    /// <summary>
    /// Show the window
    /// </summary>
    public void Show()
    {
        this.IsHidden = false;
    }

    /// <summary>
    /// </summary>
    /// <returns>Returns if window is visible</returns>
    public bool IsVisible()
    {
        return !this.IsHidden;
    }

    /// <summary>
    /// Hide the window
    /// </summary>
    public void Hide()
    {
        this.IsHidden = true;
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }

}
