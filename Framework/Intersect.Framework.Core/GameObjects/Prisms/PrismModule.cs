namespace Intersect.Framework.Core.GameObjects.Prisms;

/// <summary>
///     Represents a module attached to a prism. Modules can later be expanded with
///     additional behaviour; currently only the module name is stored.
/// </summary>
public class PrismModule
{
    /// <summary>
    ///     Name or identifier of the module.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

