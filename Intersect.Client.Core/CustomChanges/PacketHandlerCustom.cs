using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Entities.Events;
using Intersect.Client.Entities.Projectiles;
using Intersect.Client.Framework.Entities;
using Intersect.Client.Framework.Items;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
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

  
}
