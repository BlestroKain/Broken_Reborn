using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Control.EventArguments.InputSubmissionEvent;
using Intersect.Client.General;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Maps;
using Intersect.Enums;
using Intersect.Framework;
using Intersect.Framework.Core.GameObjects.Guild;
using Intersect.Framework.Core.GameObjects.Maps;
using Intersect.Models;
using Intersect.Network.Packets.Client;
using AdminAction = Intersect.Admin.Actions.AdminAction;

namespace Intersect.Client.Networking;


public static partial class PacketSender
{

    public static void SendCreateGuild(string name, string logoBackground, byte backgroundR, byte backgroundG, byte backgroundB,
        string logoSymbol, byte symbolR, byte symbolG, byte symbolB)
    {
        Network.SendPacket(new CreateGuildPacket(name, logoBackground, backgroundR, backgroundG, backgroundB,
            logoSymbol, symbolR, symbolG, symbolB));
    }
    public static void SendUpdatateGuildWindow(string backgroundFile, byte backgroundR, byte backgroundG, byte backgroundB,
        string symbolFile, byte symbolR, byte symbolG, byte symbolB)
    {
        Network.SendPacket(new RequestGuildPacket(backgroundFile, backgroundR, backgroundG, backgroundB,
            symbolFile, symbolR, symbolG, symbolB));
        SendChatMsg("Guild window updated", 5);
    }
    public static void SendUpdateGuildXpContribution(float contribution)
    {
        Network.SendPacket(new GuildExpPercentagePacket(contribution));
    }
 
    public static void SendApplyGuildUpgrade(GuildUpgradeType type)
    {
       Network.SendPacket(new ApplyGuildUpgradePacket(type));
    }
    public static void SendEnchantItem(int itemId, int targetLevel, Guid currencyId, int currencyAmount, bool useAmulet)
    {
        Network.SendPacket(new EnchantItemPacket(itemId, targetLevel, currencyId, currencyAmount, useAmulet));
    }
    public static void SendUpgradeStat(int itemSlot, int orbSlot)
    {
        Network.SendPacket(new UpgradeItemStatPacket(itemSlot, orbSlot));
    }
    public static void SendBreakItem(int itemSlot)
    {
        var packet = new BrokeItemPacket(itemSlot);
        Network.SendPacket(packet);
    }
}
