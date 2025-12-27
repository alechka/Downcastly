using Microsoft.CodeAnalysis;

namespace Downcastly;

internal class TypeInfo
{
    public bool SetsRequiredMembers { get; internal set; }

    public string ConstructorAccessibility { get; internal set; } = "public";

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

    internal List<string> Properties { get; set; } = [];

    internal string BaseName { get; set; } = null!;
    
}
