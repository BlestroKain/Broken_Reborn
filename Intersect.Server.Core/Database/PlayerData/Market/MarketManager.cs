using System;
using System.Collections.Generic;
using System.Linq;
using Intersect;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Network.Packets.Server;
using Intersect.Enums;

using Intersect.GameObjects;
using Intersect.Server.Networking;
using Intersect.Server.Database.PlayerData.Players;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;
using Intersect.Framework.Core.GameObjects.Items;
using Serilog;

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
                    var itemBase = ItemDescriptor.Get(l.ItemId);
                    return itemBase != null &&
                           itemBase.Name.Contains(name, StringComparison.OrdinalIgnoreCase);
                }).ToList();
            }

            if (type.HasValue)
            {
                result = result.Where(l =>
                {
                    var itemBase = ItemDescriptor.Get(l.ItemId);
                    return itemBase != null &&
                           itemBase.Type == GameObjectType.Item &&
                           itemBase.ItemType == type.Value;
                }).ToList();
            }

            return result;
        }

        public static bool TryListItem(Player seller, Item item, int quantity, int pricePerUnit, bool autoSplit = false, int slotIndex = -1)
        {
            if (item == null || quantity <= 0 || pricePerUnit <= 0)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidlisting, ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            var itemBase = ItemDescriptor.Get(item.ItemId);
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

            var cooldown = Options.Instance.Market.ActionCooldownSeconds;
            if (cooldown > 0 && seller.LastMarketAction.HasValue)
            {
                var elapsed = DateTime.UtcNow - seller.LastMarketAction.Value;
                if (elapsed.TotalSeconds < cooldown)
                {
                    var waitsecond = (int)Math.Ceiling(cooldown - elapsed.TotalSeconds);
                    PacketSender.SendChatMsg(seller, $"⏳ Debes esperar {waitsecond}s antes de realizar otra operación en el mercado.", ChatMessageType.Error, CustomColors.Alerts.Error);
                    return false;
                }
            }

            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.StopTrackingUsersExcept(seller.User);
            context.Attach(seller);

            var maxListings = Options.Instance.Market.MaxActiveListings;
            var activeListings = context.Market_Listings.Count(l => l.Seller.Name == seller.Name && !l.IsSold && l.ExpireAt > DateTime.UtcNow);
            if (maxListings > 0 && activeListings >= maxListings)
            {
                PacketSender.SendChatMsg(seller, $"❌ Has alcanzado el máximo de listados activos ({maxListings}).", ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            var itemProperties = item.Properties;
            var packageSizes = autoSplit ? new[] { 1000,500, 100,50, 10, 1 } : new[] { quantity };

            bool isStackable = itemBase.Stackable;
            if (!isStackable)
            {
                if (slotIndex < 0 || slotIndex >= seller.Items.Count)
                {
                    PacketSender.SendChatMsg(seller, Strings.Market.invalidlisting, ChatMessageType.Error, CustomColors.Alerts.Error);
                    return false;
                }
            }

            int remaining = quantity;
            bool atLeastOneListed = false;
            bool hitLimit = false;

            foreach (var size in packageSizes)
            {
                while (remaining >= size)
                {
                    if (maxListings > 0 && activeListings >= maxListings)
                    {
                        hitLimit = true;
                        break;
                    }

                    var tax = CalculateTax(pricePerUnit, size);
                    var totalPrice = pricePerUnit * size;

                    bool tookItem;
                    if (isStackable)
                    {
                        tookItem = seller.TryTakeItem(item.ItemId, size);
                    }
                    else
                    {
                        tookItem = seller.TryTakeItem(seller.Items[slotIndex], size);
                    }
                    if (!tookItem) break;

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
                    activeListings++;
                }

                if (hitLimit)
                {
                    break;
                }
            }

            if (hitLimit)
            {
                PacketSender.SendChatMsg(seller, $"❌ Has alcanzado el máximo de listados activos ({maxListings}).", ChatMessageType.Error, CustomColors.Alerts.Error);
            }

            if (atLeastOneListed)
            {
                seller.LastMarketAction = DateTime.UtcNow;
                context.Update(seller);
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

            // Transacción para evitar ventas dobles y garantizar atomicidad
            using var tx = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);

            // Cargar listing + seller desde ESTE contexto
            var listing = context.Market_Listings
                .Include(l => l.Seller)
                .FirstOrDefault(l => l.Id == listingId);

            if (listing == null || listing.IsSold || listing.ExpireAt <= DateTime.UtcNow)
            {
                PacketSender.SendChatMsg(
                    buyer, Strings.Market.listingunavailable, ChatMessageType.Error, CustomColors.Alerts.Declined
                );
                return false;
            }

            // Moneda por defecto
            var currencyBase = GetDefaultCurrency();
            if (currencyBase == null)
            {
                PacketSender.SendChatMsg(
                    buyer, "Currency not configured.", ChatMessageType.Error, CustomColors.Alerts.Error
                );
                return false;
            }

            // Validaciones sobre el jugador "vivo" (inventario/dinero) – esto es parte del runtime del juego
            var totalCost = listing.Price;
            var currencyAmount = buyer.FindInventoryItemQuantity(currencyBase.Id);
            if (currencyAmount < totalCost)
            {
                PacketSender.SendChatMsg(
                    buyer, Strings.Market.notenoughmoney, ChatMessageType.Error, CustomColors.Alerts.Declined
                );
                return false;
            }

            var itemToGive = new Item(listing.ItemId, listing.Quantity) { Properties = listing.ItemProperties };
            if (!buyer.CanGiveItem(itemToGive.ItemId, itemToGive.Quantity))
            {
                PacketSender.SendChatMsg(
                    buyer, Strings.Market.inventoryfull, ChatMessageType.Error, CustomColors.Alerts.Declined
                );
                return false;
            }

            // Cobro: quitar moneda del comprador (runtime)
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
                // Revertir cobro en memoria si algo falló
                foreach (var kvp in removedItems)
                {
                    buyer.TryGiveItem(kvp.Key, kvp.Value);
                }

                PacketSender.SendChatMsg(
                    buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined
                );
                return false;
            }

            // Marcar vendido y quitar del mercado en BD
            listing.IsSold = true;
            context.Market_Listings.Remove(listing); // ✅ Remove es suficiente; no hagas Update también

            // Entregar ítem al comprador (runtime)
            buyer.TryGiveItem(itemToGive, -1);

            // Registrar transacción
            var transaction = new MarketTransaction
            {
                ListingId = listing.Id,
                Seller = listing.Seller,           // ya está trackeado por este contexto
                BuyerName = buyer.Name,
                ItemId = listing.ItemId,
                Quantity = listing.Quantity,
                Price = listing.Price,
                ItemProperties = listing.ItemProperties
            };
            context.Market_Transactions.Add(transaction);

            // Preparar mail con el oro para el vendedor
            var goldAttachment = new MailAttachment
            {
                ItemId = currencyBase.Id,
                Quantity = totalCost
            };

            var sellerOnline = Player.FindOnline(listing.Seller.Name);
            if (sellerOnline != null)
            {
                // En línea: usa el runtime y notifica
                var mailOnline = new MailBox(
                    sender: buyer,                       // runtime ok porque NO lo metemos en EF
                    receiver: sellerOnline,
                    title: Strings.Market.salecompleted,
                    message: Strings.Market.yoursolditem.ToString(ItemDescriptor.GetName(listing.ItemId)),
                    attachments: new List<MailAttachment> { goldAttachment }
                );

                sellerOnline.MailBoxs.Add(mailOnline);
                PacketSender.SendChatMsg(sellerOnline, Strings.Market.yoursolditem, ChatMessageType.Trading, CustomColors.Alerts.Accepted);
                PacketSender.SendOpenMailBox(sellerOnline);
            }
            else
            {
                // Offline: NUNCA uses la instancia 'buyer' del runtime dentro del contexto EF.
                // Crea un STUB del comprador y adjúntalo (o usa SenderId si tu modelo lo soporta).
                var buyerStub = context.Players.Local.FirstOrDefault(p => p.Id == buyer.Id)
                                ?? context.Attach(new Player { Id = buyer.Id }).Entity;

                var mailOffline = new MailBox(
                    sender: buyerStub,                 // stub adjuntado al contexto
                    receiver: listing.Seller,          // ya trackeado
                    title: Strings.Market.salecompleted,
                    message: Strings.Market.yoursolditem.ToString(ItemDescriptor.GetName(listing.ItemId)),
                    attachments: new List<MailAttachment> { goldAttachment }
                );

                context.Player_MailBox.Add(mailOffline);
            }

            try
            {
                context.SaveChanges();
                tx.Commit();
            }
            catch (Exception ex) // puedes acotar a DbUpdateConcurrencyException/DbUpdateException si prefieres
            {
                // Revertir cobro en memoria si la persistencia falló
                foreach (var kvp in removedItems)
                {
                    buyer.TryGiveItem(kvp.Key, kvp.Value);
                }

                PacketSender.SendChatMsg(
                    buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined
                );
                return false;
            }

            // Actualizaciones auxiliares
            UpdateStatistics(transaction);
            MarketStatisticsManager.UpdateStatistics(transaction);
            PacketSender.SendChatMsg(buyer, Strings.Market.itempurchased, ChatMessageType.Trading, CustomColors.Alerts.Accepted);
            PacketSender.SendRefreshMarket(buyer);

            return true;
        }



        private const float DefaultMarketTax = 0.02f; // 2% por publicación

        public static float GetMarketTaxPercentage()
        {
            // Si en el futuro quieres hacer dinámico esto por tipo de ítem, ciudad, etc., puedes modificar aquí
            return DefaultMarketTax;
        }
        private static ItemDescriptor GetDefaultCurrency()
        {
            return ItemDescriptor.Lookup.Values
                .OfType<ItemDescriptor>()
                .FirstOrDefault(i => i.ItemType == ItemType.Currency);
        }


        public static int CleanExpiredListings()
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
                    receiver: listing.Seller,
                    title: Strings.Market.expiredlisting,
                    message: Strings.Market.yourlistingexpired,
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

            return expired.Count;
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
