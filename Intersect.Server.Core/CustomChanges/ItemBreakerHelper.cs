using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ItemBreakHelper
{
    // Lista global de todas las runas
    private static List<ItemDescriptor> AllRunes = new();

    /// <summary>
    /// Debe llamarse una vez al arrancar el servidor, tras cargar los ItemDescriptor.
    /// </summary>
    public static void InitializeRunes()
    {
        AllRunes = ItemDescriptor.Lookup.Values
            .OfType<ItemDescriptor>()
            .Where(d =>
                d.ItemType == ItemType.Resource &&
                d.Subtype == "Rune"
            )
            .ToList();
    }

    public static List<ItemDescriptor> CalculateRunesFromItem(ItemDescriptor item, ItemProperties? props)
    {
        var result = new List<ItemDescriptor>();

        int rarity = item.Rarity;
        int multiplier = 1 + rarity;

        // --- Procesar TODOS los Stats ---
        foreach (Stat stat in Enum.GetValues<Stat>())
        {
            int idx = (int)stat;
            int baseVal = item.StatsGiven[idx];
            int flatMod = props?.StatModifiers[idx] ?? 0;
            int percentMod = item.PercentageStatsGiven[idx];

            int totalStat = baseVal + flatMod;
            totalStat += (int)Math.Floor(totalStat * (percentMod / 100f));

            if (totalStat <= 0) continue;

            // Filtrar runas para este stat
            var pool = AllRunes
                .Where(r =>
                    r.TargetStat == stat &&
                    IsRuneAllowedForRarity(r, rarity)
                )
                .OrderBy(r => r.AmountModifier)
                .ToList();

            if (pool.Count == 0) continue;

            int guaranteed = (totalStat / 10) * multiplier;
            double extraP = (totalStat % 10) / 10.0;

            for (int i = 0; i < guaranteed; i++)
            {
                result.Add(RandomRune(pool));
            }
            if (Random.Shared.NextDouble() < extraP)
            {
                result.Add(RandomRune(pool));
            }
        }

        // --- Procesar TODOS los Vitals ---
        foreach (Vital vital in Enum.GetValues<Vital>())
        {
            int idx = (int)vital;
            int baseVal = (int)item.VitalsGiven[idx];
            int flatMod = props?.VitalModifiers[idx] ?? 0;

            int totalVital = baseVal + flatMod;
            if (totalVital <= 0) continue;

            // Filtrar runas para este vital
            var pool = AllRunes
                .Where(r =>
                    r.TargetVital == vital &&
                    IsRuneAllowedForRarity(r, rarity)
                )
                .OrderBy(r => r.AmountModifier)
                .ToList();

            if (pool.Count == 0) continue;

            int guaranteed = (totalVital / 20) * multiplier;
            double extraP = (totalVital % 20) / 20.0;

            for (int i = 0; i < guaranteed; i++)
            {
                result.Add(RandomRune(pool));
            }
            if (Random.Shared.NextDouble() < extraP)
            {
                result.Add(RandomRune(pool));
            }
        }

        return result;
    }

    private static bool IsRuneAllowedForRarity(ItemDescriptor rune, int rarity)
    {
        // Ejemplo de filtro por rareza
        if (rune.AmountModifier > 3 && rarity < 3) return false;
        return true;
    }

    private static ItemDescriptor RandomRune(List<ItemDescriptor> runes)
    {
        var idx = Random.Shared.Next(runes.Count);
        return runes[idx];
    }
}


