using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network;
using Intersect.Network.Packets;
using Intersect.Network.Packets.Client;
using Intersect.Server.Database;
using Intersect.Server.Database.Logging.Entities;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Utilities;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using Intersect.Core;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Crafting;
using Intersect.Framework.Core.GameObjects.Events;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.Maps;
using Intersect.Framework.Core.GameObjects.PlayerClass;
using Intersect.Framework.Core.Security;
using Intersect.Network.Packets.Server;
using Intersect.Server.Core;
using Microsoft.Extensions.Logging;
using ChatMsgPacket = Intersect.Network.Packets.Client.ChatMsgPacket;
using LoginPacket = Intersect.Network.Packets.Client.LoginPacket;
using PartyInvitePacket = Intersect.Network.Packets.Client.PartyInvitePacket;
using PingPacket = Intersect.Network.Packets.Client.PingPacket;
using TradeRequestPacket = Intersect.Network.Packets.Client.TradeRequestPacket;
using Serilog;

namespace Intersect.Server.Networking;

internal sealed partial class PacketHandler
{
    public void HandlePacket(Client client, GuildExpPercentagePacket packet)
    {
        var player = client?.Entity;
        if (player == null) return;

        player.SetGuildExpPercentage(packet.Percentage);
    }

    public void HandlePacket(Client client, ApplyGuildUpgradePacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        var guild = player.Guild;
        if (player?.Guild != null)
        {
            var success = player.Guild.ApplyUpgrade(packet.UpgradeType);

            if (success)
            {
                PacketSender.UpdateGuild(player);
                PacketSender.SendChatMsg(player, "¡Mejora aplicada exitosamente!", ChatMessageType.Notice);
            }
            else
            {
                PacketSender.SendChatMsg(player, "No se pudo aplicar la mejora.", ChatMessageType.Notice);
            }
        }
    }

    public void HandlePacket(Client client, CreateGuildPacket packet)
    {
        var player = client?.Entity;
        if (player == null)
        {
            return;
        }

        var success = false;

        // We only accept Strings as our Guild Names!
        if (FieldChecking.IsValidGuildName(packet.Name, Strings.Regex.GuildName))
        {
            // Is the name already in use?
            if (Guild.GetGuild(packet.Name) == null)
            {
                // Is the player already in a guild?
                if (player.Guild == null)
                {
                    // Finally, we can actually MAKE this guild happen!
                    var guild = Guild.CreateGuild(player, packet.Name);
                    if (guild != null)
                    {
                        // Set the guild logo
                        guild.SetLogo(packet.LogoBackground, packet.BackgroundR, packet.BackgroundG, packet.BackgroundB, packet.LogoSymbol, packet.SymbolR, packet.SymbolG, packet.SymbolB);

                        // Send them a welcome message!
                        PacketSender.SendChatMsg(player, Strings.Guilds.Welcome.ToString(packet.Name), ChatMessageType.Guild, CustomColors.Alerts.Success);

                        // Denote that we were successful.
                        success = true;
                    }
                }
                else
                {
                    // This cheeky bugger is already in a guild, tell him so!
                    PacketSender.SendChatMsg(player, Strings.Guilds.AlreadyInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                }
            }
            else
            {
                // This name already exists, oh dear!
                PacketSender.SendChatMsg(player, Strings.Guilds.GuildNameInUse, ChatMessageType.Guild, CustomColors.Alerts.Error);
            }
        }
        else
        {
            // Let our player know they need to adjust their name.
            PacketSender.SendChatMsg(player, Strings.Guilds.VariableInvalid, ChatMessageType.Guild, CustomColors.Alerts.Error);
        }

        if (success)
        {
            // Additional logic for success case if needed
            PacketSender.SendChatMsg(player, Strings.Guilds.GuildCreated, ChatMessageType.Guild, CustomColors.Alerts.Success);
            Log.Information($"Guild '{packet.Name}' created successfully by player '{player.Name}'.");
        }
        else
        {
            // Additional logic for failure case if needed
            PacketSender.SendChatMsg(player, Strings.Guilds.GuildCreationFailed, ChatMessageType.Guild, CustomColors.Alerts.Error);
            Log.Warning($"Failed to create guild '{packet.Name}' for player '{player.Name}'.");
        }
    }
    public void HandlePacket(Client client, EnchantItemPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }

        // Validar índice del ítem
        if (packet.ItemIndex < 0 || packet.ItemIndex >= player.Items.Count)
        {
            PacketSender.SendChatMsg(player, "Ítem no encontrado en el inventario.", ChatMessageType.Error);
            return;
        }

        var item = player.Items[packet.ItemIndex];
        if (item == null || item.Descriptor?.ItemType != ItemType.Equipment || item.Properties == null || !item.Descriptor.CanBeEnchanted())
        {
            PacketSender.SendChatMsg(player, "El ítem no es válido o no se puede encantar.", ChatMessageType.Error);
            return;
        }

        // Validar moneda de encantamiento
        var currency = player.Items.FirstOrDefault(i => i?.ItemId == packet.CurrencyId && i.Quantity >= packet.CurrencyAmount);
        if (currency == null)
        {
            PacketSender.SendChatMsg(player, "No tienes suficiente moneda para encantar el ítem.", ChatMessageType.Error);
            return;
        }

        // Validar existencia del amuleto si se quiere usar
        if (packet.UseAmulet)
        {
            var amuletId = item.Descriptor.GetamuletMaterialId();
            var amuletItem = player.Items.FirstOrDefault(i => i?.ItemId == amuletId && i.Quantity > 0);

            if (amuletItem == null)
            {
                PacketSender.SendChatMsg(player, "No tienes amuletos de protección para usar.", ChatMessageType.Error);
                return;
            }
        }

        // Ejecutar mejora
        player.TryUpgradeItem(packet.ItemIndex, packet.TargetLevel, packet.UseAmulet);

        // Refrescar inventario en cliente
        PacketSender.SendInventoryItemUpdate(player, packet.ItemIndex);
    }

