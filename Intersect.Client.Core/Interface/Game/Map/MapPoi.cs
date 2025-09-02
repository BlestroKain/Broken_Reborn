using System;

namespace Intersect.Client.Interface.Game.Map
{
    public class MapPoi
    {
        public Guid MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string PoiType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
