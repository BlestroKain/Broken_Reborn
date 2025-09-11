using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using Intersect.Framework.Core.GameObjects.Items;


namespace Intersect.Server.Database.PlayerData
{
    public class MarketTransaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ListingId { get; set; }

        [Required]
        public string BuyerName { get; set; }

        [Required]
        public virtual Player Seller { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }

        [NotMapped]
        public ItemProperties ItemProperties { get; set; }

        public DateTime SoldAt { get; set; } = DateTime.UtcNow;
    }
}
