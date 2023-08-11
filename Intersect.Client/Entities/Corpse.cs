using Intersect.Network.Packets.Server;
using System;
using Intersect.Client.Framework.Entities;
using Intersect.Enums;
using Intersect.Utilities;

namespace Intersect.Client.Entities
{
    public partial class Corpse : Entity
    {
        public const long TIME_TO_RESPAWN = 10000; // TODO: refactor for get infinite time.
        public long TickCount { get; set; }

        public Corpse(Guid id, EntityPacket packet) : base(id, packet, EntityType.Player)
        {
        }

        public Corpse(Guid id, Entity entityBase) : base(id, null, EntityType.Player)
        {
            Id = id;
            X = entityBase.X;
            Y = entityBase.Y;
            Name = entityBase.Name;
            Sprite = entityBase.Sprite;
            Texture = entityBase.Texture;
            MapId = entityBase.MapId;
            SpriteAnimation = SpriteAnimations.Death;
            TickCount = Timing.Global.Milliseconds;
        }
    }
}