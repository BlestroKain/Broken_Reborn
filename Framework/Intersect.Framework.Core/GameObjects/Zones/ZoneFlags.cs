using System;

namespace Intersect.Framework.Core.GameObjects.Zones;

[Flags]
public enum ZoneFlags
{
    None = 0,

    // Players cannot engage in PVP combat inside this zone.
    NoPvp = 1 << 0,

    // Trading between players is disabled.
    NoTrading = 1 << 1,

    // Players may safely log out without delay.
    SafeLogout = 1 << 2,
}

