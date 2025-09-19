using Intersect.Enums;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;

public partial class SpawnPetCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.SpawnPet;

    public Guid PetId { get; set; }

    public Direction Dir { get; set; }

    // Pet tile spawn variables (spawns on the specified map tile when MapId is set)
    public Guid MapId { get; set; }

    // Pet entity spawn variables (spawns relative to the specified entity when EntityId is set)
    public Guid EntityId { get; set; }

    // Map coordinates or offsets relative to the pet spawn target
    public sbyte X { get; set; }

    public sbyte Y { get; set; }
}
