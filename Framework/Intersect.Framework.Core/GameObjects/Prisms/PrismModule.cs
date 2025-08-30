namespace Intersect.Framework.Core.GameObjects.Prisms;

/// <summary>
///     Represents a module attached to a prism.
/// </summary>
public class PrismModule
{
    /// <summary>
    ///     The module type.
    /// </summary>
    public PrismModuleType Type { get; set; }

    /// <summary>
    ///     Level of the module (1-3).
    /// </summary>
    public int Level { get; set; }
}

/// <summary>
///     Available module types for prisms.
/// </summary>
public enum PrismModuleType
{
    Vision,
    Prospecting
}

