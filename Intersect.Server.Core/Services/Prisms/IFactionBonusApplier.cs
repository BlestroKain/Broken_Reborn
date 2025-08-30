using System;
using Intersect.Server.Entities;
using Intersect.Server.Database.Prisms;

namespace Intersect.Server.Services.Prisms;

/// <summary>
/// Applies faction based area bonuses to various calculations.
/// </summary>
public interface IFactionBonusApplier
{
    /// <summary>
    /// Applies the bonus multiplier for item drop calculations.
    /// </summary>
    /// <param name="player">Player for which the calculation is occurring.</param>
    /// <param name="value">Base value.</param>
    /// <returns>Modified value including any faction bonus.</returns>
    float ApplyDropBonus(Player player, float value);

    /// <summary>
    /// Applies the bonus multiplier for resource gathering calculations.
    /// </summary>
    float ApplyGatherBonus(Player player, float value);

    /// <summary>
    /// Applies the bonus multiplier for crafting calculations.
    /// </summary>
    float ApplyCraftBonus(Player player, float value);

    /// <summary>
    /// Clears any cached bonus for the specified prism.
    /// </summary>
    void ClearBonus(Guid prismId);

    /// <summary>
    /// Applies a faction area bonus making it available for future calculations.
    /// </summary>
    void ApplyBonus(FactionAreaBonus bonus);
}

