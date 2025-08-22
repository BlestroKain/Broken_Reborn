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
using Intersect.Network.Packets.Server;
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

    public static void SendMail(string to, string title, string message, List<Intersect.Network.Packets.Server.MailAttachmentPacket> attachments)
    {
        // Validación básica
        if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(title) || attachments == null)
        {
            return;
        }

        // Enviar el paquete con los adjuntos
        Network.SendPacket(new MailBoxSendPacket(to, title, message, attachments));
    }

    public static void SendCloseMail()
    {
        // Enviar paquete para cerrar el buzón
        Network.SendPacket(new MailBoxClosePacket());
    }

    public static void SendTakeMail(Guid mailID)
    {
        // Validación básica
        if (mailID == Guid.Empty)
        {
            return;
        }

        // Enviar paquete para tomar un correo
        Network.SendPacket(new TakeMailPacket(mailID));
    }

    public static void SendSpellPropertiesChange(int slotIndex, int delta)
    {
        var packet = new SpellPropertiesChangePacket
        {
            SpellSlot = slotIndex,
            Delta = delta
        };

        Network.SendPacket(packet);
    }

}
