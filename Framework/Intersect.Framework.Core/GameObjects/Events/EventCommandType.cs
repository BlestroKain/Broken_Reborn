namespace Intersect.Framework.Core.GameObjects.Events;

public enum EventCommandType
{
    Null = 0,

    //Dialog
    ShowText,

    ShowOptions,

    AddChatboxText,

    //Logic Flow
    SetVariable = 5,

    SetSelfSwitch,

    ConditionalBranch,

    ExitEventProcess,

    Label,

    GoToLabel,

    StartCommonEvent,

    //Player Control
    RestoreHp,

    RestoreMp,

    LevelUp,

    GiveExperience,

    ChangeLevel,

    ChangeSpells,

    ChangeItems,

    ChangeSprite,

    ChangeFace,

    ChangeGender = 21,

    SetAlignment = 60,

    SetAccess = 22,

    //Movement,
    WarpPlayer,

    SetMoveRoute,

    WaitForRouteCompletion,

    HoldPlayer,

    ReleasePlayer,

    SpawnNpc,

    //Special Effects
    PlayAnimation,

    PlayBgm,

    FadeoutBgm,

    PlaySound,

    StopSounds,

    //Etc
    Wait,

    //Shop and Bank
    OpenBank,

    OpenShop,

    OpenCraftingTable,

    //Extras
    SetClass,

    DespawnNpc,

    //Questing
    StartQuest,

    CompleteQuestTask,

    EndQuest,

    //Pictures
    ShowPicture,

    HidePicture,

    //Hide/show player
    HidePlayer,

    ShowPlayer,

    //Equip Items
    EquipItem,

    //Change Name Color
    ChangeNameColor,

    //Player Input variable.
    InputVariable,

    //Player Label
    PlayerLabel,

    // Player Color
    ChangePlayerColor,

    ChangeName,

    //Guilds
    CreateGuild,
    DisbandGuild,
    OpenGuildBank,
    SetGuildBankSlots,

    //End Guilds

    //Reset Stats
    ResetStatPointAllocations,

    CastSpellOn,

    Fade,
    GiveJobExperience=200,
    OpenEnchantment = 201,
    OpenMage = 202,
    OpenBrokeItem = 203,
    OpenMailBox = 204,
    SendMail = 205,
    ChangeBestiary = 206,
    OpenMarket = 207,
    OpenSellMarket = 208,
}
