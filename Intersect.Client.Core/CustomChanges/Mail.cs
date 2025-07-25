using System;
using System.Collections.Generic;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Network.Packets.Server;

namespace Intersect.Client
{
    public class Mail
    {
        public Mail(
            Guid mailId,
            string name,
            string message,
            string senderName,
            List<MailAttachment> attachments = null)
        {
            MailID = mailId;
            Name = name;
            Message = message;
            SenderName = senderName;
            Attachments = attachments ?? new List<MailAttachment>();
        }

        public Guid MailID { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string SenderName { get; set; }

        public List<MailAttachment> Attachments { get; set; }

        /// <summary>
        /// Add an item attachment to the mail.
        /// </summary>
        public void AddAttachment(Guid itemId, int quantity, ItemProperties properties = null)
        {
            Attachments.Add(new MailAttachment
            {
                ItemId = itemId,
                Quantity = quantity,
                Properties = properties ?? new ItemProperties()
            });
        }

        /// <summary>
        /// Get all item attachments.
        /// </summary>
        public List<MailAttachment> GetAttachments()
        {
            return Attachments;
        }
    }

    public class MailAttachment
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public ItemProperties Properties { get; set; } = new ItemProperties();

        public override string ToString()
        {
            return $"ItemID: {ItemId}, Quantity: {Quantity}, Properties: {Properties}";
        }
    }
}
