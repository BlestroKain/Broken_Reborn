using System;
using System.Collections.Generic;
using Intersect.Server.Networking;
using Intersect.Enums;
using Intersect.Server.Localization;
using Intersect.Config;
using System.Text.Json.Serialization;
using Intersect.Server.Database.PlayerData.Players;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Network.Packets.Server;

namespace Intersect.Server.Entities
{
    public partial class Player : Entity
    {

        public float GuildExpPercentage { get; set; } = 10f;

        public long DonateXPGuild { get; set; } = 0;

        public void SetGuildExpPercentage(float percentage)
        {
            GuildExpPercentage = Math.Clamp(percentage, 0.0f, 100.0f);
            PacketSender.SendChatMsg(this, $"Has cambiado tu contribución de experiencia al gremio a {GuildExpPercentage}%.", ChatMessageType.Guild);

            // ACTUALIZAR EL VALOR EN Guild.Members (si existe)
            if (Guild != null && Guild.Members.TryGetValue(this.Id, out var member))
            {
                member.ExperiencePerc = GuildExpPercentage;
            }

            // Notificar a todos los miembros del gremio
            Guild?.UpdateMemberList();
        }

        private void DonateGuildExperience(long amount)
        {
            if (Guild == null || amount <= 0) return;

            Guild.AddExperience(amount);
            DonateXPGuild += amount;
            PacketSender.SendChatMsg(this, $"Your guild has received {amount} XP.", ChatMessageType.Guild);

            // Obtener el miembro del gremio correspondiente
            if (Guild.Members.TryGetValue(this.Id, out var member))
            {
                member.DonatedXp = DonateXPGuild;
            }
            Guild?.UpdateMemberList();
        }

    }

}