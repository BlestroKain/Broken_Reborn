using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Entities.Events;
using Intersect.Client.Entities.Projectiles;
using Intersect.Client.Framework.Entities;
using Intersect.Client.Framework.Items;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Market;
using Intersect.Client.Interface.Menu;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Maps;
using Intersect.Configuration;
using Intersect.Core;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network;
using Intersect.Network.Packets;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using Intersect.Framework;
using Intersect.Models;
using Intersect.Client.Interface.Shared;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Animations;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Crafting;
using Intersect.Framework.Core.GameObjects.Events;
using Intersect.Framework.Core.GameObjects.Mapping.Tilesets;
using Intersect.Framework.Core.GameObjects.Maps;
using Intersect.Framework.Core.GameObjects.Maps.Attributes;
using Intersect.Framework.Core.GameObjects.Maps.MapList;
using Intersect.Framework.Core.Security;
using Intersect.Localization;
using Microsoft.Extensions.Logging;
using Intersect.Config;
using Intersect.Network.Packets.Client;
using Intersect.Framework.Core.GameObjects.Guild;
using Serilog;
using Intersect.Client.Controllers;

namespace Intersect.Client.Networking;


internal sealed partial class PacketHandler
{
    public void HandlePacket(IPacketSender packetSender, JobSyncPacket packet)
    {
        if (Globals.Me == null)
        {
            PacketSender.SendChatMsg("Error: El jugador no está inicializado.", 5);
            return;
        }

        if (packet.Jobs == null || packet.Jobs.Count == 0)
        {
            PacketSender.SendChatMsg("Error: El paquete de trabajos está vacío o no contiene datos.", 5);
            return;
        }

        // Actualizar los datos del jugador
        Globals.Me.UpdateJobsFromPacket(packet.Jobs);

        foreach (var job in packet.Jobs)
        {
            if (job.Key == JobType.None || job.Key == JobType.JobCount)
            {
                PacketSender.SendChatMsg($"Advertencia: Trabajo inválido '{job.Key}' ignorado.", 5);
                continue;
            }

            var jobData = job.Value;
            //PacketSender.SendChatMsg($"[DEBUG] Trabajo {job.Key}: Nivel {jobData.Level}, Exp {jobData.Experience}/{jobData.ExperienceToNextLevel}", 5);
        }
    }

    public void HandlePacket(IPacketSender packetSender, GuildCreationWindowPacket packet)
    {
        Interface.Interface.EnqueueInGame(gameInterface => gameInterface.NotifyOpenGuildCreation());
    }

    public void HandlePacket(IPacketSender packetSender, GuildExpPercentagePacket packet)
    {
        if (Globals.Me == null) return;

        // Establecemos el nuevo porcentaje de donación a la guild
        Globals.Me.GuildXpContribution = Math.Clamp(packet.Percentage, 0f, 100f);

        // Mostramos un mensaje al jugador
        ChatboxMsg.AddMessage(new ChatboxMsg($"Ahora donas {Globals.Me.GuildXpContribution}% de tu XP a la guild.", Color.ForestGreen, ChatMessageType.Notice));

        // Enviamos la actualización al servidor
        PacketSender.SendUpdateGuildXpContribution(Globals.Me.GuildXpContribution);
    }
    public void HandlePacket(IPacketSender packetSender, GuildUpdate packet)
    {
        if (Globals.Me == null || Globals.Me.Guild == null)
        {
            return;
        }

        // Nombre del gremio
        Globals.Me.Guild = packet.Name;

        // Logo visual
        Globals.Me.GetLogo(
            packet.LogoBackground,
            packet.BackgroundR,
            packet.BackgroundG,
            packet.BackgroundB,
            packet.LogoSymbol,
            packet.SymbolR,
            packet.SymbolG,
            packet.SymbolB
        );

        Globals.Me.GuildBackgroundFile = packet.LogoBackground;
        Globals.Me.GuildSymbolFile = packet.LogoSymbol;
        Globals.Me.GuildBackgroundB = packet.BackgroundB;
        Globals.Me.GuildBackgroundR = packet.BackgroundR;
        Globals.Me.GuildBackgroundG = packet.BackgroundG;

        // Datos de progreso
        Guild.GuildLevel = packet.GuildLevel;
        Guild.GuildExp = packet.GuildExp;
        Guild.GuildExpToNextLevel = packet.GuildExpToNextLevel;

        // Puntos del gremio
        Guild.GuildPoints = packet.GuildPoints;
        Guild.GuildSpent = packet.SpentGuildPoints;

        // Cargar mejoras desde el diccionario
        Guild.GuildUpgrades.Clear();
        foreach (var kvp in packet.GuildUpgrades)
        {
            if (Enum.TryParse(kvp.Key, out GuildUpgradeType upgradeType))
            {
                Guild.GuildUpgrades[upgradeType] = kvp.Value;
            }
        }

        // Notificar a la interfaz (si usas bindings o UI dinámica)
        // Interface.Interface.GameUi.NotifyUpdateGuild();
    }

