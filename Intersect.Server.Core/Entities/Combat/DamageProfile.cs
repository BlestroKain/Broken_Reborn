using Intersect.Enums;

namespace Intersect.Server.Entities.Combat;

public struct DamageProfile
{
    public DamageType DamageType { get; set; }
    public long Damage { get; set; }
    public long SecondaryDamage { get; set; }
}
