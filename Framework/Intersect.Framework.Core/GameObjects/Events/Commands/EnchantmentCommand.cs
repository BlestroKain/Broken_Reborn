using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;
public class OpenEnchantmentWindowCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.OpenEnchantment;
}
public class OpenMageWindowCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.OpenMage;
}
public class OpenBrokeItemWindowCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.OpenMage;
}
