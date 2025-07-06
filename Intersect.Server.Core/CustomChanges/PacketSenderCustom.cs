using System.Collections.Concurrent;
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
}
