using Intersect.Server.Entities;

namespace Intersect.Server.General;

public static class SpellMath
{
    public static long Scale(long value, int? level = null)
    {
        if (level.HasValue && level.Value > 1)
        {
            return value * level.Value;
        }
        return value;
    }
}
