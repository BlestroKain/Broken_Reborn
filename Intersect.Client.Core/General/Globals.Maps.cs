using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Config;
using Intersect.Framework.Core.Collections;

namespace Intersect.Client.General
{
    public static partial class Globals
    {
        /// <summary>
        /// Grid de descubrimiento por mapa. Cada grid es de tamaño
        /// (MapWidth x MapHeight) y ocupa ((W*H)+7)/8 bytes.
        /// </summary>
        public static readonly Dictionary<Guid, BitGrid> MapDiscoveries = new();

        /// <summary>
        /// Obtiene (o crea) la BitGrid de un mapa con las dimensiones de Options.
        /// </summary>
        private static BitGrid GetOrCreateDiscoveryGrid(Guid mapId)
        {
            if (mapId == Guid.Empty) throw new ArgumentException("Invalid map id.", nameof(mapId));

            if (!MapDiscoveries.TryGetValue(mapId, out var grid))
            {
                grid = new BitGrid(Options.Instance.Map.MapWidth, Options.Instance.Map.MapHeight);
                MapDiscoveries[mapId] = grid;
            }

            return grid;
        }

        /// <summary>
        /// Marca un tile como descubierto (silencioso si está fuera de rango).
        /// </summary>
        public static void DiscoverTile(Guid mapId, int x, int y)
        {
            if (mapId == Guid.Empty) return;

            var grid = GetOrCreateDiscoveryGrid(mapId);
            grid.Set(x, y);
        }

        /// <summary>
        /// Indica si un tile está descubierto (false si no hay grid o fuera de rango).
        /// </summary>
        public static bool IsTileDiscovered(Guid mapId, int x, int y)
        {
            return MapDiscoveries.TryGetValue(mapId, out var grid) && grid.Get(x, y);
        }

        /// <summary>
        /// Carga (reemplaza) todos los descubrimientos desde buffers brutos.
        /// Valida tamaños para evitar grids corruptas.
        /// </summary>
        public static void LoadDiscoveries(Dictionary<Guid, byte[]> data)
        {
            MapDiscoveries.Clear();

            if (data == null || data.Count == 0) return;

            var w = Options.Instance.Map.MapWidth;
            var h = Options.Instance.Map.MapHeight;
            var expected = BitGrid.GetExpectedSize(w, h);

            foreach (var kv in data)
            {
                if (kv.Key == Guid.Empty || kv.Value == null) continue;
                if (kv.Value.Length != expected) continue; // descartamos buffers mal formados

                // pasa validación — adoptamos el buffer
                MapDiscoveries[kv.Key] = new BitGrid(w, h, kv.Value);
            }
        }

        /// <summary>
        /// Exporta los descubrimientos a buffers brutos (copy-out) para enviarlos/guardar.
        /// </summary>
        public static Dictionary<Guid, byte[]> ExportDiscoveries()
        {
            var result = new Dictionary<Guid, byte[]>(MapDiscoveries.Count);

            foreach (var kv in MapDiscoveries)
            {
                // Copia defensiva para no exponer el buffer interno
                var clone = kv.Value.Clone();
                result[kv.Key] = clone.Data;
            }

            return result;
        }

        /// <summary>
        /// Convierte la grid de un mapa en una lista de runs (RLE por filas).
        /// Devuelve lista vacía si el mapa no existe o no hay bits en 1.
        /// </summary>
        public static List<BitGrid.Run> ToRuns(Guid mapId)
        {
            if (!MapDiscoveries.TryGetValue(mapId, out var grid))
            {
                return new List<BitGrid.Run>();
            }

            return grid.ExtractRuns();
        }

        /// <summary>
        /// Aplica runs (encendidos) a la grid del mapa (OR-in).
        /// Ignora runs fuera de rango.
        /// </summary>
        public static void MergeRuns(Guid mapId, IEnumerable<BitGrid.Run> runs)
        {
            if (mapId == Guid.Empty || runs == null) return;

            var grid = GetOrCreateDiscoveryGrid(mapId);
            grid.ApplyRuns(runs);
        }

        /// <summary>
        /// Hace OR de grids completas provenientes del servidor (mismo tamaño).
        /// Si no existe el mapa, lo crea. Si difiere en tamaño, ignora.
        /// </summary>
        public static void MergeDiscoveries(Dictionary<Guid, byte[]> data)
        {
            if (data == null || data.Count == 0) return;

            var w = Options.Instance.Map.MapWidth;
            var h = Options.Instance.Map.MapHeight;
            var expected = BitGrid.GetExpectedSize(w, h);

            foreach (var kv in data)
            {
                if (kv.Key == Guid.Empty || kv.Value == null) continue;
                if (kv.Value.Length != expected) continue;

                var incoming = new BitGrid(w, h, kv.Value);
                var local = GetOrCreateDiscoveryGrid(kv.Key);
                local.OrWith(incoming);
            }
        }

        /// <summary>
        /// Devuelve un snapshot (copia) de las grids actuales en bruto para requests.
        /// Útil cuando el flujo de red espera Dictionary&lt;Guid, byte[]&gt;.
        /// </summary>
        public static Dictionary<Guid, byte[]> SnapshotDiscoveries()
        {
            // Equivalente a ExportDiscoveries(); lo dejamos por semántica de "snapshot".
            return ExportDiscoveries();
        }
    }
}
