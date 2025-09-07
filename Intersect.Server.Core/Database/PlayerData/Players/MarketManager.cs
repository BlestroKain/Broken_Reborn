using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Network.Packets.Server;
using Intersect.Enums;

using Intersect.GameObjects;
using Intersect.Server.Networking;
using Intersect.Server.Database.PlayerData.Players;
using Microsoft.EntityFrameworkCore;
using Intersect.Logging;
using System.Diagnostics;

namespace Intersect.Server.Database.PlayerData.Players

{
    public static class MarketManager
    {
        public static List<MarketListing> SearchMarket(
     string name = "",
     int? minPrice = null,
     int? maxPrice = null,
     ItemType? type = null
 )
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: true);

            // 🔎 Consulta base: solo ítems no vendidos y dentro de fecha de expiración
            var query = context.Market_Listings
                .Where(l => !l.IsSold && l.ExpireAt > DateTime.UtcNow);

            // 💰 Filtros de precio
            if (minPrice.HasValue)
            {
                query = query.Where(l => l.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(l => l.Price <= maxPrice.Value);
            }

            var result = query.ToList();

            // 🧠 Filtrado avanzado (no puede hacerse en EF)
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(l =>
                {
                    var itemBase = ItemBase.Get(l.ItemId);
                    return itemBase != null &&
                           itemBase.Name.Contains(name, StringComparison.OrdinalIgnoreCase);
                }).ToList();
            }

            if (type.HasValue)
            {
                result = result.Where(l =>
                {
                    var itemBase = ItemBase.Get(l.ItemId);
                    return itemBase != null &&
                           itemBase.Type == GameObjectType.Item &&
                           itemBase.ItemType == type.Value;
                }).ToList();
            }

