using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class MailBox : IPlayerOwned
    {
        public MailBox() { }

        public MailBox(Player sender, Player receiver, string title, string message, List<MailAttachment> attachments)
        {
            SenderPlayer = sender;
            Player = receiver;
            Title = title;
            Message = message;
            Attachments = attachments ?? new List<MailAttachment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
        public Guid Id { get; set; }

        // ✅ Jugador destinatario
        [JsonIgnore]
        public Guid PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }

        // ✅ Jugador remitente (relación completa)
        [JsonIgnore]
        public Guid SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public virtual Player SenderPlayer { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // ✅ Adjuntos serializados
        [Column("Attachments")]
        public string AttachmentsJson
        {
            get => JsonConvert.SerializeObject(Attachments);
            set => Attachments = JsonConvert.DeserializeObject<List<MailAttachment>>(value) ?? new List<MailAttachment>();
        }

        [NotMapped]
        public List<MailAttachment> Attachments { get; set; } = new();

        public void AddAttachment(Item item)
        {
            Attachments.Add(new MailAttachment
            {
                ItemId = item.ItemId,
                Quantity = item.Quantity,
                Properties = item.Properties
            });
        }

        public List<MailAttachment> GetAttachments() => Attachments;
    
    public static void GetMails(PlayerContext context, Player player)
        {
            // Cargamos los correos del jugador, incluyendo el remitente
            var mails = context.Player_MailBox
                .Where(m => m.PlayerId == player.Id)
                .Include(m => m.SenderPlayer) // ✅ Relación cargada
                .ToList();

            if (mails == null || mails.Count == 0)
            {
                player.MailBoxs = new List<MailBox>();
                return;
            }

            // Restauramos la lista en el objeto Player
            player.MailBoxs = mails;

            // Reconstruimos los adjuntos (deserialización)
            foreach (var mail in mails)
            {
                mail.Attachments = string.IsNullOrEmpty(mail.AttachmentsJson)
                    ? new List<MailAttachment>()
                    : JsonConvert.DeserializeObject<List<MailAttachment>>(mail.AttachmentsJson) ?? new List<MailAttachment>();
            }
        }
    }
    public class MailAttachment
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public ItemProperties Properties { get; set; } = new ItemProperties();
    }
}
