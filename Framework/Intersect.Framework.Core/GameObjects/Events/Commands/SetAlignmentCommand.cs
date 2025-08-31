using System.IO;
using Intersect.Enums;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class SetAlignmentCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.SetAlignment;

    public Factions Desired { get; set; } = Factions.Neutral;

    public bool IgnoreCooldown { get; set; }

    public bool IgnoreGuildLock { get; set; }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write((int)Desired);
        writer.Write(IgnoreCooldown);
        writer.Write(IgnoreGuildLock);
    }

    public void Deserialize(BinaryReader reader)
    {
        Desired = (Factions)reader.ReadInt32();
        IgnoreCooldown = reader.ReadBoolean();
        IgnoreGuildLock = reader.ReadBoolean();
    }
}