    public void HandlePacket(Client client, UpgradeItemStatPacket packet)
    {
        var player = client.Entity;
        if (player == null) return;

        // 1) Validaciones...
        if (packet.ItemSlot < 0 || packet.ItemSlot >= player.Items.Count ||
            packet.RuneSlot < 0 || packet.RuneSlot >= player.Items.Count)
        {
            PacketSender.SendChatMsg(player, "Ítem o Runa inválido.", ChatMessageType.Error);
            return;
        }

        var equipment = player.Items[packet.ItemSlot];
        var rune = player.Items[packet.RuneSlot];

        if (equipment == null || rune == null)
        {
            PacketSender.SendChatMsg(player, "Faltan ítems para el proceso.", ChatMessageType.Error);
            return;
        }

        // 2) Aplica la runa
        if (!equipment.ApplyRuneUpgrade(equipment, rune, out var success, out var message))
        {
            PacketSender.SendChatMsg(player, message, ChatMessageType.Error);
            return;
        }

        // 3) Ajusta el slot en memoria, pero **no** quites la entidad de EF
        if (rune.Quantity <= 0)
        {
            rune.ItemId = Guid.Empty;
            rune.Quantity = 0;
            // opcional: limpia también sus Properties si hace falta
        }

        // 4) Guarda **solo** las dos filas de inventario
        using (var ctx = DbInterface.CreatePlayerContext(readOnly: false))
        {
            try
            {
                ctx.Player_Items.Update(equipment);
                ctx.Player_Items.Update(rune);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error guardando mejora de ítem con runa.");
                PacketSender.SendChatMsg(player, "Ocurrió un error al guardar en la base de datos.", ChatMessageType.Error);
                return;
            }
        }

        // 5) Notifica al usuario y refresca su inventario
        PacketSender.SendChatMsg(player, message, ChatMessageType.Notice);
        PacketSender.SendInventory(player);
    }


    public void HandlePacket(Client client, BrokeItemPacket packet)
    {
        var player = client.Entity;
        if (player == null)
        {
            return;
        }
        ItemBreakHelper.InitializeRunes();
        player.BreakItem(packet.ItemSlot);
    }
}