            return result;
        }

        public static bool TryListItem(Player seller, Item item, int quantity, int pricePerUnit, bool autoSplit = false)
        {
            if (item == null || quantity <= 0 || pricePerUnit <= 0)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidlisting, ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            var itemBase = ItemBase.Get(item.ItemId);
            if (itemBase == null || !itemBase.CanSell)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.cannotlist, ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            var stats = MarketStatisticsManager.GetStatistics(item.ItemId);
            var avg = stats.AveragePricePerUnit;
            var min = stats.GetMinAllowedPrice();
            var max = stats.GetMaxAllowedPrice();

            if (pricePerUnit < min || pricePerUnit > max)
            {
                PacketSender.SendChatMsg(
                    seller,
                    $"❌ Precio fuera del rango permitido para este ítem. Rango actual: {min} - {max} 🪙",
                    ChatMessageType.Error,
                    CustomColors.Alerts.Declined
                );
                return false;
            }

            var currencyBase = GetDefaultCurrency();
            if (currencyBase == null)
            {
                PacketSender.SendChatMsg(seller, "Currency item not configured!", ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.StopTrackingUsersExcept(seller.User);
            context.Attach(seller);

            var itemProperties = item.Properties;
            var packageSizes = autoSplit ? new[] { 1000,500, 100,50, 10, 1 } : new[] { quantity };

            int remaining = quantity;
            bool atLeastOneListed = false;

            foreach (var size in packageSizes)
            {
                while (remaining >= size)
                {
                    var tax = CalculateTax(pricePerUnit, size);
                    var totalPrice = pricePerUnit * size;

                    if (!seller.TryTakeItem(item.ItemId, size)) break;
                    if (!seller.TryTakeItem(currencyBase.Id, tax))
                    {
                        seller.TryGiveItem(item.ItemId, size);
                        break;
                    }

                    var listing = new MarketListing
                    {
                        Seller = seller,
                        ItemId = item.ItemId,
                        Quantity = size,
                        Price = totalPrice,
                        ItemProperties = itemProperties,
                        ListedAt = DateTime.UtcNow,
                        ExpireAt = DateTime.UtcNow.AddDays(7),
                        IsSold = false
                    };

                    context.Market_Listings.Add(listing);
                    remaining -= size;
                    atLeastOneListed = true;
                }
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Log.Error($"❌ Error guardando listados divididos: {ex}");
                PacketSender.SendChatMsg(seller, "❌ Error al guardar el listado en el mercado.", ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            if (atLeastOneListed)
            {
                //PacketSender.SendChatMsg(seller, "📤 Publicación realizada con éxito.", ChatMessageType.Trading, CustomColors.Alerts.Accepted);
                PacketSender.SendRefreshMarket(seller);
                return true;
            }

            PacketSender.SendChatMsg(seller, "❌ No se pudo dividir ni publicar el ítem.", ChatMessageType.Error, CustomColors.Alerts.Error);
            return false;
        }

        private static int CalculateTax(int pricePerUnit, int quantity)
        {
            return (int)Math.Ceiling(pricePerUnit * quantity * GetMarketTaxPercentage());
        }


        public static bool TryBuyListing(Player buyer, Guid listingId)
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.StopTrackingUsersExcept(buyer.User); // ✅ Prevenir tracking duplicado

            // ✅ Adjuntar comprador (en caso de que lo requiera EF)
            context.Attach(buyer);

            var listing = context.Market_Listings
                .Include(l => l.Seller)
                .FirstOrDefault(l => l.Id == listingId);

            if (listing == null || listing.IsSold || listing.ExpireAt <= DateTime.UtcNow)
            {
                PacketSender.SendChatMsg(buyer, Strings.Market.listingunavailable, ChatMessageType.Error, CustomColors.Alerts.Declined);
                return false;
            }

            var currencyBase = GetDefaultCurrency();
            if (currencyBase == null)
            {
                PacketSender.SendChatMsg(buyer, "Currency not configured.", ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            var totalCost = listing.Price;
            var currencyAmount = buyer.FindInventoryItemQuantity(currencyBase.Id);

            if (currencyAmount < totalCost)
            {
                PacketSender.SendChatMsg(buyer, Strings.Market.notenoughmoney, ChatMessageType.Error, CustomColors.Alerts.Declined);
                return false;
            }

            var itemToGive = new Item(listing.ItemId, listing.Quantity) { Properties = listing.ItemProperties };
            if (!buyer.CanGiveItem(itemToGive.ItemId, itemToGive.Quantity))
            {
                PacketSender.SendChatMsg(buyer, Strings.Market.inventoryfull, ChatMessageType.Error, CustomColors.Alerts.Declined);
                return false;
            }

            var currencySlots = buyer.FindInventoryItemSlots(currencyBase.Id);
            var remainingCost = totalCost;
            var removedItems = new Dictionary<Guid, int>();
            var success = true;

            foreach (var itemSlot in currencySlots)
            {
                var quantityToRemove = Math.Min(remainingCost, itemSlot.Quantity);
                if (!buyer.TryTakeItem(itemSlot, quantityToRemove))
                {
                    success = false;
                    break;
                }

                removedItems[itemSlot.Id] = quantityToRemove;
                remainingCost -= quantityToRemove;

                if (remainingCost <= 0) break;
            }

            if (!success || remainingCost > 0)
            {
                foreach (var kvp in removedItems)
                {
                    buyer.TryGiveItem(kvp.Key, kvp.Value);
                }

                PacketSender.SendChatMsg(buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined);
                return false;
            }

            // ✅ Marcar como vendido y dar el ítem
            listing.IsSold = true;
            context.Remove(listing);
            context.Update(listing);
            buyer.TryGiveItem(itemToGive, -1);

            var transaction = new MarketTransaction
            {
                ListingId = listing.Id,
                Seller = listing.Seller,
                BuyerName = buyer.Name,
                ItemId = listing.ItemId,
                Quantity = listing.Quantity,
                Price = listing.Price,
                ItemProperties = listing.ItemProperties
            };
            context.Market_Transactions.Add(transaction);

            var goldAttachment = new MailAttachment
            {
                ItemId = currencyBase.Id,
                Quantity = totalCost
            };

            var mail = new MailBox(
                sender: buyer,
                to: listing.Seller,
                title: Strings.Market.salecompleted,
                msg: Strings.Market.yoursolditem.ToString(ItemBase.GetName(listing.ItemId)),
                attachments: new List<MailAttachment> { goldAttachment }
            );

            var sellerOnline = Player.FindOnline(listing.Seller.Name);
            if (sellerOnline != null)
            {
                sellerOnline.MailBoxs.Add(mail);
                PacketSender.SendChatMsg(sellerOnline, Strings.Market.yoursolditem, ChatMessageType.Trading, CustomColors.Alerts.Accepted);
                PacketSender.SendOpenMailBox(sellerOnline);
            }
            else
            {
                // ❌ Ya está traqueado por EF, NO volver a hacer context.Attach(listing.Seller)
                context.Player_MailBox.Add(mail);
            }

            int retries = 3;
            while (retries > 0)
            {
                try
                {
                    context.SaveChanges();
                    break;
                }
                catch (DbUpdateException ex)
                {
                    Log.Error($"❌ Error de concurrencia al comprar ítem. Reintentos restantes: {retries - 1}", ex);
                    retries--;

                    if (retries == 0)
                    {
                        PacketSender.SendChatMsg(buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined);
                        return false;
                    }
                }
            }
            UpdateStatistics(transaction);

            PacketSender.SendChatMsg(buyer, Strings.Market.itempurchased, ChatMessageType.Trading, CustomColors.Alerts.Accepted);
            PacketSender.SendRefreshMarket(buyer); // Actualiza al cliente con nuevo mercado
            MarketStatisticsManager.UpdateStatistics(transaction);

            return true;
        }


        private const float DefaultMarketTax = 0.02f; // 2% por publicación

        public static float GetMarketTaxPercentage()
        {
            // Si en el futuro quieres hacer dinámico esto por tipo de ítem, ciudad, etc., puedes modificar aquí
            return DefaultMarketTax;
        }
        private static ItemBase GetDefaultCurrency()
        {
            return ItemBase.Lookup.Values
                .OfType<ItemBase>()
                .FirstOrDefault(i => i.ItemType == ItemType.Currency);
        }


        public static void CleanExpiredListings()
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            var expired = context.Market_Listings
                .Where(l => !l.IsSold && l.ExpireAt <= DateTime.UtcNow)
                .ToList();

            foreach (var listing in expired)
            {
                var item = new MailAttachment
                {
                    ItemId = listing.ItemId,
                    Quantity = listing.Quantity,
                    Properties = listing.ItemProperties
                };

                var mail = new MailBox(
                    sender: null,
                    to: listing.Seller,
                    title: Strings.Market.expiredlisting,
                    msg: Strings.Market.yourlistingexpired,
                    attachments: new List<MailAttachment> { item }
                );

                if (Player.FindOnline(listing.Seller.Name) is Player onlineSeller)
                {
                    onlineSeller.MailBoxs.Add(mail);
                    PacketSender.SendOpenMailBox(onlineSeller);
                }
                else
                {
                    context.Player_MailBox.Add(mail);
                }

                context.Market_Listings.Remove(listing);
            }

            if (expired.Count > 0)
            {
                context.SaveChanges();
            }
        }

        private static readonly Dictionary<Guid, MarketStatistics> _statisticsCache = new();

        public static void UpdateStatistics(MarketTransaction tx)
        {
            if (!_statisticsCache.TryGetValue(tx.ItemId, out var stats))
            {
                stats = new MarketStatistics(tx.ItemId);
                _statisticsCache[tx.ItemId] = stats;
            }

            stats.AddTransaction(tx);
        }

       

    }
}
