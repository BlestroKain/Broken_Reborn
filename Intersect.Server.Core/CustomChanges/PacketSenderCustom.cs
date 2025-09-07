using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Intersect.Config;
using Intersect.Core;
using Intersect.Enums;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Animations;
using Intersect.Framework.Core.GameObjects.Crafting;
using Intersect.Framework.Core.GameObjects.Events;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Mapping.Tilesets;
using Intersect.Framework.Core.GameObjects.Maps.MapList;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Framework.Core.GameObjects.PlayerClass;
using Intersect.Framework.Core.GameObjects.Resources;
using Intersect.Framework.Core.GameObjects.Variables;
using Intersect.Framework.Core.Network.Packets.Security;
using Intersect.Framework.Core.Security;
using Intersect.GameObjects;
using Intersect.Models;
using Intersect.Network;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.Logging.Entities;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Intersect.Server.Networking;


public static partial class PacketSender
{

    public static void SendJobSync(Player player)
    {
        if (player == null || player.Jobs == null || player.Jobs.Count == 0)
        {
            player.InitializeJobs();
            return;
        }

        var jobData = new Dictionary<JobType, JobData>();
        // Inicializar trabajos si no están presentes

        foreach (var job in player.Jobs)
        {
            var jobType = job.Key;
            var jobDetails = job.Value;

            // Crear un JobData con los datos actuales del trabajo
            jobData[jobType] = new JobData
            {
                Level = jobDetails.JobLevel,
                Experience = jobDetails.JobExp,
                ExperienceToNextLevel = jobDetails.GetExperienceToNextLevel(jobDetails.JobLevel)
            };
        }

        // Crear y enviar el paquete
        var packet = new JobSyncPacket(jobData);
        player.SendPacket(packet, TransmissionMode.Any);

        // Depuración en el servidor
        //  PacketSender.SendChatMsg(player,$"[DEBUG] Paquete de trabajos enviado a {player.Name} con {jobData.Count} trabajos.",ChatMessageType.Notice);
    }
    public static void SendOpenGuildWindow(Player player)
    {
        if (player == null) return;
        player.SendPacket(new GuildCreationWindowPacket());
    }
    public static void UpdateGuild(Player player)
    {
        if (player == null || player.Guild == null)
        {
            return;
        }
        var guild = player.Guild;
        var guildUpdatePacket = new GuildUpdate
        (
               guild.Name,
    guild.LogoBackground,
    guild.BackgroundR, guild.BackgroundG, guild.BackgroundB,
    guild.LogoSymbol,
    guild.SymbolR, guild.SymbolG, guild.SymbolB,
    guild.Level,
    guild.Experience,
    guild.ExperienceToNextLevel,
    guild.GuildPoints,
    guild.SpentGuildPoints,
    guild.GuildUpgrades.ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value)


        );
        player.SendPacket(guildUpdatePacket);
    }

    public static void UpdateExpPercent(Player player)
    {
        if (player == null) return;

        player.SendPacket(new GuildExperienceUpdatePacket(player.GuildExpPercentage));
    }
    public static void SendUpdateItemLevel(Player player, int itemIndex, int newEnchantmentLevel)
    {
        if (player == null)
        {
            return;
        }

        // Validar el índice del ítem
        if (itemIndex < 0 || itemIndex >= player.Items.Count)
        {
            return; // Índice no válido
        }

        // Obtener el ítem en el índice especificado
        var item = player.Items[itemIndex];
        if (item == null)
        {
            return; // El ítem no existe en este índice
        }

        // Actualizar el nivel de encantamiento del ítem
        item.Properties.EnchantmentLevel = newEnchantmentLevel;

        // Crear el paquete con los datos necesarios
        var packet = new UpdateItemLevelPacket(itemIndex, newEnchantmentLevel);

        // Enviar el paquete al cliente
        player.SendPacket(packet, TransmissionMode.All);
    }

    public static void SendOpenEnchantmentWindow(Player player)
    {
        // Abre la ventana para vender ítems
        player.SendPacket(new EnchantmentWindowPacket());
    }
    public static void SendOpenMageWindow(Player player)
    {
        // Abre la ventana para vender ítems
        player.SendPacket(new MageWindowPacket());
    }
    public static void SendOpenBrokeItemWindow(Player player)
    {
        // Abre la ventana para vender ítems
        player.SendPacket(new BrokeItemWindowPacket());
    }

    public static void SendOpenMailBox(Player player)
    {
        var mails = new List<MailBoxUpdatePacket>();

        foreach (var mail in player.MailBoxs)
        {
            var attachments = mail.Attachments.Select(a => new MailAttachmentPacket
            {
                ItemId = a.ItemId,
                Quantity = a.Quantity,
                Properties = a.Properties
            }).ToList();

            // Corregido: usar remitente
            mails.Add(new MailBoxUpdatePacket(
                mail.Id,
                mail.Title,
                mail.Message,
                mail.SenderPlayer?.Name ?? "Desconocido",
                attachments
            ));
        }

        player.SendPacket(new MailBoxsUpdatePacket(mails.ToArray()));
        player.SendPacket(new MailBoxPacket(true, false));
    }


    public static void SendCloseMailBox(Player player)
    {
        // Enviar paquete para cerrar la bandeja de entrada
        player.SendPacket(new MailBoxPacket(false, false));
    }

    public static void SendOpenSendMail(Player player)
    {
        // Enviar inventario del jugador al cliente
        SendInventory(player);

        // Enviar paquete para abrir la ventana de envío de correos

        player.SendPacket(new MailBoxPacket(true, true));
    }
    //UnlockedBestiaryEntriesPacket
    public static void SendUnlockedBestiaryEntries(Player player)
    {
        var unlocks = player.BestiaryUnlocks
            .Where(b => b.Value > 0) // Ya no se excluye Kill
            .GroupBy(b => b.NpcId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(b => (int)b.UnlockType).Distinct().ToArray()
            );

        var killCounts = player.BestiaryUnlocks
            .Where(b => b.UnlockType == BestiaryUnlock.Kill)
            .ToDictionary(b => b.NpcId, b => b.Value);

        player.SendPacket(new UnlockedBestiaryEntriesPacket(unlocks, killCounts));
    }

    public static void SendMarketListingCreated(Player player, Guid listingId)
    {
        player.SendPacket(new MarketListingCreatedPacket(listingId));
    }

    public static void SendMarketListings(Player player, List<MarketListingPacket> listings)
    {
        player.SendPacket(new MarketListingsPacket(listings));
    }

    public static void SendMarketPurchaseSuccess(Player player, Guid listingId)
    {
        player.SendPacket(new MarketPurchaseSuccessPacket(listingId));
    }

    public static void SendMarketTransactions(Player player, List<MarketTransactionInfo> transactions)
    {
        player.SendPacket(new MarketTransactionsPacket(transactions));
    }

    public static void SendMarketPriceInfo(Player player, int itemId, long suggestedPrice, long minPrice, long maxPrice)
    {
        player.SendPacket(new MarketPriceInfoPacket(itemId, suggestedPrice, minPrice, maxPrice));
    }


}
