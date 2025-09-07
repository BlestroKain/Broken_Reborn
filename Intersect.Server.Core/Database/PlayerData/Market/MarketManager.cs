using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Network.Packets.Server;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;
using Intersect.Server.Networking;
using Intersect.Server.Database.PlayerData.Players;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Intersect.Server.Database.PlayerData.Market

{
    public static class MarketManager
    {
        public static (List<MarketListing> Listings, int Total) SearchMarket(
            int page,
            int pageSize,
            int? itemId = null,
            int? minPrice = null,
            int? maxPrice = null,
            bool? status = null,
            Guid? sellerId = null
        )
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: true);

            var query = context.Market_Listings.AsQueryable();

            if (!status.HasValue || !status.Value)
            {
                query = query.Where(l => l.ExpireAt > DateTime.UtcNow);
            }

            if (status.HasValue)
            {
                query = query.Where(l => l.IsSold == status.Value);
            }
            else
            {
                query = query.Where(l => !l.IsSold);
            }

            if (itemId.HasValue)
            {
                query = query.Where(l => l.ItemId == itemId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(l => l.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(l => l.Price <= maxPrice.Value);
            }

            if (sellerId.HasValue)
            {
                query = query.Where(l => l.SellerId == sellerId.Value);
            }

            var total = query.Count();

            var result = query
                .OrderBy(l => l.ListedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, total);
        }

        public static bool CreateListing(
            Player seller,
            int itemSlot,
            int quantity,
            long pricePerUnit,
            ItemProperties properties,
            bool autoSplit
        )
        {
            if (seller == null)
            {
                return false;
            }

            if (itemSlot < 0 || itemSlot >= seller.Items.Count)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidlisting, ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            var item = seller.Items[itemSlot];
            if (item == null || item.Quantity < quantity)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidlisting, ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            item.Properties = properties ?? item.Properties;
            return TryListItem(seller, item, quantity, (int)pricePerUnit, autoSplit);
        }

        public static bool TryListItem(Player seller, Item item, int quantity, int pricePerUnit, bool autoSplit = false)
        {
            if (seller == null || item == null || !seller.Items.Contains(item))
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidlisting, ChatMessageType.Error, CustomColors.Alerts.Error);
                Log.Warning("[MARKET:E01] Listing failed: item not owned by seller {SellerId}", seller?.Id);
                return false;
            }

            if (quantity <= 0)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidquantity, ChatMessageType.Error, CustomColors.Alerts.Error);
                Log.Warning("[MARKET:E02] Listing failed: invalid quantity {Quantity}", quantity);
                return false;
            }

            if (pricePerUnit <= 0)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.invalidprice, ChatMessageType.Error, CustomColors.Alerts.Error);
                Log.Warning("[MARKET:E03] Listing failed: invalid price {Price}", pricePerUnit);
                return false;
            }

            var itemDescriptor = ItemDescriptor.Get(item.ItemId);
            if (itemDescriptor == null || !itemDescriptor.CanSell || !itemDescriptor.CanDrop || !itemDescriptor.CanTrade)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.bounditem, ChatMessageType.Error, CustomColors.Alerts.Error);
                Log.Warning("[MARKET:E04] Listing failed: item is bound or trade-locked {ItemId}", item.ItemId);
                return false;
            }

            var stats = MarketStatisticsManager.GetStatistics(ItemDescriptor.ListIndex(item.ItemId));
            var min = stats.min;
            var max = stats.max;

            if (min > 0 && max > 0 && (pricePerUnit < min || pricePerUnit > max))
            {
                PacketSender.SendChatMsg(
                    seller,
                    Strings.Market.priceoutofrange.ToString(min, max),
                    ChatMessageType.Error,
                    CustomColors.Alerts.Declined
                );
                Log.Warning("[MARKET:E05] Listing failed: price {Price} outside allowed range {Min}-{Max}", pricePerUnit, min, max);
                return false;
            }

            var currencyBase = GetDefaultCurrency();
            if (currencyBase == null)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Error);
                Log.Error("[MARKET:E06] Currency item not configured");
                return false;
            }

            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.StopTrackingUsersExcept(seller.User);
            context.Attach(seller);

            var itemProperties = new ItemProperties(item.Properties);
            var packageSizes = autoSplit ? new[] { 1000,500, 100,50, 10, 1 } : new[] { quantity };

            var remaining = quantity;
            var atLeastOneListed = false;

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
                        SellerId = seller.Id,
                        ItemId = ItemDescriptor.ListIndex(item.ItemId),
                        Quantity = size,
                        Price = totalPrice,
                        ItemProperties = itemProperties,
                        ListedAt = DateTime.UtcNow,
                        ExpireAt = DateTime.UtcNow.AddDays(7),
                        IsSold = false
                    };

                    context.Market_Listings.Add(listing);
                    MarketStatisticsManager.RecordListing(listing.ItemId, totalPrice);
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
                Log.Error(ex, "[MARKET:E07] Error saving listings for seller {SellerId}", seller.Id);
                PacketSender.SendChatMsg(seller, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Error);
                return false;
            }

            if (atLeastOneListed)
            {
                PacketSender.SendRefreshMarket(seller);
                return true;
            }

            PacketSender.SendChatMsg(seller, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Error);
            Log.Warning("[MARKET:E08] Listing failed: could not split or list item for seller {SellerId}", seller.Id);
            return false;
        }

        private static int CalculateTax(int pricePerUnit, int quantity)
        {
            return (int)Math.Ceiling(pricePerUnit * quantity * GetMarketTaxPercentage());
        }


        public static async Task<bool> BuyAsync(Player buyer, Guid listingId)
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.StopTrackingUsersExcept(buyer.User);
            context.Attach(buyer);

            await using var tx = await context.Database.BeginTransactionAsync();

            var removedItems = new Dictionary<Guid, int>();
            Item itemToGive = null;
            var itemGiven = false;

            try
            {
                var listing = await context.Market_Listings
                    .Include(l => l.Seller)
                    .AsTracking()
                    .Where(l => l.Id == listingId)
                    .ForUpdate()
                    .FirstOrDefaultAsync();

                if (listing == null || listing.IsSold || listing.ExpireAt <= DateTime.UtcNow)
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.listingunavailable, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    Log.Warning("[MARKET:E10] Purchase failed: listing unavailable {ListingId}", listingId);
                    await tx.RollbackAsync();
                    return false;
                }

                if (listing.SellerId == buyer.Id)
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.ownlisting, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    Log.Warning("[MARKET:E11] Purchase failed: buyer attempted to purchase own listing {ListingId}", listingId);
                    await tx.RollbackAsync();
                    return false;
                }

                if (listing.Quantity <= 0)
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.invalidquantity, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    Log.Warning("[MARKET:E12] Purchase failed: invalid quantity for listing {ListingId}", listingId);
                    await tx.RollbackAsync();
                    return false;
                }

                if (listing.Price <= 0)
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.invalidprice, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    Log.Warning("[MARKET:E13] Purchase failed: invalid price for listing {ListingId}", listingId);
                    await tx.RollbackAsync();
                    return false;
                }

                var currencyBase = GetDefaultCurrency();
                if (currencyBase == null)
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Error);
                    Log.Error("[MARKET:E14] Currency item not configured");
                    await tx.RollbackAsync();
                    return false;
                }

                var totalCost = listing.Price;
                var currencyAmount = buyer.FindInventoryItemQuantity(currencyBase.Id);

                if (currencyAmount < totalCost)
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.notenoughmoney, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    Log.Warning("[MARKET:E15] Purchase failed: insufficient funds {BuyerId}", buyer.Id);
                    await tx.RollbackAsync();
                    return false;
                }

                itemToGive = new Item(ItemDescriptor.IdFromList(listing.ItemId), listing.Quantity) { Properties = listing.ItemProperties };
                if (!buyer.CanGiveItem(itemToGive.ItemId, itemToGive.Quantity))
                {
                    PacketSender.SendChatMsg(buyer, Strings.Market.inventoryfull, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    Log.Warning("[MARKET:E16] Purchase failed: inventory full for buyer {BuyerId}", buyer.Id);
                    await tx.RollbackAsync();
                    return false;
                }

                var currencySlots = buyer.FindInventoryItemSlots(currencyBase.Id);
                var remainingCost = totalCost;

                foreach (var itemSlot in currencySlots)
                {
                    var quantityToRemove = Math.Min(remainingCost, itemSlot.Quantity);
                    if (!buyer.TryTakeItem(itemSlot, (int)quantityToRemove))
                    {
                        foreach (var kvp in removedItems)
                        {
                            buyer.TryGiveItem(kvp.Key, kvp.Value);
                        }

                        PacketSender.SendChatMsg(buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined);
                        await tx.RollbackAsync();
                        return false;
                    }

                    removedItems[itemSlot.Id] = (int)quantityToRemove;
                    remainingCost -= quantityToRemove;

                    if (remainingCost <= 0)
                    {
                        break;
                    }
                }

                if (remainingCost > 0)
                {
                    foreach (var kvp in removedItems)
                    {
                        buyer.TryGiveItem(kvp.Key, kvp.Value);
                    }

                    PacketSender.SendChatMsg(buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined);
                    await tx.RollbackAsync();
                    return false;
                }

                buyer.TryGiveItem(itemToGive, -1);
                itemGiven = true;

                // Mark the listing as sold but keep it in the table for auditing.
                // A scheduled cleanup job will remove it later.
                listing.IsSold = true;

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
                await context.Market_Transactions.AddAsync(transaction);

                var goldAttachment = new MailAttachment
                {
                    ItemId = currencyBase.Id,
                    Quantity = (int)totalCost
                };

                var mail = new MailBox(
                    sender: buyer,
                    receiver: listing.Seller,
                    title: Strings.Market.salecompleted,
                    message: Strings.Market.yoursolditem.ToString(ItemDescriptor.GetName(ItemDescriptor.IdFromList(listing.ItemId))),
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
                    context.Player_MailBox.Add(mail);
                }

                await context.SaveChangesAsync();
                await tx.CommitAsync();

                MarketStatisticsManager.UpdateStatistics(transaction);

                return true;
            }
            catch (Exception ex)
            {
                if (itemGiven && itemToGive != null)
                {
                    buyer.TryTakeItem(itemToGive.ItemId, itemToGive.Quantity);
                }

                foreach (var kvp in removedItems)
                {
                    buyer.TryGiveItem(kvp.Key, kvp.Value);
                }

                await tx.RollbackAsync();
                Log.Error(ex, "Error buying market listing {ListingId}", listingId);
                PacketSender.SendChatMsg(buyer, Strings.Market.transactionfailed, ChatMessageType.Error, CustomColors.Alerts.Declined);
                return false;
            }
        }

        public static void CancelListing(Player seller, Guid listingId)
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            context.StopTrackingUsersExcept(seller.User);
            context.Attach(seller);

            var listing = context.Market_Listings.FirstOrDefault(l => l.Id == listingId && l.SellerId == seller.Id && !l.IsSold);
            if (listing == null)
            {
                PacketSender.SendChatMsg(seller, Strings.Market.listingunavailable, ChatMessageType.Error, CustomColors.Alerts.Declined);
                return;
            }

            context.Market_Listings.Remove(listing);

            if (!seller.TryGiveItem(ItemDescriptor.IdFromList(listing.ItemId), listing.Quantity, listing.ItemProperties))
            {
                var attachment = new MailAttachment
                {
                    ItemId = ItemDescriptor.IdFromList(listing.ItemId),
                    Quantity = listing.Quantity,
                    Properties = listing.ItemProperties
                };

                var mail = new MailBox(
                    sender: null,
                    receiver: seller,
                    title: "Listing canceled.",
                    message: "Your listing has been canceled.",
                    attachments: new List<MailAttachment> { attachment }
                );

                seller.MailBoxs.Add(mail);
                PacketSender.SendOpenMailBox(seller);
            }

            context.SaveChanges();
            PacketSender.SendRefreshMarket(seller);
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


        public static void CleanExpiredListings()
        {
            using var context = DbInterface.CreatePlayerContext(readOnly: false);
            var removable = context.Market_Listings
                .Where(l => l.IsSold || l.ExpireAt <= DateTime.UtcNow)
                .ToList();

            foreach (var listing in removable)
            {
                if (!listing.IsSold && listing.ExpireAt <= DateTime.UtcNow)
                {
                    var item = new MailAttachment
                    {
                        ItemId = ItemDescriptor.IdFromList(listing.ItemId),
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
                }

                context.Market_Listings.Remove(listing);
            }

            if (removable.Count > 0)
            {
                context.SaveChanges();
            }
        }

    }
}
