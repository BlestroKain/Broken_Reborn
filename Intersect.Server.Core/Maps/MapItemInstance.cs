using System;
using System.Collections.Generic;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Newtonsoft.Json;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Server.Maps;


public partial class MapItem : Item
{

    [JsonIgnore] public int AttributeSpawnX = -1;

    [JsonIgnore] public int AttributeSpawnY = -1;

    [JsonIgnore] public long AttributeRespawnTime = Options.Instance.Map.ItemAttributeRespawnTime;

    [JsonIgnore] public long DespawnTime;

    public int X { get; private set; }

    public int Y { get; private set; } 

    [JsonIgnore] public int TileIndex => Y * Options.Instance.Map.MapWidth + X;

    /// <summary>
    /// The Unique Id of this particular MapItemInstance so we can refer to it elsewhere.
    /// </summary>
    public Guid UniqueId { get; private set; }

    public Guid Owner;

    [JsonIgnore] public long OwnershipTime;

    // We need this mostly for the client-side.. They can't keep track of our timer after all!
    public bool VisibleToAll = true;

    public MapItem(Guid itemId, int quantity, int x, int y, long respawnTime = 0) : base(itemId, quantity)
    {
        UniqueId = Guid.NewGuid();
        X = x;
        Y = y;

        if (respawnTime > 0)
        {
            AttributeRespawnTime = respawnTime;
        }
    }

    public MapItem(Guid itemId, int quantity, int x, int y, Guid? bagId, Bag bag) : base(itemId, quantity, bagId, bag)
    {
        UniqueId = Guid.NewGuid();
        X = x;
        Y = y;
    }

    public string Data()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    ///     Copies the relevant <see cref="ItemProperties"/> from the supplied item to this map item instance so
    ///     that dropped items retain their enchantment and other property values.
    /// </summary>
    /// <param name="item">The inventory item whose properties should be mirrored on this map item.</param>
    public void SetupProperties(Item item)
    {
        if (item?.Properties == null)
        {
            return;
        }

        // Ensure we have an ItemProperties object to work with
        Properties ??= new ItemProperties();

        Properties.EnchantmentLevel = item.Properties.EnchantmentLevel;
        Properties.MageSink = item.Properties.MageSink;

        if (Properties.StatModifiers != null && item.Properties.StatModifiers != null)
        {
            Array.Copy(item.Properties.StatModifiers, Properties.StatModifiers, Properties.StatModifiers.Length);
        }

        if (Properties.VitalModifiers != null && item.Properties.VitalModifiers != null)
        {
            Array.Copy(item.Properties.VitalModifiers, Properties.VitalModifiers, Properties.VitalModifiers.Length);
        }

        Properties.EnchantmentRolls = new Dictionary<int, int[]>(item.Properties.EnchantmentRolls);
    }

}
