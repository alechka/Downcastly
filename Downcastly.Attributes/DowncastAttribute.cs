using System;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Downcastly;
#pragma warning restore IDE0130 // Namespace does not match folder structure

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class DowncastAttribute : Attribute
{
    public DowncastAttribute()
    {
    }

    /// <summary>
    /// Adds [SetsRequiredMembers] attribute to the generated downcast constructor. 
    /// Use this if you have required members in your base class, but no requeried members in the derived class.
    /// </summary>
    public bool SetsRequeiredMembers { get; set; } = false;

    /// <summary>
    /// Accessibility level of the generated downcast constructor. Default is "public".
    /// </summary>
    public string ConstructorAccessibility { get; set; } = "public";
}
