using Microsoft.CodeAnalysis;

namespace Downcastly;

internal class TypeInfo
{
    /// <summary>
    /// Gets a value indicating whether this member sets all required members during object initialization
    /// </summary>
    internal bool SetsRequiredMembers { get; set; }

    /// <summary>
    /// Constructor accessibility level
    /// </summary>
    internal string ConstructorAccessibility { get; set; } = "public";

    /// <summary>
    /// Type name
    /// </summary>
    internal string Name { get; init; } = string.Empty;

    /// <summary>
    /// Containing name space
    /// </summary>
    internal string Namespace { get; init; } = string.Empty;

    /// <summary>
    /// Accessibility level
    /// </summary>
    internal Accessibility Accessibility { get; set; }

    /// <summary>
    /// Type kind: class or record or whatever the future holds
    /// </summary>
    internal TypeKind Kind { get; init; } = TypeKind.Class;

    /// <summary>
    /// Properties that need to be copied
    /// </summary>
    internal List<string> Properties { get; set; } = [];

    /// <summary>
    /// Base type name
    /// </summary>
    internal string BaseName { get; set; } = null!;
    
}
