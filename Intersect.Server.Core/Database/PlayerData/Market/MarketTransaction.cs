using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Server.Entities;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Intersect.Server.Database.PlayerData.Market;



    public class MarketTransaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ListingId { get; set; }

        [Required]
        public string BuyerName { get; set; }

        [Required]
        public Guid SellerId { get; set; }

        [Required]
        [ForeignKey(nameof(SellerId))]
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


