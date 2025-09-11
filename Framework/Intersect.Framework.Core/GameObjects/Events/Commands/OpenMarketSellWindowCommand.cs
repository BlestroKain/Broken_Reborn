using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Framework.Core.GameObjects.Events.Commands;
public class OpenMarketSellWindowCommand : EventCommand
{
    public override EventCommandType Type { get; } = EventCommandType.OpenMarketSell;
}