    public void HandlePacket(IPacketSender packetSender, GuildExperienceUpdatePacket packet)
    {
        if (Globals.Me == null) return;

        // Establecemos el nuevo porcentaje de donación a la guild
        Globals.Me.GuildXpContribution = Math.Clamp(packet.Experience, 0f, 100f);

        // Mostramos un mensaje al jugador
        ChatboxMsg.AddMessage(new ChatboxMsg($"Ahora donas {Globals.Me.GuildXpContribution}% de tu XP a la guild.", Color.ForestGreen, ChatMessageType.Notice));


    }
    public void HandlePacket(IPacketSender packetSender, UpdateItemLevelPacket packet)
    {
        // Validar el índice del ítem
        if (packet.ItemIndex < 0 || packet.ItemIndex >= Globals.Me.Inventory.Length)
        {
            Log.Error($"Índice de ítem inválido: {packet.ItemIndex}.");
            return;
        }

        // Obtener el ítem del inventario utilizando el índice
        var inventoryItem = Globals.Me.Inventory[packet.ItemIndex];

        if (inventoryItem != null)
        {
            // Actualizar nivel de encantamiento
            inventoryItem.ItemProperties.EnchantmentLevel = packet.NewEnchantmentLevel;
            Interface.Interface.GameUi?.mEnchantItemWindow.UpdateProjection();

        }
        else
        {
            Log.Error($"No se encontró ningún ítem en el índice {packet.ItemIndex}.");
        }
    }
    public void HandlePacket(IPacketSender packetSender, EnchantmentWindowPacket packet)
    {
        Interface.Interface.GameUi?.OpenEnchantWindow();
    }
    public void HandlePacket(IPacketSender packetSender, MageWindowPacket packet)
    {
        Interface.Interface.GameUi?.OpenRuneItemWindow();
    }
    public void HandlePacket(IPacketSender packetSender, BrokeItemWindowPacket packet)
    {
        Interface.Interface.GameUi?.OpenBrokeItemWindow();
    }

    // Mail
    public void HandlePacket(IPacketSender packetSender, MailBoxsUpdatePacket packet)
    {
        Globals.Mails.Clear();
        foreach (MailBoxUpdatePacket mail in packet.Mails)
        {
            var attachments = new List<MailAttachment>();
            if (mail.Attachments != null)
            {
                foreach (var attachment in mail.Attachments)
                {
                    attachments.Add(new MailAttachment
                    {
                        ItemId = attachment.ItemId,
                        Quantity = attachment.Quantity,
                        Properties = attachment.Properties
                    });
                }
            }
            Globals.Mails.Add(new Mail(mail.MailID, mail.Name, mail.Message, mail.SenderName, attachments));
        }
    }

    public void HandlePacket(IPacketSender packetSender, MailBoxPacket packet)
    {
        if (!packet.Close)
        {
            Interface.Interface.GameUi.CloseSendMailBox();
            Interface.Interface.GameUi.CloseMailBox();
        }
        else
        {
            if (packet.Send)
            {
                Interface.Interface.GameUi.OpenSendMailBox();
            }
            else
            {
                Interface.Interface.GameUi.OpenMailBox();
            }
        }
    }

    //UnlockedBestiaryEntriesPacket
    public void HandlePacket(IPacketSender packetSender, UnlockedBestiaryEntriesPacket packet)
    {
        BestiaryController.ApplyPacket(packet);
    }

    public void HandlePacket(IPacketSender packetSender, MarketListingCreatedPacket packet)
    {
        // Placeholder for handling market listing creation confirmation
    }

    public void HandlePacket(IPacketSender packetSender, MarketListingPacket packet)
    {
        // Placeholder for handling a single market listing
        var _ = packet.Properties;
    }

    public void HandlePacket(IPacketSender packetSender, MarketListingsPacket packet)
    {
        Interface.Interface.EnqueueInGame(gameInterface =>
        {
            gameInterface.GetMarketWindow()?.LoadListings(packet.Listings, packet.Page, packet.PageSize, packet.Total);
        });
    }

    public void HandlePacket(IPacketSender packetSender, MarketPurchaseSuccessPacket packet)
    {
        // Placeholder for handling successful market purchases
    }

    public void HandlePacket(IPacketSender packetSender, MarketTransactionsPacket packet)
    {
        // Placeholder for handling market transaction history
    }

    public void HandlePacket(IPacketSender packetSender, MarketPriceInfoPacket packet)
    {
        var descriptorId = ItemDescriptor.IdFromList(packet.ItemId);
        MarketPriceCache.Update(descriptorId, (int)packet.SuggestedPrice, (int)packet.MinPrice, (int)packet.MaxPrice);
    }

    public void HandlePacket(IPacketSender packetSender, MarketWindowPacket packet)
    {
        Interface.Interface.EnqueueInGame(gameInterface =>
        {
            switch (packet.Action)
            {
                case MarketWindowPacketType.OpenMarket:
                    gameInterface.NotifyOpenMarket();
                    break;
                case MarketWindowPacketType.CloseMarket:
                    gameInterface.NotifyCloseMarket();
                    break;
                case MarketWindowPacketType.OpenSell:
                    gameInterface.NotifyOpenSellMarket(packet.Slot);
                    break;
                case MarketWindowPacketType.CloseSell:
                    gameInterface.NotifyCloseSellMarket();
                    break;
            }
        });
    }

}
